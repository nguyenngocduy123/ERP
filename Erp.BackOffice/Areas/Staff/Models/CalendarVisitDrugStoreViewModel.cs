using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Staff.Models
{
    public class CalendarVisitDrugStoreViewModel
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

        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [StringLength(350, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "StringError", ErrorMessage = null)]
        [Display(Name = "Note", ResourceType = typeof(Wording))]
        public string Note { get; set; }

        [Display(Name = "DrugStore", ResourceType = typeof(Wording))]
        public Nullable<int> DrugStoreId { get; set; }
        [Display(Name = "StartDate", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> StartDate { get; set; }
        [Display(Name = "EndDate", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> EndDate { get; set; }
        [Display(Name = "Status", ResourceType = typeof(Wording))]
        public string Status { get; set; }
        [Display(Name = "StaffName", ResourceType = typeof(Wording))]
        public Nullable<int> StaffId { get; set; }

        public Nullable<int> Month { get; set; }
        public Nullable<int> Year { get; set; }
        public string BranchName { get; set; }
        public string BranchCode { get; set; }
        [Display(Name = "DrugStore", ResourceType = typeof(Wording))]
        public string DrugStoreName { get; set; }
        [Display(Name = "DrugStoreCode", ResourceType = typeof(Wording))]
        public string DrugStoreCode { get; set; }
        [Display(Name = "StaffName", ResourceType = typeof(Wording))]
        public string StaffName { get; set; }
        [Display(Name = "StaffCode", ResourceType = typeof(Wording))]
        public string StaffCode { get; set; }

        public string DistrictId { get; set; }
        public string CityId { get; set; }
        public string ProvinceName { get; set; }
        public string DistrictName { get; set; }
        public SearchCalendarVisitDrugStore search { get; set; }
    }

    public class SearchCalendarVisitDrugStore
    {
        public Nullable<int> branchId { get; set; }
        public Nullable<int> Month { get; set; }
        public Nullable<int> Year { get; set; }
        public string DistrictId { get; set; }
        public string CityId { get; set; }
    }
}