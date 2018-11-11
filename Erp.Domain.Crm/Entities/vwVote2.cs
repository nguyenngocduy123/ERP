using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Crm.Entities
{
    public class vwVote2
    {
        public vwVote2()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public Nullable<int> InvoiceId { get; set; }
        public Nullable<int> QuestionId { get; set; }
        public Nullable<int> AnswerId { get; set; }
        public Nullable<int> UsingServiceLogDetailId { get; set; }
        public string Note { get; set; }

        //vw
        public string StaffName { get; set; }
        public string CustomerName { get; set; }
        public Nullable<int> StaffId { get; set; }
        public string Type { get; set; }
        public string StaffCode { get; set; }
        public string ProfileImage { get; set; }
        public string ProductInvoiceCode { get; set; }
        public Nullable<int> ProductInvoiceId { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerImage { get; set; }
        public string ServiceName { get; set; }
        public string QuestionName { get; set; }
        public string AnswerContent { get; set; }
        public Nullable<int> BranchId { get; set; }
        public string BranchName { get; set; }
    }
}
