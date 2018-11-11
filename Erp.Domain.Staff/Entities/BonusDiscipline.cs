using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities
{
    public class BonusDiscipline
    {
        public BonusDiscipline()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public string Category { get; set; }
        public string Formality { get; set; }
        public string Reason { get; set; }
        public Nullable<System.DateTime> DayDecision { get; set; }
        public Nullable<System.DateTime> DayEffective { get; set; }
        public Nullable<int> PlaceDecisions { get; set; }
        public string Note { get; set; }
        public Nullable<int> StaffId { get; set; }
        public string Code { get; set; }
        public Nullable<int> Money { get; set; }
    }
}
