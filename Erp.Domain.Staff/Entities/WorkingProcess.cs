using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities
{
    public class WorkingProcess
    {
        public WorkingProcess()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string Name { get; set; }

        public string WorkPlace { get; set; }
        public string Position { get; set; }
        public Nullable<System.DateTime> DayStart { get; set; }
        public Nullable<System.DateTime> DayEnd { get; set; }
        public Nullable<int> BonusDisciplineId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Nullable<int> StaffId { get; set; }
       
    }
}
