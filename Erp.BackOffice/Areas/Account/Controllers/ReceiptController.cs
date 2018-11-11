using System.Globalization;
using Erp.BackOffice.Account.Models;
using Erp.BackOffice.Filters;
using Erp.Domain.Entities;
using Erp.Domain.Interfaces;
using Erp.Domain.Account.Entities;
using Erp.Domain.Account.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Erp.Utilities;
using WebMatrix.WebData;
using Erp.BackOffice.Helpers;
using Erp.BackOffice.Sale.Models;
using Erp.Domain.Sale.Interfaces;

namespace Erp.BackOffice.Account.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class ReceiptController : Controller
    {
        private readonly IReceiptRepository ReceiptRepository;
        private readonly IUserRepository userRepository;
        private readonly ITransactionLiabilitiesRepository transactionRepository;
        private readonly IProcessPaymentRepository processPaymentRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly ITemplatePrintRepository templatePrintRepository;
        private readonly IReceiptDetailRepository ReceiptDetailRepository;
        public ReceiptController(
            IReceiptRepository _Receipt
            , IUserRepository _user
            , ITransactionLiabilitiesRepository _Transaction
            , IProcessPaymentRepository _ProcessPayment
            , ICustomerRepository _customer
            , ITemplatePrintRepository _templatePrint
            , IReceiptDetailRepository _ReceiptDetail
            )
        {
            ReceiptRepository = _Receipt;
            userRepository = _user;
            transactionRepository = _Transaction;
            processPaymentRepository = _ProcessPayment;
            customerRepository = _customer;
            templatePrintRepository = _templatePrint;
            ReceiptDetailRepository = _ReceiptDetail;
        }

        #region Index

        public ViewResult Index(int? CustomerId, int? SalerId, string Code)
        {
            var start = Request["start"];
            var end = Request["end"];
            var startDate = Request["startDate"];
            var endDate = Request["endDate"];
            IQueryable<ReceiptViewModel> q = ReceiptRepository.GetAllReceipt()
                .Select(item => new ReceiptViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    Code = item.Code,
                    Amount = item.Amount,
                    Address = item.Address,
                    Note = item.Note,
                    Payer = item.Payer,
                    SalerId = item.SalerId,
                    CompanyName = item.CompanyName,
                    VoucherDate = item.VoucherDate,
                    SalerName = item.SalerName,
                    CustomerId = item.CustomerId,
                }).OrderByDescending(x => x.CreatedDate);
            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                DateTime start_d;
                if (DateTime.TryParseExact(startDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out start_d))
                {
                    DateTime end_d;
                    if (DateTime.TryParseExact(endDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out end_d))
                    {
                        end_d = end_d.AddHours(23);
                        q = q.Where(x => start_d <= x.CreatedDate && x.CreatedDate <= end_d);
                    }
                }
            }
            if (!string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(end))
            {
                DateTime start_d;
                if (DateTime.TryParseExact(start, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out start_d))
                {
                    DateTime end_d;
                    if (DateTime.TryParseExact(end, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out end_d))
                    {
                        end_d = end_d.AddHours(23);
                        q = q.Where(x => start_d <= x.VoucherDate && x.VoucherDate <= end_d);
                    }
                }
            }
            if (CustomerId != null && CustomerId.Value > 0)
            {
                q = q.Where(x => x.CustomerId == CustomerId);
            }
            if (SalerId != null && SalerId.Value > 0)
            {
                q = q.Where(x => x.SalerId == SalerId);
            }
            if (!string.IsNullOrEmpty(Code))
            {
                q = q.Where(x => x.Code.Contains(Code));
            }
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        #endregion

        #region Create
        public ViewResult Create()
        {
            var model = new ReceiptViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ReceiptViewModel model)
        {
            if (ModelState.IsValid)
            {
                var receipt = new Receipt();
                AutoMapper.Mapper.Map(model, receipt);
                receipt.IsDeleted = false;
                receipt.CreatedUserId = WebSecurity.CurrentUserId;
                receipt.ModifiedUserId = WebSecurity.CurrentUserId;
                receipt.AssignedUserId = WebSecurity.CurrentUserId;
                receipt.CreatedDate = DateTime.Now;
                receipt.ModifiedDate = DateTime.Now;
                receipt.VoucherDate = DateTime.Now;
                ReceiptRepository.InsertReceipt(receipt);

                var receiptDetail = new ReceiptDetail();
                receiptDetail.IsDeleted = false;
                receiptDetail.CreatedUserId = WebSecurity.CurrentUserId;
                receiptDetail.ModifiedUserId = WebSecurity.CurrentUserId;
                receiptDetail.AssignedUserId = WebSecurity.CurrentUserId;
                receiptDetail.CreatedDate = DateTime.Now;
                receiptDetail.ModifiedDate = DateTime.Now;
                receiptDetail.Name = model.Name;
                receiptDetail.Amount = model.Amount;
                receiptDetail.ReceiptId = receipt.Id;
                ReceiptDetailRepository.InsertReceiptDetail(receiptDetail);

                var prefix = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_ReceiptOther");
                receipt.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix, receipt.Id);
                ReceiptRepository.UpdateReceipt(receipt);

                if (Request.IsAjaxRequest())
                {
                    return Content("success");
                }
                else
                {
                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var Receipt = ReceiptRepository.GetvwReceiptById(Id.Value);
            if (Receipt != null && Receipt.IsDeleted != true)
            {
                var model = new ReceiptViewModel();
                AutoMapper.Mapper.Map(Receipt, model);

                //if (model.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
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
        public ActionResult Edit(ReceiptViewModel model)
        {
            if (ModelState.IsValid)
            {
                //if (Request["Submit"] == "Save")
                //{
                    var Receipt = ReceiptRepository.GetReceiptById(model.Id);
                    AutoMapper.Mapper.Map(model, Receipt);
                    Receipt.ModifiedUserId = WebSecurity.CurrentUserId;
                    Receipt.ModifiedDate = DateTime.Now;
                    ReceiptRepository.UpdateReceipt(Receipt);

                    var receiptDetail = ReceiptDetailRepository.GetReceiptDetailByReceiptId(model.Id);
                    receiptDetail.ModifiedUserId = WebSecurity.CurrentUserId;
                    receiptDetail.ModifiedDate = DateTime.Now;
                    receiptDetail.Name = model.Name;
                    receiptDetail.Amount = model.Amount;
                    ReceiptDetailRepository.UpdateReceiptDetail(receiptDetail);
                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                    return RedirectToAction("Index");
                //}

                //return View(model);
            }

            return View(model);

            //if (Request.UrlReferrer != null)
            //    return Redirect(Request.UrlReferrer.AbsoluteUri);
            //return RedirectToAction("Index");
        }

        #endregion

        #region Detail
        public ActionResult Detail(int? Id, string TransactionCode)
        {
            var receipt = new vwReceipt();
            if (Id != null && Id.Value > 0)
            {
                receipt = ReceiptRepository.GetvwReceiptById(Id.Value);
            }

            if (!string.IsNullOrEmpty(TransactionCode))
            {
                receipt = ReceiptRepository.GetAllvwReceiptFull().Where(item => item.Code == TransactionCode).FirstOrDefault();
            }

            if (receipt != null)
            {
                var model = new ReceiptViewModel();
                AutoMapper.Mapper.Map(receipt, model);

               // model.ModifiedUserName = userRepository.GetUserById(model.ModifiedUserId.Value).FullName;

                ViewBag.SuccessMessage = TempData["SuccessMessage"];
                ViewBag.FailedMessage = TempData["FailedMessage"];
                ViewBag.AlertMessage = TempData["AlertMessage"];
                ViewBag.ReceiptDetail = ReceiptDetailRepository.GetAllReceiptDetailByReceiptId(model.Id).ToList();
                return View(model);
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        [HttpPost]
        public ActionResult Delete(int Id, string CancelReason, bool IsPopup)
        {
            var receipt = ReceiptRepository.GetReceiptById(Id);
            if (receipt != null)
            {
                receipt.IsDeleted = true;
                receipt.CancelReason = CancelReason;
                receipt.ModifiedUserId = WebSecurity.CurrentUserId;
                receipt.ModifiedDate = DateTime.Now;
                ReceiptRepository.UpdateReceipt(receipt);
                var receiptDetail = ReceiptDetailRepository.GetReceiptDetailByReceiptId(Id);
                receiptDetail.IsDeleted = true;
                receiptDetail.ModifiedUserId = WebSecurity.CurrentUserId;
                receiptDetail.ModifiedDate = DateTime.Now;
                ReceiptDetailRepository.UpdateReceiptDetail(receiptDetail);
            }

            TempData[Globals.SuccessMessageKey] = "Đã hủy chứng từ";

            if (IsPopup)
            {
                return RedirectToAction("Detail", new { Id = receipt.Id, IsPopup = IsPopup });
            }
            return RedirectToAction("Detail", new { Id = receipt.Id });
        }
        #endregion

        #region Print
        public ActionResult Print(int Id)
        {
            var model = new TemplatePrintViewModel();
            //lấy logo công ty
            //var logo = Erp.BackOffice.Helpers.Common.GetSetting("LogoCompany");
            var company = Erp.BackOffice.Helpers.Common.GetSetting("companyName");
            var address = Erp.BackOffice.Helpers.Common.GetSetting("addresscompany");
            var phone = Erp.BackOffice.Helpers.Common.GetSetting("phonecompany");
            var fax = Erp.BackOffice.Helpers.Common.GetSetting("faxcompany");
            var bankcode = Erp.BackOffice.Helpers.Common.GetSetting("bankcode");
            var bankname = Erp.BackOffice.Helpers.Common.GetSetting("bankname");
            //var ImgLogo = "<div class=\"logo\"><img src=" + logo + " height=\"73\" /></div>";
            //lấy phiếu chi.
            var receipt = ReceiptRepository.GetvwReceiptById(Id);

            //lấy template phiếu xuất.
            var template = templatePrintRepository.GetAllTemplatePrint().Where(x => x.Code.Contains("Receipt")).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            //truyền dữ liệu vào template.
            model.Content = template.Content;
            model.Content = model.Content.Replace("{Code}", receipt.Code);
            model.Content = model.Content.Replace("{Company}", receipt.CompanyName);
            model.Content = model.Content.Replace("{Customer}", receipt.Payer);
            model.Content = model.Content.Replace("{Address}", receipt.Address);
            model.Content = model.Content.Replace("{Reason}", receipt.Name);
            model.Content = model.Content.Replace("{Money}", Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(receipt.Amount,null));
            model.Content = model.Content.Replace("{VoucherDate}", receipt.VoucherDate.Value.ToShortDateString());

            model.Content = model.Content.Replace("{CreatedDate}", receipt.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm"));
            model.Content = model.Content.Replace("{SalerName}", receipt.SalerName);
            model.Content = model.Content.Replace("{MoneyText}", Erp.BackOffice.Helpers.Common.ChuyenSoThanhChu(receipt.Amount.ToString()));
            model.Content = model.Content.Replace("{Note}", receipt.Note);

            model.Content = model.Content.Replace("{System.CompanyName}", company);
            model.Content = model.Content.Replace("{System.AddressCompany}", address);
            model.Content = model.Content.Replace("{System.PhoneCompany}", phone);
            model.Content = model.Content.Replace("{System.Fax}", fax);
            model.Content = model.Content.Replace("{System.BankCodeCompany}", bankcode);
            model.Content = model.Content.Replace("{System.BankNameCompany}", bankname);
            return View(model);
        }
        #endregion
    }
}
