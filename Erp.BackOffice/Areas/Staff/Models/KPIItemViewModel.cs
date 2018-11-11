using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Staff.Models
{
    public class KPIItemViewModel
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

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [StringLength(100, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "StringError", ErrorMessage = null)]
        [Display(Name = "Name", ResourceType = typeof(Wording))]
        public string Name { get; set; }

        [Display(Name = "KPICatalogId", ResourceType = typeof(Wording))]
        public Nullable<int> KPICatalogId { get; set; }
        [Display(Name = "Measure", ResourceType = typeof(Wording))]
        public string Measure { get; set; }
        [Display(Name = "TargetScore_From", ResourceType = typeof(Wording))]
        public double TargetScore_From { get; set; }
        [Display(Name = "TargetScore_To", ResourceType = typeof(Wording))]
        public double TargetScore_To { get; set; }
        [Display(Name = "KPIWeight", ResourceType = typeof(Wording))]
        public double KPIWeight { get; set; }
        [Display(Name = "Description", ResourceType = typeof(Wording))]
        public string Description { get; set; }
        public SelectList MeasureSelectList { get; set; }
    }
}