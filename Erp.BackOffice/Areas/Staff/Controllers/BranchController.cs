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

namespace Erp.BackOffice.Staff.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class BranchController : Controller
    {
        private readonly IBranchRepository BranchRepository;
        private readonly IUserRepository userRepository;
        private readonly IBranchDepartmentRepository branchDepartmentRepository;
        private readonly IStaffsRepository staffRepository;
        private readonly ISettingRepository settingRepository;
        private readonly ILocationRepository locationRepository;
        public BranchController(
            IBranchRepository _Branch
            , IUserRepository _user
              , IBranchDepartmentRepository branchDepartment
            , IStaffsRepository staff
            , ISettingRepository setting
             , ILocationRepository location
            )
        {
            BranchRepository = _Branch;
            userRepository = _user;
            branchDepartmentRepository = branchDepartment;
            staffRepository = staff;
            locationRepository = location;
            settingRepository = setting;
        }

        #region Index
        public ViewResult Index(string txtSearch)
        {

            IQueryable<BranchViewModel> q = BranchRepository.GetAllBranch()
                .Select(item => new BranchViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                }).OrderByDescending(m => m.ModifiedDate);

            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var Branch = BranchRepository.GetBranchById(Id.Value);
            if (Branch != null && Branch.IsDeleted != true)
            {
                var model = new BranchViewModel();
                AutoMapper.Mapper.Map(Branch, model);
                model.ProvinceList = Helpers.SelectListHelper.GetSelectList_Location("0", model.CityId, App_GlobalResources.Wording.Empty);
                model.DistrictList = Helpers.SelectListHelper.GetSelectList_Location(model.CityId, model.DistrictId, App_GlobalResources.Wording.Empty);
                model.WardList = Helpers.SelectListHelper.GetSelectList_Location(model.DistrictId, model.WardId, App_GlobalResources.Wording.Empty);
                return View(model);
            }

            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(BranchViewModel model)
        {
            var urlRefer = Request["UrlReferrer"];
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var Branch = BranchRepository.GetBranchById(model.Id);
                    AutoMapper.Mapper.Map(model, Branch);
                    Branch.ModifiedUserId = WebSecurity.CurrentUserId;
                    Branch.ModifiedDate = DateTime.Now;
                    BranchRepository.UpdateBranch(Branch);

                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                    if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                    {
                        TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                        ViewBag.closePopup = "true";
                        model.Id = Branch.Id;
                        ViewBag.urlRefer = urlRefer;
                        model.ProvinceList = Helpers.SelectListHelper.GetSelectList_Location("0", model.CityId, App_GlobalResources.Wording.Empty);
                        model.DistrictList = Helpers.SelectListHelper.GetSelectList_Location(model.CityId, model.DistrictId, App_GlobalResources.Wording.Empty);
                        model.WardList = Helpers.SelectListHelper.GetSelectList_Location(model.DistrictId, model.WardId, App_GlobalResources.Wording.Empty);
                        return View(model);
                    }
                }

                return Redirect(urlRefer);
            }

            return View(model);
        }

        #endregion

        #region Create
        public ViewResult Create(int? BranchId)
        {
            var model = new BranchViewModel();
            model.ProvinceList = Helpers.SelectListHelper.GetSelectList_Location("0", null, App_GlobalResources.Wording.Empty);
            model.DistrictList = Helpers.SelectListHelper.GetSelectList_Location(null, null, App_GlobalResources.Wording.Empty);
            model.WardList = Helpers.SelectListHelper.GetSelectList_Location(null, null, App_GlobalResources.Wording.Empty);
            model.ParentId = BranchId;
            model.CooperationDay = DateTime.Now;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(BranchViewModel model)
        {
            var urlRefer = Request["UrlReferrer"];
            if (ModelState.IsValid)
            {
                var Branch = new Domain.Staff.Entities.Branch();
                AutoMapper.Mapper.Map(model, Branch);
                Branch.IsDeleted = false;
                Branch.CreatedUserId = WebSecurity.CurrentUserId;
                Branch.ModifiedUserId = WebSecurity.CurrentUserId;
                Branch.CreatedDate = DateTime.Now;
                Branch.ModifiedDate = DateTime.Now;
                BranchRepository.InsertBranch(Branch);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                {
                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                    ViewBag.closePopup = "true";
                    // model.Id = Branch.Id;
                    ViewBag.urlRefer = urlRefer;
                    model.ProvinceList = Helpers.SelectListHelper.GetSelectList_Location("0", null, App_GlobalResources.Wording.Empty);
                    model.DistrictList = Helpers.SelectListHelper.GetSelectList_Location(null, null, App_GlobalResources.Wording.Empty);
                    model.WardList = Helpers.SelectListHelper.GetSelectList_Location(null, null, App_GlobalResources.Wording.Empty);
                    return View(model);
                }
                return Redirect(urlRefer);
            }
            return View(model);
        }
        #endregion

        #region Delete

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var item = BranchRepository.GetBranchById(id);
                if (item != null)
                {
                    BranchRepository.DeleteBranch(item.Id);
                }
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

        #region DetailBasic
        public PartialViewResult DetailBasic(int? Id)
        {
            var model = new BranchViewModel();
            var branch = BranchRepository.GetvwBranchById(Id.Value);
            if (branch != null)
            {
                AutoMapper.Mapper.Map(branch, model);
                return PartialView(model);
            }
            return PartialView();
        }
        #endregion

        #region diagrams
        public ActionResult Diagrams()
        {
            var list = BranchRepository.GetAllBranch();
            var liststaff = staffRepository.GetAllStaffs().ToList();

            IEnumerable<BranchViewModel> q = list.Where(x=>x.ParentId==null).Select(item => new BranchViewModel
            {
                Id = item.Id,
                //CreatedUserId = item.CreatedUserId,
                //CreatedDate = item.CreatedDate,
                //ModifiedUserId = item.ModifiedUserId,
                //ModifiedDate = item.ModifiedDate,
                Name = item.Name,
                Code = item.Code,
                //Address = item.Address,
                //WardId = item.WardId,
                //DistrictId = item.DistrictId,
                //CityId = item.CityId,
                //Phone = item.Phone,
                //Email = item.Email,
                Type = item.Type,
                ParentId=item.ParentId,
                //MaxDebitAmount=item.MaxDebitAmount
            }).ToList();

            foreach (var item in q)
            {
                item.TotalDepartment = list.Where(x => x.ParentId == item.Id).Count();
                //item.TotalStaff = staffRepository.GetvwAllStaffs().Where(x => x.Sale_BranchId == item.Id).Count();
            }
            ViewBag.AccessRightCreate = SecurityFilter.AccessRight("Create", "Branch", "Staff");
            ViewBag.AccessRightEdit = SecurityFilter.AccessRight("Edit", "Branch", "Staff");
            ViewBag.AccessRightDelete = SecurityFilter.AccessRight("Delete", "Branch", "Staff");
            ViewBag.AlertMessage = TempData["AlertMessage"];

            //var branchDepartment = branchDepartmentRepository.GetAllBranchDepartment().ToList();

            var CongTy = settingRepository.GetAlls()
             .Where(item => item.Code == "settingprint" && item.Key == "companyName")
             .OrderBy(item => item.Note).ToList();
            if (CongTy.Count() > 0)
            {
                TreeNode Node = new TreeNode("");
                var _a = new BranchViewModel()
                {
                    Name = CongTy.Where(x => x.Key == "companyName").FirstOrDefault().Value,
                    Id = -1
                };

                TreeNode _node = new TreeNode(_a.Name);
                _node.Id = _a.Id;
                _node.TableName = typeof(TreeNode).Name;
                _node.GuidId = Guid.NewGuid().ToString();
                _node.ParentId = null;

                //FormatNode(_node, q.OrderBy(n => n.ParentId), q.Where(n => n.ParentId == null).OrderBy(n => n.IsOfficeOfAcademicAffairs), liststaff);
                //Chi nhánh
                foreach (var branch in q)
                {
                    TreeNode _node_chinhanh = new TreeNode(branch.Name);
                    _node_chinhanh.Id = branch.Id;
                    _node_chinhanh.TableName = typeof(BranchViewModel).Name;
                    _node_chinhanh.GuidId = Guid.NewGuid().ToString();
                    _node_chinhanh.TypeName = "Branch";
                    _node_chinhanh.ParentId = _node.Id;

                    //Thêm phòng ban
                    var deparmentByBranchs = list.Where(n => n.ParentId!=null&&n.ParentId == branch.Id).ToList();
                    if (deparmentByBranchs != null && deparmentByBranchs.Count() > 0)
                    {
                        foreach (var branchDeparment in deparmentByBranchs)
                        {
                            TreeNode _node_phongban = new TreeNode(branchDeparment.Name);
                            _node_phongban.Id = branchDeparment.Id;
                            _node_phongban.TableName = typeof(BranchViewModel).Name;
                            _node_phongban.GuidId = Guid.NewGuid().ToString();
                            _node_phongban.ParentId = branch.Id;
                            _node_phongban.TypeName = "DrugStore";
                            _node_chinhanh.ChildNode.Add(_node_phongban);
                        }
                    }

                    _node.ChildNode.Add(_node_chinhanh);

                }

                //Phòng ban (Nhân viên)
                Node.ChildNode.Add(_node);
                ViewBag.TreeNode = Node;
            }
            else
            {
                ViewBag.FailedMessage = "Chưa có thông tin khởi tạo của Công ty";
            }

            return View(q);
        }
        #endregion

        #region Config
        public ViewResult Config()
        {
            var q = settingRepository.GetAlls()
                .Where(item => item.Code == "settingprint")
                .OrderBy(item => item.Note).ToList();

            List<Erp.BackOffice.Areas.Administration.Models.SettingViewModel> model = new List<Areas.Administration.Models.SettingViewModel>();
            AutoMapper.Mapper.Map(q, model);

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(model);
        }
        #endregion
    }
}
