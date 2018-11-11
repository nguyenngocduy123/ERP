using Erp.BackOffice.Filters;
using Erp.BackOffice.Staff.Models;
using Erp.Domain.Interfaces;
using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using Erp.Utilities;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Erp.BackOffice.Staff.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class TechniqueController : Controller
    {
        private readonly ITechniqueRepository TechniqueRepository;
        private readonly IUserRepository userRepository;

        public TechniqueController(
            ITechniqueRepository _Technique
            , IUserRepository _user
            )
        {
            TechniqueRepository = _Technique;
            userRepository = _user;
        }

        #region Index

        public ViewResult Index(int? StaffId)
        {
            ViewBag.StaffId = StaffId;
            IQueryable<TechniqueViewModel> q = TechniqueRepository.GetAllTechnique().Where(x=>x.StaffId==StaffId)
                .Select(item => new TechniqueViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    IdCardDate=item.IdCardDate,
                    IdCardIssued=item.IdCardIssued,
                    Rank=item.Rank,
                    StaffId=item.StaffId
                }).OrderByDescending(m => m.ModifiedDate);

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            ViewBag.AccessRightCreate = SecurityFilter.AccessRight("Create", "Technique", "Staff");
            //ViewBag.AccessRightEdit = SecurityFilter.AccessRight("Edit", "Technique", "Staff");
            //ViewBag.AccessRightDelete = SecurityFilter.AccessRight("Delete", "Technique", "Staff");
            return View(q);
        }
        #endregion

        #region Create
        public ViewResult Create(int? StaffId)
        {
            var model = new TechniqueViewModel();
            model.StaffId = StaffId;
            //model.RankList = Helpers.SelectListHelper.GetSelectList_Category("rank", null, "Name", App_GlobalResources.Wording.Empty);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(TechniqueViewModel model)
        {
            if (ModelState.IsValid)
            {
                var Technique = new Domain.Staff.Entities.Technique();
                AutoMapper.Mapper.Map(model, Technique);
                Technique.IsDeleted = false;
                Technique.CreatedUserId = WebSecurity.CurrentUserId;
                Technique.ModifiedUserId = WebSecurity.CurrentUserId;
                Technique.CreatedDate = DateTime.Now;
                Technique.ModifiedDate = DateTime.Now;
                TechniqueRepository.InsertTechnique(Technique);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                {
                    ViewBag.closePopup = "true";
                    model.Id = Technique.Id;
                    //model.RankList = Helpers.SelectListHelper.GetSelectList_Category("rank", null, "Name", App_GlobalResources.Wording.Empty);
                    return View(model);
                }
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var Technique = TechniqueRepository.GetTechniqueById(Id.Value);
            if (Technique != null && Technique.IsDeleted != true)
            {
                var model = new TechniqueViewModel();
                AutoMapper.Mapper.Map(Technique, model);
                //model.RankList = Helpers.SelectListHelper.GetSelectList_Category("rank", model.Rank, "Name", App_GlobalResources.Wording.Empty);
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
        public ActionResult Edit(TechniqueViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var Technique = TechniqueRepository.GetTechniqueById(model.Id);
                    AutoMapper.Mapper.Map(model, Technique);
                    Technique.ModifiedUserId = WebSecurity.CurrentUserId;
                    Technique.ModifiedDate = DateTime.Now;
                    TechniqueRepository.UpdateTechnique(Technique);

                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                    if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                    {
                        ViewBag.closePopup = "true";
                      //  model.Id = Technique.Id;
                        //model.RankList = Helpers.SelectListHelper.GetSelectList_Category("rank", model.Rank, "Name", App_GlobalResources.Wording.Empty);
                        return View(model);
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


        #region Delete
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            try
            {

                var item = TechniqueRepository.GetTechniqueById(id.Value);
                if (item != null)
                {
                    //if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                    //{
                    //    TempData["FailedMessage"] = "NotOwner";
                    //    return RedirectToAction("Index");
                    //}
                    //item.IsDeleted = true;
                    //TechniqueRepository.UpdateTechnique(item);
                    TechniqueRepository.DeleteTechnique(id.Value);
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
