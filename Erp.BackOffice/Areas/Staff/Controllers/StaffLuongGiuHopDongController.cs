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
    public class StaffLuongGiuHopDongController : Controller
    {
        private readonly IStaffLuongGiuHopDongRepository StaffLuongGiuHopDongRepository;
        private readonly IUserRepository userRepository;

        public StaffLuongGiuHopDongController(
            IStaffLuongGiuHopDongRepository _StaffLuongGiuHopDong
            , IUserRepository _user
            )
        {
            StaffLuongGiuHopDongRepository = _StaffLuongGiuHopDong;
            userRepository = _user;
        }

        #region Index

        public ViewResult Index(string txtSearch)
        {

            IQueryable<StaffLuongGiuHopDongViewModel> q = StaffLuongGiuHopDongRepository.GetAllStaffLuongGiuHopDong()
                .Select(item => new StaffLuongGiuHopDongViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    TargetMonth = item.TargetMonth.Value,
                    TargetYear = item.TargetYear.Value,

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
            var model = new StaffLuongGiuHopDongViewModel();
            model.StaffId = Id;

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(StaffLuongGiuHopDongViewModel model)
        {
            if (ModelState.IsValid)
            {
                var StaffLuongGiuHopDong = new StaffLuongGiuHopDong();
                AutoMapper.Mapper.Map(model, StaffLuongGiuHopDong);
                StaffLuongGiuHopDong.IsDeleted = false;
                StaffLuongGiuHopDong.CreatedUserId = WebSecurity.CurrentUserId;
                StaffLuongGiuHopDong.ModifiedUserId = WebSecurity.CurrentUserId;
                StaffLuongGiuHopDong.AssignedUserId = WebSecurity.CurrentUserId;
                StaffLuongGiuHopDong.CreatedDate = DateTime.Now;
                StaffLuongGiuHopDong.ModifiedDate = DateTime.Now;
                //StaffLuongGiuHopDong.TargetMonth = DateTime.Now.Month;
                //StaffLuongGiuHopDong.TargetYear = DateTime.Now.Year;

                StaffLuongGiuHopDongRepository.InsertStaffLuongGiuHopDong(StaffLuongGiuHopDong);
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
            var StaffLuongGiuHopDong = StaffLuongGiuHopDongRepository.GetStaffLuongGiuHopDongById(Id.Value);
            if (StaffLuongGiuHopDong != null && StaffLuongGiuHopDong.IsDeleted != true)
            {
                var model = new StaffLuongGiuHopDongViewModel();
                AutoMapper.Mapper.Map(StaffLuongGiuHopDong, model);
                
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
        public ActionResult Edit(StaffLuongGiuHopDongViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var StaffLuongGiuHopDong = StaffLuongGiuHopDongRepository.GetStaffLuongGiuHopDongById(model.Id);
                    AutoMapper.Mapper.Map(model, StaffLuongGiuHopDong);
                    StaffLuongGiuHopDong.ModifiedUserId = WebSecurity.CurrentUserId;
                    StaffLuongGiuHopDong.ModifiedDate = DateTime.Now;
                    StaffLuongGiuHopDongRepository.UpdateStaffLuongGiuHopDong(StaffLuongGiuHopDong);
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
            var StaffLuongGiuHopDong = StaffLuongGiuHopDongRepository.GetStaffLuongGiuHopDongById(Id.Value);
            if (StaffLuongGiuHopDong != null && StaffLuongGiuHopDong.IsDeleted != true)
            {
                var model = new StaffLuongGiuHopDongViewModel();
                AutoMapper.Mapper.Map(StaffLuongGiuHopDong, model);
                
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
                    var item = StaffLuongGiuHopDongRepository.GetStaffLuongGiuHopDongById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if(item != null)
                    {
                        if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        {
                            TempData["FailedMessage"] = "NotOwner";
                            return RedirectToAction("Index");
                        }

                        item.IsDeleted = true;
                        StaffLuongGiuHopDongRepository.UpdateStaffLuongGiuHopDong(item);
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
