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
    public class DotBCBHXHController : Controller
    {
        private readonly IDotBCBHXHRepository DotBCBHXHRepository;
        private readonly IUserRepository userRepository;
        private readonly IDotBCBHXHDetailRepository DotBCBHXHDetailRepository;
        private readonly IStaffSocialInsuranceRepository StaffSocialInsuranceRepository;
        public DotBCBHXHController(
            IDotBCBHXHRepository _DotBCBHXH
            , IUserRepository _user
            , IDotBCBHXHDetailRepository _DotBCBHXHDetail
            , IStaffSocialInsuranceRepository _StaffSocialInsurance
            )
        {
            DotBCBHXHRepository = _DotBCBHXH;
            userRepository = _user;
            DotBCBHXHDetailRepository = _DotBCBHXHDetail;
            StaffSocialInsuranceRepository = _StaffSocialInsurance;
        }

        #region Index

        public ViewResult Index(string txtSearch)
        {

            IQueryable<DotBCBHXHViewModel> q = DotBCBHXHRepository.GetAllDotBCBHXH()
                .Select(item => new DotBCBHXHViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    BatchNumber = item.BatchNumber
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
            var model = new DotBCBHXHViewModel();
            model.Month = DateTime.Now.Month;
            model.Year = DateTime.Now.Year;
            model.Name = "Danh sách bảo hiểm";
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(DotBCBHXHViewModel model)
        {
            if (ModelState.IsValid)
            {
                var itemp = DotBCBHXHRepository.GetAllDotBCBHXH().Where(item => item.BatchNumber == model.BatchNumber && item.Month == model.Month && item.Year == model.Year).ToList();
                if(itemp.Count()>0)
                {
                    TempData[Globals.FailedMessageKey] = "Đã tạo đợt "+ model.BatchNumber + " tháng "+ model.Month +" năm "+ model.Year;
                    return RedirectToAction("Index");
                }
                var DotBCBHXH = new DotBCBHXH();
                AutoMapper.Mapper.Map(model, DotBCBHXH);
                DotBCBHXH.IsDeleted = false;
                DotBCBHXH.CreatedUserId = WebSecurity.CurrentUserId;
                DotBCBHXH.ModifiedUserId = WebSecurity.CurrentUserId;
                DotBCBHXH.AssignedUserId = WebSecurity.CurrentUserId;
                DotBCBHXH.CreatedDate = DateTime.Now;
                DotBCBHXH.ModifiedDate = DateTime.Now;
                DotBCBHXHRepository.InsertDotBCBHXH(DotBCBHXH);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var DotBCBHXH = DotBCBHXHRepository.GetDotBCBHXHById(Id.Value);
            var detail = DotBCBHXHDetailRepository.GetAllvwDotBCBHXHDetailByDotBCBHXHId(Id.Value).ToList();
            if (DotBCBHXH != null && DotBCBHXH.IsDeleted != true)
            {
                var model = new DotBCBHXHViewModel();
                AutoMapper.Mapper.Map(DotBCBHXH, model);
                model.ListDotBCBHXHDetail = new List<DotBCBHXHDetailViewModel>();
                foreach (var item in detail)
                {
                    var detailModel = new DotBCBHXHDetailViewModel();
                    AutoMapper.Mapper.Map(item, detailModel);
                    model.ListDotBCBHXHDetail.Add(detailModel);
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
        public ActionResult Edit(DotBCBHXHViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var DotBCBHXH = DotBCBHXHRepository.GetDotBCBHXHById(model.Id);
                    AutoMapper.Mapper.Map(model, DotBCBHXH);
                    DotBCBHXH.ModifiedUserId = WebSecurity.CurrentUserId;
                    DotBCBHXH.ModifiedDate = DateTime.Now;
                    DotBCBHXHRepository.UpdateDotBCBHXH(DotBCBHXH);
                    //var DotBCBHXHDetail = DotBCBHXHDetailRepository.GetAllDotBCBHXHDetail().Where(u => u.DotBCBHXHId == DotBCBHXH.Id).ToList();
                    //foreach (var item in DotBCBHXHDetail)
                    //{
                    //    var SocialInsurance = StaffSocialInsuranceRepository.GetStaffSocialInsuranceById(item.SocialInsuranceId.Value);
                    //    if (SocialInsurance != null)
                    //    {
                    //        item.StaffId = SocialInsurance.StaffId;
                    //        item.MedicalCode = SocialInsurance.MedicalCode;
                    //        item.MedicalStartDate = SocialInsurance.MedicalStartDate;
                    //        item.MedicalEndDate = SocialInsurance.MedicalEndDate;
                    //        item.MedicalIssue = SocialInsurance.MedicalIssue;
                    //        item.MedicalDefaultValue = SocialInsurance.MedicalDefaultValue;
                    //        item.SocietyCode = SocialInsurance.SocietyCode;
                    //        item.SocietyStartDate = SocialInsurance.SocietyStartDate;
                    //        item.SocietyEndDate = SocialInsurance.SocietyEndDate;
                    //        item.SocietyIssue = SocialInsurance.SocietyIssue;
                    //        item.SocietyDefaultValue = SocialInsurance.SocietyDefaultValue;
                    //        item.PC_CV = SocialInsurance.PC_CV;
                    //        item.PC_TNVK = SocialInsurance.PC_TNVK;
                    //        item.PC_TNN = SocialInsurance.PC_TNN;
                    //        item.PC_Khac = SocialInsurance.PC_Khac;
                    //        item.TienLuong = SocialInsurance.TienLuong;
                    //        item.Note = SocialInsurance.Note;
                    //        item.Status = SocialInsurance.Status;
                    //        DotBCBHXHDetailRepository.UpdateDotBCBHXHDetail(item);
                    //    }
                    //}

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
        public ActionResult AddSocialInsurance(int? DotBCBHXHId, string Type, int? StaffId)
        {



            var DotBCBHXHDetail = new DotBCBHXHDetail();
            DotBCBHXHDetail.IsDeleted = false;
            DotBCBHXHDetail.CreatedUserId = WebSecurity.CurrentUserId;
            DotBCBHXHDetail.ModifiedUserId = WebSecurity.CurrentUserId;
            DotBCBHXHDetail.AssignedUserId = WebSecurity.CurrentUserId;
            DotBCBHXHDetail.CreatedDate = DateTime.Now;
            DotBCBHXHDetail.ModifiedDate = DateTime.Now;
            DotBCBHXHDetail.DotBCBHXHId = DotBCBHXHId;
            //DotBCBHXHDetail.SocialInsuranceId = SocialInsuranceId;
            DotBCBHXHDetail.Type = Type;
            DotBCBHXHDetail.StaffId = StaffId;
            DotBCBHXHDetailRepository.InsertDotBCBHXHDetail(DotBCBHXHDetail);

            return Content("success");

        }
        #region Detail
        public ActionResult Detail(int? Id)
        {
            var DotBCBHXH = DotBCBHXHRepository.GetDotBCBHXHById(Id.Value);
            var detail = DotBCBHXHDetailRepository.GetAllvwDotBCBHXHDetailByDotBCBHXHId(Id.Value).ToList();
            if (DotBCBHXH != null && DotBCBHXH.IsDeleted != true)
            {
                var model = new DotBCBHXHViewModel();
                AutoMapper.Mapper.Map(DotBCBHXH, model);
                model.ListDotBCBHXHDetail = new List<DotBCBHXHDetailViewModel>();
                foreach (var item in detail)
                {
                    var detailModel = new DotBCBHXHDetailViewModel();
                    AutoMapper.Mapper.Map(item, detailModel);
                    model.ListDotBCBHXHDetail.Add(detailModel);
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
                    var item = DotBCBHXHRepository.GetDotBCBHXHById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        {
                            TempData["FailedMessage"] = "NotOwner";
                            return RedirectToAction("Index");
                        }

                        item.IsDeleted = true;
                        DotBCBHXHRepository.UpdateDotBCBHXH(item);
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
