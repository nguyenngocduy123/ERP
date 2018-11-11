using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Crm.Models
{
    public class ProcessViewModel
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

        [Display(Name = "Category", ResourceType = typeof(Wording))]
        public string Category { get; set; }
        [Display(Name = "Entity", ResourceType = typeof(Wording))]
        public string DataSource { get; set; }
        //[Display(Name = "RecordIsCreated", ResourceType = typeof(Wording))]
        //public Nullable<bool> RecordIsCreated { get; set; }
        //[Display(Name = "RecordIsAssigned", ResourceType = typeof(Wording))]
        //public Nullable<bool> RecordIsAssigned { get; set; }
        //[Display(Name = "RecordFieldsChanges", ResourceType = typeof(Wording))]
        //public Nullable<bool> RecordFieldsChanges { get; set; }
        //[Display(Name = "RecordIsDeleted", ResourceType = typeof(Wording))]
        //public Nullable<bool> RecordIsDeleted { get; set; }
        [Display(Name = "IsActive", ResourceType = typeof(Wording))]
        public Nullable<bool> IsActive { get; set; }
        [Display(Name = "ActivateAs", ResourceType = typeof(Wording))]
        public string ActivateAs { get; set; }
        public SelectList CategorySelectList { get; set; }
        public SelectList EntitySelectList { get; set; }
        public List<ProcessAppliedViewModel> DetailList { get; set; }
         [Display(Name = "QueryRecivedUser", ResourceType = typeof(Wording))]
        public string QueryRecivedUser { get; set; }

    }
}