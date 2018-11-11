using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Entities
{
    public class Location
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string ParentId { get; set; }

        public string Group { get; set; }
    }
}
