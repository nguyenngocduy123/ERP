using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities
{
    public class SymbolTimekeeping
    {
        public SymbolTimekeeping()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<bool> Timekeeping { get; set; }
        public Nullable<bool> DayOff { get; set; }
        public Nullable<bool> Absent { get; set; }
        public string Color { get; set; }
        public string CodeDefault { get; set; }
        public string Icon { get; set; }
    }
}
