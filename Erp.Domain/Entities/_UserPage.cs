using System;
using System.Collections.Generic;

namespace Erp.Domain.Entities
{
    public partial class UserPage
    {
        public int UserId { get; set; }
        public int PageId { get; set; }
        public Nullable<bool> View { get; set; }
        public Nullable<bool> Edit { get; set; }
        public Nullable<bool> Add { get; set; }
        public Nullable<bool> Delete { get; set; }
        public Nullable<bool> Import { get; set; }
        public Nullable<bool> Export { get; set; }
        public Nullable<bool> Print { get; set; }
        //public virtual Page Page { get; set; }
        //public virtual User User { get; set; }
    }
}
