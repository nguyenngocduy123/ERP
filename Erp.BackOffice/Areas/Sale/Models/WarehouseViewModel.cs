using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Sale.Models
{
    public class WarehouseViewModel
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


        [StringLength(100, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "StringError", ErrorMessage = null)]
        [Display(Name = "Name", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        public string Name { get; set; }

        [Display(Name = "Code", ResourceType = typeof(Wording))]
        public string Code { get; set; }
        
        [Display(Name = "Note", ResourceType = typeof(Wording))]
        public string Note { get; set; }
        
        [Display(Name = "Address", ResourceType = typeof(Wording))]
        public string Address { get; set; }
        
        [Display(Name = "WardName", ResourceType = typeof(Wording))]
        public string WardId { get; set; }
        
        [Display(Name = "DistrictName", ResourceType = typeof(Wording))]
        public string DistrictId { get; set; }
        
        [Display(Name = "CityName", ResourceType = typeof(Wording))]
        public string CityId { get; set; }

        [Display(Name = "DrugStore", ResourceType = typeof(Wording))]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        public int? BranchId { get; set; }

        [Display(Name = "KeeperWarehouse", ResourceType = typeof(Wording))]
        public string KeeperId { get; set; }
        [Display(Name = "IsSale", ResourceType = typeof(Wording))]
        public bool? IsSale { get; set; }

        public List<ObjectAttributeValueViewModel> AttributeValueList { get; set; }
         [Display(Name = "CategoriesProduct", ResourceType = typeof(Wording))]
        public string Categories { get; set; }
    }
}