using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class vwServiceReminderGroup
    {
        public vwServiceReminderGroup()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }

        public Nullable<int> ServiceId { get; set; }
        public Nullable<int> ServiceReminderId { get; set; }
        public string Name { get; set; }
        public Nullable<bool> Reminder { get; set; }
        public Nullable<int> QuantityDate { get; set; }
    }
}
