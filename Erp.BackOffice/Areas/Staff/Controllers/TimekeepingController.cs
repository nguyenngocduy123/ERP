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
using Erp.Domain.Staff.Repositories;
using System.Diagnostics;
using Erp.BackOffice.Crm.Models;
using Erp.Domain.Crm.Interfaces;
using Erp.BackOffice.Sale.Models;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;

namespace Erp.BackOffice.Staff.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class TimekeepingController : Controller
    {
        private readonly ITimekeepingRepository TimekeepingRepository;
        private readonly IUserRepository userRepository;
        private readonly IDayOffRepository dayoffRepository;
        private readonly IWorkSchedulesRepository workSchedulesRepository;
        private readonly IShiftsRepository shiftsRepository;
        private readonly IHolidaysRepository holidayRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly ITimekeepingSynthesisRepository timekeepingSynthesisRepository;
        private readonly IBranchRepository branchRepository;
        private readonly IBranchDepartmentRepository departmentRepository;
        private readonly ISymbolTimekeepingRepository symboltimekeepingRepository;
        private readonly IRegisterForOvertimeRepository registerForOvertimeRepository;
        private readonly ITimekeepingListRepository timekeepingListRepository;
        private readonly IStaffsRepository staffRepository;
        private readonly ITaskRepository taskRepository;

        public TimekeepingController(
            ITimekeepingRepository _Timekeeping
            , IUserRepository _user
            , IDayOffRepository dayoff
            , IWorkSchedulesRepository workSchedules
            , IShiftsRepository shifts
            , IHolidaysRepository holiday
            , ICategoryRepository category
            , ITimekeepingSynthesisRepository timekeepingSynthesis
            , IBranchDepartmentRepository department
            , IBranchRepository branch
            , ISymbolTimekeepingRepository symbolTimekeeping
            , IRegisterForOvertimeRepository registerForOvertime
            , ITimekeepingListRepository timekeepingList
            , IStaffsRepository staff
            , ITaskRepository task
            )
        {
            TimekeepingRepository = _Timekeeping;
            userRepository = _user;
            dayoffRepository = dayoff;
            workSchedulesRepository = workSchedules;
            shiftsRepository = shifts;
            holidayRepository = holiday;
            categoryRepository = category;
            timekeepingSynthesisRepository = timekeepingSynthesis;
            departmentRepository = department;
            branchRepository = branch;
            symboltimekeepingRepository = symbolTimekeeping;
            registerForOvertimeRepository = registerForOvertime;
            timekeepingListRepository = timekeepingList;
            staffRepository = staff;
            taskRepository = task;
        }



        #region Edit
        public ActionResult Edit(int? Id)
        {
            var workSchedules = workSchedulesRepository.GetvwWorkSchedulesById(Id.Value);
            var staff = staffRepository.GetvwStaffsById(workSchedules.StaffId.Value);

            if (workSchedules != null)
            {
                var model = new WorkSchedulesViewModel();

                AutoMapper.Mapper.Map(workSchedules, model);
                var path = Helpers.Common.GetSetting("Staff");
                model.ProfileImage = staff.ProfileImage;
                model.ImagePath = path + staff.ProfileImage;

                ViewBag.symbolList = symboltimekeepingRepository.GetAllSymbolTimekeeping().AsEnumerable()
                 .Select(x => new SymbolTimekeepingViewModel
                 {
                     Absent = x.Absent,
                     Code = x.Code,
                     CodeDefault = x.CodeDefault,
                     Color = x.Color,
                     DayOff = x.DayOff,
                     Icon = x.Icon,
                     Id = x.Id,
                     Name = x.Name,
                     Quantity = x.Quantity,
                     Timekeeping = x.Timekeeping
                 });
                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(WorkSchedulesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var timekeeping = workSchedulesRepository.GetWorkSchedulesById(model.Id);
                var q = workSchedulesRepository.GetvwWorkSchedulesById(model.Id);
                if (Request["Submit"] == "Save")
                {
                    var service_schedule = Request["group_choice"];
                    string[] list = service_schedule.Split(',');
                    for (int i = 0; i < list.Count(); i++)
                    {
                        if (list[i] != "")
                        {
                            timekeeping.Symbol = int.Parse(list[i], CultureInfo.InvariantCulture);
                        }
                    }
                    //timekeeping.ShiftsId = model.ShiftsId;
                    timekeeping.ModifiedUserId = WebSecurity.CurrentUserId;
                    timekeeping.ModifiedDate = DateTime.Now;
                    //timekeeping.Symbol = model.Symbol;
                    var symbol = symboltimekeepingRepository.GetSymbolTimekeepingById(timekeeping.Symbol.Value);
                    if (symbol.Absent == true)
                    {
                        timekeeping.Total_minute_work = 0;
                        timekeeping.Total_minute_work_early = 0;
                        timekeeping.Total_minute_work_late = 0;
                        timekeeping.Total_minute_work_overtime = 0;
                    }
                    else
                    {
                        q.HoursIn = Convert.ToDateTime(timekeeping.Day).AddHours(model.HoursIn.Value.Hour).AddMinutes(model.HoursIn.Value.Minute);
                        q.HoursOut = Convert.ToDateTime(timekeeping.Day).AddHours(model.HoursOut.Value.Hour).AddMinutes(model.HoursOut.Value.Minute);
                        //hàm tính dữ liệu chấm công.
                        Erp.BackOffice.Staff.Controllers.TimekeepingController.KiemTraVaTinhDuLieuChamCong(q, timekeeping, q.HoursIn, q.HoursOut);
                    }
                    workSchedulesRepository.UpdateWorkSchedules(timekeeping);
                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                }
                if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                {
                    return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
                }
                else
                {
                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                    return RedirectToAction("Index");
                }
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        #endregion

        #region List
        public ViewResult List(int? Id, int? page)
        {
            var model = new WorkSchedulesViewModel();
            model.TimekeepingListId = Id;
            var timekeepingList = timekeepingListRepository.GetvwTimekeepingListById(Id.Value);
            var staff = Erp.BackOffice.Helpers.Common.GetStaffByCurrentUser();
            DateTime aDateTime = new DateTime(timekeepingList.Year.Value, timekeepingList.Month.Value, 1);
            // Cộng thêm 1 tháng và trừ đi một ngày.
            DateTime retDateTime = aDateTime.AddMonths(1).AddDays(-1);
            ViewBag.aDateTime = aDateTime;
            ViewBag.retDateTime = retDateTime;
            var listTinmeKeeping = workSchedulesRepository.GetvwAllWorkSchedules().Where(x => x.TimekeepingListId.Value == Id).ToList();

            var pager = new Pager(listTinmeKeeping.Count(), page, 20);
            model.pageIndexViewModel = new IndexViewModel<WorkSchedulesViewModel>
            {
                Items = listTinmeKeeping.OrderBy(m => m.Code).Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize)
              .Select(i => new WorkSchedulesViewModel
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
                  //HoursIn = i.HoursIn,
                  //HoursOut = i.HoursOut,
                  Month = i.Month,
                  Name = i.Name,
                  NameShifts = i.NameShifts,
                  //Sale_BranchId = i.Sale_BranchId,
                  //ShiftsId = i.ShiftsId,
                  StaffId = i.StaffId,
                  StartTime = i.StartTime,
                  StartTimeOut = i.StartTimeOut,
                  Symbol = i.Symbol,
                  Timekeeping = i.Timekeeping,
                  Total_minute_work = i.Total_minute_work,
                  Total_minute_work_early = i.Total_minute_work_early,
                  Total_minute_work_late = i.Total_minute_work_late,
                  Total_minute_work_overtime = i.Total_minute_work_overtime,
                  //UserEnrollNumber = i.UserEnrollNumber,
                  Year = i.Year,
                  TimekeepingListId = i.TimekeepingListId
              }).OrderBy(x => x.ShiftsId).ToList(),
                Pager = pager
            };

            //lấy danh sách các ngày nghỉ trong tuần, theo quy định từng công ty.
            var DayOff = categoryRepository.GetCategoryByCode("DayOffDefault").Where(x => x.Value == "True").AsEnumerable().ToList();
            ViewBag.DayOff = DayOff;
            //lấy danh sách các ngày nghỉ lễ trong năm
            var holiday = holidayRepository.GetAllHolidays().AsEnumerable().ToList();
            ViewBag.DayHoliday = holiday;

            if (model.pageIndexViewModel.Items != null)
            {
                //lấy danh sách nhân viên chấm công của tháng đó, chi nhánh đó
                model.StaffList = model.pageIndexViewModel.Items
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
                //lấy danh sách ngày công của nhân viên trong bảng chấm công nhóm theo id nhân viên và ngày công.
                model.DayWorkList = model.pageIndexViewModel.Items.GroupBy(x => new { x.StaffId, x.Day }, (key, group) =>
                    new WorkSchedulesViewModel
                    {
                        StaffId = key.StaffId,
                        Day = key.Day,
                        Id = group.FirstOrDefault().Id
                    }).OrderBy(x => x.Day).ToList();
            }
            return View(model);
        }

        #endregion

        #region Create
        public ActionResult Create(int StaffId, DateTime? Day)
        {
            var model = new WorkSchedulesViewModel();
            model.StaffId = StaffId;
            model.Day = Day;
            ////model.DayWork = DayWork;
            //model.ShiftsList = Helpers.SelectListHelper.GetSelectList_Shifts(null);
            //model.staffList = Helpers.SelectListHelper.GetSelectList_Staff(null);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(WorkSchedulesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var timekeeping = new Domain.Staff.Entities.WorkSchedules();
                var shift = shiftsRepository.GetShiftsById(model.ShiftsId.Value);
                AutoMapper.Mapper.Map(model, timekeeping);
                timekeeping.IsDeleted = false;
                timekeeping.CreatedUserId = WebSecurity.CurrentUserId;
                timekeeping.ModifiedUserId = WebSecurity.CurrentUserId;
                timekeeping.CreatedDate = DateTime.Now;
                timekeeping.ModifiedDate = DateTime.Now;
                timekeeping.Day = model.Day;
                timekeeping.ShiftsId = model.ShiftsId;
                if (timekeeping.HoursIn > timekeeping.HoursOut)
                {
                    timekeeping.HoursOut = timekeeping.HoursOut.Value.AddDays(1);
                }
                var service_schedule = Request["group_choice"];
                string[] list = service_schedule.Split(',');
                for (int i = 0; i < list.Count(); i++)
                {
                    if (list[i] != "")
                    {
                        timekeeping.Symbol = int.Parse(list[i], CultureInfo.InvariantCulture);
                    }
                }
                timekeeping.ShiftsId = model.ShiftsId;
                var symbol = symboltimekeepingRepository.GetSymbolTimekeepingById(timekeeping.Symbol.Value);
                if (symbol.Absent == true)
                {
                    timekeeping.Total_minute_work = 0;
                    timekeeping.Total_minute_work_early = 0;
                    timekeeping.Total_minute_work_late = 0;
                    timekeeping.Total_minute_work_overtime = 0;
                }
                else
                {
                    timekeeping.HoursIn = model.HoursIn;
                    timekeeping.HoursOut = model.HoursOut;

                }
                workSchedulesRepository.InsertWorkSchedules(timekeeping);
                if (symbol.Absent == true)
                {
                    var q = workSchedulesRepository.GetvwWorkSchedulesById(timekeeping.Id);
                    //hàm tính dữ liệu chấm công.
                    Erp.BackOffice.Staff.Controllers.TimekeepingController.KiemTraVaTinhDuLieuChamCong(q, timekeeping, model.HoursIn, model.HoursOut);
                }
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                {
                    return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
                }
                else
                {
                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Create");
        }
        #endregion

        #region Delete
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            try
            {
                var item = TimekeepingRepository.GetTimekeepingById(id.Value);
                if (item != null)
                {
                    //if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                    //{
                    //    TempData["FailedMessage"] = "NotOwner";
                    //    return RedirectToAction("List");

                    //}
                    //item.IsDeleted = true;
                    // TimekeepingRepository.UpdateTimekeeping(item);
                    TimekeepingRepository.DeleteTimekeeping(id.Value);
                }
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.DeleteSuccess;
                return RedirectToAction("List");

            }
            catch (DbUpdateException)
            {
                TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.RelationError;
                return RedirectToAction("List");

            }
        }
        #endregion

        #region Calendar
        public ViewResult Calender(int? TimekeepingListId, int BranchId)
        {
            //var user = userRepository.GetUserById(WebSecurity.CurrentUserId);
            //List<string> ListBranchId = new List<string>();
            //var Branch_id = 0;
            //if (!string.IsNullOrEmpty(BranchId))
            //{
            //    ListBranchId = BranchId.Split(',').ToList();
            //    Branch_id = Convert.ToInt32(ListBranchId.FirstOrDefault().ToString());
            //}

            var timekeepingList = timekeepingListRepository.GetTimekeepingListById(TimekeepingListId.Value);
            DateTime aDateTime = new DateTime();
            aDateTime = new DateTime(timekeepingList.Year.Value, timekeepingList.Month.Value, 1);
            var dataChamCong = Erp.Domain.Helper.SqlHelper.QuerySP<WorkSchedulesViewModel>("spDanhSachChamCong", new
            {
                TimekeepingListId = TimekeepingListId,
                BranchId = BranchId
            }).ToList();

            var dataNhanVien = Erp.Domain.Helper.SqlHelper.QuerySP<WorkSchedulesViewModel>("spDanhSachNhanVienChamCong", new
            {
                TimekeepingListId = TimekeepingListId,
                BranchId = BranchId
            }).ToList();

            var resource = dataNhanVien.Select(e => new
            {
                id = e.StaffId,
                title = e.Name,
                branch_id = e.BranchName
            }).ToList();

            var dataEvent = dataChamCong.Select(e => new
            {
                id = e.Id,
                resourceId = e.StaffId,
                title = e.DayOffCode,
                start = (e.HoursIn == null ? Convert.ToDateTime(e.Day.Value.ToString("dd/MM/yyyy") + " " + e.StartTime).ToString("yyyy-MM-ddTHH:mm:ssZ") : e.HoursIn.Value.ToString("yyyy-MM-ddTHH:mm:ssZ")),
                end = (e.HoursOut == null ? Convert.ToDateTime(e.Day.Value.ToString("dd/MM/yyyy") + " " + e.EndTime).ToString("yyyy-MM-ddTHH:mm:ssZ") : e.HoursOut.Value.ToString("yyyy-MM-ddTHH:mm:ssZ")),
                //allDay = false,
                className = (e.Color),
                url = e.Id,
                backgroundColor = e.Color,
                tooltip = e.DayOffName
            }).ToList();

            ViewBag.dataEvent = new JavaScriptSerializer().Serialize(dataEvent);
            ViewBag.resource = new JavaScriptSerializer().Serialize(resource);

            ViewBag.aDateTime = aDateTime.ToString("yyyy-MM-dd");
            return View();
        }
        #endregion

        #region KiemTraVaTinhDuLieuChamCong
        public static void KiemTraVaTinhDuLieuChamCong(vwWorkSchedules item, WorkSchedules save_database)
        {
            StaffsRepository staffRepository = new StaffsRepository(new Domain.Staff.ErpStaffDbContext());
            SymbolTimekeepingRepository symboltimekeepingRepository = new SymbolTimekeepingRepository(new Domain.Staff.ErpStaffDbContext());
            DayOffRepository dayoffRepository = new DayOffRepository(new Domain.Staff.ErpStaffDbContext());
            ShiftsRepository shiftsRepository = new ShiftsRepository(new Domain.Staff.ErpStaffDbContext());
            CheckInOutRepository checkInOutRepository = new CheckInOutRepository(new Domain.Staff.ErpStaffDbContext());
            WorkSchedulesRepository workSchedulesRepository = new WorkSchedulesRepository(new Domain.Staff.ErpStaffDbContext());
            RegisterForOvertimeRepository registerForOvertimeRepository = new RegisterForOvertimeRepository(new Domain.Staff.ErpStaffDbContext());
            ////nếu có chọn tháng năm khi upload database thì lọc dữ liệu khi upload (mặc định thì upload hết dữ liệu).
            IEnumerable<vwCheckInOut> list = checkInOutRepository.GetAllvwCheckInOut().Where(x => x.StaffId == item.StaffId
                && x.TimeDate.Value.Day == item.Day.Value.Day
                && x.TimeDate.Value.Month == item.Day.Value.Month
                && x.TimeDate.Value.Year == item.Day.Value.Year).AsEnumerable();

            var listcheckinout = list.Select(x => new CheckInOutViewModel
            {
                TimeDate = x.TimeDate,
                TimeStr = x.TimeStr,
                UserId = x.UserId,
                StaffId = x.StaffId,
                BranchDepartmentId = x.BranchDepartmentId,
                Sale_BranchId = x.Sale_BranchId,
                FPMachineId=x.FPMachineId
            }).ToList();

            if (listcheckinout.Count() > 1)
            {
                //lấy danh sách đăng ký tăng ca có thời gian bắt đầu tăng ca nằm trong khoảng thời gian của ca làm việc.
                //sắp xếp theo thời gian bắt đầu tăng ca... nếu có nhiều đăng ký tăng ca cùng 1 lúc thì chỉ lấy dòng đăng ký đầu tiên.
                //var dkTangCa = registerForOvertimeRepository.GetAllvwRegisterForOvertime().Where(x => x.DayOvertime == item.Day && x.StaffId == item.StaffId).OrderBy(x => x.StartHour);

                #region chuyển đổi thời gian của ca làm việc từ string sang DateTime
                //tách chuỗi thời gian của ca làm việc
                string strStartTime = item.Day.Value.ToString("dd/MM/yyyy") + " " + item.StartTime;
                string strStartTimeOut = item.Day.Value.ToString("dd/MM/yyyy") + " " + item.StartTimeOut;
                string strStartTimeIn = item.Day.Value.ToString("dd/MM/yyyy") + " " + item.StartTimeIn;
                string strEndTime = item.Day.Value.ToString("dd/MM/yyyy") + " " + item.EndTime;
                string strEndTimeIn = item.Day.Value.ToString("dd/MM/yyyy") + " " + item.EndTimeIn;
                string strEndTimeOut = item.Day.Value.ToString("dd/MM/yyyy") + " " + item.EndTimeOut;
                // chuyển đổi thời gian của ca làm việc từ string sang DateTime
                //DateTime d = DateTime.ParseExact("08/11/2017 12:00", "dd/MM/yyyy HH:mm", null);
                DateTime StartTime = DateTime.ParseExact(strStartTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                DateTime StartTimeOut = DateTime.ParseExact(strStartTimeOut, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                DateTime StartTimeIn = DateTime.ParseExact(strStartTimeIn, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                DateTime EndTime = DateTime.ParseExact(strEndTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                DateTime EndTimeIn = DateTime.ParseExact(strEndTimeIn, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                DateTime EndTimeOut = DateTime.ParseExact(strEndTimeOut, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                #endregion

                #region //nếu thời gian kết thúc ca mà nhỏ hơn thời gian bắt đầu ca thì xác định ca đó qua đêm... cộng thêm 1 ngày vào thời gian kết thúc ca.
                if (StartTime > EndTime)
                {
                    EndTime = EndTime.AddDays(1);
                }
                if (StartTime > EndTimeIn)
                {
                    EndTimeIn = EndTimeIn.AddDays(1);
                }
                if (StartTime > EndTimeOut)
                {
                    EndTimeOut = EndTimeOut.AddDays(1);
                }
                #endregion
                //thời gian bắt đầu tính tăng ca
                int setting_overtime = Convert.ToInt32(Helpers.Common.GetSetting("time_starts_counting_overtime"));
                DateTime DateOverTime = EndTime.AddMinutes(setting_overtime);


                //lấy tất cả check in out trong khoảng thời gian của ca hiện tại... đi sớm vẫn lấy được
                var HourIn = listcheckinout.Where(xx => xx.TimeStr > StartTimeIn && xx.TimeStr < StartTimeOut && xx.UserId == item.UserEnrollNumber).OrderBy(xx => xx.TimeStr).ToList();

                if (HourIn.Count() > 0)
                {
                    save_database.HoursIn = HourIn.FirstOrDefault().TimeStr;
                    save_database.FPMachineId = HourIn.FirstOrDefault().FPMachineId;
                    #region   //nếu thời gian vào của nhân viên lớn hơn thời gian của ca làm việc thì tính thời gian đi trễ
                    if (save_database.HoursIn > StartTime)
                    {
                        //tính số phút đi trễ 
                        TimeSpan a = save_database.HoursIn.Value.Subtract(StartTime);
                        //trừ đi số giờ đi trễ cho phép.
                        var q = Convert.ToInt32(a.TotalMinutes) - item.MinuteLate.Value;
                        //cập nhật số giờ đi trễ
                        if (q > 0)
                        {
                            //đi trễ
                            save_database.Total_minute_work_late = q;
                        }
                        else
                        {
                            //đi đúng giờ
                            save_database.Total_minute_work_late = 0;
                        }
                    }
                    else
                    {
                        //đi đúng giờ
                        save_database.Total_minute_work_late = 0;
                    }
                    #endregion
                }
                else
                {
                    //để đây xử lý sau, cho=0 để ko bị lỗi... còn trường hợp nghỉ phép nữa
                    save_database.Total_minute_work_late = 0;
                }

                var HourOut = listcheckinout.Where(xx => xx.TimeStr > EndTimeIn&& xx.TimeDate.Value.ToString("dd/MM/yyyy") == EndTime.ToString("dd/MM/yyyy")  && xx.UserId == item.UserEnrollNumber).OrderBy(xx => xx.TimeStr).ToList();
                if (HourOut.Count() > 0)
                {
                save_database.HoursOut = HourOut.FirstOrDefault().TimeStr;
                save_database.FPMachineId = HourOut.FirstOrDefault().FPMachineId;
                if (save_database.HoursOut >= DateOverTime)
                {
                    #region tăng ca
                    TimeSpan a = save_database.HoursOut.Value.Subtract(EndTime);
                    save_database.Total_minute_work_overtime = Convert.ToInt32(a.TotalMinutes);
                    #endregion
                }
                else
                {
                    #region về sớm (về đúng giờ) không có tăng ca

                    TimeSpan a = EndTime.Subtract(save_database.HoursOut.Value);
                    var q = Convert.ToInt32(a.TotalMinutes) - item.MinuteEarly;
                    if (q > 0)
                    {
                        //về sớm
                        save_database.Total_minute_work_early = q;
                    }
                    else
                    {
                        //về đúng giờ
                        save_database.Total_minute_work_early = 0;
                    }
                    save_database.Total_minute_work_overtime = 0;
                    #endregion
                }
                }
               
                   
                #region tính số phút làm trong 1 ca
                if (save_database.HoursIn != null && save_database.HoursOut != null)
                {
                    TimeSpan total = EndTime.Subtract(StartTime);
                    save_database.Total_minute_work = Convert.ToInt32(total.TotalMinutes) + save_database.Total_minute_work_overtime - save_database.Total_minute_work_late - save_database.Total_minute_work_early;
                }
                #endregion

                #region xét ký hiệu chấm công
                //if (save_database.HoursIn != null && save_database.HoursOut == null)
                //{
                //    save_database.HoursOut = EndTime;
                //}
               
                //if (save_database.HoursIn == null && save_database.HoursOut != null)
                //{
                //    save_database.HoursIn = StartTime;
                //}
                //if (save_database.Symbol == null)
                //{
                //nếu chưa đi làm thì xét trường hợp có phép hoặc không phép.
                if (save_database.HoursIn == null && save_database.HoursOut == null)
                {
                    var phep = dayoffRepository.GetAllvwDayOff().Where(x => x.DayStart <= save_database.Day && x.DayEnd >= save_database.Day && x.StaffId == save_database.StaffId);
                    if (phep.Count() > 0)
                    {
                        save_database.Symbol = phep.FirstOrDefault().TypeDayOffId;
                    }
                    else
                    {
                        save_database.Symbol = symboltimekeepingRepository.GetSymbolTimekeepingByCodeDefault("K").Id;
                    }
                }

                    //nếu chỉ có giờ ra thì lưu ký hiệu đi làm chưa có
                else if (save_database.HoursIn == null)
                {
                    save_database.Symbol = symboltimekeepingRepository.GetSymbolTimekeepingByCodeDefault("DLo").Id;
                    save_database.Total_minute_work = 0;
                    save_database.Total_minute_work_early = 0;
                    save_database.Total_minute_work_late = 0;
                    save_database.Total_minute_work_overtime = 0;
                }
                //nếu chỉ có giờ vào thì lưu ký hiệu chưa có giờ vào.
                else if (save_database.HoursOut == null)
                {
                    save_database.Symbol = symboltimekeepingRepository.GetSymbolTimekeepingByCodeDefault("DVo").Id;
                    save_database.Total_minute_work = 0;
                    save_database.Total_minute_work_early = 0;
                    save_database.Total_minute_work_late = 0;
                    save_database.Total_minute_work_overtime = 0;
                }
                else
                {
                    //nếu có đi làm thì xét trường hợp đi trễ, về sớm, đi đúng giờ, tăng ca
                    if (save_database.Total_minute_work_late == 0 && save_database.Total_minute_work_early == 0)
                    {
                        save_database.Symbol = symboltimekeepingRepository.GetSymbolTimekeepingByCodeDefault("X").Id;
                    }
                    else if (save_database.Total_minute_work_late > 0 && save_database.Total_minute_work_early > 0)
                    {
                        save_database.Symbol = symboltimekeepingRepository.GetSymbolTimekeepingByCodeDefault("Tr,Sm").Id;
                    }
                    else if (save_database.Total_minute_work_late > 0)
                    {
                        save_database.Symbol = symboltimekeepingRepository.GetSymbolTimekeepingByCodeDefault("Tr").Id;
                    }
                    else if (save_database.Total_minute_work_early > 0)
                    {
                        save_database.Symbol = symboltimekeepingRepository.GetSymbolTimekeepingByCodeDefault("Sm").Id;
                    }
                    if (save_database.Total_minute_work_overtime > 0)
                    {
                        save_database.Symbol = symboltimekeepingRepository.GetSymbolTimekeepingByCodeDefault("+").Id;
                    }
                }
                //}
            }
            else
            {
                save_database.Symbol = symboltimekeepingRepository.GetSymbolTimekeepingByCodeDefault("K").Id;
                save_database.Total_minute_work = 0;
                save_database.Total_minute_work_early = 0;
                save_database.Total_minute_work_late = 0;
                save_database.Total_minute_work_overtime = 0;
            }
                #endregion

        }
        #endregion

        #region KiemTraVaTinhDuLieuChamCong
        public static void KiemTraVaTinhDuLieuChamCong(vwWorkSchedules item, WorkSchedules save_database, DateTime? HourIn, DateTime? HourOut)
        {
            StaffsRepository staffRepository = new StaffsRepository(new Domain.Staff.ErpStaffDbContext());
            SymbolTimekeepingRepository symboltimekeepingRepository = new SymbolTimekeepingRepository(new Domain.Staff.ErpStaffDbContext());
            DayOffRepository dayoffRepository = new DayOffRepository(new Domain.Staff.ErpStaffDbContext());
            ShiftsRepository shiftsRepository = new ShiftsRepository(new Domain.Staff.ErpStaffDbContext());
            CheckInOutRepository checkInOutRepository = new CheckInOutRepository(new Domain.Staff.ErpStaffDbContext());
            WorkSchedulesRepository workSchedulesRepository = new WorkSchedulesRepository(new Domain.Staff.ErpStaffDbContext());
            RegisterForOvertimeRepository registerForOvertimeRepository = new RegisterForOvertimeRepository(new Domain.Staff.ErpStaffDbContext());

            //lấy danh sách đăng ký tăng ca có thời gian bắt đầu tăng ca nằm trong khoảng thời gian của ca làm việc.
            //sắp xếp theo thời gian bắt đầu tăng ca... nếu có nhiều đăng ký tăng ca cùng 1 lúc thì chỉ lấy dòng đăng ký đầu tiên.
            //var dkTangCa = registerForOvertimeRepository.GetAllvwRegisterForOvertime().Where(x => x.StartHour >= item.HoursIn && x.StartHour <= item.HoursOut && x.StaffId == item.StaffId).OrderBy(x => x.StartHour);

            #region chuyển đổi thời gian của ca làm việc từ string sang DateTime
            //tách chuỗi thời gian của ca làm việc
            string strStartTime = item.Day.Value.ToString("dd/MM/yyyy") + " " + item.StartTime;
            string strStartTimeOut = item.Day.Value.ToString("dd/MM/yyyy") + " " + item.StartTimeOut;
            string strStartTimeIn = item.Day.Value.ToString("dd/MM/yyyy") + " " + item.StartTimeIn;
            string strEndTime = item.Day.Value.ToString("dd/MM/yyyy") + " " + item.EndTime;
            string strEndTimeIn = item.Day.Value.ToString("dd/MM/yyyy") + " " + item.EndTimeIn;
            string strEndTimeOut = item.Day.Value.ToString("dd/MM/yyyy") + " " + item.EndTimeOut;
            // chuyển đổi thời gian của ca làm việc từ string sang DateTime
            //DateTime d = DateTime.ParseExact("08/11/2017 12:00", "dd/MM/yyyy HH:mm", null);
            DateTime StartTime = DateTime.ParseExact(strStartTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            DateTime StartTimeOut = DateTime.ParseExact(strStartTimeOut, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            DateTime StartTimeIn = DateTime.ParseExact(strStartTimeIn, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            DateTime EndTime = DateTime.ParseExact(strEndTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            DateTime EndTimeIn = DateTime.ParseExact(strEndTimeIn, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            DateTime EndTimeOut = DateTime.ParseExact(strEndTimeOut, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            #endregion

            #region //nếu thời gian kết thúc ca mà nhỏ hơn thời gian bắt đầu ca thì xác định ca đó qua đêm... cộng thêm 1 ngày vào thời gian kết thúc ca.
            if (StartTime > EndTime)
            {
                EndTime = EndTime.AddDays(1);
            }
            if (StartTime > EndTimeIn)
            {
                EndTimeIn = EndTimeIn.AddDays(1);
            }
            if (StartTime > EndTimeOut)
            {
                EndTimeOut = EndTimeOut.AddDays(1);
            }
            #endregion
            //thời gian bắt đầu tính tăng ca
            int setting_overtime = Convert.ToInt32(Helpers.Common.GetSetting("time_starts_counting_overtime"));
            DateTime DateOverTime = EndTime.AddMinutes(setting_overtime);


            if (HourIn.Value != null)
            {
                save_database.HoursIn = HourIn;
                #region   //nếu thời gian vào của nhân viên lớn hơn thời gian của ca làm việc thì tính thời gian đi trễ
                if (save_database.HoursIn > StartTime)
                {
                    //tính số phút đi trễ 
                    TimeSpan a = save_database.HoursIn.Value.Subtract(StartTime);
                    //trừ đi số giờ đi trễ cho phép.
                    var q = Convert.ToInt32(a.TotalMinutes) - item.MinuteLate;
                    //cập nhật số giờ đi trễ
                    if (q > 0)
                    {
                        //đi trễ
                        save_database.Total_minute_work_late = q;
                    }
                    else
                    {
                        //đi đúng giờ
                        save_database.Total_minute_work_late = 0;
                    }
                }
                else
                {
                    //đi đúng giờ
                    save_database.Total_minute_work_late = 0;
                }
                #endregion
            }
            else
            {
                //để đây xử lý sau, cho=0 để ko bị lỗi... còn trường hợp nghỉ phép nữa
                save_database.Total_minute_work_late = 0;
            }
            //var HourOut = listcheckinout.Where(xx => xx.TimeStr > EndTimeIn && xx.TimeDate.Value.ToString("dd/MM/yyyy") == EndTime.ToString("dd/MM/yyyy") && xx.UserId == item.UserEnrollNumber).OrderByDescending(xx => xx.TimeStr).ToList();
            if (HourOut!=null)
            {
                save_database.HoursOut = HourOut;
            }
            if (save_database.HoursOut >= DateOverTime)
            {
                #region tăng ca
                TimeSpan a = save_database.HoursOut.Value.Subtract(EndTime);
                save_database.Total_minute_work_overtime = Convert.ToInt32(a.TotalMinutes);
                #endregion
            }
            else
            {
                #region về sớm (về đúng giờ) không có tăng ca

                TimeSpan a = EndTime.Subtract(save_database.HoursOut.Value);
                var q = Convert.ToInt32(a.TotalMinutes) - item.MinuteEarly;
                if (q > 0)
                {
                    //về sớm
                    save_database.Total_minute_work_early = q;
                }
                else
                {
                    //về đúng giờ
                    save_database.Total_minute_work_early = 0;
                }
                save_database.Total_minute_work_overtime = 0;
                #endregion
            }
            #region tính số phút làm trong 1 ca
            if (save_database.HoursIn != null && save_database.HoursOut != null)
            {
                TimeSpan total = EndTime.Subtract(StartTime);
                save_database.Total_minute_work = Convert.ToInt32(total.TotalMinutes) + save_database.Total_minute_work_overtime - save_database.Total_minute_work_late - save_database.Total_minute_work_early;
            }
            #endregion

            #region xét ký hiệu chấm công
            if (save_database.Symbol == null)
            {
                //nếu chưa đi làm thì xét trường hợp có phép hoặc không phép.
                if (save_database.HoursIn == null && save_database.HoursOut == null)
                {
                    var phep = dayoffRepository.GetAllvwDayOff().Where(x => x.DayStart <= save_database.Day && x.DayEnd >= save_database.Day && x.StaffId == save_database.StaffId);
                    if (phep.Count() > 0)
                    {
                        save_database.Symbol = phep.FirstOrDefault().TypeDayOffId;
                    }
                    else
                    {
                        save_database.Symbol = symboltimekeepingRepository.GetSymbolTimekeepingByCodeDefault("K").Id;
                    }
                }

                //nếu chỉ có giờ ra thì lưu ký hiệu đi làm chưa có
                else if (save_database.HoursIn == null)
                {
                    save_database.Symbol = symboltimekeepingRepository.GetSymbolTimekeepingByCodeDefault("DLo").Id;
                    save_database.Total_minute_work = 0;
                    save_database.Total_minute_work_early = 0;
                    save_database.Total_minute_work_late = 0;
                    save_database.Total_minute_work_overtime = 0;
                }
                //nếu chỉ có giờ vào thì lưu ký hiệu chưa có giờ vào.
                else if (save_database.HoursOut == null)
                {
                    save_database.Symbol = symboltimekeepingRepository.GetSymbolTimekeepingByCodeDefault("DVo").Id;
                    save_database.Total_minute_work = 0;
                    save_database.Total_minute_work_early = 0;
                    save_database.Total_minute_work_late = 0;
                    save_database.Total_minute_work_overtime = 0;
                }
                else
                {
                    //nếu có đi làm thì xét trường hợp đi trễ, về sớm, đi đúng giờ, tăng ca
                    if (save_database.Total_minute_work_late == 0 && save_database.Total_minute_work_early == 0)
                    {
                        save_database.Symbol = symboltimekeepingRepository.GetSymbolTimekeepingByCodeDefault("X").Id;
                    }
                    else if (save_database.Total_minute_work_late > 0 && save_database.Total_minute_work_early > 0)
                    {
                        save_database.Symbol = symboltimekeepingRepository.GetSymbolTimekeepingByCodeDefault("Tr,Sm").Id;
                    }
                    else if (save_database.Total_minute_work_late > 0)
                    {
                        save_database.Symbol = symboltimekeepingRepository.GetSymbolTimekeepingByCodeDefault("Tr").Id;
                    }
                    else if (save_database.Total_minute_work_early > 0)
                    {
                        save_database.Symbol = symboltimekeepingRepository.GetSymbolTimekeepingByCodeDefault("Sm").Id;
                    }
                    if (save_database.Total_minute_work_overtime > 0)
                    {
                        save_database.Symbol = symboltimekeepingRepository.GetSymbolTimekeepingByCodeDefault("+").Id;
                    }
                }
            }
            #endregion

        }
        #endregion

        #region Total
        public ViewResult Total(int? Id, int BranchId)
        {
            //List<string> ListBranchId = new List<string>();
            //  int Branch_id=0;
            //if (!string.IsNullOrEmpty(BranchId))
            //{
            //    ListBranchId = BranchId.Split(',').ToList();
            //    Branch_id = Convert.ToInt32(ListBranchId.FirstOrDefault().ToString());
            //}

            var staff = Erp.BackOffice.Helpers.Common.GetStaffByCurrentUser();
            List<vwTimekeepingSynthesisViewModel> q;
            var timekeepingList = timekeepingListRepository.GetTimekeepingListById(Id.Value);

            var list = timekeepingSynthesisRepository.GetAllvwTimekeepingSynthesis().Where(x => x.TimekeepingListId == timekeepingList.Id);
            q = list.Select(i => new vwTimekeepingSynthesisViewModel
            {
                Id = i.Id,
                BranchDepartmentId = i.BranchDepartmentId,
                Code = i.Code,
                GioDiTre = i.GioDiTre,
                TangCaNgayThuong = i.TangCaNgayThuong,
                TangCaNgayNghi = i.TangCaNgayNghi,
                TangCaNgayLe = i.TangCaNgayLe,
                SoNgayNghiBu = i.SoNgayNghiBu,
                SoNgayNghiLe = i.SoNgayNghiLe,
                TrongGioNgayThuong = i.TrongGioNgayThuong,
                TrongGioNgayNghi = i.TrongGioNgayNghi,
                TrongGioNgayLe = i.TrongGioNgayLe,
                GioVeSom = i.GioVeSom,
                Month = i.Month,
                Name = i.Name,
                NgayCongThucTe = i.NgayCongThucTe,
                NgayNghiCoPhep = i.NgayNghiCoPhep,
                Sale_BranchId = i.Sale_BranchId,
                StaffId = i.StaffId,
                Year = i.Year,
                GioLamCaDem = i.GioCaDem,
                TongGioTangCa = (i.TangCaNgayLe + i.TangCaNgayNghi + i.TangCaNgayThuong),
                TimekeepingListId = i.TimekeepingListId,
                NgayDiTre=i.NgayDiTre,
                NgayVeSom=i.NgayVeSom
            }).ToList();
            if (BranchId > 0)
            {
                q = q.Where(x => x.Sale_BranchId == BranchId).ToList();
            }
            return View(q);
        }
        #endregion

        #region UpdateDate
        [HttpPost]
        public ActionResult UpdateDate(int? Id, string date)
        {

            var item = workSchedulesRepository.GetWorkSchedulesById(Id.Value);
            if (item != null)
            {
                var time = Convert.ToDateTime(date);
                DateTime aDateTime = new DateTime(time.Year, time.Month, time.Day);
                item.Day = aDateTime;
                workSchedulesRepository.UpdateWorkSchedules(item);
                return Content("success");
            }
            return Content("error");
        }
        #endregion

        #region Detail
        public ActionResult Detail(int? Id)
        {
            var workSchedules = workSchedulesRepository.GetvwWorkSchedulesById(Id.Value);
            if (workSchedules != null && workSchedules.IsDeleted != true)
            {
                var model = new WorkSchedulesViewModel();
                AutoMapper.Mapper.Map(workSchedules, model);
                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index", "Timekeeping", new { area = "Staff" });
        }
        #endregion

    }
}
