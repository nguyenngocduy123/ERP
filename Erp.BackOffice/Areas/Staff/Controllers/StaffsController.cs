using Erp.BackOffice.Filters;
using Erp.BackOffice.Staff.Models;
using Erp.Domain.Account.Interfaces;
using Erp.Domain.Entities;
using Erp.Domain.Interfaces;
using Erp.Domain.Sale.Interfaces;
using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using Erp.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebMatrix.WebData;

namespace Erp.BackOffice.Staff.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class StaffsController : Controller
    {
        private readonly ICustomerRepository CustomerRepository;
        private readonly IStaffsRepository StaffsRepository;
        private readonly IUserRepository userRepository;
        private readonly ILocationRepository locationRepository;
        private readonly IStaffFamilyRepository staffFamilyRepository;
        private readonly IBankRepository bankRepository;
        private readonly IBranchDepartmentRepository branchDepartmentRepository;
        private readonly ICategoryRepository CategoryRepository;
        private readonly IWorkSchedulesRepository workSchedulesRepository;
        private readonly IStaffSocialInsuranceRepository staffSocialInsuranceRepository;
        private readonly ISalaryAdvanceRepository salaryAdvanceRepository;
        private readonly ICommisionStaffRepository commisionStaffRepository;
        private readonly IProcessPayRepository processPayRepository;
        private readonly IDayOffRepository dayoffRepository;
        private readonly ISymbolTimekeepingRepository symbolTimekeepingRepository;
        private readonly IRegisterForOvertimeRepository registerForOvertimeRepository;
        private readonly ITransferWorkRepository transferWorkRepository;
        private readonly IWorkingProcessRepository workingProcessRepository;
        private readonly ITechniqueRepository techniqueRepository;
        private readonly ILabourContractRepository labourContractRepository;
        private readonly ICheckInOutRepository checkInOutRepository;
        private readonly IBonusDisciplineRepository bonusDisciplineRepository;
        private readonly IFPMachineRepository fPMachineRepository;
        private readonly IBranchRepository branchRepository;
        private readonly IUserTypeRepository user_typeRepository;
        private readonly IPositionRepository positionRepository;
        public StaffsController(
            IStaffsRepository _Staffs
            , IUserRepository _user
            , ILocationRepository location
            , IStaffFamilyRepository staffFamily
            , IBankRepository bank
            , IBranchDepartmentRepository branchDepartment
            , ICategoryRepository category
            , IWorkSchedulesRepository workSchedules
            , IStaffSocialInsuranceRepository staffSocialInsurance
            , ISalaryAdvanceRepository salaryAdvance
            , ICommisionStaffRepository commisionStaff
            , IProcessPayRepository processPay
            , IDayOffRepository dayoff
            , ISymbolTimekeepingRepository symbolTimekeeping
            , IRegisterForOvertimeRepository registerForOvertime
            , ITransferWorkRepository transferWork
            , IWorkingProcessRepository workingProcess
            , ITechniqueRepository technique
            , ILabourContractRepository labourContract
            , ICheckInOutRepository checkInOut
            , IBonusDisciplineRepository bonusDiscipline
            , IFPMachineRepository FPMachine
            , IBranchRepository branch
            , IUserTypeRepository user_type
            , IPositionRepository position
            )
        {
            StaffsRepository = _Staffs;
            userRepository = _user;
            locationRepository = location;
            bankRepository = bank;
            branchDepartmentRepository = branchDepartment;
            CategoryRepository = category;
            workSchedulesRepository = workSchedules;
            staffSocialInsuranceRepository = staffSocialInsurance;
            salaryAdvanceRepository = salaryAdvance;
            commisionStaffRepository = commisionStaff;
            processPayRepository = processPay;
            dayoffRepository = dayoff;
            symbolTimekeepingRepository = symbolTimekeeping;
            registerForOvertimeRepository = registerForOvertime;
            staffFamilyRepository = staffFamily;
            transferWorkRepository = transferWork;
            workingProcessRepository = workingProcess;
            techniqueRepository = technique;
            labourContractRepository = labourContract;
            checkInOutRepository = checkInOut;
            bonusDisciplineRepository = bonusDiscipline;
            fPMachineRepository = FPMachine;
            branchRepository = branch;
            user_typeRepository = user_type;
            positionRepository = position;
        }

        #region Index

        public ActionResult Index(string BranchId)
        {
            var list = StaffsRepository.GetvwAllStaffs();//.Where(x => x.Sale_BranchId == BranchId.Value);
            IQueryable<StaffsViewModel> q = list.Select(item => new StaffsViewModel
            {
                Id = item.Id,
                CreatedUserId = item.CreatedUserId,
                CreatedDate = item.CreatedDate,
                ModifiedUserId = item.ModifiedUserId,
                ModifiedDate = item.ModifiedDate,
                Name = item.Name,
                Birthday = item.Birthday,
                ProfileImage = item.ProfileImage,
                Address = item.Address,
                Gender = item.Gender,
                Ethnic = item.Ethnic,
                Phone = item.Phone,
                Email = item.Email,
                Code = item.Code,
                DistrictId = item.DistrictId,
                IdCardDate = item.IdCardDate,
                IdCardIssued = item.IdCardIssued,
                IdCardNumber = item.IdCardNumber,
                Language = item.Language,
                Literacy = item.Literacy,
                MaritalStatus = item.MaritalStatus,
                PositionId = item.PositionId,
                Religion = item.Religion,
                WardId = item.WardId,
                Technique = item.Technique,
                ProvinceId = item.ProvinceId,
                //   BranchName = item.BranchName,
                CardIssuedName = item.CardIssuedName,
                GenderName = item.GenderName,
                PositionName = item.PositionName,
                PositionCode = item.PositionCode
            }).OrderBy(x => x.Code);

            var model = new StaffListViewModel();
            model.ListStaffsViewModel = q;

            ViewBag.AccessRightCreate = SecurityFilter.AccessRight("Create", "Staffs", "Staff");
            ViewBag.AccessRightEdit = SecurityFilter.AccessRight("Edit", "Staffs", "Staff");
            ViewBag.AccessRightDelete = SecurityFilter.AccessRight("Delete", "Staffs", "Staff");
            ViewBag.AlertMessage = TempData["AlertMessage"];
            ViewBag.BranchId = BranchId;

            return View(model);
        }
        #endregion

        #region Search
        public ViewResult Search(string Code, string Name, string Ethnic, string ProvinceId, string DistrictId, string WardId, string CountryId, int? PositionId, string Religion, string MaritalStatus, int? branchId, int? DepartmentId, string module_list)
        {
            ViewData["Code"] = Code;
            ViewData["Name"] = Name;
            ViewData["CountryList"] = Helpers.SelectListHelper.GetSelectList_Category("country", null, "Value", "Quốc tịch");
            ViewData["ProvinceList"] = Helpers.SelectListHelper.GetSelectList_Location("0", null, "Thành phố/Tỉnh");
            ViewData["DistrictList"] = Helpers.SelectListHelper.GetSelectList_Location(null, null, "Quận/Huyện");
            ViewData["WardList"] = Helpers.SelectListHelper.GetSelectList_Location(null, null, "Phường/Xã");
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
                     //BranchCode = item.BranchCode,
                     //BranchDepartmentId = item.BranchDepartmentId,
                     GenderName = item.GenderName,
                     Gender = item.Gender,
                     Ethnic = item.Ethnic,
                     //BranchName = item.BranchName,
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
                     //Sale_BranchId = item.Sale_BranchId,
                     WardId = item.WardId,
                     //Staff_DepartmentId = item.Staff_DepartmentId,
                     Religion = item.Religion,
                     IdCardNumber = item.IdCardNumber,
                     //Position = item.Position,
                     WardName = item.WardName,
                     MaritalStatus = item.MaritalStatus,
                     Email2 = item.Email2,
                     Phone2 = item.Phone2,
                     PositionName = item.PositionName,
                     IsWorking = item.IsWorking,
                     CommissionPercent = item.CommissionPercent,
                     DrugStore = item.DrugStore,
                     MinimumRevenue = item.MinimumRevenue,
                     UserName = item.UserName,
                     PositionCode = item.PositionCode,
                     PositionId = item.PositionId
                 }).ToList();

            if (!string.IsNullOrEmpty(Code))
            {
                q = q.Where(item => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.Code).ToLower().Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(Code).ToLower()));
            }

            if (!string.IsNullOrEmpty(Name))
            {
                q = q.Where(item => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.Name).ToLower().Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(Name).ToLower()));
            }
            if (!string.IsNullOrEmpty(Ethnic))
            {
                q = q.Where(item => item.Ethnic == Ethnic);
            }
            if (!string.IsNullOrEmpty(ProvinceId))
            {
                q = q.Where(item => item.ProvinceId == ProvinceId);
            }
            if (!string.IsNullOrEmpty(DistrictId))
            {
                q = q.Where(item => item.DistrictId == DistrictId);
            }
            if (!string.IsNullOrEmpty(WardId))
            {
                q = q.Where(item => item.WardId == WardId);

            }
            if (!string.IsNullOrEmpty(CountryId))
            {
                q = q.Where(item => item.CountryId == CountryId);
            }
            if (PositionId != null && PositionId.Value > 0)
            {
                q = q.Where(item => item.PositionId == PositionId);
            }
            if (!string.IsNullOrEmpty(Religion))
            {
                q = q.Where(item => item.Religion == Religion);
            }
            if (!string.IsNullOrEmpty(MaritalStatus))
            {
                q = q.Where(item => item.MaritalStatus == MaritalStatus);
            }
            if (branchId != null && branchId.Value > 0)
            {
                q = q.Where(item => ("," + item.DrugStore + ",").Contains("," + branchId + ",") == true);
            }
            else
            {
                if (Request["search"] == null)
                {
                    if (user.BranchId != null && user.BranchId.Value > 0)
                    {
                        q = q.Where(item => ("," + item.DrugStore + ",").Contains("," + branchId + ",") == true);
                    }
                }
            }
            //if (DepartmentId != null && DepartmentId.Value > 0)
            //{
            //    q = q.Where(item => item.BranchDepartmentId == DepartmentId);
            //}
            //if (!string.IsNullOrEmpty(module_list))
            //{
            //    if (module_list == "StaffFamily")
            //    {
            //        List<int> staff_family = staffFamilyRepository.GetAllStaffFamily().Select(n => n.StaffId.Value).Distinct().ToList();
            //        q = q.Where(id1 => !staff_family.Any(id2 => id2 == id1.Id)).ToList();
            //    }
            //    if (module_list == "StaffSocialInsurance")
            //    {
            //        DateTime date = DateTime.Now;
            //        List<int> staff_used = staffSocialInsuranceRepository.GetAllStaffSocialInsurance().Where(n => n.SocietyEndDate >= date || n.MedicalEndDate >= date).Select(n => n.StaffId.Value).ToList();
            //        q = q.Where(id1 => !staff_used.Any(id2 => id2 == id1.Id)).ToList();
            //    }
            //}
            ViewBag.SuccessMessage = TempData[Globals.SuccessMessageKey];
            ViewBag.FailedMessage = TempData[Globals.FailedMessageKey];
            return View(q);
        }

        #endregion

        #region DeleteAll
        [HttpPost]
        public ActionResult DeleteAll(int Id)
        {
            try
            {
                var item = StaffsRepository.GetStaffsById(Id);
                if (item != null)
                {
                    //if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                    //{
                    //    TempData["FailedMessage"] = "NotOwner";
                    //    return RedirectToAction("List");
                    //}
                    item.IsDeleted = true;
                    StaffsRepository.UpdateStaffs(item);
                }
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.DeleteSuccess;
                if (Request.UrlReferrer != null)
                    return Redirect(Request.UrlReferrer.AbsoluteUri);
                return RedirectToAction("List", "Staffs");
            }
            catch (DbUpdateException)
            {
                TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.RelationError;
                return RedirectToAction("List");
            }
        }
        #endregion

        #region Diagrams

        #region DetailBasic
        public ActionResult DetailBasic(int Id, bool? ShowProfileImageName, bool HasWidget = true)
        {
            ViewBag.HasWidget = HasWidget;

            var student = StaffsRepository.GetStaffsById(Id);
            if (student != null)
            {
                var model = new StaffsViewModel();
                AutoMapper.Mapper.Map(student, model);

                if (!string.IsNullOrEmpty(model.ProfileImage))
                {
                    var originalDirectory = new DirectoryInfo(string.Format("{0}Data\\HinhSV\\", Server.MapPath(@"\")));

                    string pathString = System.IO.Path.Combine(originalDirectory.ToString());
                    model.ProfileImagePath = pathString + model.ProfileImage;
                    if (!System.IO.File.Exists(model.ProfileImagePath))
                    {
                        model.ProfileImagePath = "/assets/img/no-avatar.png";
                    }
                    else
                    {
                        model.ProfileImagePath = "/Data/HinhSV/" + model.ProfileImage;
                    }
                }
                else
                    if (string.IsNullOrEmpty(model.ProfileImage))//Đã có hình
                {
                    model.ProfileImagePath = "/assets/img/no-avatar.png";
                }

                if (ShowProfileImageName.HasValue)
                {
                    ViewBag.ShowProfileImageName = ShowProfileImageName.Value;
                }
                else
                {
                    ViewBag.ShowProfileImageName = false;
                }

                return View(model);
            }

            return null;
        }

        #endregion

        private bool CheckUsernameExists(string username)
        {
            var user = userRepository.GetByUserName(username);

            return user != null;
        }

        #region Create
        public ViewResult Create(int? BranchDepartmentId)
        {
            var model = new StaffsViewModel();
            var departmentList = branchRepository.GetAllBranch().Where(x => x.ParentId != null || x.ParentId > 0)
                     .Select(item => new BranchViewModel
                     {
                         Name = item.Name,
                         Id = item.Id,
                         ParentId = item.ParentId
                     }).ToList();
            ViewBag.departmentList = departmentList;
            model.IsWorking = true;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(StaffsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var DrugStore = Request["DrugStore"];
                var CustomerArray = Request["Customers"];

                string[] csSplited = CustomerArray.Split(',');

                foreach (var id in csSplited)
                {
                    var Customer = CustomerRepository.GetCustomerById(model.Id);
                    Customer.ManagerStaffId = Int32.Parse(id);

                    CustomerRepository.UpdateCustomer(Customer);
                }

                var Staffs = new Staffs();
                AutoMapper.Mapper.Map(model, Staffs);
                Staffs.IsDeleted = false;
                Staffs.CreatedUserId = WebSecurity.CurrentUserId;
                Staffs.ModifiedUserId = WebSecurity.CurrentUserId;
                Staffs.CreatedDate = DateTime.Now;
                Staffs.ModifiedDate = DateTime.Now;
                Staffs.Name = model.FirstName + " " + model.LastName;
                Staffs.DrugStore = DrugStore;
                //lấy mã.
                StaffsRepository.InsertStaffs(Staffs);
                var prefix1 = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_Staff");
                Staffs.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix1, Staffs.Id);
                StaffsRepository.UpdateStaffs(Staffs);

                var userDN = userRepository.GetUserById(WebSecurity.CurrentUserId);
                var path = System.Web.HttpContext.Current.Server.MapPath("~" + Helpers.Common.GetSetting("Staff"));
                if (Request.Files["file-image"] != null)
                {
                    var file = Request.Files["file-image"];
                    if (file.ContentLength > 0)
                    {
                        string image_name = Staffs.Code + "." + file.FileName.Split('.').Last();
                        bool isExists = System.IO.Directory.Exists(path);
                        if (!isExists)
                            System.IO.Directory.CreateDirectory(path);
                        file.SaveAs(path + image_name);
                        Staffs.ProfileImage = image_name;
                        StaffsRepository.UpdateStaffs(Staffs);
                    }

                }

                if (model.UserName != null)
                {

                    if (!CheckUsernameExists(model.UserName))
                    {
                        WebSecurity.CreateUserAndAccount(model.UserName, model.Password);

                        //==== Update User ===//
                        int userId = WebSecurity.GetUserId(model.UserName);
                        User user = userRepository.GetUserById(userId);
                        user.Address = model.Address;
                        user.CreatedDate = DateTime.Now;
                        user.DateOfBirth = model.Birthday;
                        user.Email = model.Email;
                        user.Mobile = model.Phone;
                        user.Sex = model.Gender;
                        user.UserName = model.UserName;
                        var position = positionRepository.GetPositionById(model.PositionId.Value);
                        var user_type = user_typeRepository.GetUserTypeByCode(position.Code);
                        user.UserTypeId = user_type.Id;
                        user.FullName = Staffs.Name;
                        user.FirstName = model.FirstName;
                        user.LastName = model.LastName;
                        user.Status = UserStatus.Active;
                        if (model.StaffParentId != null && model.StaffParentId.Value > 0)
                        {
                            //var parentId = StaffsRepository.GetvwStaffsById(model.StaffParentId.Value);
                            user.ParentId = model.StaffParentId;
                        }
                        user.DrugStore = Staffs.DrugStore;
                        userRepository.UpdateUser(user);
                        TempData["AlertMessage"] = " Đã tạo User thành công.";
                        Staffs.UserId = user.Id;
                        StaffsRepository.UpdateStaffs(Staffs);
                    }
                    if (model.StaffParentId != null && model.StaffParentId.Value > 0)
                    {
                        var List_user_parent = StaffsRepository.GetAllStaffs().Where(x => x.StaffParentId == model.StaffParentId.Value).ToList();
                        var user_parent = StaffsRepository.GetStaffsById(model.StaffParentId.Value);
                        for (int i = 0; i < List_user_parent.Count(); i++)
                        {
                            if (i == 0)
                            {
                                user_parent.DrugStore = List_user_parent[i].DrugStore;
                            }
                            else
                            {
                                user_parent.DrugStore += "," + List_user_parent[i].DrugStore;
                            }
                        }
                        StaffsRepository.UpdateStaffs(user_parent);
                        if (user_parent.UserId != null)
                        {
                            var parent = userRepository.GetUserById(user_parent.UserId.Value);
                            if (parent != null)
                            {
                                parent.DrugStore = user_parent.DrugStore;
                                userRepository.UpdateUser(parent);
                            }
                        }
                    }

                }

                ViewBag.SuccessMessage = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("DetailBasicFull", "Staffs", new { area = "Staff", Id = Staffs.Id, IsLayout = true });

            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var Staffs = StaffsRepository.GetvwStaffsById(Id.Value);
            if (Staffs != null && Staffs.IsDeleted != true)
            {
                var model = new StaffsViewModel();
                AutoMapper.Mapper.Map(Staffs, model);
                model.UserList = Helpers.SelectListHelper.GetSelectList_User(model.UserId);
                var departmentList = branchRepository.GetAllBranch().Where(x => x.ParentId != null || x.ParentId > 0)
                   .Select(item => new BranchViewModel
                   {
                       Name = item.Name,
                       Id = item.Id,
                       ParentId = item.ParentId
                   }).ToList();
                ViewBag.departmentList = departmentList;
                return View(model);
            }

            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(StaffsViewModel model)
        {
            foreach (var modelKey in ModelState.Keys)
            {
                if (modelKey == "UserName" || modelKey == "Password")
                {
                    var index = ModelState.Keys.ToList().IndexOf(modelKey);
                    ModelState.Values.ElementAt(index).Errors.Clear();
                }
            }

            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var DrugStore = Request["DrugStore"];
                    var Staffs = StaffsRepository.GetStaffsById(model.Id);
                    var Staff_Parent_old = Staffs.StaffParentId;
                    AutoMapper.Mapper.Map(model, Staffs);
                    Staffs.DrugStore = DrugStore;
                    Staffs.ModifiedUserId = WebSecurity.CurrentUserId;
                    Staffs.ModifiedDate = DateTime.Now;
                    Staffs.Name = model.FirstName + " " + model.LastName;
                    //Staffs.DrugStore = DrugStore;
                    var path = Helpers.Common.GetSetting("Staff");
                    var filepath = System.Web.HttpContext.Current.Server.MapPath("~" + path);
                    if (Request.Files["file-image"] != null)
                    {
                        var file = Request.Files["file-image"];
                        if (file.ContentLength > 0)
                        {
                            FileInfo fi = new FileInfo(Server.MapPath("~" + path) + Staffs.ProfileImage);
                            if (fi.Exists)
                            {
                                fi.Delete();
                            }

                            string image_name = Staffs.Code + "." + file.FileName.Split('.').Last();

                            bool isExists = System.IO.Directory.Exists(filepath);
                            if (!isExists)
                                System.IO.Directory.CreateDirectory(filepath);
                            file.SaveAs(filepath + image_name);
                            Staffs.ProfileImage = image_name;
                        }
                    }
                    StaffsRepository.UpdateStaffs(Staffs);

                    if (model.UserId != null && model.UserId.Value > 0)
                    {
                        User user = userRepository.GetUserById(model.UserId.Value);
                        user.Address = model.Address;
                        user.DateOfBirth = model.Birthday;
                        user.Email = model.Email;
                        user.Mobile = model.Phone;
                        user.Sex = model.Gender;
                        var position = positionRepository.GetPositionById(model.PositionId.Value);
                        var user_type = user_typeRepository.GetUserTypeByCode(position.Code);
                        user.UserTypeId = user_type.Id;
                        //  user.UserName = model.UserName;
                        user.FullName = Staffs.Name;
                        user.FirstName = model.FirstName;
                        user.LastName = model.LastName;
                        if (model.StaffParentId != null && model.StaffParentId.Value > 0)
                        {
                            //var parentId = StaffsRepository.GetvwStaffsById(model.StaffParentId.Value);
                            user.ParentId = model.StaffParentId;
                        }
                        user.DrugStore = Staffs.DrugStore;
                        user.ProfileImage = Staffs.ProfileImage;
                        userRepository.UpdateUser(user);

                    }
                    var List_staff = StaffsRepository.GetAllStaffs();
                    if (Staff_Parent_old != null && Staff_Parent_old.Value > 0)
                    {
                        var List_user_parent = List_staff.Where(x => x.StaffParentId == Staff_Parent_old.Value).ToList();
                        var user_parent = List_staff.Where(x => x.Id == Staff_Parent_old.Value).ToList().FirstOrDefault();
                        for (int i = 0; i < List_user_parent.Count(); i++)
                        {
                            if (i == 0)
                            {
                                user_parent.DrugStore = List_user_parent[i].DrugStore;
                            }
                            else
                            {
                                user_parent.DrugStore += "," + List_user_parent[i].DrugStore;
                            }
                        }
                        StaffsRepository.UpdateStaffs(user_parent);
                        if (user_parent.UserId != null)
                        {
                            var parent = userRepository.GetUserById(user_parent.UserId.Value);
                            if (parent != null)
                            {
                                parent.DrugStore = user_parent.DrugStore;
                                userRepository.UpdateUser(parent);
                            }
                        }
                    }
                    if (model.StaffParentId != null && model.StaffParentId.Value > 0)
                    {
                        var List_user_parent = List_staff.Where(x => x.StaffParentId == model.StaffParentId.Value).ToList();
                        var user_parent = StaffsRepository.GetStaffsById(model.StaffParentId.Value);
                        for (int i = 0; i < List_user_parent.Count(); i++)
                        {
                            if (i == 0)
                            {
                                user_parent.DrugStore = List_user_parent[i].DrugStore;
                            }
                            else
                            {
                                user_parent.DrugStore += "," + List_user_parent[i].DrugStore;
                            }
                        }
                        StaffsRepository.UpdateStaffs(user_parent);
                        if (user_parent.UserId != null)
                        {
                            var parent = userRepository.GetUserById(user_parent.UserId.Value);
                            if (parent != null)
                            {
                                parent.DrugStore = user_parent.DrugStore;
                                userRepository.UpdateUser(parent);
                            }
                        }
                    }

                    return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });

                }
                return View(model);
            }

            string errorMessage = string.Empty;
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    errorMessage += error.ErrorMessage;
                }
            }

            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        #endregion

        #region MyPage

        public ActionResult MyPage(int? Id)
        {
            var student = StaffsRepository.GetvwStaffsById(Id.Value);

            if (student != null)
            {
                var model = new StaffsViewModel();
                AutoMapper.Mapper.Map(student, model);
                return View(model);
            }

            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult MyPage(StaffsViewModel model)
        {
            if (Request["Submit"] == "Save")
            {
                bool bHasUploadFile = false;
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];
                    if (file != null && file.ContentLength > 0)
                    {
                        bHasUploadFile = true;
                    }
                }
                if (bHasUploadFile == false)
                {
                    TempData[Globals.FailedMessageKey] = App_GlobalResources.Wording.KhongTrungTenHinh;
                    return View(model);
                }
                else
                {
                    var file = Request.Files[0];
                    if (file != null && file.ContentLength > 0)
                    {
                        var extention = file.FileName.Split('.').Last();

                        var student = StaffsRepository.GetStaffsById(model.Id);
                        student.ModifiedUserId = WebSecurity.CurrentUserId;
                        student.ModifiedDate = DateTime.Now;

                        //  if (student.ProfileImage == null)
                        student.ProfileImage = student.Code + "." + extention;
                        StaffsRepository.UpdateStaffs(student);
                        var path = System.Web.HttpContext.Current.Server.MapPath("~" + Helpers.Common.GetSetting("Staff"));
                        bool isExists = System.IO.Directory.Exists(path);
                        if (!isExists)
                            System.IO.Directory.CreateDirectory(path);
                        file.SaveAs(path + student.ProfileImage);
                    }
                    return RedirectToAction("Detail", new { Id = model.Id });
                }
            }
            return RedirectToAction("Detail", new { Id = model.Id });
        }
        #endregion

        #region Delete
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            try
            {
                var item = StaffsRepository.GetStaffsById(Id);
                item.IsDeleted = true;
                StaffsRepository.UpdateStaffs(item);
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.DeleteSuccess;
                return RedirectToAction("Diagrams", "Branch", new { area = "Staff" });
            }
            catch (DbUpdateException)
            {
                TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.RelationError;
                return RedirectToAction("Diagrams", "Branch", new { area = "Staff" });
            }
        }
        #endregion

        #endregion

        //cập nhật ngày vào làm, ngày nghỉ việc của nhân viên dựa vào hợp đồng
        #region UpdateStaff
        public static void UpdateStaff(int? StaffId, DateTime? StartDate, DateTime? EndDate)
        {
            Erp.Domain.Staff.Repositories.StaffsRepository staffsRepository = new Erp.Domain.Staff.Repositories.StaffsRepository(new Domain.Staff.ErpStaffDbContext());
            var q = staffsRepository.GetStaffsById(StaffId.Value);
            q.StartDate = StartDate;
            if (EndDate != null)
            {
                q.EndDate = EndDate;
            }
            staffsRepository.UpdateStaffs(q);
        }
        #endregion

        //public ActionResult Update()
        //{
        //    var staff = StaffsRepository.GetAllStaffs().ToList();
        //    var aa = fPMachineRepository.GetAllFPMachine().ToList();
        //    var ds_vantay = fPMachineRepository.GetAllFingerPrint().ToList();
        //    for (int i = 0; i < staff.Count(); i++)
        //    {
        //        if (staff[i].Sale_BranchId != null)
        //        {
        //            var aabb = aa.Where(x => x.BranchId == staff[i].Sale_BranchId).FirstOrDefault();
        //            if (staff[i].CheckInOut_UserId != null)
        //            {
        //                var user = staff[i].CheckInOut_UserId.Value;
        //                var vantay = ds_vantay.Where(x => x.UserId.Value == user).ToList();
        //                for (int j = 0; j < vantay.Count(); j++)
        //                {
        //                    vantay[j].FPMachineId = aabb.Id;
        //                    fPMachineRepository.UpdateFingerPrint(vantay[j]);
        //                }
        //            }
        //        }
        //    }
        //    return RedirectToAction("Search");
        //}

        public ActionResult DetailBasicFull(int? Id)
        {
            var student = StaffsRepository.GetvwStaffsById(Id.Value);

            if (student != null)
            {
                var model = new StaffsViewModel();
                AutoMapper.Mapper.Map(student, model);

                return View(model);
            }

            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        #region Calendar
        public ViewResult Calendar(int? StaffId, int? month, int? year)
        {
            var user = userRepository.GetUserById(WebSecurity.CurrentUserId);
            List<WorkSchedulesViewModel> q = workSchedulesRepository.GetvwAllWorkSchedules()
                .Select(i => new WorkSchedulesViewModel
                {
                    Absent = i.Absent,
                    Id = i.Id,
                    CreatedUserId = i.CreatedUserId,
                    CreatedDate = i.CreatedDate,
                    ModifiedUserId = i.ModifiedUserId,
                    ModifiedDate = i.ModifiedDate,
                    BranchDepartmentId = i.BranchDepartmentId,
                    Sale_BranchId = i.Sale_BranchId,
                    Code = i.Code,
                    CodeShifts = i.Code,
                    Color = i.Color,
                    Day = i.Day,
                    DayOff = i.DayOff,
                    DayOffCode = i.DayOffCode,
                    DayOffName = i.DayOffName,
                    EndTime = i.EndTime,
                    EndTimeIn = i.EndTimeIn,
                    HoursIn = i.HoursIn,
                    Month = i.Month,
                    Name = i.Name,
                    NameShifts = i.NameShifts,
                    Year = i.Year,
                    ShiftsId = i.ShiftsId,
                    HoursOut = i.HoursOut,
                    NightShifts = i.NightShifts,
                    StartTime = i.StartTime,
                    StaffId = i.StaffId,
                    StartTimeOut = i.StartTimeOut,
                    Symbol = i.Symbol,
                    UserEnrollNumber = i.UserEnrollNumber,
                    Total_minute_work_overtime = i.Total_minute_work_overtime,
                    Total_minute_work_late = i.Total_minute_work_late,
                    Total_minute_work_early = i.Total_minute_work_early,
                    Total_minute_work = i.Total_minute_work,
                    TimekeepingListId = i.TimekeepingListId
                }).ToList();

            DateTime aDateTime = new DateTime();
            if (month == null && year == null)
            {
                aDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            }
            else
            {
                aDateTime = new DateTime(year.Value, month.Value, 1);
            }
            // Cộng thêm 1 tháng và trừ đi một ngày.
            DateTime retDateTime = aDateTime.AddMonths(1).AddDays(-1);

            if (year != null && year.Value > 0)
            {
                q = q.Where(x => x.Day.Value.Year == year).ToList();
            }
            //if (month != null && month.Value > 0)
            //{
            //    q = q.Where(x => x.Day.Value.Month == month).ToList();
            //}
            //if (year == null && month == null)
            //{
            //    q = q.Where(x => aDateTime <= x.Day.Value && x.Day.Value <= retDateTime).OrderBy(x => x.Day).ToList();
            //}
            if (StaffId != null && StaffId.Value > 0)
            {
                q = q.Where(x => x.StaffId == StaffId).ToList();
            }

            foreach (var item in q)
            {
                //tách chuỗi thời gian của ca làm việc
                string[] strStartTime = item.StartTime.Split(':');
                string[] strStartTimeOut = item.StartTimeOut.Split(':');
                string[] strEndTime = item.EndTime.Split(':');
                string[] strEndTimeIn = item.EndTimeIn.Split(':');
                // chuyển đổi thời gian của ca làm việc từ string sang DateTime
                item.StartTime = item.Day.Value.AddHours(Convert.ToDouble(strStartTime[0])).AddMinutes(Convert.ToDouble(strStartTime[1])).ToString();
                item.StartTimeOut = item.Day.Value.AddHours(Convert.ToDouble(strStartTimeOut[0])).AddMinutes(Convert.ToDouble(strStartTimeOut[1])).ToString();
                item.EndTime = item.Day.Value.AddHours(Convert.ToDouble(strEndTime[0])).AddMinutes(Convert.ToDouble(strEndTime[1])).ToString();
                item.EndTimeIn = item.Day.Value.AddHours(Convert.ToDouble(strEndTimeIn[0])).AddMinutes(Convert.ToDouble(strEndTimeIn[1])).ToString();
            }


            var dataEvent = q.Select(e => new
            {
                title = e.NameShifts,
                start = (e.HoursIn == null ? Convert.ToDateTime(e.StartTime).ToString("yyyy-MM-ddTHH:mm:ssZ") : e.HoursIn.Value.ToString("yyyy-MM-ddTHH:mm:ssZ")),
                end = (e.HoursOut == null ? Convert.ToDateTime(e.EndTime).ToString("yyyy-MM-ddTHH:mm:ssZ") : e.HoursOut.Value.ToString("yyyy-MM-ddTHH:mm:ssZ")),
                allDay = false,
                className = (e.Color),
                url = e.Id,
                backgroundColor = e.Color
            }).ToList();

            ViewBag.dataEvent = new JavaScriptSerializer().Serialize(dataEvent);
            ViewBag.aDateTime = aDateTime.ToString("yyyy-MM-dd");
            ViewBag.StaffId = StaffId;
            ViewBag.date = aDateTime;
            return View(q);
        }
        #endregion

        #region - Json -
        public JsonResult GetListJsonStaffs(string Name)
        {
            var list = StaffsRepository.GetvwAllStaffs().ToList();
            list = list.Where(x => x.Name.ToLower().Contains(Name.ToLower())).ToList();
            foreach (var item in list)
            {
                item.ProfileImage = Erp.BackOffice.Helpers.Common.KiemTraTonTaiHinhAnh(item.ProfileImage, "Staffs", "user");
            };
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region Detail

        public ActionResult Detail(int? Id)
        {
            var student = StaffsRepository.GetvwStaffsById(Id.Value);

            if (student != null)
            {
                var model = new StaffsViewModel();
                AutoMapper.Mapper.Map(student, model);

                #region Processbar
                //hàm tính % hoàn thành cập nhật thông tin nhân viên
                //var list_no_personal_info = "CheckInOut_UserId,Phone,Phone2,Email,Email2,Address,WardId,DistrictId,ProvinceId,UserId,PositionId,BranchDepartmentId,StartDate,EndDate,Technique,Literacy,Language,FirstName,Name,LastName,Code,ProfileImage";
                //var list_no_contact_info = "CheckInOut_UserId,Birthday,Gender,Religion,Ethnic,MaritalStatus,CountryId,IdCardNumber,IdCardDate,IdCardIssued,UserId,PositionId,BranchDepartmentId,StartDate,EndDate,Technique,Literacy,Language,FirstName,Name,LastName,Code,ProfileImage";
                //var list_no_info_staff = "CheckInOut_UserId,Birthday,Gender,Religion,Ethnic,MaritalStatus,CountryId,IdCardNumber,IdCardDate,IdCardIssued,Phone,Phone2,Email,Email2,Address,WardId,DistrictId,ProvinceId,FirstName,Name,LastName,Code,ProfileImage";
                //ViewBag.info_personal = Erp.BackOffice.Helpers.Common.Percent_ProcessBar(model, list_no_personal_info, "Staffs");
                //ViewBag.info_contact = Erp.BackOffice.Helpers.Common.Percent_ProcessBar(model, list_no_contact_info, "Staffs");
                //ViewBag.info_staff = Erp.BackOffice.Helpers.Common.Percent_ProcessBar(model, list_no_info_staff, "Staffs");
                #endregion

                #region Salary_Advance
                //dashboard tạm ứng
                var list_advance = salaryAdvanceRepository.GetAllSalaryAdvance().Where(x => x.StaffId == model.Id && x.Status == "Đã trả tiền" && x.DayAdvance.Value.Year == DateTime.Now.Year);
                var count_advance = list_advance.Count();
                var money_advance_month = list_advance.Where(x => x.DayAdvance.Value.Month == DateTime.Now.Month && x.DayAdvance.Value.Year == DateTime.Now.Year).ToList().Sum(x => x.Pay);
                ViewBag.count_advance = count_advance;
                ViewBag.money_advance_month = money_advance_month;
                #endregion

                #region CommisionStaff
                //dashboard hoa hồng.
                var commision = commisionStaffRepository.GetAllvwCommisionStaff().Where(x => x.StaffId == model.Id);
                var list_commision_now = commision.Where(x => x.month == DateTime.Now.Month && x.year == DateTime.Now.Year);
                //lấy ngày đầu tiên của tháng hiện tại
                //trừ 1 ngày ra ngày cuối cùng của tháng trước đó.
                DateTime aDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime retDateTime = aDateTime.AddDays(-1);
                var list_commision_last_month = commision.Where(x => x.month == retDateTime.Month && x.year == retDateTime.Year);
                var money_commision_month_now = list_commision_now.ToList().Sum(x => x.AmountOfCommision);
                var money_commision_last_month = list_commision_last_month.ToList().Sum(x => x.AmountOfCommision);
                ViewBag.money_commision_month_now = money_commision_month_now;
                int? percent_commision_month_now = 0;
                if (money_commision_last_month <= 0)
                {
                    if (money_commision_month_now > 0)
                    {
                        percent_commision_month_now = 100;
                    }
                }
                else
                {
                    if (money_commision_month_now > 0)
                    {
                        percent_commision_month_now = Convert.ToInt32((money_commision_month_now - money_commision_last_month) * 100 / money_commision_last_month);
                    }
                }
                ViewBag.percent_commision_month_now = percent_commision_month_now;
                #endregion

                #region ProcessPay
                //bậc lương
                var process_pay = processPayRepository.GetAllProcessPay().Where(x => x.StaffId == model.Id);
                ViewBag.count_process_pay = process_pay.Count();
                int? money_now = 0;
                if (process_pay.Count() > 0)
                {
                    money_now = process_pay.OrderByDescending(x => x.DayEffective).FirstOrDefault().BasicPay;
                }
                ViewBag.money_now = money_now;
                #endregion

                #region DayOff
                var dayoff = dayoffRepository.GetAllvwDayOff().Where(x => x.StaffId == model.Id && x.CreatedDate.Value.Year == DateTime.Now.Year);
                ViewBag.count_dayoff = dayoff.Count();
                var quantity = symbolTimekeepingRepository.GetSymbolTimekeepingByCodeDefault("PN").Quantity;
                int? no_dayoff_of_phep_nam = quantity;
                if (dayoff.Where(x => x.CodeSymbol == "PN").Count() > 0)
                {
                    no_dayoff_of_phep_nam = dayoff.Where(x => x.CodeSymbol == "PN").OrderByDescending(x => x.CreatedDate).FirstOrDefault().QuantityNotUsed;
                }
                ViewBag.no_dayoff_of_phep_nam = no_dayoff_of_phep_nam;
                #endregion

                #region RegisterForOvertime
                var Overtime = registerForOvertimeRepository.GetAllRegisterForOvertime().Where(x => x.StaffId == model.Id && x.DayOvertime.Value.Year == DateTime.Now.Year);
                ViewBag.count_Overtime = Overtime.Count();
                int? Overtime_month_now = Overtime.Where(x => x.DayOvertime.Value.Month == DateTime.Now.Month).Count();
                ViewBag.Overtime_month_now = Overtime_month_now;
                #endregion

                #region Family
                var Family = staffFamilyRepository.GetAllStaffFamily().Where(x => x.StaffId == model.Id);
                ViewBag.count_Family = Family.Count();
                #endregion

                #region TransferWork
                var TransferWork = transferWorkRepository.GetAllTransferWork().Where(x => x.StaffId == model.Id);
                ViewBag.count_TransferWork = TransferWork.Count();
                #endregion

                #region SocialInsurance
                var SocialInsurance = staffSocialInsuranceRepository.GetAllViewStaffSocialInsurance().Where(x => x.StaffId == model.Id);
                ViewBag.count_SocialInsurance = SocialInsurance.Count();
                #endregion

                #region workingProcess
                var workingProcess = workingProcessRepository.GetAllWorkingProcess().Where(x => x.StaffId == model.Id);
                ViewBag.count_workingProcess = workingProcess.Count();
                #endregion

                #region Technique
                var Technique = techniqueRepository.GetAllTechnique().Where(x => x.StaffId == model.Id);
                ViewBag.count_Technique = Technique.Count();
                #endregion

                #region labourContract
                var labourContract = labourContractRepository.GetAllLabourContract().Where(x => x.StaffId == model.Id);
                ViewBag.count_labourContract = labourContract.Count();
                #endregion

                #region Bank
                var Bank = bankRepository.GetAllBank().Where(x => x.StaffId == model.Id);
                ViewBag.count_Bank = Bank.Count();
                string bank_active = "chưa có";
                if (Bank.Where(x => x.IsActive == true).Count() > 0)
                {
                    bank_active = Bank.Where(x => x.IsActive == true).FirstOrDefault().CodeBank;
                }
                ViewBag.bank_active = bank_active;
                #endregion

                #region bonusDiscipline
                var bonusDiscipline = bonusDisciplineRepository.GetAllvwBonusDiscipline().Where(x => x.StaffId == model.Id);
                var count_bonusDiscipline = bonusDiscipline.Count();
                ViewBag.count_bonusDiscipline = count_bonusDiscipline;
                int persent_bonus = 0;
                var count_bonus = bonusDiscipline.Where(x => x.Category == "Bonus").ToList().Count();
                if (count_bonusDiscipline > 0)
                {
                    persent_bonus = count_bonus * 100 / count_bonusDiscipline;
                }
                ViewBag.persent_bonus = persent_bonus;

                string last_bonusDiscipline = "";
                string last_time_bonusDiscipline = "";
                if (count_bonusDiscipline > 0)
                {
                    var aa = bonusDiscipline.OrderByDescending(x => x.DayEffective).FirstOrDefault();
                    last_bonusDiscipline = aa.Category;
                    last_time_bonusDiscipline = aa.DayEffective.Value.ToString("dd/MM/yyyy");
                }
                ViewBag.last_bonusDiscipline = last_bonusDiscipline;
                ViewBag.last_time_bonusDiscipline = last_time_bonusDiscipline;
                #endregion

                #region checkInOut
                var checkInOut = checkInOutRepository.GetAllvwCheckInOut().Where(x => x.StaffId != null && x.StaffId.Value == model.Id);
                string date = "chưa có";
                if (checkInOut.Count() > 0)
                {
                    date = checkInOut.FirstOrDefault().TimeStr.Value.ToString("dd/MM/yyyy HH:mm");
                }
                ViewBag.date_checkInOut = date;
                //model.ListFingerPrint = fPMachineRepository.GetAllvwFingerPrint().Where(x => x.UserId == model.CheckInOut_UserId).ToList();
                #endregion
                return View(model);
            }

            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Detail(StaffsViewModel model)
        {
            if (Request["Submit"] == "Save")
            {
                bool bHasUploadFile = false;
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];
                    if (file != null && file.ContentLength > 0)
                    {
                        bHasUploadFile = true;
                    }
                }
                if (bHasUploadFile == false)
                {
                    TempData[Globals.FailedMessageKey] = App_GlobalResources.Wording.KhongTrungTenHinh;
                    return View(model);
                }
                else
                {
                    var file = Request.Files[0];
                    if (file != null && file.ContentLength > 0)
                    {
                        var extention = file.FileName.Split('.').Last();

                        var student = StaffsRepository.GetStaffsById(model.Id);
                        student.ModifiedUserId = WebSecurity.CurrentUserId;
                        student.ModifiedDate = DateTime.Now;

                        //  if (student.ProfileImage == null)
                        student.ProfileImage = student.Code + "." + extention;
                        StaffsRepository.UpdateStaffs(student);
                        var path = System.Web.HttpContext.Current.Server.MapPath("~" + Helpers.Common.GetSetting("Staff"));
                        bool isExists = System.IO.Directory.Exists(path);
                        if (!isExists)
                            System.IO.Directory.CreateDirectory(path);
                        file.SaveAs(path + student.ProfileImage);
                    }
                    return RedirectToAction("MyPage", new { Id = model.Id });
                }
            }
            return RedirectToAction("MyPage", new { Id = model.Id });
        }
        #endregion
    }
}
