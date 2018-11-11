using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities
{
    public class KPIItem
    {
        public KPIItem()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string Name { get; set; }

        public int KPICatalogId { get; set; }
        public string Measure { get; set; }
        public double TargetScore_From { get; set; }
        public double TargetScore_To { get; set; }
        public double KPIWeight { get; set; }
        public string Description { get; set; }

    }
}
