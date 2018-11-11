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
using System.ComponentModel.DataAnnotations;

namespace Erp.BackOffice.Staff.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class StaffSocialInsuranceController : Controller
    {
        private readonly IStaffSocialInsuranceRepository StaffSocialInsuranceRepository;
        private readonly IUserRepository userRepository;
        private readonly ILocationRepository locationRepository;
        private readonly IStaffsRepository StaffsRepository;
        private readonly IDotBCBHXHDetailRepository dotBCBHXHDetailRepository;
        private readonly IDotBCBHXHRepository dotBCBHXHRepository;
        public StaffSocialInsuranceController(
            IStaffSocialInsuranceRepository _StaffSocialInsurance
            , IUserRepository _user
            , ILocationRepository _location
            , IStaffsRepository _staff
            , IDotBCBHXHDetailRepository _dot
            , IDotBCBHXHRepository _dot_normal
            )
        {
            StaffSocialInsuranceRepository = _StaffSocialInsurance;
            userRepository = _user;
            locationRepository = _location;
            StaffsRepository = _staff;
            dotBCBHXHDetailRepository = _dot;
            dotBCBHXHRepository = _dot_normal;
        }

        public enum StatusSocialInsurance
        {
            [Display(Name = "Hết hạn sử dụng")]
            HetHangSuDung,
            [Display(Name = "Đang hoạt động")]
            DangHoatDong
        }
       

        #region Index

        public ViewResult Index(string Code, string Name, string bhyt, string bhxh)
        {
            //bool _bhxh = string.IsNullOrEmpty(bhxh) ? false : true;
            //bool _bhyt = string.IsNullOrEmpty(bhyt) ? false : true;
            IEnumerable<StaffSocialInsuranceViewModel> q = StaffSocialInsuranceRepository.GetAllViewStaffSocialInsurance()
                .Select(item => new StaffSocialInsuranceViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    ModifiedDate = item.ModifiedDate,
                    AssignedUserId = item.AssignedUserId,
                    Birthday = item.Birthday,
                    BranchName = item.BranchName,
                    CodeName = item.CodeName,
                    Email = item.Email,
                    Ethnic = item.Ethnic,
                    IdCardDate = item.IdCardDate,
                    IdCardIssued = item.IdCardIssued,
                    IdCardNumber = item.IdCardNumber,
                    IsDeleted = item.IsDeleted,
                    Literacy = item.Literacy,
                    MedicalCode = item.MedicalCode,
                    MedicalDefaultValue = item.MedicalDefaultValue.ToString(),
                    MedicalEndDate = item.MedicalEndDate,
                    MedicalIssue = item.MedicalIssue,
                    MedicalStartDate = item.MedicalStartDate,
                    PositionName = item.PositionName,
                    ProvinceName = item.ProvinceName,
                    Note = item.Note,
                    SocietyCode = item.SocietyCode,
                    SocietyDefaultValue = item.SocietyDefaultValue.ToString(),
                    SocietyEndDate = item.SocietyEndDate,
                    SocietyIssue = item.SocietyIssue,
                    SocietyStartDate = item.SocietyStartDate,
                    StaffId = item.StaffId,
                    StaffName = item.StaffName,
                    StaffCode = item.StaffCode,
                    Status = item.Status

                }).OrderByDescending(m => m.ModifiedDate).ToList();

            //Cập nhật trạng thái khi bảo hiểm y tế hoặc bh xã hội hết hạn
            //TH1: Hết hạn BHXH 
            //TH2: Hết hạn BHYT và bảo hiểm XH luôn
            DateTime datenow = DateTime.Now;
            var hhsd = q.Where(n => n.MedicalEndDate < datenow && n.SocietyEndDate < datenow).ToList();
            if (hhsd != null && hhsd.Count > 0)
            {
                for (int i = 0; i < hhsd.Count; i++)
                {
                    if (string.IsNullOrEmpty(hhsd[i].Status) || hhsd[i].Status == StatusSocialInsurance.DangHoatDong.GetName())
                    {
                        //Update status
                        StaffSocialInsurance _staffSocial = new StaffSocialInsurance();
                        _staffSocial = StaffSocialInsuranceRepository.GetStaffSocialInsuranceById(hhsd[i].Id);
                        _staffSocial.Status = StatusSocialInsurance.HetHangSuDung.GetName();
                        //Update
                        StaffSocialInsuranceRepository.UpdateStaffSocialInsurance(_staffSocial);
                    }
                }
            }
            var consd = q.Where(n => n.MedicalEndDate >= datenow || n.SocietyEndDate >= datenow).ToList();
            if (consd != null && consd.Count > 0)
            {
                for (int i = 0; i < consd.Count; i++)
                {
                    if (string.IsNullOrEmpty(consd[i].Status) || consd[i].Status == StatusSocialInsurance.HetHangSuDung.GetName())
                    {
                        //Update status
                        StaffSocialInsurance _staffSocial = new StaffSocialInsurance();
                        _staffSocial = StaffSocialInsuranceRepository.GetStaffSocialInsuranceById(consd[i].Id);
                        _staffSocial.Status = StatusSocialInsurance.DangHoatDong.GetName();
                        //Update
                        StaffSocialInsuranceRepository.UpdateStaffSocialInsurance(_staffSocial);
                    }
                }
            }


            if (!string.IsNullOrEmpty(Code))
            {
                q = q.Where(n => n.StaffCode.Contains(Code));
            }
            if (!string.IsNullOrEmpty(Name))
            {
                q = q.Where(n => n.StaffCode.Contains(Name));
            }
            if (!string.IsNullOrEmpty(bhxh))
            {
                q = q.Where(n => !string.IsNullOrEmpty(n.SocietyCode));
            }
            if (!string.IsNullOrEmpty(bhyt))
            {
                q = q.Where(n => !string.IsNullOrEmpty(n.MedicalCode));
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

            var model = new StaffSocialInsuranceViewModel();
            model.MedicalDefaultValue = "1.5";
            model.MedicalCode = "000";
            model.SocietyDefaultValue = "8";
            model.SocietyCode = "000";
            model.TienLuong = "0";
            model.PC_CV = "0";
            model.PC_TNN = "0";
            model.PC_TNVK = "0";
            model.PC_Khac = "0";

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(StaffSocialInsuranceViewModel model)
        {
            bool checkcode_medical = StaffSocialInsuranceRepository.GetAllStaffSocialInsurance().Any(n => !string.IsNullOrEmpty(n.MedicalCode) && n.MedicalCode.Equals(model.MedicalCode.Trim()));
            bool checkcode_social = StaffSocialInsuranceRepository.GetAllStaffSocialInsurance().Any(n => !string.IsNullOrEmpty(n.SocietyCode) && n.SocietyCode.Equals(model.SocietyCode.Trim()));
            string fail = "";
            if (checkcode_medical)
            {
                fail = "Mã bảo hiểm y tế đã được cấp";
            }

            if (checkcode_social)
            {
                if (!string.IsNullOrEmpty(fail))
                    fail = fail + " và Mã bảo hiểm xã hội đã được cấp";
                else
                    fail = "Mã bảo hiểm xã hội đã được cấp";
            }

            if (!string.IsNullOrEmpty(fail))
            {
                ViewBag.FailedMessage = fail;
                return View(model);
            }

            if (ModelState.IsValid)
            {
                var province = locationRepository.GetProvinceList();
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
                    StaffSocialInsurance.MedicalDefaultValue = Decimal.Parse(model.MedicalDefaultValue.Replace(",", "."), new CultureInfo("en-GB"));
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
                    StaffSocialInsurance.SocietyDefaultValue = Decimal.Parse(model.SocietyDefaultValue.Replace(",", "."), new CultureInfo("en-GB"));
                    StaffSocialInsurance.SocietyIssue = model.ProvinceSociety != null ? province.SingleOrDefault(n => n.Id == model.ProvinceSociety.Value.ToString()).Name : "";
                    StaffSocialInsurance.SocietyStartDate = model.SocietyStartDate;
                    StaffSocialInsurance.SocietyEndDate = model.SocietyEndDate;
                }
                else
                {
                    StaffSocialInsurance.MedicalDefaultValue = 0;
                }

                //Thông tin lương
                StaffSocialInsurance.TienLuong = string.IsNullOrEmpty(model.TienLuong) ? 0 : Decimal.Parse(model.TienLuong.Replace(",", "."), new CultureInfo("en-GB"));
                StaffSocialInsurance.PC_CV = string.IsNullOrEmpty(model.PC_CV) ? 0 : Decimal.Parse(model.PC_CV.Replace(",", "."), new CultureInfo("en-GB"));
                StaffSocialInsurance.PC_TNN = string.IsNullOrEmpty(model.PC_TNN) ? 0 : Decimal.Parse(model.PC_TNN.Replace(",", "."), new CultureInfo("en-GB"));
                StaffSocialInsurance.PC_TNVK = string.IsNullOrEmpty(model.PC_TNVK) ? 0 : Decimal.Parse(model.PC_TNVK.Replace(",", "."), new CultureInfo("en-GB"));
                StaffSocialInsurance.PC_Khac = string.IsNullOrEmpty(model.PC_Khac) ? 0 : Decimal.Parse(model.PC_Khac.Replace(",", "."), new CultureInfo("en-GB"));

                StaffSocialInsurance.StaffId = model.StaffId;
                StaffSocialInsurance.Note = model.Note;
                StaffSocialInsurance.Status = StatusSocialInsurance.DangHoatDong.GetName();

                StaffSocialInsuranceRepository.InsertStaffSocialInsurance(StaffSocialInsurance);

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
            var StaffSocialInsurance = StaffSocialInsuranceRepository.GetvwStaffSocialInsuranceById(Id.Value);
            if (StaffSocialInsurance != null && StaffSocialInsurance.IsDeleted != true)
            {
                var model = new StaffSocialInsuranceViewModel();
                AutoMapper.Mapper.Map(StaffSocialInsurance, model);
                var ProvinceSociety = province.FirstOrDefault(x => x.Name.Contains(StaffSocialInsurance.SocietyIssue == null ? "" : StaffSocialInsurance.SocietyIssue));
                var ProvinceMedical = province.FirstOrDefault(x => x.Name.Contains(StaffSocialInsurance.MedicalIssue == null ? "" : StaffSocialInsurance.MedicalIssue));
                model.ProvinceSociety = Convert.ToInt32(ProvinceSociety == null ? "0" : ProvinceSociety.Id);
                model.ProvinceMedical = Convert.ToInt32(ProvinceMedical == null ? "0" : ProvinceMedical.Id);
                if (model.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                {
                    TempData["FailedMessage"] = "NotOwner";
                    return RedirectToAction("Index");
                }
                if (string.IsNullOrEmpty(model.SocietyDefaultValue))
                    model.SocietyDefaultValue = "0";
                if (string.IsNullOrEmpty(model.MedicalDefaultValue))
                    model.MedicalDefaultValue = "0";

                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(StaffSocialInsuranceViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var province = locationRepository.GetProvinceList();
                    var StaffSocialInsurance = StaffSocialInsuranceRepository.GetStaffSocialInsuranceById(model.Id);
                    StaffSocialInsurance.ModifiedUserId = WebSecurity.CurrentUserId;
                    StaffSocialInsurance.ModifiedDate = DateTime.Now;

                    StaffSocialInsurance.MedicalCode = model.MedicalCode;
                    StaffSocialInsurance.MedicalDefaultValue = Decimal.Parse(model.MedicalDefaultValue.Replace(",", "."), new CultureInfo("en-GB"));
                    StaffSocialInsurance.MedicalStartDate = model.MedicalStartDate;
                    StaffSocialInsurance.MedicalEndDate = model.MedicalEndDate;
                    StaffSocialInsurance.MedicalIssue = model.ProvinceMedical != null ? province.SingleOrDefault(n => n.Id == model.ProvinceMedical.Value.ToString()).Name : "";
                    StaffSocialInsurance.SocietyCode = model.SocietyCode;
                    StaffSocialInsurance.SocietyDefaultValue = Decimal.Parse(model.SocietyDefaultValue.Replace(",", "."), new CultureInfo("en-GB"));
                    StaffSocialInsurance.SocietyIssue = model.ProvinceSociety != null ? province.SingleOrDefault(n => n.Id == model.ProvinceSociety.Value.ToString()).Name : "";
                    StaffSocialInsurance.SocietyStartDate = model.SocietyStartDate;
                    StaffSocialInsurance.SocietyEndDate = model.SocietyEndDate;
                    StaffSocialInsurance.StaffId = model.StaffId;
                    StaffSocialInsurance.Note = model.Note;

                    //Thông tin lương
                    StaffSocialInsurance.TienLuong = string.IsNullOrEmpty(model.TienLuong) ? 0 : Decimal.Parse(model.TienLuong.Replace(",", "."), new CultureInfo("en-GB"));
                    StaffSocialInsurance.PC_CV = string.IsNullOrEmpty(model.PC_CV) ? 0 : Decimal.Parse(model.PC_CV.Replace(",", "."), new CultureInfo("en-GB"));
                    StaffSocialInsurance.PC_TNN = string.IsNullOrEmpty(model.PC_TNN) ? 0 : Decimal.Parse(model.PC_TNN.Replace(",", "."), new CultureInfo("en-GB"));
                    StaffSocialInsurance.PC_TNVK = string.IsNullOrEmpty(model.PC_TNVK) ? 0 : Decimal.Parse(model.PC_TNVK.Replace(",", "."), new CultureInfo("en-GB"));
                    StaffSocialInsurance.PC_Khac = string.IsNullOrEmpty(model.PC_Khac) ? 0 : Decimal.Parse(model.PC_Khac.Replace(",", "."), new CultureInfo("en-GB"));

                    StaffSocialInsuranceRepository.UpdateStaffSocialInsurance(StaffSocialInsurance);

                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
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

        #region Detail
        public ActionResult Detail(int? Id)
        {
            var StaffSocialInsurance = StaffSocialInsuranceRepository.GetAllViewStaffSocialInsuranceById(Id.Value);
            if (StaffSocialInsurance != null && StaffSocialInsurance.IsDeleted != true)
            {
                var path = Helpers.Common.GetSetting("Staff");
                var filepath = System.Web.HttpContext.Current.Server.MapPath("~" + Helpers.Common.GetSetting("Staff"));
                var model = new StaffSocialInsuranceViewModel();
                AutoMapper.Mapper.Map(StaffSocialInsurance, model);

                ////Staffs
                //var student = StaffsRepository.GetvwStaffsById(StaffSocialInsurance.StaffId.Value);
                //var staff = new StaffsViewModel();
                //AutoMapper.Mapper.Map(student, model);

                if (model.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                {
                    TempData["FailedMessage"] = "NotOwner";
                    return RedirectToAction("Index");
                }
                //lấy hình
                if (!string.IsNullOrEmpty(model.ProfileImage))
                {
                    model.ProfileImagePath = path + model.ProfileImage;
                    if (!System.IO.File.Exists(filepath + model.ProfileImage))
                    {
                        model.ProfileImagePath = "/assets/img/no-avatar.png";
                    }
                    else
                    {
                        model.ProfileImagePath = path + model.ProfileImage;
                    }
                }
                else
                    if (string.IsNullOrEmpty(model.ProfileImage))//Đã có hình
                    {
                        model.ProfileImagePath = "/assets/img/no-avatar.png";
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
                    var item = StaffSocialInsuranceRepository.GetStaffSocialInsuranceById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        {
                            TempData["FailedMessage"] = "NotOwner";
                            return RedirectToAction("Index");
                        }

                        item.IsDeleted = true;
                        StaffSocialInsuranceRepository.UpdateStaffSocialInsurance(item);
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

        #region Báo cáo
        public ActionResult ListOfParticipatingAndProposedInsurance(int? DotBCBHXHId)
        {
            var model = dotBCBHXHDetailRepository.GetAllViewDotBCBHXHDetail().ToList();

            var DotBHXH = new DotBCBHXH();
            //Lấy danh sách nhân viên theo đợt

            //- Danh sách đã tham gia bảo hiểm

            //- chưa tham gia bảo hiểm
            if (DotBCBHXHId != null && DotBCBHXHId!=-1)
            {
                model = model.Where(n => n.DotBCBHXHId == DotBCBHXHId).ToList();
                DotBHXH = dotBCBHXHRepository.GetDotBCBHXHById(DotBCBHXHId.Value);
            }
            ViewBag.DotBHXH = DotBHXH;

            return View(model);
        }
        #endregion

        #region List

        public ViewResult List(int? StaffId)
        {
            IEnumerable<StaffSocialInsuranceViewModel> q = StaffSocialInsuranceRepository.GetAllViewStaffSocialInsurance()
                .Select(item => new StaffSocialInsuranceViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    ModifiedDate = item.ModifiedDate,
                    AssignedUserId = item.AssignedUserId,
                    Birthday = item.Birthday,
                    BranchName = item.BranchName,
                    CodeName = item.CodeName,
                    Email = item.Email,
                    Ethnic = item.Ethnic,
                    IdCardDate = item.IdCardDate,
                    IdCardIssued = item.IdCardIssued,
                    IdCardNumber = item.IdCardNumber,
                    IsDeleted = item.IsDeleted,
                    Literacy = item.Literacy,
                    MedicalCode = item.MedicalCode,
                    MedicalDefaultValue = item.MedicalDefaultValue.ToString(),
                    MedicalEndDate = item.MedicalEndDate,
                    MedicalIssue = item.MedicalIssue,
                    MedicalStartDate = item.MedicalStartDate,
                    PositionName = item.PositionName,
                    ProvinceName = item.ProvinceName,
                    Note = item.Note,
                    SocietyCode = item.SocietyCode,
                    SocietyDefaultValue = item.SocietyDefaultValue.ToString(),
                    SocietyEndDate = item.SocietyEndDate,
                    SocietyIssue = item.SocietyIssue,
                    SocietyStartDate = item.SocietyStartDate,
                    StaffId = item.StaffId,
                    StaffName = item.StaffName,
                    StaffCode = item.StaffCode,
                    Status = item.Status

                }).OrderByDescending(m => m.ModifiedDate).ToList();

            //Cập nhật trạng thái khi bảo hiểm y tế hoặc bh xã hội hết hạn
            //TH1: Hết hạn BHXH 
            //TH2: Hết hạn BHYT và bảo hiểm XH luôn
            DateTime datenow = DateTime.Now;
            var hhsd = q.Where(n => n.MedicalEndDate < datenow && n.SocietyEndDate < datenow).ToList();
            if (hhsd != null && hhsd.Count > 0)
            {
                for (int i = 0; i < hhsd.Count; i++)
                {
                    if (string.IsNullOrEmpty(hhsd[i].Status) || hhsd[i].Status == StatusSocialInsurance.DangHoatDong.GetName())
                    {
                        //Update status
                        StaffSocialInsurance _staffSocial = new StaffSocialInsurance();
                        _staffSocial = StaffSocialInsuranceRepository.GetStaffSocialInsuranceById(hhsd[i].Id);
                        _staffSocial.Status = StatusSocialInsurance.HetHangSuDung.GetName();
                        //Update
                        StaffSocialInsuranceRepository.UpdateStaffSocialInsurance(_staffSocial);
                    }
                }
            }
            var consd = q.Where(n => n.MedicalEndDate >= datenow || n.SocietyEndDate >= datenow).ToList();
            if (consd != null && consd.Count > 0)
            {
                for (int i = 0; i < consd.Count; i++)
                {
                    if (string.IsNullOrEmpty(consd[i].Status) || consd[i].Status == StatusSocialInsurance.HetHangSuDung.GetName())
                    {
                        //Update status
                        StaffSocialInsurance _staffSocial = new StaffSocialInsurance();
                        _staffSocial = StaffSocialInsuranceRepository.GetStaffSocialInsuranceById(consd[i].Id);
                        _staffSocial.Status = StatusSocialInsurance.DangHoatDong.GetName();
                        //Update
                        StaffSocialInsuranceRepository.UpdateStaffSocialInsurance(_staffSocial);
                    }
                }
            }


            if (StaffId != null && StaffId.Value > 0)
            {
                q = q.Where(n => n.StaffId == StaffId);
            }
            else
            {
                q = null;
                ViewBag.FailedMessage = "Không tìm thấy dữ liệu!";
            }
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        #endregion
    }
}
