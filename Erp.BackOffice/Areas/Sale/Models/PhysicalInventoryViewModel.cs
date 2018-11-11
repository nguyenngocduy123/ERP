using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Sale.Models
{
    public class PhysicalInventoryViewModel
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
        [Display(Name = "Code", ResourceType = typeof(Wording))]
        public string Code { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "Warehouse", ResourceType = typeof(Wording))]
        public Nullable<int> WarehouseId { get; set; }

        [Display(Name = "Warehouse", ResourceType = typeof(Wording))]
        public string WarehouseName { get; set; }
        [Display(Name = "Manufacturer", ResourceType = typeof(Wording))]
        public string Manufacturer { get; set; }
        
        [Display(Name = "Note", ResourceType = typeof(Wording))]
        public string Note { get; set; }
        [Display(Name = "Status", ResourceType = typeof(Wording))]
        public string Status { get; set; }

        [Display(Name = "TotalBreak", ResourceType = typeof(Wording))]
        public int? TotalBreak { get; set; }

        [Display(Name = "TotalLost", ResourceType = typeof(Wording))]
        public int? TotalLost { get; set; }

        [Display(Name = "TotalProductCheck", ResourceType = typeof(Wording))]
        public int? TotalProductCheck { get; set; }

        [Display(Name = "BranchId", ResourceType = typeof(Wording))]
        public int? BranchId { get; set; }

        public bool? IsExchange { get; set; }

        [Display(Name = "ProductInboundCode", ResourceType = typeof(Wording))]
        public string ProductInboundCode { get; set; }

        [Display(Name = "ProductOutboundCode", ResourceType = typeof(Wording))]
        public string ProductOutboundCode { get; set; }

        public List<PhysicalInventoryDetailViewModel> DetailList { get; set; }
    }
}