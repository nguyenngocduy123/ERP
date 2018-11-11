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
    public class DotGQCDBHXHController : Controller
    {
        private readonly IDotGQCDBHXHRepository DotGQCDBHXHRepository;
        private readonly IUserRepository userRepository;
        private readonly IDotGQCDBHXHDetailRepository DotGQCDBHXHDetailRepository;
        private readonly IDayOffRepository DayOffRepository;
        private readonly IStaffSocialInsuranceRepository StaffSocialInsuranceRepository;
        public DotGQCDBHXHController(
            IDotGQCDBHXHRepository _DotQGCDBHXH
            , IUserRepository _user
            , IDotGQCDBHXHDetailRepository _DotGQCDBHXHDetail
            , IDayOffRepository _DayOff
            , IStaffSocialInsuranceRepository _StaffSocialInsurance
            )
        {
            DotGQCDBHXHRepository = _DotQGCDBHXH;
            userRepository = _user;
            DotGQCDBHXHDetailRepository = _DotGQCDBHXHDetail;
            DayOffRepository = _DayOff;
            StaffSocialInsuranceRepository = _StaffSocialInsurance;
        }

        #region Index

        public ViewResult Index(string txtSearch)
        {

            IQueryable<DotGQCDBHXHViewModel> q = DotGQCDBHXHRepository.GetAllDotGQCDBHXH()
                .Select(item => new DotGQCDBHXHViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    BatchNumber = item.BatchNumber,
                    Month = item.Month,
                    Year = item.Year
                }).OrderByDescending(m => m.ModifiedDate);

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        #endregion

        #region Create
        public ViewResult Create()
        {
            var model = new DotGQCDBHXHViewModel();
            model.Name = "Danh sách đề nghi giải quyết BHXH";
            model.Month = DateTime.Now.Month;
            model.Year = DateTime.Now.Year;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(DotGQCDBHXHViewModel model)
        {
            if (ModelState.IsValid)
            {
                var itemp = DotGQCDBHXHRepository.GetAllDotGQCDBHXH().Where(item => item.BatchNumber == model.BatchNumber && item.Month == model.Month && item.Year == model.Year).ToList();
                if (itemp.Count() > 0)
                {
                    TempData[Globals.FailedMessageKey] = "Đã tạo đợt " + model.BatchNumber + " tháng " + model.Month + " năm " + model.Year;
                    return RedirectToAction("Index");
                }
                var DotQGCDBHXH = new DotGQCDBHXH();
                AutoMapper.Mapper.Map(model, DotQGCDBHXH);
                DotQGCDBHXH.IsDeleted = false;
                DotQGCDBHXH.CreatedUserId = WebSecurity.CurrentUserId;
                DotQGCDBHXH.ModifiedUserId = WebSecurity.CurrentUserId;
                DotQGCDBHXH.AssignedUserId = WebSecurity.CurrentUserId;
                DotQGCDBHXH.CreatedDate = DateTime.Now;
                DotQGCDBHXH.ModifiedDate = DateTime.Now;
                DotGQCDBHXHRepository.InsertDotGQCDBHXH(DotQGCDBHXH);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var DotQGCDBHXH = DotGQCDBHXHRepository.GetDotGQCDBHXHById(Id.Value);
            var detail = DotGQCDBHXHDetailRepository.GetAllvwDotGQCDBHXHDetail().Where(u => u.DotGQCDBHXHId == DotQGCDBHXH.Id).ToList();
            if (DotQGCDBHXH != null && DotQGCDBHXH.IsDeleted != true)
            {
                var model = new DotGQCDBHXHViewModel();
                model.ListDotGQCDBHXHDetail = new List<DotGQCDBHXHDetailViewModel>();
                AutoMapper.Mapper.Map(DotQGCDBHXH, model);
                foreach (var item in detail)
                {
                    var detailModel = new DotGQCDBHXHDetailViewModel();
                    AutoMapper.Mapper.Map(item, detailModel);
                    model.ListDotGQCDBHXHDetail.Add(detailModel);
                }

                if (model.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                {
                    TempData["FailedMessage"] = "NotOwner";
                    return RedirectToAction("Index");
                }

                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(DotGQCDBHXHViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var DotQGCDBHXH = DotGQCDBHXHRepository.GetDotGQCDBHXHById(model.Id);
                    AutoMapper.Mapper.Map(model, DotQGCDBHXH);
                    DotQGCDBHXH.ModifiedUserId = WebSecurity.CurrentUserId;
                    DotQGCDBHXH.ModifiedDate = DateTime.Now;
                    DotGQCDBHXHRepository.UpdateDotGQCDBHXH(DotQGCDBHXH);

                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                    return RedirectToAction("Index");
                }

                return View(model);
            }

            return View(model);

            //if (Request.UrlReferrer != null)
            //    return Redirect(Request.UrlReferrer.AbsoluteUri);
            //return RedirectToAction("Index");
        }

        #endregion
        public ActionResult AddDotGQCDBHXHDetail(int? DotGQCDBHXHId, int? DayOffId, int? StaffId)
        {
            var DotGQCDBHXHDetail = new DotGQCDBHXHDetail();
            DotGQCDBHXHDetail.IsDeleted = false;
            DotGQCDBHXHDetail.CreatedUserId = WebSecurity.CurrentUserId;
            DotGQCDBHXHDetail.ModifiedUserId = WebSecurity.CurrentUserId;
            DotGQCDBHXHDetail.AssignedUserId = WebSecurity.CurrentUserId;
            DotGQCDBHXHDetail.CreatedDate = DateTime.Now;
            DotGQCDBHXHDetail.ModifiedDate = DateTime.Now;
            DotGQCDBHXHDetail.DotGQCDBHXHId = DotGQCDBHXHId;
            DotGQCDBHXHDetail.DayOffId = DayOffId;
            DotGQCDBHXHDetail.StaffId = StaffId;
            var dayoff = DayOffRepository.GetDayOffById(DayOffId.Value);
            if(dayoff != null)
            {
                DotGQCDBHXHDetail.DayEnd = dayoff.DayEnd;
                DotGQCDBHXHDetail.DayStart = dayoff.DayStart;
                DotGQCDBHXHDetail.Quantity = dayoff.Quantity;
                var SocietyCode = StaffSocialInsuranceRepository.GetStaffSocialInsuranceByStaffId(StaffId.Value);
                if(SocietyCode != null)
                {
                    DotGQCDBHXHDetail.SocietyCode = SocietyCode.SocietyCode;
                }

            }
            DotGQCDBHXHDetailRepository.InsertDotGQCDBHXHDetail(DotGQCDBHXHDetail);

            return Content("success");

        }
        #region Detail
        public ActionResult Detail(int? Id)
        {
            var DotQGCDBHXH = DotGQCDBHXHRepository.GetDotGQCDBHXHById(Id.Value);
            var detail = DotGQCDBHXHDetailRepository.GetAllvwDotGQCDBHXHDetail().Where(u => u.DotGQCDBHXHId == DotQGCDBHXH.Id).ToList();
            if (DotQGCDBHXH != null && DotQGCDBHXH.IsDeleted != true)
            {
                var model = new DotGQCDBHXHViewModel();
                model.ListDotGQCDBHXHDetail = new List<DotGQCDBHXHDetailViewModel>();
                AutoMapper.Mapper.Map(DotQGCDBHXH, model);
                foreach (var item in detail)
                {
                    var detailModel = new DotGQCDBHXHDetailViewModel();
                    AutoMapper.Mapper.Map(item, detailModel);
                    model.ListDotGQCDBHXHDetail.Add(detailModel);
                }

                if (model.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                {
                    TempData["FailedMessage"] = "NotOwner";
                    return RedirectToAction("Index");
                }

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
                    var item = DotGQCDBHXHRepository.GetDotGQCDBHXHById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        {
                            TempData["FailedMessage"] = "NotOwner";
                            return RedirectToAction("Index");
                        }

                        item.IsDeleted = true;
                        DotGQCDBHXHRepository.UpdateDotGQCDBHXH(item);
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
