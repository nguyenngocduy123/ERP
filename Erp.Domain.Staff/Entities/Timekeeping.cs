using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities
{
    public class Timekeeping
    {
        public Timekeeping()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public Nullable<int> StaffId { get; set; }
        public Nullable<System.DateTime> HoursIn { get; set; }
        public Nullable<System.DateTime> HoursOut { get; set; }
        //public Nullable<int> Pay { get; set; }
        public Nullable<int> ShiftsId { get; set; }
        //public Nullable<int> Money { get; set; }
       
    }
}
