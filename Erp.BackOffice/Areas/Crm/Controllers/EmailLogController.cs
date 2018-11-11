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
    public class EmailLogController : Controller
    {
        private readonly IEmailLogRepository EmailLogRepository;
        private readonly IUserRepository userRepository;

        public EmailLogController(IEmailLogRepository _EmailLog, IUserRepository _user)
        {
            EmailLogRepository = _EmailLog;
            userRepository = _user;
        }

        #region Index
        public ViewResult Index(string txtCustomer, string startDate, string endDate, string txtEmail, string TargetModule)
        {
            IEnumerable<EmailLogViewModel> q = EmailLogRepository.GetAllvwEmailLog()
                .Select(item => new EmailLogViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    ModifiedDate = item.ModifiedDate,
                    //Campaign = item.Campaign,
                    Customer = item.Customer,
                    Body = item.Body,
                    SentDate = item.SentDate,
                    Status = item.Status,
                    CustomerID = item.CustomerID,
                    //CampaignID = item.CampaignID,
                    Email = item.Email,
                    TargetModule = item.TargetModule
                }).OrderByDescending(m => m.ModifiedDate);
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];

           
             
            if (!string.IsNullOrEmpty(txtCustomer))
            {
                q = q.Where(item => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.Customer).ToLower().Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(txtCustomer).ToLower()));
            }
            if (!string.IsNullOrEmpty(txtEmail))
            {
                q = q.Where(item => item.Email == txtEmail);
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

        #region ListEmail
        public ViewResult ListEmail(string TargetModule, int? TargetID, bool? layout)
        {
            IEnumerable<EmailLogViewModel> q = EmailLogRepository.GetAllvwEmailLog()
                .Select(item => new EmailLogViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    ModifiedDate = item.ModifiedDate,
                    //Campaign = item.Campaign,
                    Customer = item.Customer,
                    Body = item.Body,
                    SentDate = item.SentDate,
                    Status = item.Status,
                    CustomerID = item.CustomerID,
                    //CampaignID = item.CampaignID,
                    TargetID = item.TargetID,
                    TargetModule = item.TargetModule,
                    Email = item.Email
                }).OrderByDescending(m => m.ModifiedDate);
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];

            //if (Campaign != null && Campaign.Value > 0)
            //{
            //    q = q.Where(item => item.CampaignID == Campaign);
            //}
             
            q = q.Where(item => item.TargetID == TargetID && item.TargetModule == TargetModule);
            
            ViewBag.layout = layout;
            return View(q);
        }
        #endregion

        #region Detail
        public ActionResult Detail(int? Id)
        {
            var EmailLog = EmailLogRepository.GetvwEmailLogById(Id.Value);
            if (EmailLog != null && EmailLog.IsDeleted != true)
            {
                var model = new EmailLogViewModel();
                AutoMapper.Mapper.Map(EmailLog, model);
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
        #endregion
        #region Send Email
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SendEmail(EmailLogViewModel model)
        {
            var urlRefer = Request["UrlReferrerDetail"];
            var emailLog = EmailLogRepository.GetvwEmailLogById(model.Id);
            if (emailLog != null && !string.IsNullOrEmpty(emailLog.Email))
            {
               
                string sentTo = emailLog.Email;
                string subject = emailLog.SubjectEmail;
                string body = emailLog.Body;
                //string cc = null;
                //string bcc = null;
                //string displayName = emailLog.Campaign;
                //string filePath = null;
                //string fileNameDisplayHasExtention = null;
                Erp.BackOffice.Helpers.Common.SendEmail( sentTo, subject, body);
                if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                {
                    return RedirectToAction("Detail", "EmailLog", new { area = "Crm", Id = model.Id, closePopup = "true" });
                }
                return Redirect(urlRefer);
            }
            return RedirectToAction("Index");
        }

        //public string EmailTemplate(EmailLogViewModel model)
        //{

        //} 
        #endregion

        #region Create
        public ViewResult Create( string TargetModule, int? TargetID)
        {
            var model = new EmailLogViewModel();
            model.TargetModule = TargetModule;
            model.TargetID = TargetID;
            
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(EmailLogViewModel model)
        {
            var urlRefer = Request["UrlReferrer"];
            if (ModelState.IsValid)
            {
                var EmailLog = new Domain.Crm.Entities.EmailLog();
                AutoMapper.Mapper.Map(model, EmailLog);
                EmailLog.IsDeleted = false;
                EmailLog.CreatedUserId = WebSecurity.CurrentUserId;
                EmailLog.ModifiedUserId = WebSecurity.CurrentUserId;
                EmailLog.CreatedDate = DateTime.Now;
                EmailLog.ModifiedDate = DateTime.Now;
                EmailLog.SentDate = DateTime.Now;
                EmailLog.Status = "Đã gửi";
                EmailLog.SubjectEmail = Erp.BackOffice.Helpers.Common.GetSetting("companyName") + " - " + EmailLog.TargetModule;
                EmailLogRepository.InsertEmailLog(EmailLog);
                
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                
                Erp.BackOffice.Helpers.Common.SendEmail(EmailLog.Email, EmailLog.TargetModule, EmailLog.Body);
                if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                {
                    ViewBag.closePopup = "true";
                    model.Id = EmailLog.Id;
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
            var EmailLog = EmailLogRepository.GetEmailLogById(Id.Value);
            if (EmailLog != null && EmailLog.IsDeleted != true)
            {
                var model = new EmailLogViewModel();
                AutoMapper.Mapper.Map(EmailLog, model);

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
        public ActionResult Edit(EmailLogViewModel model)
        {
            var urlRefer = Request["UrlReferrer"];
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var EmailLog = EmailLogRepository.GetEmailLogById(model.Id);
                    AutoMapper.Mapper.Map(model, EmailLog);
                    EmailLog.ModifiedUserId = WebSecurity.CurrentUserId;
                    EmailLog.ModifiedDate = DateTime.Now;
                    EmailLog.SentDate = DateTime.Now;
                    EmailLogRepository.InsertEmailLog(EmailLog);
                    Erp.BackOffice.Helpers.Common.SendEmail(EmailLog.Email, EmailLog.TargetModule, EmailLog.Body);
                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                    if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                    {
                        ViewBag.closePopup = "true";
                        model.Id = EmailLog.Id;
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
        public ActionResult Delete()
        {
            var urlRefer = Request["UrlReferrerDelete"];
            try
            {
                string idDeleteAll = Request["DeleteId-checkbox-Email"];
                string[] arrDeleteId = idDeleteAll.Split(',');
                for (int i = 0; i < arrDeleteId.Count(); i++)
                {
                    var item = EmailLogRepository.GetEmailLogById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                        {
                            TempData["FailedMessage"] = "NotOwner";
                            return RedirectToAction("Index");
                        }
                        item.IsDeleted = true;
                        EmailLogRepository.UpdateEmailLog(item);
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

        public static void SaveEmail(string email,string body, int? customerID, int TargetID, string TargetModule, string subj)
        {
            Erp.Domain.Crm.Repositories.EmailLogReponsitory emailLogRepository = new Erp.Domain.Crm.Repositories.EmailLogReponsitory(new Domain.Crm.ErpCrmDbContext());
            var EmailLog = new Domain.Crm.Entities.EmailLog();
            EmailLog.IsDeleted = false;
            EmailLog.CreatedUserId = WebSecurity.CurrentUserId;
            EmailLog.ModifiedUserId = WebSecurity.CurrentUserId;
            EmailLog.CreatedDate = DateTime.Now;
            EmailLog.ModifiedDate = DateTime.Now;
            EmailLog.SentDate = DateTime.Now;
            EmailLog.Status = "Đã gửi";
            EmailLog.Email = email;
            EmailLog.Body = body;
            EmailLog.CustomerID = customerID.Value;
            EmailLog.TargetID = TargetID;
            EmailLog.TargetModule = TargetModule;
            EmailLog.SubjectEmail = Erp.BackOffice.Helpers.Common.GetSetting("companyName") + " - " + subj;

            emailLogRepository.InsertEmailLog(EmailLog);
        }

    }
}
