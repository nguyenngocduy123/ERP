using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Entities
{
    public class LoginLog
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Nullable<System.DateTime> LoginTime { get; set; }
        public string TypeWebsite { get; set; }
     

    }
}
