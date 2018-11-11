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
    public class BranchDepartmentController : Controller
    {
        private readonly IBranchDepartmentRepository BranchDepartmentRepository;
        private readonly IUserRepository userRepository;
        private readonly IStaffsRepository staffRepository;
        private readonly ICategoryRepository CategoryRepository;
        private readonly IBranchRepository branchRepository;

        public BranchDepartmentController(
            IBranchDepartmentRepository _BranchDepartment
            , IUserRepository _user
            , IStaffsRepository staff
            , ICategoryRepository category
            , IBranchRepository _branchRepository
            )
        {
            BranchDepartmentRepository = _BranchDepartment;
            userRepository = _user;
            staffRepository = staff;
            CategoryRepository = category;
            branchRepository = _branchRepository;
        }

        #region Index
        public ViewResult Index(string BranchId)
        {
            //if (BranchId != null && BranchId.Value > 0)
            //{
            //    ViewBag.BoolOffice = branchRepository.GetBranchById(BranchId.Value).IsOfficeOfAcademicAffairs;
            //}
            var list = staffRepository.GetvwAllStaffs().Where(x => ("," + BranchId + ",").Contains("," + x.DrugStore + ",") == true);
            IEnumerable<StaffsViewModel> q = list.Select(item => new StaffsViewModel
            {
                Id = item.Id,
                CreatedUserId = item.CreatedUserId,
                CreatedDate = item.CreatedDate,
                ModifiedUserId = item.ModifiedUserId,
                ModifiedDate = item.ModifiedDate,
                Phone = item.Phone,
                Email = item.Email,
                //BranchCode = item.BranchCode,
                //BranchName = item.BranchName,
                //Sale_BranchId = item.Sale_BranchId,
                Birthday = item.Birthday,
                Code = item.Code,
                Name = item.Name,
                PositionId = item.PositionId,
                PositionName=item.PositionName,
                PositionCode=item.PositionCode
            }).ToList();
            //var position = CategoryRepository.GetAllCategories().Where(x => x.Code == "position");
            //foreach (var item in q)
            //{
            //    if (!string.IsNullOrEmpty(item.Position))
            //    {
            //        item.PositionName = position.Where(x => x.Value == item.PositionId).FirstOrDefault().Name;
            //    }
            //}
            ViewBag.AccessRightCreate = SecurityFilter.AccessRight("Create", "Staffs", "Staff");
            ViewBag.AccessRightEdit = SecurityFilter.AccessRight("Edit", "Staffs", "Staff");
            ViewBag.AccessRightDelete = SecurityFilter.AccessRight("Delete", "Staffs", "Staff");

            ViewBag.AlertMessage = TempData["AlertMessage"];
            ViewBag.BranchId = BranchId;
            //ViewBag.SchoolId = SchoolId;
            return View(q);
        }
        #endregion

        #region Create
        public ViewResult Create(int? BranchId)
        {
            var model = new BranchDepartmentViewModel();
            model.Sale_BranchId = BranchId;
          //  model.DepartmentList = Helpers.SelectListHelper.GetSelectList_Category("Department", null, "Name", false);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(BranchDepartmentViewModel model)
        {
            var urlRefer = Request["UrlReferrer"];
            if (ModelState.IsValid)
            {
                var BranchDepartment = new BranchDepartment();

                AutoMapper.Mapper.Map(model, BranchDepartment);
                BranchDepartment.IsDeleted = false;
                BranchDepartment.CreatedUserId = WebSecurity.CurrentUserId;
                BranchDepartment.ModifiedUserId = WebSecurity.CurrentUserId;
                BranchDepartment.CreatedDate = DateTime.Now;
                BranchDepartment.ModifiedDate = DateTime.Now;
                BranchDepartmentRepository.InsertBranchDepartment(BranchDepartment);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                {
                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                    ViewBag.closePopup = "true";
                    model.Id = BranchDepartment.Id;
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
            var BranchDepartment = BranchDepartmentRepository.GetBranchDepartmentById(Id.Value);
            if (BranchDepartment != null && BranchDepartment.IsDeleted != true)
            {
                var model = new BranchDepartmentViewModel();
                AutoMapper.Mapper.Map(BranchDepartment, model);
              //  model.DepartmentList = Helpers.SelectListHelper.GetSelectList_Category("Department", model.Staff_DepartmentId, "Name", false);
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
        public ActionResult Edit(BranchDepartmentViewModel model)
        {
            var urlRefer = Request["UrlReferrer"];
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var BranchDepartment = BranchDepartmentRepository.GetBranchDepartmentById(model.Id);
                    AutoMapper.Mapper.Map(model, BranchDepartment);
                    BranchDepartment.ModifiedUserId = WebSecurity.CurrentUserId;
                    BranchDepartment.ModifiedDate = DateTime.Now;
                    BranchDepartmentRepository.UpdateBranchDepartment(BranchDepartment);

                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                    if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                    {
                        TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                        ViewBag.closePopup = "true";
                        model.Id = BranchDepartment.Id;
                        ViewBag.urlRefer = urlRefer;
                        return View(model);
                    }
                }

                return Redirect(urlRefer);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        #endregion

        #region Delete
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            try
            {
                // var deleteClassId = int.Parse(Request["DeleteId"], CultureInfo.InvariantCulture);
                //  BranchDepartmentRepository.DeleteBranchDepartment(Id);
                var branchDepartment = BranchDepartmentRepository.GetBranchDepartmentById(Id);
                var staff = staffRepository.GetAllStaffs().Where(x => x.BranchDepartmentId == Id);
                foreach (var item in staff)
                {
                    item.IsDeleted = true;
                    staffRepository.UpdateStaffs(item);
                }
                branchDepartment.IsDeleted = true;
                BranchDepartmentRepository.UpdateBranchDepartment(branchDepartment);
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.DeleteSuccess;
                return RedirectToAction("Diagrams", "Branch", new { area = "Staff" });
            }
            catch (DbUpdateException)
            {
                TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.RelationError;
                return RedirectToAction("Index");
            }
        }
        #endregion

        //#region Detail
        //public PartialViewResult Detail(int? id)
        //{
        //    var BranchDepartment = BranchDepartmentRepository.GetvwBranchDepartmentById(id.Value);
        //    if (BranchDepartment != null && BranchDepartment.IsDeleted != true)
        //    {
        //        var model = new BranchDepartmentViewModel();
        //        AutoMapper.Mapper.Map(BranchDepartment, model);
        //        var list = staffRepository.GetAllStaffs().Where(x => x.BranchDepartmentId == model.Id);
        //        model.StaffList = list.Select(item => new StaffsViewModel
        //        {
        //            Id = item.Id,
        //            CreatedUserId = item.CreatedUserId,
        //            CreatedDate = item.CreatedDate,
        //            ModifiedUserId = item.ModifiedUserId,
        //            ModifiedDate = item.ModifiedDate,
        //            Name = item.Name,
        //            Birthday = item.Birthday,
        //            ProfileImage = item.ProfileImage,
        //            Address = item.Address,
        //            Gender = item.Gender,
        //            Ethnic = item.Ethnic,
        //            Phone = item.Phone,
        //            Email = item.Email,
        //            Code = item.Code,
        //            BranchDepartmentId = item.BranchDepartmentId,
        //            DistrictId = item.DistrictId,
        //            IdCardDate = item.IdCardDate,
        //            IdCardIssued = item.IdCardIssued,
        //            IdCardNumber = item.IdCardNumber,
        //            Language = item.Language,
        //            Literacy = item.Literacy,
        //            MaritalStatus = item.MaritalStatus,
        //            Position = item.Position,
        //            Religion = item.Religion,
        //            WardId = item.WardId,
        //            Technique = item.Technique,
        //            ProvinceId = item.ProvinceId

        //        }).ToList().OrderBy(x => x.Code);
        //        foreach (var item in model.StaffList)
        //        {
        //            item.PositionName=CategoryRepository.GetAllCategories().Where(x => x.Code == "position" && x.Value == item.Position).FirstOrDefault().Name;
        //        }
        //        ViewBag.AccessRightCreate = SecurityFilter.AccessRight("Create", "Staffs", "Staff");
        //        ViewBag.AccessRightEdit = SecurityFilter.AccessRight("Edit", "Staffs", "Staff");
        //        ViewBag.AccessRightDelete = SecurityFilter.AccessRight("Delete", "Staffs", "Staff");
        //        ViewBag.AlertMessage = TempData["AlertMessage"];
        //        return PartialView(model);
        //    }

        //    return null;
        //}
        //#endregion

    }
}
