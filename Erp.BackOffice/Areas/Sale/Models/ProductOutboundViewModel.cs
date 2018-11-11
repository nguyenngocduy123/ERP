using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Sale.Models
{
    public class ProductOutboundViewModel
    {
        public int Id { get; set; }

        [Display(Name = "IsDeleted")]
        public bool? IsDeleted { get; set; }

        [Display(Name = "CreatedUser", ResourceType = typeof(Wording))]
        public int? CreatedUserId { get; set; }

        [Display(Name = "StockKeeper", ResourceType = typeof(Wording))]
        public string CreatedUserName { get; set; }

        [Display(Name = "NgayChungTu", ResourceType = typeof(Wording))]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "ModifiedUser", ResourceType = typeof(Wording))]
        public int? ModifiedUserId { get; set; }
        public string ModifiedUserName { get; set; }

        [Display(Name = "ModifiedDate", ResourceType = typeof(Wording))]
        public DateTime? ModifiedDate { get; set; }

        [Display(Name = "MaChungTu", ResourceType = typeof(Wording))]
        public string Code { get; set; }

        [Display(Name = "Note", ResourceType = typeof(Wording))]
        public string Note { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "WarehouseSource", ResourceType = typeof(Wording))]
        public Nullable<int> WarehouseSourceId { get; set; }

        [Display(Name = "WarehouseSource", ResourceType = typeof(Wording))]
        public string WarehouseSourceName { get; set; }

        [Display(Name = "WarehouseKeeperId", ResourceType = typeof(Wording))]
        public Nullable<int> WarehouseKeeperId { get; set; }

        [Display(Name = "Type", ResourceType = typeof(Wording))]
        public string Type { get; set; }

        [Display(Name = "InvoiceCode", ResourceType = typeof(Wording))]
        public Nullable<int> InvoiceId { get; set; }

        [Display(Name = "InvoiceCode", ResourceType = typeof(Wording))]
        public string InvoiceCode { get; set; }


        [Display(Name = "Destination", ResourceType = typeof(Wording))]
        public Nullable<int> WarehouseDestinationId { get; set; }

        [Display(Name = "Destination", ResourceType = typeof(Wording))]
        public string WarehouseDestinationName { get; set; }

        [Display(Name = "TotalAmount", ResourceType = typeof(Wording))]
        public decimal? TotalAmount { get; set; }

        [Display(Name = "PurchaseOrderCode", ResourceType = typeof(Wording))]
        public int? PurchaseOrderId { get; set; }
        [Display(Name = "PurchaseOrderCode", ResourceType = typeof(Wording))]
        public string PurchaseOrderCode { get; set; }

        [Display(Name = "Saler", ResourceType = typeof(Wording))]
        public int? SalerId { get; set; }

        [Display(Name = "ReasonManualOutbound", ResourceType = typeof(Wording))]
        public string ReasonManual { get; set; }
        [Display(Name = "CustomerName", ResourceType = typeof(Wording))]
        public string CustomerName { get; set; }

        public List<ProductOutboundDetailViewModel> DetailList { get; set; }
        public IEnumerable<SelectListItem> SelectList_WarehouseSource { get; set; }
        public List<WarehouseLocationItemViewModel> LocationItemList { get; set; }
        [Display(Name = "RequestInbound", ResourceType = typeof(Wording))]
        public int? RequestInboundId { get; set; }
        [Display(Name = "BranchName", ResourceType = typeof(Wording))]
        public string BranchName { get; set; }
        public bool AllowEdit { get; set; }
        public bool IsArchive { get; set; }
        public bool InvoiceIsDeleted { get; set; }
        [Display(Name = "CreatedStaffName", ResourceType = typeof(Wording))]
        public int? CreatedStaffId { get; set; }
        [Display(Name = "CreatedStaffName", ResourceType = typeof(Wording))]
        public string CreatedStaffName { get; set; }
        [Display(Name = "DrugStore", ResourceType = typeof(Wording))]
        public int? BranchId { get; set; }

        public string DistrictId { get; set; }
        public string CityId { get; set; }
        public string ProvinceName { get; set; }
        public string DistrictName { get; set; }
        public RequestInboundViewModel requestInboundViewModel { get; set; }
    }
}