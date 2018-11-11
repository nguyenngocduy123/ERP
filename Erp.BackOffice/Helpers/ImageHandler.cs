using System;
using System.Web;
using Erp.Utilities;
//using System.Web.SessionState;

namespace Erp.BackOffice.Helpers
{
    public class ImageHandler : IHttpHandler //, IRequiresSessionState
    {
        /// <summary>
        /// You will need to configure this handler in the Web.config file of your 
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpHandler Members

        public bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            //write your handler implementation here.ư
            int width = 100;
            int height = 100;
            string path = Globals.NoImageDefaultPath;
            if (context.Request["W"] != null)
            {
                int.TryParse(context.Request["W"], out width);
            }
            if (context.Request["H"] != null)
            {
                int.TryParse(context.Request["H"], out height);
            }

            if (context.Request["ImagePath"] != null)
            {
                path = context.Request["ImagePath"];
            }


            try
            {
                System.Drawing.Image fullSizeImg = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(path.Replace("~", "")));
                System.Drawing.Image thumbNailImg = Erp.Utilities.ImageExt.Resize(fullSizeImg, width, height);
                thumbNailImg.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                thumbNailImg.Dispose();
                fullSizeImg.Dispose();
                
            }
            catch (Exception ex)
            {
                //throw ex;
            }            
            context.Response.ContentType = "image/jpeg";
            

        }

        #endregion
    }
}
