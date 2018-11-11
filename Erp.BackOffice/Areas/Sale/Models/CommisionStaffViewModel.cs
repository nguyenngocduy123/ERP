using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Sale.Models
{
    public class CommisionStaffViewModel
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

        [Display(Name = "StaffId", ResourceType = typeof(Wording))]
        public Nullable<int> StaffId { get; set; }

        [Display(Name = "BranchId", ResourceType = typeof(Wording))]
        public Nullable<int> BranchId { get; set; }

        [Display(Name = "ProductInvoiceId", ResourceType = typeof(Wording))]
        public Nullable<int> InvoiceId { get; set; }

        //[Display(Name = "CommisionId", ResourceType = typeof(Wording))]
        public Nullable<int> InvoiceDetailId { get; set; }

        [Display(Name = "PercentOfCommision", ResourceType = typeof(Wording))]
        public Nullable<int> PercentOfCommision { get; set; }

        [Display(Name = "AmountOfCommision", ResourceType = typeof(Wording))]
        public decimal? AmountOfCommision { get; set; }

        [Display(Name = "Note", ResourceType = typeof(Wording))]
        public string Note { get; set; }

        //[Display(Name = "CommisionName", ResourceType = typeof(Wording))]
        public string InvoiceType { get; set; }
        public bool? IsResolved { get; set; }
        public string StaffProfileImage { get; set; }

        [Display(Name = "ProductInvoiceCode", ResourceType = typeof(Wording))]
        public string ProductInvoiceCode { get; set; }

        [Display(Name = "StaffCode", ResourceType = typeof(Wording))]
        public string StaffCode { get; set; }
        [Display(Name = "NameStaff", ResourceType = typeof(Wording))]
        public string StaffName { get; set; }
        [Display(Name = "BranchName", ResourceType = typeof(Wording))]
        public string BranchName { get; set; }
        public string ProductCode { get; set; }
        public string ProductImage { get; set; }
        public string ProductName { get; set; }
        public Nullable<int> month { get; set; }
        public Nullable<int> year { get; set; }
    }
}