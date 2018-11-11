using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Sale.Models
{
    public class LogLoyaltyPointViewModel
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

        //[Display(Name = "CustomerId", ResourceType = typeof(Wording))]
        public Nullable<int> CustomerId { get; set; }
        //[Display(Name = "ProductInvoiceId", ResourceType = typeof(Wording))]
        public Nullable<int> ProductInvoiceId { get; set; }
        [Display(Name = "PlusPoint", ResourceType = typeof(Wording))]
        public Nullable<int> PlusPoint { get; set; }
        [Display(Name = "TotalPoint", ResourceType = typeof(Wording))]
        public Nullable<int> TotalPoint { get; set; }
        [Display(Name = "MemberCardId", ResourceType = typeof(Wording))]
        public Nullable<int> MemberCardId { get; set; }

        public string ProductInvoiceCode { get; set; }
        public string ProductInvoiceDate { get; set; }
        public string MemberCardCode { get; set; }
        public decimal? TotalAmount { get; set; }
    }
}