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
    public class KPIItemController : Controller
    {
        private readonly IKPIItemRepository KPIItemRepository;
        private readonly IUserRepository userRepository;

        public KPIItemController(
            IKPIItemRepository _KPIItem
            , IUserRepository _user
            )
        {
            KPIItemRepository = _KPIItem;
            userRepository = _user;
        }

        #region Index
        public ViewResult Index(int KPICatalogId)
        {
            IQueryable<KPIItemViewModel> q = KPIItemRepository.GetAllKPIItem()
                .Where(item => item.KPICatalogId == KPICatalogId)
                .Select(item => new KPIItemViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    KPIWeight = item.KPIWeight,
                    Measure = item.Measure,
                    TargetScore_From = item.TargetScore_From,
                    TargetScore_To = item.TargetScore_To,
                    Description = item.Description
                }).OrderBy(m => m.Name);
            return View(q);
        }
        #endregion

        #region Create
        public ViewResult Create(int KPICatalogId)
        {
            var model = new KPIItemViewModel();
            model.MeasureSelectList = Helpers.SelectListHelper.GetSelectList_Category("KPIItem_measure", null, "Value", App_GlobalResources.Wording.Empty);
            model.KPICatalogId = KPICatalogId;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(KPIItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                var KPIItem = new Domain.Staff.Entities.KPIItem();
                AutoMapper.Mapper.Map(model, KPIItem);
                KPIItem.IsDeleted = false;
                KPIItem.CreatedUserId = WebSecurity.CurrentUserId;
                KPIItem.ModifiedUserId = WebSecurity.CurrentUserId;
                KPIItem.CreatedDate = DateTime.Now;
                KPIItem.ModifiedDate = DateTime.Now;
                KPIItemRepository.InsertKPIItem(KPIItem);

                //TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("_ClosePopup", "Home", new { area = "" });
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var KPIItem = KPIItemRepository.GetKPIItemById(Id.Value);
            if (KPIItem != null && KPIItem.IsDeleted != true)
            {
                var model = new KPIItemViewModel();
                AutoMapper.Mapper.Map(KPIItem, model);

                //if (model.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                //{
                //    TempData["FailedMessage"] = "NotOwner";
                //    return RedirectToAction("Index");
                //}
                model.MeasureSelectList = Helpers.SelectListHelper.GetSelectList_Category("KPIItem_measure", null, "Value", App_GlobalResources.Wording.Empty);
                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(KPIItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var KPIItem = KPIItemRepository.GetKPIItemById(model.Id);
                    AutoMapper.Mapper.Map(model, KPIItem);
                    KPIItem.ModifiedUserId = WebSecurity.CurrentUserId;
                    KPIItem.ModifiedDate = DateTime.Now;
                    KPIItemRepository.UpdateKPIItem(KPIItem);

                    //TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                    return RedirectToAction("_ClosePopup", "Home", new { area = "" });
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
            var KPIItem = KPIItemRepository.GetKPIItemById(Id.Value);
            if (KPIItem != null && KPIItem.IsDeleted != true)
            {
                var model = new KPIItemViewModel();
                AutoMapper.Mapper.Map(KPIItem, model);

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
                    var item = KPIItemRepository.GetKPIItemById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        //if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                        //{
                        //    TempData["FailedMessage"] = "NotOwner";
                        //    return RedirectToAction("Index");
                        //}

                        item.IsDeleted = true;
                        KPIItemRepository.UpdateKPIItem(item);
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
