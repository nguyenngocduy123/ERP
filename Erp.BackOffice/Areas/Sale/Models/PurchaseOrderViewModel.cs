using Erp.BackOffice.Account.Models;
using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Sale.Models
{
    public class PurchaseOrderViewModel
    {
        public PurchaseOrderViewModel()
        {
            Discount = 0;
            TaxFee = 0;
            TotalAmount = 0;
        }
        
        public int Id { get; set; }

        [Display(Name = "IsDeleted")]
        public bool? IsDeleted { get; set; }

        [Display(Name = "CreatedUser", ResourceType = typeof(Wording))]
        public int? CreatedUserId { get; set; }
        [Display(Name = "CreatedUser", ResourceType = typeof(Wording))]
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

        [Display(Name = "Code", ResourceType = typeof(Wording))]
        public string Code { get; set; }

        [Display(Name = "TotalAmount", ResourceType = typeof(Wording))]
        public decimal? TotalAmount { get; set; }

        [Display(Name = "TaxFee", ResourceType = typeof(Wording))]
        public double? TaxFee { get; set; }

        [Display(Name = "Discount", ResourceType = typeof(Wording))]
        public double? Discount { get; set; }

        [Display(Name = "DiscountCode", ResourceType = typeof(Wording))]
        public string DiscountCode { get; set; }

        [Display(Name = "Supplier", ResourceType = typeof(Wording))]
        public Nullable<int> SupplierId { get; set; }        

        [Display(Name = "Status", ResourceType = typeof(Wording))]
        public string Status { get; set; }

        [Display(Name = "Note", ResourceType = typeof(Wording))]
        public string Note { get; set; }

        [Display(Name = "Phone", ResourceType = typeof(Wording))]
        public string Phone { get; set; }

        [Display(Name = "PaymentNow", ResourceType = typeof(Wording))]
        public bool IsArchive { get; set; }

        public int? BranchId { get; set; }

        public string PaymentMethod { get; set; }

        public decimal? PaidAmount { get; set; }

        public decimal? RemainingAmount { get; set; }

        public string CancelReason { get; set; }

        public string BarCode { get; set; }

        [Display(Name = "Type", ResourceType = typeof(Wording))]
        public string Type { get; set; }

        [Display(Name = "WarehouseSource", ResourceType = typeof(Wording))]
        public int? WarehouseSourceId { get; set; }

        [Display(Name = "WarehouseDestination", ResourceType = typeof(Wording))]
        public int? WarehouseDestinationId { get; set; }

        [Display(Name = "Supplier", ResourceType = typeof(Wording))]
        public string SupplierName { get; set; }
        [Display(Name = "SupplierCode", ResourceType = typeof(Wording))]
        public string SupplierCode { get; set; }

        [Display(Name = "Source", ResourceType = typeof(Wording))]
        public string WarehouseSourceName { get; set; }

        [Display(Name = "Destination", ResourceType = typeof(Wording))]
        public string WarehouseDestinationName { get; set; }
        [Display(Name = "NextPaymentDate", ResourceType = typeof(Wording))]
        public DateTime? NextPaymentDate { get; set; }

        [Display(Name = "NextPaymentDate", ResourceType = typeof(Wording))]
        public DateTime? NextPaymentDate_Temp { get; set; }

        public string BrandName { get; set; }
        //public string WardName { get; set; }
        //public string DistrictName { get; set; }
        //public string CityName { get; set; }
        public int? ProductInboundId { get; set; }
        public string ProductInboundCode { get; set; }
        public bool AllowEdit { get; set; }
        public List<PurchaseOrderDetailViewModel> DetailList { get; set; }
        public List<PurchaseOrderDetailViewModel> GroupProduct { get; set; }
        public ProductInboundViewModel ProductInboundViewModel { get; set; }
        public List<TransactionLiabilitiesViewModel> ListTransactionLiabilities { get; set; }
        public List<TransactionRelationshipViewModel> ListTransactionRelationship { get; set; }
        public List<ProcessPaymentViewModel> ListProcessPayment { get; set; }
        public PaymentViewModel PaymentViewModel { get; set; }
        public int? QuantityCodeSaleReturns { get; set; }
    }
}