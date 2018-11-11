using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Entities
{
    public class News_ViewedUser
    {
        public int Id { get; set; }
        public int NewsId { get; set; }
        public int ViewedUser { get; set; }
        public int ViewCount { get; set; }
        public System.DateTime ViewedDT { get; set; }

    }
}
