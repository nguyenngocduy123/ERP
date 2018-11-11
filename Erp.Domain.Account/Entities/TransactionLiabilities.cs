using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Account.Entities
{
    public class TransactionLiabilities
    {
        public TransactionLiabilities()
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
        public string TransactionModule { get; set; }
        public string TransactionName { get; set; }
        public string TargetModule { get; set; }
        public string TargetCode { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public Nullable<System.DateTime> NextPaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public string MaChungTuGoc { get; set; }
        public string LoaiChungTuGoc { get; set; }
        public string Note { get; set; }
    }
}
