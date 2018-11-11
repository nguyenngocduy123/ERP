using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Staff.Models
{
    public class FormulaEditorViewModel
    {
        public int TargetId { get; set; }
        public int SalarySettingId { get; set; }
        public string FormulaEditor_Value { get; set; }
        public List<SalarySettingDetailViewModel> ListAllColumns { get; set; }
        public List<string> ListColumnsTimekeepingSynthesis { get; set; } // ki?m tra filed
        public List<string> ListColumnsProcessPay { get; set; } // ki?m tra filed
        public List<string> ListColumnsSalaryAdvance { get; set; } 
        public List<Setting> ListColumnsSetting { get; set; }
        public List<string> ListColumnsBank { get; set; }
        public List<string> ListAllColumnsSalarySeniorit { get; set; }
        public List<string> ListColumnsStaff { get; set; }
        public List<Domain.Staff.Entities.SalarySetting> ListSalarySetting { get; set; }

        public List<string> ListAllColumnsPhat { get; set; }

        public List<string> ListAllColumnsKhenThuong { get; set; }

        public List<string> ListAllColumnsPhuCap { get; set; }

        public List<string> ListAllColumnsGiuLuongTheoHopDong { get; set; }
    }
}