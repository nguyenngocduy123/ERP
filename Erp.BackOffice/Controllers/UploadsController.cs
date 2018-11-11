using Erp.BackOffice.Models;
using Erp.Utilities;
using Erp.Utilities.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Erp.BackOffice.Controllers
{
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class UploadsController : Controller
    {
        public ActionResult Browse(string ckEditorFuncNum)
        {
            List<FileInformation> fileInfoList = GetCurrentFiles();

            var model = new FileListingViewModel
            {
                Files = fileInfoList,
                CKEditorFuncNum = ckEditorFuncNum
            };

            return View(model);
        }

        public ActionResult Upload(HttpPostedFileBase upload, string ckEditorFuncNum, string ckEditor, string langCode)
        {
            string fileExts = WebConfigurationManager.AppSettings["file-not-upload"];
            if (string.IsNullOrEmpty(fileExts))
            {
                fileExts = ".html,.htm,.jsp,.asp,.aspx,.php,.css,.js,.cshtml";
            }
            var fileExtsArrays = fileExts.Split(',');
            

            string fileName = upload.FileName;
            string basePath = Server.MapPath(Globals.UploadedFilePath);
            string fileNameExt = Path.GetExtension(upload.FileName);
            if (fileExtsArrays.Any(x => x.Contains(fileNameExt.ToLower())))
            {
                ViewBag.msgUpload = "Upload fail, not support: " + fileExts;
            }
            else
            {
                fileName = FileHelper.StandardizeFileName(basePath, fileName);
                fileName = basePath + fileName;
                upload.SaveAs(fileName);
                ViewBag.msgUpload = "Upload Succeeded";
            }
            return View();
        }

        private List<FileInformation> GetCurrentFiles()
        {
            string basePath = Server.MapPath(Globals.UploadedFilePath);

            var fileInfoList = new List<FileInformation>();

            string[] files = Directory.GetFiles(basePath);

            string tempFileName = "";
            string tempFullFileName = "";
            files.ToList().ForEach(file =>
            {
                tempFileName = Path.GetFileName(file);
                tempFullFileName = basePath + tempFileName;
                fileInfoList.Add(new FileInformation { FileName = tempFileName, CreatedDate = new FileInfo(tempFullFileName).CreationTime });
            });

            return fileInfoList.OrderByDescending(x => x.CreatedDate).ToList();
        }

    }
}
