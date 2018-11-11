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

namespace Erp.BackOffice.Cms.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;
        public CategoryController(ICategoryRepository category, IUserRepository user)
        {
            _categoryRepository = category;
            _userRepository = user;
        }

        #region MvcHtmlString
        #endregion

        #region List Category
        public ViewResult Index(string txtSearch, string code, int? parentId)
        {
            var selectListItems = new List<SelectListItem>() { 
                new SelectListItem { Text = App_GlobalResources.Wording.Empty, Value = null }
            };

            try
            {
                var q = _categoryRepository.GetAllCategories()
                    .OrderBy(item => item.Code)
                    .Select(item => item.Code)
                    .Distinct().Select(str => new SelectListItem { Text = str, Value = str }).ToList();

                selectListItems.AddRange(q);

                if (parentId != null)
                    selectListItems.Add(new SelectListItem { Text = code, Value = code });
            }
            catch { }

            ViewData["CodeList"] = new SelectList(selectListItems, "Value", "Text", null);

            IEnumerable<CategoryViewModel> listCategory = new List<CategoryViewModel>();
            if (code != null && code != "")
            {
                var model = _categoryRepository.GetCategoryByCode(code).OrderBy(m => m.OrderNo);

                if (!string.IsNullOrEmpty(txtSearch))
                {
                    model = model.Where(m => m.Name.ToLower(CultureInfo.CurrentCulture).Contains(txtSearch.ToLower(CultureInfo.CurrentCulture))).OrderByDescending(m => m.ModifiedDate);
                }

                listCategory = AutoMapper.Mapper.Map(model, listCategory);

                foreach (var item in listCategory)
                {
                    var createUserId = item.CreatedUserId ?? -1;
                    item.NameCreateUser = _userRepository.GetUserById(createUserId) != null ? _userRepository.GetUserById(createUserId).UserName : string.Empty;

                    var modifiedUserId = item.ModifiedUserId ?? -1;
                    item.NameModifiedUser = _userRepository.GetUserById(modifiedUserId) != null ? _userRepository.GetUserById(modifiedUserId).UserName : string.Empty;

                    var cateId = item.ParentId ?? -1;
                    item.NameParent = _categoryRepository.GetCategoryById(cateId) != null ? _categoryRepository.GetCategoryById(cateId).Name : string.Empty;
                }
            }

            ViewBag.SuccessMessage = TempData[Globals.SuccessMessageKey];
            ViewBag.FailedMessage = TempData[Globals.FailedMessageKey];

            return View(listCategory);
        }
        #endregion

        #region Edit Category
        public ActionResult EditCategory(int categoryId, string code)
        {
            if (code != null || code != "")
            {
                var category = _categoryRepository.GetCategoryById(categoryId);
                var model = new CategoryViewModel();
                if (category != null && category.IsDeleted != true)
                {
                    AutoMapper.Mapper.Map(category, model);
                    return View(model);
                }
                return RedirectToAction("ListCategory");
            }
            return RedirectToAction("ListCategory");
        }

        [HttpPost]
        public ActionResult EditCategory(CategoryViewModel model)
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

                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                    return RedirectToAction("Index", new { Code = category.Code });
                }
                return RedirectToAction("EditCategory", new { categoryId = model.Id });
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        #endregion

        #region Create Category
        public ActionResult CreateCategory(string Code, int? parentId)
        {
            if (Code != null || Code != "")
            {
                var model = new CategoryViewModel { ListCategory = _categoryRepository.GetAllCategories() };
                model.Code = Code;
                model.ParentId = parentId;

                if (parentId == null && string.IsNullOrEmpty(Code) == false)
                {
                    var categoryParent = _categoryRepository.GetAllCategories().Where(x => x.Value == Code).FirstOrDefault();
                    if (categoryParent != null)
                        model.ParentId = categoryParent.Id;
                }

                return View(model);
            }

            return RedirectToAction("ListCategory");
        }

        [HttpPost]
        public ActionResult CreateCategory(CategoryViewModel model)
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

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Index", new { Code = category.Code });
            }
            return RedirectToAction("CreateCategory");
        }
        #endregion

        #region Delete Category
        [HttpPost]
        public ActionResult DeleteCategory()
        {
            try
            {
                var deleteCategoryId = int.Parse(Request["DeleteCategoryId"], CultureInfo.InvariantCulture);
                _categoryRepository.DeleteCategory(deleteCategoryId);
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.DeleteSuccess;
                return RedirectToAction("Index");
            }
            catch (DbUpdateException)
            {
                TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.RelationError;
                return RedirectToAction("ListCategory");
            }
        }

        [HttpPost]
        public ActionResult DeleteAll(string Code)
        {
            try
            {
                if (Code != null || Code != "")
                {
                    string idDeleteAll = Request["DeleteAll-checkbox"];
                    string[] arrDeleteId = idDeleteAll.Split(',');
                    for (int i = 0; i < arrDeleteId.Count(); i++)
                    {
                        _categoryRepository.DeleteCategory(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    }
                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.DeleteSuccess;
                    return RedirectToAction("Index", new { Code = Code });
                }
                return RedirectToAction("Index");
            }
            catch (DbUpdateException)
            {
                TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.RelationError;
                return RedirectToAction("Index");
            }
        }
        #endregion

        public JsonResult GetListJsonCategoryByParentId(int? parentId)
        {
            if (parentId == null)
                return Json(new List<int>(), JsonRequestBehavior.AllowGet);

            var list = _categoryRepository.GetCategoryByParentId(parentId.Value);
            return Json(list.Select(x => new { Id = x.Id, Name = x.Name, Code = x.Code, ParentId = x.ParentId, Value = x.Value }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListJsonCategoryByCode(string code)
        {
            if (string.IsNullOrEmpty(code) == true)
                return Json(new List<int>(), JsonRequestBehavior.AllowGet);

            var list = _categoryRepository.GetCategoryByCode(code);
            return Json(list.Select(x => new { Id = x.Id, Name = x.Name, Code = x.Code, ParentId = x.ParentId, Value = x.Value }), JsonRequestBehavior.AllowGet);
        }

        public static void CreateAndEditCategory(string value, string name, string Code, string Description, int? OrderNo)
        {
            Erp.Domain.Repositories.CategoryRepository _categoryRepository = new Erp.Domain.Repositories.CategoryRepository(new Domain.ErpDbContext());
            var ktra = _categoryRepository.GetCategoryByCode(Code).Where(x => x.OrderNo == OrderNo);
            if (ktra.Count() > 0)
            {
                var q = ktra.FirstOrDefault();
                if (q != null)
                {
                    q.ModifiedUserId = WebSecurity.CurrentUserId;
                    q.ModifiedDate = DateTime.Now;
                    q.Value = value;
                    q.Code = Code;
                    q.Name = name;
                    q.Description = Description;
                    _categoryRepository.UpdateCategory(q);
                }
            }
            else
            {
                var category = new Category();
                category.CreatedUserId = WebSecurity.CurrentUserId;
                category.ModifiedUserId = WebSecurity.CurrentUserId;
                category.CreatedDate = DateTime.Now;
                category.ModifiedDate = DateTime.Now;
                category.Value = value;
                category.Code = Code;
                category.Name = name;
                category.Description = Description;
                category.OrderNo = OrderNo;
                _categoryRepository.InsertCategory(category);
            }
        }
        public static void DeleteCategory(string value, string Code)
        {
            Erp.Domain.Repositories.CategoryRepository _categoryRepository = new Erp.Domain.Repositories.CategoryRepository(new Domain.ErpDbContext());
            var ktra = _categoryRepository.GetCategoryByCode(Code).Where(x => x.Value == value);
            if (ktra.Count() > 0)
            {
                var q = ktra.FirstOrDefault();
                if (q != null)
                {
                    _categoryRepository.DeleteCategory(q.Id);
                }
            }
            
        }
    }
}
