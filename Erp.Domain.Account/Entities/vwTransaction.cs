using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Account.Entities
{
    public class vwTransaction
    {
        public vwTransaction()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }
        public string TransactionCode { get; set; }

        public string TransactionType { get; set; }
        public string TransactionTypeName { get; set; }
        public string TargetType { get; set; }
        public string TargetCode { get; set; }
        public double? Total { get; set; }
        public double? Payment { get; set; }
        public double? Remain { get; set; }
        public Nullable<int> ParentId { get; set; }
        public string TargetName { get; set; }
        public Nullable<System.DateTime> NextPaymentDate { get; set; }
        public string PaymentMethod { get; set; }
    }
}
