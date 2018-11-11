using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Staff.Models
{
    public class StaffAllowanceViewModel
    {
        public int Id { get; set; }

       // [Display(Name = "IsDeleted")]
        public bool? IsDeleted { get; set; }

        //[Display(Name = "CreatedUser", ResourceType = typeof(Wording))]
        public int? CreatedUserId { get; set; }
        public string CreatedUserName { get; set; }

        //[Display(Name = "CreatedDate", ResourceType = typeof(Wording))]
        public DateTime? CreatedDate { get; set; }
//
       // [Display(Name = "ModifiedUser", ResourceType = typeof(Wording))]
        public int? ModifiedUserId { get; set; }
        public string ModifiedUserName { get; set; }

       // [Display(Name = "ModifiedDate", ResourceType = typeof(Wording))]
        public DateTime? ModifiedDate { get; set; }

       // [Display(Name = "AssignedUserId", ResourceType = typeof(Wording))]
        public int? AssignedUserId { get; set; }
        public string AssignedUserName { get; set; }

        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        //[StringLength(100, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "StringError", ErrorMessage = null)]
        //[Display(Name = "Name", ResourceType = typeof(Wording))]
        public string Name { get; set; }

        //[Display(Name = "StaffId", ResourceType = typeof(Wording))]
        public Nullable<int> StaffId { get; set; }

        [Display(Name = "Money", ResourceType = typeof(Wording))]
        public int? PayAllowance { get; set; }
        //[Display(Name = "TargetMonth", ResourceType = typeof(Wording))]
        public Nullable<int> TargetMonth { get; set; }
        //[Display(Name = "TargetYear", ResourceType = typeof(Wording))]
        public Nullable<int> TargetYear { get; set; }

    }
}