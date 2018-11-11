using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Entities
{
    public class Language
    {
        public Language()
        {
            this.PageMenus = new List<PageMenu>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public string ActiveImage { get; set; }
        public string DeactiveImage { get; set; }
        public virtual ICollection<PageMenu> PageMenus { get; set; }
    }
}
