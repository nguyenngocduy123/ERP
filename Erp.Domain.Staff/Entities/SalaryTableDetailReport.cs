using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities
{
    public class SalaryTableDetailReport
    {
        public SalaryTableDetailReport()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }
        public Nullable<int> SalaryTableId { get; set; }
        public Nullable<int> StaffId { get; set; }
        public string ColumName { get; set; }
       // public Nullable<int> SalaryTableDetailId { get; set; }
        public string DataType { get; set; }
        public string Value { get; set; }

    }
}
