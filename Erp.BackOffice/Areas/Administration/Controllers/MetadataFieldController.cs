using Erp.BackOffice.Administration.Models;
using Erp.BackOffice.Filters;
using Erp.Domain.Entities;
using Erp.Domain.Interfaces;
using Erp.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Erp.BackOffice.Administration.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class MetadataFieldController : Controller
    {
        private readonly IMetadataFieldRepository MetadataFieldRepository;
        private readonly IUserRepository userRepository;

        public MetadataFieldController(
            IMetadataFieldRepository _MetadataField
            , IUserRepository _user
            )
        {
            MetadataFieldRepository = _MetadataField;
            userRepository = _user;
        }

        #region Index

        public ViewResult Index(int ModuleId)
        {
            IQueryable<MetadataFieldViewModel> q = MetadataFieldRepository.GetAllMetadataField()
                .Where(item => item.ModuleId == ModuleId)
                .Select(item => new MetadataFieldViewModel
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
            return View(q);
        }

        public JsonResult SelectList(string ModuleName)
        {
            List<SelectListItem> q = MetadataFieldRepository.GetAllMetadataField()
                .Where(item => item.ModuleName == ModuleName)
                .Select(item => new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Name
                }).ToList();
            return Json(q, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Create
        public ViewResult Create()
        {
            var model = new MetadataFieldViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(MetadataFieldViewModel model)
        {
            if (ModelState.IsValid)
            {
                var MetadataField = new Domain.Entities.MetadataField();
                AutoMapper.Mapper.Map(model, MetadataField);
                MetadataField.IsDeleted = false;
                MetadataField.CreatedUserId = WebSecurity.CurrentUserId;
                MetadataField.ModifiedUserId = WebSecurity.CurrentUserId;
                MetadataField.CreatedDate = DateTime.Now;
                MetadataField.ModifiedDate = DateTime.Now;
                MetadataFieldRepository.InsertMetadataField(MetadataField);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var MetadataField = MetadataFieldRepository.GetMetadataFieldById(Id.Value);
            if (MetadataField != null && MetadataField.IsDeleted != true)
            {
                var model = new MetadataFieldViewModel();
                AutoMapper.Mapper.Map(MetadataField, model);

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
        public ActionResult Edit(MetadataFieldViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var MetadataField = MetadataFieldRepository.GetMetadataFieldById(model.Id);
                    AutoMapper.Mapper.Map(model, MetadataField);
                    MetadataField.ModifiedUserId = WebSecurity.CurrentUserId;
                    MetadataField.ModifiedDate = DateTime.Now;
                    MetadataFieldRepository.UpdateMetadataField(MetadataField);

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
            var MetadataField = MetadataFieldRepository.GetMetadataFieldById(Id.Value);
            if (MetadataField != null && MetadataField.IsDeleted != true)
            {
                var model = new MetadataFieldViewModel();
                AutoMapper.Mapper.Map(MetadataField, model);

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
                    var item = MetadataFieldRepository.GetMetadataFieldById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                        {
                            TempData["FailedMessage"] = "NotOwner";
                            return RedirectToAction("Index");
                        }

                        item.IsDeleted = true;
                        MetadataFieldRepository.UpdateMetadataField(item);
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
