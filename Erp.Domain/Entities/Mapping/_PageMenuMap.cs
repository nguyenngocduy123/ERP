using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Erp.Domain.Entities.Mapping
{
    public class PageMenuMap : EntityTypeConfiguration<PageMenu>
    {
        public PageMenuMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Id });

            // Properties
            this.Property(t => t.LanguageId)
                .HasMaxLength(10);

            this.Property(t => t.Name)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("System_PageMenu");
            this.Property(t => t.LanguageId).HasColumnName("LanguageId");
            this.Property(t => t.PageId).HasColumnName("PageId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Url).HasColumnName("Url");
            this.Property(t => t.OrderNo).HasColumnName("OrderNo");
            this.Property(t => t.CssClassIcon).HasColumnName("CssClassIcon");
            this.Property(t => t.ParentId).HasColumnName("ParentId");
            this.Property(t => t.IsVisible).HasColumnName("IsVisible");
            this.Property(t => t.IsDashboard).HasColumnName("IsDashboard");
            this.Property(t => t.DashboardView).HasColumnName("DashboardView");
        }
    }
}
