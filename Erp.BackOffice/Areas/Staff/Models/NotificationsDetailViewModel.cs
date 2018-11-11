using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Staff.Entities;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Staff.Models
{
    public class NotificationsDetailViewModel
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

      
        [Display(Name = "Comment", ResourceType = typeof(Wording))]
        public string Comment { get; set; }

        public int? StaffId { get; set; }
        public int? NotificationsId { get; set; }
        [Display(Name = "NameStaff", ResourceType = typeof(Wording))]
        public string Name { get; set; }
        [Display(Name = "CodeStaff", ResourceType = typeof(Wording))]
        public string Code { get; set; }

        public Nullable<bool> Gender { get; set; }
        public string ProfileImage { get; set; }
    }
}