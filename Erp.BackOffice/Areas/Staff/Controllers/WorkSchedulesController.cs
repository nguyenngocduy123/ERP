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
    public class WorkSchedulesController : Controller
    {
        private readonly IWorkSchedulesRepository WorkSchedulesRepository;
        private readonly IUserRepository userRepository;
        private readonly IStaffsRepository StaffsRepository;
        private readonly IShiftsRepository shiftsRepository;
        private readonly ILocationRepository locationRepository;
        private readonly IHolidaysRepository holidayRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly ITimekeepingRepository timekeepingRepository;
        private readonly IDayOffRepository dayoffRepository;
        private readonly ITimekeepingListRepository timekeepingListRepository;
        public WorkSchedulesController(
            IWorkSchedulesRepository _WorkSchedules
            , IUserRepository _user
            , IStaffsRepository staff
            , IShiftsRepository shifts
            , ILocationRepository location
            , IHolidaysRepository holiday
            , ICategoryRepository category
            , ITimekeepingRepository timekeeping
            , IDayOffRepository dayoff
            , ITimekeepingListRepository timekeepingList
            )
        {
            WorkSchedulesRepository = _WorkSchedules;
            userRepository = _user;
            StaffsRepository = staff;
            shiftsRepository = shifts;
            locationRepository = location;
            holidayRepository = holiday;
            categoryRepository = category;
            timekeepingRepository = timekeeping;
            dayoffRepository = dayoff;
            timekeepingListRepository = timekeepingList;
        }


        [HttpGet]
        public JsonResult FetchShifts()
        {
            var list = WorkSchedulesRepository.GetAllWorkSchedules();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        #region -- Staff --
        public ViewResult Staff(string Code, string Name, string Ethnic, string ProvinceId, string DistrictId, string WardId, string CountryId, string Position, string Religion, string MaritalStatus, int? branchId, int? DepartmentId)
        {
            ViewData["Code"] = Code;
            ViewData["Name"] = Name;
            //ViewData["EthnicList"] = Helpers.SelectListHelper.GetSelectList_Category("ethnic", null, "Name","Dân tộc");
            //ViewData["BranchList"] = Helpers.SelectListHelper.GetSelectList_Branch(null);
            //ViewData["DepartmentList"] = Helpers.SelectListHelper.GetSelectList_BranchDepartment(null, branchId.HasValue ? branchId.Value : 0);
            //ViewData["MaritalStatusList"] = Helpers.SelectListHelper.GetSelectList_Category("MaritalStatus", null, "Name","Tình trạng hôn nhân");
            //ViewData["PositionList"] = Helpers.SelectListHelper.GetSelectList_Category("position", null, "Value","Chức vụ");
            //ViewData["ReligionList"] = Helpers.SelectListHelper.GetSelectList_Category("religion", null, "Name","Tôn giáo");
            ViewData["CountryList"] = Helpers.SelectListHelper.GetSelectList_Category("country", null, "Name", App_GlobalResources.Wording.Empty);
            ViewData["ProvinceList"] = Helpers.SelectListHelper.GetSelectList_Location("0", null, App_GlobalResources.Wording.Empty);
            ViewData["DistrictList"] = Helpers.SelectListHelper.GetSelectList_Location(null, null, App_GlobalResources.Wording.Empty);
            ViewData["WardList"] = Helpers.SelectListHelper.GetSelectList_Location(null, null, App_GlobalResources.Wording.Empty);
            var staff = Erp.BackOffice.Helpers.Common.GetStaffByCurrentUser();
            var user = userRepository.GetUserById(WebSecurity.CurrentUserId);
            // var userType = userTypeRepository.GetUserTypeById((int)user.UserTypeId);
            IEnumerable<StaffsViewModel> q = StaffsRepository.GetvwAllStaffs()
                .Select(item => new StaffsViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    Birthday = item.Birthday,
                    ProfileImage = item.ProfileImage,
                    Code = item.Code,
                    Address = item.Address,
                   // BranchCode = item.BranchCode,
                  //  BranchDepartmentId = item.BranchDepartmentId,
                    GenderName = item.GenderName,
                    Gender = item.Gender,
                    Ethnic = item.Ethnic,
                  //  BranchName = item.BranchName,
                    CountryId = item.CountryId,
                    DistrictId = item.DistrictId,
                    Phone = item.Phone,
                    DistrictName = item.DistrictName,
                    CardIssuedName = item.CardIssuedName,
                    Email = item.Email,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    IdCardDate = item.IdCardDate,
                    IdCardIssued = item.IdCardIssued,
                    ProvinceId = item.ProvinceId,
                    ProvinceName = item.ProvinceName,
                //    Sale_BranchId = item.Sale_BranchId,
                    WardId = item.WardId,
                   // Staff_DepartmentId = item.Staff_DepartmentId,
                    Religion = item.Religion,
                    IdCardNumber = item.IdCardNumber,
                 //   Position = item.Position,
                    WardName = item.WardName,
                    MaritalStatus = item.MaritalStatus,
                    Email2 = item.Email2,
                    Phone2 = item.Phone2
                }).ToList();

            bool bIsSearch = false;

            if (!string.IsNullOrEmpty(Code))
            {
                q = q.Where(item => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.Code).ToLower().Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(Code).ToLower()));
                bIsSearch = true;
            }

            if (!string.IsNullOrEmpty(Name))
            {
                q = q.Where(item => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.Name).ToLower().Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(Name).ToLower()));
                bIsSearch = true;
            }
            if (!string.IsNullOrEmpty(Ethnic))
            {
                q = q.Where(item => item.Ethnic == Ethnic);
                bIsSearch = true;
            }
            if (!string.IsNullOrEmpty(ProvinceId))
            {
                q = q.Where(item => item.ProvinceId == ProvinceId);
                bIsSearch = true;
            }
            if (!string.IsNullOrEmpty(DistrictId))
            {
                q = q.Where(item => item.DistrictId == DistrictId);
                bIsSearch = true;
            }
            if (!string.IsNullOrEmpty(WardId))
            {
                q = q.Where(item => item.WardId == WardId);
                bIsSearch = true;
            }
            if (!string.IsNullOrEmpty(CountryId))
            {
                q = q.Where(item => item.CountryId == CountryId);
                bIsSearch = true;
            }
            if (!string.IsNullOrEmpty(Position))
            {
                q = q.Where(item => item.Position == Position);
                bIsSearch = true;
            }
            if (!string.IsNullOrEmpty(Religion))
            {
                q = q.Where(item => item.Religion == Religion);
                bIsSearch = true;
            }
            if (!string.IsNullOrEmpty(MaritalStatus))
            {
                q = q.Where(item => item.MaritalStatus == MaritalStatus);
                bIsSearch = true;
            }
            if (branchId != null && branchId.Value > 0)
            {
                q = q.Where(item => item.Sale_BranchId == branchId);
                bIsSearch = true;
            }
            if (DepartmentId != null && DepartmentId.Value > 0)
            {
                q = q.Where(item => item.BranchDepartmentId == DepartmentId);
                bIsSearch = true;
            }
            if (bIsSearch)
            {
                if (user.UserTypeId == 1)
                {
                    q = q.OrderByDescending(m => m.CreatedDate);
                }
                else
                {
                    q = q.Where(x => x.Sale_BranchId == user.BranchId).OrderByDescending(m => m.CreatedDate);
                }
            }
            else
            {
                if (Request["search"] != null)
                {
                    bIsSearch = true;
                    if (user.UserTypeId == 1)
                    {
                        q = q.OrderByDescending(m => m.CreatedDate);
                    }
                    else
                    {
                        q = q.Where(x => x.Sale_BranchId == user.BranchId).OrderByDescending(m => m.CreatedDate);
                    }
                }
                else
                {
                    q = null;
                }

            }
            if (q != null)
            {
                foreach (var item in q)
                {
                    item.PositionName = categoryRepository.GetAllCategories().Where(x => x.Code == "position" && x.Value == item.Position).FirstOrDefault().Name;
                    if (!string.IsNullOrEmpty(item.ProfileImage))
                    {
                        var originalDirectory = new DirectoryInfo(string.Format("{0}Data\\HinhSV\\", Server.MapPath(@"\")));

                        string pathString = System.IO.Path.Combine(originalDirectory.ToString());
                        item.ProfileImagePath = pathString + item.ProfileImage;
                        if (!System.IO.File.Exists(item.ProfileImagePath))
                        {
                            item.ProfileImagePath = "/assets/img/no-avatar.png";
                        }
                        else
                        {
                            item.ProfileImagePath = "/Data/HinhSV/" + item.ProfileImage;
                        }
                    }
                    else
                        if (string.IsNullOrEmpty(item.ProfileImage))//Đã có hình
                        {
                            item.ProfileImagePath = "/assets/img/no-avatar.png";
                        }
                }
            }

            ViewBag.Search = bIsSearch;
            ViewBag.SuccessMessage = TempData[Globals.SuccessMessageKey];
            ViewBag.FailedMessage = TempData[Globals.FailedMessageKey];
            return View(q);
        }
        [HttpPost]
        public ActionResult Staff()
        {
            string idDeleteAll = Request["staff-checkbox"];
            string[] arrDeleteId = idDeleteAll.Split(',');
            return RedirectToAction("Assign", "WorkSchedules", new { area = "Staff", Staff = idDeleteAll });
        }
        #endregion

        #region -- Assign --
        public ActionResult Assign(int? Id)
        {
            var model = new TimekeepingListViewModel();
            var timekeepingList = timekeepingListRepository.GetvwTimekeepingListById(Id.Value);
            AutoMapper.Mapper.Map(timekeepingList, model);
            var user = userRepository.GetUserById(WebSecurity.CurrentUserId);
            var holiday = holidayRepository.GetAllHolidays().AsEnumerable().ToList();
            var staff = StaffsRepository.GetvwAllStaffs().Where(x => x.BranchDepartmentId == timekeepingList.DepartmentId);
            ViewBag.staffList = staff;
            ViewBag.DayHoliday = holiday;
            DateTime aDateTime = new DateTime(timekeepingList.Year.Value, timekeepingList.Month.Value, 1);
            // Cộng thêm 1 tháng và trừ đi một ngày.
            DateTime retDateTime = aDateTime.AddMonths(1).AddDays(-1);
            ViewBag.aDateTime = aDateTime;
            ViewBag.retDateTime = retDateTime;
            var DayOff = categoryRepository.GetCategoryByCode("DayOffDefault").AsEnumerable().ToList();
            ViewBag.DayOff = DayOff;
            var ShiftsList = shiftsRepository.GetAllShifts().Where(x => x.CategoryShifts == timekeepingList.CategoryShifts).AsEnumerable();
            ViewBag.ShiftsList = ShiftsList;
            return View(model);
        }
        [HttpPost]
        public ActionResult Assign(FormCollection fc)
        {
            var model = new TimekeepingListViewModel();
            var Id = Request["timekeepingListId"];
            var timekeepingList = timekeepingListRepository.GetTimekeepingListById(Convert.ToInt32(Id));
            var holiday = holidayRepository.GetAllHolidays().AsEnumerable().ToList();
            var staff = StaffsRepository.GetvwAllStaffs().Where(x => x.BranchDepartmentId == timekeepingList.DepartmentId);
            ViewBag.staffList = staff;
            ViewBag.DayHoliday = holiday;
            DateTime aDateTime = new DateTime(timekeepingList.Year.Value, timekeepingList.Month.Value, 1);
            // Cộng thêm 1 tháng và trừ đi một ngày.
            DateTime retDateTime = aDateTime.AddMonths(1).AddDays(-1);
            ViewBag.aDateTime = aDateTime;
            ViewBag.retDateTime = retDateTime;
            var DayOff = categoryRepository.GetCategoryByCode("DayOffDefault").Where(x => x.Value == "True").AsEnumerable().ToList();
            ViewBag.DayOff = DayOff;
            var ShiftsList = shiftsRepository.GetAllShifts().Where(x => x.CategoryShifts == timekeepingList.CategoryShifts).AsEnumerable();
            ViewBag.ShiftsList = ShiftsList;

            var user = userRepository.GetUserById(WebSecurity.CurrentUserId);
            List<string> listIdShifts = new List<string>();
            if (Request["shifts_id"] != null)
                listIdShifts = Request["shifts_id"].Split(',').ToList();


            List<string> ListWorkSchedules = new List<string>();
            foreach (var item in staff)
            {
                List<string> listIdCurrent = WorkSchedulesRepository.GetAllWorkSchedules().AsEnumerable().Where(x => aDateTime <= x.Day && x.Day <= retDateTime && x.ShiftsId != null && x.StaffId == item.Id).Select(x => x.ShiftsId.ToString().Replace(x.ShiftsId.ToString(), x.StaffId + "-" + x.Day.Value.ToString("dd/MM/yyyy") + "-" + x.ShiftsId)).ToList();
                ListWorkSchedules = ListWorkSchedules.Union(listIdCurrent).ToList();
            }
            // query id new to insert (not in database)
            List<string> listIdNew = listIdShifts.Where(id1 => !ListWorkSchedules.Any(id2 => id2 == id1)).ToList();
            //query id to delete not listIdFromRequest
            List<string> listIdToDelete = ListWorkSchedules.Where(id1 => !listIdShifts.Any(id2 => id2 == id1)).ToList();

            foreach (var id in listIdNew)
            {
                var item = new WorkSchedules();
                item.CreatedUserId = WebSecurity.CurrentUserId;
                item.ModifiedUserId = WebSecurity.CurrentUserId;
                item.CreatedDate = DateTime.Now;
                item.ModifiedDate = DateTime.Now;
                item.IsDeleted = false;
                string[] arrVal = id.Split('-');
                item.StaffId = int.Parse(arrVal[0], CultureInfo.InstalledUICulture);
                item.Day = DateTime.ParseExact(arrVal[1], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                item.ShiftsId = int.Parse(arrVal[2], CultureInfo.InvariantCulture);
                item.TimekeepingListId = Convert.ToInt32(Id);
                WorkSchedulesRepository.InsertWorkSchedules(item);
            }
            foreach (var id in listIdToDelete)
            {
                string[] arrVal = id.Split('-');
                int StaffId = int.Parse(arrVal[0], CultureInfo.InstalledUICulture);
                string Day = arrVal[1];
                int ShiftsId = int.Parse(arrVal[2], CultureInfo.InvariantCulture);
                WorkSchedulesRepository.Delete(Day, StaffId, ShiftsId);
            }
            timekeepingList.Status = "timekeeping";
            timekeepingListRepository.UpdateTimekeepingList(timekeepingList);
            // TempData[Globals.SuccessMessageKey] = "Lưu phân công ca làm việc thành công";
            return RedirectToAction("Detail", "TimekeepingList", new { area = "Staff", Id = Convert.ToInt32(Id) });
        }
        #endregion

        #region Delete
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            try
            {

                var item = WorkSchedulesRepository.GetWorkSchedulesById(id.Value);
                if (item != null)
                {
                    //if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                    //{
                    //    TempData["FailedMessage"] = "NotOwner";
                    //    return RedirectToAction("Staff");
                    //}
                    //item.IsDeleted = true;
                    //TechniqueRepository.UpdateTechnique(item);
                    WorkSchedulesRepository.DeleteWorkSchedules(id.Value);
                }
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.DeleteSuccess;
                return Content("");
            }
            catch (DbUpdateException)
            {
                TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.RelationError;
                return RedirectToAction("Staff");
            }
        }
        #endregion

        #region CheckWorkSchedules
        public ActionResult CheckWorkSchedules(int? shifts, string DayWork, int? StaffId)
        {
            int workSchedules = WorkSchedulesRepository.GetAllWorkSchedules().AsEnumerable().Where(x => x.ShiftsId == shifts && x.Day.Value == Convert.ToDateTime(DayWork) && x.StaffId == StaffId).Count();
            if (workSchedules <= 0)
            {

                return Content("success");
            }
            else
            {

                return Content("error");
            }
        }
        #endregion

        #region AddWorkSchedules

        [HttpPost]
        public ActionResult AddWorkSchedules(int? shifts, string DayWork, int? StaffId)
        {
            var workSchedules = WorkSchedulesRepository.GetAllWorkSchedules().AsEnumerable().Where(x => x.ShiftsId == shifts && x.Day.Value == Convert.ToDateTime(DayWork) && x.StaffId == StaffId);
            vwTimekeepingViewModel model = new vwTimekeepingViewModel();
            WorkSchedules work = new WorkSchedules();
            if (workSchedules.Count() <= 0)
            {
                work.IsDeleted = false;
                work.CreatedDate = DateTime.Now;
                work.ModifiedDate = DateTime.Now;
                work.StaffId = StaffId;
                work.ShiftsId = shifts;
                work.Day = Convert.ToDateTime(DayWork);
                WorkSchedulesRepository.InsertWorkSchedules(work);
                var q = timekeepingRepository.GetvwTimekeepingByWorkSchedulesId(work.Id);
                AutoMapper.Mapper.Map(q, model);
            }
            else
            {
                var q = timekeepingRepository.GetvwTimekeepingByWorkSchedulesId(workSchedules.FirstOrDefault().Id);
                AutoMapper.Mapper.Map(q, model);
            }

            if (string.IsNullOrEmpty(model.HoursIn.ToString()) && string.IsNullOrEmpty(model.HoursOut.ToString()))
            {
                var dayoff = dayoffRepository.GetAllvwDayOff().Where(x => x.StaffId == model.StaffId && x.DayStart <= model.DayWork && model.DayWork <= x.DayEnd);
                foreach (var i in dayoff)
                {
                    //model.Pay = i.Pay;
                    model.DayOff = "P";
                    model.IdDayOff = i.Id;
                    model.DayOffName = i.NameSymbol;
                    model.DayOffCode = i.CodeSymbol;
                }

                if (model.DayWork.Value.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy") && string.IsNullOrEmpty(model.DayOff))
                {
                    model.DayOff = "DL";
                    //  model.DayOffTooltip = "Đang làm";
                }
                if (model.DayWork < DateTime.Now && string.IsNullOrEmpty(model.DayOff))
                {
                    model.DayOff = "KP";
                    // model.DayOffTooltip = "Nghỉ không phép";
                }
                if (model.DayWork > DateTime.Now && string.IsNullOrEmpty(model.DayOff))
                {
                    model.DayOff = "CL";
                    //  model.DayOffTooltip = "Chưa làm";
                }

            }

            return View(model);
        }
        #endregion
    }
}
