using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Crm.Models
{
    [Serializable]
    public class TaskTemplateViewModel
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
        [Display(Name = "Subject", ResourceType = typeof(Wording))]
        public string Subject { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "Status", ResourceType = typeof(Wording))]
        public string Status { get; set; }

        [Display(Name = "StartDate", ResourceType = typeof(Wording))]
        public string StartDate { get; set; }

        [Display(Name = "DueDate", ResourceType = typeof(Wording))]
        public string DueDate { get; set; }
        [Display(Name = "ParentType", ResourceType = typeof(Wording))]
        public string ParentType { get; set; }
        [Display(Name = "ParentId", ResourceType = typeof(Wording))]
        public Nullable<int> ParentId { get; set; }
        [Display(Name = "ContactId", ResourceType = typeof(Wording))]
        public Nullable<int> ContactId { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "Priority", ResourceType = typeof(Wording))]
        public string Priority { get; set; }
        [Display(Name = "Description", ResourceType = typeof(Wording))]
        public string Description { get; set; }
        public string Type { get; set; }
        public string ProfileImage { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
       [Display(Name = "Note", ResourceType = typeof(Wording))]
        public string Note { get; set; }
    }
}