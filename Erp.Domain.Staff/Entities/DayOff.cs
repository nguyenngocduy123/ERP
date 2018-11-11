using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities
{
    public class DayOff
    {
        public DayOff()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public string Code { get; set; }
        public Nullable<System.DateTime> DayStart { get; set; }
        public Nullable<System.DateTime> DayEnd { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> TypeDayOffId { get; set; }
        public Nullable<int> QuantityNotUsed { get; set; }
        public Nullable<int> StaffId { get; set; }

    }
}
