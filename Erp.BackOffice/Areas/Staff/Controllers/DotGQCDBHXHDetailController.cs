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
using Erp.Domain.Helper;

namespace Erp.BackOffice.Staff.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class DotGQCDBHXHDetailController : Controller
    {
        private readonly IDotGQCDBHXHDetailRepository DotGQCDBHXHDetailRepository;
        private readonly IUserRepository userRepository;

        public DotGQCDBHXHDetailController(
            IDotGQCDBHXHDetailRepository _DotGQCDBHXHDetail
            , IUserRepository _user
            )
        {
            DotGQCDBHXHDetailRepository = _DotGQCDBHXHDetail;
            userRepository = _user;
        }

        #region Index

        public ViewResult Index(string txtSearch)
        {

            IQueryable<DotGQCDBHXHDetailViewModel> q = DotGQCDBHXHDetailRepository.GetAllDotGQCDBHXHDetail()
                .Select(item => new DotGQCDBHXHDetailViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                     
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
            var model = new DotGQCDBHXHDetailViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(DotGQCDBHXHDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var DotGQCDBHXHDetail = new DotGQCDBHXHDetail();
                AutoMapper.Mapper.Map(model, DotGQCDBHXHDetail);
                DotGQCDBHXHDetail.IsDeleted = false;
                DotGQCDBHXHDetail.CreatedUserId = WebSecurity.CurrentUserId;
                DotGQCDBHXHDetail.ModifiedUserId = WebSecurity.CurrentUserId;
                DotGQCDBHXHDetail.AssignedUserId = WebSecurity.CurrentUserId;
                DotGQCDBHXHDetail.CreatedDate = DateTime.Now;
                DotGQCDBHXHDetail.ModifiedDate = DateTime.Now;
                DotGQCDBHXHDetailRepository.InsertDotGQCDBHXHDetail(DotGQCDBHXHDetail);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var DotGQCDBHXHDetail = DotGQCDBHXHDetailRepository.GetvwDotGQCDBHXHDetailById(Id.Value);
            if (DotGQCDBHXHDetail != null && DotGQCDBHXHDetail.IsDeleted != true)
            {
                var model = new DotGQCDBHXHDetailViewModel();
                AutoMapper.Mapper.Map(DotGQCDBHXHDetail, model);

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
        public ActionResult Edit(DotGQCDBHXHDetailViewModel model, bool IsPopup)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var DotGQCDBHXHDetail = DotGQCDBHXHDetailRepository.GetDotGQCDBHXHDetailById(model.Id);
                    AutoMapper.Mapper.Map(model, DotGQCDBHXHDetail);
                    DotGQCDBHXHDetail.ModifiedUserId = WebSecurity.CurrentUserId;
                    DotGQCDBHXHDetail.ModifiedDate = DateTime.Now;
                    DotGQCDBHXHDetailRepository.UpdateDotGQCDBHXHDetail(DotGQCDBHXHDetail);

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
            var DotGQCDBHXHDetail = DotGQCDBHXHDetailRepository.GetvwDotGQCDBHXHDetailById(Id.Value);
            if (DotGQCDBHXHDetail != null && DotGQCDBHXHDetail.IsDeleted != true)
            {
                var model = new DotGQCDBHXHDetailViewModel();
                AutoMapper.Mapper.Map(DotGQCDBHXHDetail, model);

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
        public ActionResult Delete(int Id)
        {
            try
            {
                var item = DotGQCDBHXHDetailRepository.GetDotGQCDBHXHDetailById(Id);
                if (item != null)
                {
                    if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                    {
                        TempData["FailedMessage"] = "NotOwner";
                        return RedirectToAction("Index");
                    }

                    item.IsDeleted = true;
                    DotGQCDBHXHDetailRepository.UpdateDotGQCDBHXHDetail(item);
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

        #region ReportList
        public ActionResult ReportList(int? BatchNumber, int? Month, int? Year)
        {
            if (BatchNumber == null)
            {
                BatchNumber = 0;
            }
            if (Month == null)
            {
                Month = DateTime.Now.Month;
            }
            if (Year == null)
            {
                Year = DateTime.Now.Year;
            }
            var DotBCBHXHDetail = SqlHelper.QuerySP<DotGQCDBHXHReport>("spStaff_DotGQCDBHXH", new
            {
                BatchNumber = BatchNumber,
                Month = Month,
                Year = Year,
            }).ToList();
            return View(DotBCBHXHDetail);
        }
        #endregion
    }
}
