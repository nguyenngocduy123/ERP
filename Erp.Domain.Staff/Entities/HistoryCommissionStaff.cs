using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities
{
    public class HistoryCommissionStaff
    {
        public HistoryCommissionStaff()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }
        public Nullable<int> StaffParentId { get; set; }
        public Nullable<int> StaffId { get; set; }
        public string StaffName { get; set; }
        public Nullable<int> Month { get; set; }
        public Nullable<int> Year { get; set; }
        public string PositionName { get; set; }
        public decimal? CommissionPercent { get; set; }
        public decimal? MinimumRevenue { get; set; }
        public decimal? RevenueDS { get; set; }
        public decimal? AmountCommission { get; set; }

    }
}
