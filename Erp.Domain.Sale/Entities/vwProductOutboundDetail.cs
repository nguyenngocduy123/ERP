using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class vwProductOutboundDetail
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }

        public int? ProductOutboundId { get; set; }

        public int? ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }

        public int? Quantity { get; set; }

        public decimal? Price { get; set; }

        public string Unit { get; set; }

        public string WarehouseDestinationName { get; set; }

        public string WarehouseSourceName { get; set; }

        public string PurchaseOrderCode { get; set; }

        public string ProductOutboundCode { get; set; }
        public string LoCode { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public int? WarehouseDestinationId { get; set; }

        public int? WarehouseSourceId { get; set; }
        public bool IsArchive { get; set; }

        public string Type { get; set; }
        public string InvoiceCode { get; set; }
        public string CustomerName { get; set; }
        public string ProductType { get; set; }
    }
}
