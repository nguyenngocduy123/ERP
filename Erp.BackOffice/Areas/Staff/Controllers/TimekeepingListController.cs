using System.Globalization;
using Erp.BackOffice.Staff.Models;
using Erp.BackOffice.Filters;
using Erp.BackOffice.Helpers;
using Erp.Domain.Entities;
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
using System.IO;

namespace Erp.BackOffice.Staff.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class TimekeepingListController : Controller
    {
        private readonly ITimekeepingListRepository TimekeepingListRepository;
        private readonly IUserRepository userRepository;
        private readonly ITimekeepingSynthesisRepository timekeepingSynthesisRepository;
        private readonly IWorkSchedulesRepository WorkSchedulesRepository;
        private readonly IStaffsRepository StaffsRepository;
        private readonly IShiftsRepository shiftsRepository;
        private readonly ILocationRepository locationRepository;
        private readonly IHolidaysRepository holidayRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly ITimekeepingRepository timekeepingRepository;
        private readonly IDayOffRepository dayoffRepository;
        private readonly IBranchDepartmentRepository departmentRepository;
        private readonly IBranchRepository branchRepository;
        public TimekeepingListController(
            ITimekeepingListRepository _TimekeepingList
            , IUserRepository _user
            , IWorkSchedulesRepository _WorkSchedules
            , ITimekeepingSynthesisRepository timekeepingSynthesis
            , IStaffsRepository staff
            , IShiftsRepository shifts
            , ILocationRepository location
            , IHolidaysRepository holiday
            , ICategoryRepository category
            , ITimekeepingRepository timekeeping
            , IDayOffRepository dayoff
            , IBranchDepartmentRepository department
            ,IBranchRepository branch
            )
        {
            TimekeepingListRepository = _TimekeepingList;
            userRepository = _user;
            departmentRepository = department;
            WorkSchedulesRepository = _WorkSchedules;
            StaffsRepository = staff;
            shiftsRepository = shifts;
            locationRepository = location;
            holidayRepository = holiday;
            categoryRepository = category;
            timekeepingRepository = timekeeping;
            dayoffRepository = dayoff;
            timekeepingSynthesisRepository = timekeepingSynthesis;
            branchRepository=branch;
        }

        #region Index

        public ViewResult Index(int? branchId, int? DepartmentId, int? month, int? year, string Code, string status, string CategoryShifts)
        {

            IEnumerable<TimekeepingListViewModel> q = TimekeepingListRepository.GetAllvwTimekeepingList()
                .Select(item => new TimekeepingListViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    BranchName = item.BranchName,
                    CategoryShifts = item.CategoryShifts,
                    DepartmentId = item.DepartmentId,
                    DepartmentName = item.DepartmentName,
                    Month = item.Month,
                    Year = item.Year,
                    Sale_BranchId = item.Sale_BranchId,
                    Code = item.Code,
                    Status = item.Status
                }).OrderByDescending(m => m.ModifiedDate);
            if (branchId != null && branchId.Value > 0)
            {
                q = q.Where(item => item.Sale_BranchId == branchId);
            }
            if (DepartmentId != null && DepartmentId.Value > 0)
            {
                q = q.Where(item => item.DepartmentId == DepartmentId);
            }
            if (month != null && month.Value > 0)
            {
                q = q.Where(item => item.Month == month);
            }
            if (year != null && year.Value > 0)
            {
                q = q.Where(item => item.Year == year);
            }
            if (!string.IsNullOrEmpty(Code))
            {
                q = q.Where(item => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.Code).ToLower().Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(Code).ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(status))
            {
                q = q.Where(item => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.Status).ToLower().Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(status).ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(CategoryShifts))
            {
                q = q.Where(item => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.CategoryShifts).ToLower().Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(CategoryShifts).ToLower())).ToList();
            }
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        #endregion

        #region Create
        public ViewResult Create()
        {
            var model = new TimekeepingListViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(TimekeepingListViewModel model)
        {
            if (ModelState.IsValid)
            {
                var TimekeepingList = new TimekeepingList();
                AutoMapper.Mapper.Map(model, TimekeepingList);
                TimekeepingList.IsDeleted = false;
                TimekeepingList.CreatedUserId = WebSecurity.CurrentUserId;
                TimekeepingList.ModifiedUserId = WebSecurity.CurrentUserId;
                TimekeepingList.AssignedUserId = WebSecurity.CurrentUserId;
                TimekeepingList.CreatedDate = DateTime.Now;
                TimekeepingList.ModifiedDate = DateTime.Now;
                TimekeepingList.CheckSalary = false;
                if (model.Sale_BranchId != null)
                {
                    if (model.DepartmentId != null)
                    {
                        var department = departmentRepository.GetvwBranchDepartmentById(model.DepartmentId.Value);
                        TimekeepingList.Name = "Danh sách chấm công tháng " + model.Month + " năm " + model.Year + " - " + department.Staff_DepartmentId + " - " + department.BranchName;
                    }
                    else
                    {
                        var branch = branchRepository.GetBranchById(model.Sale_BranchId.Value);
                        TimekeepingList.Name = "Danh sách chấm công tháng " + model.Month + " năm " + model.Year + " - " + branch.Name;
                    }
                }
                else
                {
                    TimekeepingList.Name = "Danh sách chấm công tháng " + model.Month + " năm " + model.Year + " - Tất cả chi nhánh";
                }

                //mặc định khởi tạo danh sách thì trạng thái là pending... nếu có thêm phân công tự động thì là assigned
                if (model.CategoryShifts == "Full-time")
                {
                    TimekeepingList.Status = "timekeeping";
                }
                else
                {
                    TimekeepingList.Status = "assign";
                }
                TimekeepingListRepository.InsertTimekeepingList(TimekeepingList);
                var prefix2 = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_timekeepingList");
                TimekeepingList.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix2, TimekeepingList.Id);
                TimekeepingListRepository.UpdateTimekeepingList(TimekeepingList);
                //nếu danh sách khởi tạo chấm công là Toàn thời gian thì tự động tạo phân công cho tất cả nhân viên trong phòng ban
                if (model.CategoryShifts == "Full-time")
                {
                    //chuẩn bị dữ liệu ngày nghỉ để duyệt ngày nghỉ thì không phân công.
                    var DayOff = categoryRepository.GetCategoryByCode("DayOffDefault").Where(x => x.Value == "True").AsEnumerable().ToList();
                    //lấy ca làm việc toàn thời gian ra.
                    var shift = shiftsRepository.GetAllShifts().Where(x => x.CategoryShifts == "Full-time").OrderByDescending(x => x.CreatedDate).FirstOrDefault();
                    //lấy danh sách nhân viên của phòng ban để phân công.
                    List<vwStaffs> list_staff_insert = new List<vwStaffs>();
                    var staff = StaffsRepository.GetvwAllStaffs().Where(x=>!string.IsNullOrEmpty(x.BranchName)&&x.IsWorking==true);
                    if (model.Sale_BranchId != null)
                    {
                        if (model.DepartmentId != null)
                        {
                            list_staff_insert = staff.Where(x => x.BranchDepartmentId == model.DepartmentId).ToList();
                        }
                        else
                        {
                            list_staff_insert = staff.Where(x => x.Sale_BranchId == model.Sale_BranchId).ToList();
                        }
                    }
                    else
                    {
                        list_staff_insert = staff.OrderBy(x=>x.Id).ToList();
                    }
                    //dựa vào tháng năm của danh sách khởi tạo ở trên, tạo ra list ngày trong tháng.
                    DateTime aDateTime = new DateTime(TimekeepingList.Year.Value, TimekeepingList.Month.Value, 1);
                    // Cộng thêm 1 tháng và trừ đi một ngày.
                    DateTime retDateTime = aDateTime.AddMonths(1).AddDays(-1);
                    //phần duyệt ngày trong tháng để thêm phân công cho từng nhân viên.
                    for (DateTime dt = aDateTime; dt <= retDateTime; dt = dt.AddDays(1))
                    {
                        if (DayOff.Where(x => Convert.ToInt32(x.OrderNo) == (int)dt.DayOfWeek && x.Value == "True").Count() <= 0)
                        {
                            foreach (var i in list_staff_insert)
                            {
                                var item = new WorkSchedules();
                                item.CreatedUserId = WebSecurity.CurrentUserId;
                                item.ModifiedUserId = WebSecurity.CurrentUserId;
                                item.CreatedDate = DateTime.Now;
                                item.ModifiedDate = DateTime.Now;
                                item.IsDeleted = false;
                                item.StaffId = i.Id;
                                item.Day = dt;
                                item.ShiftsId = shift.Id;
                                item.TimekeepingListId = TimekeepingList.Id;
                                WorkSchedulesRepository.InsertWorkSchedules(item);
                            }
                        }
                    }
                }
                //TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Detail", "TimekeepingList", new { area = "Staff", Id = TimekeepingList.Id });
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var TimekeepingList = TimekeepingListRepository.GetvwTimekeepingListById(Id.Value);
            if (TimekeepingList != null && TimekeepingList.IsDeleted != true)
            {
                var model = new TimekeepingListViewModel();
                AutoMapper.Mapper.Map(TimekeepingList, model);

                //if (model.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                //{
                //    TempData["FailedMessage"] = "NotOwner";
                //    return RedirectToAction("Index");
                //}                

                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(TimekeepingListViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var TimekeepingList = TimekeepingListRepository.GetTimekeepingListById(model.Id);
                    //nếu danh sách chấm công sửa từ part-time sang full-time thì tự động phân công ca làm việc.
                    if (TimekeepingList.CategoryShifts == "Part-time" && model.CategoryShifts == "Full-time")
                    {
                        //nếu danh sách khởi tạo chấm công là Toàn thời gian thì tự động tạo phân công cho tất cả nhân viên trong phòng ban
                        if (model.CategoryShifts == "Full-time")
                        {
                            //chuẩn bị dữ liệu ngày nghỉ để duyệt ngày nghỉ thì không phân công.
                            var DayOff = categoryRepository.GetCategoryByCode("DayOffDefault").Where(x => x.Value == "True").AsEnumerable().ToList();
                            //lấy ca làm việc toàn thời gian ra.
                            var shift = shiftsRepository.GetAllShifts().Where(x => x.CategoryShifts == "Full-time").OrderByDescending(x => x.CreatedDate).FirstOrDefault();
                            //lấy danh sách nhân viên của phòng ban để phân công.
                            var staff = StaffsRepository.GetAllStaffs().Where(x => x.BranchDepartmentId == TimekeepingList.DepartmentId).ToList();
                            //dựa vào tháng năm của danh sách khởi tạo ở trên, tạo ra list ngày trong tháng.
                            DateTime aDateTime = new DateTime(TimekeepingList.Year.Value, TimekeepingList.Month.Value, 1);
                            // Cộng thêm 1 tháng và trừ đi một ngày.
                            DateTime retDateTime = aDateTime.AddMonths(1).AddDays(-1);
                            //phần duyệt ngày trong tháng để thêm phân công cho từng nhân viên.
                            for (DateTime dt = aDateTime; dt <= retDateTime; dt = dt.AddDays(1))
                            {
                                if (DayOff.Where(x => Convert.ToInt32(x.OrderNo) == (int)dt.DayOfWeek && x.Value == "True").Count() <= 0)
                                {
                                    foreach (var i in staff)
                                    {
                                        var item = new WorkSchedules();
                                        item.CreatedUserId = WebSecurity.CurrentUserId;
                                        item.ModifiedUserId = WebSecurity.CurrentUserId;
                                        item.CreatedDate = DateTime.Now;
                                        item.ModifiedDate = DateTime.Now;
                                        item.IsDeleted = false;
                                        item.StaffId = i.Id;
                                        item.Day = dt;
                                        item.ShiftsId = shift.Id;
                                        item.TimekeepingListId = TimekeepingList.Id;
                                        WorkSchedulesRepository.InsertWorkSchedules(item);
                                    }
                                }
                            }
                        }
                    }
                    AutoMapper.Mapper.Map(model, TimekeepingList);
                    TimekeepingList.ModifiedUserId = WebSecurity.CurrentUserId;
                    TimekeepingList.ModifiedDate = DateTime.Now;
                    //if (model.CategoryShifts == "Full-time")
                    //{
                    //    TimekeepingList.Status = "assigned";
                    //}
                    var department = departmentRepository.GetvwBranchDepartmentById(model.DepartmentId.Value);
                    TimekeepingList.Name = "Danh sách chấm công tháng " + model.Month + " năm " + model.Year + " - " + department.Staff_DepartmentId + " - " + department.BranchName;
                    TimekeepingListRepository.UpdateTimekeepingList(TimekeepingList);
                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                    return RedirectToAction("Index");
                }

                return View(model);
            }

            return View(model);

            //if (Request.UrlReferrer != null)
            //    return Redirect(Request.UrlReferrer.AbsoluteUri);
            //return RedirectToAction("Index");
        }

        #endregion

        #region CreateTimekeeping
        public static void CreateTimekeeping(int? Year, int? Month, int? Id)
        {
            Erp.Domain.Staff.Repositories.HolidaysRepository holidayRepository = new Erp.Domain.Staff.Repositories.HolidaysRepository(new Domain.Staff.ErpStaffDbContext());
            Erp.Domain.Staff.Repositories.TimekeepingSynthesisRepository timekeepingSynthesisRepository = new Erp.Domain.Staff.Repositories.TimekeepingSynthesisRepository(new Domain.Staff.ErpStaffDbContext());
            Erp.Domain.Repositories.CategoryRepository categoryRepository = new Erp.Domain.Repositories.CategoryRepository(new Domain.ErpDbContext());
            Erp.Domain.Staff.Repositories.WorkSchedulesRepository workSchedulesRepository = new Erp.Domain.Staff.Repositories.WorkSchedulesRepository(new Domain.Staff.ErpStaffDbContext());

            //lấy danh sách chấm công từng ngày trong tháng
            var listTinmeKeeping = workSchedulesRepository.GetvwAllWorkSchedules().Where(x => x.TimekeepingListId.Value == Id).ToList();
            var timekeepingList = listTinmeKeeping.Select(i => new WorkSchedulesViewModel
            {
                BranchDepartmentId = i.BranchDepartmentId,
                Code = i.Code,
                CodeShifts = i.CodeShifts,
                Color = i.Color,
                Day = i.Day,
                DayOff = i.DayOff,
                DayOffCode = i.DayOffCode,
                DayOffName = i.DayOffName,
                EndTime = i.EndTime,
                EndTimeIn = i.EndTimeIn,
                Id = i.Id,
                HoursIn = i.HoursIn,
                HoursOut = i.HoursOut,
                Month = i.Month,
                Name = i.Name,
                NameShifts = i.NameShifts,
                Sale_BranchId = i.Sale_BranchId,
                ShiftsId = i.ShiftsId,
                StaffId = i.StaffId,
                StartTime = i.StartTime,
                StartTimeOut = i.StartTimeOut,
                Symbol = i.Symbol,
                Timekeeping = i.Timekeeping,
                Total_minute_work = i.Total_minute_work,
                Total_minute_work_early = i.Total_minute_work_early,
                Total_minute_work_late = i.Total_minute_work_late,
                Total_minute_work_overtime = i.Total_minute_work_overtime,
                UserEnrollNumber = i.UserEnrollNumber,
                Year = i.Year,
                TimekeepingListId = i.TimekeepingListId,
                Absent=i.Absent
            }).OrderBy(x => x.ShiftsId).ToList();
            //lấy danh sách nhân viên trong phòng ban, nên lấy theo danh sách phân công để chính xác
            var StaffList = timekeepingList
          .GroupBy(x => new { x.StaffId }, (key, group) => new WorkSchedulesViewModel
          {
              StaffId = key.StaffId,
              Code = group.FirstOrDefault().Code,
              Name = group.FirstOrDefault().Name,
              Id = group.FirstOrDefault().Id,
              TimekeepingListId = group.FirstOrDefault().TimekeepingListId,
              Year = group.FirstOrDefault().Year,
              Month = group.FirstOrDefault().Month
          }).ToList();

            //lấy danh sách ngày lễ trog năm
            var holiday = holidayRepository.GetAllHolidays();
            //lấy danh sách các ngày cần xem chấm công
            var datesNghiBu = new List<DateTime>();
            var datesNghiLe = new List<DateTime>();
            var datesTatCaNgayNghi = new List<DateTime>();
            //dựa vào tháng, năm truyền vào lấy ngày bắt đầu, kết thúc của tháng
            DateTime aDateTime = new DateTime(Year.Value, Month.Value, 1);
            // Cộng thêm 1 tháng và trừ đi một ngày.
            DateTime retDateTime = aDateTime.AddMonths(1).AddDays(-1);
            // ..duyệt từng ngày
            for (DateTime dt = aDateTime; dt <= retDateTime; dt = dt.AddDays(1))
            {
                //nếu là ngày nghỉ bù của ngày lễ thì thêm vào list datesNghiBu
                if (holiday.Where(x => x.DayStart <= dt && dt <= x.DayEnd && x.DayOffset == true).Count() > 0)
                {
                    datesNghiBu.Add(dt);
                }
                //nếu là ngày nghỉ lễ thì thêm váo list datesNghiLe
                if (holiday.Where(x => x.DayStart <= dt && dt <= x.DayEnd && x.DayOffset != true).Count() > 0)
                {
                    datesNghiLe.Add(dt);
                }
                //lấy danh sách tất cả ngày nghỉ
                if (holiday.Where(x => x.DayStart <= dt && dt <= x.DayEnd).Count() > 0)
                {
                    datesTatCaNgayNghi.Add(dt);
                }
            }
            // ngày nghỉ mặc định trong tuần... theo từng công ty khác nhau thì ngày nghỉ khác nhau.
            var DayOff = categoryRepository.GetCategoryByCode("DayOffDefault").Where(x => x.Value == "True").AsEnumerable().ToList();
            //duyệt danh sách nhân viên 
            foreach (var item in StaffList)
            {
                var timekeepingSynthesis = new TimekeepingSynthesis();
                //kiểm tra xem nhân viên có được tổng hợp chấm công theo tháng đó chưa>>> nếu có chỉ cập nhật lại.
                var list = timekeepingSynthesisRepository.GetTimekeepingSynthesisByStaff(item.StaffId.Value, item.Month.Value, item.Year.Value);
                if (list != null)
                {
                    timekeepingSynthesisRepository.DeleteTimekeepingSynthesis(list.Id);
                }
                //dựa vào bảng workSchedules tổng hợp chấm công
                timekeepingSynthesis.IsDeleted = false;
                timekeepingSynthesis.CreatedUserId = WebSecurity.CurrentUserId;
                timekeepingSynthesis.ModifiedUserId = WebSecurity.CurrentUserId;
                timekeepingSynthesis.AssignedUserId = WebSecurity.CurrentUserId;
                timekeepingSynthesis.CreatedDate = DateTime.Now;
                timekeepingSynthesis.ModifiedDate = DateTime.Now;
                //tính số phút về sớm
                timekeepingSynthesis.GioVeSom = Math.Round(timekeepingList.Where(x => x.StaffId == item.StaffId).Sum(x => Convert.ToDouble(x.Total_minute_work_early)) / Convert.ToDouble(60), 2);
                //tính giờ đi trễ.
                timekeepingSynthesis.GioDiTre = Math.Round(timekeepingList.Where(x => x.StaffId == item.StaffId).Sum(x => Convert.ToDouble(x.Total_minute_work_late)) / Convert.ToDouble(60), 2);
                //lưu lại id chấm công của tháng đó
                timekeepingSynthesis.TimekeepingListId = item.TimekeepingListId;
                //tính ngày nghỉ phép.
                //timekeeping là có tính công
                //absent là vắng group by theo ngày để đếm (vì 1 ngày có thể làm nhiều ca khác nhau.)
                timekeepingSynthesis.NgayNghiCoPhep = timekeepingList.Where(x => x.StaffId == item.StaffId && x.Timekeeping == true && x.Absent == true).GroupBy(x => x.Day).Count();
                //
                var aaa = timekeepingList.Where(x => x.StaffId == item.StaffId);
                List<WorkSchedulesViewModel> listIdDayWork = aaa.Where(id1 => !DayOff.Any(id2 => id2.OrderNo.Value.ToString() == id1.Day.Value.DayOfWeek.ToString())).ToList();
                List<WorkSchedulesViewModel> listIdDayOff = aaa.Where(id1 => DayOff.Any(id2 => id2.OrderNo.Value.ToString() == id1.Day.Value.DayOfWeek.ToString())).ToList();
                List<WorkSchedulesViewModel> listIdNgayLe = aaa.Where(id1 => datesTatCaNgayNghi.Any(id2 => id2.DayOfWeek == id1.Day.Value.DayOfWeek)).ToList();
                //tính tổng số phút làm việc trong ngày thường
                timekeepingSynthesis.TrongGioNgayThuong = Math.Round((listIdDayWork.Sum(x => Convert.ToDouble(x.Total_minute_work))) / Convert.ToDouble(60), 2);
                //tính tổng số phút làm việc trong ngày nghỉ cuối tuần (t7, chủ nhật)
                timekeepingSynthesis.TrongGioNgayNghi = Math.Round((listIdDayOff.Sum(x => Convert.ToDouble(x.Total_minute_work))) / Convert.ToDouble(60), 2);
                //tính tổng số phút làm việc trong ngày lễ
                timekeepingSynthesis.TrongGioNgayLe = Math.Round((listIdNgayLe.Sum(x => Convert.ToDouble(x.Total_minute_work))) / Convert.ToDouble(60), 2);
                //tính tổng số phút tăng ca ngày lễ
                timekeepingSynthesis.TangCaNgayLe = Math.Round((listIdNgayLe.Sum(x => Convert.ToDouble(x.Total_minute_work_overtime))) / Convert.ToDouble(60), 2);
                //tính tổng số phút tăng ca ngày nghỉ
                timekeepingSynthesis.TangCaNgayNghi = Math.Round((listIdDayOff.Sum(x => Convert.ToDouble(x.Total_minute_work_overtime))) / Convert.ToDouble(60), 2);
                //tính tổng số phút tăng ca ngày thường
                timekeepingSynthesis.TangCaNgayThuong = Math.Round((listIdDayWork.Sum(x => Convert.ToDouble(x.Total_minute_work_overtime))) / Convert.ToDouble(60), 2);
                //tính số ngày công thực tế là ngày phân công đi làm.
                int ngay_di_lam=timekeepingList.Where(x => x.StaffId == item.StaffId&&x.Absent==false).GroupBy(x => x.Day).Count();
                int nghi_co_phep=timekeepingList.Where(x => x.StaffId == item.StaffId&&x.Absent==true&&x.Timekeeping==true).GroupBy(x => x.Day).Count();
                timekeepingSynthesis.NgayCongThucTe = ngay_di_lam + nghi_co_phep;
                //timekeepingSynthesis.NgayNghiKoPhep = item.NgayNghiKhongPhep;
                timekeepingSynthesis.StaffId = item.StaffId;
                //timekeepingSynthesis.TongNgayNghi = item.TongNgayNghi;
                timekeepingSynthesis.Year = item.Year;
                timekeepingSynthesis.Month = item.Month;
                //tính số phút làm ban đêm
                timekeepingSynthesis.GioCaDem = Math.Round((timekeepingList.Where(x => x.StaffId == item.StaffId && x.NightShifts == true).Sum(x => Convert.ToDouble(x.Total_minute_work))) / Convert.ToDouble(60), 2);
                //đếm số ngày nghỉ bù
                timekeepingSynthesis.SoNgayNghiBu = datesNghiBu.Count();
                //số ngày nghỉ lễ
                timekeepingSynthesis.SoNgayNghiLe = datesNghiLe.Count();
                timekeepingSynthesis.NgayVeSom = aaa.Where(x => x.DayOffCode == "Sm" || x.DayOffCode == "Tr,Sm").Count();
                timekeepingSynthesis.NgayDiTre = aaa.Where(x => x.DayOffCode == "Tr" || x.DayOffCode == "Tr,Sm").Count();
                timekeepingSynthesisRepository.InsertTimekeepingSynthesis(timekeepingSynthesis);
            }
        }
        #endregion

        #region Detail

        public ActionResult Detail(int? Id)
        {
            //ViewBag.HTML = RenderPartialViewToString(this, "Assign", new WorkSchedules(), Id);
            var TimekeepingList = TimekeepingListRepository.GetvwTimekeepingListById(Id.Value);

            if (TimekeepingList != null && TimekeepingList.IsDeleted != true)
            {
                var model = new TimekeepingListViewModel();
                AutoMapper.Mapper.Map(TimekeepingList, model);
                var dataChiNhanh = Erp.Domain.Helper.SqlHelper.QuerySP<WorkSchedulesViewModel>("spDanhSachChiNhanhChamCong", new
                {
                    TimekeepingListId = Id
                }).ToList();
                ViewBag.ListBranch = dataChiNhanh;
                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        #endregion

        #region Delete
        [HttpPost]
        public ActionResult Delete()
        {
            try
            {
                string idDeleteAll = Request["DeleteId-checkbox"];
                string[] arrDeleteId = idDeleteAll.Split(',');
                for (int i = 0; i < arrDeleteId.Count(); i++)
                {
                    var item = TimekeepingListRepository.GetTimekeepingListById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        //if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        //{
                        //    TempData["FailedMessage"] = "NotOwner";
                        //    return RedirectToAction("Index");
                        //}
                        var workSchedules = WorkSchedulesRepository.GetAllWorkSchedules().Where(x => x.TimekeepingListId == item.Id).ToList();
                        for (int ii = 0; ii < workSchedules.Count(); ii++)
                        {
                            WorkSchedulesRepository.DeleteWorkSchedules(workSchedules[ii].Id);
                        }
                        var synthesisList = timekeepingSynthesisRepository.GetAllTimekeepingSynthesis().Where(x => x.TimekeepingListId == item.Id).ToList();
                        for (int a = 0; a < synthesisList.Count(); a++)
                        {
                            timekeepingSynthesisRepository.DeleteTimekeepingSynthesis(synthesisList[a].Id);
                        }
                        TimekeepingListRepository.DeleteTimekeepingList(item.Id);
                    }
                }
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.DeleteSuccess;
                return RedirectToAction("Index");
            }
            catch (DbUpdateException)
            {
                TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.RelationError;
                return RedirectToAction("Index");
            }
        }
        #endregion


        #region LockTimekeeping
        public ActionResult LockTimekeeping(int Id)
        {
            var timekeepingList = TimekeepingListRepository.GetTimekeepingListById(Id);

            Erp.BackOffice.Staff.Controllers.TimekeepingListController.CreateTimekeeping(timekeepingList.Year, timekeepingList.Month, timekeepingList.Id);
            timekeepingList.Status = "success";
            TimekeepingListRepository.UpdateTimekeepingList(timekeepingList);
            //   TempData[Globals.SuccessMessageKey] = "Lấy dữ liệu từ máy chấm công thành công";
            return RedirectToAction("Detail", "TimekeepingList", new { area = "Staff", Id = Id });
        }
        #endregion

        #region SyncDatabase
        public ActionResult SyncDatabase(string start, string end, int Id,int? BranchId)
        {
            var timekeepingList = TimekeepingListRepository.GetTimekeepingListById(Id);
            //lấy danh sách phân công làm việc.
            IEnumerable<vwWorkSchedules> Listworkschedules = WorkSchedulesRepository.GetvwAllWorkSchedules()
                .Where(x => x.Month == timekeepingList.Month&&x.TimekeepingListId== timekeepingList.Id && x.Year == timekeepingList.Year).ToList();
            if (BranchId != null && BranchId.Value > 0)
            {
                Listworkschedules = Listworkschedules.Where(x => x.Sale_BranchId == BranchId);
            }
            if (!string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(end))
            {
                DateTime start_d;
                if (DateTime.TryParseExact(start, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out start_d))
                {
                    DateTime end_d;
                    if (DateTime.TryParseExact(end, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out end_d))
                    {
                        end_d = end_d.AddHours(23);
                        Listworkschedules = Listworkschedules.Where(x => start_d <= x.Day && x.Day <= end_d);
                    }
                }
            }
            foreach (var item in Listworkschedules)
            {
                ////lấy dòng phân công làm việc đang xét để update dữ liệu lại.
                var update_du_lieu = WorkSchedulesRepository.GetWorkSchedulesById(item.Id);
                //hàm tính dữ liệu chấm công.
                Erp.BackOffice.Staff.Controllers.TimekeepingController.KiemTraVaTinhDuLieuChamCong(item, update_du_lieu);
                //lưu lại
                WorkSchedulesRepository.UpdateWorkSchedules(update_du_lieu);
            }
            return Content("success");
        }
        #endregion

    }
}
