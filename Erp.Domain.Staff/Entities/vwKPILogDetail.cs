using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities
{
    public class vwKPILogDetail
    {
        public vwKPILogDetail()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public int KPILogId { get; set; }
        public int StaffId { get; set; }
        public double AchieveKPIWeight { get; set; }
        public string StaffName { get; set; }
        public bool Completed { get; set; }
    }
}
