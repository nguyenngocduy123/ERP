using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities
{
    public class DocumentAttribute
    {
        public DocumentAttribute()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }

        public Nullable<int> DocumentFieldId { get; set; }
        public string File { get; set; }
        public string Note { get; set; }
        public Nullable<int> OrderNo { get; set; }
        public string Size { get; set; }
        public string TypeFile { get; set; }
    }
}
