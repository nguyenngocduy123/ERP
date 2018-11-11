using System.Globalization;
using Erp.BackOffice.Areas.Cms.Models;
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
using Erp.Domain.Repositories;

namespace Erp.BackOffice.Sale.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class SaleCategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;
        public SaleCategoryController(ICategoryRepository category, IUserRepository user)
        {
            _categoryRepository = category;
            _userRepository = user;
        }

        #region List Category
        IEnumerable<CategoryViewModel> getCategories(string code)
        {
            IEnumerable<CategoryViewModel> listCategory = new List<CategoryViewModel>();
            var model = _categoryRepository.GetAllCategories()
                .Where(item => item.Code == code)
                .OrderBy(m => m.OrderNo).ToList();

            listCategory = AutoMapper.Mapper.Map(model, listCategory);

            return listCategory;
        }

        public ViewResult ProductCategory()
        {
            string code = "product";
            ViewBag.SuccessMessage = TempData[Globals.SuccessMessageKey];
            ViewBag.FailedMessage = TempData[Globals.FailedMessageKey];
            ViewBag.CategoryCode = code;
            ViewBag.ActionName = "ProductCategory";

            return View("Index", getCategories(code));
        }

        public ViewResult ProductUnit()
        {
            string code = "productUnit";
            ViewBag.SuccessMessage = TempData[Globals.SuccessMessageKey];
            ViewBag.FailedMessage = TempData[Globals.FailedMessageKey];
            ViewBag.CategoryCode = code;
            ViewBag.ActionName = "ProductUnit";

            return View("Index", getCategories(code));
        }

        public ViewResult ProductManufacturer()
        {
            string code = "manufacturerList";
            ViewBag.SuccessMessage = TempData[Globals.SuccessMessageKey];
            ViewBag.FailedMessage = TempData[Globals.FailedMessageKey];
            ViewBag.CategoryCode = code;
            ViewBag.ActionName = "ProductManufacturer";

            return View("Index", getCategories(code));
        }

        public ViewResult ProductGroup()
        {
            string code = "ProductGroup";
            ViewBag.SuccessMessage = TempData[Globals.SuccessMessageKey];
            ViewBag.FailedMessage = TempData[Globals.FailedMessageKey];
            ViewBag.CategoryCode = code;
            ViewBag.ActionName = "ProductGroup";

            return View("Index", getCategories(code));
        }

        public ViewResult TitleSalarySetting()
        {
            string code = "TitleSalarySetting";
            ViewBag.SuccessMessage = TempData[Globals.SuccessMessageKey];
            ViewBag.FailedMessage = TempData[Globals.FailedMessageKey];
            ViewBag.CategoryCode = code;
            ViewBag.ActionName = "TitleSalarySetting";

            return View("Index", getCategories(code));
        }
        #endregion

        #region Edit Category
        public ActionResult Edit(int Id, string ActionName)
        {
            var category = _categoryRepository.GetCategoryById(Id);
            var model = new CategoryViewModel();
            if (category != null && category.IsDeleted != true)
            {
                AutoMapper.Mapper.Map(category, model);
                return View(model);
            }

            ViewBag.ActionName = ActionName;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(CategoryViewModel model, string ActionName, bool IsPopup)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var category = _categoryRepository.GetCategoryById(model.Id);
                    AutoMapper.Mapper.Map(model, category);
                    category.ModifiedUserId = WebSecurity.CurrentUserId;
                    category.ModifiedDate = DateTime.Now;
                    _categoryRepository.UpdateCategory(category);

                    if (IsPopup)
                    {
                        return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
                    }
                    else
                    {
                        TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                        return RedirectToAction(ActionName, new { Code = category.Code });
                    }
                }
                return RedirectToAction("Edit", new { categoryId = model.Id });
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        #endregion

        #region Create Category
        public ActionResult Create(string ActionName, string Code)
        {
            var model = new CategoryViewModel { };
            model.Code = Code;
            ViewBag.ActionName = ActionName;

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CategoryViewModel model, string ActionName, bool IsPopup)
        {
            if (ModelState.IsValid)
            {
                var category = new Category();
                AutoMapper.Mapper.Map(model, category);
                category.CreatedUserId = WebSecurity.CurrentUserId;
                category.ModifiedUserId = WebSecurity.CurrentUserId;
                category.CreatedDate = DateTime.Now;
                category.ModifiedDate = DateTime.Now;

                if (string.IsNullOrEmpty(category.Value) == true)
                    category.Value = Helpers.Common.ChuyenThanhKhongDau(category.Name).Replace(" ", "_");

                _categoryRepository.InsertCategory(category);

                if (IsPopup)
                {
                    return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
                }
                else
                {
                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                    return RedirectToAction(ActionName, new { Code = category.Code });
                }
            }
            return RedirectToAction("Create");
        }
        #endregion

        #region Delete Category

           [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                //var deleteCategoryId = int.Parse(Request["DeleteCategoryId"], CultureInfo.InvariantCulture);
                _categoryRepository.DeleteCategory(id);
                //TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.DeleteSuccess;
                return Content("Success");
            }
            catch (DbUpdateException)
            {
                //TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.RelationError;
                return Content("Error");
            }
        }
        #endregion

    }
}
