using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Account.Entities
{
    public class vwPayment
    {
        public vwPayment()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }
        public string Name { get; set; }

        public string Code { get; set; }
        public decimal? Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string BankAccountNo { get; set; }
        public string BankAccountName { get; set; }
        public string BankName { get; set; }
        public string Receiver { get; set; }
        public string Note { get; set; }
        public Nullable<int> SalerId { get; set; }
        public Nullable<System.DateTime> VoucherDate { get; set; }
        public string Address { get; set; }
        public string SalerName { get; set; }
        public Nullable<int> TargetId { get; set; }
        public string TargetName { get; set; }
        public string ReceiverUserName { get; set; }
        public string MaChungTuGoc { get; set; }
        public string LoaiChungTuGoc { get; set; }
        public string CancelReason { get; set; }
        public Nullable<int> BranchId { get; set; }
        public bool IsArchive { get; set; }
    }
}
