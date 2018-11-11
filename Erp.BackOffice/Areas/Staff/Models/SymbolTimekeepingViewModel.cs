using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Staff.Entities;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Erp.BackOffice.Areas.Cms.Models;

namespace Erp.BackOffice.Staff.Models
{
    public class SymbolTimekeepingViewModel
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
        [Display(Name = "Symbol", ResourceType = typeof(Wording))]
        public string Code { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [StringLength(100, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "StringError", ErrorMessage = null)]
        [Display(Name = "SymbolName", ResourceType = typeof(Wording))]
        public string Name { get; set; }
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "QuantityDayOff", ResourceType = typeof(Wording))]
        public Nullable<int> Quantity { get; set; }
        //   [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "Timekeeping", ResourceType = typeof(Wording))]
        public bool? Timekeeping { get; set; }
        [Display(Name = "ApplicationForLeave", ResourceType = typeof(Wording))]
        public bool? DayOff { get; set; }
        public List<CategoryViewModel> ListCategory { get; set; }
        [Display(Name = "Color", ResourceType = typeof(Wording))]
        public string Color { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "SymbolDefault", ResourceType = typeof(Wording))]
        public string CodeDefault { get; set; }
         [Display(Name = "Absent", ResourceType = typeof(Wording))]
        public bool? Absent { get; set; }
         public string Icon { get; set; }
    }
}