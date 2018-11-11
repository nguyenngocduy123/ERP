using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Staff.Models
{
    public class BranchViewModel
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
        [Display(Name = "BranchName", ResourceType = typeof(Wording))]
        public string Name { get; set; }

        [Display(Name = "CodeBranch", ResourceType = typeof(Wording))]
        public string Code { get; set; }
        [Display(Name = "Address", ResourceType = typeof(Wording))]
        public string Address { get; set; }
        [Display(Name = "Ward", ResourceType = typeof(Wording))]
        public string WardId { get; set; }
        [Display(Name = "District", ResourceType = typeof(Wording))]
        public string DistrictId { get; set; }
        [Display(Name = "Province", ResourceType = typeof(Wording))]
        public string CityId { get; set; }
        [Display(Name = "Phone", ResourceType = typeof(Wording))]
        public string Phone { get; set; }
        [Display(Name = "Email", ResourceType = typeof(Wording))]
        public string Email { get; set; }
        // [Display(Name = "Type", ResourceType = typeof(Wording))]
        public string Type { get; set; }

        [Display(Name = "Province", ResourceType = typeof(Wording))]
        public string ProvinceName { get; set; }
        [Display(Name = "District", ResourceType = typeof(Wording))]
        public string DistrictName { get; set; }
        [Display(Name = "Ward", ResourceType = typeof(Wording))]
        public string WardName { get; set; }
        [Display(Name = "MaxDebitAmount", ResourceType = typeof(Wording))]
        public decimal? MaxDebitAmount { get; set; }
        public int? ParentId { get; set; }
        [Display(Name = "CooperationDay", ResourceType = typeof(Wording))]
        public DateTime? CooperationDay { get; set; }
        public int TotalDepartment { get; set; }
        public int TotalStaff { get; set; }
        public IEnumerable<SelectListItem> DistrictList { get; set; }
        public IEnumerable<SelectListItem> WardList { get; set; }
        public IEnumerable<SelectListItem> ProvinceList { get; set; }
        public List<StaffsViewModel> StaffList { get; set; }
    }
}