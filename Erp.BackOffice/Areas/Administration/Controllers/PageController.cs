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
using System.Reflection;
using Erp.BackOffice.Administration.Models;

namespace Erp.BackOffice.Administration.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class PageController : Controller
    {
        private readonly IPageRepository _pageRepository;
        private readonly ISettingLanguageRepository _languageRepository;
        private readonly IQueryHelper QueryHelper;

        public PageController(IPageRepository page, IQueryHelper _QueryHelper)
        {
            _pageRepository = page;
            QueryHelper = _QueryHelper;
        }

        //
        // GET: /Administration/Page/

        public ActionResult Index(string AreaName)
        {
            if (!string.IsNullOrEmpty(AreaName))
            {
                var q = _pageRepository.GetPages()
                    .Where(item => item.AreaName == AreaName)
                    .OrderBy(item=>item.ControllerName)
                    .ToList();

                var model = new List<ListPagesModel>();
                AutoMapper.Mapper.Map(q, model);

                ViewBag.AlertMessage = TempData["AlertMessage"];
                return View(model);
            }

            return View();
        }

        public ActionResult JsonList(string AreaName)
        {
            var q = Helpers.SelectListHelper.GetSelectList_Page(AreaName);

            return Json(q, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create(string AreaName)
        {
            var model = new CreatePageModel
            {
                Page = new PageModel
                {
                    AreaName = AreaName
                },
                IsTranslate = false
            };

            return View(model);
        }

        public ActionResult CreatePageMenuForLanguage(int id, string languageId)
        {
            var creteModel = new CreatePageModel
            {
                Page = new PageModel(),
                IsTranslate = true
            };
            creteModel.Page.Id = id;
            return View("Create", creteModel);
        }

        //
        // POST: /Administration/Page/Create

        [HttpPost]
        public ActionResult Create(CreatePageModel model, bool? IsPopup)
        {
            if (ModelState.IsValid)
            {
                if (model.Page.Id == 0)
                {
                    model.Page.IsView = HttpContext.Request["IsView"] != null ? true : false;
                    model.Page.IsAdd = HttpContext.Request["IsAdd"] != null ? true : false;
                    model.Page.IsEdit = HttpContext.Request["IsEdit"] != null ? true : false;
                    model.Page.IsDelete = HttpContext.Request["IsDelete"] != null ? true : false;
                    model.Page.IsImport = HttpContext.Request["IsImport"] != null ? true : false;
                    model.Page.IsExport = HttpContext.Request["IsExport"] != null ? true : false;
                    model.Page.IsPrint = HttpContext.Request["IsPrint"] != null ? true : false;

                    var page = new Page();
                    AutoMapper.Mapper.Map(model.Page, page);
                    page.Name = page.AreaName + "_" + page.ControllerName + "_" + page.ActionName;
                    int pageId = _pageRepository.InsertAndScopIdPage(page);
                }

                //Reset cache for pagerightaccess & pagemenu
                Erp.BackOffice.Helpers.CacheHelper.PagesAccessRight = null;
                Erp.BackOffice.Helpers.CacheHelper.PagesMenu = null;

                if (Request["isAjax"] != null)
                    return Content("success");

                if (IsPopup == true)
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

        //
        // GET: /Administration/Page/Edit/5

        public ActionResult Edit(int id, string languageId)
        {
            var editPage = new EditPageModel();
            Page page = _pageRepository.GetPageById(id);
            if (page != null)
            {
                AutoMapper.Mapper.Map(page, editPage);
            }

            return View("Edit", editPage);
        }

        //
        // POST: /Administration/Page/Edit/5

        [HttpPost]
        public ActionResult Edit(EditPageModel model, bool IsPopup)
        {
            if (ModelState.IsValidField("MenuItemName"))
            {
                if (Request["Submit"] == "Save")
                {
                    Page page = _pageRepository.GetPageById(model.Id);
                    model.IsView = HttpContext.Request["IsView"] != null ? true : false;
                    model.IsAdd = HttpContext.Request["IsAdd"] != null ? true : false;
                    model.IsEdit = HttpContext.Request["IsEdit"] != null ? true : false;
                    model.IsDelete = HttpContext.Request["IsDelete"] != null ? true : false;
                    model.IsImport = HttpContext.Request["IsImport"] != null ? true : false;
                    model.IsExport = HttpContext.Request["IsExport"] != null ? true : false;
                    model.IsPrint = HttpContext.Request["IsPrint"] != null ? true : false;

                    // always update the parent bevore children
                    AutoMapper.Mapper.Map(model, page);
                    _pageRepository.UpdatePage(page);
                }
                else if (Request["Submit"] == "Delete")
                {
                    Page page = _pageRepository.GetPageById(model.Id);

                    TempData["AlertMessage"] = "MenuPage was delete successfully";
                }

                //Reset cache for pagerightaccess & pagemenu
                Erp.BackOffice.Helpers.CacheHelper.PagesAccessRight = null;
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
            string errors = string.Empty;
            foreach (var modalState in ModelState.Values)
            {
                errors += modalState.Value + ": ";
                foreach (var error in modalState.Errors)
                {
                    errors += error.ErrorMessage;
                }
            }

            ViewBag.errors = errors;

            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult EditInline(int Id, string fieldName, string value)
        {
            Dictionary<string, object> field_value = new Dictionary<string, object>();
            field_value.Add(fieldName, value);

            var flag = QueryHelper.UpdateFields("Page", field_value, Id);

            if (fieldName == "Name")
            {
                
            }

            if (flag == true)
                return Json(new { status = "success", id = Id, fieldName = fieldName, value = value }, JsonRequestBehavior.AllowGet);

            return Json(new { status = "error", id = Id, fieldName = fieldName, value = value }, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Administration/Page/Delete/5
        [HttpPost]
        public ActionResult Delete(string AreaName)
        {
            int pageId = int.Parse(Request["pageId"], CultureInfo.InvariantCulture);
            _pageRepository.DeletePage(pageId);

            //Reset cache for pagerightaccess & pagemenu
            Erp.BackOffice.Helpers.CacheHelper.PagesAccessRight = null;
            Erp.BackOffice.Helpers.CacheHelper.PagesMenu = null;

            if(Request["isAjax"] != null)
                return Content("success");

            TempData["AlertMessage"] = App_GlobalResources.Wording.DeleteSuccess;
            return RedirectToAction("Index", new { AreaName = AreaName });
        }

        public ActionResult Synch() 
        {
            Assembly asm = Assembly.GetExecutingAssembly(); //Assembly.GetAssembly(typeof(MyWebDll.MvcApplication));

            var controllerActionlist = asm.GetTypes()
                    .Where(type => typeof(System.Web.Mvc.Controller).IsAssignableFrom(type))
                    .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                    .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                    .Select(x => new ActionControllerInfoModel {
                        Controller = x.DeclaringType.Name.Replace("Controller", "")
                        , Action = x.Name
                        , ReturnType = x.ReturnType.Name
                        , Attributes = String.Join(", ", x.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", "")))
                        , ControllerAction = x.DeclaringType.Name.Replace("Controller", "").ToLower() + "_" + x.Name.ToLower()
                        , Area = findAreaForControllerType(x.DeclaringType)

                    }).OrderBy(x => x.Controller).ThenBy(x => x.Action).ToList();

            var controllerActionlistNoneSameAction = controllerActionlist.GroupBy(x => new { x.Action, x.Controller }, (key, group) => group.OrderBy(x => x.Attributes).FirstOrDefault()).Where(x => x.Attributes.Contains("AllowAnonymous") == false && x.Attributes.Contains("ChildActionOnly") == false).ToList();


            var listInDatabase = _pageRepository.GetAllPage().Select(x => new ActionControllerInfoModel
            {
                Area = x.AreaName,
                Controller = x.ControllerName,
                Action = x.ActionName,
                ReturnType = x.Id + "",
                ControllerAction = x.ControllerName.ToLower() + "_" + x.ActionName.ToLower()
            }).OrderBy(x => x.Controller).ThenBy(x => x.Action).ToList();


            var listToDelete = listInDatabase.Where(x => controllerActionlistNoneSameAction.Any(y => y.ControllerAction == x.ControllerAction) == false).ToList();

            var listToAdd = controllerActionlistNoneSameAction.Where(x => listInDatabase.Any(y => y.ControllerAction == x.ControllerAction) == false).ToList();

            ViewBag.controllerActionScan = controllerActionlistNoneSameAction;
            ViewBag.listInDatabase = listInDatabase;
            ViewBag.listToDelete = listToDelete;
            ViewBag.listToAdd = listToAdd;

            return View();
        
        }

        [HttpPost]
        public ActionResult AddPageAjax(string AreaName, string ControllerName, string ActionName)
        {
            try
            {
                Page page = new Page { AreaName = AreaName, ControllerName = ControllerName, ActionName = ActionName, IsDelete = false, IsDeleted = false, Status = true, };

                page.Name = page.AreaName + "_" + page.ControllerName + "_" + page.ActionName;
                int pageId = _pageRepository.InsertAndScopIdPage(page);

                //Reset cache for pagerightaccess & pagemenu
                Erp.BackOffice.Helpers.CacheHelper.PagesAccessRight = null;
                Erp.BackOffice.Helpers.CacheHelper.PagesMenu = null;

                return Content("success");
            }
            catch
            {
                return Content("failed");
            }
        }

        private string findAreaForControllerType(Type controllerType)
        {
            var areaTypes = getAllAreasRegistered();

            foreach (var areaType in areaTypes)
            {
                if (controllerType.Namespace.StartsWith(areaType.Namespace))
                {
                    var area = (AreaRegistration)Activator.CreateInstance(areaType);
                    return area.AreaName;
                }
            }
            return string.Empty;
        }

        private IEnumerable<Type> getAllAreasRegistered()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var areaTypes = assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(AreaRegistration)));

            return areaTypes;
        }


    }
}