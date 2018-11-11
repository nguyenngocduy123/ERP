using Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace Erp.BackOffice.Helpers
{
    public class ExcelHelper
    {
        public static DataTable readDataExcel(String filePath)
        {
            bool isEx2003 = false;
            DataTable dt = new DataTable();
            FileStream stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read);

            IExcelDataReader excelReader;
            if (Path.GetExtension(filePath).Equals(".xlsx"))
            {
                excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            }
            else
            {
                excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                isEx2003 = true;
            }

            //4. DataSet - Create column names from first row
            excelReader.IsFirstRowAsColumnNames = true;
            DataSet result = excelReader.AsDataSet();
            excelReader.Dispose();
            excelReader.Close();
            if (dt != null && dt.Rows.Count > 0 && isEx2003 == true)
                dt.Rows.RemoveAt(0);
            return result.Tables[0];
        }
    }
}