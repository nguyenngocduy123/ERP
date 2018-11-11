using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Staff.Entities;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Erp.BackOffice.Sale.Models;
namespace Erp.BackOffice.Staff.Models
{
    public class WorkSchedulesViewModel
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

        [Display(Name = "NameStaff", ResourceType = typeof(Wording))]
        public string Name { get; set; }

        [Display(Name = "StaffName", ResourceType = typeof(Wording))]
        public Nullable<int> StaffId { get; set; }
        [Display(Name = "DayWork", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> Day { get; set; }
        [Display(Name = "ShiftsName", ResourceType = typeof(Wording))]
        public Nullable<int> ShiftsId { get; set; }

        [Display(Name = "CodeStaff", ResourceType = typeof(Wording))]
        public string Code { get; set; }
        [Display(Name = "ShiftsName", ResourceType = typeof(Wording))]
        public string NameShifts { get; set; }
        [Display(Name = "ShiftsCode", ResourceType = typeof(Wording))]
        public string CodeShifts { get; set; }
        [Display(Name = "StartTime", ResourceType = typeof(Wording))]
        public string StartTime { get; set; }
        [Display(Name = "EndTime", ResourceType = typeof(Wording))]
        public string EndTime { get; set; }
        [Display(Name = "StartTimeOut", ResourceType = typeof(Wording))]
        public string StartTimeOut { get; set; }
        [Display(Name = "EndTimeIn", ResourceType = typeof(Wording))]
        public string EndTimeIn { get; set; }
        [Display(Name = "UserEnrollNumber", ResourceType = typeof(Wording))]
        public Nullable<int> UserEnrollNumber { get; set; }
        [Display(Name = "HoursIn", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> HoursIn { get; set; }
        [Display(Name = "HoursOut", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> HoursOut { get; set; }
        [Display(Name = "Symbol", ResourceType = typeof(Wording))]
        public Nullable<int> Symbol { get; set; }
        [Display(Name = "Total_minute_work_late", ResourceType = typeof(Wording))]
        public Nullable<int> Total_minute_work_late { get; set; }
        [Display(Name = "Total_minute_work_early", ResourceType = typeof(Wording))]
        public Nullable<int> Total_minute_work_early { get; set; }
        [Display(Name = "Total_minute_work_overtime", ResourceType = typeof(Wording))]
        public Nullable<int> Total_minute_work_overtime { get; set; }
        [Display(Name = "Total_minute_work", ResourceType = typeof(Wording))]
        public Nullable<int> Total_minute_work { get; set; }
        [Display(Name = "Department", ResourceType = typeof(Wording))]
        public Nullable<int> BranchDepartmentId { get; set; }
        [Display(Name = "BranchName", ResourceType = typeof(Wording))]
        public Nullable<int> Sale_BranchId { get; set; }
        [Display(Name = "Timekeeping", ResourceType = typeof(Wording))]
        public Nullable<bool> Timekeeping { get; set; }
        [Display(Name = "ApplicationForLeave", ResourceType = typeof(Wording))]
        public Nullable<bool> DayOff { get; set; }
        [Display(Name = "SymbolName", ResourceType = typeof(Wording))]
        public string DayOffName { get; set; }
        [Display(Name = "Symbol", ResourceType = typeof(Wording))]
        public string DayOffCode { get; set; }
        [Display(Name = "Color", ResourceType = typeof(Wording))]
        public string Color { get; set; }
        [Display(Name = "Month", ResourceType = typeof(Wording))]
        public Nullable<int> Month { get; set; }
        [Display(Name = "Year", ResourceType = typeof(Wording))]
        public Nullable<int> Year { get; set; }
        public Nullable<int> TimekeepingListId { get; set; }
        public Nullable<bool> Absent { get; set; }
        public Nullable<bool> NightShifts { get; set; }
        public IEnumerable<WorkSchedulesViewModel> timekeepingList { get; set; }
        public List<WorkSchedulesViewModel> StaffList { get; set; }
        public IEnumerable<WorkSchedulesViewModel> DayWorkList { get; set; }
        public string ImagePath { get; set; }
        public string ProfileImage { get; set; }
        //public vwTimekeepingList timekeepinglist { get; set; }
        [Display(Name = "StartTimeIn", ResourceType = typeof(Wording))]
        public string StartTimeIn { get; set; }
        [Display(Name = "EndTimeOut", ResourceType = typeof(Wording))]
        public string EndTimeOut { get; set; }
        public string BranchName { get; set; }
        public IndexViewModel<WorkSchedulesViewModel> pageIndexViewModel { get; set; }
        [Display(Name = "MinuteLate", ResourceType = typeof(Wording))]
        public Nullable<int> MinuteLate { get; set; }
        [Display(Name = "MinuteEarly", ResourceType = typeof(Wording))]
        public Nullable<int> MinuteEarly { get; set; }

        public Nullable<int> FPMachineId { get; set; }
         [Display(Name = "FPMachine", ResourceType = typeof(Wording))]
        public string Ten_may { get; set; }
    }
}