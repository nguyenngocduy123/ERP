using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class SalaryTableDetailMap : EntityTypeConfiguration<SalaryTableDetail>
    {
        public SalaryTableDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Col1).HasMaxLength(150);
            this.Property(t => t.Col2).HasMaxLength(150);
            this.Property(t => t.Col3).HasMaxLength(150);
            this.Property(t => t.Col4).HasMaxLength(150);
            this.Property(t => t.Col5).HasMaxLength(150);
            this.Property(t => t.Col6).HasMaxLength(150);
            this.Property(t => t.Col7).HasMaxLength(150);
            this.Property(t => t.Col8).HasMaxLength(150);
            this.Property(t => t.Col9).HasMaxLength(150);
            this.Property(t => t.Col10).HasMaxLength(150);
            this.Property(t => t.Col11).HasMaxLength(150);
            this.Property(t => t.Col12).HasMaxLength(150);
            this.Property(t => t.Col13).HasMaxLength(150);
            this.Property(t => t.Col14).HasMaxLength(150);
            this.Property(t => t.Col15).HasMaxLength(150);
            this.Property(t => t.Col16).HasMaxLength(150);
            this.Property(t => t.Col17).HasMaxLength(150);
            this.Property(t => t.Col18).HasMaxLength(150);
            this.Property(t => t.Col19).HasMaxLength(150);
            this.Property(t => t.Col20).HasMaxLength(150);
            this.Property(t => t.Col21).HasMaxLength(150);
            this.Property(t => t.Col22).HasMaxLength(150);
            this.Property(t => t.Col23).HasMaxLength(150);
            this.Property(t => t.Col24).HasMaxLength(150);
            this.Property(t => t.Col25).HasMaxLength(150);
            this.Property(t => t.Col26).HasMaxLength(150);
            this.Property(t => t.Col27).HasMaxLength(150);
            this.Property(t => t.Col28).HasMaxLength(150);
            this.Property(t => t.Col29).HasMaxLength(150);
            this.Property(t => t.Col30).HasMaxLength(150);


            // Table & Column Mappings
            this.ToTable("Staff_SalaryTableDetail");

            this.Property(t => t.SalaryTableId).HasColumnName("SalaryTableId");
            this.Property(t => t.Col1).HasColumnName("Col1");
            this.Property(t => t.Col2).HasColumnName("Col2");
            this.Property(t => t.Col3).HasColumnName("Col3");
            this.Property(t => t.Col4).HasColumnName("Col4");
            this.Property(t => t.Col5).HasColumnName("Col5");
            this.Property(t => t.Col6).HasColumnName("Col6");
            this.Property(t => t.Col7).HasColumnName("Col7");
            this.Property(t => t.Col8).HasColumnName("Col8");
            this.Property(t => t.Col9).HasColumnName("Col9");
            this.Property(t => t.Col10).HasColumnName("Col10");
            this.Property(t => t.Col11).HasColumnName("Col11");
            this.Property(t => t.Col12).HasColumnName("Col12");
            this.Property(t => t.Col13).HasColumnName("Col13");
            this.Property(t => t.Col14).HasColumnName("Col14");
            this.Property(t => t.Col15).HasColumnName("Col15");
            this.Property(t => t.Col16).HasColumnName("Col16");
            this.Property(t => t.Col17).HasColumnName("Col17");
            this.Property(t => t.Col18).HasColumnName("Col18");
            this.Property(t => t.Col19).HasColumnName("Col19");
            this.Property(t => t.Col20).HasColumnName("Col20");
            this.Property(t => t.Col21).HasColumnName("Col21");
            this.Property(t => t.Col22).HasColumnName("Col22");
            this.Property(t => t.Col23).HasColumnName("Col23");
            this.Property(t => t.Col24).HasColumnName("Col24");
            this.Property(t => t.Col25).HasColumnName("Col25");
            this.Property(t => t.Col26).HasColumnName("Col26");
            this.Property(t => t.Col27).HasColumnName("Col27");
            this.Property(t => t.Col28).HasColumnName("Col28");
            this.Property(t => t.Col29).HasColumnName("Col29");
            this.Property(t => t.Col30).HasColumnName("Col30");

        }
    }
}
