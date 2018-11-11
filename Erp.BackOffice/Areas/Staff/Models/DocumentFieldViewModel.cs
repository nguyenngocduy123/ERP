using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Staff.Entities;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Erp.BackOffice.Helpers;
using Erp.BackOffice.Sale.Models;

namespace Erp.BackOffice.Staff.Models
{
    public class DocumentFieldViewModel
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

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        //[StringLength(100, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "StringError", ErrorMessage = null)]
        [Display(Name = "DocumentName", ResourceType = typeof(Wording))]
        public string Name { get; set; }

       // [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "DocumentType", ResourceType = typeof(Wording))]
        public Nullable<int> DocumentTypeId { get; set; }
        [Display(Name = "DocumentCode", ResourceType = typeof(Wording))]
        public string Code { get; set; }
      //  [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "IsSearch", ResourceType = typeof(Wording))]
        public string IsSearch { get; set; }
        [Display(Name = "IsSearch", ResourceType = typeof(Wording))]
        public string PositionName { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "Module", ResourceType = typeof(Wording))]
        public string Category { get; set; }

        [Display(Name = "DocumentType", ResourceType = typeof(Wording))]
        public string TypeName { get; set; }
        public IEnumerable<SelectListItem> DocumentTypeList { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> SearchList { get; set; }
        public IEnumerable<SelectListItem> DetailCategoryList { get; set; }
        public string File { get; set; }
        public string FilePath { get; set; }
        public int? CountFile { get; set; }
        public int? QuantityDownload { get; set; }
        public int? DocumentAttributeId { get; set; }
        public string TypeFile { get; set; }
     //   [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "DetailModule", ResourceType = typeof(Wording))]
        public Nullable<int> CategoryId { get; set; }
        public IEnumerable<DocumentAttributeViewModel> DocumentAttributeList { get; set; }
        public List<ObjectAttributeValueViewModel> AttributeValueList { get; set; }
        [Display(Name = "DetailModule", ResourceType = typeof(Wording))]
        public string CategoryDetail { get; set; }
    }
}