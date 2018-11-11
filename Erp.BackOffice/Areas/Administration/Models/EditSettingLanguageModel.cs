using Erp.BackOffice.App_GlobalResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Erp.BackOffice.Administration.Models
{
    public class EditSettingLanguageModel
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "LanguageCode", ResourceType = typeof(Wording))]
        public string Id { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "Language", ResourceType = typeof(Wording))]
        public string Name { get; set; }

        [Display(Name = "Default", ResourceType = typeof(Wording))]
        public bool IsDefault { get; set; }

        //[Required]
        [Display(Name = "ActiveImage", ResourceType = typeof(Wording))]
        public string ActiveImage { get; set; }
        public string ActiveImage64String { get; set; }

        //[Required]
        [Display(Name = "DeactiveImage", ResourceType = typeof(Wording))]
        public string DeactiveImage { get; set; }
        public string DeactiveImage64String { get; set; }
    }
}