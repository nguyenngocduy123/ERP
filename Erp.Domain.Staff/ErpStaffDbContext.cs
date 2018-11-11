using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Entities.Mapping;
using System.Reflection;
namespace Erp.Domain.Staff
{
    public class ErpStaffDbContext : DbContext, IDbContext
    {
        static ErpStaffDbContext()
        {
            Database.SetInitializer<ErpStaffDbContext>(null);
        }

        public ErpStaffDbContext()
            : base("Name=ErpDbContext")
        {
            // this.Configuration.LazyLoadingEnabled = false;
            // this.Configuration.ProxyCreationEnabled = false;
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        //Erp
        public DbSet<Branch> Branch { get; set; }
        public DbSet<vwBranch> vwBranch { get; set; }
        public DbSet<Staffs> Staffs { get; set; }
        public DbSet<StaffFamily> StaffFamily { get; set; }
        public DbSet<BranchDepartment> BranchDepartment { get; set; }
        public DbSet<vwBranchDepartment> vwBranchDepartment { get; set; }
        public DbSet<vwStaffs> vwStaffs { get; set; }
        public DbSet<KPICatalog> KPICatalog { get; set; }
        public DbSet<KPIItem> KPIItem { get; set; }
        public DbSet<Bank> Bank { get; set; }
        public DbSet<Technique> Technique { get; set; }
        public DbSet<DayOff> DayOff { get; set; }
        public DbSet<WorkingProcess> WorkingProcess { get; set; }
        public DbSet<ProcessPay> ProcessPay { get; set; }
        public DbSet<ProcessPayDetail> ProcessPayDetail { get; set; }
        public DbSet<BonusDiscipline> BonusDiscipline { get; set; }
        public DbSet<vwBonusDiscipline> vwBonusDiscipline { get; set; }
        public DbSet<SymbolTimekeeping> SymbolTimekeeping { get; set; }
        public DbSet<vwDayOff> vwDayOff { get; set; }
        public DbSet<Shifts> Shifts { get; set; }
        public DbSet<Timekeeping> Timekeeping { get; set; }
        public DbSet<vwTimekeeping> vwTimekeeping { get; set; }
        public DbSet<WorkSchedules> WorkSchedules { get; set; }
        public DbSet<Holidays> Holidays { get; set; }
        public DbSet<vwWorkSchedules> vwWorkSchedules { get; set; }
        public DbSet<TransferWork> TransferWork { get; set; }
        public DbSet<vwTransferWork> vwTransferWork { get; set; }
        public DbSet<DocumentType> DocumentType { get; set; }
        public DbSet<DocumentAttribute> DocumentAttribute { get; set; }
        public DbSet<DocumentField> DocumentField { get; set; }
        public DbSet<vwDocumentField> vwDocumentField { get; set; }
        public DbSet<LogDocumentAttribute> LogDocumentAttribute { get; set; }
        public DbSet<vwLogDocumentAttribute> vwLogDocumentAttribute { get; set; }
        public DbSet<LabourContract> LabourContract { get; set; }
        public DbSet<vwLabourContract> vwLabourContract { get; set; }
        public DbSet<LabourContractType> LabourContractType { get; set; }
      
        public DbSet<NotificationsDetail> NotificationsDetail { get; set; }
        public DbSet<vwNotificationsDetail> vwNotificationsDetail { get; set; }
        public DbSet<SalaryAdvance> SalaryAdvance { get; set; }
        public DbSet<vwSalaryAdvance> vwSalaryAdvance { get; set; }
        public DbSet<vwTotalTimekeeping> vwTotalTimekeeping { get; set; }
        public DbSet<SalarySetting> SalarySetting { get; set; }
        public DbSet<SalarySettingDetail> SalarySettingDetail { get; set; }
        public DbSet<TimekeepingSynthesis> TimekeepingSynthesis { get; set; }
        public DbSet<vwTimekeepingSynthesis> vwTimekeepingSynthesis { get; set; }
        public DbSet<SalarySettingDetail_Staff> SalarySettingDetail_Staff { get; set; }
        public DbSet<SalaryTable> SalaryTable { get; set; }
        public DbSet<RegisterForOvertime> RegisterForOvertime { get; set; }
        public DbSet<vwRegisterForOvertime> vwRegisterForOvertime { get; set; }
        public DbSet<SalaryTableDetail> SalaryTableDetail { get; set; }
        public DbSet<FPMachine> FPMachine { get; set; }
        public DbSet<CheckInOut> CheckInOut { get; set; }
        public DbSet<vwCheckInOut> vwCheckInOut { get; set; }
        public DbSet<FingerPrint> FingerPrint { get; set; }
        public DbSet<TimekeepingList> TimekeepingList { get; set; }
        public DbSet<vwTimekeepingList> vwTimekeepingList { get; set; }
        public DbSet<InternalNotifications> InternalNotifications { get; set; }
        public DbSet<vwInternalNotifications> vwInternalNotifications { get; set; }
        public DbSet<SalaryTableDetailReport> SalaryTableDetailReport { get; set; }
        public DbSet<SalaryTableDetail_Staff> SalaryTableDetail_Staff { get; set; }
        public DbSet<StaffSocialInsurance> StaffSocialInsurance { get; set; }
        public DbSet<vwStaffSocialInsurance> vwStaffSocialInsurance { get; set; }
        public DbSet<KPILog> KPILog { get; set; }
        public DbSet<KPILogDetail> KPILogDetail { get; set; }
        public DbSet<vwKPILogDetail> vwKPILogDetail { get; set; }
        public DbSet<KPILogDetail_Item> KPILogDetail_Item { get; set; }
        public DbSet<DotBCBHXH> DotBCBHXH { get; set; }
        public DbSet<DotBCBHXHDetail> DotBCBHXHDetail { get; set; }
        public DbSet<vwDotBCBHXHDetail> vwDotBCBHXHDetail { get; set; }
        public DbSet<WelfarePrograms> WelfarePrograms { get; set; }
        public DbSet<WelfareProgramsDetail> WelfareProgramsDetail { get; set; }
        public DbSet<vwWelfareProgramsDetail> vwWelfareProgramsDetail { get; set; }
        public DbSet<DotGQCDBHXH> DotGQCDBHXH { get; set; }
        public DbSet<DotGQCDBHXHDetail> DotGQCDBHXHDetail { get; set; }
        public DbSet<vwDotGQCDBHXHDetail> vwDotGQCDBHXHDetail { get; set; }
        public DbSet<TaxRate> TaxRate { get; set; }
        public DbSet<TaxIncomePerson> TaxIncomePerson { get; set; }
        public DbSet<TaxIncomePersonDetail> TaxIncomePersonDetail { get; set; }
        public DbSet<vwTaxIncomePersonDetail> vwTaxIncomePersonDetail { get; set; }
        public DbSet<ThuNhapChiuThue> ThuNhapChiuThue { get; set; }
        public DbSet<GiamTruThueTNCN> GiamTruThueTNCN { get; set; }
        public DbSet<Tax> Tax { get; set; }
        public DbSet<vwTax> vwTax { get; set; }

        public DbSet<vwFingerPrint> vwFingerPrint { get; set; }
        public DbSet<StaffAllowance> StaffAllowance { get; set; }
        public DbSet<StaffLuongGiuHopDong> StaffLuongGiuHopDong { get; set; }
        public DbSet<CalendarVisitDrugStore> CalendarVisitDrugStore { get; set; }
        public DbSet<vwCalendarVisitDrugStore> vwCalendarVisitDrugStore { get; set; }
        public DbSet<Position> Position { get; set; }
        public DbSet<HistoryCommissionStaff> HistoryCommissionStaff { get; set; }
        //<append_content_DbSet_here>

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        //    var addMethod = typeof(System.Data.Entity.ModelConfiguration.Configuration.ConfigurationRegistrar)
        //      .GetMethods()
        //      .Single(m =>
        //        m.Name == "Add"
        //        && m.GetGenericArguments().Any(a => a.Name == "TEntityType"));

        //    var domainCurrent = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.GetName().Name == "Erp.Domain.Staff");
        //    foreach (var assembly in domainCurrent)
        //    {
        //        var configTypes = assembly
        //          .GetTypes()
        //          .Where(t => t.BaseType != null
        //            && t.BaseType.IsGenericType
        //            && t.BaseType.GetGenericTypeDefinition() == typeof(System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<>));

        //        foreach (var type in configTypes)
        //        {
        //            var entityType = type.BaseType.GetGenericArguments().Single();

        //            var entityConfig = assembly.CreateInstance(type.FullName);
        //            addMethod.MakeGenericMethod(entityType).Invoke(modelBuilder.Configurations, new object[] { entityConfig });
        //        }
        //    }
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(GetType())); //Current Assembly
            base.OnModelCreating(modelBuilder);
        }
            
    }

    public interface IDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
        void Dispose();
    }
}
