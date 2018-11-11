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
using Erp.BackOffice.Areas.Cms.Models;

namespace Erp.BackOffice.Staff.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class SymbolTimekeepingController : Controller
    {
        private readonly ISymbolTimekeepingRepository symboltimekeepingRepository;
        private readonly IUserRepository userRepository;
        private readonly ICategoryRepository categoryRepository;
        public SymbolTimekeepingController(
            ISymbolTimekeepingRepository _symboltimekeeping
            , IUserRepository _user
            ,ICategoryRepository category
            )
        {
            symboltimekeepingRepository = _symboltimekeeping;
            userRepository = _user;
            categoryRepository = category;
        }

        #region Index

        public ViewResult Index(string txtSearch)
        {

            IQueryable<SymbolTimekeepingViewModel> q = symboltimekeepingRepository.GetAllSymbolTimekeeping()
                .Select(item => new SymbolTimekeepingViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    Quantity=item.Quantity,
                    Timekeeping = item.Timekeeping,
                    DayOff=item.DayOff,
                    Code=item.Code,
                    Color=item.Color,
                   CodeDefault=item.CodeDefault,
                   Absent=item.Absent
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
            var model = new SymbolTimekeepingViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(SymbolTimekeepingViewModel model)
        {
            if (ModelState.IsValid)
            {
                var SymbolTimekeeping = new Domain.Staff.Entities.SymbolTimekeeping();
                AutoMapper.Mapper.Map(model, SymbolTimekeeping);
                SymbolTimekeeping.IsDeleted = false;
                SymbolTimekeeping.CreatedUserId = WebSecurity.CurrentUserId;
                SymbolTimekeeping.ModifiedUserId = WebSecurity.CurrentUserId;
                SymbolTimekeeping.CreatedDate = DateTime.Now;
                SymbolTimekeeping.ModifiedDate = DateTime.Now;
                symboltimekeepingRepository.InsertSymbolTimekeeping(SymbolTimekeeping);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                {
                    ViewBag.closePopup = "true";
                    model.Id = SymbolTimekeeping.Id;
                    return View(model);
                }
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var SymbolTimekeeping = symboltimekeepingRepository.GetSymbolTimekeepingById(Id.Value);
            if (SymbolTimekeeping != null && SymbolTimekeeping.IsDeleted != true)
            {
                var model = new SymbolTimekeepingViewModel();
                AutoMapper.Mapper.Map(SymbolTimekeeping, model);
                
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
        public ActionResult Edit(SymbolTimekeepingViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var SymbolTimekeeping = symboltimekeepingRepository.GetSymbolTimekeepingById(model.Id);
                    AutoMapper.Mapper.Map(model, SymbolTimekeeping);
                    SymbolTimekeeping.ModifiedUserId = WebSecurity.CurrentUserId;
                    SymbolTimekeeping.ModifiedDate = DateTime.Now;
                    symboltimekeepingRepository.UpdateSymbolTimekeeping(SymbolTimekeeping);

                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                    if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                    {
                        ViewBag.closePopup = "true";
                        model.Id = SymbolTimekeeping.Id;
                        //model.RankList = Helpers.SelectListHelper.GetSelectList_Category("rank", null, "Name", App_GlobalResources.Wording.Empty);
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
        public ActionResult Delete()
        {
            try
            {
                string idDeleteAll = Request["DeleteId-checkbox"];
                string[] arrDeleteId = idDeleteAll.Split(',');
                for (int i = 0; i < arrDeleteId.Count(); i++)
                {
                    var item = symboltimekeepingRepository.GetSymbolTimekeepingById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if(item != null)
                    {
                        //if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                        //{
                        //    TempData["FailedMessage"] = "NotOwner";
                        //    return RedirectToAction("Index");
                        //}

                        item.IsDeleted = true;
                        symboltimekeepingRepository.UpdateSymbolTimekeeping(item);
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


        #region DayOff

        public ViewResult DayOff()
        {
            SymbolTimekeepingViewModel model = new SymbolTimekeepingViewModel();
            model.ListCategory = categoryRepository.GetCategoryByCode("DayOffDefault")
                .Select(item => new CategoryViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    OrderNo=item.OrderNo,
                    Value=item.Value,
                    Code = item.Code
                }).OrderByDescending(m => m.OrderNo).ToList();
            foreach (var item in model.ListCategory)
            {
                item.Check = Convert.ToBoolean(item.Value);
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult DayOff(SymbolTimekeepingViewModel model)
        {
            var urlRefer = Request["UrlReferrer"];
            for (int i = 0; i < model.ListCategory.Count(); i++)
            {
                var list = categoryRepository.GetCategoryById(model.ListCategory[i].Id);
                list.Value = model.ListCategory[i].Check.Value.ToString();
                categoryRepository.UpdateCategory(list);
            }
            return Redirect(urlRefer);
        }
        #endregion
    }
}
