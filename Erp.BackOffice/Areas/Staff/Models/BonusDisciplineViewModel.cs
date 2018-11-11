using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Staff.Entities;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Staff.Models
{
    public class BonusDisciplineViewModel
    {
        public int Id { get; set; }

        [Display(Name = "IsDeleted")]
        public bool? IsDeleted { get; set; }

        [Display(Name = "CreatedUser", ResourceType = typeof(Wording))]
        public int? CreatedUserId { get; set; }

        [Display(Name = "CreatedUser", ResourceType = typeof(Wording))]
        public string CreatedUserName { get; set; }

        [Display(Name = "CreatedDate", ResourceType = typeof(Wording))]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "ModifiedUser", ResourceType = typeof(Wording))]
        public int? ModifiedUserId { get; set; }
        public string ModifiedUserName { get; set; }

        [Display(Name = "ModifiedDate", ResourceType = typeof(Wording))]
        public DateTime? ModifiedDate { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "Category", ResourceType = typeof(Wording))]
        public string Category { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "Formality", ResourceType = typeof(Wording))]
        public string Formality { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "Reason", ResourceType = typeof(Wording))]
        public string Reason { get; set; }
        [Display(Name = "DayDecision", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> DayDecision { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "DayEffective", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> DayEffective { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "PlaceDecisions", ResourceType = typeof(Wording))]
        public Nullable<int> PlaceDecisions { get; set; }
        [Display(Name = "Note", ResourceType = typeof(Wording))]
        public string Note { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "NameStaff", ResourceType = typeof(Wording))]
        public int? StaffId { get; set; }
        [Display(Name = "CodeDecisions", ResourceType = typeof(Wording))]
        public string Code { get; set; }

        //view BonusDiscipline
        [Display(Name = "NameStaff", ResourceType = typeof(Wording))]
        public string Name { get; set; }
        public string CodeName { get; set; }
        [Display(Name = "BranchName", ResourceType = typeof(Wording))]
        public string BranchName { get; set; }
        [Display(Name = "Position", ResourceType = typeof(Wording))]
        public string Position { get; set; }
        [Display(Name = "Department", ResourceType = typeof(Wording))]
        public string Staff_DepartmentId { get; set; }
        [Display(Name = "BranchName", ResourceType = typeof(Wording))]
        public Nullable<int> Sale_BranchId { get; set; }
        [Display(Name = "Department", ResourceType = typeof(Wording))]
        public Nullable<int> BranchDepartmentId { get; set; }

        [Display(Name = "Gender", ResourceType = typeof(Wording))]
        public Nullable<bool> Gender { get; set; }
        [Display(Name = "ProfileImage", ResourceType = typeof(Wording))]
        public string ProfileImage { get; set; }
        [Display(Name = "CodeStaff", ResourceType = typeof(Wording))]
        public string StaffCode { get; set; }
        [Display(Name = "Birthday", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> Birthday { get; set; }
        [Display(Name = "PlaceDecisions", ResourceType = typeof(Wording))]
        public string PlaceDecisionsName { get; set; }
        [Display(Name = "BranchName", ResourceType = typeof(Wording))]
        public Nullable<int> PlaceDecisions_Branch { get; set; }
         [Display(Name = "Money", ResourceType = typeof(Wording))]
        public Nullable<int> Money { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> StaffList { get; set; }
        public IEnumerable<SelectListItem> DepartmentList { get; set; }
        public IEnumerable<SelectListItem> BranchList { get; set; }
        public IEnumerable<SelectListItem> ReasonList { get; set; }
        public IEnumerable<SelectListItem> FormalityList { get; set; }
    }
}