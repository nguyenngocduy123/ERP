using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Sale.Models
{
    public class vwCommision_BranchViewModel
    {
        public int Id { get; set; }

        [Display(Name = "CommisionId", ResourceType = typeof(Wording))]
        public Nullable<int> CommisionId { get; set; }
        [Display(Name = "BranchId", ResourceType = typeof(Wording))]
        public Nullable<int> BranchId { get; set; }
        [Display(Name = "CommisionName", ResourceType = typeof(Wording))]
        public string CommisionName { get; set; }
        [Display(Name = "PercentOfCommision", ResourceType = typeof(Wording))]
        public Nullable<int> PercentOfCommision { get; set; }
        [Display(Name = "StartDate", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> StartDate { get; set; }
        [Display(Name = "EndDate", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> EndDate { get; set; }
        [Display(Name = "BranchName", ResourceType = typeof(Wording))]
        public string BranchName { get; set; }

    }
}