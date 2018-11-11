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
    public class KPICatalogController : Controller
    {
        private readonly IKPICatalogRepository KPICatalogRepository;
        private readonly IUserRepository userRepository;
        private readonly IKPIItemRepository KPIItemRepository;

        public KPICatalogController(
            IKPICatalogRepository _KPICatalog
            , IUserRepository _user
            , IKPIItemRepository _KPIItem
            )
        {
            KPICatalogRepository = _KPICatalog;
            userRepository = _user;
            KPIItemRepository = _KPIItem;
        }

        #region Index

        public ViewResult Index(string txtSearch)
        {

            IQueryable<KPICatalogViewModel> q = KPICatalogRepository.GetAllKPICatalog()
                .Select(item => new KPICatalogViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                }).OrderBy(m => m.Name);

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        #endregion

        #region Create
        public ViewResult Create()
        {
            var model = new KPICatalogViewModel();
            //model.DepartmentSelectList = Helpers.SelectListHelper.GetSelectList_BranchDepartment(null);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(KPICatalogViewModel model)
        {
            if (ModelState.IsValid)
            {
                var KPICatalog = new Domain.Staff.Entities.KPICatalog();
                AutoMapper.Mapper.Map(model, KPICatalog);
                KPICatalog.IsDeleted = false;
                KPICatalog.CreatedUserId = WebSecurity.CurrentUserId;
                KPICatalog.ModifiedUserId = WebSecurity.CurrentUserId;
                KPICatalog.CreatedDate = DateTime.Now;
                KPICatalog.ModifiedDate = DateTime.Now;
                KPICatalogRepository.InsertKPICatalog(KPICatalog);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var KPICatalog = KPICatalogRepository.GetKPICatalogById(Id.Value);
            if (KPICatalog != null && KPICatalog.IsDeleted != true)
            {
                var model = new KPICatalogViewModel();
                AutoMapper.Mapper.Map(KPICatalog, model);

                //if (model.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                //{
                //    TempData["FailedMessage"] = "NotOwner";
                //    return RedirectToAction("Index");
                //}
                //model.DepartmentSelectList = Helpers.SelectListHelper.GetSelectList_Department(null);
                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(KPICatalogViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var KPICatalog = KPICatalogRepository.GetKPICatalogById(model.Id);
                    AutoMapper.Mapper.Map(model, KPICatalog);
                    KPICatalog.ModifiedUserId = WebSecurity.CurrentUserId;
                    KPICatalog.ModifiedDate = DateTime.Now;
                    KPICatalogRepository.UpdateKPICatalog(KPICatalog);

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
            var KPICatalog = KPICatalogRepository.GetKPICatalogById(Id.Value);
            if (KPICatalog != null && KPICatalog.IsDeleted != true)
            {
                var model = new KPICatalogViewModel();
                AutoMapper.Mapper.Map(KPICatalog, model);

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
                    var item = KPICatalogRepository.GetKPICatalogById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        //if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                        //{
                        //    TempData["FailedMessage"] = "NotOwner";
                        //    return RedirectToAction("Index");
                        //}

                        item.IsDeleted = true;
                        KPICatalogRepository.UpdateKPICatalog(item);
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
