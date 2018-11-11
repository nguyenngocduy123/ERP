using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities
{
    public class vwDotGQCDBHXHDetail
    {
        public vwDotGQCDBHXHDetail()
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

        public Nullable<int> DotGQCDBHXHId { get; set; }
        public Nullable<int> StaffId { get; set; }
        public Nullable<int> DayOffId { get; set; }
        public string SocietyCode { get; set; }
        public string DKTH_TinhTrang { get; set; }
        public Nullable<System.DateTime> DKTH_ThoiDiem { get; set; }
        public Nullable<System.DateTime> DayStart { get; set; }
        public Nullable<System.DateTime> DayEnd { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string PaymentMethod { get; set; }
        public string Note { get; set; }
        public string StaffName { get; set; }
        public string DayOffName { get; set; }
        public string Code { get; set; }
        public string Type { get; set;}
    }
}
