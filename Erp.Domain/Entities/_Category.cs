using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Entities
{
    public class Category
    {
        public Category()
        {
            this.Categories = new List<Category>();
            this.News = new List<News>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Nullable<int> OrderNo { get; set; }
        public Nullable<int> ParentId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<News> News { get; set; }
    }
}
