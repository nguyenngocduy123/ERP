using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Account.Models
{
    public class TransactionLiabilitiesViewModel
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
        [Display(Name = "TransactionCode", ResourceType = typeof(Wording))]
        public string TransactionCode { get; set; }

        [Display(Name = "TransactionModule", ResourceType = typeof(Wording))]
        public string TransactionModule { get; set; }

        [Display(Name = "TransactionType", ResourceType = typeof(Wording))]
        public string TransactionName { get; set; }

        [Display(Name = "TargetModule", ResourceType = typeof(Wording))]
        public string TargetModule { get; set; }

        [Display(Name = "TargetCode", ResourceType = typeof(Wording))]
        public string TargetCode { get; set; }

        [Display(Name = "TargetName", ResourceType = typeof(Wording))]
        public string TargetName { get; set; }
        [Display(Name = "Debit", ResourceType = typeof(Wording))]
        public decimal Debit { get; set; }
        [Display(Name = "Credit", ResourceType = typeof(Wording))]
        public decimal Credit { get; set; }

        [Display(Name = "NextPaymentDate", ResourceType = typeof(Wording))]
        public DateTime? NextPaymentDate { get; set; }
        public string Phone { get; set; }
        public string Note { get; set; }
    }
}