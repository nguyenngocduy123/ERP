using Erp.BackOffice.App_GlobalResources;
using Erp.BackOffice.Sale.Models;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Account.Models
{
    public class ResolveLiabilitiesViewModel
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

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "Amount", ResourceType = typeof(Wording))]
        public decimal? Amount { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "PaymentMethod", ResourceType = typeof(Wording))]
        public string PaymentMethod { get; set; }
        [Display(Name = "BankAccountNo", ResourceType = typeof(Wording))]
        public string BankAccountNo { get; set; }
        [Display(Name = "BankAccountName", ResourceType = typeof(Wording))]
        public string BankAccountName { get; set; }
        [Display(Name = "BankName", ResourceType = typeof(Wording))]
        public string BankName { get; set; }        
        public int? ProcessPaymentId { get; set; }

        [Display(Name = "NextPaymentDate", ResourceType = typeof(Wording))]
        public DateTime? NextPaymentDate { get; set; }
        public string TargetModule { get; set; }
        public string TargetCode { get; set; }
        public string TargetName { get; set; }
        public List<ProductInvoiceViewModel> ListProductInvoice { get; set; }
        public List<PurchaseOrderViewModel> ListPurchaseOrder { get; set; }
        public string TransactionName { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        public string MaChungTuGoc { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        public string LoaiChungTuGoc { get; set; }

        public string Note { get; set; }
    }
}