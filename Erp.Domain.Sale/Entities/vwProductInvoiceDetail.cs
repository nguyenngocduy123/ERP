using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class vwProductInvoiceDetail
    {
        public vwProductInvoiceDetail()
        {

        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public Nullable<int> ProductInvoiceId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public decimal? Price { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string Unit { get; set; }
        public string ProductType { get; set; }

        public int? PromotionId { get; set; }
        public int? PromotionDetailId { get; set; }
        public double? PromotionValue { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string CategoryCode { get; set; }
        public string ProductGroup { get; set; }
        public string ProductInvoiceCode { get; set; }
        //public int? CustomerId { get; set; }

        public Nullable<bool> CheckPromotion { get; set; }
        public bool IsReturn { get; set; }
        public bool IsArchive { get; set; }
        public System.DateTime ProductInvoiceDate { get; set; }
        public decimal Amount { get; set; }
        public int? SalerId { get; set; }
        public string SalerFullName { get; set; }
        public string Manufacturer { get; set; }
        public int BranchId { get; set; }
        public Nullable<bool> IsCombo { get; set; }
        public int? QuantitySaleReturn { get; set; }
        public string BranchName { get; set; }
        public string StaffName { get; set; }
        public int? StaffId { get; set; }
        public string Status { get; set; }
        public int? IrregularDiscount { get; set; }
        public decimal? IrregularDiscountAmount { get; set; }
        public int? FixedDiscount { get; set; }
        public decimal? FixedDiscountAmount { get; set; }
        public string Type { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public int? CustomerId { get; set; }

        public string LoCode { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public string DistrictId { get; set; }
        public string CityId { get; set; }
        public string ProvinceName { get; set; }
        public string DistrictName { get; set; }
        public Nullable<int> Day { get; set; }
        public Nullable<int> Month { get; set; }
        public Nullable<int> Year { get; set; }
    }
}
