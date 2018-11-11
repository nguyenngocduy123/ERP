using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Account.Models
{
    public class PaymentViewModel
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
        [StringLength(100, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "StringError", ErrorMessage = null)]
        [Display(Name = "Reason", ResourceType = typeof(Wording))]
        public string Name { get; set; }

        [Display(Name = "ShortName", ResourceType = typeof(Wording))]
        public string ShortName { get; set; }
        [Display(Name = "Code", ResourceType = typeof(Wording))]
        public string Code { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "Amount", ResourceType = typeof(Wording))]
        public decimal? Amount { get; set; }
        [Display(Name = "PaymentMethod", ResourceType = typeof(Wording))]
        public string PaymentMethod { get; set; }
        [Display(Name = "BankAccountNo", ResourceType = typeof(Wording))]
        public string BankAccountNo { get; set; }
        [Display(Name = "BankAccountName", ResourceType = typeof(Wording))]
        public string BankAccountName { get; set; }
        [Display(Name = "BankName", ResourceType = typeof(Wording))]
        public string BankName { get; set; }

        [Display(Name = "Receiver", ResourceType = typeof(Wording))]
        public string Receiver { get; set; }
        public string TransactionCode { get; set; }
        public string TransactionType { get; set; }
        public string TargetCode { get; set; }
        public string TargetType { get; set; }
        [Display(Name = "Saler", ResourceType = typeof(Wording))]
        public Nullable<int> SalerId { get; set; }
        [Display(Name = "Saler", ResourceType = typeof(Wording))]
        public string SalerName { get; set; }
        [Display(Name = "Interpretations", ResourceType = typeof(Wording))]
        public string Note { get; set; }
        [Display(Name = "VoucherDate", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> VoucherDate { get; set; }
        [Display(Name = "Address", ResourceType = typeof(Wording))]
        public string Address { get; set; }
        [Display(Name = "SubjectName", ResourceType = typeof(Wording))]
        public Nullable<int> TargetId { get; set; }
        public string TargetName { get; set; }
        [Display(Name = "Receiver", ResourceType = typeof(Wording))]
        public string ReceiverUserName { get; set; }
        public string MaChungTuGoc { get; set; }
        public string LoaiChungTuGoc { get; set; }
        [Display(Name = "CancelReason", ResourceType = typeof(Wording))]
        public string CancelReason { get; set; }

        [Display(Name = "BranchName", ResourceType = typeof(Wording))]
        public Nullable<int> BranchId { get; set; }

        public bool IsArchive { get; set; }
    }
}