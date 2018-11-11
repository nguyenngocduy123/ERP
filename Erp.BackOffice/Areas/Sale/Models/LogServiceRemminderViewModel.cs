using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Sale.Models
{
    public class LogServiceRemminderViewModel
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

        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        //[StringLength(100, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "StringError", ErrorMessage = null)]
        //[Display(Name = "Name", ResourceType = typeof(Wording))]
        //public string Name { get; set; }

        [Display(Name = "ProductInvoiceDetailId", ResourceType = typeof(Wording))]
        public Nullable<int> ProductInvoiceDetailId { get; set; }
        [Display(Name = "ReminderId", ResourceType = typeof(Wording))]
        public Nullable<int> ReminderId { get; set; }
        [Display(Name = "ReminderName", ResourceType = typeof(Wording))]
        public string ReminderName { get; set; }
        [Display(Name = "ReminderDate", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> ReminderDate { get; set; }
        [Display(Name = "ServiceId", ResourceType = typeof(Wording))]
        public Nullable<int> ServiceId { get; set; }

        public string ProductGroupCode { get; set; }
        public string ProductGroupName { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string CustomerName { get; set; }
        public string ProductInvoiceCode { get; set; }
        public Nullable<System.DateTime> ProductInvoiceDate { get; set; }
        public Nullable<int> ProductInvoiceId { get; set; }
        public Nullable<int> CustomerId { get; set; }

    }
}