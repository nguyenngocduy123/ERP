using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Erp.BackOffice.App_GlobalResources;

namespace Erp.BackOffice.Areas.Administration.Models
{
    public class UserUserViewModel
    {
        public int UserId {get;set;}

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "OldPassword", ResourceType = typeof(Wording))]
        public string OldPassword { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "NewPassword", ResourceType = typeof(Wording))]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "Mật khẩu không ngắn hơn 6 kí tự")]
        public string NewPassword { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Wording))]
        [Compare("NewPassword", ErrorMessage="Mật khẩu nhập không giống nhau.")]
        public string ConfirmPassword { get; set; }
    }
}