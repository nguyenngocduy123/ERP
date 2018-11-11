
using Erp.BackOffice.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Routing;
using WebMatrix.WebData;

namespace Erp.BackOffice.Filters
{
    public class ImageRouteHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            Erp.BackOffice.Helpers.Common.TrackRequest();

            string filename = requestContext.RouteData.Values["filename"] as string;
            string k = requestContext.HttpContext.Request.Url.Query.Replace("?k=", "");

            //var a = Erp.BackOffice.Helpers.StringCipher.Encrypt("84494", "caipasszodekhoilayduochinh");
            //var b = Erp.BackOffice.Helpers.StringCipher.Decrypt(k, "caipasszodekhoilayduochinh");

            if (string.IsNullOrEmpty(filename))
            {
                // return a 404 HttpHandler here
            }
            else
            {
                requestContext.HttpContext.Response.Clear();

                // find physical path to image here.  
                string filepath = "";
                if (filename != "img" && WebSecurity.IsAuthenticated)
                    filepath = requestContext.HttpContext.Server.MapPath("~/Data/HinhSV/" + filename);
                else
                {
                    try
                    {
                        filepath = requestContext.HttpContext.Server.MapPath("~/Data/HinhSV/" + Erp.BackOffice.Helpers.StringCipher.Decrypt(k, "caipasszodekhoilayduochinh") + ".jpg");
                    }
                    catch { }
                }

                if (File.Exists(filepath))
                {
                    requestContext.HttpContext.Response.ContentType = GetContentType(requestContext.HttpContext.Request.Url.ToString());
                    requestContext.HttpContext.Response.WriteFile(filepath);
                }
                else
                {
                    requestContext.HttpContext.Response.ContentType = "text/plain";
                    requestContext.HttpContext.Response.Write("File not found!");
                }

                requestContext.HttpContext.Response.End();
            }

            return null;
        }

        private static string GetContentType(String path)
        {
            switch (Path.GetExtension(path))
            {
                case ".bmp": return "Image/bmp";
                case ".gif": return "Image/gif";
                case ".jpg": return "Image/jpeg";
                case ".png": return "Image/png";
                default: break;
            }
            return "";
        }
    }
}