using Erp.BackOffice.App_GlobalResources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Erp.BackOffice.Administration.Models
{
    public class EditUserTypeModel
    {
        public int Id { get; set; }

        [StringLength(250, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "StringError")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "UserType", ResourceType = typeof(Wording))]
        public string Name { get; set; }

        [RegularExpression(@"^[0-9]+[0-9]*$", ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "ValueInvalid")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "OrderNo", ResourceType = typeof(Wording))]
        [Range(0, int.MaxValue, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "ValueInvalid", ErrorMessage = null)]
        public int OrderNo { get; set; }

        [Display(Name = "Note", ResourceType = typeof(Wording))]
        public string Note { get; set; }

        [Display(Name = "Scope", ResourceType = typeof(Wording))]
        public bool? Scope { get; set; }

        //  [Display(Name = "IsSystem", ResourceType = typeof(Wording))]
        public bool? IsSystem { get; set; }
        [StringLength(50, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "StringError")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "Code", ResourceType = typeof(Wording))]
        public string Code { get; set; }
    }
}