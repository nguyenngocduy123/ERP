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

namespace Erp.BackOffice.Staff.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class DayOffController : Controller
    {
        private readonly IDayOffRepository DayOffRepository;
        private readonly IUserRepository userRepository;
        private readonly ISymbolTimekeepingRepository typeDayOffRepository;
        private readonly IWorkSchedulesRepository workSchedulesRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IHolidaysRepository holidayRepository;
        private readonly IStaffsRepository staffRepository;
        public DayOffController(
            IDayOffRepository _DayOff
            , IUserRepository _user
            , ISymbolTimekeepingRepository typeDayOff
            , IWorkSchedulesRepository workSchedules
            , ICategoryRepository category
            , IHolidaysRepository holiday
            ,IStaffsRepository staff
            )
        {
            DayOffRepository = _DayOff;
            userRepository = _user;
            typeDayOffRepository = typeDayOff;
            workSchedulesRepository = workSchedules;
            categoryRepository = category;
            holidayRepository = holiday;
            staffRepository = staff;
        }

        #region Index
        public ViewResult Index(string Name, string Code, int? TypeDayOff, int? branchId, int? DepartmentId, string start_date, string end_date)
        {
            var user = userRepository.GetUserById(WebSecurity.CurrentUserId);
            IEnumerable<DayOffViewModel> q = DayOffRepository.GetAllvwDayOff()
                .Select(item => new DayOffViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    DayEnd = item.DayEnd,
                    DayStart = item.DayStart,
                    Quantity = item.Quantity,
                    QuantityNotUsed = item.QuantityNotUsed,
                    StaffId = item.StaffId,
                    TypeDayOffId = item.TypeDayOffId,
                    NameSymbol = item.NameSymbol,
                    BranchName=item.BranchName,
                    DepartmentName=item.DepartmentName,
                    CodeStaff=item.CodeStaff,
                    NameStaff=item.NameStaff,
                    TypeDayOffQuantity = item.TypeDayOffQuantity,
                    Code = item.Code,
                    ProfileImage=item.ProfileImage,
                    CodeSymbol=item.CodeSymbol,
                    BranchDepartmentId=item.BranchDepartmentId,
                    Sale_BranchId=item.Sale_BranchId
                });

            if (!string.IsNullOrEmpty(Code))
            {
                q = q.Where(item => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.CodeStaff).ToLower().Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(Code).ToLower()));
            }

            if (!string.IsNullOrEmpty(Name))
            {
                q = q.Where(item => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.NameStaff).ToLower().Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(Name).ToLower()));
            }
            if (TypeDayOff != null && TypeDayOff.Value > 0)
            {
                q = q.Where(item => item.TypeDayOffId == TypeDayOff);
            }
            if (branchId != null && branchId.Value > 0)
            {
                q = q.Where(item => item.Sale_BranchId == branchId);
            }
            if (DepartmentId != null && DepartmentId.Value > 0)
            {
                q = q.Where(item => item.BranchDepartmentId == DepartmentId);
            }
            if (!string.IsNullOrEmpty(start_date) && !string.IsNullOrEmpty(end_date))
            {
                DateTime start_d;
                if (DateTime.TryParseExact(start_date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out start_d))
                {
                    DateTime end_d;
                    if (DateTime.TryParseExact(end_date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out end_d))
                    {
                        end_d = end_d.AddHours(23);
                        q = q.Where(x => start_d <= x.DayStart && x.DayEnd <= end_d);
                    }
                }
            }
            if (user.UserTypeId == 1)
            {
                q = q.OrderByDescending(m => m.CreatedDate);
            }
            else
            {
                q = q.Where(x => x.Sale_BranchId == user.BranchId).OrderByDescending(m => m.CreatedDate);
            }
            return View(q);
        }
        #endregion

        #region List

        public ViewResult List(int? StaffId)
        {
            ViewBag.StaffId = StaffId;
            IQueryable<DayOffViewModel> q = DayOffRepository.GetAllvwDayOff().Where(x => x.StaffId == StaffId)
                .Select(item => new DayOffViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    DayEnd = item.DayEnd,
                    DayStart = item.DayStart,
                    Quantity = item.Quantity,
                    QuantityNotUsed = item.QuantityNotUsed,
                    StaffId = item.StaffId,
                    TypeDayOffId = item.TypeDayOffId,
                    NameSymbol = item.NameSymbol,
                    //Pay=item.Pay,
                    TypeDayOffQuantity = item.TypeDayOffQuantity,
                    Code=item.Code,
                    CodeStaff=item.CodeStaff,
                    CodeSymbol=item.CodeSymbol
                }).OrderByDescending(m => m.ModifiedDate);


            ViewBag.AccessRightCreate = SecurityFilter.AccessRight("Create", "DayOff", "Staff");
            return View(q);
        }
        #endregion

        #region Create
        public ViewResult Create(int? StaffId, DateTime? DayWork)
        {
            var model = new DayOffViewModel();
            model.StaffId = StaffId;
            model.DayStart = DayWork;
            model.DayEnd = DayWork;
            var staffList = staffRepository.GetvwAllStaffs()
             .Select(item => new StaffsViewModel
             {
                 Code = item.Code,
                 Name = item.Name,
                 Id = item.Id,
              //   BranchName = item.BranchName,
                 ProfileImage=item.ProfileImage
             });
            ViewBag.staffList = staffList;
            //model.TypeList = Helpers.SelectListHelper.GetSelectList_TypeDayOff(null);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(DayOffViewModel model)
        {
            var urlRefer = Request["UrlReferrer"];
            var staffList = staffRepository.GetvwAllStaffs()
           .Select(item => new StaffsViewModel
           {
               Code = item.Code,
               Name = item.Name,
               Id = item.Id,
               BranchName = item.BranchName,
               ProfileImage = item.ProfileImage
           });
            ViewBag.staffList = staffList;
            if (ModelState.IsValid)
            {
                model.TypeList = Helpers.SelectListHelper.GetSelectList_SymbolTimekeeping(null, App_GlobalResources.Wording.Empty, true);
                var DayOff = new Domain.Staff.Entities.DayOff();
                AutoMapper.Mapper.Map(model, DayOff);
                DayOff.IsDeleted = false;
                DayOff.CreatedUserId = WebSecurity.CurrentUserId;
                DayOff.ModifiedUserId = WebSecurity.CurrentUserId;
                DayOff.CreatedDate = DateTime.Now;
                DayOff.ModifiedDate = DateTime.Now;
                var dates = new List<DateTime>();
                //lấy danh sách các ngày nghỉ trong tuần, theo quy định từng công ty.
                var off = categoryRepository.GetCategoryByCode("DayOffDefault").Where(x => x.Value == "True").ToList();
                var holiday = holidayRepository.GetAllHolidays().ToList();
                for (DateTime dt = model.DayStart.Value; dt <= model.DayEnd.Value; dt = dt.AddDays(1))
                {
                    //không tính ngày lễ , ngày nghỉ cuối tuần vào trong ngày xin nghỉ phép.
                    if (holiday.Where(x => x.DayStart <= dt && dt <= x.DayEnd).Count() <= 0)
                    {
                        if (off.Where(x => x.OrderNo.Value == (int)dt.DayOfWeek).Count() <= 0)
                        {
                            dates.Add(dt);
                        }
                    }
                }
                DayOff.Quantity = dates.Count();
                var typeDayOff = typeDayOffRepository.GetSymbolTimekeepingById(model.TypeDayOffId.Value);
                if (typeDayOff.DayOff == true)
                {
                    //lấy danh sách đơn xin nghỉ phép của nhân viên trong năm thuộc loại phép đang xin nghỉ
                    var dayoff = DayOffRepository.GetAllDayOff().AsEnumerable().Where(x => x.TypeDayOffId == model.TypeDayOffId && x.DayStart.Value.ToString("yyyy") == model.DayStart.Value.ToString("yyyy") && x.StaffId == model.StaffId).ToList();
                    int QuantityDayOff = 0;
                    if (dayoff.Count() > 0)
                    {
                        foreach (var item in dayoff)
                        {
                            QuantityDayOff += item.Quantity.Value;
                        }
                    }
                    DayOff.QuantityNotUsed = typeDayOff.Quantity - (QuantityDayOff + DayOff.Quantity);
                    if (DayOff.QuantityNotUsed < 0)
                    {
                        TempData[Globals.FailedMessageKey] = "Số ngày nghỉ" + model.NameSymbol + " còn lại của bạn không đủ";
                        if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                            ViewBag.closePopup = "true";
                        ViewBag.urlRefer = urlRefer;
                        return View(model);
                    }
                }
                DayOffRepository.InsertDayOff(DayOff);
                //tạo mã tự động 
                var prefix2 = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_DayOff");
                DayOff.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix2, DayOff.Id);
                DayOffRepository.UpdateDayOff(DayOff);
                //var workShedules=workSchedulesRepository.GetvwAllWorkSchedules().Any(dates).Where(x=>x.Day==item.ToShortDateString())
                if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                {
                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                    ViewBag.closePopup = "true";
                    model.Id = DayOff.Id;
                    ViewBag.urlRefer = urlRefer;
                    return View(model);
                }
                return Redirect(urlRefer);
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var DayOff = DayOffRepository.GetvwDayOffById(Id.Value);
            if (DayOff != null && DayOff.IsDeleted != true)
            {
                var model = new DayOffViewModel();
                AutoMapper.Mapper.Map(DayOff, model);

                //model.TypeList = Helpers.SelectListHelper.GetSelectList_TypeDayOff(model.TypeDayOffId);
                //if (model.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
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
        public ActionResult Edit(DayOffViewModel model)
        {
            if (ModelState.IsValid)
            {
                var urlRefer = Request["UrlReferrer"];
                var DayOff = DayOffRepository.GetDayOffById(model.Id);
                if (Request["Submit"] == "Save")
                {
                    AutoMapper.Mapper.Map(model, DayOff);
                    DayOff.ModifiedUserId = WebSecurity.CurrentUserId;
                    DayOff.ModifiedDate = DateTime.Now;
                    var dates = new List<DateTime>();
                    for (DateTime dt = model.DayStart.Value; dt <= model.DayEnd.Value; dt = dt.AddDays(1))
                    {
                        dates.Add(dt);
                    }
                    DayOff.Quantity = dates.Count();
                    var typeDayOff = typeDayOffRepository.GetSymbolTimekeepingById(model.TypeDayOffId.Value);

                    var dayoff = DayOffRepository.GetAllDayOff().AsEnumerable().Where(x => x.TypeDayOffId == model.TypeDayOffId && x.DayStart.Value.ToString("yyyy") == model.DayStart.Value.ToString("yyyy") && x.StaffId == model.StaffId).ToList();
                    int QuantityDayOff = 0;
                    if (dayoff.Count() > 0)
                    {
                        foreach (var item in dayoff)
                        {
                            if (item.Id != model.Id)
                            {
                                QuantityDayOff += item.Quantity.Value;
                            }
                        }
                    }
                    DayOff.QuantityNotUsed = typeDayOff.Quantity - (QuantityDayOff + DayOff.Quantity);
                    if (DayOff.QuantityNotUsed < 0)
                    {
                        TempData[Globals.FailedMessageKey] = "Số ngày nghỉ" + model.NameSymbol + " còn lại của bạn không đủ";
                        if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                            ViewBag.closePopup = "true";
                        ViewBag.urlRefer = urlRefer;
                        return View(model);
                    }
                    else
                    {
                        DayOffRepository.UpdateDayOff(DayOff);
                        if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                        {
                            TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                            ViewBag.closePopup = "true";
                            ViewBag.urlRefer = urlRefer;
                            return View(model);
                        }
                    }
                }
                return Redirect(urlRefer);
            }
            return View(model);
        }

        #endregion

        #region Delete
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            try
            {

                var item = DayOffRepository.GetDayOffById(id.Value);
                if (item != null)
                {
                    //if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                    //{
                    //    TempData["FailedMessage"] = "NotOwner";
                    //    return RedirectToAction("Index");
                    //}
                    //item.IsDeleted = true;
                    //TechniqueRepository.UpdateTechnique(item);
                    DayOffRepository.DeleteDayOff(id.Value);
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

        #region AddTimekeeping
        public ViewResult AddTimekeeping(int? StaffId, DateTime? DayWork)
        {
            var model = new DayOffViewModel();
            model.StaffId = StaffId;
            model.DayStart = DayWork;
            model.DayEnd = DayWork;

            //model.TypeList = Helpers.SelectListHelper.GetSelectList_TypeDayOff(null);
            return View(model);
        }

        [HttpPost]
        public ActionResult AddTimekeeping(DayOffViewModel model)
        {
            var urlRefer = Request["UrlReferrer"];

            if (ModelState.IsValid)
            {
                model.TypeList = Helpers.SelectListHelper.GetSelectList_SymbolTimekeeping(null, App_GlobalResources.Wording.Empty, true);
                var DayOff = new Domain.Staff.Entities.DayOff();
                AutoMapper.Mapper.Map(model, DayOff);
                DayOff.IsDeleted = false;
                DayOff.CreatedUserId = WebSecurity.CurrentUserId;
                DayOff.ModifiedUserId = WebSecurity.CurrentUserId;
                DayOff.CreatedDate = DateTime.Now;
                DayOff.ModifiedDate = DateTime.Now;
                var dates = new List<DateTime>();
                for (DateTime dt = model.DayStart.Value; dt <= model.DayEnd.Value; dt = dt.AddDays(1))
                {
                    dates.Add(dt);
                }
                DayOff.Quantity = dates.Count();
                var typeDayOff = typeDayOffRepository.GetSymbolTimekeepingById(model.TypeDayOffId.Value);
                if (typeDayOff.DayOff == true)
                {
                    var dayoff = DayOffRepository.GetAllDayOff().AsEnumerable().Where(x => x.TypeDayOffId == model.TypeDayOffId && x.DayStart.Value.ToString("yyyy") == model.DayStart.Value.ToString("yyyy") && x.StaffId == model.StaffId).ToList();
                    int QuantityDayOff = 0;
                    if (dayoff.Count() > 0)
                    {
                        foreach (var item in dayoff)
                        {
                            QuantityDayOff += item.Quantity.Value;
                        }
                    }
                    DayOff.QuantityNotUsed = typeDayOff.Quantity - (QuantityDayOff + DayOff.Quantity);
                    if (DayOff.QuantityNotUsed < 0)
                    {
                        TempData[Globals.FailedMessageKey] = "Số ngày nghỉ" + model.NameSymbol + " còn lại của bạn không đủ";
                        if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                            ViewBag.closePopup = "true";
                        ViewBag.urlRefer = urlRefer;
                        return View(model);
                    }
                }
                else
                {
                    DayOffRepository.InsertDayOff(DayOff);
                    if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                    {
                        TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                        ViewBag.closePopup = "true";
                        model.Id = DayOff.Id;
                        ViewBag.urlRefer = urlRefer;
                        return View(model);
                    }
                }
                return Redirect(urlRefer);
            }
            return View(model);
        }
        #endregion
    }
}
