using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Staff.Entities;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Staff.Models
{
    public class SalaryAdvanceViewModel
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

        [Display(Name = "CodeSalaryAdvance", ResourceType = typeof(Wording))]
        public string CodeAdvance { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "SalaryAdvance", ResourceType = typeof(Wording))]
        public Nullable<int> Pay { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "Staff", ResourceType = typeof(Wording))]
        public Nullable<int> StaffId { get; set; }
        [Display(Name = "Status", ResourceType = typeof(Wording))]
        public string Status { get; set; }
        [Display(Name = "NameStaff", ResourceType = typeof(Wording))]
        public string Name { get; set; }
        [Display(Name = "CodeStaff", ResourceType = typeof(Wording))]
        public string CodeStaff { get; set; }
        [Display(Name = "ProfileImage", ResourceType = typeof(Wording))]
        public string ProfileImage { get; set; }

        [Display(Name = "Department", ResourceType = typeof(Wording))]
        public Nullable<int> BranchDepartmentId { get; set; }

        [Display(Name = "BranchName", ResourceType = typeof(Wording))]
        public Nullable<int> Sale_BranchId { get; set; }

        [Display(Name = "Department", ResourceType = typeof(Wording))]
        public string Staff_DepartmentId { get; set; }

        [Display(Name = "BranchName", ResourceType = typeof(Wording))]
        public string BranchName { get; set; }
        [Display(Name = "Note", ResourceType = typeof(Wording))]
        public string Note { get; set; }


        [Display(Name = "DayAdvance", ResourceType = typeof(Wording))]
        public DateTime? DayAdvance { get; set; }
    }
}