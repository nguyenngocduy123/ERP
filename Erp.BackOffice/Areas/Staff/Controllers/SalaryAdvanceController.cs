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
using Erp.BackOffice.Helpers;

namespace Erp.BackOffice.Staff.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class SalaryAdvanceController : Controller
    {
        private readonly ISalaryAdvanceRepository SalaryAdvanceRepository;
        private readonly IUserRepository userRepository;

        public SalaryAdvanceController(
            ISalaryAdvanceRepository _SalaryAdvance
            , IUserRepository _user
            )
        {
            SalaryAdvanceRepository = _SalaryAdvance;
            userRepository = _user;
        }

        #region Index

        public ViewResult Index(string Code, string Name, string CodeStaff,string Status, int? branchId, int? DepartmentId)
        {
            var start = Request["start"];
            var end = Request["end"];
            IEnumerable<SalaryAdvanceViewModel> q = SalaryAdvanceRepository.GetAllvwSalaryAdvance()
                .Select(item => new SalaryAdvanceViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    CodeAdvance = item.CodeAdvance,
                    Pay=item.Pay,
                    StaffId=item.StaffId,
                    Status=item.Status,
                    Name=item.Name,
                    ProfileImage=item.ProfileImage,
                    CodeStaff=item.CodeStaff,
                    BranchDepartmentId=item.BranchDepartmentId,
                    BranchName=item.BranchName,
                    Sale_BranchId=item.Sale_BranchId,
                    Staff_DepartmentId=item.Staff_DepartmentId,
                    Note=item.Note,
                    DayAdvance = item.DayAdvance
                }).ToList();
            if (!string.IsNullOrEmpty(Code))
            {
                q = q.Where(item => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.CodeAdvance).ToLower().Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(Code).ToLower()));
            }

            if (!string.IsNullOrEmpty(CodeStaff))
            {
                q = q.Where(item => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.CodeStaff).ToLower().Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(CodeStaff).ToLower()));
            }
            if (!string.IsNullOrEmpty(Name))
            {
                q = q.Where(item => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.Name).ToLower().Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(Name).ToLower()));
            }
            if (!string.IsNullOrEmpty(Status))
            {
                q = q.Where(item => item.Status == Status);
            }
            if (branchId != null && branchId.Value > 0)
            {
                q = q.Where(item => item.Sale_BranchId == branchId);
            }
            if (DepartmentId != null && DepartmentId.Value > 0)
            {
                q = q.Where(item => item.BranchDepartmentId == DepartmentId);
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
                        q = q.Where(x => start_d <= x.CreatedDate && x.CreatedDate <= end_d);
                    }
                }
            }
            var user = userRepository.GetUserById(WebSecurity.CurrentUserId);
            //admin thì load toàn bộ dữ liệu các chi nhánh.
            if (user.UserTypeId == 1)
            {
                q = q.OrderByDescending(m => m.ModifiedDate);
            }
            else
            {
                //nếu có quyền thêm mới mà ko phải admin thì load dữ liệu theo chi nhánh người đăng nhập
                // ngược lại thì load dữ liệu theo người tạo.
                if (Erp.BackOffice.Filters.SecurityFilter.AccessRight("Create", "SalaryAdvance", "Staff"))
                {
                    var a = q;
                    q = q.Where(x => x.Status != App_GlobalResources.Wording.StatusSalaryAdvance_Cancel&&x.Sale_BranchId==user.BranchId);
                    a = a.Where(x => x.Status == App_GlobalResources.Wording.StatusSalaryAdvance_Cancel && x.CreatedUserId == user.Id);
                    q = q.Union(a).OrderByDescending(m => m.ModifiedDate);
                }
                else
                {
                    q = q.Where(x => x.Status != App_GlobalResources.Wording.StatusSalaryAdvance_Cancel&&x.Sale_BranchId==user.BranchId).OrderByDescending(m => m.ModifiedDate);
                }
            }
            ViewBag.User = user.Id;
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        #endregion

        #region List

        public ViewResult List(int? StaffId)
        {
            IEnumerable<SalaryAdvanceViewModel> q = SalaryAdvanceRepository.GetAllvwSalaryAdvance().AsEnumerable()
                .Select(item => new SalaryAdvanceViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    CodeAdvance = item.CodeAdvance,
                    Pay = item.Pay,
                    StaffId = item.StaffId,
                    Status = item.Status,
                    Name = item.Name,
                    ProfileImage = item.ProfileImage,
                    CodeStaff = item.CodeStaff,
                    BranchDepartmentId = item.BranchDepartmentId,
                    BranchName = item.BranchName,
                    Sale_BranchId = item.Sale_BranchId,
                    Staff_DepartmentId = item.Staff_DepartmentId,
                    Note = item.Note,
                    DayAdvance = item.DayAdvance
                });
            if (StaffId != null && StaffId.Value > 0)
            {
                q = q.Where(x => x.StaffId == StaffId);
            }
            else
            {
                q = null;
            }
            return View(q);
        }
        #endregion
        #region Create
        public ViewResult Create()
        {
            var model = new SalaryAdvanceViewModel();
            var staff = Erp.BackOffice.Helpers.Common.GetStaffByCurrentUser();
            if (staff != null)
            {
                model.StaffId = staff.Id;
                model.ProfileImage = staff.ProfileImage;
            }
            model.DayAdvance = DateTime.Now;
            var user = userRepository.GetUserById(WebSecurity.CurrentUserId);
            ViewBag.UserType = user.UserTypeId;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(SalaryAdvanceViewModel model)
        {
            var urlRefer = Request["UrlReferrer"];
            if (ModelState.IsValid)
            {
                var SalaryAdvance = new SalaryAdvance();
                AutoMapper.Mapper.Map(model, SalaryAdvance);
                SalaryAdvance.IsDeleted = false;
                SalaryAdvance.CreatedUserId = WebSecurity.CurrentUserId;
                SalaryAdvance.ModifiedUserId = WebSecurity.CurrentUserId;
                SalaryAdvance.AssignedUserId = WebSecurity.CurrentUserId;
                //SalaryAdvance.CreatedDate = DateTime.Now;
                SalaryAdvance.ModifiedDate = DateTime.Now;
                SalaryAdvance.Status = "Chờ duyệt";
                SalaryAdvanceRepository.InsertSalaryAdvance(SalaryAdvance);
                //tạo mã tạm ứng lương
                var prefix = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_SalaryAdvance");
                SalaryAdvance.CodeAdvance = Erp.BackOffice.Helpers.Common.GetCode(prefix, SalaryAdvance.Id);
                SalaryAdvanceRepository.UpdateSalaryAdvance(SalaryAdvance);
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                {
                    return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
                }
                return Redirect(urlRefer);
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var SalaryAdvance = SalaryAdvanceRepository.GetvwSalaryAdvanceById(Id.Value);
            if (SalaryAdvance != null && SalaryAdvance.IsDeleted != true)
            {
                var model = new SalaryAdvanceViewModel();
                AutoMapper.Mapper.Map(SalaryAdvance, model);
                
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
        public ActionResult Edit(SalaryAdvanceViewModel model)
        {
            if (ModelState.IsValid)
            {
                var urlRefer = Request["UrlReferrer"];
                if (Request["Submit"] == "Save")
                {
                    var SalaryAdvance = SalaryAdvanceRepository.GetSalaryAdvanceById(model.Id);
                    AutoMapper.Mapper.Map(model, SalaryAdvance);
                    SalaryAdvance.ModifiedUserId = WebSecurity.CurrentUserId;
                    SalaryAdvance.ModifiedDate = DateTime.Now;
                    SalaryAdvance.Status = App_GlobalResources.Wording.StatusSalaryAdvance_Pending;
                    SalaryAdvanceRepository.UpdateSalaryAdvance(SalaryAdvance);

                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                    if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                    {
                        TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                        return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
                    }
                    return Redirect(urlRefer);
                }
            }

            return View(model);

            //if (Request.UrlReferrer != null)
            //    return Redirect(Request.UrlReferrer.AbsoluteUri);
            //return RedirectToAction("Index");
        }

        #endregion

        #region Detail
        public ActionResult Detail(int? Id)
        {
            var SalaryAdvance = SalaryAdvanceRepository.GetSalaryAdvanceById(Id.Value);
            if (SalaryAdvance != null && SalaryAdvance.IsDeleted != true)
            {
                var model = new SalaryAdvanceViewModel();
                AutoMapper.Mapper.Map(SalaryAdvance, model);
                
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
                    var item = SalaryAdvanceRepository.GetSalaryAdvanceById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if(item != null)
                    {
                        //if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        //{
                        //    TempData["FailedMessage"] = "NotOwner";
                        //    return RedirectToAction("Index");
                        //}

                        item.IsDeleted = true;
                        SalaryAdvanceRepository.UpdateSalaryAdvance(item);
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

        #region Cancel
        public ActionResult Cancel(int Id)
        {
            var q = SalaryAdvanceRepository.GetSalaryAdvanceById(Id);
            q.Status = App_GlobalResources.Wording.Cancel ;
            q.ModifiedUserId = WebSecurity.CurrentUserId;
            q.ModifiedDate = DateTime.Now;
            SalaryAdvanceRepository.UpdateSalaryAdvance(q);
            TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.CancelSuccess + " " + q.CodeAdvance;
            return RedirectToAction("Index");
        }
        #endregion

        #region Approval
        public ActionResult Approval(int Id)
        {
            // nếu trạng thái của tạm ứng lương là chờ duyệt thì chuyển thành đã duyệt
            // ngược lại thì chuyển thành Đã trả tiền 
            var q = SalaryAdvanceRepository.GetSalaryAdvanceById(Id);
            if (q.Status == App_GlobalResources.Wording.StatusSalaryAdvance_Pending)
            {
                q.Status = App_GlobalResources.Wording.StatusSalaryAdvance_Approved;
                q.ModifiedUserId = WebSecurity.CurrentUserId;
                q.ModifiedDate = DateTime.Now;
                SalaryAdvanceRepository.UpdateSalaryAdvance(q);
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.ApprovedSuccess + " " + q.CodeAdvance;
            }
            else
            {
                q.Status =App_GlobalResources.Wording.StatusSalaryAdvance_Paid;
                q.ModifiedUserId = WebSecurity.CurrentUserId;
                q.ModifiedDate = DateTime.Now;
                SalaryAdvanceRepository.UpdateSalaryAdvance(q);
                TempData[Globals.SuccessMessageKey] = "Đã trả tiền mã tạm ứng lương" + " " + q.CodeAdvance;
             
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Refure
        public ActionResult Refure(int? Id)
        {
            var q = SalaryAdvanceRepository.GetSalaryAdvanceById(Id.Value);
            if (q != null && q.IsDeleted != true)
            {
                var model = new SalaryAdvanceViewModel();
                AutoMapper.Mapper.Map(q, model);
                return View(model);
            }
            return RedirectToAction("Edit", new { Id = q.Id });
        }
        [HttpPost]
        public ActionResult Refure(SalaryAdvanceViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var q = SalaryAdvanceRepository.GetSalaryAdvanceById(model.Id);
                    q.Status =App_GlobalResources.Wording.StatusSalaryAdvance_Refure;
                    q.Note = model.Note;
                    q.ModifiedUserId = WebSecurity.CurrentUserId;
                    q.ModifiedDate = DateTime.Now;
                    SalaryAdvanceRepository.UpdateSalaryAdvance(q);
                    TempData[Globals.SuccessMessageKey] = "Bạn đã từ chối thành công mã tạm ứng lương" + " " + q.CodeAdvance;
                    return View("_ClosePopup");
                }
                return RedirectToAction("Edit", new { Id = model.Id });
            }
            TempData[Globals.SuccessMessageKey] = "Bạn đã từ chối không thành công mã tạm ứng lương" + " " + model.CodeAdvance;
            return View("_ClosePopup");
        }
        #endregion
    }
}
