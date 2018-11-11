using System.Globalization;
using Erp.BackOffice.Filters;
using Erp.Domain.Interfaces;
using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Erp.Utilities;
using WebMatrix.WebData;
using Erp.BackOffice.Staff.Models;
using Erp.BackOffice.Helpers;
using Erp.Domain.Helper;
using Newtonsoft.Json;
using Erp.Domain.Account.Interfaces;
using Erp.BackOffice.Account.Models;
using System.Data;

namespace Erp.BackOffice.Staff.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class StaffReportController : Controller
    {
        private readonly ISalaryAdvanceRepository SalaryAdvanceRepository;
        private readonly IBranchRepository BranchRepository;
        private readonly IUserRepository userRepository;
        private readonly IBranchDepartmentRepository branchDepartmentRepository;
        private readonly IStaffsRepository staffRepository;
        private readonly IStaffReportRepository staffReportRepository;
        private readonly IDayOffRepository dayoffRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly IQueryHelper QueryHelper;
        private readonly ISymbolTimekeepingRepository symboltimekeepingRepository;
        private readonly ITransferWorkRepository TransferWorkRepository;
        private readonly IPaymentRepository paymentRepository;
        public StaffReportController(
            IBranchRepository _Branch
            , IUserRepository _user
            , IBranchDepartmentRepository branchDepartment
            , IStaffsRepository staff
            , IStaffReportRepository staffReport
            , IQueryHelper _QueryHelper
             , ICustomerRepository _Customer
            , IDayOffRepository dayoff
            , ISymbolTimekeepingRepository symboltimekeeping
            , ITransferWorkRepository _TransferWork
            ,  ISalaryAdvanceRepository _SalaryAdvance
            ,IPaymentRepository payment
            )
        {
            BranchRepository = _Branch;
            userRepository = _user;
            branchDepartmentRepository = branchDepartment;
            staffRepository = staff;
            staffReportRepository = staffReport;
            customerRepository = _Customer;
            QueryHelper = _QueryHelper;
            dayoffRepository = dayoff;
            symboltimekeepingRepository = symboltimekeeping;
            TransferWorkRepository = _TransferWork;
           SalaryAdvanceRepository = _SalaryAdvance;
            paymentRepository = payment;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Summary(int? year, int? month, int? branchId, int? DepartmentId)
        {
            IEnumerable<SymbolTimekeepingViewModel> model = symboltimekeepingRepository.GetAllSymbolTimekeeping().ToList().Where(x => x.DayOff == true)
                .Select(item => new SymbolTimekeepingViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Quantity = item.Quantity,
                    Timekeeping = item.Timekeeping,
                    DayOff = item.DayOff,
                    Color = item.Color,
                    CodeDefault = item.CodeDefault,
                    Absent = item.Absent,
                    Icon = item.Icon
                }).ToList();
            year = year == null ? DateTime.Now.Year : year;
            month = month == null ? DateTime.Now.Month : month;
            var user = userRepository.GetUserById(WebSecurity.CurrentUserId);
            //if (user.UserTypeId != 1)
            //{
            //    branchId = branchId == null ? Helpers.Common.CurrentUser.BranchId.Value : branchId;
            //}
            //DateTime StartDate = DateTime.Now;
            //DateTime EndDate = DateTime.Now;
            DateTime aDateTime = new DateTime(year.Value, month.Value, 1);
            // Cộng thêm 1 tháng và trừ đi một ngày.
            DateTime retDateTime = aDateTime.AddMonths(1).AddDays(-1);
            ViewBag.DateRangeText = "Tháng " + month + " năm " + year;
            ViewBag.aDateTime = aDateTime.ToShortDateString();
            ViewBag.retDateTime = retDateTime.ToShortDateString();
            //ViewBag.BranchId = branchId;
            //ViewBag.DepartmentId = DepartmentId;
            //var qThongKeNghiPhep = dayoffRepository.GetAllvwDayOff()
            //        .Where(x => aDateTime <= x.DayStart && x.DayEnd <= retDateTime).ToList();

            //if (branchId != null && branchId.Value > 0)
            //{
            //    qThongKeNghiPhep = qThongKeNghiPhep.Where(item => item.Sale_BranchId == branchId).ToList();
            //}
            //if (DepartmentId != null && DepartmentId.Value > 0)
            //{
            //    qThongKeNghiPhep = qThongKeNghiPhep.Where(item => item.BranchDepartmentId == DepartmentId).ToList();
            //}
            //ViewBag.qThongKeNghiPhep = qThongKeNghiPhep;
            return View(model);
        }

        #region TransferWork
        public ViewResult TransferWork()
        {
            IEnumerable<TransferWorkViewModel> q = TransferWorkRepository.GetvwAllTransferWork()
                .Select(item => new TransferWorkViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    BranchDepartmentOldId = item.BranchDepartmentOldId,
                    BranchDepartmentNewId = item.BranchDepartmentNewId,
                    BranchNameNew = item.BranchNameNew,
                    BranchNameOld = item.BranchNameOld,
                    Code = item.Code,
                    CodeStaff = item.CodeStaff,
                    DayDecision = item.DayDecision,
                    DayEffective = item.DayEffective,
                    NameStaff = item.NameStaff,
                    NameUser = item.NameUser,
                    PositionNew = item.PositionNew,
                    PositionOld = item.PositionOld,
                    Reason = item.Reason,
                    Staff_DepartmentNew = item.Staff_DepartmentNew,
                    Staff_DepartmentOld = item.Staff_DepartmentOld,
                    StaffId = item.StaffId,
                    Status = item.Status,
                    UserId = item.UserId,
                    Birthday = item.Birthday,
                    Gender = item.Gender,
                    CodeName = item.CodeName,
                    BranchIdNew = item.BranchIdNew,
                    BranchIdOld = item.BranchIdOld,
                    ProfileImage = item.ProfileImage,
                    CodeStaffNew = item.CodeStaffNew,
                    CodeStaffOld = item.CodeStaffOld
                }).OrderByDescending(x => x.CreatedDate).Take(10).ToList();
            return View(q);
        }
        #endregion

        #region SalaryAdvance
        public ViewResult SalaryAdvance(int? branchId, int? DepartmentId)
        {
            IEnumerable<SalaryAdvanceViewModel> q = SalaryAdvanceRepository.GetAllvwSalaryAdvance()
                .Select(item => new SalaryAdvanceViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    CreatedDate = item.CreatedDate,
                    CodeAdvance = item.CodeAdvance,
                    Pay = item.Pay,
                    StaffId = item.StaffId,
                    Status = item.Status,
                    Name = item.Name,
                    BranchDepartmentId = item.BranchDepartmentId,
                    BranchName = item.BranchName,
                    Sale_BranchId = item.Sale_BranchId,
                    Staff_DepartmentId = item.Staff_DepartmentId
                }).ToList();
            if (branchId != null && branchId.Value > 0)
            {
                q = q.Where(item => item.Sale_BranchId == branchId).ToList();
            }
            if (DepartmentId != null && DepartmentId.Value > 0)
            {
                q = q.Where(item => item.BranchDepartmentId == DepartmentId).ToList();
            }
            q = q.OrderByDescending(x => x.CreatedDate).Take(10).ToList();
            return View(q);
        }
        #endregion

        public ActionResult ChartSalaryInMonth(int? month_chart_salary, int? year_chart_salary)
        {
            year_chart_salary = year_chart_salary == null ? DateTime.Now.Year : year_chart_salary;
            month_chart_salary = month_chart_salary == null ? DateTime.Now.Month : month_chart_salary;
            var jsonData = new List<Erp.BackOffice.Staff.Models.spChartSalaryInMonth>();


            var data = SqlHelper.QuerySP<Erp.BackOffice.Staff.Models.spChartSalaryInMonth>("spChartSalaryInMonth", new
                 {
                     Year = year_chart_salary,
                     Month = month_chart_salary
                 });

                foreach (var item in data)
                {
                    var obj = item;
                    jsonData.Add(obj);
                }

             ViewBag.Total_Du_toan = data.Sum(x => x.TotalSalary);
             ViewBag.Date = "tháng " + month_chart_salary + " năm " + year_chart_salary;
            var name = "Chi trả lương tháng " + month_chart_salary + "/" + year_chart_salary;
            var total_quyet_toan = paymentRepository.GetAllvwPayment().Where(x => x.IsArchive == true && x.Name == name).Sum(x => x.Amount);
            if (total_quyet_toan == null)
            {
                total_quyet_toan = 0;
            }
            ViewBag.total_quyet_toan = total_quyet_toan;
            string json = JsonConvert.SerializeObject(data);
            ViewBag.json = json;

            return View();
        }

        public ActionResult SummaryStaff(string single, int? year, int? month, int? quarter, int? week, string CityId, string DistrictId, string branchId,int? TDVId)
        {
            year = year == null ? DateTime.Now.Year : year;
            month = month == null ? DateTime.Now.Month : month;
            single = single == null ? "month" : single;
            quarter = quarter == null ? 1 : quarter;
            CityId = string.IsNullOrEmpty(CityId) ? "" : CityId;
            DistrictId = string.IsNullOrEmpty(DistrictId) ? "" : DistrictId;
            branchId = branchId == null ? "" : branchId;
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;
            var weekdefault = calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            week = week == null ? weekdefault : week;
            if (!Filters.SecurityFilter.IsAdmin() && !Filters.SecurityFilter.IsKeToan() && string.IsNullOrEmpty(branchId))
            {
                branchId = Helpers.Common.CurrentUser.DrugStore;
            }
            if (TDVId != null && TDVId.Value > 0)
            {
                var staff = staffRepository.GetStaffsById(TDVId.Value);
                if (staff != null)
                {
                    branchId = staff.DrugStore;
                }
            }
            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;

            ViewBag.DateRangeText = Helpers.Common.ConvertToDateRange(ref StartDate, ref EndDate, single, year.Value, month.Value, quarter.Value, ref week);
            var data = new List<SummaryStaffViewModel>();
            var title = "";
            if (Erp.BackOffice.Filters.SecurityFilter.IsAdminDrugStore())
            {
                data = SqlHelper.QuerySP<SummaryStaffViewModel>("spSale_SummaryStaff", new
                {
                    StartDate = StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    EndDate = EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    branchId = branchId,
                    CityId = CityId,
                    DistrictId = DistrictId
                }).ToList();
                title = "Thống kê doanh số theo nhân viên nhà thuốc";
            }
            else
            {
                data = SqlHelper.QuerySP<SummaryStaffViewModel>("spSale_SummaryDrugStore", new
                {
                    StartDate = StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    EndDate = EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    branchId = branchId,
                    CityId = CityId,
                    DistrictId = DistrictId
                }).ToList();
                title = "Thống kê doanh số theo Nhà thuốc";
            }
            //if (branchId != null && branchId.Value > 0)
            //{
            //    data = SqlHelper.QuerySP<SummaryStaffViewModel>("spSale_SummaryStaff", new
            //    {
            //        StartDate = StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
            //        EndDate = EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
            //        branchId = branchId,
            //        CityId = CityId,
            //        DistrictId = DistrictId
            //    }).ToList();
            //    title = "Thống kê doanh số theo nhân viên nhà thuốc";
            //}
            //else
            //{
            //    if (string.IsNullOrEmpty(DistrictId))
            //    {
            //        if (string.IsNullOrEmpty(CityId))
            //        {
            //            data = SqlHelper.QuerySP<SummaryStaffViewModel>("spSale_SummaryCityId", new
            //            {
            //                StartDate = StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
            //                EndDate = EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
            //                branchId = branchId,
            //                CityId = CityId,
            //                DistrictId = DistrictId
            //            }).ToList();
            //            title = "Thống kê doanh số theo Tỉnh/Thành phố";

            //        }
            //        else
            //        {
            //            data = SqlHelper.QuerySP<SummaryStaffViewModel>("spSale_SummaryDistrictId", new
            //            {
            //                StartDate = StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
            //                EndDate = EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
            //                branchId = branchId,
            //                CityId = CityId,
            //                DistrictId = DistrictId
            //            }).ToList();
            //            title = "Thống kê doanh số theo Quận/Huyện";
            //        }
            //    }
            //    else
            //    {
            //        data = SqlHelper.QuerySP<SummaryStaffViewModel>("spSale_SummaryDrugStore", new
            //        {
            //            StartDate = StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
            //            EndDate = EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
            //            branchId = branchId,
            //            CityId = CityId,
            //            DistrictId = DistrictId
            //        }).ToList();
            //        title = "Thống kê doanh số theo Nhà thuốc";
            //    }
            //}
            ViewBag.single = single;
            ViewBag.year = year;
            ViewBag.month = month;
            ViewBag.quarter = quarter;
            ViewBag.week = week;
            ViewBag.branchId = branchId;
            ViewBag.DistrictId = DistrictId;
            ViewBag.CityId = CityId;
            ViewBag.Title_qThongKeBanHang_TheoKhachHang = title;
            return View(data);
        }
    }
}
