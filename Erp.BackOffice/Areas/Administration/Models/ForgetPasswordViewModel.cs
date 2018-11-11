using Erp.BackOffice.App_GlobalResources;
using System.ComponentModel.DataAnnotations;

namespace Erp.BackOffice.Areas.Administration.Models
{
    public class ForgetPasswordViewModel
    {
        [EmailAddress(ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "EmailError",ErrorMessage = null)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        public string Email { get; set; }
    }
}