using System;
using DSofT.Framework.Internal.ApiListener.Filters;
using System.Web.Http;
using AutoMapper;

namespace DSofT.Warehouse.ApiListener
{


    public class WebApiApplication : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configuration.Filters.Add(new ExceptionApiAttribute());
            GlobalConfiguration.Configure(WebApiConfig.Register);
           
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}