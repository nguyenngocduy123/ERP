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
using Erp.Domain.Helper;

namespace Erp.BackOffice.Staff.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class DotBCBHXHDetailController : Controller
    {
        private readonly IDotBCBHXHDetailRepository DotBCBHXHDetailRepository;
        private readonly IUserRepository userRepository;
        private readonly ILocationRepository locationRepository;
        private readonly IStaffSocialInsuranceRepository StaffSocialInsuranceRepository;
        private readonly IStaffsRepository staffRepository;
        public DotBCBHXHDetailController(
            IDotBCBHXHDetailRepository _DotBCBHXHDetail
            , IUserRepository _user
            , ILocationRepository _location
            , IStaffSocialInsuranceRepository _StaffSocialInsurance
            , IStaffsRepository staff
            )
        {
            DotBCBHXHDetailRepository = _DotBCBHXHDetail;
            userRepository = _user;
            locationRepository = _location;
            StaffSocialInsuranceRepository = _StaffSocialInsurance;
            staffRepository = staff;
        }

        #region Index

        public ViewResult Index(string txtSearch)
        {

            IQueryable<DotBCBHXHDetailViewModel> q = DotBCBHXHDetailRepository.GetAllDotBCBHXHDetail()
                .Select(item => new DotBCBHXHDetailViewModel
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

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        #endregion

        #region Create
        public ViewResult Create()
        {
            var model = new DotBCBHXHDetailViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(DotBCBHXHDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var DotBCBHXHDetail = new DotBCBHXHDetail();
                AutoMapper.Mapper.Map(model, DotBCBHXHDetail);
                DotBCBHXHDetail.IsDeleted = false;
                DotBCBHXHDetail.CreatedUserId = WebSecurity.CurrentUserId;
                DotBCBHXHDetail.ModifiedUserId = WebSecurity.CurrentUserId;
                DotBCBHXHDetail.AssignedUserId = WebSecurity.CurrentUserId;
                DotBCBHXHDetail.CreatedDate = DateTime.Now;
                DotBCBHXHDetail.ModifiedDate = DateTime.Now;
                DotBCBHXHDetailRepository.InsertDotBCBHXHDetail(DotBCBHXHDetail);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var province = locationRepository.GetProvinceList();
            var DotBCBHXHDetail = DotBCBHXHDetailRepository.GetDotBCBHXHDetailById(Id.Value);
            if (DotBCBHXHDetail != null && DotBCBHXHDetail.IsDeleted != true)
            {
                var model = new DotBCBHXHDetailViewModel();
                var StaffSocialInsurance = StaffSocialInsuranceRepository.GetStaffSocialInsuranceByStaffId(DotBCBHXHDetail.StaffId.Value);
                if (StaffSocialInsurance != null)
                {
                    var ProvinceSociety = province.FirstOrDefault(x => x.Name.Contains(StaffSocialInsurance.SocietyIssue == null ? "" : StaffSocialInsurance.SocietyIssue));
                    var ProvinceMedical = province.FirstOrDefault(x => x.Name.Contains(StaffSocialInsurance.MedicalIssue == null ? "" : StaffSocialInsurance.MedicalIssue));
                    model.ProvinceSociety = Convert.ToInt32(ProvinceSociety == null ? "0" : ProvinceSociety.Id);
                    model.ProvinceMedical = Convert.ToInt32(ProvinceMedical == null ? "0" : ProvinceMedical.Id);
                    model.SocialInsuranceId = StaffSocialInsurance.Id;
                    model.MedicalCode = StaffSocialInsurance.MedicalCode;
                    model.MedicalStartDate = StaffSocialInsurance.MedicalStartDate;
                    model.MedicalEndDate = StaffSocialInsurance.MedicalEndDate;
                    model.MedicalIssue = StaffSocialInsurance.MedicalIssue;
                    model.MedicalDefaultValue = StaffSocialInsurance.MedicalDefaultValue.ToString();
                    model.SocietyCode = StaffSocialInsurance.SocietyCode;
                    model.SocietyStartDate = StaffSocialInsurance.SocietyStartDate;
                    model.SocietyEndDate = StaffSocialInsurance.SocietyEndDate;
                    model.SocietyIssue = StaffSocialInsurance.SocietyIssue;
                    model.SocietyDefaultValue = StaffSocialInsurance.SocietyDefaultValue.ToString();
                    model.PC_CV = StaffSocialInsurance.PC_CV.ToString();
                    model.PC_TNVK = StaffSocialInsurance.PC_TNVK.ToString();
                    model.PC_TNN = StaffSocialInsurance.PC_TNN.ToString();
                    model.PC_Khac = StaffSocialInsurance.PC_Khac.ToString();
                    model.TienLuong = StaffSocialInsurance.TienLuong;
                    model.Note = StaffSocialInsurance.Note;
                    model.Status = StaffSocialInsurance.Status;
                    model.StaffId = StaffSocialInsurance.StaffId;
                }
                else
                {
                    AutoMapper.Mapper.Map(DotBCBHXHDetail, model);
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
        public ActionResult Edit(DotBCBHXHDetailViewModel model, bool IsPopup)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var province = locationRepository.GetProvinceList();
                    var DotBCBHXHDetail = DotBCBHXHDetailRepository.GetDotBCBHXHDetailById(model.Id);

                    DotBCBHXHDetail.ModifiedUserId = WebSecurity.CurrentUserId;
                    DotBCBHXHDetail.ModifiedDate = DateTime.Now;

                    DotBCBHXHDetail.MedicalIssue = model.ProvinceMedical != null ? province.SingleOrDefault(n => n.Id == model.ProvinceMedical.Value.ToString()).Name : "";
                    DotBCBHXHDetail.SocietyIssue = model.ProvinceSociety != null ? province.SingleOrDefault(n => n.Id == model.ProvinceSociety.Value.ToString()).Name : "";
                    DotBCBHXHDetail.MedicalCode = model.MedicalCode;
                    DotBCBHXHDetail.MedicalStartDate = model.MedicalStartDate;
                    DotBCBHXHDetail.MedicalEndDate = model.MedicalEndDate;
                    DotBCBHXHDetail.MedicalDefaultValue = Decimal.Parse(model.MedicalDefaultValue == null ? "0" : model.MedicalDefaultValue.Replace(",", "."), new CultureInfo("en-GB"));
                    DotBCBHXHDetail.SocietyCode = model.SocietyCode;
                    DotBCBHXHDetail.SocietyStartDate = model.SocietyStartDate;
                    DotBCBHXHDetail.SocietyEndDate = model.SocietyEndDate;
                    DotBCBHXHDetail.SocietyDefaultValue = Decimal.Parse(model.SocietyDefaultValue == null ? "0" : model.SocietyDefaultValue.Replace(",", "."), new CultureInfo("en-GB"));
                    DotBCBHXHDetail.PC_CV = string.IsNullOrEmpty(model.PC_CV) ? 0 : Decimal.Parse(model.PC_CV.Replace(",", "."), new CultureInfo("en-GB"));
                    DotBCBHXHDetail.PC_TNN = string.IsNullOrEmpty(model.PC_TNN) ? 0 : Decimal.Parse(model.PC_TNN.Replace(",", "."), new CultureInfo("en-GB"));
                    DotBCBHXHDetail.PC_TNVK = string.IsNullOrEmpty(model.PC_TNVK) ? 0 : Decimal.Parse(model.PC_TNVK.Replace(",", "."), new CultureInfo("en-GB"));
                    DotBCBHXHDetail.PC_Khac = string.IsNullOrEmpty(model.PC_Khac) ? 0 : Decimal.Parse(model.PC_Khac.Replace(",", "."), new CultureInfo("en-GB"));
                    DotBCBHXHDetail.TienLuong = model.TienLuong;
                    DotBCBHXHDetail.Note = model.Note;
                    DotBCBHXHDetail.Status = model.Status;
                    DotBCBHXHDetailRepository.UpdateDotBCBHXHDetail(DotBCBHXHDetail);

                    if (model.SocialInsuranceId != null) // nếu có hồ sơ BHXH thì cập nhật lại
                    {
                        var StaffSocialInsurance = StaffSocialInsuranceRepository.GetStaffSocialInsuranceByStaffId(DotBCBHXHDetail.StaffId.Value);

                        StaffSocialInsurance.MedicalCode = model.MedicalCode;
                        StaffSocialInsurance.MedicalStartDate = model.MedicalStartDate;
                        StaffSocialInsurance.MedicalEndDate = model.MedicalEndDate;
                        StaffSocialInsurance.MedicalIssue = model.ProvinceMedical != null ? province.SingleOrDefault(n => n.Id == model.ProvinceMedical.Value.ToString()).Name : "";
                        StaffSocialInsurance.MedicalDefaultValue = Decimal.Parse(model.MedicalDefaultValue == null ? "0" : model.MedicalDefaultValue.Replace(",", "."), new CultureInfo("en-GB"));
                        StaffSocialInsurance.SocietyCode = model.SocietyCode;
                        StaffSocialInsurance.SocietyStartDate = model.SocietyStartDate;
                        StaffSocialInsurance.SocietyEndDate = model.SocietyEndDate;
                        StaffSocialInsurance.SocietyIssue = model.ProvinceSociety != null ? province.SingleOrDefault(n => n.Id == model.ProvinceSociety.Value.ToString()).Name : "";
                        StaffSocialInsurance.SocietyDefaultValue = Decimal.Parse(model.SocietyDefaultValue == null ? "0" : model.SocietyDefaultValue.Replace(",", "."), new CultureInfo("en-GB"));
                        StaffSocialInsurance.PC_CV = string.IsNullOrEmpty(model.PC_CV) ? 0 : Decimal.Parse(model.PC_CV.Replace(",", "."), new CultureInfo("en-GB"));
                        StaffSocialInsurance.PC_TNN = string.IsNullOrEmpty(model.PC_TNN) ? 0 : Decimal.Parse(model.PC_TNN.Replace(",", "."), new CultureInfo("en-GB"));
                        StaffSocialInsurance.PC_TNVK = string.IsNullOrEmpty(model.PC_TNVK) ? 0 : Decimal.Parse(model.PC_TNVK.Replace(",", "."), new CultureInfo("en-GB"));
                        StaffSocialInsurance.PC_Khac = string.IsNullOrEmpty(model.PC_Khac) ? 0 : Decimal.Parse(model.PC_Khac.Replace(",", "."), new CultureInfo("en-GB"));
                        StaffSocialInsurance.TienLuong = model.TienLuong;
                        StaffSocialInsurance.Note = model.Note;
                        StaffSocialInsuranceRepository.UpdateStaffSocialInsurance(StaffSocialInsurance);

                        DotBCBHXHDetail.SocialInsuranceId = StaffSocialInsurance.Id;
                        DotBCBHXHDetailRepository.UpdateDotBCBHXHDetail(DotBCBHXHDetail);
                    }
                    else // ngược lại nếu không có thì thêm mới hồ sơ BHXH
                    {
                        var StaffSocialInsurance = new StaffSocialInsurance();

                        StaffSocialInsurance.IsDeleted = false;
                        StaffSocialInsurance.CreatedUserId = WebSecurity.CurrentUserId;
                        StaffSocialInsurance.ModifiedUserId = WebSecurity.CurrentUserId;
                        StaffSocialInsurance.AssignedUserId = WebSecurity.CurrentUserId;
                        StaffSocialInsurance.CreatedDate = DateTime.Now;
                        StaffSocialInsurance.ModifiedDate = DateTime.Now;


                        if (!string.IsNullOrEmpty(model.MedicalCode))
                        {
                            StaffSocialInsurance.MedicalCode = model.MedicalCode.Trim();
                            StaffSocialInsurance.MedicalDefaultValue = Decimal.Parse(model.MedicalDefaultValue == null ? "0" : model.MedicalDefaultValue.Replace(",", "."), new CultureInfo("en-GB"));
                            StaffSocialInsurance.MedicalStartDate = model.MedicalStartDate;
                            StaffSocialInsurance.MedicalEndDate = model.MedicalEndDate;
                            StaffSocialInsurance.MedicalIssue = model.ProvinceMedical != null ? province.SingleOrDefault(n => n.Id == model.ProvinceMedical.Value.ToString()).Name : "";
                        }
                        else
                        {
                            StaffSocialInsurance.MedicalDefaultValue = 0;
                        }


                        if (!string.IsNullOrEmpty(model.SocietyCode))
                        {
                            StaffSocialInsurance.SocietyCode = model.SocietyCode.Trim();
                            StaffSocialInsurance.SocietyDefaultValue = Decimal.Parse(model.SocietyDefaultValue == null ? "0" : model.SocietyDefaultValue.Replace(",", "."), new CultureInfo("en-GB"));
                            StaffSocialInsurance.SocietyIssue = model.ProvinceSociety != null ? province.SingleOrDefault(n => n.Id == model.ProvinceSociety.Value.ToString()).Name : "";
                            StaffSocialInsurance.SocietyStartDate = model.SocietyStartDate;
                            StaffSocialInsurance.SocietyEndDate = model.SocietyEndDate;
                        }
                        else
                        {
                            StaffSocialInsurance.MedicalDefaultValue = 0;
                        }

                        //Thông tin lương
                        StaffSocialInsurance.TienLuong = model.TienLuong;
                        StaffSocialInsurance.PC_CV = string.IsNullOrEmpty(model.PC_CV) ? 0 : Decimal.Parse(model.PC_CV.Replace(",", "."), new CultureInfo("en-GB"));
                        StaffSocialInsurance.PC_TNN = string.IsNullOrEmpty(model.PC_TNN) ? 0 : Decimal.Parse(model.PC_TNN.Replace(",", "."), new CultureInfo("en-GB"));
                        StaffSocialInsurance.PC_TNVK = string.IsNullOrEmpty(model.PC_TNVK) ? 0 : Decimal.Parse(model.PC_TNVK.Replace(",", "."), new CultureInfo("en-GB"));
                        StaffSocialInsurance.PC_Khac = string.IsNullOrEmpty(model.PC_Khac) ? 0 : Decimal.Parse(model.PC_Khac.Replace(",", "."), new CultureInfo("en-GB"));

                        StaffSocialInsurance.StaffId = model.StaffId;
                        StaffSocialInsurance.Note = model.Note;
                        StaffSocialInsurance.Status = Erp.BackOffice.Staff.Controllers.StaffSocialInsuranceController.StatusSocialInsurance.DangHoatDong.GetName();

                        StaffSocialInsuranceRepository.InsertStaffSocialInsurance(StaffSocialInsurance);

                        DotBCBHXHDetail.SocialInsuranceId = StaffSocialInsurance.Id;
                        DotBCBHXHDetailRepository.UpdateDotBCBHXHDetail(DotBCBHXHDetail);
                    }

                   
                    if (IsPopup)
                    {
                        return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
                    }
                    else
                    {
                        TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                        return RedirectToAction("Index");
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
            var province = locationRepository.GetProvinceList();
            var DotBCBHXHDetail = DotBCBHXHDetailRepository.GetDotBCBHXHDetailById(Id.Value);
            if (DotBCBHXHDetail != null && DotBCBHXHDetail.IsDeleted != true)
            {
                var model = new DotBCBHXHDetailViewModel();

                var ProvinceSociety = province.FirstOrDefault(x => x.Name.Contains(DotBCBHXHDetail.SocietyIssue == null ? "" : DotBCBHXHDetail.SocietyIssue));
                var ProvinceMedical = province.FirstOrDefault(x => x.Name.Contains(DotBCBHXHDetail.MedicalIssue == null ? "" : DotBCBHXHDetail.MedicalIssue));
                    model.ProvinceSociety = Convert.ToInt32(ProvinceSociety == null ? "0" : ProvinceSociety.Id);
                    model.ProvinceMedical = Convert.ToInt32(ProvinceMedical == null ? "0" : ProvinceMedical.Id);
                    model.SocialInsuranceId = DotBCBHXHDetail.Id;
                    model.MedicalCode = DotBCBHXHDetail.MedicalCode;
                    model.MedicalStartDate = DotBCBHXHDetail.MedicalStartDate;
                    model.MedicalEndDate = DotBCBHXHDetail.MedicalEndDate;
                    model.MedicalIssue = DotBCBHXHDetail.MedicalIssue;
                    model.MedicalDefaultValue = DotBCBHXHDetail.MedicalDefaultValue.ToString();
                    model.SocietyCode = DotBCBHXHDetail.SocietyCode;
                    model.SocietyStartDate = DotBCBHXHDetail.SocietyStartDate;
                    model.SocietyEndDate = DotBCBHXHDetail.SocietyEndDate;
                    model.SocietyIssue = DotBCBHXHDetail.SocietyIssue;
                    model.SocietyDefaultValue = DotBCBHXHDetail.SocietyDefaultValue.ToString();
                    model.PC_CV = DotBCBHXHDetail.PC_CV.ToString();
                    model.PC_TNVK = DotBCBHXHDetail.PC_TNVK.ToString();
                    model.PC_TNN = DotBCBHXHDetail.PC_TNN.ToString();
                    model.PC_Khac = DotBCBHXHDetail.PC_Khac.ToString();
                    model.TienLuong = DotBCBHXHDetail.TienLuong;
                    model.Note = DotBCBHXHDetail.Note;
                    model.Status = DotBCBHXHDetail.Status;
                    model.StaffId = DotBCBHXHDetail.StaffId;
               
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
        public ActionResult Delete(int Id)
        {
            try
            {
                var item = DotBCBHXHDetailRepository.GetDotBCBHXHDetailById(Id);
                if (item != null)
                {
                    item.IsDeleted = true;
                    DotBCBHXHDetailRepository.UpdateDotBCBHXHDetail(item);
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

        #region ReportList
        public ActionResult ReportList(int? BatchNumber, int? Month, int? Year)
        {
            if (BatchNumber == null)
            {
                BatchNumber = 0;
            }
            if (Month == null)
            {
                Month = DateTime.Now.Month;
            }
            if (Year == null)
            {
                Year = DateTime.Now.Year;
            }
            var DotBCBHXHDetail = SqlHelper.QuerySP<DotBCBHXHReport>("spStaff_DotBCBHXH", new
            {
                BatchNumber = BatchNumber,
                Month = Month,
                Year = Year,
            }).ToList();
            return View(DotBCBHXHDetail);
        }
        #endregion
    }
}
