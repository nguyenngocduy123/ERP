using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class CommisionCustomer
    {
        public CommisionCustomer()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public Nullable<int> CommissionCusId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public decimal CommissionValue { get; set; }
        public Nullable<bool> IsMoney { get; set; }
    }
}
