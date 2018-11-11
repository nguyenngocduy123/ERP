using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace Erp.BackOffice.Staff.Models
{
    public class DotBCBHXHReport
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

        [Display(Name = "BatchNumber", ResourceType = typeof(Wording))]
        public Nullable<int> BatchNumber { get; set; }
        [Display(Name = "Month", ResourceType = typeof(Wording))]
        public Nullable<int> Month { get; set; }
        [Display(Name = "Year", ResourceType = typeof(Wording))]
        public Nullable<int> Year { get; set; }

        [Display(Name = "DotBCBHXHId", ResourceType = typeof(Wording))]
        public Nullable<int> DotBCBHXHId { get; set; }
        [Display(Name = "SocialInsuranceId", ResourceType = typeof(Wording))]
        public Nullable<int> SocialInsuranceId { get; set; }
        [Display(Name = "Type", ResourceType = typeof(Wording))]
        public string Type { get; set; }

        // SocialInsurance
        public Nullable<System.DateTime> SignedDay { get; set; }
        [Display(Name = "StaffName", ResourceType = typeof(Wording))]
        public Nullable<int> StaffId { get; set; }
        [Display(Name = "Code", ResourceType = typeof(Wording))]
        public string Code { get; set; }
        [Display(Name = "StaffName", ResourceType = typeof(Wording))]
        public string StaffName { get; set; }
        [Display(Name = "PositionName", ResourceType = typeof(Wording))]
        public string PositionName { get; set; }
        [Display(Name = "MedicalCode", ResourceType = typeof(Wording))]
        public string MedicalCode { get; set; }
        [Display(Name = "MedicalStartDate", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> MedicalStartDate { get; set; }
        [Display(Name = "MedicalEndDate", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> MedicalEndDate { get; set; }
        [Display(Name = "MedicalIssue", ResourceType = typeof(Wording))]
        public string MedicalIssue { get; set; }
        [Display(Name = "MedicalDefaultValue", ResourceType = typeof(Wording))]
        public string MedicalDefaultValue { get; set; }
        [Display(Name = "SocietyCode", ResourceType = typeof(Wording))]
        public string SocietyCode { get; set; }
        [Display(Name = "SocietyStartDate", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> SocietyStartDate { get; set; }
        [Display(Name = "SocietyEndDate", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> SocietyEndDate { get; set; }
        [Display(Name = "SocietyIssue", ResourceType = typeof(Wording))]
        public string SocietyIssue { get; set; }
        [Display(Name = "SocietyDefaultValue", ResourceType = typeof(Wording))]
        public string SocietyDefaultValue { get; set; }
        [Display(Name = "Note", ResourceType = typeof(Wording))]
        public string Note { get; set; }
        [Display(Name = "PC_CV", ResourceType = typeof(Wording))]
        public double? PC_CV { get; set; }
        [Display(Name = "PC_TNVK", ResourceType = typeof(Wording))]
        public double? PC_TNVK { get; set; }
        [Display(Name = "PC_TNN", ResourceType = typeof(Wording))]
        public double? PC_TNN { get; set; }
        [Display(Name = "PC_Khac", ResourceType = typeof(Wording))]
        public double? PC_Khac { get; set; }
        [Display(Name = "TienLuong", ResourceType = typeof(Wording))]
        public decimal? TienLuong { get; set; }
    }
}