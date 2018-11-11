using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Erp.BackOffice.Staff.Models;
using Erp.BackOffice.Crm.Models;

namespace Erp.BackOffice.Sale.Models
{
    public class UsingServiceLogDetailViewModel
    {
        public int Id { get; set; }

        [Display(Name = "IsDeleted")]
        public bool? IsDeleted { get; set; }

        [Display(Name = "CreatedUser", ResourceType = typeof(Wording))]
        public int? CreatedUserId { get; set; }
        public string CreatedUserName { get; set; }

        [Display(Name = "UsingServiceDate", ResourceType = typeof(Wording))]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "ModifiedUser", ResourceType = typeof(Wording))]
        public int? ModifiedUserId { get; set; }
        public string ModifiedUserName { get; set; }

        [Display(Name = "ModifiedDate", ResourceType = typeof(Wording))]
        public DateTime? ModifiedDate { get; set; }

        [Display(Name = "AssignedUserId", ResourceType = typeof(Wording))]
        public int? AssignedUserId { get; set; }
        public string AssignedUserName { get; set; }

        public Nullable<int> UsingServiceId { get; set; }
        [Display(Name = "NameStaff", ResourceType = typeof(Wording))]
        public Nullable<int> StaffId { get; set; }
         [Display(Name = "NameStaff", ResourceType = typeof(Wording))]
        public string Name { get; set; }
         [Display(Name = "CodeStaff", ResourceType = typeof(Wording))]
        public string Code { get; set; }
        public string ProductInvoiceCode { get; set; }
        public int? ProductInvoiceId { get; set; }
        public int? CustomerId { get; set; }
        public string Type { get; set; }
        [Display(Name = "StatusQuo", ResourceType = typeof(Wording))]
        public string Status { get; set; }
        public string LastName { get; set; }
        [Display(Name = "ServiceName", ResourceType = typeof(Wording))]
        public string ServiceName { get; set; }

        [Display(Name = "CustomerName", ResourceType = typeof(Wording))]
        public string CustomerName { get; set; }
        [Display(Name = "CustomerCode", ResourceType = typeof(Wording))]
        public string CustomerCode { get; set; }
        public Nullable<bool> IsVote { get; set; }
        public string CustomerImage { get; set; }
        public string ProfileImage { get; set; }
        public List<DocumentAttributeViewModel> DetailList { get; set; }
        public int? BranchId { get; set; }
        public List<vwVoteViewModel> list_vote { get; set; }
    }
}