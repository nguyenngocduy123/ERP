using System.Data.Entity.Infrastructure;
using System.Globalization;
using Erp.BackOffice.Areas.Cms.Models;
using Erp.BackOffice.Filters;
using Erp.Domain.Interfaces;
using Erp.Utilities;
using Erp.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Erp.BackOffice.Cms.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class NewsController : Controller
    {
        private static INewsRepository newsRepository;
        private static IUserRepository userRepository;
        private static ICategoryRepository categoryRepository;

        public NewsController(INewsRepository news, IUserRepository user, ICategoryRepository category)
        {
            newsRepository = news;
            userRepository = user;
            categoryRepository = category;
        }

        #region List News
        public ViewResult ListNews()
        {
            var newsList = newsRepository.GetAllNews().OrderByDescending(m => m.ModifiedDate);
            IEnumerable<NewsViewModel> newsViewModelList = null;
            newsViewModelList = AutoMapper.Mapper.Map(newsList, newsViewModelList);

            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(newsViewModelList);
        }
        #endregion

        #region Search News
        public ActionResult Search(int? categoryId, string txtSearch)
        {
            categoryId = categoryId ?? 0;
            txtSearch = !string.IsNullOrEmpty(txtSearch) ? txtSearch : "";
            var newsList = newsRepository.GetAllNews();

            if (categoryId != 0)
            {
                newsList = newsList.Where(u => u.CategoryId == categoryId);
            }
            if (!string.IsNullOrEmpty(txtSearch))
            {
                newsList = newsList.Where(u => u.Title.ToLower().Contains(txtSearch.ToLower()));
            }

            IEnumerable<NewsViewModel> newsViewModelList = null;
            newsViewModelList = AutoMapper.Mapper.Map(newsList, newsViewModelList);

            foreach (var item in newsViewModelList)
            {
                item.CreateUserName = item.CreatedUser != null ? userRepository.GetUserById(item.CreatedUser.Value).UserName : string.Empty;
                item.UpdateUserName = item.UpdateUser != null ? userRepository.GetUserById(item.UpdateUser.Value).UserName : string.Empty;
            }

            return View("ListNews", newsViewModelList);
        }
        #endregion

        #region Edit News
        public ActionResult EditNews(int newsId)
        {
            var newss = newsRepository.GetNewsById(newsId);
            var model = new NewsViewModel();
            AutoMapper.Mapper.Map(newss, model);
            //model.CategoryList = Erp.BackOffice.Helpers.Common.GetSelectList_Category(categoryRepository, "articles", null, "Id", false);
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditNews(NewsViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var browserName = Request["browserName"];
                    if (browserName == "IE" || browserName == "Safari")
                    {
                        // For normal Upload
                        // Save normal upload for Image
                        model.ImagePath = model.SourceFileImage != null ? FileHelper.SaveFile(model.SourceFileImage, Globals.UploadedFilePath) : newsRepository.GetImgPath(model.Id);

                        // Save normal upload for Thumbnail Image
                        model.ThumbnailPath = model.SourceFileThumbnailImage != null ? FileHelper.SaveFile(model.SourceFileThumbnailImage, Globals.UploadedFilePath) : newsRepository.GetThumbnailImgPath(model.Id);
                    }
                    else
                    {
                        // For Image64String
                        // Save NormalImage
                        if (!string.IsNullOrEmpty(model.ImagePath64String))
                        {
                            model.ImagePath = FileHelper.WriteFileFromBase64String(model.ImagePath64String, Globals.UploadedFilePath);
                        }
                        // Save ThumbnailImage
                        if (!string.IsNullOrEmpty(model.ThumbnailPath64String))
                        {
                            model.ThumbnailPath = FileHelper.WriteFileFromBase64String(model.ThumbnailPath64String, Globals.UploadedFilePath);
                        }
                    }
                    var newss = newsRepository.GetNewsById(model.Id);
                    AutoMapper.Mapper.Map(model, newss);
                    newss.ModifiedDate = DateTime.Now;
                    newss.UpdateUser = WebSecurity.CurrentUserId;
                    newsRepository.UpdateNews(newss);
                    TempData["AlertMessage"] = App_GlobalResources.Wording.UpdateSuccess;
                    return RedirectToAction("ListNews");
                }

                return RedirectToAction("EditNews", new { newsId = model.Id });
            }
            model.ListCategory = categoryRepository.GetAllCategories();
            return View(model);
        }
        #endregion

        #region Create News
        public ActionResult CreateNews()
        {
            var model = new NewsViewModel();
            //model.CategoryList = Erp.BackOffice.Helpers.Common.GetSelectList_Category(categoryRepository, "articles", null, "Id", false);
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateNews(NewsViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var browserName = Request["browserName"];
                //if (browserName == "IE" || browserName == "Safari")
                //{
                //    // For normal Upload
                //    // Save normal upload for Image
                //    model.ImagePath = model.SourceFileImage != null ? FileHelper.SaveFile(model.SourceFileImage, Globals.UploadedFilePath) : newsRepository.GetImgPath(model.Id);

                //    // Save normal upload for Thumbnail Image
                //    model.ThumbnailPath = model.SourceFileThumbnailImage != null ? FileHelper.SaveFile(model.SourceFileThumbnailImage, Globals.UploadedFilePath) : newsRepository.GetThumbnailImgPath(model.Id);
                //}
                //else
                //{
                //    // For Image64String
                //    // Save NormalImage
                //    if (!string.IsNullOrEmpty(model.ImagePath64String))
                //    {
                //        model.ImagePath = FileHelper.WriteFileFromBase64String(model.ImagePath64String, Globals.UploadedFilePath);
                //    }

                //    // Save ThumbnailImage
                //    if (!string.IsNullOrEmpty(model.ThumbnailPath64String))
                //    {
                //        model.ThumbnailPath = FileHelper.WriteFileFromBase64String(model.ThumbnailPath64String, Globals.UploadedFilePath);
                //    }
                //}
                var newss = new Domain.Entities.News();
                AutoMapper.Mapper.Map(model, newss);
                newss.CreatedUser = WebSecurity.CurrentUserId;
                newss.UpdateUser = WebSecurity.CurrentUserId;
                newss.CreatedDate = DateTime.Now;
                newss.ModifiedDate = DateTime.Now;
                newsRepository.InsertNews(newss);
                TempData["AlertMessage"] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("ListNews");
            }

            model.ListCategory = categoryRepository.GetAllCategories();
            return View(model);
        }
        #endregion

        #region Delete News
        [HttpPost]
        public ActionResult DeleteNews()
        {
            var deleteNewsId = int.Parse(Request["DeleteNewsId"], CultureInfo.InvariantCulture);
            newsRepository.DeleteNews(deleteNewsId, WebSecurity.CurrentUserId);
            TempData["AlertMessage"] = App_GlobalResources.Wording.DeleteSuccess;
            return RedirectToAction("ListNews");
        }

        [HttpPost]
        public ActionResult DeleteAll()
        {
            try
            {
                string idDeleteAll = Request["DeleteAll-checkbox"];
                string[] arrDeleteId = idDeleteAll.Split(',');
                for (int i = 0; i < arrDeleteId.Count(); i++)
                {
                    newsRepository.DeleteNews(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture), WebSecurity.CurrentUserId);
                }
                TempData["AlertMessage"] = App_GlobalResources.Wording.DeleteSuccess;
                return RedirectToAction("ListNews");
            }
            catch(DbUpdateException)
            {
                TempData["AlertMessage"] = App_GlobalResources.Error.DeletingError;
                return RedirectToAction("ListNews");
            }
        }
        #endregion

    }
}
