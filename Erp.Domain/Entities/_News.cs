using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Entities
{
    public class News
    {
        public News()
        {
        }

        public int Id { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> CreatedUser { get; set; }
        public Nullable<int> UpdateUser { get; set; }
        public Nullable<int> OrderNo { get; set; }
        public string ThumbnailPath { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string ImagePath { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<bool> IsPublished { get; set; }
        public string Title { get; set; }
        public string ShortContent { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public Nullable<System.DateTime> PublishedDate { get; set; }
        public virtual Category Category { get; set; }
    }
}
