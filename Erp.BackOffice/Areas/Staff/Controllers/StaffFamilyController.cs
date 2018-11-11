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
using Erp.BackOffice.App_GlobalResources;

namespace Erp.BackOffice.Staff.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class StaffFamilyController : Controller
    {
        private readonly IStaffFamilyRepository StaffFamilyRepository;
        private readonly IUserRepository userRepository;
        private readonly IStaffsRepository staffsRepository;

        public StaffFamilyController(
            IStaffFamilyRepository _StaffFamily
            , IUserRepository _user
            , IStaffsRepository _staff
            )
        {
            StaffFamilyRepository = _StaffFamily;
            userRepository = _user;
            staffsRepository = _staff;
        }

        #region Index

        public ViewResult Index(int? StaffId)
        {
            ViewBag.StaffId = StaffId;
            IQueryable<StaffFamilyViewModel> q = StaffFamilyRepository.GetAllStaffFamily().Where(x => x.StaffId == StaffId)
                .Select(item => new StaffFamilyViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    StaffId = item.StaffId,
                    Address = item.Address,
                    Birthday = item.Birthday,
                    Correlative = item.Correlative,
                    Gender = item.Gender,
                    Phone = item.Phone,
                    IsDependencies = item.IsDependencies

                }).OrderByDescending(m => m.ModifiedDate);

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            ViewBag.AccessRightCreate = SecurityFilter.AccessRight("Create", "StaffFamily", "Staff");
            //ViewBag.AccessRightEdit = SecurityFilter.AccessRight("Edit", "Technique", "Staff");
            //ViewBag.AccessRightDelete = SecurityFilter.AccessRight("Delete", "Technique", "Staff");
            return View(q);
        }
        #endregion

        #region Create
        public ViewResult Create(int? StaffId)
        {
            var model = new StaffFamilyViewModel();
            model.StaffId = StaffId;
            //model.GenderList = Helpers.SelectListHelper.GetSelectList_Gender(null);
            //model.CorrelativeList = Helpers.SelectListHelper.GetSelectList_Category("Correlative", null, "Name",App_GlobalResources.Wording.Empty);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(StaffFamilyViewModel model)
        {
            if (ModelState.IsValid)
            {
                var StaffFamily = new Domain.Staff.Entities.StaffFamily();
                AutoMapper.Mapper.Map(model, StaffFamily);
                StaffFamily.IsDeleted = false;
                StaffFamily.CreatedUserId = WebSecurity.CurrentUserId;
                StaffFamily.ModifiedUserId = WebSecurity.CurrentUserId;
                StaffFamily.CreatedDate = DateTime.Now;
                StaffFamily.ModifiedDate = DateTime.Now;
                StaffFamilyRepository.InsertStaffFamily(StaffFamily);

                if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                {
                    ViewBag.closePopup = "true";
                    model.Id = StaffFamily.Id;
                    //model.GenderList = Helpers.SelectListHelper.GetSelectList_Gender(null);
                    //model.CorrelativeList = Helpers.SelectListHelper.GetSelectList_Category("Correlative", null, "Name", App_GlobalResources.Wording.Empty);
                    return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
                    //return View(model);
                }

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Detail", "Staffs", new { area = "Staff", Id = model.StaffId });

            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var StaffFamily = StaffFamilyRepository.GetStaffFamilyById(Id.Value);
            if (StaffFamily != null && StaffFamily.IsDeleted != true)
            {
                var model = new StaffFamilyViewModel();
                AutoMapper.Mapper.Map(StaffFamily, model);
                model.GenderList = Helpers.SelectListHelper.GetSelectList_Gender(model.Gender != null && model.Gender.Value ? true : false);
                //model.CorrelativeList = Helpers.SelectListHelper.GetSelectList_Category("Correlative", model.Correlative, "Name", App_GlobalResources.Wording.Empty);
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
        public ActionResult Edit(StaffFamilyViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var StaffFamily = StaffFamilyRepository.GetStaffFamilyById(model.Id);
                    AutoMapper.Mapper.Map(model, StaffFamily);
                    StaffFamily.ModifiedUserId = WebSecurity.CurrentUserId;
                    StaffFamily.ModifiedDate = DateTime.Now;
                    StaffFamilyRepository.UpdateStaffFamily(StaffFamily);
                    if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                    {
                        ViewBag.closePopup = "true";
                        model.Id = StaffFamily.Id;
                        model.GenderList = Helpers.SelectListHelper.GetSelectList_Gender(model.Gender != null && model.Gender.Value ? true : false);
                        //model.CorrelativeList = Helpers.SelectListHelper.GetSelectList_Category("Correlative", model.Correlative, "Name", App_GlobalResources.Wording.Empty);
                        return View(model);
                    }
                    //TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                    //return RedirectToAction("Detail", "Staffs", new { area = "Staff", Id = model.StaffId });
                }

                return View(model);
            }

            return View(model);

            //if (Request.UrlReferrer != null)
            //    return Redirect(Request.UrlReferrer.AbsoluteUri);
            //return RedirectToAction("Index");
        }

        #endregion

        #region Delete
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            try
            {

                var item = StaffFamilyRepository.GetStaffFamilyById(id.Value);
                if (item != null)
                {
                    //if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                    //{
                    //    TempData["FailedMessage"] = "NotOwner";
                    //    return RedirectToAction("Index");
                    //}

                    item.IsDeleted = true;
                    StaffFamilyRepository.UpdateStaffFamily(item);
                }
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.DeleteSuccess;
                return RedirectToAction("Detail", "Staffs", new { area = "Staff", Id = item.StaffId });
            }
            catch (DbUpdateException)
            {
                TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.RelationError;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult DeleteAll()
        {
            try
            {
                string idDeleteAll = Request["DeleteId-checkbox"];
                string[] arrDeleteId = idDeleteAll.Split(',');
                for (int i = 0; i < arrDeleteId.Count(); i++)
                {
                    var item = StaffFamilyRepository.GetStaffFamilyById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        {
                            TempData["FailedMessage"] = "NotOwner";
                            return RedirectToAction("Index");
                        }

                        item.IsDeleted = true;
                        StaffFamilyRepository.UpdateStaffFamily(item);
                    }
                }
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.DeleteSuccess;
                return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });

            }
            catch (DbUpdateException)
            {
                TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.RelationError;
                return RedirectToAction("Index");
            }
        }

        #endregion

        #region Hoàn cảnh gia đình

        public ActionResult ListFamily(string Name, string Code)
        {
            //Danh sách Staff
            List<StaffsViewModel> q = staffsRepository.GetvwAllStaffs()
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
                 //   BranchName = item.BranchName,
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
               //     Sale_BranchId = item.Sale_BranchId,
                    WardId = item.WardId,
                //    Staff_DepartmentId = item.Staff_DepartmentId,
                    Religion = item.Religion,
                    IdCardNumber = item.IdCardNumber,
                //    Position = item.Position,
                    WardName = item.WardName,
                    MaritalStatus = item.MaritalStatus,
                    Email2 = item.Email2,
                    Phone2 = item.Phone2,
                    PositionName = item.PositionName
                }).ToList();

            var AllStaffFamily = StaffFamilyRepository.GetAllStaffFamily().ToList();
            //Danh sách nhân viên có gia đình
            var staff_is_family = StaffFamilyRepository.GetAllStaffFamily().Select(n => n.StaffId).Distinct().ToList();
            if (staff_is_family != null && staff_is_family.Count > 0)
            {
                //Lấy danh sách staff
                q = q.Where(n => staff_is_family.Contains(n.Id)).ToList();
                if (!string.IsNullOrEmpty(Name))
                {
                    q = q.Where(n => n.Name.Contains(Name)).ToList();
                }
                if (!string.IsNullOrEmpty(Code))
                {
                    q = q.Where(n => n.Code.Contains(Code)).ToList();
                }

                //Thêm ListStaffFamily cho từng nhân viên
                for (int i = 0; i < q.Count; i++)
                {
                    int count = AllStaffFamily.Where(n => n.StaffId == q[i].Id).Count();
                    q[i].CountFamily = count;
                }
                
                return View(q);
            }
            q = new List<StaffsViewModel>();
            return View(q);
        }

        public ActionResult CreateListFamily()
        {
            var model = new StaffFamilyViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateListFamily(StaffFamilyViewModel model)
        {
            if (ModelState.IsValid)
            {
                var StaffFamily = new Domain.Staff.Entities.StaffFamily();
                AutoMapper.Mapper.Map(model, StaffFamily);
                StaffFamily.IsDeleted = false;
                StaffFamily.CreatedUserId = WebSecurity.CurrentUserId;
                StaffFamily.ModifiedUserId = WebSecurity.CurrentUserId;
                StaffFamily.CreatedDate = DateTime.Now;
                StaffFamily.ModifiedDate = DateTime.Now;
                StaffFamilyRepository.InsertStaffFamily(StaffFamily);

                if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                {
                    ViewBag.closePopup = "true";
                    model.Id = StaffFamily.Id;

                    return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
                }

            }
            return View(model);
        }

        public ActionResult DetailList(int? staffId)
        {
            ViewBag.AccessRightCreate = SecurityFilter.AccessRight("Create", "StaffFamily", "Staff");
            if (staffId != null)
            {
                ViewBag.Staff = staffsRepository.GetvwStaffsById(staffId.Value);

                IEnumerable<StaffFamilyViewModel> q = StaffFamilyRepository.GetAllStaffFamily().Where(x => x.StaffId == staffId)
                    .Select(item => new StaffFamilyViewModel
                    {
                        Id = item.Id,
                        CreatedUserId = item.CreatedUserId,
                        CreatedDate = item.CreatedDate,
                        ModifiedUserId = item.ModifiedUserId,
                        ModifiedDate = item.ModifiedDate,
                        Name = item.Name,
                        StaffId = item.StaffId,
                        Address = item.Address,
                        Birthday = item.Birthday,
                        Correlative = item.Correlative,
                        Gender = item.Gender,
                        Phone = item.Phone,
                        IsDependencies = item.IsDependencies

                    }).OrderByDescending(m => m.ModifiedDate).ToList();

                return View(q);
            }
            return View();
        }

        #endregion
    }
}
