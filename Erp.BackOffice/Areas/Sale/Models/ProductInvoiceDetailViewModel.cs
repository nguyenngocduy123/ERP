using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Sale.Models
{
    public class ProductInvoiceDetailViewModel
    {
        public int Id { get; set; }

        [Display(Name = "IsDeleted")]
        public bool? IsDeleted { get; set; }

        [Display(Name = "CreatedUser", ResourceType = typeof(Wording))]
        public int? CreatedUserId { get; set; }
        public string CreatedUserName { get; set; }

        [Display(Name = "CreatedDate", ResourceType = typeof(Wording))]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "ModifiedUser", ResourceType = typeof(Wording))]
        public int? ModifiedUserId { get; set; }
        public string ModifiedUserName { get; set; }

        [Display(Name = "ModifiedDate", ResourceType = typeof(Wording))]
        public DateTime? ModifiedDate { get; set; }

        [Display(Name = "ProductOrderId", ResourceType = typeof(Wording))]
        public Nullable<int> ProductInvoiceId { get; set; }

        [Display(Name = "ProductId", ResourceType = typeof(Wording))]
        public Nullable<int> ProductId { get; set; }

        [Display(Name = "ProductId", ResourceType = typeof(Wording))]
        public string ProductName { get; set; }

        [Display(Name = "ProductCode", ResourceType = typeof(Wording))]
        public string ProductCode { get; set; }

        [Display(Name = "Price", ResourceType = typeof(Wording))]
        public decimal? Price { get; set; }
        [Display(Name = "Price", ResourceType = typeof(Wording))]
        public decimal? PriceTest { get; set; }
        [Display(Name = "Quantity", ResourceType = typeof(Wording))]
        public Nullable<int> Quantity { get; set; }

        [Display(Name = "Promotion", ResourceType = typeof(Wording))]
        public int? PromotionId { get; set; }

        public int? PromotionDetailId { get; set; }

        [Display(Name = "PromotionValue", ResourceType = typeof(Wording))]
        public double? PromotionValue { get; set; }

        public int? QuantityInInventory { get; set; }

        public string Unit { get; set; }

        [Display(Name = "CategoryCode", ResourceType = typeof(Wording))]
        public string CategoryCode { get; set; }
        public string ProductType { get; set; }

        [Display(Name = "ProductCode", ResourceType = typeof(Wording))]
        public string ProductInvoiceCode { get; set; }
        public int OrderNo { get; set; }
        public List<WarehouseLocationItemViewModel> ListWarehouseLocationItemViewModel { get; set; }
        [Display(Name = "ProductGroup", ResourceType = typeof(Wording))]
        public string ProductGroup { get; set; }
        public string ProductGroupName { get; set; }
        [Display(Name = "LoCode", ResourceType = typeof(Wording))]
        public string LoCode { get; set; }
        [Display(Name = "ExpiryDateItem", ResourceType = typeof(Wording))]
        public DateTime? ExpiryDate { get; set; }
        public string strExpiryDate { get; set; }
        [Display(Name = "CheckPromotion", ResourceType = typeof(Wording))]
        public bool? CheckPromotion { get; set; }
        public bool IsReturn { get; set; }
        public bool? IsCombo { get; set; }
        public System.DateTime ProductInvoiceDate { get; set; }

        public string NoteOfServiceReminder { get; set; }
        ////using service log detail
        //public string Status { get; set; }
        //public int? StaffId { get; set; }
        //public string SalerName { get; set; }
        //public int? Point { get; set; }
        //lưu số sản phẩm trả lại trong đơn hàng trả lại.
        public int? QuantitySaleReturn { get; set; }
        public decimal? Amount { get; set; }
        [Display(Name = "Description", ResourceType = typeof(Wording))]
        public string Description { get; set; }

        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string StaffName { get; set; }

        public int? IrregularDiscount { get; set; }
        public decimal? IrregularDiscountAmount { get; set; }
        public int? FixedDiscount { get; set; }
        public decimal? FixedDiscountAmount { get; set; }
        public string Type { get; set; }
        [Display(Name = "Phone", ResourceType = typeof(Wording))]
        public string CustomerPhone { get; set; }
        [Display(Name = "Customer", ResourceType = typeof(Wording))]
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public int? CustomerId { get; set; }
        public Nullable<int> Day { get; set; }
        public Nullable<int> Month { get; set; }
        public Nullable<int> Year { get; set; }
        public string DistrictId { get; set; }
        public string CityId { get; set; }
        public string ProvinceName { get; set; }
        public string DistrictName { get; set; }
    }
}