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
using Erp.Domain.Entities;
using Erp.BackOffice.Areas.Administration.Models;
using Erp.BackOffice.Helpers;

namespace Erp.BackOffice.CMS.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class ReportController : Controller
    {
        private static IPageRepository pageRepository;
        private static IPageMenuRepository pageMenuRepository;
        private static INewsRepository newsRepository;
        private static IUserRepository userRepository;
        private static ICategoryRepository categoryRepository;

        public ReportController(IPageRepository page, IPageMenuRepository pageMenu, INewsRepository news, IUserRepository user, ICategoryRepository category)
        {
            newsRepository = news;
            userRepository = user;
            categoryRepository = category;
            pageRepository = page;
            pageMenuRepository = pageMenu;
        }

        public ActionResult List(int? id)
        {
            List<vwPage> pageMenu = CacheHelper.PagesAccessRight;

            List<PageMenuViewModel> list = new List<PageMenuViewModel>();
            //if (id != null)
            //{
            //    list = pageRepository.GetPagesByParent(id)
            //        .Select(x => new PageMenuViewModel
            //        {
            //            Id = x.Id,
            //            ActionName = x.ActionName,
            //            ControllerName = x.ControllerName,
            //            AreaName = x.AreaName,
            //            Url = x.Url,
            //            Name = x.Name
            //        }).ToList();

            //    foreach (var item in list)
            //    {
            //        var subList = pageRepository.GetPagesByParent(item.Id)
            //            .Select(x => new PageMenuViewModel
            //            {
            //                Id = x.Id,
            //                ActionName = x.ActionName,
            //                ControllerName = x.ControllerName,
            //                AreaName = x.AreaName,
            //                Url = x.Url
            //            }).ToList();

            //        foreach (var subItem in subList)
            //        {
            //            var subItemAccsessRight = pageMenu.Where(x => x.Id == subItem.Id).FirstOrDefault();
            //            if (subItemAccsessRight != null)
            //            {
            //                subItem.IsDelete = false;
            //                subItem.Name = subItemAccsessRight.MenuItemName;
            //            }
            //            else
            //            {
            //                subItem.IsDelete = true;
            //            }
            //        }

            //        item.SubMenu = subList.Where(x => x.IsDelete == false).ToList();
            //    }

            //    var page = pageMenuRepository.GetPageName((int)id, "vi-VN");
            //    ViewBag.PageName = page;
            //}

            //return View(list.Where(item => item.SubMenu.Count > 0).ToList());

            return View();
        }

    }
}
