using Erp.Domain;
using Erp.Domain.Entities;
using Erp.Domain.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Caching;
using WebMatrix.WebData;

namespace Erp.BackOffice.Helpers
{
    public class CacheHelper
    {
        public static DateTime DefaultCacheExpiration()
        {
#if DEBUG
            return DateTime.Now.AddSeconds(1);
#else
			return DateTime.Now.AddDays(1);
#endif
        }

        #region PagesAccessRight & PagesMenu
        public static List<vwPage> PagesAccessRight
        {
            get
            {
                //int UserTypeId = 1;
                UserRepository userRepository = new UserRepository(new ErpDbContext());
                int UserTypeId = userRepository.GetUserTypeId(WebSecurity.CurrentUserId);

                System.Web.Caching.Cache Cache = HttpRuntime.Cache;
                string sCacheName = "PagesAccessRight";
                string sObjectName = UserTypeId.ToString();

                //Kiểm tra menu trong cache có chưa? Chưa có thì lấy từ db
                PageRepository pageRepository = new PageRepository(new ErpDbContext());
                List<vwPage> pagesAccessRight = Cache.Get(sCacheName + "." + sObjectName) as List<vwPage>;

                if (pagesAccessRight == null)
                {
                    if (Common.CurrentUser.UserTypeId == 1)
                    {
                        //Load menu for user have user type is admin
                        pagesAccessRight = pageRepository.GetPages().ToList();
                    }
                    else
                    {
                        pagesAccessRight = pageRepository.GetPagesAccessRight(WebSecurity.CurrentUserId, UserTypeId).ToList();
                    }

                    Cache.Insert(sCacheName + "." + sObjectName, pagesAccessRight, null, DefaultCacheExpiration(), Cache.NoSlidingExpiration);
                }

                return pagesAccessRight;
            }

            set
            {
                RemoveCache("PagesAccessRight.");
            }
        }

        public static List<vwPageMenu> PagesMenu
        {
            get
            {
                //int UserTypeId = 1;
                //UserRepository userRepository = new UserRepository(new ErpDbContext());
                //int UserTypeId = userRepository.GetUserTypeId(WebSecurity.CurrentUserId);

                System.Web.Caching.Cache Cache = HttpRuntime.Cache;
                string sCacheName = "PagesMenu";
                string sObjectName = Common.CurrentUser.UserTypeId.ToString();//UserTypeId.ToString();

                //Kiểm tra menu trong cache có chưa? Chưa có thì lấy từ db
                PageMenuRepository pageMenuRepository = new PageMenuRepository(new ErpDbContext());
                List<vwPageMenu> pagesMenu = Cache.Get(sCacheName + "." + sObjectName) as List<vwPageMenu>;

                if (pagesMenu == null)
                {
                    pagesMenu = new List<vwPageMenu>();
                    List<vwPageMenu> listAllPageMenu = pageMenuRepository.GetPageMenus("vi-VN").Where(item => item.IsVisible.HasValue && item.IsVisible.Value == true).ToList();
                    List<vwPage> listPagesAccessRight = PagesAccessRight;

                    var q = listAllPageMenu.Where(item => item.Url != null || (item.PageId != null && listPagesAccessRight.Select(i => i.Id).Contains(item.PageId.Value))).ToList();

                    foreach (var item in q)
                    {
                        if (!pagesMenu.Any(i => i.Id == item.Id))
                            pagesMenu.Add(item);

                        if (item.ParentId != null)
                        {
                            //Thêm parent menu cap tren
                            var parent = listAllPageMenu.Where(i => i.Id == item.ParentId).FirstOrDefault();
                            if (parent != null && !pagesMenu.Any(i => i.Id == parent.Id))
                            {
                                pagesMenu.Add(parent);
                                if (parent.ParentId != null)
                                {
                                    //Thêm parent menu cap tren nua
                                    parent = listAllPageMenu.Where(i => i.Id == parent.ParentId).FirstOrDefault();
                                    if (parent != null && !pagesMenu.Any(i => i.Id == parent.Id))
                                    {
                                        pagesMenu.Add(parent);
                                    }
                                }
                            }
                        }
                    }

                    Cache.Insert(sCacheName + "." + sObjectName, pagesMenu, null, DefaultCacheExpiration(), Cache.NoSlidingExpiration);
                }

                return pagesMenu;
            }
            set
            {
                RemoveCache("PagesMenu.");
            }
        }

        //static int? AddParentMenu(List<vwPageMenu> listAllPageMenu, int? Id)
        //{
        //    if (Id != null && pagesMenu.Where(i => i.Id == Id).Count() == 0)
        //    {
        //        var parentMenuItem = listAllPages.Where(i => i.Id == Id).FirstOrDefault();

        //        try
        //        {
        //            //Nếu menu này không có quyền, thì cho action & controller == ""
        //            if (listPagesAccessRight.Where(i => i.ControllerName == parentMenuItem.ControllerName && i.ActionName == parentMenuItem.ActionName).Count() == 0)
        //            {
        //                parentMenuItem.ControllerName = "";
        //                parentMenuItem.ActionName = "";
        //            }

        //            if ((parentMenuItem.ControllerName != "" && parentMenuItem.ActionName != "") || pagesMenu.Where(i => i.ParentId == parentMenuItem.Id && i.IsVisible.HasValue && i.IsVisible.Value == true).Count() > 0)
        //            {
        //                pagesMenu.Add(parentMenuItem);

        //                return parentMenuItem.ParentId;
        //            }
        //        }
        //        catch (Exception ex) { }
        //    }

        //    return null;
        //}
        #endregion

        public static void RemoveCache(string prefix)
        {
            System.Web.Caching.Cache Cache = HttpRuntime.Cache;
            List<string> itemsToRemove = new List<string>();

            IDictionaryEnumerator enumerator = Cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Key.ToString().StartsWith(prefix))
                {
                    itemsToRemove.Add(enumerator.Key.ToString());
                }
            }

            foreach (string itemToRemove in itemsToRemove)
            {
                Cache.Remove(itemToRemove);
            }
        }
    }
}