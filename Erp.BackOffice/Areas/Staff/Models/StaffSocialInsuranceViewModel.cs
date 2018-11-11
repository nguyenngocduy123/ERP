using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Staff.Models
{
    public class StaffSocialInsuranceViewModel
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
        public Nullable<System.DateTime> SignedDay { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "StaffName", ResourceType = typeof(Wording))]
        public Nullable<int> StaffId { get; set; }
        [Display(Name = "MedicalCode", ResourceType = typeof(Wording))]
        public string MedicalCode { get; set; }
        [Display(Name = "MedicalStartDate", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> MedicalStartDate { get; set; }
        [Display(Name = "MedicalEndDate", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> MedicalEndDate { get; set; }
        [Display(Name = "MedicalIssue", ResourceType = typeof(Wording))]
        public string MedicalIssue { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
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

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "SocietyDefaultValue", ResourceType = typeof(Wording))]
        public string SocietyDefaultValue { get; set; }
        [Display(Name = "Note", ResourceType = typeof(Wording))]
        public string Note { get; set; }

        [Display(Name = "StaffName", ResourceType = typeof(Wording))]
        public string StaffName { get; set; }

        [Display(Name = "Birthday", ResourceType = typeof(Wording))]
        public DateTime? Birthday { get; set; }

        public string IdCardNumber { get; set; }

        public DateTime? IdCardDate { get; set; }

        public string IdCardIssued { get; set; }

        public string Ethnic { get; set; }

        public string Email { get; set; }

        public string Literacy { get; set; }

        public string ProvinceName { get; set; }

        [Display(Name = "BranchName", ResourceType = typeof(Wording))]
        public string BranchName { get; set; }

        public string PositionName { get; set; }

        public string CodeName { get; set; }
        [Display(Name = "CodeStaff", ResourceType = typeof(Wording))]
        public string StaffCode { get; set; }
        [Display(Name = "SocietyIssue", ResourceType = typeof(Wording))]
        public int? ProvinceSociety { get; set; }
        [Display(Name = "MedicalIssue", ResourceType = typeof(Wording))]
        public int? ProvinceMedical { get; set; }
        [Display(Name = "Status", ResourceType = typeof(Wording))]
        public string Status { get; set; }
        public string ProfileImage { get; set; }

        [Display(Name = "PC_CV", ResourceType = typeof(Wording))]
        public string PC_CV { get; set; }
        [Display(Name = "PC_TNVK", ResourceType = typeof(Wording))]
        public string PC_TNVK { get; set; }
        [Display(Name = "PC_TNN", ResourceType = typeof(Wording))]
        public string PC_TNN { get; set; }
        [Display(Name = "PC_Khac", ResourceType = typeof(Wording))]
        public string PC_Khac { get; set; }
        [Display(Name = "TienLuong", ResourceType = typeof(Wording))]
        public string TienLuong { get; set; }

        public string ProfileImagePath { get; set; }

        public int? DotBCBHXHId { get; set; }

        

    }
}