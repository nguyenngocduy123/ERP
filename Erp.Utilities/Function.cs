using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Utilities
{
   public class Function
    {
        public static int? GetInt(object _object)
        {
            int? result = null;
            if ((_object == null) || (_object == DBNull.Value))
                return result;
            try
            {
                result = Convert.ToInt32(_object);
            }
            catch
            {
                result = null;
            }
            return result;
        }
    }
}
