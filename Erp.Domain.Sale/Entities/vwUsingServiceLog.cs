using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class vwUsingServiceLog
    {
        public vwUsingServiceLog()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }

        public Nullable<int> ServiceInvoiceDetailId { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> QuantityUsed { get; set; }
        public Nullable<int> ServiceId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string CategoryCode { get; set; }
        public Nullable<bool> IsCombo { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public string ItemCategory { get; set; }
        public Nullable<int> ServiceComboId { get; set; }
        public string SalerName { get; set; }
        public Nullable<int> ProductInvoiceId { get; set; }
      
        public Nullable<System.DateTime> ProductInvoiceDate { get; set; }
        public string ProductInvoiceCode { get; set; }

        public Nullable<int> CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
    }
}
