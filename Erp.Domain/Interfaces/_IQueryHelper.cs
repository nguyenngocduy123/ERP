using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Interfaces
{
    public interface IQueryHelper
    {
        int InsertEntity(object entity, string id = "id");
        int InsertEntity(List<object> list, string id = "id");
        bool UpdateFields(string tableName, Dictionary<string, object> field_Value, int id, string epressionCondition = "");
    }
}
