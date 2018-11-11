using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using Erp.Domain.Crm.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Crm.Models
{
    public class vwVoteViewModel
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
        [Display(Name = "Customer", ResourceType = typeof(Wording))]
        public Nullable<int> CustomerId { get; set; }
       
        public Nullable<int> InvoiceId { get; set; }

        public Nullable<int> QuestionId { get; set; }
        public Nullable<int> AnswerId { get; set; }
        public Nullable<int> UsingServiceLogDetailId { get; set; }
        [Display(Name = "Note", ResourceType = typeof(Wording))]
        public string Note { get; set; }

        //vw
        [Display(Name = "NameStaff", ResourceType = typeof(Wording))]
        public string StaffName { get; set; }
        [Display(Name = "Customer", ResourceType = typeof(Wording))]
        public string CustomerName { get; set; }
        public Nullable<int> StaffId { get; set; }
        public string Type { get; set; }
        [Display(Name = "CodeStaff", ResourceType = typeof(Wording))]
        public string StaffCode { get; set; }
        public string ProfileImage { get; set; }
        [Display(Name = "ProductInvoiceCode", ResourceType = typeof(Wording))]
        public string ProductInvoiceCode { get; set; }
        public Nullable<int> ProductInvoiceId { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerImage { get; set; }
        public string ServiceName { get; set; }
        public string QuestionName { get; set; }
        public string AnswerContent { get; set; }
        public Nullable<int> BranchId { get; set; }
        public string BranchName { get; set; }

        public List<AnswerViewModel> AnswerList { get; set; }
    }

   
}