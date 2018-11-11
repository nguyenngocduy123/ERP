using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Staff.Entities;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Staff.Models
{
    public class TransferWorkViewModel
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
        [Display(Name = "StaffName", ResourceType = typeof(Wording))]
        public Nullable<int> StaffId { get; set; }
        [Display(Name = "BranchDepartmentOldId", ResourceType = typeof(Wording))]
        public Nullable<int> BranchDepartmentOldId { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "BranchDepartmentNewId", ResourceType = typeof(Wording))]
        public Nullable<int> BranchDepartmentNewId { get; set; }
        [Display(Name = "PositionOld", ResourceType = typeof(Wording))]
        public string PositionOld { get; set; }
        [Display(Name = "PositionOld", ResourceType = typeof(Wording))]
        public string PositionOldName { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "PositionNew", ResourceType = typeof(Wording))]
        public string PositionNew { get; set; }
        [Display(Name = "PositionNew", ResourceType = typeof(Wording))]
        public string PositionNewName { get; set; }
        [Display(Name = "DayDecision", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> DayDecision { get; set; }
        [Display(Name = "User", ResourceType = typeof(Wording))]
        public Nullable<int> UserId { get; set; }
        [Display(Name = "Status", ResourceType = typeof(Wording))]
        public string Status { get; set; }
        [Display(Name = "Reason", ResourceType = typeof(Wording))]
        public string Reason { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "DayEffective", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> DayEffective { get; set; }
        [Display(Name = "Code", ResourceType = typeof(Wording))]
        public string Code { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "BranchNewId", ResourceType = typeof(Wording))]
        public Nullable<int> BranchIdNew { get; set; }

        [Display(Name = "BranchDepartmentNewId", ResourceType = typeof(Wording))]
        public string Staff_DepartmentNew { get; set; }
        [Display(Name = "BranchNewId", ResourceType = typeof(Wording))]
        public string BranchNameNew { get; set; }
        [Display(Name = "BranchDepartmentOldId", ResourceType = typeof(Wording))]
        public string Staff_DepartmentOld { get; set; }
        [Display(Name = "BranchOldId", ResourceType = typeof(Wording))]
        public string BranchNameOld { get; set; }
        [Display(Name = "CodeStaff", ResourceType = typeof(Wording))]
        public string CodeStaff { get; set; }
        [Display(Name = "NameStaff", ResourceType = typeof(Wording))]
        public string NameStaff { get; set; }
        [Display(Name = "User", ResourceType = typeof(Wording))]
        public string NameUser { get; set; }

        [Display(Name = "Birthday", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> Birthday { get; set; }

        [Display(Name = "Gender", ResourceType = typeof(Wording))]
        public Nullable<bool> Gender { get; set; }
        public string CodeName { get; set; }
        public Nullable<int> BranchIdOld { get; set; }
        public string ProfileImage { get; set; }
        public string CodeStaffOld { get; set; }
        public string CodeStaffNew { get; set; }
        public IEnumerable<SelectListItem> StaffList { get; set; }
        public IEnumerable<SelectListItem> DepartmentOldList { get; set; }
        public IEnumerable<SelectListItem> DepartmentNewList { get; set; }
        public IEnumerable<SelectListItem> BranchList { get; set; }
        public IEnumerable<SelectListItem> PositionList { get; set; }


    }
}