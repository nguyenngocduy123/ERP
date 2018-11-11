using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Sale.Models
{
    public class RequestInboundViewModel
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

        [Display(Name = "AssignedUserId", ResourceType = typeof(Wording))]
        public int? AssignedUserId { get; set; }
        public string AssignedUserName { get; set; }

        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        //[StringLength(100, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "StringError", ErrorMessage = null)]
        //[Display(Name = "Name", ResourceType = typeof(Wording))]
        //public string Name { get; set; }

        [Display(Name = "Code", ResourceType = typeof(Wording))]
        public string Code { get; set; }
        [Display(Name = "TotalAmount", ResourceType = typeof(Wording))]
        public decimal? TotalAmount { get; set; }
        [Display(Name = "WarehouseSource", ResourceType = typeof(Wording))]
        public Nullable<int> WarehouseSourceId { get; set; }
        [Display(Name = "WarehouseRequired", ResourceType = typeof(Wording))]
        public Nullable<int> WarehouseDestinationId { get; set; }
        [Display(Name = "Status", ResourceType = typeof(Wording))]
        public string Status { get; set; }
        [Display(Name = "Barcode", ResourceType = typeof(Wording))]
        public string BarCode { get; set; }
        [Display(Name = "CancelReason", ResourceType = typeof(Wording))]
        public string CancelReason { get; set; }
        [Display(Name = "Note", ResourceType = typeof(Wording))]
        public string Note { get; set; }
        [Display(Name = "InboundId", ResourceType = typeof(Wording))]
        public Nullable<int> InboundId { get; set; }
        [Display(Name = "OutboundId", ResourceType = typeof(Wording))]
        public Nullable<int> OutboundId { get; set; }
        [Display(Name = "ShippingName", ResourceType = typeof(Wording))]
        public string ShipName { get; set; }
        [Display(Name = "ShipPhone", ResourceType = typeof(Wording))]
        public string ShipPhone { get; set; }
        [Display(Name = "BranchName", ResourceType = typeof(Wording))]
        public Nullable<int> BranchId { get; set; }
        [Display(Name = "StaffId", ResourceType = typeof(Wording))]
        public Nullable<int> StaffId { get; set; }
        public List<RequestInboundDetailViewModel> DetailList { get; set; }
        public List<RequestInboundDetailViewModel> GroupProduct { get; set; }
        [Display(Name = "BranchName", ResourceType = typeof(Wording))]
        public string BranchName { get; set; }
        [Display(Name = "WarehouseSource", ResourceType = typeof(Wording))]
        public string WarehouseSourceName { get; set; }
        [Display(Name = "WarehouseRequired", ResourceType = typeof(Wording))]
        public string WarehouseDestinationName { get; set; }

        public bool? Error { get; set; }
        public int? ErrorQuantity { get; set; }
        public decimal? Liabilities { get; set; }
        public decimal? MaxDebitAmount { get; set; }
        public decimal? DinhMucKho { get; set; }

        public string DistrictId { get; set; }
        public string CityId { get; set; }
        public string ProvinceName { get; set; }
        public string DistrictName { get; set; }
    }
}