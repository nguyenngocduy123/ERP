using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Crm.Entities
{
    public class Process
    {
        public Process()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string Name { get; set; }

        public string Category { get; set; }
        public string DataSource { get; set; }
        //public Nullable<bool> RecordIsCreated { get; set; }
        //public Nullable<bool> RecordIsAssigned { get; set; }
        //public Nullable<bool> RecordFieldsChanges { get; set; }
        //public Nullable<bool> RecordIsDeleted { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string ActivateAs { get; set; }
        public string QueryRecivedUser { get; set; }
    }
}
