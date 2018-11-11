using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Staff.Entities;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Staff.Models
{
    public class BranchDepartmentViewModel
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

        [Display(Name = "BranchName", ResourceType = typeof(Wording))]
        public int? Sale_BranchId { get; set; }

        [Display(Name = "DrugStore", ResourceType = typeof(Wording))]
        public string Staff_DepartmentId { get; set; }

        [Display(Name = "BranchName", ResourceType = typeof(Wording))]
        public string BranchName { get; set; }

        [Display(Name = "BranchCode", ResourceType = typeof(Wording))]
        public string BranchCode { get; set; }

        [Display(Name = "Address", ResourceType = typeof(Wording))]
        public string Address { get; set; }

        [Display(Name = "Phone", ResourceType = typeof(Wording))]
        public string Phone { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Wording))]
        public string Email { get; set; }

        [Display(Name = "CodeDrugStore", ResourceType = typeof(Wording))]
        public string CodeDepartment { get; set; }
         [Display(Name = "MaxDebitAmount", ResourceType = typeof(Wording))]
        public decimal? MaxDebitAmount { get; set; }
        public int StaffTotal { get; set; }
        public int StaffTotalMale { get; set; }
        public int StaffTotalFemale { get; set; }
        public IEnumerable<SelectListItem> DepartmentList { get; set; }
        public IEnumerable<StaffsViewModel> StaffList { get; set; }
    }
}