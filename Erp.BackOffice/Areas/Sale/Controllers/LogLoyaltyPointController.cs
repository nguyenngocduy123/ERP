using System.Globalization;
using Erp.BackOffice.Sale.Models;
using Erp.BackOffice.Filters;
using Erp.Domain.Entities;
using Erp.Domain.Interfaces;
using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Erp.Utilities;
using WebMatrix.WebData;
using Erp.BackOffice.Helpers;
using Erp.Domain.Account.Interfaces;

namespace Erp.BackOffice.Sale.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class LogLoyaltyPointController : Controller
    {
        private readonly ILoyaltyPointRepository loyaltyPointRepository;
        private readonly ILogLoyaltyPointRepository LogLoyaltyPointRepository;
        private readonly IUserRepository userRepository;
        private readonly ICustomerRepository customerRepository;
        public LogLoyaltyPointController(
            ILogLoyaltyPointRepository _LogLoyaltyPoint
            , IUserRepository _user
            ,ICustomerRepository _customer
            , ILoyaltyPointRepository loyaltyPoint
            )
        {
            LogLoyaltyPointRepository = _LogLoyaltyPoint;
            userRepository = _user;
            customerRepository = _customer;
            loyaltyPointRepository = loyaltyPoint;
        }

        #region Index

        public ViewResult Index(int? CustomerId)
        {
            IQueryable<LogLoyaltyPointViewModel> q = LogLoyaltyPointRepository.GetAllvwLogLoyaltyPoint()
                .Select(item => new LogLoyaltyPointViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    CustomerId = item.CustomerId,
                    //MemberCardCode=item.MemberCardCode,
                    //MemberCardId=item.MemberCardId,
                    PlusPoint=item.PlusPoint,
                    TotalAmount=item.TotalAmount,
                    TotalPoint=item.TotalPoint,
                    ProductInvoiceDate=item.ProductInvoiceDate,
                    ProductInvoiceCode=item.ProductInvoiceCode,
                    ProductInvoiceId=item.ProductInvoiceId
                }).OrderByDescending(m => m.ModifiedDate);
            if (CustomerId != null && CustomerId.Value > 0)
            {
                q = q.Where(x => x.CustomerId == CustomerId);
            }
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        #endregion

        #region CreateLogLoyaltyPoint
        public static void CreateLogLoyaltyPoint(int? CustomerId, decimal? totalAmount, int? ProductInvoiceId)
        {
            Erp.Domain.Sale.Repositories.LogLoyaltyPointRepository logLoyaltyPointRepository = new Erp.Domain.Sale.Repositories.LogLoyaltyPointRepository(new Domain.Sale.ErpSaleDbContext());
            Erp.Domain.Account.Repositories.CustomerRepository customerRepository = new Erp.Domain.Account.Repositories.CustomerRepository(new Domain.Account.ErpAccountDbContext());
            Erp.Domain.Sale.Repositories.LoyaltyPointRepository loyaltyPointRepository = new Erp.Domain.Sale.Repositories.LoyaltyPointRepository(new Domain.Sale.ErpSaleDbContext());
            
            int? point = 0;

            var min = loyaltyPointRepository.GetAllLoyaltyPoint().OrderBy(x => x.MinMoney).FirstOrDefault();
            if(totalAmount <  min.MinMoney)
                point = min.PlusPoint;
            else
            {
                var max = loyaltyPointRepository.GetAllLoyaltyPoint().OrderByDescending(x => x.MinMoney).FirstOrDefault();
                if(totalAmount > max.MaxMoney)
                    point = max.PlusPoint;
                else
                {
                    var setting_point = loyaltyPointRepository.GetAllLoyaltyPoint().Where(x => x.MinMoney <= totalAmount && totalAmount <= x.MaxMoney).FirstOrDefault();
                    point = setting_point != null ? setting_point.PlusPoint : 0;
                }
            }

            //kiểm tra thông tin khách hàng có tồn tại hay không và cập nhật điểm vào khách hàng
                var customer = customerRepository.GetCustomerById(CustomerId.Value);
                if (customer != null)
                {   
                    var logLoyaltyPoint = new LogLoyaltyPoint();
                    logLoyaltyPoint.IsDeleted = false;
                    logLoyaltyPoint.CreatedUserId = WebSecurity.CurrentUserId;
                    logLoyaltyPoint.ModifiedUserId = WebSecurity.CurrentUserId;
                    logLoyaltyPoint.AssignedUserId = WebSecurity.CurrentUserId;
                    logLoyaltyPoint.CreatedDate = DateTime.Now;
                    logLoyaltyPoint.ModifiedDate = DateTime.Now;

                    logLoyaltyPoint.PlusPoint = point;
                    logLoyaltyPoint.ProductInvoiceId = ProductInvoiceId;
                    if (customer.Point != null)
                    {
                        logLoyaltyPoint.TotalPoint = customer.Point + logLoyaltyPoint.PlusPoint;
                    }
                    else
                    {
                        logLoyaltyPoint.TotalPoint =logLoyaltyPoint.PlusPoint;
                    }
                    logLoyaltyPointRepository.InsertLogLoyaltyPoint(logLoyaltyPoint);
                    //cập nhật điểm vào khách hàng
                    customer.Point = logLoyaltyPoint.TotalPoint;
                    customerRepository.UpdateCustomer(customer);
                }
        }
        #endregion

        #region DeleteLogLoyaltyPoint
        public static void DeleteLogLoyaltyPoint(int? CustomerId, decimal? totalAmount, int? ProductInvoiceId)
        {
            Erp.Domain.Sale.Repositories.LogLoyaltyPointRepository logLoyaltyPointRepository = new Erp.Domain.Sale.Repositories.LogLoyaltyPointRepository(new Domain.Sale.ErpSaleDbContext());
            Erp.Domain.Account.Repositories.CustomerRepository customerRepository = new Erp.Domain.Account.Repositories.CustomerRepository(new Domain.Account.ErpAccountDbContext());
            Erp.Domain.Sale.Repositories.LoyaltyPointRepository loyaltyPointRepository = new Erp.Domain.Sale.Repositories.LoyaltyPointRepository(new Domain.Sale.ErpSaleDbContext());
            var setting_point = loyaltyPointRepository.GetAllLoyaltyPoint().Where(x => x.MinMoney <= totalAmount && totalAmount <= x.MaxMoney).ToList();

            //kiểm tra thông tin khách hàng có tồn tại hay không và cập nhật điểm vào khách hàng
            var customer = customerRepository.GetCustomerById(CustomerId.Value);
            if (customer != null)
            {
                int so_diem_cu_cua_hoa_don = 0;
                var LoyaltyPoint = logLoyaltyPointRepository.GetAllLogLoyaltyPoint().Where(x => x.CustomerId == CustomerId && x.ProductInvoiceId == ProductInvoiceId).ToList();
                if (LoyaltyPoint.Count() > 0)
                {
                    so_diem_cu_cua_hoa_don = LoyaltyPoint.FirstOrDefault().PlusPoint.Value;
                    //cập nhật lại điểm vào khách hàng khi bỏ ghi sổ hóa đơn
                    customer.Point = customer.Point - so_diem_cu_cua_hoa_don;
                    customerRepository.UpdateCustomer(customer);
                    //xóa lịch sử tích lũy điểm
                    logLoyaltyPointRepository.DeleteLogLoyaltyPoint(LoyaltyPoint.FirstOrDefault().Id);
                }
               
            }


        }
        #endregion
    }
}
