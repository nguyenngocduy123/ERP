using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Sale.Models
{
    public class UsingServiceLogViewModel
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

        [Display(Name = "ServiceInvoiceDetailId", ResourceType = typeof(Wording))]
        public Nullable<int> ServiceInvoiceDetailId { get; set; }

        public Nullable<int> Quantity { get; set; }
        public Nullable<int> QuantityUsed { get; set; }
        [Display(Name = "ServiceName", ResourceType = typeof(Wording))]
        public Nullable<int> ServiceId { get; set; }

        public Nullable<int> ServiceComboId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string CategoryCode { get; set; }
        public Nullable<bool> IsCombo { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public string ItemCategory { get; set; }
        public Nullable<int> ProductInvoiceId { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public Nullable<System.DateTime> ProductInvoiceDate { get; set; }
        public string ProductInvoiceCode { get; set; }

        [Display(Name = "CustomerName", ResourceType = typeof(Wording))]
        public string CustomerName { get; set; }
        [Display(Name = "CustomerCode", ResourceType = typeof(Wording))]
        public string CustomerCode { get; set; }
    }
}