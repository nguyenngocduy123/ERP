using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Sale.Models
{
    public class ProductInboundDetailViewModel
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

        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        //[StringLength(100, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "StringError", ErrorMessage = null)]

        [Display(Name = "ProductInboundId", ResourceType = typeof(Wording))]
        public Nullable<int> ProductInboundId { get; set; }
        [Display(Name = "ProductName", ResourceType = typeof(Wording))]
        public Nullable<int> ProductId { get; set; }
        [Display(Name = "ProductName", ResourceType = typeof(Wording))]
        public string ProductName { get; set; }
        public string ProductCode { get; set; }

        [Display(Name = "Quantity", ResourceType = typeof(Wording))]
        public Nullable<int> Quantity { get; set; }
        [Display(Name = "Price", ResourceType = typeof(Wording))]
        public decimal? Price { get; set; }
        [Display(Name = "Unit", ResourceType = typeof(Wording))]
        public string Unit { get; set; }
        public string ProductBarCode { get; set; }
        public string ProductType { get; set; }
        public int OrderNo { get; set; }
        [Display(Name = "LoCode", ResourceType = typeof(Wording))]
        public string LoCode { get; set; }
        [Display(Name = "ExpiryDateItem", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> ExpiryDate { get; set; }

        public List<WarehouseLocationItemViewModel> ListWarehouseLocationItemViewModel { get; set; }

        public int QuantityUsed { get; set; }
        //nhập kho từ phiếu xuất.
        [Display(Name = "Quantity", ResourceType = typeof(Wording))]
        public int? QuantityTest { get; set; }
        public int? ProductDamagedId { get; set; }
        public string Status { get; set; }
        public int? NumberAmount { get; set; }
        public string Reason { get; set; }
    }
}