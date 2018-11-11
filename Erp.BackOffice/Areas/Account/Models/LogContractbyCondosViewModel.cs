using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Staff.Entities;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Account.Models
{
    public class LogContractbyCondosViewModel
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

        [Display(Name = "ContactType", ResourceType = typeof(Wording))]
        public string Type { get; set; }
        [Display(Name = "ContractCode", ResourceType = typeof(Wording))]
        public string Code { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "PlaceContract", ResourceType = typeof(Wording))]
        public string Place { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "DayEffective", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> EffectiveDate { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "QuantityContract", ResourceType = typeof(Wording))]
        public Nullable<int> ContractQuantity { get; set; }

        [Display(Name = "TemplateFile", ResourceType = typeof(Wording))]
        public string TemplateFile { get; set; }


        [Display(Name = "surrogate", ResourceType = typeof(Wording))]
        public Nullable<int> InfoPartyAId { get; set; }
        //[Display(Name = "IdTypeContract", ResourceType = typeof(Wording))]
        public string IdTypeContract { get; set; }


        [Display(Name = "Customer", ResourceType = typeof(Wording))]
        public Nullable<int> CustomerId { get; set; }

        [Display(Name = "CodeTrading", ResourceType = typeof(Wording))]
        public string TransactionCode { get; set; }
        [Display(Name = "Status", ResourceType = typeof(Wording))]
        public string Status { get; set; }

        [Display(Name = "Customer", ResourceType = typeof(Wording))]
        public string CustomerName { get; set; }
        [Display(Name = "surrogate", ResourceType = typeof(Wording))]
        public string InfoPartyAName { get; set; }
       [Display(Name = "CompanyName", ResourceType = typeof(Wording))]
        public string InfoPartyCompanyName { get; set; }


       [Display(Name = "NameCondos", ResourceType = typeof(Wording))]
       public Nullable<int> CondosId { get; set; }
       [Display(Name = "NameCondos", ResourceType = typeof(Wording))]
       public string NameCondos { get; set; }
       [Display(Name = "Quantity", ResourceType = typeof(Wording))]
       public Nullable<int> Quantity { get; set; }
       [Display(Name = "Price", ResourceType = typeof(Wording))]
       public Nullable<int> Price { get; set; }
       [Display(Name = "Unit", ResourceType = typeof(Wording))]
       public string Unit { get; set; }
       [Display(Name = "UnitMoney", ResourceType = typeof(Wording))]
       public string UnitMoney { get; set; }
       [Display(Name = "DayHandOver", ResourceType = typeof(Wording))]
       public Nullable<System.DateTime> DayHandOver { get; set; }
       [Display(Name = "DayPay", ResourceType = typeof(Wording))]
       public Nullable<System.DateTime> DayPay { get; set; }
    }
}