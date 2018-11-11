using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Staff.Models
{
    public class spChartSalaryInMonth
    {
        public string BranchName { get; set; }
        public decimal? TotalSalary { get; set; }
        public Nullable<int> Id { get; set; }
    }
}