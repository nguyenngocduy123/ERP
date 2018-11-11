using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Office.Interop.PowerPoint;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;
using System.Configuration;
using System.Xml;
using Erp.Utilities;
//using HtmlAgilityPack;
//using ICSharpCode.SharpZipLib.Zip;


namespace Erp.BackOffice.Helpers
{
    using System.Diagnostics;

    public class PPTConvertHelper
    {
        /// <summary>
        /// Delete File or Directory
        /// </summary>
        /// <param name="path"></param>
        public void DeletePath(string path)
        {
            try
            {
                FileInfo finfo = new FileInfo(path);
                if (finfo.Attributes == FileAttributes.Directory)
                {
                    //recursively delete directory
                    Directory.Delete(path, true);
                }
                else
                {
                    File.Delete(path);
                }
            }
            catch
            {

            }
        }

        public static void WriteToErrorLog(string msg)
        {
            string path = Globals.UploadedFilePath;
            path = string.Format("{0}{1}", path, "PPTlogs.txt");
            path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(path));
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter s = new StreamWriter(fs);
            s.Close();
            fs.Close();
            FileStream fs1 = new FileStream(path, FileMode.Append, FileAccess.Write);
            StreamWriter s1 = new StreamWriter(fs1);
            s1.WriteLine(msg);
            s1.WriteLine();
            s1.Close();
            fs1.Close();
        }

        public static void KillProcessPPT()
        {
            //Application ppApp = new Application();
            //List<PPTConvertInfor> lstPPTConvertInfor = new List<PPTConvertInfor>();
            //try
            //{
            //    ppApp.WindowState = PpWindowState.ppWindowMinimized;
            //    ppApp.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
            //    ppApp.Quit();
            //}
            //catch (Exception ex)
            //{
            //    WriteToErrorLog(ex.Message);
            //}

            Process[] processes = Process.GetProcessesByName("POWERPNT");
            foreach (var process in processes)
            {
                process.Kill();
                process.WaitForExit();
            }
        }
    }
}