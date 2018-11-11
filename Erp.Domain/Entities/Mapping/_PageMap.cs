using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Erp.Domain.Entities.Mapping
{
    public class PageMap : EntityTypeConfiguration<Page>
    {
        public PageMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(250);

            this.Property(t => t.AreaName)
                .HasMaxLength(50);

            this.Property(t => t.ActionName)
                .HasMaxLength(50);

            this.Property(t => t.ControllerName)
                .HasMaxLength(50);

            this.Property(t => t.Url)
                .HasMaxLength(255);

            this.Property(t => t.CssClassIcon)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("System_Page");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.AreaName).HasColumnName("AreaName");
            this.Property(t => t.ActionName).HasColumnName("ActionName");
            this.Property(t => t.ControllerName).HasColumnName("ControllerName");
            this.Property(t => t.Url).HasColumnName("Url");
            this.Property(t => t.OrderNo).HasColumnName("OrderNo");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CssClassIcon).HasColumnName("CssClassIcon");
            this.Property(t => t.ParentId).HasColumnName("ParentId");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.IsVisible).HasColumnName("IsVisible");
            this.Property(t => t.IsView).HasColumnName("IsView");
            this.Property(t => t.IsAdd).HasColumnName("IsAdd");
            this.Property(t => t.IsEdit).HasColumnName("IsEdit");
            this.Property(t => t.IsDelete).HasColumnName("IsDelete");
            this.Property(t => t.IsImport).HasColumnName("IsImport");
            this.Property(t => t.IsExport).HasColumnName("IsExport");
            this.Property(t => t.IsPrint).HasColumnName("IsPrint");
        }        
    }
}
