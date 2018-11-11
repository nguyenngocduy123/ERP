using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class ProductDamaged
    {
        public ProductDamaged()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }

        public string Status { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string Reason { get; set; }
        public Nullable<int> ProductInboundId { get; set; }
        public Nullable<int> ProductInboundDetailId { get; set; }

    }
}
