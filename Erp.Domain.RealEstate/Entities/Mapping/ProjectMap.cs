using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.RealEstate.Entities.Mapping
{
    public class ProjectMap : EntityTypeConfiguration<Project>
    {
        public ProjectMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name).HasMaxLength(150);
                        this.Property(t => t.Location).HasMaxLength(300);
            this.Property(t => t.Investor).HasMaxLength(150);
            this.Property(t => t.Constructor).HasMaxLength(150);
            this.Property(t => t.ProjectType).HasMaxLength(100);


            // Table & Column Mappings
            this.ToTable("RE_Project");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");
            this.Property(t => t.Name).HasColumnName("Name");

            this.Property(t => t.Location).HasColumnName("Location");
            this.Property(t => t.Investor).HasColumnName("Investor");
            this.Property(t => t.Constructor).HasColumnName("Constructor");
            this.Property(t => t.SiteArea).HasColumnName("SiteArea");
            this.Property(t => t.GrossFloorArea).HasColumnName("GrossFloorArea");
            this.Property(t => t.DensityOfBuilding).HasColumnName("DensityOfBuilding");
            this.Property(t => t.LaunchDate).HasColumnName("LaunchDate");
            this.Property(t => t.FinishDate).HasColumnName("FinishDate");
            this.Property(t => t.NumbersOfBlocks).HasColumnName("NumbersOfBlocks");
            this.Property(t => t.FloorHeight).HasColumnName("FloorHeight");
            this.Property(t => t.NumbersOfCondos).HasColumnName("NumbersOfCondos");
            this.Property(t => t.SmallestArea).HasColumnName("SmallestArea");
            this.Property(t => t.BiggestArea).HasColumnName("BiggestArea");
            this.Property(t => t.ProjectType).HasColumnName("ProjectType");

        }
    }
}
