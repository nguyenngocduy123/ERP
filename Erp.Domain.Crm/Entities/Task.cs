using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Crm.Entities
{
    public class Task
    {
        public Task()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }

        public string Subject { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public string ParentType { get; set; }
        public Nullable<int> ParentId { get; set; }
        public Nullable<int> ContactId { get; set; }
        public string Priority { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Note { get; set; }
        public Nullable<bool> IsSendNotifications { get; set; }
    }
}
