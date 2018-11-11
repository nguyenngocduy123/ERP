using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class vwUsingServiceLogDetail
    {
        public vwUsingServiceLogDetail()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }

        public Nullable<int> UsingServiceId { get; set; }
        public Nullable<int> StaffId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ProfileImage { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string ProductInvoiceCode { get; set; }
        public string LastName { get; set; }
        public Nullable<int> ProductInvoiceId { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public string ServiceName { get; set; }
        public Nullable<int> ServiceInvoiceDetailId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public Nullable<bool> IsVote { get; set; }
        public string CustomerImage { get; set; }
        public Nullable<int> BranchId { get; set; }
    }
}
