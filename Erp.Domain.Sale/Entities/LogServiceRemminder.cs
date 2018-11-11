using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class LogServiceRemminder
    {
        public LogServiceRemminder()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }
        //public string Name { get; set; }

        public Nullable<int> ProductInvoiceDetailId { get; set; }
        public Nullable<int> ReminderId { get; set; }
        public string ReminderName { get; set; }
        public Nullable<System.DateTime> ReminderDate { get; set; }
        public Nullable<int> ServiceId { get; set; }

    }
}
