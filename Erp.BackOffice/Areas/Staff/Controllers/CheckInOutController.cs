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
    public class CheckInOutController : Controller
    {
        private readonly ICheckInOutRepository CheckInOutRepository;
        private readonly IUserRepository userRepository;

        public CheckInOutController(
            ICheckInOutRepository _CheckInOut
            , IUserRepository _user
            )
        {
            CheckInOutRepository = _CheckInOut;
            userRepository = _user;
        }

        #region Index
        public ViewResult Index(string Name, string Code, int? Branch, int? Department)
        {
            var user = userRepository.GetUserById(WebSecurity.CurrentUserId);
            IEnumerable<CheckInOutViewModel> q = CheckInOutRepository.GetAllvwCheckInOut()
                .Select(item => new CheckInOutViewModel
                {
                    MachineNo = item.MachineNo,
                    UserId = item.UserId,
                    TimeStr = item.TimeStr,
                    BranchDepartmentId=item.BranchDepartmentId,
                    Sale_BranchId=item.Sale_BranchId,
                    Code=item.Code,
                    StaffId=item.StaffId,
                    Name=item.Name
                }).OrderByDescending(m => m.TimeStr);
            if (!string.IsNullOrEmpty(Name))
            {
                q = q.Where(x => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(x.Name).ToLower().Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(Name).ToLower()));
            }
            if (!string.IsNullOrEmpty(Code))
            {
                q = q.Where(x => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(x.Code).ToLower().Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(Code).ToLower()));
            }
            if (Branch != null && Branch.Value > 0)
            {
                q = q.Where(x => x.Sale_BranchId==Branch);
            }
            else
            {
                if (Request["search"] == null)
                {
                    //if (user.BranchId != null && user.BranchId.Value > 0)
                    //{
                    //    q = q.Where(item => item.Sale_BranchId == user.BranchId);
                    //}
                }
            }
            if (Department != null && Department.Value > 0)
            {
                q = q.Where(x => x.BranchDepartmentId == Department);
            }
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        #endregion
        #region List
        public ViewResult List(int? StaffId)
        {
            var user = userRepository.GetUserById(WebSecurity.CurrentUserId);
            IEnumerable<CheckInOutViewModel> q = CheckInOutRepository.GetAllvwCheckInOut()
                .Select(item => new CheckInOutViewModel
                {
                    MachineNo = item.MachineNo,
                    UserId = item.UserId,
                    TimeStr = item.TimeStr,
                    BranchDepartmentId = item.BranchDepartmentId,
                    Sale_BranchId = item.Sale_BranchId,
                    Code = item.Code,
                    StaffId = item.StaffId,
                    Name = item.Name
                }).OrderByDescending(m => m.TimeStr);

            if (StaffId != null && StaffId.Value > 0)
            {
                q = q.Where(x => x.StaffId == StaffId.Value);
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
        //#region Edit
        //[HttpPost]
        //public ActionResult Edit(CheckInOutViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (Request["Submit"] == "Save")
        //        {
        //            var CheckInOut = CheckInOutRepository.GetCheckInOutById(model.Id);
        //            AutoMapper.Mapper.Map(model, CheckInOut);
        //            CheckInOut.ModifiedUserId = WebSecurity.CurrentUserId;
        //            CheckInOut.ModifiedDate = DateTime.Now;
        //            CheckInOutRepository.UpdateCheckInOut(CheckInOut);

        //            TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
        //            return RedirectToAction("Index");
        //        }

        //        return View(model);
        //    }

        //    return View(model);

        //    //if (Request.UrlReferrer != null)
        //    //    return Redirect(Request.UrlReferrer.AbsoluteUri);
        //    //return RedirectToAction("Index");
        //}

        //#endregion

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
                    var item = CheckInOutRepository.GetCheckInOutById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        
                        CheckInOutRepository.UpdateCheckInOut(item);
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
    }
}
