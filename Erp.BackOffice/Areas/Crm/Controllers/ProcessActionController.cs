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
    public class ProcessActionController : Controller
    {
        private readonly IProcessRepository ProcessRepository;
        private readonly IUserRepository userRepository;
        private readonly IProcessActionRepository ProcessActionRepository;

        public ProcessActionController(
            IProcessRepository _Process
            , IUserRepository _user
            , IProcessActionRepository _ProcessAction
            )
        {
            ProcessRepository = _Process;
            userRepository = _user;
            ProcessActionRepository = _ProcessAction;
        }

        #region Index

        public ViewResult Index(int ProcessId)
        {
            IQueryable<ProcessActionViewModel> q = ProcessActionRepository.GetAllProcessAction()
                .Where(item => item.ProcessId == ProcessId)
                .Select(item => new ProcessActionViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    ActionType = item.ActionType,
                    OrderNo = item.OrderNo
                }).OrderByDescending(m => m.ModifiedDate);
            return View(q);
        }
        #endregion

        #region Create
        public ViewResult Create(int ProcessId, string ActionType, string ProcessEntity)
        {
            var model = new ProcessActionViewModel();
            model.ProcessId = ProcessId;
            model.ActionType = ActionType;
            var Process = ProcessRepository.GetProcessById(ProcessId);
            if (Process != null && Process.IsDeleted != true)
            {
                model.ProcessEntity = Process.DataSource;
            }

            switch (ActionType)
            {
                case "SendEmail":
                    model.EmailTemplateViewModel = new EmailTemplateViewModel();
                    model.EmailTemplateViewModel.From = Erp.BackOffice.Helpers.Common.GetSetting("Email");
                    break;
                case "CreateTask":
                    model.TaskTemplateViewModel = new TaskTemplateViewModel();
                    break;
                case "CreateNotifications":
                    model.TaskViewModel = new TaskViewModel();
                    break;
                default:
                    break;
            }
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ProcessActionViewModel model, bool IsPopup)
        {
            if (ModelState.IsValid)
            {
                var ProcessAction = new Domain.Crm.Entities.ProcessAction();
                AutoMapper.Mapper.Map(model, ProcessAction);
                ProcessAction.IsDeleted = false;
                ProcessAction.CreatedUserId = WebSecurity.CurrentUserId;
                ProcessAction.ModifiedUserId = WebSecurity.CurrentUserId;
                ProcessAction.CreatedDate = DateTime.Now;
                ProcessAction.ModifiedDate = DateTime.Now;
                switch (model.ActionType)
                {
                    case "SendEmail":
                        ProcessAction.TemplateObject = Erp.BackOffice.Helpers.Common.ObjectToByteArray(model.EmailTemplateViewModel);
                        break;
                    case "CreateTask":
                        ProcessAction.TemplateObject = Erp.BackOffice.Helpers.Common.ObjectToByteArray(model.TaskTemplateViewModel);
                        break;
                    case "CreateNotifications":
                        ProcessAction.TemplateObject = Erp.BackOffice.Helpers.Common.ObjectToByteArray(model.TaskViewModel);
                        break;
                    default:
                        break;
                }
                
                ProcessActionRepository.InsertProcessAction(ProcessAction);

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
            var ProcessAction = ProcessActionRepository.GetProcessActionById(Id.Value);
            if (ProcessAction != null && ProcessAction.IsDeleted != true)
            {
                var model = new ProcessActionViewModel();
                AutoMapper.Mapper.Map(ProcessAction, model);

                //if (model.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                //{
                //    TempData["FailedMessage"] = "NotOwner";
                //    return RedirectToAction("Index");
                //}

                var Process = ProcessRepository.GetProcessById(ProcessAction.ProcessId);
                if (Process != null && Process.IsDeleted != true)
                {
                    model.ProcessEntity = Process.DataSource;
                }
                
                switch (model.ActionType)
                {
                    case "SendEmail":
                        model.EmailTemplateViewModel = Erp.BackOffice.Helpers.Common.ByteArrayToObject(ProcessAction.TemplateObject) as EmailTemplateViewModel;
                        break;
                    case "CreateTask":
                        model.TaskTemplateViewModel = Erp.BackOffice.Helpers.Common.ByteArrayToObject(ProcessAction.TemplateObject) as TaskTemplateViewModel;
                        break;
                    case "CreateNotifications":
                        model.TaskViewModel = Erp.BackOffice.Helpers.Common.ByteArrayToObject(ProcessAction.TemplateObject) as TaskViewModel;
                        break;
                    default:
                        break;
                }
                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ProcessActionViewModel model, bool IsPopup)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var ProcessAction = ProcessActionRepository.GetProcessActionById(model.Id);
                    AutoMapper.Mapper.Map(model, ProcessAction);
                    ProcessAction.ModifiedUserId = WebSecurity.CurrentUserId;
                    ProcessAction.ModifiedDate = DateTime.Now;
                    switch (model.ActionType)
                    {
                        case "SendEmail":
                            ProcessAction.TemplateObject = Erp.BackOffice.Helpers.Common.ObjectToByteArray(model.EmailTemplateViewModel);
                            break;
                        case "CreateTask":
                            ProcessAction.TemplateObject = Erp.BackOffice.Helpers.Common.ObjectToByteArray(model.TaskTemplateViewModel);
                            break;
                        case "CreateNotifications":
                            ProcessAction.TemplateObject = Erp.BackOffice.Helpers.Common.ObjectToByteArray(model.TaskViewModel);
                            break;
                        default:
                            break;
                    }
                    ProcessActionRepository.UpdateProcessAction(ProcessAction);

                    if (IsPopup)
                    {
                        return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
                    }
                    else
                    {
                        TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
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
            var ProcessAction = ProcessActionRepository.GetProcessActionById(Id.Value);
            if (ProcessAction != null && ProcessAction.IsDeleted != true)
            {
                var model = new ProcessActionViewModel();
                AutoMapper.Mapper.Map(ProcessAction, model);

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
                    var item = ProcessActionRepository.GetProcessActionById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                        {
                            TempData["FailedMessage"] = "NotOwner";
                            return RedirectToAction("Index");
                        }

                        item.IsDeleted = true;
                        ProcessActionRepository.UpdateProcessAction(item);
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
