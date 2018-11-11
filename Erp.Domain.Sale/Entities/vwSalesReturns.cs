using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class vwSalesReturns
    {
        public vwSalesReturns()
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
        public Nullable<int> CustomerId { get; set; }
        public decimal? TotalAmount { get; set; }
        public double? TaxFee { get; set; }
        public double? Discount { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
        public Nullable<int> BranchId { get; set; }
        public string PaymentMethod { get; set; }
        public decimal? PaidAmount { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string BranchName { get; set; }
        public int? SalerId { get; set; }
        public string SalerFullName { get; set; }
    }
}
