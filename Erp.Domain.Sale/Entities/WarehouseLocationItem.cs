using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class WarehouseLocationItem
    {
        public WarehouseLocationItem()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }

        public string SN { get; set; }
        public string Shelf { get; set; }
        public string Floor { get; set; }
        public string Position { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public Nullable<bool> IsOut { get; set; }
        public Nullable<int> WarehouseId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<int> ProductInboundId { get; set; }
        public Nullable<int> ProductInboundDetailId { get; set; }
        public Nullable<int> ProductOutboundId { get; set; }
        public Nullable<int> ProductOutboundDetailId { get; set; }
        public string LoCode { get; set; }
    }
}
