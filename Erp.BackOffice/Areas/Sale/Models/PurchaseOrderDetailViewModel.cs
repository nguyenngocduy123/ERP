using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Sale.Models
{
    public class PurchaseOrderDetailViewModel
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

        [Display(Name = "CategoryCode", ResourceType = typeof(Wording))]
        public string CategoryCode { get; set; }
        [Display(Name = "PurchaseOrderId", ResourceType = typeof(Wording))]
        public Nullable<int> PurchaseOrderId { get; set; }
        [Display(Name = "ProductId", ResourceType = typeof(Wording))]
        public Nullable<int> ProductId { get; set; }
        [Display(Name = "ProductId", ResourceType = typeof(Wording))]
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        [Display(Name = "Price", ResourceType = typeof(Wording))]
        public decimal? Price { get; set; }
        [Display(Name = "Quantity", ResourceType = typeof(Wording))]
        public Nullable<int> Quantity { get; set; }
        public int? QuantityInInventory  { get; set; }
        public int? DisCount { get; set; }
        public int? DisCountAmount { get; set; }
        public string Unit { get; set; }
        public int OrderNo { get; set; }
        public int? QuantityRemaining { get; set; }

        public string ProductGroup { get; set; }
        public string PurchaseOrderCode { get; set; }
        public bool IsArchive { get; set; }
        public System.DateTime PurchaseOrderDate { get; set; }
        public decimal Amount { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Manufacturer { get; set; }
        public string Description { get; set; }
    }
}