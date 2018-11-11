using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Staff.Entities;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Erp.BackOffice.Staff.Models;
namespace Erp.BackOffice.Staff.Models
{
    public class InternalNotificationsViewModel
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

        public Nullable<bool> Disable { get; set; }
        public Nullable<bool> Seen { get; set; }
        public string AcctionName { get; set; }
        public string ModuleName { get; set; }

        [Display(Name = "PlaceOfNotice", ResourceType = typeof(Wording))]
        public string PlaceOfNotice { get; set; }
        [Display(Name = "PlaceOfNotice", ResourceType = typeof(Wording))]
        public string FullName { get; set; }
        [Display(Name = "PlaceOfReceipt", ResourceType = typeof(Wording))]
        public string PlaceOfReceipt { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "Title", ResourceType = typeof(Wording))]
        public string Titles { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "Content", ResourceType = typeof(Wording))]
        public string Content { get; set; }


        public string Type { get; set; }
        public int? Sale_BranchId { get; set; }

        public string ProfileImage { get; set; }
        public string UserName { get; set; }
        public IEnumerable<StaffsViewModel> StaffList { get; set; }
        public List<StaffsViewModel> ListCheckStaff { get; set; }
        public IEnumerable<NotificationsDetailViewModel> NotificationsDetailList { get; set; }
    }
}