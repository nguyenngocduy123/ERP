using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Account.Entities
{
    public class ContractSell
    {
        public ContractSell()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }

        public Nullable<int> CondosId { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> Price { get; set; }
        public string Unit { get; set; }
        public string VAT { get; set; }
        public Nullable<int> MaintenanceCosts { get; set; }
        public string UnitMoney { get; set; }
        public Nullable<System.DateTime> DayHandOver { get; set; }
        public Nullable<System.DateTime> DayPay { get; set; }

    }
}
