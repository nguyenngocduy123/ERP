using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web.Mvc;

namespace Erp.BackOffice.Staff.Models
{
    public class SalaryTableViewModel
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

        [Display(Name = "Name", ResourceType = typeof(Wording))]
        public string Name { get; set; }

        [Display(Name = "Duration", ResourceType = typeof(Wording))]
        public Nullable<int> TargetMonth { get; set; }
        [Display(Name = "TargetYear", ResourceType = typeof(Wording))]
        public Nullable<int> TargetYear { get; set; }
        [Display(Name = "PaymentDate", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> PaymentDate { get; set; }


        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "SalarySettingId", ResourceType = typeof(Wording))]
        public int SalarySettingId { get; set; }
        [Display(Name = "Status", ResourceType = typeof(Wording))]
        public string Status { get; set; }
        public List<SalarySettingDetailViewModel> ListAllColumns { get; set; }
        public DataTable SalaryTableData { get; set; }
        [Display(Name = "SubjectsName", ResourceType = typeof(Wording))]
        public string SubjectsName { get; set; }
        [Display(Name = "BranchId", ResourceType = typeof(Wording))]
        public Nullable<int> BranchId { get; set; }
        [Display(Name = "BranchDepartment", ResourceType = typeof(Wording))]
        public Nullable<int> BranchDepartmentId { get; set; }
        public byte[] ListSalarySettingDetail { get; set; }
        [Display(Name = "BranchName", ResourceType = typeof(Wording))]
        public string BranchName { get; set; }
        public bool IsSend { get; set; }
        public bool Submitted { get; set; }
        public string SalaryApprovalType { get; set; }
        public bool HiddenForMonth { get; set; }
        public List<Erp.Domain.Staff.Entities.SalaryTableDetailReport> ListalaryTableDetailReport { get; set; }

        public DataTable SalaryTableLink { get; set; }
    }
}