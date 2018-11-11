using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class PurchaseOrder
    {
        public PurchaseOrder()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }
        public string Code { get; set; }
        public decimal? TotalAmount { get; set; }
        public double? TaxFee { get; set; }
        public double? Discount { get; set; }
        public string DiscountCode { get; set; }
        public Nullable<int> SupplierId { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
        public string Phone { get; set; }

        public bool? IsArchive { get; set; }
        public int? BranchId { get; set; }
        public string PaymentMethod { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? RemainingAmount { get; set; }
        public string CancelReason { get; set; }
        public string BarCode { get; set; }
        public int? WarehouseSourceId { get; set; }
        public int? WarehouseDestinationId { get; set; }
        public int? ProductInboundId { get; set; }
        public string Type { get; set; }
        public Nullable<System.DateTime> NextPaymentDate { get; set; }
    }
}
