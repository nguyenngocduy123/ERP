using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Erp.Domain.Interfaces;

namespace Erp.Domain.Repositories
{
    public class QueryHelper : IQueryHelper
    {
        readonly ErpDbContext context;

        public QueryHelper(ErpDbContext context)
        {
            this.context = context;
        }

        public int InsertEntity(object entity, string id = "id")
        {
            var tableName = entity.GetType().Name;
            var properties = entity.GetType().GetProperties();
            string propName = "";
            string propValue = "";
            foreach(var pro in properties)
            {
                var value = pro.GetValue(entity);
                if (pro.Name.ToLower() != id && value != null)
                {
                    propName += pro.Name + ",";
                    if (pro.PropertyType.Name == "String" || pro.PropertyType.Name == "Guid")
                        propValue += "N'" + value + "',";
                    else
                        propValue += value + ",";
                }
            }
            propName = propName.TrimEnd(',');
            propValue = propValue.TrimEnd(',');
            string sql = string.Format("INSERT INTO {0} ({1}) VALUES({2});", tableName, propName, propValue);

            try
            {
                return context.Database.ExecuteSqlCommand(sql);
            }
            catch { return -1; }
        }
        public int InsertEntity(List<object> list, string id = "id")
        {
            string sql = string.Empty;
            foreach (var entity in list)
            {
                var tableName = entity.GetType().Name;
                var properties = entity.GetType().GetProperties();
                string propName = "";
                string propValue = "";
                foreach (var pro in properties)
                {
                    var value = pro.GetValue(entity);
                    if (pro.Name.ToLower() != id && value != null)
                    {
                        propName += pro.Name + ",";
                        if (pro.PropertyType.Name == "String" || pro.PropertyType.Name == "Guid")
                            propValue += "N'" + value + "',";
                        else
                        {
                            if (pro.PropertyType == typeof(DateTime) || pro.PropertyType == typeof(Nullable<DateTime>))
                            {
                                DateTime date = (DateTime)value;
                                propValue += "CAST(N'" + date.ToString("yyyy-MM-dd hh:mm:ss") + "' AS DateTime)";
                            }
                            else
                                propValue += value + ",";
                        }
                    }
                }
                propName = propName.TrimEnd(',');
                propValue = propValue.TrimEnd(',');
                sql += string.Format("INSERT INTO {0} ({1}) VALUES({2});", tableName, propName, propValue);
            }

            if(!string.IsNullOrEmpty(sql))
                return context.Database.ExecuteSqlCommand(sql);

            return -1;
        }

        public bool UpdateFields(string tableName, Dictionary<string, object> field_Value, int id, string epressionCondition = "")
        {

            var listColumn = GetDataColumn(tableName);

            string sql = "Update " + tableName + " Set ";

            foreach(var field in field_Value)
            {
                var columnDataType = listColumn.Where(x => x.ColumnName == field.Key).First();

                string numberType = "int, decimal, float, bigint, smallint, money, numeric, tinyint, smallmoney";

                sql += field.Key + " = ";
                object value = field.Value;
                if (numberType.Contains(columnDataType.DataType) == true)                
                    sql += value + "";
                else
                    sql += "N'" + value + "'";

                if (field_Value.Last().Key != field.Key)
                    sql += ", ";
            }

            sql += " Where Id = " + id;

            if(string.IsNullOrEmpty(epressionCondition) == false)
            {
                sql += " And " + epressionCondition;
            }

            sql += ";";

            try
            {
                var recordEffect = context.Database.ExecuteSqlCommand(sql);

                if(recordEffect > 0)
                    return true;
            }
            catch (Exception ex){ }
            return false;
        }

        List<ColumnTableInfo> GetDataColumn(string tableName)
        {
            string sql = string.Format(@"SELECT ORDINAL_POSITION As OrdinalPosition, " +
                                     "COLUMN_NAME As ColumnName, " +
                                     "DATA_TYPE As DataType, " +
                                     "CHARACTER_MAXIMUM_LENGTH As CharMaxLen, " +
                                     "IS_NULLABLE As IsNullable" +
                                " FROM INFORMATION_SCHEMA.COLUMNS " +
                                " WHERE TABLE_NAME = '{0}'", tableName);


            var result = context.Database.SqlQuery<ColumnTableInfo>(sql);

            //List<ColumnTableInfo> listDataColumn = new List<ColumnTableInfo>();

            return result.ToList();
        }
    }

    public class ColumnTableInfo
    {
        public int? OrdinalPosition { get; set; }
        public string ColumnName { get; set; }
        public string DataType { get; set; }
        public int? CharMaxLen { get; set; }
        public string IsNullable { get; set; }
    }
}
