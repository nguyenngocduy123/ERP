using System.Globalization;
using Erp.BackOffice.Administration.Models;
using Erp.BackOffice.Filters;
using Erp.Domain.Entities;
using Erp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Erp.Utilities;
using WebMatrix.WebData;
using Erp.BackOffice.Helpers;

namespace Erp.BackOffice.Administration.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class ModuleRelationshipController : Controller
    {
        private readonly IModuleRelationshipRepository ModuleRelationshipRepository;
        private readonly IUserRepository userRepository;

        public ModuleRelationshipController(
            IModuleRelationshipRepository _ModuleRelationship
            , IUserRepository _user
            )
        {
            ModuleRelationshipRepository = _ModuleRelationship;
            userRepository = _user;
        }

        #region Index

        public ViewResult Index(string ModuleName)
        {
            IQueryable<ModuleRelationshipViewModel> q = ModuleRelationshipRepository.GetAllModuleRelationship()
                .Where(item => item.First_ModuleName == ModuleName)
                .Select(item => new ModuleRelationshipViewModel
                {
                    Id = item.Id,
                    First_ModuleName = item.First_ModuleName,
                    First_MetadataFieldName = item.First_MetadataFieldName,
                    Second_ModuleName = item.Second_ModuleName,
                    Second_MetadataFieldName = item.Second_MetadataFieldName,
                    Second_ModuleName_Alias = item.Second_ModuleName_Alias,
                    ModifiedDate = item.ModifiedDate
                }).OrderByDescending(m => m.ModifiedDate);

            ViewBag.First_ModuleName = ModuleName;
            return View(q);
        }
        #endregion

        #region Create
        public ViewResult Create(string First_ModuleName)
        {
            var model = new ModuleRelationshipViewModel();
            model.First_ModuleName = First_ModuleName;
            model.First_MetadataFieldName = "Id";
            model.SelectListModule = Helpers.SelectListHelper.GetSelectList_Module(null);
            model.SelectListMetadataFields = Helpers.SelectListHelper.GetSelectList_MetadataFields(First_ModuleName, null);

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ModuleRelationshipViewModel model)
        {
            if (ModelState.IsValid)
            {
                var ModuleRelationship = new ModuleRelationship();
                AutoMapper.Mapper.Map(model, ModuleRelationship);
                ModuleRelationship.IsDeleted = false;
                ModuleRelationship.CreatedUserId = WebSecurity.CurrentUserId;
                ModuleRelationship.ModifiedUserId = WebSecurity.CurrentUserId;
                ModuleRelationship.CreatedDate = DateTime.Now;
                ModuleRelationship.ModifiedDate = DateTime.Now;
                ModuleRelationshipRepository.InsertModuleRelationship(ModuleRelationship);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var ModuleRelationship = ModuleRelationshipRepository.GetModuleRelationshipById(Id.Value);
            if (ModuleRelationship != null && ModuleRelationship.IsDeleted != true)
            {
                var model = new ModuleRelationshipViewModel();
                AutoMapper.Mapper.Map(ModuleRelationship, model);
                
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
        public ActionResult Edit(ModuleRelationshipViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var ModuleRelationship = ModuleRelationshipRepository.GetModuleRelationshipById(model.Id);
                    AutoMapper.Mapper.Map(model, ModuleRelationship);
                    ModuleRelationship.ModifiedUserId = WebSecurity.CurrentUserId;
                    ModuleRelationship.ModifiedDate = DateTime.Now;
                    ModuleRelationshipRepository.UpdateModuleRelationship(ModuleRelationship);

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
            var ModuleRelationship = ModuleRelationshipRepository.GetModuleRelationshipById(Id.Value);
            if (ModuleRelationship != null && ModuleRelationship.IsDeleted != true)
            {
                var model = new ModuleRelationshipViewModel();
                AutoMapper.Mapper.Map(ModuleRelationship, model);
                
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
                    var item = ModuleRelationshipRepository.GetModuleRelationshipById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if(item != null)
                    {
                        if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                        {
                            TempData["FailedMessage"] = "NotOwner";
                            return RedirectToAction("Index");
                        }

                        item.IsDeleted = true;
                        ModuleRelationshipRepository.UpdateModuleRelationship(item);
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
