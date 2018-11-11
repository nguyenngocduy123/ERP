using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Staff.Models
{
    public class HistoryCommissionStaffViewModel
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

        [Display(Name = "StaffName", ResourceType = typeof(Wording))]
        public Nullable<int> StaffId { get; set; }
        [Display(Name = "Month", ResourceType = typeof(Wording))]
        public Nullable<int> Month { get; set; }
        [Display(Name = "Year", ResourceType = typeof(Wording))]
        public Nullable<int> Year { get; set; }
        [Display(Name = "PositionName", ResourceType = typeof(Wording))]
        public string PositionName { get; set; }
        [Display(Name = "CommissionPercent", ResourceType = typeof(Wording))]
        public decimal? CommissionPercent { get; set; }
        [Display(Name = "MinimumRevenue", ResourceType = typeof(Wording))]
        public decimal? MinimumRevenue { get; set; }
        [Display(Name = "RevenueDS", ResourceType = typeof(Wording))]
        public decimal? RevenueDS { get; set; }
        [Display(Name = "AmountCommission", ResourceType = typeof(Wording))]
        public decimal? AmountCommission { get; set; }
        [Display(Name = "StaffName", ResourceType = typeof(Wording))]
        public string StaffName { get; set; }

        public Nullable<int> StaffParentId { get; set; }
    }
}