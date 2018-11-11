using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Sale.Models
{
    public class SalesReturnsDetailViewModel
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
        public Nullable<int> SalesReturnsId { get; set; }

        [Display(Name = "ProductId", ResourceType = typeof(Wording))]
        public Nullable<int> ProductId { get; set; }

        [Display(Name = "ProductId", ResourceType = typeof(Wording))]
        public string ProductName { get; set; }

        [Display(Name = "ProductCode", ResourceType = typeof(Wording))]
        public string ProductCode { get; set; }

        [Display(Name = "Price", ResourceType = typeof(Wording))]
        public decimal? Price { get; set; }

        [Display(Name = "Quantity", ResourceType = typeof(Wording))]
        public Nullable<int> Quantity { get; set; }

        [Display(Name = "Discount", ResourceType = typeof(Wording))]
        public int? DisCount { get; set; }
        [Display(Name = "DisCountAmount", ResourceType = typeof(Wording))]
        public int? DisCountAmount { get; set; }

        //[Display(Name = "Promotion", ResourceType = typeof(Wording))]
        //public int? PromotionId { get; set; }

        //public int? PromotionDetailId { get; set; }

        //[Display(Name = "PromotionValue", ResourceType = typeof(Wording))]
        //public double? PromotionValue { get; set; }

        public int? QuantityInInventory { get; set; }
        [Display(Name = "CategoryCode", ResourceType = typeof(Wording))]
        public string CategoryCode { get; set; }
        public bool? Check { get; set; }
        public int OrderNo { get; set; }
        [Display(Name = "CheckPromotion", ResourceType = typeof(Wording))]
        public bool? CheckPromotion { get; set; }
        [Display(Name = "ProductGroup", ResourceType = typeof(Wording))]
        public string ProductGroup { get; set; }

        [Display(Name = "Unit", ResourceType = typeof(Wording))]
        public string UnitProduct { get; set; }

        [Display(Name = "SalesReturnDate", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> SalesReturnDate { get; set; }

        public Nullable<int> ProductInvoiceId { get; set; }
        public Nullable<int> ProductInvoiceDetailId { get; set; }
        [Display(Name = "ProductInvoiceCode", ResourceType = typeof(Wording))]
        public string ProductInvoiceCode { get; set; }
        [Display(Name = "SalerInvoiceName", ResourceType = typeof(Wording))]
        public string SalerInvoiceName { get; set; }
        [Display(Name = "ProductInvoiceDate", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> ProductInvoiceDate { get; set; }
    }
}