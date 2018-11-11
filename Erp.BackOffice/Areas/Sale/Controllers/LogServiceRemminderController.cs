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
    public class LogServiceRemminderController : Controller
    {
        private readonly ILogServiceRemminderRepository LogServiceRemminderRepository;
        private readonly IUserRepository userRepository;

        public LogServiceRemminderController(
            ILogServiceRemminderRepository _LogServiceRemminder
            , IUserRepository _user
            )
        {
            LogServiceRemminderRepository = _LogServiceRemminder;
            userRepository = _user;
        }

        #region Index

        public ViewResult Index(string txtSearch)
        {

            IQueryable<LogServiceRemminderViewModel> q = LogServiceRemminderRepository.GetAllLogServiceRemminder()
                .Select(item => new LogServiceRemminderViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    //Name = item.Name,
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
            var model = new LogServiceRemminderViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(LogServiceRemminderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var LogServiceRemminder = new LogServiceRemminder();
                AutoMapper.Mapper.Map(model, LogServiceRemminder);
                LogServiceRemminder.IsDeleted = false;
                LogServiceRemminder.CreatedUserId = WebSecurity.CurrentUserId;
                LogServiceRemminder.ModifiedUserId = WebSecurity.CurrentUserId;
                LogServiceRemminder.AssignedUserId = WebSecurity.CurrentUserId;
                LogServiceRemminder.CreatedDate = DateTime.Now;
                LogServiceRemminder.ModifiedDate = DateTime.Now;
                LogServiceRemminderRepository.InsertLogServiceRemminder(LogServiceRemminder);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var LogServiceRemminder = LogServiceRemminderRepository.GetLogServiceRemminderById(Id.Value);
            if (LogServiceRemminder != null && LogServiceRemminder.IsDeleted != true)
            {
                var model = new LogServiceRemminderViewModel();
                AutoMapper.Mapper.Map(LogServiceRemminder, model);
                
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

        [HttpPost]
        public ActionResult Edit(LogServiceRemminderViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var LogServiceRemminder = LogServiceRemminderRepository.GetLogServiceRemminderById(model.Id);
                    AutoMapper.Mapper.Map(model, LogServiceRemminder);
                    LogServiceRemminder.ModifiedUserId = WebSecurity.CurrentUserId;
                    LogServiceRemminder.ModifiedDate = DateTime.Now;
                    LogServiceRemminderRepository.UpdateLogServiceRemminder(LogServiceRemminder);

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
            var LogServiceRemminder = LogServiceRemminderRepository.GetLogServiceRemminderById(Id.Value);
            if (LogServiceRemminder != null && LogServiceRemminder.IsDeleted != true)
            {
                var model = new LogServiceRemminderViewModel();
                AutoMapper.Mapper.Map(LogServiceRemminder, model);
                
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
                    var item = LogServiceRemminderRepository.GetLogServiceRemminderById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if(item != null)
                    {
                        if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        {
                            TempData["FailedMessage"] = "NotOwner";
                            return RedirectToAction("Index");
                        }

                        item.IsDeleted = true;
                        LogServiceRemminderRepository.UpdateLogServiceRemminder(item);
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
