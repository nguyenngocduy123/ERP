using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Erp.Utilities.Helpers
{
    public sealed class FileHelper
    {
        private FileHelper(){ }

        /// <summary>
        /// base64string is in form of data:image/png;base64,iVBORw0KGgoAAA
        /// this function is to write a physical file based on the based64String into the given filePath.
        /// </summary>
        /// <param name="base64">base64string is in form of data:image/png;base64,iVBORw0KGgoAAA</param>
        /// <param name="filePath">the path where the phsical file to be saved
        /// <remarks>the path is enclosed by a forward slash '/'</remarks>
        /// </param>
        /// <returns>the physical file's relative path; return string.empty if it's failed to save the file.
        /// </returns>
        public static string WriteFileFromBase64String(string base64, string filePath)
        {
            string dataType = base64.Substring(0, base64.IndexOf(';'));// return some kind of "data:image/png"
            string fileExtesion = dataType.Substring(dataType.IndexOf('/') + 1);
            string fileName = Guid.NewGuid().ToString() + "." + fileExtesion;
            string absoluteFileName = HttpContext.Current.Server.MapPath(filePath) + fileName; // full file name which includes server absolute path and file name.


            string dataBinaryContent = base64.Substring(base64.IndexOf(',') + 1);
            byte[] buffer = Convert.FromBase64String(dataBinaryContent);


            if (WriteFile(buffer, absoluteFileName))
            {
                string relativeFileName = filePath + fileName;
                return relativeFileName;
            }

            return string.Empty;
        }

        /// <summary>
        /// save file to server
        /// </summary>
        /// <param name="objFile"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string SaveFile(HttpPostedFileBase objFile, string filePath)
        {
            string sourceFileName = string.Empty;
            string targetFolder;
            if (objFile != null)
            {
                try
                {
                    targetFolder = HttpContext.Current.Server.MapPath(filePath);

                    sourceFileName = Path.GetFileName(objFile.FileName);

                    string fileName = StandardizeFileName(targetFolder, sourceFileName);

                    objFile.SaveAs(targetFolder + fileName);

                    

                    return filePath + fileName;

                }
                catch (IOException)
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// create fileName Standar.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string StandardizeFileName(string path, string fileName)
        {
            string name = Path.GetFileNameWithoutExtension(fileName);
            string ext = Path.GetExtension(fileName);
            string result = name;
            int count = 1;
            while (File.Exists(path + fileName.Replace(name, result)) == true)
            {
                result = name + "_" + count.ToString(System.Globalization.CultureInfo.CurrentCulture);
                count++;
            }
            result = result + ext;
            return result;
        }

        public static bool WriteFile(byte[] buffer, string fullName)
        {
            try
            {
                FileStream fileStream = new FileStream(fullName, FileMode.Append);
                fileStream.Write(buffer, 0, buffer.Length);
                fileStream.Flush();
                fileStream.Close();
                return true;
            }
            catch(IOException)
            {
                return false;
            }
        }

        public static bool IsValidImage(string filePath)
        {
            return File.Exists(filePath) && IsValidImage(new FileStream(filePath, FileMode.Open, FileAccess.Read));
        }

        public static bool IsValidImage(Stream imageStream)
        {
            if (imageStream.Length > 0)
            {
                byte[] header = new byte[4]; // Change size if needed.
                string[] imageHeaders = new[]{
                "\xFF\xD8", // JPEG
                "BM",       // BMP
                "GIF",      // GIF
                "JPG",
                Encoding.ASCII.GetString(new byte[]{255, 216, 255, 224}),//jpeg
                Encoding.ASCII.GetString(new byte[]{255, 216, 255, 225}),//jpeg 2 canon
                Encoding.ASCII.GetString(new byte[]{137, 80, 78, 71})}; // PNG

                imageStream.Read(header, 0, header.Length);

                bool isImageHeader = imageHeaders.Count(str => Encoding.ASCII.GetString(header).StartsWith(str)) > 0;
                if (isImageHeader == true)
                {
                    try
                    {
                        System.Drawing.Image.FromStream(imageStream).Dispose();
                        imageStream.Close();
                        return true;
                    }

                    catch
                    {

                    }
                }
            }

            imageStream.Close();
            return false;
        }

        public static void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
                File.Delete(filePath);
        }
        public static bool IsValidExcel(string contentType)
        {
            string a = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string b = "application/vnd.ms-excel";
            return contentType.ToLower() == a || contentType.ToLower() == b;
        }
  
    }
}
