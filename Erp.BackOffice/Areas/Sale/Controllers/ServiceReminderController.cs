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

namespace Erp.BackOffice.Sale.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class ServiceReminderController : Controller
    {
        private readonly IServiceReminderRepository ServiceReminderRepository;
        private readonly IUserRepository userRepository;

        public ServiceReminderController(
            IServiceReminderRepository _ServiceReminder
            , IUserRepository _user
            )
        {
            ServiceReminderRepository = _ServiceReminder;
            userRepository = _user;
        }

        #region Index

        public ViewResult Index(string txtSearch)
        {

            IQueryable<ServiceReminderViewModel> q = ServiceReminderRepository.GetAllServiceReminder()
                .Select(item => new ServiceReminderViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    QuantityDate=item.QuantityDate,
                    Reminder=item.Reminder
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
            var model = new ServiceReminderViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ServiceReminderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var ServiceReminder = new ServiceReminder();
                AutoMapper.Mapper.Map(model, ServiceReminder);
                ServiceReminder.IsDeleted = false;
                ServiceReminder.CreatedUserId = WebSecurity.CurrentUserId;
                ServiceReminder.ModifiedUserId = WebSecurity.CurrentUserId;
                ServiceReminder.AssignedUserId = WebSecurity.CurrentUserId;
                ServiceReminder.CreatedDate = DateTime.Now;
                ServiceReminder.ModifiedDate = DateTime.Now;
                ServiceReminderRepository.InsertServiceReminder(ServiceReminder);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var ServiceReminder = ServiceReminderRepository.GetServiceReminderById(Id.Value);
            if (ServiceReminder != null && ServiceReminder.IsDeleted != true)
            {
                var model = new ServiceReminderViewModel();
                AutoMapper.Mapper.Map(ServiceReminder, model);
                
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
        public ActionResult Edit(ServiceReminderViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var ServiceReminder = ServiceReminderRepository.GetServiceReminderById(model.Id);
                    AutoMapper.Mapper.Map(model, ServiceReminder);
                    ServiceReminder.ModifiedUserId = WebSecurity.CurrentUserId;
                    ServiceReminder.ModifiedDate = DateTime.Now;
                    ServiceReminderRepository.UpdateServiceReminder(ServiceReminder);

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
            var ServiceReminder = ServiceReminderRepository.GetServiceReminderById(Id.Value);
            if (ServiceReminder != null && ServiceReminder.IsDeleted != true)
            {
                var model = new ServiceReminderViewModel();
                AutoMapper.Mapper.Map(ServiceReminder, model);
                
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
                    var item = ServiceReminderRepository.GetServiceReminderById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if(item != null)
                    {
                        //if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        //{
                        //    TempData["FailedMessage"] = "NotOwner";
                        //    return RedirectToAction("Index");
                        //}

                        item.IsDeleted = true;
                        ServiceReminderRepository.UpdateServiceReminder(item);
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
    }
}
