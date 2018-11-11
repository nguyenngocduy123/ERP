using System.Globalization;
using Erp.BackOffice.Staff.Models;
using Erp.BackOffice.Filters;
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
    public class RegisterForOvertimeController : Controller
    {
        private readonly IRegisterForOvertimeRepository RegisterForOvertimeRepository;
        private readonly IUserRepository userRepository;

        public RegisterForOvertimeController(
            IRegisterForOvertimeRepository _RegisterForOvertime
            , IUserRepository _user
            )
        {
            RegisterForOvertimeRepository = _RegisterForOvertime;
            userRepository = _user;
        }

        #region Index

        public ViewResult Index(string Name, string Code, string CodeStaff, int? branchId, int? DepartmentId)
        {
            var start_date = Request["start_date"];
            var end_date = Request["end_date"];
            IEnumerable<RegisterForOvertimeViewModel> q = RegisterForOvertimeRepository.GetAllvwRegisterForOvertime()
                .Select(item => new RegisterForOvertimeViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    BranchDepartmentId=item.BranchDepartmentId,
                    Code=item.Code,
                    CodeStaff=item.CodeStaff,
                    DayOvertime=item.DayOvertime,
                    EndHour=item.EndHour,
                    Note=item.Note,
                    Sale_BranchId=item.Sale_BranchId,
                    StaffId=item.StaffId,
                    StartHour=item.StartHour,
                    BranchName=item.BranchName,
                    ProfileImage=item.ProfileImage
                }).OrderByDescending(m => m.ModifiedDate).ToList();
            if (!string.IsNullOrEmpty(CodeStaff))
            {
                q = q.Where(item => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.CodeStaff).ToLower().Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(CodeStaff).ToLower())).ToList();
                //bIsSearch = true;
            }
            if (!string.IsNullOrEmpty(Code))
            {
                q = q.Where(item => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.Code).ToLower().Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(Code).ToLower())).ToList();
               // bIsSearch = true;
            }
            if (!string.IsNullOrEmpty(Name))
            {
                q = q.Where(item => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.Name).ToLower().Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(Name).ToLower())).ToList();
              //  bIsSearch = true;
            }
            if (branchId != null && branchId.Value > 0)
            {
                q = q.Where(item => item.Sale_BranchId == branchId).ToList();
            }
            if (DepartmentId != null && DepartmentId.Value > 0)
            {
                q = q.Where(item => item.BranchDepartmentId == DepartmentId).ToList();
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
                        q = q.Where(x => start_d <= x.DayOvertime && x.DayOvertime <= end_d);
                    }
                }
            }
            var user = userRepository.GetUserById(WebSecurity.CurrentUserId);
            if (user.UserTypeId == 1)
            {
                q = q.OrderByDescending(m => m.ModifiedDate).ToList();

            }
            else
            {
                q = q.Where(x => x.Sale_BranchId == user.BranchId).OrderByDescending(m => m.ModifiedDate).ToList();

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
            var model = new RegisterForOvertimeViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(RegisterForOvertimeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var RegisterForOvertime = new RegisterForOvertime();
                AutoMapper.Mapper.Map(model, RegisterForOvertime);
                RegisterForOvertime.IsDeleted = false;
                RegisterForOvertime.CreatedUserId = WebSecurity.CurrentUserId;
                RegisterForOvertime.ModifiedUserId = WebSecurity.CurrentUserId;
                RegisterForOvertime.AssignedUserId = WebSecurity.CurrentUserId;
                RegisterForOvertime.CreatedDate = DateTime.Now;
                RegisterForOvertime.ModifiedDate = DateTime.Now;
                string strStartTime = RegisterForOvertime.DayOvertime.Value.ToString("dd/MM/yyyy") + " " + model.str_StartHour;
                DateTime StartTime = DateTime.ParseExact(strStartTime, "dd/MM/yyyy HH:mm", null);
                string strEndTime = RegisterForOvertime.DayOvertime.Value.ToString("dd/MM/yyyy") + " " + model.str_EndHour;
                DateTime EndTime = DateTime.ParseExact(strEndTime, "dd/MM/yyyy HH:mm", null);
                RegisterForOvertime.StartHour = StartTime;
                RegisterForOvertime.EndHour = EndTime;
                RegisterForOvertimeRepository.InsertRegisterForOvertime(RegisterForOvertime);

                var prefix1 = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_RegisterForOverTime");
                RegisterForOvertime.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix1, RegisterForOvertime.Id);
                RegisterForOvertimeRepository.UpdateRegisterForOvertime(RegisterForOvertime);
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                {
                    ViewBag.closePopup = "true";
                    model.Id = RegisterForOvertime.Id;
                    return View(model);
                }
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var RegisterForOvertime = RegisterForOvertimeRepository.GetvwRegisterForOvertimeById(Id.Value);
            if (RegisterForOvertime != null && RegisterForOvertime.IsDeleted != true)
            {
                var model = new RegisterForOvertimeViewModel();
                AutoMapper.Mapper.Map(RegisterForOvertime, model);
                model.str_StartHour = model.StartHour.Value.ToString("HH:mm");
                model.str_EndHour = model.EndHour.Value.ToString("HH:mm");
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
        public ActionResult Edit(RegisterForOvertimeViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var RegisterForOvertime = RegisterForOvertimeRepository.GetRegisterForOvertimeById(model.Id);
                    AutoMapper.Mapper.Map(model, RegisterForOvertime);
                    RegisterForOvertime.ModifiedUserId = WebSecurity.CurrentUserId;
                    RegisterForOvertime.ModifiedDate = DateTime.Now;
                    string strStartTime = RegisterForOvertime.DayOvertime.Value.ToString("dd/MM/yyyy") + " " + model.str_StartHour;
                    DateTime StartTime = DateTime.ParseExact(strStartTime, "dd/MM/yyyy HH:mm", null);
                    string strEndTime = RegisterForOvertime.DayOvertime.Value.ToString("dd/MM/yyyy") + " " + model.str_EndHour;
                    DateTime EndTime = DateTime.ParseExact(strEndTime, "dd/MM/yyyy HH:mm", null);
                    RegisterForOvertime.StartHour = StartTime;
                    RegisterForOvertime.EndHour = EndTime;
                    RegisterForOvertimeRepository.UpdateRegisterForOvertime(RegisterForOvertime);

                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                    if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                    {
                        ViewBag.closePopup = "true";
                        model.Id = RegisterForOvertime.Id;
                        return View(model);
                    }
                }

                return View(model);
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
            var RegisterForOvertime = RegisterForOvertimeRepository.GetRegisterForOvertimeById(Id.Value);
            if (RegisterForOvertime != null && RegisterForOvertime.IsDeleted != true)
            {
                var model = new RegisterForOvertimeViewModel();
                AutoMapper.Mapper.Map(RegisterForOvertime, model);
                
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
                    var item = RegisterForOvertimeRepository.GetRegisterForOvertimeById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if(item != null)
                    {
                        //if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        //{
                        //    TempData["FailedMessage"] = "NotOwner";
                        //    return RedirectToAction("Index");
                        //}

                        item.IsDeleted = true;
                        RegisterForOvertimeRepository.UpdateRegisterForOvertime(item);
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

        #region List

        public ViewResult List(int? StaffId)
        {
            IEnumerable<RegisterForOvertimeViewModel> q = RegisterForOvertimeRepository.GetAllvwRegisterForOvertime()
                .Select(item => new RegisterForOvertimeViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    BranchDepartmentId = item.BranchDepartmentId,
                    Code = item.Code,
                    CodeStaff = item.CodeStaff,
                    DayOvertime = item.DayOvertime,
                    EndHour = item.EndHour,
                    Note = item.Note,
                    Sale_BranchId = item.Sale_BranchId,
                    StaffId = item.StaffId,
                    StartHour = item.StartHour,
                    BranchName = item.BranchName
                }).OrderByDescending(m => m.ModifiedDate).ToList();
            if (StaffId != null && StaffId.Value > 0)
            {
                q = q.Where(x => x.StaffId == StaffId).ToList();
            }
            else
            {
                q = null;
            }
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        #endregion
    }
}
