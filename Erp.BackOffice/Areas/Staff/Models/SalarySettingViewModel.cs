using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web.Mvc;

namespace Erp.BackOffice.Staff.Models
{
    public class SalarySettingViewModel
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

        [Display(Name = "Note", ResourceType = typeof(Wording))]
        public string Note { get; set; }

        public bool IsTemplate { get; set; }

        //[Display(Name = "IsSend", ResourceType = typeof(Wording))]
        public bool IsSend { get; set; }
        [Display(Name = "SalaryApprovalType", ResourceType = typeof(Wording))]
        public string SalaryApprovalType { get; set; }
        [Display(Name = "OrderNo", ResourceType = typeof(Wording))]
        public int OrderNo { get; set; }

        //[Display(Name = "HiddenForMonth", ResourceType = typeof(Wording))]
        public bool HiddenForMonth { get; set; }
    }
}