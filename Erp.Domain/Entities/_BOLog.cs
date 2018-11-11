using System;
using System.Collections.Generic;

namespace Erp.Domain.Entities
{
    public partial class BOLog
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Action { get; set; }
        public string Note { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string Controller { get; set; }
        public string Area { get; set; }
        public string Data { get; set; }
        public Nullable<int> Type { get; set; }
    }
}
