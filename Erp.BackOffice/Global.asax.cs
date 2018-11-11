using StackExchange.Profiling;
using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebMatrix.WebData;
using Telerik.Reporting.Services.WebApi;
using System.Diagnostics;
using AutoMapper;
using Erp.Domain.Entities;
using Erp.BackOffice.Models;
using System.Collections.Generic;
using System.Linq;
using Erp.BackOffice.Helpers;
using Erp.BackOffice.App_Start;
using Autofac;
using Erp.Domain.Repositories;
using Erp.Domain.Interfaces;
using Erp.Domain;
using Autofac.Integration.Mvc;
using StackExchange.Profiling.EntityFramework6;

namespace Erp.BackOffice
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {        
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            //for MiniProfiler
            MiniProfilerEF6.Initialize();
            MiniProfiler.Settings.PopupRenderPosition = RenderPosition.BottomRight;
            GlobalFilters.Filters.Add(new StackExchange.Profiling.Mvc.ProfilingActionFilter());

            //init Interface & Repository Domain
            IoCContainer.InitializeContainer();

            //for report telerik
            ReportsControllerConfiguration.RegisterRoutes(GlobalConfiguration.Configuration);

            //Quartz
            ScheduleHelper.init();
        }        

        protected void Application_BeginRequest()
        {
            var ListRequest = Application["ListRequest"] as List<RequestInfo>;
            if (ListRequest != null)
            {
                var ip = Request.UserHostAddress;
                var requestInfo = ListRequest.Where(item => item.IP == ip).FirstOrDefault();

                if (requestInfo != null && requestInfo.UrlList != null && requestInfo.UrlList.Count > 0)
                {
                    //lay 100 rq cuoi
                    requestInfo.UrlList = requestInfo.UrlList.Take(100).ToList();
                    requestInfo.RequestCount = requestInfo.UrlList.Count;
                    var firstTime = requestInfo.UrlList.FirstOrDefault().Date;
                    var lastTime = requestInfo.UrlList.LastOrDefault().Date;

                    //Coi thu trong vong 1 phut, ma rq hon 100 thi lock
                    if (requestInfo.RequestCount > 5000 || (requestInfo.RequestCount > 100 && lastTime.Subtract(firstTime).TotalSeconds <= 60))
                        requestInfo.IsLocked = true;

                    if (requestInfo.IsLocked && !Request.RawUrl.Contains("/ErrorPage"))
                    {
                        Response.Redirect("/ErrorPage");
                    }
                }
            }

            var info = new CultureInfo(Thread.CurrentThread.CurrentCulture.ToString()) { DateTimeFormat = { ShortDatePattern = "dd/MM/yyyy" } };
            Thread.CurrentThread.CurrentCulture = info;
            if (Request.IsLocal)
            {
                //MiniProfiler.Start();
            }
        }

        protected void Application_EndRequest()
        {
            //MiniProfiler.Stop();
        }
        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session != null)
            {
                var currentCulture =
                   (CultureInfo)Session["CurrentLanguage"];
                if (currentCulture == null)
                {
                    currentCulture = new CultureInfo("vi-vn");
                    Session["CurrentLanguage"] = currentCulture;
                }

                Thread.CurrentThread.CurrentUICulture = currentCulture;
                Thread.CurrentThread.CurrentCulture =
                CultureInfo.CreateSpecificCulture(currentCulture.Name);


                // kiem tra userName va sessionID luu vao bien Application[userName] co giong voi sessionID cua user dang request hay khong
                // neu khong giong thi dua qua trang logoff
                // tuc la chi cho nguoi login sau su dung, nguoi login truoc thi bi logoff
                var userName = HttpContext.Current.User.Identity.Name;
                var sessionID = HttpContext.Current.Session.SessionID;
                var url = HttpContext.Current.Request.Url;
                if (userName.Contains("admin") == false && Application[userName] != null && url.ToString().Contains("/User/LogOff") == false)
                {
                    string sessionIdUserLogging = Convert.ToString(Application[userName]);
                    if (sessionIdUserLogging != sessionID)
                    {
                        Response.Redirect("/User/LogOff");
                    }
                }
                // end for check other user login same User

                //////////////////////////////////////////////////////////////////
                //thêm thông tin của người dùng sử dụng để kiểm tra ai đang online
                //List<UserSessionInfo> listUserOnline = new List<UserSessionInfo>();
                //if (Application["listUserOnline"] != null)
                //    listUserOnline = (List<UserSessionInfo>)Application["listUserOnline"];

                //var userSession = listUserOnline.Where(x => x.UserName == userName).FirstOrDefault();
                //if (userSession == null)
                //{
                //    userSession = new UserSessionInfo
                //    {
                //        UserName = userName,
                //        LastActionName = url.PathAndQuery,
                //        StartTime = DateTime.Now,
                //        LastTimeAction = DateTime.Now,
                //        SessionID = sessionID
                //    };

                //    listUserOnline.Add(userSession);
                //}
                //else
                //{
                //    userSession.LastTimeAction = DateTime.Now;
                //    userSession.LastActionName = url.PathAndQuery;
                //    userSession.SessionID = sessionID;
                //    listUserOnline.RemoveAll(x => x.UserName == userName);
                //    listUserOnline.Add(userSession);
                //}

                //Application["listUserOnline"] = listUserOnline;
                //string szCookieHeader = Request.Headers["Cookie"];
                //Session["sessionID"] = sessionID;

            }
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            return;
            Exception exception = Server.GetLastError();
            var httpException = exception as HttpException;
            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", "ErrorPage");
            routeData.Values.Add("action", "Index");
            routeData.Values.Add("error", exception);

            if (httpException != null)
            {
                Response.StatusCode = httpException.GetHttpCode();
                switch (Response.StatusCode)
                {
                    case 403:
                        Response.Redirect("/ErrorPage");
                        break;
                    case 404:
                        Response.Redirect("/ErrorPage/Page404");
                        break;
                    case 500:
                        Response.Redirect("/ErrorPage/Page500");
                        break;
                }

            }

            //Response.Clear();
            //Server.ClearError();
            return;
        }

        protected void Application_End()
        {
            // and last shut down the scheduler when you are ready to close your program
            ScheduleHelper.Scheduler.Shutdown();
        }
        protected void Session_Start()
        {
            // when user begin session, check global variable Application[userName] => sample Application["admin"]
            // if global variable Application[userName] not exits, init global variable Application[userName] by userName of user logging
            var userName = HttpContext.Current.User.Identity.Name;
            var sessionID = HttpContext.Current.Session.SessionID;
            if (userName != "")
            {
                if (Application[userName] == null)
                {
                    Application[userName] = sessionID;
                }
            }
        }
        protected void Session_End()
        {
            //RemoveUserSession();

            // khi session cua user ket thuc thi xoa bien global Application[userName] voi userName la ten dang nhap cua user ket thuc session
            //if (HttpContext.Current != null)
            //{
            //    var userName = HttpContext.Current.User.Identity.Name;

            //    if (Application[userName] != null)
            //    {
            //        if (Convert.ToString(Application[userName]) == userName)
            //        {
            //            Application.Contents.Remove(userName);
            //        }
            //    }
            //}
        }

        private void RemoveUserSession()
        {
            string sessionID = Session["sessionID"].ToString();
            List<UserSessionInfo> listUserOnline = new List<UserSessionInfo>();
            if (Application["listUserOnline"] != null)
                listUserOnline = (List<UserSessionInfo>)Application["listUserOnline"];

            listUserOnline.RemoveAll(x => x.SessionID == sessionID);

            Application["listUserOnline"] = listUserOnline;
        }
    }
}