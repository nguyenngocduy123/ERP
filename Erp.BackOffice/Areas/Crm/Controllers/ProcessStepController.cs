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
    public class ProcessStepController : Controller
    {
        private readonly IProcessStepRepository ProcessStepRepository;
        private readonly IUserRepository userRepository;

        public ProcessStepController(
            IProcessStepRepository _ProcessStep
            , IUserRepository _user
            )
        {
            ProcessStepRepository = _ProcessStep;
            userRepository = _user;
        }

        #region Index

        public ViewResult Index(int StageId, string ProcessEntity)
        {
            IQueryable<ProcessStepViewModel> q = ProcessStepRepository.GetAllProcessStep()
                .Where(item => item.StageId == StageId)
                .Select(item => new ProcessStepViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    StepValue = item.StepValue,
                    IsRequired = item.IsRequired,
                    IsSequential = item.IsSequential,
                    OrderNo = item.OrderNo
                }).OrderBy(m => m.OrderNo);

            ViewBag.StageId = StageId;
            ViewBag.ProcessEntity = ProcessEntity;
            return View(q);
        }
        #endregion

        #region Create
        public ViewResult Create(int StageId, string ProcessEntity)
        {
            var model = new ProcessStepViewModel();
            model.StageId = StageId;
            model.ProcessEntity = ProcessEntity;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ProcessStepViewModel model, bool IsPopup)
        {
            if (ModelState.IsValid)
            {
                var ProcessStep = new ProcessStep();
                AutoMapper.Mapper.Map(model, ProcessStep);
                ProcessStep.IsDeleted = false;
                ProcessStep.CreatedUserId = WebSecurity.CurrentUserId;
                ProcessStep.ModifiedUserId = WebSecurity.CurrentUserId;
                ProcessStep.AssignedUserId = WebSecurity.CurrentUserId;
                ProcessStep.CreatedDate = DateTime.Now;
                ProcessStep.ModifiedDate = DateTime.Now;
                ProcessStepRepository.InsertProcessStep(ProcessStep);

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
        public ActionResult Edit(int? Id, string ProcessEntity)
        {
            var ProcessStep = ProcessStepRepository.GetProcessStepById(Id.Value);
            if (ProcessStep != null && ProcessStep.IsDeleted != true)
            {
                var model = new ProcessStepViewModel();
                AutoMapper.Mapper.Map(ProcessStep, model);
                
                if (model.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                {
                    TempData["FailedMessage"] = "NotOwner";
                    return RedirectToAction("Index");
                }

                model.ProcessEntity = ProcessEntity;

                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(ProcessStepViewModel model, bool IsPopup)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var ProcessStep = ProcessStepRepository.GetProcessStepById(model.Id);
                    AutoMapper.Mapper.Map(model, ProcessStep);
                    ProcessStep.ModifiedUserId = WebSecurity.CurrentUserId;
                    ProcessStep.ModifiedDate = DateTime.Now;
                    ProcessStepRepository.UpdateProcessStep(ProcessStep);

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
            var ProcessStep = ProcessStepRepository.GetProcessStepById(Id.Value);
            if (ProcessStep != null && ProcessStep.IsDeleted != true)
            {
                var model = new ProcessStepViewModel();
                AutoMapper.Mapper.Map(ProcessStep, model);
                
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
                    var item = ProcessStepRepository.GetProcessStepById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if(item != null)
                    {
                        if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                        {
                            TempData["FailedMessage"] = "NotOwner";
                            return RedirectToAction("Index");
                        }

                        item.IsDeleted = true;
                        ProcessStepRepository.UpdateProcessStep(item);
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
