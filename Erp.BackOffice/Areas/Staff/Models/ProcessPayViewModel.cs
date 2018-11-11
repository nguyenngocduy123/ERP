using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Staff.Entities;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Staff.Models
{
    public class ProcessPayViewModel
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

        [Display(Name = "BasicPay", ResourceType = typeof(Wording))]
        public Nullable<int> BasicPay { get; set; }
        [Display(Name = "LevelPay", ResourceType = typeof(Wording))]
        public string LevelPay { get; set; }
        [Display(Name = "DayDecision", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> DayDecision { get; set; }
        [Display(Name = "DayEffective", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> DayEffective { get; set; }
        //[Display(Name = "StaffId", ResourceType = typeof(Wording))]
        public Nullable<int> StaffId { get; set; }
        [Display(Name = "CodeDecisions", ResourceType = typeof(Wording))]
        public string CodePay { get; set; }
        public List<ProcessPayDetail> DetailList { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "Content", ResourceType = typeof(Wording))]
        public string Content { get; set; }
        public StaffsViewModel model_Staff { get; set; }
    }
}