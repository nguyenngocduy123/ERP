using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Staff.Entities;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Staff.Models
{
    public class vwTimekeepingViewModel
    {
        public int Id { get; set; }

        //[Display(Name = "StaffId", ResourceType = typeof(Wording))]
        public Nullable<int> StaffId { get; set; }
        [Display(Name = "ShiftsName", ResourceType = typeof(Wording))]
        public Nullable<int> ShiftsId { get; set; }
        public Nullable<int> TimekeepingId { get; set; }
        public Nullable<int> WorkSchedulesId { get; set; }
        public Nullable<int> IdDayOff { get; set; }
        public string CodeName { get; set; }
        public string DayOff { get; set; }

        public string DayOffName { get; set; }
        public string DayOffCode { get; set; }
        [Display(Name = "HoursIn", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> HoursIn { get; set; }
        [Display(Name = "HoursOut", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> HoursOut { get; set; }
        [Display(Name = "StartTime", ResourceType = typeof(Wording))]
        public string StartTime { get; set; }
        [Display(Name = "EndTime", ResourceType = typeof(Wording))]
        public string EndTime { get; set; }
        [Display(Name = "ShiftsName", ResourceType = typeof(Wording))]
        public string NameShifts { get; set; }
        [Display(Name = "Code", ResourceType = typeof(Wording))]
        public string CodeShifts { get; set; }
        [Display(Name = "NameStaff", ResourceType = typeof(Wording))]
        public string Name { get; set; }
        //[Display(Name = "CentPay", ResourceType = typeof(Wording))]
        //public Nullable<int> Pay { get; set; }

        [Display(Name = "CodeStaff", ResourceType = typeof(Wording))]
        public string Code { get; set; }
        [Display(Name = "ProfileImage", ResourceType = typeof(Wording))]
        public string ProfileImage { get; set; }
        [Display(Name = "BranchName", ResourceType = typeof(Wording))]
        public Nullable<int> Sale_BranchId { get; set; }
        [Display(Name = "Department", ResourceType = typeof(Wording))]
        public Nullable<int> BranchDepartmentId { get; set; }
        [Display(Name = "DayWork", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> DayWork { get; set; }

        [Display(Name = "Birthday", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> Birthday { get; set; }

        [Display(Name = "Gender", ResourceType = typeof(Wording))]
        public Nullable<bool> Gender { get; set; }
        [Display(Name = "Month", ResourceType = typeof(Wording))]
        public Nullable<int> Month { get; set; }
        [Display(Name = "Year", ResourceType = typeof(Wording))]
        public Nullable<int> Year { get; set; }

        public IEnumerable<vwTimekeepingViewModel> timekeepingList { get; set; }
        public IEnumerable<vwTimekeepingViewModel> StaffList { get; set; }
        public IEnumerable<vwTimekeepingViewModel> DayWorkList { get; set; }
        public string ProfileImagePath { get; set; }

       // [Display(Name = "Month", ResourceType = typeof(Wording))]
        public Nullable<int> TotalMinuteWork { get; set; }
     //   [Display(Name = "Year", ResourceType = typeof(Wording))]
        public Nullable<int> TotalMinuteWorkEarly { get; set; }
     //   [Display(Name = "Month", ResourceType = typeof(Wording))]
        public Nullable<int> TotalMinuteWorkLate { get; set; }
        public Nullable<int> TotalMinuteWorkOvertime { get; set; }
    }
}