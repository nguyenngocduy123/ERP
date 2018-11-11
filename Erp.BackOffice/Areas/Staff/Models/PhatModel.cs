using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Erp.BackOffice.Staff.Models
{
    public class PhatModel
    {
        public int Id { get; set; }
        public Nullable<int> StaffId { get; set; }
        public Nullable<int> MoneyPhat { get; set; }
    }
}