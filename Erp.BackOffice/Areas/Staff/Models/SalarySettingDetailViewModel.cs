using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Staff.Models
{
    [Serializable]
    public class SalarySettingDetailViewModel
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

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [StringLength(100, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "StringError", ErrorMessage = null)]
        [Display(Name = "Name", ResourceType = typeof(Wording))]
        public string Name { get; set; }

        public Nullable<int> SalarySettingId { get; set; }
        [Display(Name = "Code", ResourceType = typeof(Wording))]
        public string Code { get; set; }
        [Display(Name = "OrderNo", ResourceType = typeof(Wording))]
        public int OrderNo { get; set; }
        [Display(Name = "DefaultValue", ResourceType = typeof(Wording))]
        public double DefaultValue { get; set; }
        [Display(Name = "ValueFromSystem", ResourceType = typeof(Wording))]
        public bool IsDefaultValue { get; set; }

        [Display(Name = "Formula", ResourceType = typeof(Wording))]
        public string Formula { get; set; }
        public Nullable<int> ParentId { get; set; }
        public string FormulaType { get; set; }
        public string GroupName { get; set; }
        public bool IsGroup { get; set; }
        public bool IsDisplay { get; set; }
        public bool HasSubList { get; set; }
        public int NumberOfSubList { get; set; }
        public string DataType { get; set; }
        public bool IsSum { get; set; }
        public bool? IsMoney { get; set; }
        public bool? IsChange { get; set; }
    }
}