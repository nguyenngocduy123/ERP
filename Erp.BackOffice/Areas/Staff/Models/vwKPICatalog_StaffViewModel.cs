using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Staff.Models
{
    public class vwKPICatalog_StaffViewModel
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

        [Display(Name = "StaffId", ResourceType = typeof(Wording))]
        public Nullable<int> StaffId { get; set; }
        [Display(Name = "KPICatalogId", ResourceType = typeof(Wording))]
        public Nullable<int> KPICatalogId { get; set; }
        [Display(Name = "AchievePercent", ResourceType = typeof(Wording))]
        public Nullable<int> AchievePercent { get; set; }
        [Display(Name = "AchieveTarget", ResourceType = typeof(Wording))]
        public Nullable<bool> AchieveTarget { get; set; }
        [Display(Name = "Rate", ResourceType = typeof(Wording))]
        public string Rate { get; set; }
        [Display(Name = "KPICatalogName", ResourceType = typeof(Wording))]
        public string KPICatalogName { get; set; }
        [Display(Name = "StaffName", ResourceType = typeof(Wording))]
        public string StaffName { get; set; }

    }
}