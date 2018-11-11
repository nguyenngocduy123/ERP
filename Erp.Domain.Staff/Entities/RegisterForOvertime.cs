using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities
{
    public class RegisterForOvertime
    {
        public RegisterForOvertime()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }
        public string Code { get; set; }

        public Nullable<int> StaffId { get; set; }
        public Nullable<System.DateTime> DayOvertime { get; set; }
        public Nullable<System.DateTime> StartHour { get; set; }
        public Nullable<System.DateTime> EndHour { get; set; }
        public string Note { get; set; }
     
    }
}
