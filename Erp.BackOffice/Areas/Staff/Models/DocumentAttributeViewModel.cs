using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Staff.Entities;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Staff.Models
{
    public class DocumentAttributeViewModel
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

        [Display(Name = "DocumentFieldId", ResourceType = typeof(Wording))]
        public Nullable<int> DocumentFieldId { get; set; }
        [Display(Name = "File", ResourceType = typeof(Wording))]
        public string File { get; set; }
        [Display(Name = "Note", ResourceType = typeof(Wording))]
        public string Note { get; set; }
        [Display(Name = "OrderNo", ResourceType = typeof(Wording))]
        public Nullable<int> OrderNo { get; set; }
        [Display(Name = "SizeFile", ResourceType = typeof(Wording))]
        public string Size { get; set; }
        [Display(Name = "TypeFile", ResourceType = typeof(Wording))]
        public string TypeFile { get; set; }
        public string FilePath { get; set; }
        public string Icon { get; set; }
        public int? QuantityDownload { get; set; }
    }
}