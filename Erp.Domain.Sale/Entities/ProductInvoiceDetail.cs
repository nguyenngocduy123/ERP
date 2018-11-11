using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class ProductInvoiceDetail
    {
        public ProductInvoiceDetail()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public Nullable<int> ProductInvoiceId { get; set; }
        public int ProductId { get; set; }
        public decimal? Price { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string Unit { get; set; }

        public int? PromotionId { get; set; }
        public int? PromotionDetailId { get; set; }
        public double? PromotionValue { get; set; }

        public string ProductType { get; set; }
        public int? IrregularDiscount { get; set; }
        public decimal? IrregularDiscountAmount { get; set; }
        public int? FixedDiscount { get; set; }
        public decimal? FixedDiscountAmount { get; set; }
        public Nullable<bool> CheckPromotion { get; set; }
        public bool IsReturn { get; set; }
        public int? QuantitySaleReturn { get; set; }
        public int? StaffId { get; set; }
        public string Status { get; set; }
        public string LoCode { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
    }
}
