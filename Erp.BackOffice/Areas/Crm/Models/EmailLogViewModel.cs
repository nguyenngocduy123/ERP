using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Crm.Models
{
    public class EmailLogViewModel
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

        [Display(Name = "Customer", ResourceType = typeof(Wording))]
        public Nullable<int> CustomerID { get; set; }

        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        //[StringLength(100, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "StringError", ErrorMessage = null)]


        [Display(Name = "TargetModule", ResourceType = typeof(Wording))]
        public string TargetModule { get; set; }
        
        public Nullable<int> TargetID { get; set; }
        [Display(Name = "SubjectEmail", ResourceType = typeof(Wording))]
        public string SubjectEmail { get; set; }

        [Display(Name = "Body", ResourceType = typeof(Wording))]
        public string Body { get; set; }
        [Display(Name = "Status", ResourceType = typeof(Wording))]
        public string Status { get; set; }
        [Display(Name = "SentDate", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> SentDate { get; set; }

        [Display(Name = "Customer", ResourceType = typeof(Wording))]
        public string Customer { get; set; }
        
        [Display(Name = "Email", ResourceType = typeof(Wording))]
        public string Email { get; set; }


        public SelectList StatusSelectList { get; set; }
        public SelectList CampaignSelectList { get; set; }
        public SelectList CustomerSelectList { get; set; }
    }
}