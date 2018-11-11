using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Account.Entities
{
    public class ProcessPayment
    {
        public ProcessPayment()
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

        public Nullable<int> OrderNo { get; set; }
        public Nullable<System.DateTime> DayPayment { get; set; }
        public Nullable<int> MoneyPayment { get; set; }
        public string FormPayment { get; set; }
        public string CodeTrading { get; set; }
        public string Bank { get; set; }
        public string Payer { get; set; }
        public Nullable<int> ContractId { get; set; }
        public string Status { get; set; }
        public string TransactionCode { get; set; }
    }
}
