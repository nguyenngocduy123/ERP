using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class ProductInbound
    {
        public ProductInbound()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public string Code { get; set; }
        public string Note { get; set; }
        public Nullable<int> WarehouseKeeperId { get; set; }
        public Nullable<int> WarehouseDestinationId { get; set; }
        public string Type { get; set; }
        public Nullable<int> PurchaseOrderId { get; set; }
        public decimal? TotalAmount { get; set; }

        public Nullable<bool> IsDone { get; set; }
        public int? BranchId { get; set; }

        public string ShipperName { get; set; }
        public string ShipperPhone { get; set; }
        public int? SupplierId { get; set; }
        public Nullable<int> SalesReturnsId { get; set; }
        public Nullable<int> PhysicalInventoryId { get; set; }
        public Nullable<int> ProductOutboundId { get; set; }
        public bool IsArchive { get; set; }
        public int? CreatedStaffId { get; set; }
    }
}
