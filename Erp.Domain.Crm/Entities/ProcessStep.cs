using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Crm.Entities
{
    public class ProcessStep
    {
        public ProcessStep()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }
        public string Name { get; set; }

        public Nullable<int> StageId { get; set; }
        public string StepValue { get; set; }
        public Nullable<bool> IsRequired { get; set; }
        public Nullable<bool> IsSequential { get; set; }
        public Nullable<int> OrderNo { get; set; }
        public string EditControl { get; set; }

    }
}
