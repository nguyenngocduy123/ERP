using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class FPMachineMap : EntityTypeConfiguration<FPMachine>
    {
        public FPMachineMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties

            // Table & Column Mappings
            this.ToTable("Staff_FPMachine");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");

            this.Property(t => t.Ten_may).HasColumnName("Ten_may");
            this.Property(t => t.Ten_may_tinh).HasColumnName("Ten_may_tinh");
            this.Property(t => t.Loai_ket_noi).HasColumnName("Loai_ket_noi");
            this.Property(t => t.Ma_loai_ket_noi).HasColumnName("Ma_loai_ket_noi");
            this.Property(t => t.ID_Ket_noi_COM).HasColumnName("ID_Ket_noi_COM");
            this.Property(t => t.ID_Ket_noi_IP).HasColumnName("ID_Ket_noi_IP");
            this.Property(t => t.Cong_COM).HasColumnName("Cong_COM");
            this.Property(t => t.Dia_chi_IP).HasColumnName("Dia_chi_IP");
            this.Property(t => t.Toc_do_truyen).HasColumnName("Toc_do_truyen");
            this.Property(t => t.Port).HasColumnName("Port");
            this.Property(t => t.Loaimay).HasColumnName("Loaimay");
            this.Property(t => t.Passwd).HasColumnName("Passwd");
            this.Property(t => t.url).HasColumnName("url");
            this.Property(t => t.useurl).HasColumnName("useurl");
            this.Property(t => t.AutoID).HasColumnName("AutoID");
            this.Property(t => t.GetDataSchedule).HasColumnName("GetDataSchedule");
            this.Property(t => t.BranchId).HasColumnName("BranchId");

            this.Property(t => t.TeamviewerID).HasColumnName("TeamviewerID");
            this.Property(t => t.TeamviewerPassword).HasColumnName("TeamviewerPassword");
            this.Property(t => t.Note).HasColumnName("Note");
          
        }
    }
}
