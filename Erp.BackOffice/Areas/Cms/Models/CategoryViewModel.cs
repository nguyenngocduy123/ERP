using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Erp.BackOffice.Areas.Cms.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [StringLength(250, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "StringError",ErrorMessage = null)]
        [Display(Name = "Title", ResourceType = typeof(Wording))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [StringLength(50, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "StringError", ErrorMessage = null)]
        [Display(Name = "List", ResourceType = typeof(Wording))]
        public string Code { get; set; }

        [StringLength(5000, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "StringError", ErrorMessage = null)]
        [Display(Name = "Description", ResourceType = typeof(Wording))]
        public string Description { get; set; }

        
        [Display(Name = "OrderNo", ResourceType = typeof(Wording))]
        [Range(0, int.MaxValue, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "ValueInvalid", ErrorMessage = null)]
        public int? OrderNo { get; set; }

        [Display(Name = "ParentMenuName", ResourceType = typeof(Wording))]
        public int? ParentId { get; set; }
        public string NameParent { get; set; }
        public IEnumerable<Category> ListCategory { get; set; }

        
        [Display(Name = "CreatedDate", ResourceType = typeof(Wording))]
        public DateTime? CreatedDate { get; set; }

        
        [Display(Name = "UpdatedDate", ResourceType = typeof(Wording))]
        public DateTime? ModifiedDate { get; set; }

        
        [Display(Name = "IsDeleted")]
        public bool? IsDeleted { get; set; }

        
        [Display(Name = "CreatedUser", ResourceType = typeof(Wording))]
        public int? CreatedUserId { get; set; }
        public string NameCreateUser { get; set; }

        
        [Display(Name = "UpdatedUser", ResourceType = typeof(Wording))]
        public int? ModifiedUserId { get; set; }
        public string NameModifiedUser { get; set; }

        public string Value { get; set; }
        public bool? Check { get; set; }
    }
}