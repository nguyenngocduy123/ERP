using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class vwProductInboundDetail
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }

        public int? ProductInboundId { get; set; }

        public int? ProductId { get; set; }

        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string ProductBarCode { get; set; }

        public int? Quantity { get; set; }

        public decimal? Price { get; set; }

        public string Unit { get; set; }


        public int? WarehouseDestinationId { get; set; }

        public string WarehouseDestinationName { get; set; }

        public string ShipperName { get; set; }

        public string ProductInboundCode { get; set; }

        public int? PurchaseOrderId { get; set; }
        
        public int? BranchId { get; set; }

        public string Type { get; set; }

        public decimal? TotalAmount { get; set; }
        public string LoCode { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }

        public int? ProductDamagedId { get; set; }
        public string Status { get; set; }
        public Nullable<int> NumberAmount { get; set; }
        public string Reason { get; set; }
        public bool IsArchive { get; set; }
        public string SalesReturnsCode { get; set; }
        public string CustomerName { get; set; }
    }
}
