using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Entities.Mapping
{
    public class News_ViewedUserMap : EntityTypeConfiguration<News_ViewedUser>
    {
        public News_ViewedUserMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);
            // Table & Column Mappings
            this.ToTable("System_News_ViewedUser");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.NewsId).HasColumnName("NewsId");
            this.Property(t => t.ViewedUser).HasColumnName("ViewedUser");
            this.Property(t => t.ViewCount).HasColumnName("ViewCount");
            this.Property(t => t.ViewedDT).HasColumnName("ViewedDT");

        }
    }
}
