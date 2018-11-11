using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Erp.Domain;
using Erp.Domain.Entities;
using Erp.BackOffice.Filters;
using Erp.Domain.Interfaces;
using Erp.BackOffice.Administration.Models;
using Erp.BackOffice.Areas.Administration.Models;
using Erp.Domain.Repositories;
using WebMatrix.WebData;
using System.Web.Routing;
using Erp.BackOffice.Helpers;

namespace Erp.BackOffice.Filters
{
    //[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class SecurityFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.IsChildAction)
            {
                Erp.BackOffice.Helpers.Common.TrackRequest();
            }

            //base.OnAuthorization(filterContext); //returns to login url

            bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true) || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true);

            if (skipAuthorization)
            {
                return;
            }

            base.OnActionExecuting(filterContext);

            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("ErpDbContext", "System_User", "Id", "UserName", autoCreateTables: true);
            }

            // initialize navigation  by user
            string sControlerName = filterContext.RouteData.Values["Controller"] != null ? filterContext.RouteData.Values["Controller"].ToString().ToLower() : "";
            string sActionName = filterContext.RouteData.Values["Action"] != null ? filterContext.RouteData.Values["Action"].ToString().ToLower() : "";
            string sAreaName = filterContext.RouteData.DataTokens["area"] != null ? filterContext.RouteData.DataTokens["area"].ToString().ToLower() : "";

            //Ngoài những cái login/logoff ... Thì mới init menu
            if (sActionName != "login".ToLower() && sActionName != "logoff" && sActionName != "forgetpassword")
            {
                //Coi thử đăng nhập chưa
                if (WebSecurity.IsAuthenticated)
                {
                    //Kiểm tra truy cập
                    if (AccessRight(sActionName, sControlerName, sAreaName))
                    {
                        if (!filterContext.IsChildAction && !filterContext.HttpContext.Request.IsAjaxRequest())
                        {
                            Dictionary<string, object> userLoggedInfo = new Dictionary<string, object> { { "FullName", Helpers.Common.CurrentUser.FullName }, { "UserName", Helpers.Common.CurrentUser.UserName }, { "UserTypeName", Helpers.Common.CurrentUser.UserTypeName } };
                            filterContext.Controller.ViewData["UserLogged"] = userLoggedInfo;

                            //Kiểm tra truy cập ok thì init menu
                            List<vwPageMenu> pageMenu = CacheHelper.PagesMenu;
                            if (pageMenu != null)
                            {
                                //var listVisible = pageMenu;
                                IEnumerable<PageMenuViewModel> pageMenuViewModel = null;
                                pageMenuViewModel = AutoMapper.Mapper.Map(pageMenu, pageMenuViewModel);
                                //ham dem so luong notifications
                                //  Erp.BackOffice.Helpers.Common.CountNotificatons(pageMenuViewModel);
                                filterContext.Controller.ViewData["DataMenuItem"] = pageMenuViewModel;

                                //Kiểm tra và bật active cho menu cả con & cha
                                var homeMenu = pageMenu.Where(x => x.PageUrl == "/Home/Index").FirstOrDefault();
                                var currentUrl = "/" + sControlerName + "/" + sActionName;
                                if (filterContext.HttpContext.Request.RawUrl.Contains("/Home/Dashboard/"))
                                    currentUrl = filterContext.HttpContext.Request.RawUrl.ToLower();
                                if (pageMenu.Count() > 0)
                                {
                                    var pageMenuItem = pageMenu.Where(x => (homeMenu == null || x.ParentId != homeMenu.Id) &&
                                        ((x.Url != null && x.Url.ToLower() == currentUrl) || (x.PageUrl != null && x.PageUrl.ToLower() == currentUrl))).FirstOrDefault();

                                    if (pageMenuItem == null)
                                    {
                                        currentUrl = "/" + sControlerName + "/index";

                                        pageMenuItem = pageMenu.Where(x => (homeMenu == null || x.ParentId != homeMenu.Id) && x.PageUrl != null && x.PageUrl.ToLower() == currentUrl).FirstOrDefault();
                                    }

                                    if (pageMenuItem != null)
                                    {
                                        //Xem thử submenu của nó có cùng url thì chọn submenu
                                        var subMenu = pageMenu.Where(x => x.ParentId == pageMenuItem.Id &&
                                            ((x.Url != null && x.Url.ToLower() == currentUrl) || (x.PageUrl != null && x.PageUrl.ToLower() == currentUrl))).FirstOrDefault();
                                        if (subMenu != null)
                                            filterContext.Controller.ViewBag.IdMenuItem = subMenu.Id;
                                        else
                                            filterContext.Controller.ViewBag.IdMenuItem = pageMenuItem.Id;
                                    }
                                }
                            }
                        }
                        else //nếu request là ajax thì chuyền viewdata page menu rỗng
                        {
                            filterContext.Controller.ViewData["DataMenuItem"] = new List<PageMenuViewModel>();
                        }

                    }
                    else
                    {
                        if (filterContext.IsChildAction || filterContext.HttpContext.Request.IsAjaxRequest())
                            filterContext.Result = new ContentResult { Content = "" };
                        else
                            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "ErrorPage" }, { "action", "Index" }, { "area", "" } });
                    }
                }
                else
                {
                    if (filterContext.IsChildAction || filterContext.HttpContext.Request.IsAjaxRequest())
                        filterContext.Result = new ContentResult { Content = "" };
                    else
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "User" }, { "action", "Login" } });
                }
            }
        }

        public static bool AccessRight(string ActionName, string ControlerName, string AreaName)
        {
            string sControlerName = ControlerName != null ? ControlerName.ToLower() : "";
            string sActionName = ActionName != null ? ActionName.ToLower() : "";
            string sAreaName = string.IsNullOrEmpty(AreaName) == false ? AreaName.ToLower() : "home";

            if (sControlerName == "" || sActionName == "")
                return false;

            //Coi thử đăng nhập chưa
            if (WebSecurity.IsAuthenticated)
            {
                //Kiểm tra truy cập được khai báo trong file ==> App_Data/ActionExcepted.xml
                ExceptionActionsModel exceptionActionsModel = ExceptionActionHelper.ReadExceptionActions();
                foreach (ExceptionActionModel item in exceptionActionsModel.ExceptionActions)
                {
                    if (item.ActionName.ToLower() == sActionName && (string.IsNullOrEmpty(item.ControllerName) || item.ControllerName.ToLower() == sControlerName)
                        && (string.IsNullOrEmpty(item.AreaName) || item.AreaName.ToLower() == sAreaName))
                    {
                        return true;
                    }
                }

                //Tiếp tục kiểm tra trong bảng phân quyền của hệ thống
                List<vwPage> pagesAccessRight = CacheHelper.PagesAccessRight;
                var ItemPagesAccessRight = pagesAccessRight.Where(x => x.ActionName.ToLowerOrEmpty() == sActionName.ToLower() && x.ControllerName.ToLowerOrEmpty() == sControlerName.ToLower() && (x.AreaName != null ? x.AreaName.ToLowerOrEmpty() == sAreaName.ToLower() : true)).FirstOrDefault();
                if (ItemPagesAccessRight == null)
                {
                    //Không có quyền
                    return false;
                }

                return true;
            }
            else
                return false;
        }

        public static bool IsAdmin()
        {
            if(Helpers.Common.CurrentUser.UserTypeId == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}