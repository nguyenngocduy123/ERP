using System.Globalization;
using Erp.BackOffice.Staff.Models;
using Erp.BackOffice.Filters;
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
    public class StaffAllowanceController : Controller
    {
        private readonly IStaffAllowanceRepository StaffAllowanceRepository;
        private readonly IUserRepository userRepository;

        public StaffAllowanceController(
            IStaffAllowanceRepository _StaffAllowance
            , IUserRepository _user
            )
        {
            StaffAllowanceRepository = _StaffAllowance;
            userRepository = _user;
        }

        #region Index

        public ViewResult Index(string txtSearch)
        {

            IQueryable<StaffAllowanceViewModel> q = StaffAllowanceRepository.GetAllStaffAllowance()
                .Select(item => new StaffAllowanceViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,

                }).OrderByDescending(m => m.ModifiedDate);

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        #endregion

        #region Create
        public ViewResult Create(int Id)
        {
            var model = new StaffAllowanceViewModel();
            model.StaffId = Id;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(StaffAllowanceViewModel model)
        {
            if (ModelState.IsValid)
            {
                var StaffAllowance = new StaffAllowance();
                AutoMapper.Mapper.Map(model, StaffAllowance);
                StaffAllowance.IsDeleted = false;
                StaffAllowance.CreatedUserId = WebSecurity.CurrentUserId;
                StaffAllowance.ModifiedUserId = WebSecurity.CurrentUserId;
                StaffAllowance.AssignedUserId = WebSecurity.CurrentUserId;
                StaffAllowance.CreatedDate = DateTime.Now;
                StaffAllowance.ModifiedDate = DateTime.Now;
                //StaffAllowance.TargetMonth = DateTime.Now.Month;
                //StaffAllowance.TargetYear = DateTime.Now.Year;
                StaffAllowanceRepository.InsertStaffAllowance(StaffAllowance);
                if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                {
                    return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
                }
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var StaffAllowance = StaffAllowanceRepository.GetStaffAllowanceById(Id.Value);
            if (StaffAllowance != null && StaffAllowance.IsDeleted != true)
            {
                var model = new StaffAllowanceViewModel();
                AutoMapper.Mapper.Map(StaffAllowance, model);
                
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
        public ActionResult Edit(StaffAllowanceViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var StaffAllowance = StaffAllowanceRepository.GetStaffAllowanceById(model.Id);
                    AutoMapper.Mapper.Map(model, StaffAllowance);
                    StaffAllowance.ModifiedUserId = WebSecurity.CurrentUserId;
                    StaffAllowance.ModifiedDate = DateTime.Now;
                    StaffAllowanceRepository.UpdateStaffAllowance(StaffAllowance);
                    if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                    {
                        return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
                    }
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
            var StaffAllowance = StaffAllowanceRepository.GetStaffAllowanceById(Id.Value);
            if (StaffAllowance != null && StaffAllowance.IsDeleted != true)
            {
                var model = new StaffAllowanceViewModel();
                AutoMapper.Mapper.Map(StaffAllowance, model);
                
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
                    var item = StaffAllowanceRepository.GetStaffAllowanceById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if(item != null)
                    {
                        if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        {
                            TempData["FailedMessage"] = "NotOwner";
                            return RedirectToAction("Index");
                        }

                        item.IsDeleted = true;
                        StaffAllowanceRepository.UpdateStaffAllowance(item);
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
