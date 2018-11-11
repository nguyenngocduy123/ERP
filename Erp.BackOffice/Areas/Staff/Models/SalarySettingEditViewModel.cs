using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Staff.Models
{
    public class SalarySettingEditViewModel
    {
        public int Id { get; set; }
        public List<SalarySettingDetailViewModel> ListAllColumns { get; set; }
        public List<SelectListItem> SelectList_Group { get; set; }
        public SelectList SelectListGroupName { get; set; }
        public SelectList SelectListFormulaType { get; set; }
        public SelectList SelectListDataType { get; set; }
        public List<string> ListColumnsTimekeepingSynthesis { get; set; }
    }
}