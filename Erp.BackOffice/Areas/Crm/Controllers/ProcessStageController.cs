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
    public class ProcessStageController : Controller
    {
        private readonly IProcessStageRepository ProcessStageRepository;
        private readonly IUserRepository userRepository;

        public ProcessStageController(
            IProcessStageRepository _ProcessStage
            , IUserRepository _user
            )
        {
            ProcessStageRepository = _ProcessStage;
            userRepository = _user;
        }

        #region Index

        public ViewResult Index(int ProcessId, string ProcessEntity)
        {
            IQueryable<ProcessStageViewModel> q = ProcessStageRepository.GetAllProcessStage()
                .Where(item => item.ProcessId == ProcessId)
                .Select(item => new ProcessStageViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    OrderNo = item.OrderNo
                }).OrderBy(m => m.OrderNo);

            ViewBag.ProcessEntity = ProcessEntity;

            return View(q);
        }
        #endregion

        #region Create
        public ViewResult Create(int ProcessId)
        {
            var model = new ProcessStageViewModel();
            model.ProcessId = ProcessId;

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ProcessStageViewModel model, bool IsPopup)
        {
            if (ModelState.IsValid)
            {
                var ProcessStage = new ProcessStage();
                AutoMapper.Mapper.Map(model, ProcessStage);
                ProcessStage.IsDeleted = false;
                ProcessStage.CreatedUserId = WebSecurity.CurrentUserId;
                ProcessStage.ModifiedUserId = WebSecurity.CurrentUserId;
                ProcessStage.AssignedUserId = WebSecurity.CurrentUserId;
                ProcessStage.CreatedDate = DateTime.Now;
                ProcessStage.ModifiedDate = DateTime.Now;
                ProcessStageRepository.InsertProcessStage(ProcessStage);

                if (IsPopup)
                {
                    return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
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
            var ProcessStage = ProcessStageRepository.GetProcessStageById(Id.Value);
            if (ProcessStage != null && ProcessStage.IsDeleted != true)
            {
                var model = new ProcessStageViewModel();
                AutoMapper.Mapper.Map(ProcessStage, model);

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
        public ActionResult Edit(ProcessStageViewModel model, bool IsPopup)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var ProcessStage = ProcessStageRepository.GetProcessStageById(model.Id);
                    AutoMapper.Mapper.Map(model, ProcessStage);
                    ProcessStage.ModifiedUserId = WebSecurity.CurrentUserId;
                    ProcessStage.ModifiedDate = DateTime.Now;
                    ProcessStageRepository.UpdateProcessStage(ProcessStage);

                    if (IsPopup)
                    {
                        return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
                    }
                    else
                    {
                        TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                        return RedirectToAction("Index");
                    }
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
            var ProcessStage = ProcessStageRepository.GetProcessStageById(Id.Value);
            if (ProcessStage != null && ProcessStage.IsDeleted != true)
            {
                var model = new ProcessStageViewModel();
                AutoMapper.Mapper.Map(ProcessStage, model);

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
                    var item = ProcessStageRepository.GetProcessStageById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                        {
                            TempData["FailedMessage"] = "NotOwner";
                            return RedirectToAction("Index");
                        }

                        item.IsDeleted = true;
                        ProcessStageRepository.UpdateProcessStage(item);
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
