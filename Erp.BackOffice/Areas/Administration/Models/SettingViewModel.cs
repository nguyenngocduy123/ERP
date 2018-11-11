using Erp.BackOffice.App_GlobalResources;
using System.ComponentModel.DataAnnotations;

namespace Erp.BackOffice.Areas.Administration.Models
{
    public class SettingViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "Key", ResourceType = typeof(Wording))]
        public string Key { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "Value", ResourceType = typeof(Wording))]
        public string Value { get; set; }

        public string Code { get; set; }

        [Display(Name = "Note", ResourceType = typeof(Wording))]
        public string Note { get; set; }

        [Display(Name = "IsLocked", ResourceType = typeof(Wording))]
        public bool? IsLocked { get; set; }
    }
}