using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Entities.Mapping
{
    public class NewsMap : EntityTypeConfiguration<News>
    {
        public NewsMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ThumbnailPath)
                .HasMaxLength(500);

            this.Property(t => t.ImagePath)
                .HasMaxLength(500);

            this.Property(t => t.Title)
                .HasMaxLength(250);

            this.Property(t => t.Url)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("System_News");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.CreatedUser).HasColumnName("CreatedUser");
            this.Property(t => t.UpdateUser).HasColumnName("UpdateUser");
            this.Property(t => t.OrderNo).HasColumnName("OrderNo");
            this.Property(t => t.ThumbnailPath).HasColumnName("ThumbnailPath");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.ImagePath).HasColumnName("ImagePath");
            this.Property(t => t.CategoryId).HasColumnName("CategoryId");
            this.Property(t => t.IsPublished).HasColumnName("IsPublished");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.ShortContent).HasColumnName("ShortContent");
            this.Property(t => t.Content).HasColumnName("Content");
            this.Property(t => t.Url).HasColumnName("Url");
            this.Property(t => t.PublishedDate).HasColumnName("PublishedDate");

            // Relationships
            this.HasOptional(t => t.Category)
                .WithMany(t => t.News)
                .HasForeignKey(d => d.CategoryId);

        }
    }
}
