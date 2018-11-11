using Erp.BackOffice.Staff.Models;
using Erp.Domain.Staff.Entities;
using System.Web.Mvc;
using Erp.Domain.Sale.Entities;
namespace Erp.BackOffice.Staff
{
    public class StaffAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Staff";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
               "Staff_Branch",
               "Branch/{action}/{id}",
               new { controller = "Branch", action = "Index", id = UrlParameter.Optional }
           );
            context.MapRoute(
                     "Staff_Staffs",
                     "Staffs/{action}/{id}",
                     new { controller = "Staffs", action = "Index", id = UrlParameter.Optional }
                 );

            context.MapRoute(
                 "Staff_StaffFamily",
                 "StaffFamily/{action}/{id}",
                 new { controller = "StaffFamily", action = "Index", id = UrlParameter.Optional }
             );
            context.MapRoute(
                "Staff_BranchDepartment",
                "BranchDepartment/{action}/{id}",
                new { controller = "BranchDepartment", action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
            "Staff_KPICatalog",
            "KPICatalog/{action}/{id}",
            new { controller = "KPICatalog", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_KPIItem",
            "KPIItem/{action}/{id}",
            new { controller = "KPIItem", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_vwKPICatalog_Staff",
            "vwKPICatalog_Staff/{action}/{id}",
            new { controller = "vwKPICatalog_Staff", action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
            "Staff_Bank",
            "Bank/{action}/{id}",
            new { controller = "Bank", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_Technique",
            "Technique/{action}/{id}",
            new { controller = "Technique", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_DayOff",
            "DayOff/{action}/{id}",
            new { controller = "DayOff", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_WorkingProcess",
            "WorkingProcess/{action}/{id}",
            new { controller = "WorkingProcess", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_ProcessPay",
            "ProcessPay/{action}/{id}",
            new { controller = "ProcessPay", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_BonusDiscipline",
            "BonusDiscipline/{action}/{id}",
            new { controller = "BonusDiscipline", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_SymbolTimekeeping",
            "SymbolTimekeeping/{action}/{id}",
            new { controller = "SymbolTimekeeping", action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
            "Staff_Shifts",
            "Shifts/{action}/{id}",
            new { controller = "Shifts", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_Timekeeping",
            "Timekeeping/{action}/{id}",
            new { controller = "Timekeeping", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_WorkSchedules",
            "WorkSchedules/{action}/{id}",
            new { controller = "WorkSchedules", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_Holidays",
            "Holidays/{action}/{id}",
            new { controller = "Holidays", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_TransferWork",
            "TransferWork/{action}/{id}",
            new { controller = "TransferWork", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_DocumentType",
            "DocumentType/{action}/{id}",
            new { controller = "DocumentType", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_DocumentAttribute",
            "DocumentAttribute/{action}/{id}",
            new { controller = "DocumentAttribute", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_DocumentField",
            "DocumentField/{action}/{id}",
            new { controller = "DocumentField", action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
            "Staff_LogDocumentAttribute",
            "LogDocumentAttribute/{action}/{id}",
            new { controller = "LogDocumentAttribute", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_LabourContract",
            "LabourContract/{action}/{id}",
            new { controller = "LabourContract", action = "Index", id = UrlParameter.Optional }
            );


            context.MapRoute(
            "Staff_LabourContractType",
            "LabourContractType/{action}/{id}",
            new { controller = "LabourContractType", action = "Index", id = UrlParameter.Optional }
            );


            context.MapRoute(
            "Staff_SalaryAdvance",
            "SalaryAdvance/{action}/{id}",
            new { controller = "SalaryAdvance", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_SalarySetting",
            "SalarySetting/{action}/{id}",
            new { controller = "SalarySetting", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_SalarySettingDetail",
            "SalarySettingDetail/{action}/{id}",
            new { controller = "SalarySettingDetail", action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
            "Staff_TimekeepingSynthesis",
            "TimekeepingSynthesis/{action}/{id}",
            new { controller = "TimekeepingSynthesis", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_SalarySettingDetail_Staff",
            "SalarySettingDetail_Staff/{action}/{id}",
            new { controller = "SalarySettingDetail_Staff", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_SalaryTable",
            "SalaryTable/{action}/{id}",
            new { controller = "SalaryTable", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_RegisterForOvertime",
            "RegisterForOvertime/{action}/{id}",
            new { controller = "RegisterForOvertime", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_SalaryTableDetail",
            "SalaryTableDetail/{action}/{id}",
            new { controller = "SalaryTableDetail", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_FPMachine",
            "FPMachine/{action}/{id}",
            new { controller = "FPMachine", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_CheckInOut",
            "CheckInOut/{action}/{id}",
            new { controller = "CheckInOut", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_TimekeepingList",
            "TimekeepingList/{action}/{id}",
            new { controller = "TimekeepingList", action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
         "Staff_Report",
         "StaffReport/{action}/{id}",
         new { controller = "StaffReport", action = "Index", id = UrlParameter.Optional }
         );
            context.MapRoute(
     "Staff_InternalNotifications",
     "InternalNotifications/{action}/{id}",
     new { controller = "InternalNotifications", action = "Index", id = UrlParameter.Optional }
     );

            context.MapRoute(
            "Staff_Staff_SocialInsurance",
            "Staff_SocialInsurance/{action}/{id}",
            new { controller = "Staff_SocialInsurance", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_StaffSocialInsurance",
            "StaffSocialInsurance/{action}/{id}",
            new { controller = "StaffSocialInsurance", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_KPILog",
            "KPILog/{action}/{id}",
            new { controller = "KPILog", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_KPILogDetail",
            "KPILogDetail/{action}/{id}",
            new { controller = "KPILogDetail", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_KPILogDetail_Item",
            "KPILogDetail_Item/{action}/{id}",
            new { controller = "KPILogDetail_Item", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_DotBCBHXH",
            "DotBCBHXH/{action}/{id}",
            new { controller = "DotBCBHXH", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_DotBCBHXHDetail",
            "DotBCBHXHDetail/{action}/{id}",
            new { controller = "DotBCBHXHDetail", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_WelfarePrograms",
            "WelfarePrograms/{action}/{id}",
            new { controller = "WelfarePrograms", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_WelfareProgramsDetail",
            "WelfareProgramsDetail/{action}/{id}",
            new { controller = "WelfareProgramsDetail", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_DotGQCDBHXH",
            "DotGQCDBHXH/{action}/{id}",
            new { controller = "DotGQCDBHXH", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_DotGQCDBHXHDetail",
            "DotGQCDBHXHDetail/{action}/{id}",
            new { controller = "DotGQCDBHXHDetail", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_TaxRate",
            "TaxRate/{action}/{id}",
            new { controller = "TaxRate", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_TaxIncomePerson",
            "TaxIncomePerson/{action}/{id}",
            new { controller = "TaxIncomePerson", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_TaxIncomePersonDetail",
            "TaxIncomePersonDetail/{action}/{id}",
            new { controller = "TaxIncomePersonDetail", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_ThuNhapChiuThue",
            "ThuNhapChiuThue/{action}/{id}",
            new { controller = "ThuNhapChiuThue", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_GiamTruThueTNCN",
            "GiamTruThueTNCN/{action}/{id}",
            new { controller = "GiamTruThueTNCN", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_Tax",
            "Tax/{action}/{id}",
            new { controller = "Tax", action = "Index", id = UrlParameter.Optional }
            );




            context.MapRoute(
            "Staff_StaffAllowance",
            "StaffAllowance/{action}/{id}",
            new { controller = "StaffAllowance", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_StaffLuongGiuHopDong",
            "StaffLuongGiuHopDong/{action}/{id}",
            new { controller = "StaffLuongGiuHopDong", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_CalendarVisitDrugStore",
            "CalendarVisitDrugStore/{action}/{id}",
            new { controller = "CalendarVisitDrugStore", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Staff_Position",
            "Position/{action}/{id}",
            new { controller = "Position", action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
          "Staff_HistoryCommissionStaff",
          "HistoryCommissionStaff/{action}/{id}",
          new { controller = "HistoryCommissionStaff", action = "Index", id = UrlParameter.Optional }
          );
            //<append_content_route_here>

            RegisterAutoMapperMap();
        }

        private static void RegisterAutoMapperMap()
        {
            AutoMapper.Mapper.CreateMap<Staffs, StaffsViewModel>();
            AutoMapper.Mapper.CreateMap<StaffsViewModel, Staffs>();
            AutoMapper.Mapper.CreateMap<vwStaffs, StaffsViewModel>();
            AutoMapper.Mapper.CreateMap<StaffsViewModel, vwStaffs>();

            AutoMapper.Mapper.CreateMap<StaffFamily, StaffFamilyViewModel>();
            AutoMapper.Mapper.CreateMap<StaffFamilyViewModel, StaffFamily>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.Branch, BranchViewModel>();
            AutoMapper.Mapper.CreateMap<BranchViewModel, Domain.Staff.Entities.Branch>();
            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.vwBranch, BranchViewModel>();
            AutoMapper.Mapper.CreateMap<BranchViewModel, Domain.Staff.Entities.vwBranch>();

            AutoMapper.Mapper.CreateMap<BranchDepartment, BranchDepartmentViewModel>();
            AutoMapper.Mapper.CreateMap<BranchDepartmentViewModel, BranchDepartment>();
            AutoMapper.Mapper.CreateMap<vwBranchDepartment, BranchDepartmentViewModel>();
            AutoMapper.Mapper.CreateMap<BranchDepartmentViewModel, vwBranchDepartment>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.KPICatalog, KPICatalogViewModel>();
            AutoMapper.Mapper.CreateMap<KPICatalogViewModel, Domain.Staff.Entities.KPICatalog>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.KPIItem, KPIItemViewModel>();
            AutoMapper.Mapper.CreateMap<KPIItemViewModel, Domain.Staff.Entities.KPIItem>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.StaffFamily, StaffFamilyViewModel>();
            AutoMapper.Mapper.CreateMap<StaffFamilyViewModel, Domain.Staff.Entities.StaffFamily>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.Bank, BankViewModel>();
            AutoMapper.Mapper.CreateMap<BankViewModel, Domain.Staff.Entities.Bank>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.Technique, TechniqueViewModel>();
            AutoMapper.Mapper.CreateMap<TechniqueViewModel, Domain.Staff.Entities.Technique>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.DayOff, DayOffViewModel>();
            AutoMapper.Mapper.CreateMap<DayOffViewModel, Domain.Staff.Entities.DayOff>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.WorkingProcess, WorkingProcessViewModel>();
            AutoMapper.Mapper.CreateMap<WorkingProcessViewModel, Domain.Staff.Entities.WorkingProcess>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.ProcessPay, ProcessPayViewModel>();
            AutoMapper.Mapper.CreateMap<ProcessPayViewModel, Domain.Staff.Entities.ProcessPay>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.BonusDiscipline, BonusDisciplineViewModel>();
            AutoMapper.Mapper.CreateMap<BonusDisciplineViewModel, Domain.Staff.Entities.BonusDiscipline>();
            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.vwBonusDiscipline, BonusDisciplineViewModel>();
            AutoMapper.Mapper.CreateMap<BonusDisciplineViewModel, Domain.Staff.Entities.vwBonusDiscipline>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.BonusDiscipline, PhatModel>();
            AutoMapper.Mapper.CreateMap<PhatModel, Domain.Staff.Entities.BonusDiscipline>();
            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.BonusDiscipline, KhenThuongModel>();
            AutoMapper.Mapper.CreateMap<KhenThuongModel, Domain.Staff.Entities.BonusDiscipline>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.SymbolTimekeeping, SymbolTimekeepingViewModel>();
            AutoMapper.Mapper.CreateMap<SymbolTimekeepingViewModel, Domain.Staff.Entities.SymbolTimekeeping>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.vwDayOff, DayOffViewModel>();
            AutoMapper.Mapper.CreateMap<DayOffViewModel, Domain.Staff.Entities.vwDayOff>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.Shifts, ShiftsViewModel>();
            AutoMapper.Mapper.CreateMap<ShiftsViewModel, Domain.Staff.Entities.Shifts>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.Timekeeping, TimekeepingViewModel>();
            AutoMapper.Mapper.CreateMap<TimekeepingViewModel, Domain.Staff.Entities.Timekeeping>();
            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.vwTimekeeping, vwTimekeepingViewModel>();
            AutoMapper.Mapper.CreateMap<vwTimekeepingViewModel, Domain.Staff.Entities.vwTimekeeping>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.WorkSchedules, WorkSchedulesViewModel>();
            AutoMapper.Mapper.CreateMap<WorkSchedulesViewModel, Domain.Staff.Entities.WorkSchedules>();
            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.vwWorkSchedules, WorkSchedulesViewModel>();
            AutoMapper.Mapper.CreateMap<WorkSchedulesViewModel, Domain.Staff.Entities.vwWorkSchedules>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.Holidays, HolidaysViewModel>();
            AutoMapper.Mapper.CreateMap<HolidaysViewModel, Domain.Staff.Entities.Holidays>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.TransferWork, TransferWorkViewModel>();
            AutoMapper.Mapper.CreateMap<TransferWorkViewModel, Domain.Staff.Entities.TransferWork>();
            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.vwTransferWork, TransferWorkViewModel>();
            AutoMapper.Mapper.CreateMap<TransferWorkViewModel, Domain.Staff.Entities.vwTransferWork>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.DocumentType, DocumentTypeViewModel>();
            AutoMapper.Mapper.CreateMap<DocumentTypeViewModel, Domain.Staff.Entities.DocumentType>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.DocumentAttribute, DocumentAttributeViewModel>();
            AutoMapper.Mapper.CreateMap<DocumentAttributeViewModel, Domain.Staff.Entities.DocumentAttribute>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.DocumentField, DocumentFieldViewModel>();
            AutoMapper.Mapper.CreateMap<DocumentFieldViewModel, Domain.Staff.Entities.DocumentField>();
            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.vwDocumentField, DocumentFieldViewModel>();
            AutoMapper.Mapper.CreateMap<DocumentFieldViewModel, Domain.Staff.Entities.vwDocumentField>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.LogDocumentAttribute, LogDocumentAttributeViewModel>();
            AutoMapper.Mapper.CreateMap<LogDocumentAttributeViewModel, Domain.Staff.Entities.LogDocumentAttribute>();
            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.vwLogDocumentAttribute, LogDocumentAttributeViewModel>();
            AutoMapper.Mapper.CreateMap<LogDocumentAttributeViewModel, Domain.Staff.Entities.vwLogDocumentAttribute>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.LabourContract, LabourContractViewModel>();
            AutoMapper.Mapper.CreateMap<LabourContractViewModel, Domain.Staff.Entities.LabourContract>();
            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.vwLabourContract, LabourContractViewModel>();
            AutoMapper.Mapper.CreateMap<LabourContractViewModel, Domain.Staff.Entities.vwLabourContract>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.LabourContractType, LabourContractTypeViewModel>();
            AutoMapper.Mapper.CreateMap<LabourContractTypeViewModel, Domain.Staff.Entities.LabourContractType>();



            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.NotificationsDetail, NotificationsDetailViewModel>();
            AutoMapper.Mapper.CreateMap<NotificationsDetailViewModel, Domain.Staff.Entities.NotificationsDetail>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.SalaryAdvance, SalaryAdvanceViewModel>();
            AutoMapper.Mapper.CreateMap<SalaryAdvanceViewModel, Domain.Staff.Entities.SalaryAdvance>();
            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.vwSalaryAdvance, SalaryAdvanceViewModel>();
            AutoMapper.Mapper.CreateMap<SalaryAdvanceViewModel, Domain.Staff.Entities.vwSalaryAdvance>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.vwTotalTimekeeping, TimekeepingSynthesisViewModel>();
            AutoMapper.Mapper.CreateMap<TimekeepingSynthesisViewModel, Domain.Staff.Entities.vwTotalTimekeeping>();
            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.SalarySetting, SalarySettingViewModel>();
            AutoMapper.Mapper.CreateMap<SalarySettingViewModel, Domain.Staff.Entities.SalarySetting>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.SalarySettingDetail, SalarySettingDetailViewModel>();
            AutoMapper.Mapper.CreateMap<SalarySettingDetailViewModel, Domain.Staff.Entities.SalarySettingDetail>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.TimekeepingSynthesis, TimekeepingSynthesisViewModel>();
            AutoMapper.Mapper.CreateMap<TimekeepingSynthesisViewModel, Domain.Staff.Entities.TimekeepingSynthesis>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.SalarySettingDetail_Staff, SalarySettingDetail_StaffViewModel>();
            AutoMapper.Mapper.CreateMap<SalarySettingDetail_StaffViewModel, Domain.Staff.Entities.SalarySettingDetail_Staff>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.SalaryTable, SalaryTableViewModel>();
            AutoMapper.Mapper.CreateMap<SalaryTableViewModel, Domain.Staff.Entities.SalaryTable>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.RegisterForOvertime, RegisterForOvertimeViewModel>();
            AutoMapper.Mapper.CreateMap<RegisterForOvertimeViewModel, Domain.Staff.Entities.RegisterForOvertime>();
            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.vwRegisterForOvertime, RegisterForOvertimeViewModel>();
            AutoMapper.Mapper.CreateMap<RegisterForOvertimeViewModel, Domain.Staff.Entities.vwRegisterForOvertime>();
            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.FPMachine, FPMachineViewModel>();
            AutoMapper.Mapper.CreateMap<FPMachineViewModel, Domain.Staff.Entities.FPMachine>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.CheckInOut, CheckInOutViewModel>();
            AutoMapper.Mapper.CreateMap<CheckInOutViewModel, Domain.Staff.Entities.CheckInOut>();
            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.vwCheckInOut, CheckInOutViewModel>();
            AutoMapper.Mapper.CreateMap<CheckInOutViewModel, Domain.Staff.Entities.vwCheckInOut>();
            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.TimekeepingList, TimekeepingListViewModel>();
            AutoMapper.Mapper.CreateMap<TimekeepingListViewModel, Domain.Staff.Entities.TimekeepingList>();
            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.vwTimekeepingList, TimekeepingListViewModel>();
            AutoMapper.Mapper.CreateMap<TimekeepingListViewModel, Domain.Staff.Entities.vwTimekeepingList>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.InternalNotifications, InternalNotificationsViewModel>();
            AutoMapper.Mapper.CreateMap<InternalNotificationsViewModel, Domain.Staff.Entities.InternalNotifications>();
            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.vwInternalNotifications, InternalNotificationsViewModel>();
            AutoMapper.Mapper.CreateMap<InternalNotificationsViewModel, Domain.Staff.Entities.vwInternalNotifications>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.StaffSocialInsurance, StaffSocialInsuranceViewModel>();
            AutoMapper.Mapper.CreateMap<StaffSocialInsuranceViewModel, Domain.Staff.Entities.StaffSocialInsurance>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.vwStaffSocialInsurance, StaffSocialInsuranceViewModel>();
            AutoMapper.Mapper.CreateMap<StaffSocialInsuranceViewModel, Domain.Staff.Entities.vwStaffSocialInsurance>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.KPILog, KPILogViewModel>();
            AutoMapper.Mapper.CreateMap<KPILogViewModel, Domain.Staff.Entities.KPILog>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.KPILogDetail, KPILogDetailViewModel>();
            AutoMapper.Mapper.CreateMap<KPILogDetailViewModel, Domain.Staff.Entities.KPILogDetail>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.KPILogDetail_Item, KPILogDetail_ItemViewModel>();
            AutoMapper.Mapper.CreateMap<KPILogDetail_ItemViewModel, Domain.Staff.Entities.KPILogDetail_Item>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.KPIItem, Domain.Staff.Entities.KPILogDetail_Item>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.DotBCBHXH, DotBCBHXHViewModel>();
            AutoMapper.Mapper.CreateMap<DotBCBHXHViewModel, Domain.Staff.Entities.DotBCBHXH>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.DotBCBHXHDetail, DotBCBHXHDetailViewModel>();
            AutoMapper.Mapper.CreateMap<DotBCBHXHDetailViewModel, Domain.Staff.Entities.DotBCBHXHDetail>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.vwDotBCBHXHDetail, DotBCBHXHDetailViewModel>();
            AutoMapper.Mapper.CreateMap<DotBCBHXHDetailViewModel, Domain.Staff.Entities.vwDotBCBHXHDetail>();
            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.WelfarePrograms, WelfareProgramsViewModel>();
            AutoMapper.Mapper.CreateMap<WelfareProgramsViewModel, Domain.Staff.Entities.WelfarePrograms>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.WelfareProgramsDetail, WelfareProgramsDetailViewModel>();
            AutoMapper.Mapper.CreateMap<WelfareProgramsDetailViewModel, Domain.Staff.Entities.WelfareProgramsDetail>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.DotGQCDBHXH, DotGQCDBHXHViewModel>();
            AutoMapper.Mapper.CreateMap<DotGQCDBHXHViewModel, Domain.Staff.Entities.DotGQCDBHXH>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.DotGQCDBHXHDetail, DotGQCDBHXHDetailViewModel>();
            AutoMapper.Mapper.CreateMap<DotGQCDBHXHDetailViewModel, Domain.Staff.Entities.DotGQCDBHXHDetail>();
            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.vwDotGQCDBHXHDetail, DotGQCDBHXHDetailViewModel>();
            AutoMapper.Mapper.CreateMap<DotGQCDBHXHDetailViewModel, Domain.Staff.Entities.vwDotGQCDBHXHDetail>();
            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.TaxRate, TaxRateViewModel>();
            AutoMapper.Mapper.CreateMap<TaxRateViewModel, Domain.Staff.Entities.TaxRate>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.TaxIncomePerson, TaxIncomePersonViewModel>();
            AutoMapper.Mapper.CreateMap<TaxIncomePersonViewModel, Domain.Staff.Entities.TaxIncomePerson>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.TaxIncomePersonDetail, TaxIncomePersonDetailViewModel>();
            AutoMapper.Mapper.CreateMap<TaxIncomePersonDetailViewModel, Domain.Staff.Entities.TaxIncomePersonDetail>();
            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.vwTaxIncomePersonDetail, TaxIncomePersonDetailViewModel>();
            AutoMapper.Mapper.CreateMap<TaxIncomePersonDetailViewModel, Domain.Staff.Entities.vwTaxIncomePersonDetail>();


            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.ThuNhapChiuThue, ThuNhapChiuThueViewModel>();
            AutoMapper.Mapper.CreateMap<ThuNhapChiuThueViewModel, Domain.Staff.Entities.ThuNhapChiuThue>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.GiamTruThueTNCN, GiamTruThueTNCNViewModel>();
            AutoMapper.Mapper.CreateMap<GiamTruThueTNCNViewModel, Domain.Staff.Entities.GiamTruThueTNCN>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.Tax, TaxViewModel>();
            AutoMapper.Mapper.CreateMap<TaxViewModel, Domain.Staff.Entities.Tax>();



            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.StaffAllowance, StaffAllowanceViewModel>();
            AutoMapper.Mapper.CreateMap<StaffAllowanceViewModel, Domain.Staff.Entities.StaffAllowance>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.StaffLuongGiuHopDong, StaffLuongGiuHopDongViewModel>();
            AutoMapper.Mapper.CreateMap<StaffLuongGiuHopDongViewModel, Domain.Staff.Entities.StaffLuongGiuHopDong>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.CalendarVisitDrugStore, CalendarVisitDrugStoreViewModel>();
            AutoMapper.Mapper.CreateMap<CalendarVisitDrugStoreViewModel, Domain.Staff.Entities.CalendarVisitDrugStore>();
            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.vwCalendarVisitDrugStore, CalendarVisitDrugStoreViewModel>();
            AutoMapper.Mapper.CreateMap<CalendarVisitDrugStoreViewModel, Domain.Staff.Entities.vwCalendarVisitDrugStore>();

            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.Position, PositionViewModel>();
            AutoMapper.Mapper.CreateMap<PositionViewModel, Domain.Staff.Entities.Position>();
            AutoMapper.Mapper.CreateMap<Domain.Staff.Entities.HistoryCommissionStaff, HistoryCommissionStaffViewModel>();
            AutoMapper.Mapper.CreateMap<HistoryCommissionStaffViewModel, Domain.Staff.Entities.HistoryCommissionStaff>();
            //<append_content_mapper_here>
        }
    }
}
