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
using Erp.Domain.Sale.Repositories;

namespace Erp.BackOffice.Sale.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class CommisionStaffController : Controller
    {
        private readonly ICommisionStaffRepository CommisionStaffRepository;
        private readonly IUserRepository userRepository;
        private readonly IProductInvoiceRepository productInvoiceRepository;
        public CommisionStaffController(
            ICommisionStaffRepository _CommisionStaff
            , IUserRepository _user
            , IProductInvoiceRepository productInvoice
            )
        {
            CommisionStaffRepository = _CommisionStaff;
            userRepository = _user;
            productInvoiceRepository = productInvoice;
        }

        #region Index

        public ViewResult Index(string txtSearch)
        {
            IQueryable<CommisionStaffViewModel> q = CommisionStaffRepository.GetAllCommisionStaff()
                .Select(item => new CommisionStaffViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                }).OrderByDescending(m => m.ModifiedDate);

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        #endregion

        #region Create
        public ViewResult Create()
        {
            var model = new CommisionStaffViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CommisionStaffViewModel model)
        {
            if (ModelState.IsValid)
            {
                var CommisionStaff = new CommisionStaff();
                AutoMapper.Mapper.Map(model, CommisionStaff);
                CommisionStaff.IsDeleted = false;
                CommisionStaff.CreatedUserId = WebSecurity.CurrentUserId;
                CommisionStaff.ModifiedUserId = WebSecurity.CurrentUserId;
                //CommisionStaff.AssignedUserId = WebSecurity.CurrentUserId;
                CommisionStaff.CreatedDate = DateTime.Now;
                CommisionStaff.ModifiedDate = DateTime.Now;
                CommisionStaffRepository.InsertCommisionStaff(CommisionStaff);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public static void Create(int InvoiceId)
        {
            ProductInvoiceRepository productInvoiceRepository = new Domain.Sale.Repositories.ProductInvoiceRepository(new Domain.Sale.ErpSaleDbContext());
            CommisionRepository CommisionRepository = new Domain.Sale.Repositories.CommisionRepository(new Domain.Sale.ErpSaleDbContext());
            CommisionStaffRepository commisionStaffRepository = new Domain.Sale.Repositories.CommisionStaffRepository(new Domain.Sale.ErpSaleDbContext());
            UsingServiceLogDetailRepository usingServiceLogDetailRepository = new Domain.Sale.Repositories.UsingServiceLogDetailRepository(new Domain.Sale.ErpSaleDbContext());
            var product_invoice = productInvoiceRepository.GetAllvwInvoiceDetailsByInvoiceId(InvoiceId).ToList();
            var usingServiceLogDetail = usingServiceLogDetailRepository.GetAllvwUsingServiceLogDetail().Where(x => x.ProductInvoiceId == InvoiceId).ToList();

            foreach (var item in product_invoice)
            {

                if (item.ProductType == "service")
                {
                    //lấy nhân viên làm dịch vụ của hóa đơn ra
                    var user = usingServiceLogDetail.Where(x => x.ServiceInvoiceDetailId == item.Id).OrderBy(x => x.CreatedDate).FirstOrDefault();
                    //kiểm tra xem nhân viên đó có được nhận hoa hồng hay k
                    var setting_commision = CommisionRepository.GetAllCommision().Where(x => x.StaffId == user.StaffId && x.ProductId == item.ProductId).ToList();
                    if (setting_commision.Count() > 0)
                    {
                        var CommisionStaff = new CommisionStaff();
                        CommisionStaff.IsDeleted = false;
                        CommisionStaff.CreatedUserId = WebSecurity.CurrentUserId;
                        CommisionStaff.ModifiedUserId = WebSecurity.CurrentUserId;
                        CommisionStaff.StaffId = user.StaffId.Value;
                        //nếu hoa hồng của nhân viên là % thì dựa vào hóa đơn tính ra số tiền chiết khấu
                        //ngược lại thì dựa vào số tiền chiết khấu tính ra %.
                        if (setting_commision.FirstOrDefault().IsMoney == false)
                        {
                            CommisionStaff.PercentOfCommision = Convert.ToInt32(setting_commision.FirstOrDefault().CommissionValue);
                            CommisionStaff.AmountOfCommision = (item.Price * item.Quantity) * setting_commision.FirstOrDefault().CommissionValue / 100;
                        }
                        else
                        {
                            CommisionStaff.AmountOfCommision = setting_commision.FirstOrDefault().CommissionValue;
                            CommisionStaff.PercentOfCommision = Convert.ToInt32(setting_commision.FirstOrDefault().CommissionValue/(item.Price * item.Quantity)* 100);
                        }
                        CommisionStaff.CreatedDate = DateTime.Now;
                        CommisionStaff.ModifiedDate = DateTime.Now;
                        CommisionStaff.InvoiceType = "ProductInvoice";
                        CommisionStaff.InvoiceId = item.ProductInvoiceId.Value;
                        CommisionStaff.InvoiceDetailId = item.Id;
                        CommisionStaff.BranchId = item.BranchId;
                        commisionStaffRepository.InsertCommisionStaff(CommisionStaff);
                    }
                }

            }

        }

        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var CommisionStaff = CommisionStaffRepository.GetCommisionStaffById(Id.Value);
            if (CommisionStaff != null && CommisionStaff.IsDeleted != true)
            {
                var model = new CommisionStaffViewModel();
                AutoMapper.Mapper.Map(CommisionStaff, model);

                //if (model.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                //{
                //    TempData["FailedMessage"] = "NotOwner";
                //    return RedirectToAction("Index");
                //}                

                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(CommisionStaffViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var CommisionStaff = CommisionStaffRepository.GetCommisionStaffById(model.Id);
                    AutoMapper.Mapper.Map(model, CommisionStaff);
                    CommisionStaff.ModifiedUserId = WebSecurity.CurrentUserId;
                    CommisionStaff.ModifiedDate = DateTime.Now;
                    CommisionStaffRepository.UpdateCommisionStaff(CommisionStaff);

                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                    return RedirectToAction("Index");
                }

                return View(model);
            }

            return View(model);

            //if (Request.UrlReferrer != null)
            //    return Redirect(Request.UrlReferrer.AbsoluteUri);
            //return RedirectToAction("Index");
        }

        #endregion

        #region Detail
        public ActionResult Detail(int? Id)
        {
            var CommisionStaff = CommisionStaffRepository.GetCommisionStaffById(Id.Value);
            if (CommisionStaff != null && CommisionStaff.IsDeleted != true)
            {
                var model = new CommisionStaffViewModel();
                AutoMapper.Mapper.Map(CommisionStaff, model);

                if (model.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                {
                    TempData["FailedMessage"] = "NotOwner";
                    return RedirectToAction("Index");
                }

                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        [HttpPost]
        public ActionResult Delete()
        {
            try
            {
                string idDeleteAll = Request["DeleteId-checkbox"];
                string[] arrDeleteId = idDeleteAll.Split(',');
                for (int i = 0; i < arrDeleteId.Count(); i++)
                {
                    var item = CommisionStaffRepository.GetCommisionStaffById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        //if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        //{
                        //    TempData["FailedMessage"] = "NotOwner";
                        //    return RedirectToAction("Index");
                        //}

                        item.IsDeleted = true;
                        CommisionStaffRepository.UpdateCommisionStaff(item);
                    }
                }
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.DeleteSuccess;
                return RedirectToAction("Index");
            }
            catch (DbUpdateException)
            {
                TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.RelationError;
                return RedirectToAction("Index");
            }
        }
        #endregion

        public static void DeleteCommisionStaff(int InvoiceId)
        {
            CommisionStaffRepository commisionStaffRepository = new Domain.Sale.Repositories.CommisionStaffRepository(new Domain.Sale.ErpSaleDbContext());
            var commision_staff = commisionStaffRepository.GetAllCommisionStaff().Where(x => x.InvoiceId == InvoiceId && x.InvoiceType == "ProductInvoice").ToList();
            for (int i = 0; i < commision_staff.Count(); i++)
            {
                commisionStaffRepository.DeleteCommisionStaff(commision_staff[i].Id);
            }

        }

        public static void CreateCommission(int InvoiceId)
        {
            ProductInvoiceRepository productInvoiceRepository = new Domain.Sale.Repositories.ProductInvoiceRepository(new Domain.Sale.ErpSaleDbContext());
            CommisionRepository CommisionRepository = new Domain.Sale.Repositories.CommisionRepository(new Domain.Sale.ErpSaleDbContext());
            CommisionStaffRepository commisionStaffRepository = new Domain.Sale.Repositories.CommisionStaffRepository(new Domain.Sale.ErpSaleDbContext());
            ProductOrServiceRepository productRepository = new Domain.Sale.Repositories.ProductOrServiceRepository(new Domain.Sale.ErpSaleDbContext());
            var product_invoice = productInvoiceRepository.GetAllvwInvoiceDetailsByInvoiceId(InvoiceId).Where(x=>x.StaffId!=null).ToList();

            foreach (var item in product_invoice)
            {
                var product = productRepository.GetAllvwProductAndService().Where(x => x.Id == item.ProductId).ToList();
                if (product.Count() > 0)
                    {
                        var setting_product = product.FirstOrDefault();
                        if (setting_product.DiscountStaff != null && setting_product.DiscountStaff > 0)
                        {
                            var CommisionStaff = new CommisionStaff();
                            CommisionStaff.IsDeleted = false;
                            CommisionStaff.CreatedUserId = WebSecurity.CurrentUserId;
                            CommisionStaff.ModifiedUserId = WebSecurity.CurrentUserId;
                            CommisionStaff.StaffId = item.StaffId.Value;
                            //nếu hoa hồng của nhân viên là % thì dựa vào hóa đơn tính ra số tiền chiết khấu
                            //ngược lại thì dựa vào số tiền chiết khấu tính ra %.
                            if (setting_product.IsMoneyDiscount == null || setting_product.IsMoneyDiscount==false)
                            {
                                CommisionStaff.PercentOfCommision = Convert.ToInt32(setting_product.DiscountStaff);
                                CommisionStaff.AmountOfCommision = (item.Price * item.Quantity) * setting_product.DiscountStaff / 100;
                            }
                            else
                            {
                                CommisionStaff.AmountOfCommision = setting_product.DiscountStaff;
                                CommisionStaff.PercentOfCommision = Convert.ToInt32(setting_product.DiscountStaff / (item.Price * item.Quantity) * 100);
                            }
                            CommisionStaff.CreatedDate = DateTime.Now;
                            CommisionStaff.ModifiedDate = DateTime.Now;
                            CommisionStaff.InvoiceType = "ProductInvoice";
                            CommisionStaff.InvoiceId = item.ProductInvoiceId.Value;
                            CommisionStaff.InvoiceDetailId = item.Id;
                            CommisionStaff.BranchId = item.BranchId;
                            commisionStaffRepository.InsertCommisionStaff(CommisionStaff);
                        }
                    }
                }

        }
    }
}
