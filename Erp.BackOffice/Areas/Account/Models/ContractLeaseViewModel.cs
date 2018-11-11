using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Staff.Entities;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Account.Models
{
    public class ContractLeaseViewModel
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


        [Display(Name = "NameCondos", ResourceType = typeof(Wording))]
        public Nullable<int> CondosId { get; set; }
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
        [Display(Name = "DayEffective", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> EffectiveDate { get; set; }
        [Display(Name = "ExpiryDate", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        [Display(Name = "DayPay", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> DayPay { get; set; }
        //view condos
      
        [Display(Name = "NameCondos", ResourceType = typeof(Wording))]
        public string NameCondos { get; set; }
        public Nullable<int> ProjectId { get; set; }
        public Nullable<int> BlockId { get; set; }
        public Nullable<int> FloorId { get; set; }
        [Display(Name = "CodeCondos", ResourceType = typeof(Wording))]
        public string CodeCondos { get; set; }
        [Display(Name = "RealEstateArea", ResourceType = typeof(Wording))]
        public double? Area { get; set; }
        [Display(Name = "PriceCondos", ResourceType = typeof(Wording))]
        public double? PriceCondos { get; set; }
        [Display(Name = "NumbersOfLivingRoom", ResourceType = typeof(Wording))]
        public Nullable<int> NumbersOfLivingRoom { get; set; }
        [Display(Name = "NumbersOfBedRoom", ResourceType = typeof(Wording))]
        public Nullable<int> NumbersOfBedRoom { get; set; }
        [Display(Name = "NumbersOfKitchenRoom", ResourceType = typeof(Wording))]
        public Nullable<int> NumbersOfKitchenRoom { get; set; }
        [Display(Name = "NumbersOfToilet", ResourceType = typeof(Wording))]
        public Nullable<int> NumbersOfToilet { get; set; }
        [Display(Name = "NumbersOfBalcony", ResourceType = typeof(Wording))]
        public Nullable<int> NumbersOfBalcony { get; set; }
        [Display(Name = "Orientation", ResourceType = typeof(Wording))]
        public string Orientation { get; set; }
        [Display(Name = "Status", ResourceType = typeof(Wording))]
        public string Status { get; set; }
        //[Display(Name = "Currency", ResourceType = typeof(Wording))]
        //public string Currency { get; set; }
           [Display(Name = "ProjectName", ResourceType = typeof(Wording))]
        public string ProjectName { get; set; }
           [Display(Name = "BlockName", ResourceType = typeof(Wording))]
        public string BlockName { get; set; }
           [Display(Name = "FloorName", ResourceType = typeof(Wording))]
        public string FloorName { get; set; }
    }
}