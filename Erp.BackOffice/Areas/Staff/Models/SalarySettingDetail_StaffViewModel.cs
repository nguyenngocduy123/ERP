using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Staff.Entities;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Staff.Models
{
    public class SalarySettingDetail_StaffViewModel
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


        [Display(Name = "StaffId", ResourceType = typeof(Wording))]
        public Nullable<int> StaffId { get; set; }
        [Display(Name = "SalarySettingId", ResourceType = typeof(Wording))]
        public Nullable<int> SalarySettingId { get; set; }
        [Display(Name = "SalarySettingDetailId", ResourceType = typeof(Wording))]
        public Nullable<int> SalarySettingDetailId { get; set; }
        [Display(Name = "DefaultValue", ResourceType = typeof(Wording))]
        public double? DefaultValue { get; set; }
        public List<SalarySettingDetailViewModel> ListAllColumns { get; set; }
        public List<SelectListItem> SelectList_Group { get; set; }
    }
}