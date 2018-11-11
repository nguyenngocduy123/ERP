using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Sale.Models
{
    public class WarehouseLocationItemViewModel
    {
        public int? Id { get; set; }

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

        [Display(Name = "AssignedUserId", ResourceType = typeof(Wording))]
        public int? AssignedUserId { get; set; }
        public string AssignedUserName { get; set; }

        [Display(Name = "SN", ResourceType = typeof(Wording))]
        public string SN { get; set; }
        [Display(Name = "Shelf", ResourceType = typeof(Wording))]
        public string Shelf { get; set; }
        [Display(Name = "Floor", ResourceType = typeof(Wording))]
        public string Floor { get; set; }
        [Display(Name = "PositionItem", ResourceType = typeof(Wording))]
        public string Position { get; set; }

        [Display(Name = "ExpiryDateItem", ResourceType = typeof(Wording))]
        public DateTime? ExpiryDate { get; set; }

        [Display(Name = "IsOut", ResourceType = typeof(Wording))]
        public Nullable<bool> IsOut { get; set; }
        
        [Display(Name = "Warehouse", ResourceType = typeof(Wording))]
        public Nullable<int> WarehouseId { get; set; }
        
        [Display(Name = "Product", ResourceType = typeof(Wording))]
        public Nullable<int> ProductId { get; set; }
        
        [Display(Name = "ProductInboundCode", ResourceType = typeof(Wording))]
        public Nullable<int> ProductInboundId { get; set; }

        public Nullable<int> ProductInboundDetailId { get; set; }


        [Display(Name = "Warehouse", ResourceType = typeof(Wording))]
        public string WarehouseName { get; set; }

        [Display(Name = "Product", ResourceType = typeof(Wording))]
        public string ProductName { get; set; }
        public string ProductBarCode { get; set; }
        public string ProductCode { get; set; }
        [Display(Name = "ProductInboundCode", ResourceType = typeof(Wording))]
        public string ProductInboundCode { get; set; }

        [Display(Name = "Branch", ResourceType = typeof(Wording))]
        public int? BranchId { get; set; }
        [Display(Name = "LoCode", ResourceType = typeof(Wording))]
        public string LoCode { get; set; }
        public Nullable<bool> IsSale { get; set; }
    }
}