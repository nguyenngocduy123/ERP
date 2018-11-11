using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Erp.Domain.Entities;
using Erp.BackOffice.Filters;
using Erp.Domain.Interfaces;
using Erp.BackOffice.Areas.Administration.Models;
using System;
using WebMatrix.WebData;
using Erp.Utilities;

namespace Erp.BackOffice.Administration.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class PageMenuController : Controller
    {
        private readonly IPageRepository _pageRepository;
        private readonly IPageMenuRepository _pageMenuRepository;
        private readonly ISettingLanguageRepository _languageRepository;
        private readonly IQueryHelper QueryHelper;

        public PageMenuController(IPageRepository page, IPageMenuRepository pageMenu, ISettingLanguageRepository language
            , IQueryHelper _QueryHelper)
        {
            _pageRepository = page;
            _pageMenuRepository = pageMenu;
            _languageRepository = language;
            QueryHelper = _QueryHelper;
        }

        public ActionResult Index()
        {
            string DefaultLanguage = _languageRepository.GetDefaultLanguage();
            var q = _pageMenuRepository.GetPageMenus(DefaultLanguage)
                .OrderBy(item => item.OrderNo)
                .ToList();

            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }

        #region Create
        public ViewResult Create()
        {
            var model = new PageMenuViewModel();
            string DefaultLanguage = _languageRepository.GetDefaultLanguage();
            model.LanguageId = DefaultLanguage;
            model.CssClassIcon = "menu-icon fa fa-folder";
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(PageMenuViewModel model, bool IsPopup)
        {
            if (ModelState.IsValid)
            {
                var PageMenu = new PageMenu();
                AutoMapper.Mapper.Map(model, PageMenu);                

                _pageMenuRepository.InsertPageMenu(PageMenu);

                if (model.IsDashboard.HasValue && model.IsDashboard.Value)
                    PageMenu.Url = "/Home/Dashboard/" + PageMenu.Id;

                _pageMenuRepository.UpdatePageMenu(PageMenu);

                //Reset cache
                Erp.BackOffice.Helpers.CacheHelper.PagesMenu = null;

                if (IsPopup)
                {
                    return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
                }
                else
                {
                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
        #endregion     

        public ActionResult Edit(int id)
        {
            string DefaultLanguage = _languageRepository.GetDefaultLanguage();
            var pageMenu = _pageMenuRepository.GetPageMenus(DefaultLanguage)
                .Where(item => item.Id == id).FirstOrDefault();
            if (pageMenu != null)
            {
                var model = new PageMenuViewModel();
                AutoMapper.Mapper.Map(pageMenu, model);

                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(PageMenuViewModel model, bool IsPopup)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var PageMenu = _pageMenuRepository.GetPageMenuById(model.Id);
                    AutoMapper.Mapper.Map(model, PageMenu);

                    if (model.IsDashboard.HasValue && model.IsDashboard.Value)
                        PageMenu.Url = "/Home/Dashboard/" + PageMenu.Id;

                    _pageMenuRepository.UpdatePageMenu(PageMenu);

                    //Reset cache
                    Erp.BackOffice.Helpers.CacheHelper.PagesMenu = null;

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
        }

        [HttpPost]
        public JsonResult EditInline(int Id, string OrderNo, string Name, int? PageId, string CssClassIcon)
        {
            PageMenu pageMenu = _pageMenuRepository.GetPageMenuById(Id);
            pageMenu.OrderNo = Convert.ToInt32(OrderNo);
            pageMenu.Name = Name;
            pageMenu.PageId = PageId;
            pageMenu.CssClassIcon = CssClassIcon;
            _pageRepository.UpdatePageMenu(pageMenu);

            //Reset cache
            Erp.BackOffice.Helpers.CacheHelper.PagesMenu = null;

            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete()
        {
            int id = int.Parse(Request["Id"], CultureInfo.InvariantCulture);
            _pageMenuRepository.DeletePageMenu(id, "vi-VN");

            var listChilds = _pageMenuRepository.GetPageMenus("vi-VN").Where(item => item.ParentId == id).Select(item => item.Id).ToList();

            foreach(var item in listChilds)
            {
                _pageMenuRepository.DeletePageMenu(item, "vi-VN");
            }

            //Reset cache
            Erp.BackOffice.Helpers.CacheHelper.PagesMenu = null;

            TempData["AlertMessage"] = App_GlobalResources.Wording.DeleteSuccess;
            return RedirectToAction("Index");
        }
    }
}