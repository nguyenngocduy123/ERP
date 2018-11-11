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
using Erp.BackOffice.Areas.Administration.Models;

namespace Erp.BackOffice.Staff.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class TransferWorkController : Controller
    {
        private readonly ITransferWorkRepository TransferWorkRepository;
        private readonly IUserRepository userRepository;
        private readonly IStaffsRepository staffRepository;
        private readonly IPageRepository pageRepository;
        private readonly IUserTypePageRepository userTypePageRepository;
        private readonly ICategoryRepository CategoryRepository;
        private readonly IBranchDepartmentRepository branchDepartmentRepository;
        public TransferWorkController(
            ITransferWorkRepository _TransferWork
            , IUserRepository _user
            , IStaffsRepository staff
            ,IPageRepository page
            ,IUserTypePageRepository userTypePage
            ,ICategoryRepository Category
             , IBranchDepartmentRepository branchDepartment
            )
        {
            TransferWorkRepository = _TransferWork;
            userRepository = _user;
            staffRepository = staff;
            pageRepository = page;
            userTypePageRepository = userTypePage;
            CategoryRepository = Category;
            branchDepartmentRepository = branchDepartment;
        }

        #region Index
        public ViewResult Index(string Staff, string Code, string Status, int? branchNewId, int? DepartmentNewId, int? branchOldId, int? DepartmentOldId, string PositionNew, string PositionOld, int? UID)
        {
            var user = userRepository.GetUserById(WebSecurity.CurrentUserId);
            var start_date = Request["start_date"];
            var end_date = Request["end_date"];
            var start_DayDecision = Request["start_DayDecision"];
            var end_DayDecision = Request["end_DayDecision"];
            ViewData["Staff"] = Staff;
            ViewData["Code"] = Code;
          
            var staff = Erp.BackOffice.Helpers.Common.GetStaffByCurrentUser();
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
                    ProfileImage=item.ProfileImage,
                    CodeStaffNew=item.CodeStaffNew,
                    CodeStaffOld=item.CodeStaffOld,
                    PositionNewName=item.PositionNewName,
                    PositionOldName=item.PositionOldName
                }).ToList();
            if (!string.IsNullOrEmpty(Code))
            {
                q = q.Where(item => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.Code).ToLower().Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(Code).ToLower()));
            }

            if (!string.IsNullOrEmpty(Staff))
            {
                q = q.Where(item => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.CodeName).ToLower().Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(Staff).ToLower()));
            }
            if (!string.IsNullOrEmpty(Status))
            {
                q = q.Where(item => item.Status == Status);
            }
            if (!string.IsNullOrEmpty(PositionNew))
            {
                q = q.Where(item => item.PositionNew == PositionNew);
            }
            if (!string.IsNullOrEmpty(PositionOld))
            {
                q = q.Where(item => item.PositionOld == PositionOld);
            }
            if (branchNewId != null && branchNewId.Value > 0)
            {
                q = q.Where(item => item.BranchIdNew == branchNewId);
            }
            if (DepartmentNewId != null && DepartmentNewId.Value > 0)
            {
                q = q.Where(item => item.BranchDepartmentNewId == DepartmentNewId);
            }
            if (branchOldId != null && branchOldId.Value > 0)
            {
                q = q.Where(item => item.BranchIdOld == branchOldId);
            }
            if (DepartmentOldId != null && DepartmentOldId.Value > 0)
            {
                q = q.Where(item => item.BranchDepartmentOldId == DepartmentOldId);
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
                        q = q.Where(x => start_d <= x.DayEffective && x.DayEffective <= end_d);
                    }
                }
            }
            if (!string.IsNullOrEmpty(start_DayDecision) && !string.IsNullOrEmpty(end_DayDecision))
            {
                DateTime start_d;
                if (DateTime.TryParseExact(start_DayDecision, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out start_d))
                {
                    DateTime end_d;
                    if (DateTime.TryParseExact(end_DayDecision, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out end_d))
                    {
                        end_d = end_d.AddHours(23);
                        q = q.Where(x => start_d <= x.DayDecision && x.DayDecision <= end_d);
                    }
                }
            }
            if (UID != null && UID.Value > 0)
            {
                q = q.Where(item => item.UserId == UID);
            }
            if (user.UserTypeId == 1)
            {
                q = q.OrderByDescending(m => m.ModifiedDate);
            }
            else
            {
                if (Erp.BackOffice.Filters.SecurityFilter.AccessRight("Create", "TransferWork", "Staff"))
                {
                    var a = q;
                    q = q.Where(x => x.Status != App_GlobalResources.Wording.TransferWorkStatus_Cancel).OrderByDescending(m => m.ModifiedDate);
                    a = a.Where(x =>x.Status == App_GlobalResources.Wording.TransferWorkStatus_Cancel && x.UserId == user.Id).OrderByDescending(m => m.ModifiedDate);
                    q = q.Union(a);
                }
                else
                {
                    q = q.Where(x =>x.Status != App_GlobalResources.Wording.TransferWorkStatus_Cancel).OrderByDescending(m => m.ModifiedDate);
                }
            }
          
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            ViewBag.User = user.Id;
            return View(q);
        }
        #endregion

        #region Create
        public ViewResult Create()
        {
            var model = new TransferWorkViewModel();
            model.DayEffective = DateTime.Now;
            //model.PositionList = Helpers.SelectListHelper.GetSelectList_Category("position", null, "Value", App_GlobalResources.Wording.Empty);
            //model.DepartmentNewList = Helpers.SelectListHelper.GetSelectList_BranchDepartment(null, 0);
            //model.BranchList = Helpers.SelectListHelper.GetSelectList_Branch(null);
            //model.StaffList = Helpers.SelectListHelper.GetSelectList_Staff(null);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(TransferWorkViewModel model)
        {
            if (ModelState.IsValid)
            {
                var TransferWork = new TransferWork();
                AutoMapper.Mapper.Map(model, TransferWork);


                TransferWork.IsDeleted = false;
                TransferWork.CreatedUserId = WebSecurity.CurrentUserId;
                TransferWork.ModifiedUserId = WebSecurity.CurrentUserId;
                TransferWork.CreatedDate = DateTime.Now;
                TransferWork.ModifiedDate = DateTime.Now;
                TransferWork.DayDecision = DateTime.Now;


                var staffs = staffRepository.GetStaffsById(model.StaffId.Value);
                var user = userRepository.GetUserById(WebSecurity.CurrentUserId);
                TransferWork.BranchDepartmentOldId = staffs.BranchDepartmentId;
                TransferWork.PositionOld = staffs.Position;
                TransferWork.UserId = user.Id;
                TransferWork.Status = App_GlobalResources.Wording.TransferWorkStatus_Pending;
                TransferWork.CodeStaffOld = staffs.Code;
                //tạo mã nhân viên mới khi chuyển nhân viên đi 
                //lấy mã tăng tự động ra và cộng thêm 1 đơn vị
                var prefix1 = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_Staff");
                var department=branchDepartmentRepository.GetvwBranchDepartmentById(TransferWork.BranchDepartmentNewId.Value);
                TransferWork.CodeStaffNew = Erp.BackOffice.Helpers.Common.GetCodebyBranch(prefix1, staffs.Id, department.BranchCode);
               
                //tạo mã quyết định điều chuyển công tác
                TransferWorkRepository.InsertTransferWork(TransferWork);
                var prefix2 = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_TransferWork");
                TransferWork.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix2, TransferWork.Id);
                TransferWorkRepository.UpdateTransferWork(TransferWork);
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess + " " + TransferWork.Code;
                return RedirectToAction("Index");
            }
            TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.InsertUnsucess;
            return RedirectToAction("Index");
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var TransferWork = TransferWorkRepository.GetvwTransferWorkById(Id.Value);
            if (TransferWork != null && TransferWork.IsDeleted != true)
            {
                var model = new TransferWorkViewModel();
                AutoMapper.Mapper.Map(TransferWork, model);

                //model.PositionList = Helpers.SelectListHelper.GetSelectList_Category("position", model.PositionNew, "Value", App_GlobalResources.Wording.Empty);
                model.DepartmentNewList = Helpers.SelectListHelper.GetSelectList_BranchDepartment(model.BranchDepartmentNewId, TransferWork.BranchIdNew.Value, App_GlobalResources.Wording.Empty);
                //model.BranchList = Helpers.SelectListHelper.GetSelectList_Branch(TransferWork.BranchIdNew.Value);
               
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
        public ActionResult Edit(TransferWorkViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var TransferWork = TransferWorkRepository.GetTransferWorkById(model.Id);
                    AutoMapper.Mapper.Map(model, TransferWork);
                    var prefix1 = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_Staff");
                    var department = branchDepartmentRepository.GetvwBranchDepartmentById(TransferWork.BranchDepartmentNewId.Value);
                    TransferWork.CodeStaffNew = Erp.BackOffice.Helpers.Common.GetCodebyBranch(prefix1, model.StaffId.Value, department.BranchCode);
                    TransferWork.ModifiedUserId = WebSecurity.CurrentUserId;
                    TransferWork.ModifiedDate = DateTime.Now;
                    TransferWork.Status = App_GlobalResources.Wording.StatusSalaryAdvance_Pending;
                    TransferWorkRepository.UpdateTransferWork(TransferWork);

                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess + " " + TransferWork.Code;
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.UpdateUnsuccess + " " + model.Code;
            return RedirectToAction("Index");
        }

        #endregion

        #region Delete
        //[HttpPost]
        //public ActionResult Delete()
        //{
        //    try
        //    {
        //        string idDeleteAll = Request["DeleteId-checkbox"];
        //        string[] arrDeleteId = idDeleteAll.Split(',');
        //        string code = "";
        //        for (int i = 0; i < arrDeleteId.Count(); i++)
        //        {
        //            var item = TransferWorkRepository.GetTransferWorkById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
        //            if (item != null)
        //            {
        //                if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
        //                {
        //                    TempData["FailedMessage"] = "NotOwner";
        //                    return RedirectToAction("Index");
        //                }

        //                item.IsDeleted = true;
        //                TransferWorkRepository.UpdateTransferWork(item);
        //                code += item.Code + " ; ";
        //            }
        //        }
        //        TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.DeleteSuccess + " " + code;
        //        return RedirectToAction("Index");
        //    }
        //    catch (DbUpdateException)
        //    {
        //        TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.RelationError;
        //        return RedirectToAction("Index");
        //    }
        //}
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            try
            {

                var item = TransferWorkRepository.GetTransferWorkById(id.Value);
                if (item != null)
                {
                    //if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                    //{
                    //    TempData["FailedMessage"] = "NotOwner";
                    //    return RedirectToAction("Index");
                    //}

                    item.IsDeleted = true;
                    TransferWorkRepository.UpdateTransferWork(item);
                }
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.DeleteSuccess + " " + item.Code;
                return RedirectToAction("Index");
            }
            catch (DbUpdateException)
            {
                TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.RelationError;
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region Cancel
        public ActionResult Cancel(int Id)
        {
            var q = TransferWorkRepository.GetTransferWorkById(Id);
            q.Status = App_GlobalResources.Wording.TransferWorkStatus_Cancel;
            TransferWorkRepository.UpdateTransferWork(q);
            TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.CancelSuccess + " " + q.Code;
            return RedirectToAction("Index");
        }
        #endregion

        #region Approval
        public ActionResult Approval(int Id)
        {
            // nếu trạng thái của quyết định điều chuyển là chờ duyệt thì chuyển thành đã duyệt
            // ngược lại thì chuyển thành Đã chuyển và cập nhật lại dữ liệu nhân viên trong bảng staff
            var q = TransferWorkRepository.GetTransferWorkById(Id);
            if (q.Status == App_GlobalResources.Wording.TransferWorkStatus_Pending)
            {
                q.Status = App_GlobalResources.Wording.TransferWorkStatus_Approved;
                TransferWorkRepository.UpdateTransferWork(q);
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.ApprovedSuccess + " " + q.Code;
                return RedirectToAction("Index");
            }
            else
            {
                q.Status = App_GlobalResources.Wording.TransferWorkStatus_Delivered;
                TransferWorkRepository.UpdateTransferWork(q);
                var staff = staffRepository.GetStaffsById(q.StaffId.Value);
                //staff.BranchDepartmentId = q.BranchDepartmentNewId;
                //staff.Position = q.PositionNew;
                staff.Code = q.CodeStaffNew;
                staffRepository.UpdateStaffs(staff);
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.DeliveredSuccess + " " + q.Code;
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region Từ chối duyệt yêu cầu.
        public ActionResult Refure(int? Id)
        {
            var q = TransferWorkRepository.GetTransferWorkById(Id.Value);
            if (q != null && q.IsDeleted != true)
            {
                var model = new TransferWorkViewModel();
                AutoMapper.Mapper.Map(q, model);
                return View(model);
            }
            return RedirectToAction("Edit", new { Id = q.Id });
        }
        [HttpPost]
        public ActionResult Refure(TransferWorkViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var q = TransferWorkRepository.GetTransferWorkById(model.Id);
                    q.Status = App_GlobalResources.Wording.StatusSalaryAdvance_Refure;
                    q.Reason = model.Reason;
                    TransferWorkRepository.UpdateTransferWork(q);
                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.CancelSuccess + " " + q.Code;
                    return View("_ClosePopup");
                }
                return RedirectToAction("Edit", new { Id = model.Id });
            }
            TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.CancelUnsuccess + " " + model.Code;
            return RedirectToAction("Index");
        }
        #endregion

        #region List
        public ViewResult List(int StaffId)
        {
            IEnumerable<TransferWorkViewModel> q = TransferWorkRepository.GetvwAllTransferWork().Where(x=>x.StaffId==StaffId)
                .Select(item => new TransferWorkViewModel
                {
                    Id = item.Id,
                    BranchDepartmentNewId = item.BranchDepartmentNewId,
                    BranchNameNew = item.BranchNameNew,
                    Code = item.Code,
                    DayDecision = item.DayDecision,
                    DayEffective = item.DayEffective,
                    NameStaff = item.NameStaff,
                    NameUser = item.NameUser,
                    PositionNew = item.PositionNew,
                    Reason = item.Reason,
                    Staff_DepartmentNew = item.Staff_DepartmentNew,
                    StaffId = item.StaffId,
                    Status = item.Status,
                    BranchIdNew = item.BranchIdNew,
                    PositionOldName=item.PositionOldName,
                    PositionNewName=item.PositionNewName
                }).ToList();
          
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        #endregion

        #region Detail
        public ActionResult Detail(int? Id)
        {
            var transferWork = TransferWorkRepository.GetvwTransferWorkById(Id.Value);
            if (transferWork != null && transferWork.IsDeleted != true)
            {
                var model = new TransferWorkViewModel();
                AutoMapper.Mapper.Map(transferWork, model);
                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
