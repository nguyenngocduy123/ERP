using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Crm.Entities
{
    public class vwSMSLog
    {
        public vwSMSLog()
        {

        }
        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public int CustomerID { get; set; }
        public int TargetID { get; set; }
        public string TargetModule { get; set; }
        public string Body { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> SentDate { get; set; }
       
        public string Customer { get; set; }
        public string Phone { get; set; }
    }

}
