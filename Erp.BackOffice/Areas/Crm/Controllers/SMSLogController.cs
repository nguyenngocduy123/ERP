using System.Globalization;
using Erp.BackOffice.Crm.Models;
using Erp.BackOffice.Filters;
using Erp.Domain.Entities;
using Erp.Domain.Interfaces;
using Erp.Domain.Crm.Entities;
using Erp.Domain.Crm.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Erp.Utilities;
using WebMatrix.WebData;
using Erp.BackOffice.Helpers;

namespace Erp.BackOffice.Crm.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class SMSLogController : Controller
    {
        private readonly ISMSLogRepository SMSLogRepository;
        private readonly IUserRepository userRepository;

        public SMSLogController(ISMSLogRepository _SMSLog, IUserRepository _user)
        {
            SMSLogRepository = _SMSLog;
            userRepository = _user;
        }

        #region Index
        public ViewResult Index(string txtCustomer, string startDate, string endDate, string txtPhone, string TargetModule)
        {
            IEnumerable<SMSLogViewModel> q = SMSLogRepository.GetAllvwSMSLog()
                .Select(item => new SMSLogViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Customer = item.Customer,
                    Body = item.Body,
                    SentDate=item.SentDate,
                    Status = item.Status,
                    CustomerID = item.CustomerID,
                    Phone = item.Phone,
                    TargetModule = item.TargetModule
                }).OrderByDescending(m => m.ModifiedDate);
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];



            if (!string.IsNullOrEmpty(txtCustomer))
            {
                q = q.Where(item => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.Customer).ToLower().Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(txtCustomer).ToLower()));
            }
            if (!string.IsNullOrEmpty(txtPhone))
            {
                q = q.Where(item => item.Phone == txtPhone);
            }
            DateTime d_startDate, d_endDate;
            if (DateTime.TryParseExact(startDate, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out d_startDate))
            {
                if (DateTime.TryParseExact(endDate, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out d_endDate))
                {
                    d_endDate = d_endDate.AddHours(23).AddMinutes(59);
                    q = q.Where(x => x.SentDate >= d_startDate && x.SentDate <= d_endDate);
                }
            }
            if (!string.IsNullOrEmpty(TargetModule))
            {
                q = q.Where(item => item.TargetModule == TargetModule);
            }
            return View(q);
        }
        #endregion

        #region ListSMS
        public ViewResult ListSMS(string TargetModule, int? TargetID, bool? layout)
        {
            IEnumerable<SMSLogViewModel> q = SMSLogRepository.GetAllvwSMSLog()
                .Select(item => new SMSLogViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    
                    Customer = item.Customer,
                    Body = item.Body,
                    SentDate = item.SentDate,
                    Status = item.Status,
                    CustomerID = item.CustomerID,
                    TargetID = item.TargetID,
                    TargetModule = item.TargetModule,
                    Phone = item.Phone
                }).OrderByDescending(m => m.ModifiedDate);
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];

            q = q.Where(item => item.TargetID == TargetID && item.TargetModule == TargetModule);
            ViewBag.layout = layout;
            return View(q);
        }
        #endregion

        #region Detail & SendSMS
        public ActionResult Detail(int? Id)
        {
            
            var SMSLog = SMSLogRepository.GetvwSMSLogById(Id.Value);
            if (SMSLog != null && SMSLog.IsDeleted != true)
            {
                var model = new SMSLogViewModel();
                AutoMapper.Mapper.Map(SMSLog, model);
                if (model.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
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
        // detail and sendSMS
        [HttpPost]
        public ActionResult SendSMS(SMSLogViewModel model)
        {
            var urlRefer = Request["UrlReferrerDetail"];
            var smsLog = SMSLogRepository.GetvwSMSLogById(model.Id);
            if (smsLog != null && !string.IsNullOrEmpty(smsLog.Phone))
            {
                 
                Erp.BackOffice.Helpers.Common.SendSMS(smsLog.Phone, smsLog.Body);
                if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                {
                    ViewBag.closePopup = "true";
                    ViewBag.urlRefer = urlRefer;
                    return RedirectToAction("Detail", "SMSLog", new { area = "Crm", Id = model.Id, closePopup = "true" });
                }
                return Redirect(urlRefer);
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Create
        public ViewResult Create(string TargetModule, int? TargetID)
        {
            var model = new SMSLogViewModel();
            model.TargetModule = TargetModule;
            model.TargetID = TargetID;
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(SMSLogViewModel model)
        {
            var urlRefer = Request["UrlReferrerCreate"];
            if (ModelState.IsValid)
            {
                var SMSLog = new Domain.Crm.Entities.SMSLog();
                AutoMapper.Mapper.Map(model, SMSLog);
                SMSLog.IsDeleted = false;
                SMSLog.CreatedUserId = WebSecurity.CurrentUserId;
                SMSLog.ModifiedUserId = WebSecurity.CurrentUserId;
                SMSLog.CreatedDate = DateTime.Now;
                SMSLog.ModifiedDate = DateTime.Now;
                SMSLog.SentDate = DateTime.Now;
                SMSLog.Status = "Đã gửi";
                SMSLogRepository.InsertSMSLog(SMSLog);
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                //send sms

                Erp.BackOffice.Helpers.Common.SendSMS(SMSLog.Phone,  SMSLog.Body);

                if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                {
                    ViewBag.closePopup = "true";
                    model.Id = SMSLog.Id;
                    ViewBag.urlRefer = urlRefer;
                    return View(model);
                }
                return Redirect(urlRefer);
            }
            return RedirectToAction("Create");
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var SMSLog = SMSLogRepository.GetSMSLogById(Id.Value);
            if (SMSLog != null && SMSLog.IsDeleted != true)
            {
                var model = new SMSLogViewModel();
                AutoMapper.Mapper.Map(SMSLog, model);

                if (model.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
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
        
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(SMSLogViewModel model)
        {
            var urlRefer = Request["UrlReferrerEdit"];
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var SMSLog = SMSLogRepository.GetSMSLogById(model.Id);
                    AutoMapper.Mapper.Map(model, SMSLog);
                    SMSLog.ModifiedUserId = WebSecurity.CurrentUserId;
                    SMSLog.ModifiedDate = DateTime.Now;
                    SMSLog.SentDate = DateTime.Now;
                    SMSLogRepository.InsertSMSLog(SMSLog);
                    //send sms
                    Erp.BackOffice.Helpers.Common.SendSMS(SMSLog.Phone, SMSLog.Body);
                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                    if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                    {
                        ViewBag.closePopup = "true";
                        model.Id = SMSLog.Id;
                        ViewBag.urlRefer = urlRefer;
                        return View(model);
                    }
                    return Redirect(urlRefer);
                }

                return RedirectToAction("Edit", new { Id = model.Id });
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        #endregion

        #region Delete
        [HttpPost]
        public ActionResult Delete( )
        {
            var urlRefer = Request["UrlReferrerDelete"];
            try
            {
                string idDeleteAll = Request["DeleteId-checkboxSMS"];
                string[] arrDeleteId = idDeleteAll.Split(',');
                for (int i = 0; i < arrDeleteId.Count(); i++)
                {
                    var item = SMSLogRepository.GetSMSLogById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                        {
                            TempData["FailedMessage"] = "NotOwner";
                            return RedirectToAction("Index");
                        }
                        item.IsDeleted = true;
                        SMSLogRepository.UpdateSMSLog(item);
                    }
                }
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.DeleteSuccess;
                return Redirect(urlRefer);
            }
            catch (DbUpdateException)
            {
                TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.RelationError;
                return RedirectToAction("Index");
            }
        }
        #endregion

        public static void SaveSMS(string phone,string body, int? customerID, int TargetID, string TargetModule, string subj)
        {
            Erp.Domain.Crm.Repositories.SMSLogRepository smsLogRepository = new Erp.Domain.Crm.Repositories.SMSLogRepository(new Domain.Crm.ErpCrmDbContext());
            var SMSLog = new Domain.Crm.Entities.SMSLog();
            SMSLog.IsDeleted = false;
            SMSLog.CreatedUserId = WebSecurity.CurrentUserId;
            SMSLog.ModifiedUserId = WebSecurity.CurrentUserId;
            SMSLog.CreatedDate = DateTime.Now;
            SMSLog.ModifiedDate = DateTime.Now;
            SMSLog.SentDate = DateTime.Now;
            SMSLog.Status = "Đã gửi";
            SMSLog.Body = body;
            SMSLog.Phone = phone;
            SMSLog.CustomerID = customerID.Value;
            SMSLog.TargetID = TargetID;
            SMSLog.TargetModule = TargetModule;

            smsLogRepository.InsertSMSLog(SMSLog);
        }
    }
}
