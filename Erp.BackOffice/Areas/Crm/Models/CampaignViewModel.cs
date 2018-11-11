using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Crm.Models
{
    public class CampaignViewModel
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

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [StringLength(100, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "StringError", ErrorMessage = null)]
        [Display(Name = "Name", ResourceType = typeof(Wording))]
        public string Name { get; set; }

        [Display(Name = "Type", ResourceType = typeof(Wording))]
        public string Type { get; set; }
        [Display(Name = "Status", ResourceType = typeof(Wording))]
        public string Status { get; set; }
        [Display(Name = "StartDate", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> StartDate { get; set; }
        [Display(Name = "EndDate", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> EndDate { get; set; }
        [Display(Name = "BudgetedCost", ResourceType = typeof(Wording))]
        public Nullable<int> BudgetedCost { get; set; }
        [Display(Name = "ExpectedRevenue", ResourceType = typeof(Wording))]
        public Nullable<int> ExpectedRevenue { get; set; }
        [Display(Name = "ActualCost", ResourceType = typeof(Wording))]
        public Nullable<int> ActualCost { get; set; }
        [Display(Name = "ExpectedResponse", ResourceType = typeof(Wording))]
        public Nullable<int> ExpectedResponse { get; set; }
        [Display(Name = "Commision", ResourceType = typeof(Wording))]
        public string Commision { get; set; }

        public SelectList TypeSelectList { get; set; }
        public SelectList StatusSelectList { get; set; }
    }
}