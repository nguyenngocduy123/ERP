using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Erp.Domain.Staff.Entities;

namespace Erp.BackOffice.Staff.Models
{
    public class TaxViewModel
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
        [StringLength(100, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "StringError", ErrorMessage = null)]
        [Display(Name = "Name", ResourceType = typeof(Wording))]
        public string Name { get; set; }

        [Display(Name = "TaxCode", ResourceType = typeof(Wording))]
        public Nullable<int> TaxIncomePersonId { get; set; }

        [Display(Name = "Month", ResourceType = typeof(Wording))]
        public Nullable<int> Month { get; set; }
        public Nullable<int> Year { get; set; }
        
        [Display(Name = "PageIndex_SalaryTable", ResourceType = typeof(Wording))]
        public Nullable<int> SalaryTableId { get; set; }
        public string Status { get; set; }
        public string TaxIncomePersonName { get; set; }
        public string SalaryTableName { get; set; }

        public List<TaxIncomePersonDetailViewModel> Staffs { get; set; }
       
    }
}