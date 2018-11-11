using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;
using Erp.BackOffice.App_GlobalResources;

namespace Erp.BackOffice.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("ErpDbContext")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
    }

    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
       
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [DataType(DataType.Password)]
        [Display(Name = "CurrentPassword", ResourceType = typeof(Wording))]
        public string OldPassword { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceName = "Password_LengthError", ErrorMessageResourceType = typeof(Error))]
        [DataType(DataType.Password)]
        [Display(Name = "NewPassword", ResourceType = typeof(Wording))]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmNewPassword", ResourceType = typeof(Wording))]
        [Compare("NewPassword", ErrorMessageResourceName = "ConfirmPassword_NotMatch", ErrorMessageResourceType = typeof(Error))]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required(ErrorMessageResourceName="Username_Required", ErrorMessageResourceType=typeof(Error))]
        [Display(Name = "Username", ResourceType = typeof(Wording))]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceName="Password_Required", ErrorMessageResourceType=typeof(Error))]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Wording))]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessageResourceName="Username_Required", ErrorMessageResourceType=typeof(Error))]
        [Display(Name = "Username", ResourceType = typeof(Wording))]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceName="Password_Required", ErrorMessageResourceType=typeof(Error))]
        [StringLength(100, ErrorMessageResourceName = "Password_LengthError", ErrorMessageResourceType = typeof(Error), MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Wording))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType=typeof(Wording))]
        [Compare("Password", ErrorMessageResourceName = "ConfirmPassword_NotMatch", ErrorMessageResourceType = typeof(Error))]
        public string ConfirmPassword { get; set; }
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
