using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities
{
    public class SalarySettingDetail
    {
        public SalarySettingDetail()
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

        public Nullable<int> SalarySettingId { get; set; }
        public int OrderNo { get; set; }
        public double? DefaultValue { get; set; }
        public bool IsDefaultValue { get; set; }
        public string Formula { get; set; }
        public Nullable<int> ParentId { get; set; }
        public string FormulaType { get; set; }
        public string GroupName { get; set; }
        public bool IsGroup { get; set; }
        public bool IsDisplay { get; set; }
        public string DataType { get; set; }
        public bool? IsSum { get; set; }
        public bool? IsChange { get; set; }
    }
}
