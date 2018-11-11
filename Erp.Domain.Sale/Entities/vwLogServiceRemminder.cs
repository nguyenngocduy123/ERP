using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class vwLogServiceRemminder
    {
        public vwLogServiceRemminder()
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
        public string ProductGroupCode { get; set; }
        public string ProductGroupName { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string CustomerName { get; set; }
        public string ProductInvoiceCode { get; set; }
        public Nullable<System.DateTime> ProductInvoiceDate { get; set; }
        public Nullable<int> ProductInvoiceId { get; set; }
        public Nullable<int> CustomerId { get; set; }
    }
}
