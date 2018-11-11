using System.Globalization;
using Erp.BackOffice.Staff.Models;
using Erp.BackOffice.Filters;
using Erp.BackOffice.Helpers;
using Erp.Domain.Entities;
using Erp.Domain.Interfaces;
using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Erp.Utilities;
using WebMatrix.WebData;
using Erp.BackOffice.Helpers;

namespace Erp.BackOffice.Staff.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class WorkingProcessController : Controller
    {
        private readonly IWorkingProcessRepository WorkingProcessRepository;
        private readonly IUserRepository userRepository;
        private readonly IBonusDisciplineRepository bonusDesciplineRepository;
        public WorkingProcessController(
            IWorkingProcessRepository _WorkingProcess
            , IUserRepository _user
            , IBonusDisciplineRepository bonusDescipline
            )
        {
            WorkingProcessRepository = _WorkingProcess;
            userRepository = _user;
            bonusDesciplineRepository = bonusDescipline;
        }

        #region Index

        public ViewResult Index(int? StaffId)
        {
            ViewBag.StaffId = StaffId;
            IEnumerable<WorkingProcessViewModel> q = WorkingProcessRepository.GetAllWorkingProcess().Where(x => x.StaffId == StaffId)
                .Select(item => new WorkingProcessViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    DayEnd = item.DayEnd,
                    DayStart = item.DayStart,
                    BonusDisciplineId = item.BonusDisciplineId,
                    Email = item.Email,
                    Phone = item.Phone,
                    Position = item.Position,
                    StaffId = item.StaffId,
                    WorkPlace = item.WorkPlace
                }).OrderByDescending(m => m.ModifiedDate).ToList();
            foreach (var item in q)
            {
                if (item.BonusDisciplineId > 0)
                {
                    var code = bonusDesciplineRepository.GetBonusDisciplineById(item.BonusDisciplineId.Value);
                    if (code != null)
                    {
                        item.BonusDiscipline = code.Code;
                    }
                    else
                    {
                        item.BonusDiscipline = "Đã xóa";
                    }
                }
                else
                {
                    item.BonusDiscipline = "Chưa có";
                }
            }
            ViewBag.AccessRightCreate = SecurityFilter.AccessRight("Create", "WorkingProcess", "Staff");
            return View(q);
        }
        #endregion

        #region Create
        public ViewResult Create(int? StaffId)
        {
            var model = new WorkingProcessViewModel();
            model.StaffId = StaffId;
            var BonusDisciplineList = bonusDesciplineRepository.GetAllvwBonusDiscipline().Where(x => x.StaffId == StaffId)
             .Select(item => new BonusDisciplineViewModel
             {
                 Code = item.Code,
                 Id = item.Id,
                 Formality = item.Formality,
                 Reason = item.Reason,
                 DayDecision = item.DayDecision,
                 DayEffective = item.DayEffective
             });
            ViewBag.BonusDisciplineList = BonusDisciplineList;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(WorkingProcessViewModel model)
        {
            var BonusDisciplineList = bonusDesciplineRepository.GetAllvwBonusDiscipline().Where(x => x.StaffId == model.StaffId)
            .Select(item => new BonusDisciplineViewModel
            {
                Code = item.Code,
                Id = item.Id,
                Formality = item.Formality,
                Reason = item.Reason,
                DayDecision = item.DayDecision,
                DayEffective = item.DayEffective
            });
            ViewBag.BonusDisciplineList = BonusDisciplineList;
            if (ModelState.IsValid)
            {
                var WorkingProcess = new Domain.Staff.Entities.WorkingProcess();
                AutoMapper.Mapper.Map(model, WorkingProcess);
                WorkingProcess.IsDeleted = false;
                WorkingProcess.CreatedUserId = WebSecurity.CurrentUserId;
                WorkingProcess.ModifiedUserId = WebSecurity.CurrentUserId;
                WorkingProcess.CreatedDate = DateTime.Now;
                WorkingProcess.ModifiedDate = DateTime.Now;
                WorkingProcessRepository.InsertWorkingProcess(WorkingProcess);
                if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                {
                    ViewBag.closePopup = "true";
                    model.Id = WorkingProcess.Id;
                    //model.TypeList = Helpers.SelectListHelper.GetSelectList_Category("DayOffType", null, "Name", false);
                    return View(model);
                }
                return RedirectToAction("Detail", "Staffs", new { area = "Staff", Id = model.StaffId });
            }
            return RedirectToAction("Create");
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var WorkingProcess = WorkingProcessRepository.GetWorkingProcessById(Id.Value);
            if (WorkingProcess != null && WorkingProcess.IsDeleted != true)
            {
                var model = new WorkingProcessViewModel();
                AutoMapper.Mapper.Map(WorkingProcess, model);
                var BonusDisciplineList = bonusDesciplineRepository.GetAllvwBonusDiscipline().Where(x => x.StaffId == model.StaffId)
                   .Select(item => new BonusDisciplineViewModel
                   {
                       Code = item.Code,
                       Id = item.Id,
                       Formality = item.Formality,
                       Reason = item.Reason,
                       DayDecision = item.DayDecision,
                       DayEffective = item.DayEffective
                   });
                ViewBag.BonusDisciplineList = BonusDisciplineList;
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
        public ActionResult Edit(WorkingProcessViewModel model)
        {
            var BonusDisciplineList = bonusDesciplineRepository.GetAllvwBonusDiscipline().Where(x => x.StaffId == model.StaffId)
              .Select(item => new BonusDisciplineViewModel
              {
                  Code = item.Code,
                  Id = item.Id,
                  Formality = item.Formality,
                  Reason = item.Reason,
                  DayDecision = item.DayDecision,
                  DayEffective = item.DayEffective
              });        
            ViewBag.BonusDisciplineList = BonusDisciplineList;
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var WorkingProcess = WorkingProcessRepository.GetWorkingProcessById(model.Id);
                    AutoMapper.Mapper.Map(model, WorkingProcess);
                    WorkingProcess.ModifiedUserId = WebSecurity.CurrentUserId;
                    WorkingProcess.ModifiedDate = DateTime.Now;
                    WorkingProcessRepository.UpdateWorkingProcess(WorkingProcess);

                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                    if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                    {
                        ViewBag.closePopup = "true";

                        return View(model);
                    }
                    return RedirectToAction("Detail", "Staffs", new { area = "Staff", Id = model.StaffId });
                }
            }

            return View(model);

            //if (Request.UrlReferrer != null)
            //    return Redirect(Request.UrlReferrer.AbsoluteUri);
            //return RedirectToAction("Index");
        }

        #endregion

        #region Delete
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            try
            {

                var item = WorkingProcessRepository.GetWorkingProcessById(id.Value);
                if (item != null)
                {
                    //if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                    //{
                    //    TempData["FailedMessage"] = "NotOwner";
                    //    return RedirectToAction("Index");
                    //}
                    //item.IsDeleted = true;
                    //TechniqueRepository.UpdateTechnique(item);
                    WorkingProcessRepository.DeleteWorkingProcess(id.Value);
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
