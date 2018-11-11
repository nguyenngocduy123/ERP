using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities
{
    public class InternalNotifications
    {
        public InternalNotifications()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }

        public string PlaceOfNotice { get; set; }
        public string PlaceOfReceipt { get; set; }
        public string Titles  { get; set; }
        public string Type  { get; set; }
        public string Content  { get; set; }

        public Nullable<bool> Disable { get; set; }
        public Nullable<bool> Seen { get; set; }
        public string ActionName { get; set; }
        public string ModuleName { get; set; }

    }
}
