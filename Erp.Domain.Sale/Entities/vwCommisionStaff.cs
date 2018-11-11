using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class vwCommisionStaff
    {
        public vwCommisionStaff()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public Nullable<int> StaffId { get; set; }
        public Nullable<int> BranchId { get; set; }
        public Nullable<int> InvoiceId { get; set; }
        public Nullable<int> InvoiceDetailId { get; set; }
        public string InvoiceType { get; set; }
        public Nullable<int> PercentOfCommision { get; set; }
        public decimal? AmountOfCommision { get; set; }
        public string Note { get; set; }
        public Nullable<bool>IsResolved { get; set; }
        public string StaffName { get; set; }
        public string StaffProfileImage { get; set; }
        public string StaffCode { get; set; }
        public string ProductInvoiceCode { get; set; }
        public string BranchName { get; set; }
        public string ProductCode { get; set; }
        public string ProductImage { get; set; }
        public string ProductName { get; set; }
        public Nullable<int> month { get; set; }
        public Nullable<int> year { get; set; }
    }
}
