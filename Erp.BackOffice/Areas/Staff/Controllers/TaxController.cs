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
    public class TaxController : Controller
    {
        private readonly ITaxRepository TaxRepository;
        private readonly IUserRepository userRepository;
        private readonly ITaxIncomePersonDetailRepository taxIncomePersonDetailRepository;
        private readonly ITaxIncomePersonRepository taxIncomePersonRepository;
        private readonly IStaffsRepository staffsRepository;
        private readonly ISalaryTableDetailReportRepository salaryTableDetailReportRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IThuNhapChiuThueRepository thuNhapChiuThueRepository;
        private readonly IGiamTruThueTNCNRepository giamTruThueTNCNRepository;
        private readonly ITaxRateRepository taxRateRepository;
        private readonly IStaffFamilyRepository staffFamilyRepository;

        public TaxController(
            ITaxRepository _Tax
            , IUserRepository _user
            , ITaxIncomePersonDetailRepository _taxpd
            , ITaxIncomePersonRepository _taxp
            , IStaffsRepository _staff
            , ISalaryTableDetailReportRepository _salaryReport
            , ICategoryRepository _cate
            , IThuNhapChiuThueRepository _thunhap
            , IGiamTruThueTNCNRepository _giamtru
            , ITaxRateRepository _taxrate
            , IStaffFamilyRepository _staff_f
            )
        {
            TaxRepository = _Tax;
            userRepository = _user;
            taxIncomePersonDetailRepository = _taxpd;
            taxIncomePersonRepository = _taxp;
            staffsRepository = _staff;
            salaryTableDetailReportRepository = _salaryReport;
            categoryRepository = _cate;
            thuNhapChiuThueRepository = _thunhap;
            giamTruThueTNCNRepository = _giamtru;
            taxRateRepository = _taxrate;
            staffFamilyRepository = _staff_f;

        }

        #region Index

        public ViewResult Index(string txtSearch)
        {
            IQueryable<TaxViewModel> q = TaxRepository.GetvwAllTax()
                .Select(item => new TaxViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    TaxIncomePersonId = item.TaxIncomePersonId,
                    Month = item.Month,
                    Year = item.Year,
                    SalaryTableId = item.SalaryTableId,
                    SalaryTableName = item.SalaryTableName,
                    TaxIncomePersonName = item.TaxIncomePersonName

                }).OrderByDescending(m => m.Year).ThenBy(n => n.Month);

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        #endregion

        #region Create
        public ViewResult Create()
        {
            var model = new TaxViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(TaxViewModel model)
        {
            if (ModelState.IsValid)
            {
                var Tax = new Tax();
                AutoMapper.Mapper.Map(model, Tax);
                Tax.IsDeleted = false;
                Tax.CreatedUserId = WebSecurity.CurrentUserId;
                Tax.ModifiedUserId = WebSecurity.CurrentUserId;
                Tax.AssignedUserId = WebSecurity.CurrentUserId;
                Tax.CreatedDate = DateTime.Now;
                Tax.ModifiedDate = DateTime.Now;
                Tax.Status = App_GlobalResources.Wording.Processing;

                TaxRepository.InsertTax(Tax);

                //Hiện tại lấy hết nhân viên theo mã thuế
                var staff_in_tax = taxIncomePersonDetailRepository.GetAllTaxIncomePersonDetail().Where(n => n.TaxIncomePersonId == model.TaxIncomePersonId).ToList();

                //Lấy dữ liệu từ bảng lương
                var salaryReport = salaryTableDetailReportRepository.GetSalaryTableDetailReportBySalaryTableId(model.SalaryTableId.Value).ToList();
                //Cate Thu nhập chịu thếu và Giảm trừ
                var cate = categoryRepository.GetCategoryByCode("TaxCode").ToList();
                //Lấy dữ liệu giảm trừ từ người thân
                var staff_family = staffFamilyRepository.GetAllStaffFamily().Where(n => n.IsDependencies == true).ToList();

                for (int a = 0; a < staff_in_tax.Count(); a++)
                {
                    var tnct = cate.Where(n => n.Description == "TNCT").ToList();
                    for (int i = 0; i < tnct.Count; i++)
                    {
                        var value = salaryReport.SingleOrDefault(n => n.StaffId == staff_in_tax[a].StaffId && n.ColumName.Equals(tnct[i].Value));
                        ThuNhapChiuThue thunhapchiuthue = new ThuNhapChiuThue();
                        thunhapchiuthue.IsDeleted = false;
                        thunhapchiuthue.CreatedUserId = WebSecurity.CurrentUserId;
                        thunhapchiuthue.ModifiedUserId = WebSecurity.CurrentUserId;
                        thunhapchiuthue.AssignedUserId = WebSecurity.CurrentUserId;
                        thunhapchiuthue.CreatedDate = DateTime.Now;
                        thunhapchiuthue.ModifiedDate = DateTime.Now;

                        //Name
                        thunhapchiuthue.TaxId = Tax.Id;
                        thunhapchiuthue.StaffId = staff_in_tax[a].StaffId;
                        thunhapchiuthue.TaxIncomePersonDetailId = staff_in_tax[a].Id;
                        thunhapchiuthue.Name = tnct[i].Name;
                        thunhapchiuthue.Value = value == null ? 0 : decimal.Parse(value.Value);
                        thuNhapChiuThueRepository.InsertThuNhapChiuThue(thunhapchiuthue);
                    }

                    //Thêm thu nhập giảm thuế
                    var gt = cate.Where(n => n.Description == "GT").ToList();
                    for (int i = 0; i < gt.Count; i++)
                    {
                        var value = salaryReport.SingleOrDefault(n => n.StaffId == staff_in_tax[a].StaffId && n.ColumName.Equals(gt[i].Value));
                        GiamTruThueTNCN giamtru = new GiamTruThueTNCN();
                        giamtru.IsDeleted = false;
                        giamtru.CreatedUserId = WebSecurity.CurrentUserId;
                        giamtru.ModifiedUserId = WebSecurity.CurrentUserId;
                        giamtru.AssignedUserId = WebSecurity.CurrentUserId;
                        giamtru.CreatedDate = DateTime.Now;
                        giamtru.ModifiedDate = DateTime.Now;

                        //Name
                        giamtru.TaxId = Tax.Id;
                        giamtru.StaffId = staff_in_tax[a].StaffId;
                        giamtru.TaxIncomePersonDetailId = staff_in_tax[a].Id;
                        giamtru.Name = gt[i].Name;
                        giamtru.Value = value == null ? 0 : decimal.Parse(value.Value);
                        giamTruThueTNCNRepository.InsertGiamTruThueTNCN(giamtru);
                    }

                    //Thêm giảm trừ từ gia đình có thành viên phụ thuộc
                    var gd = staff_family.Where(n => n.StaffId == staff_in_tax[a].StaffId).ToList();
                    for (int i = 0; i < gd.Count; i++)
                    {
                        GiamTruThueTNCN giamtru = new GiamTruThueTNCN();
                        giamtru.IsDeleted = false;
                        giamtru.CreatedUserId = WebSecurity.CurrentUserId;
                        giamtru.ModifiedUserId = WebSecurity.CurrentUserId;
                        giamtru.AssignedUserId = WebSecurity.CurrentUserId;
                        giamtru.CreatedDate = DateTime.Now;
                        giamtru.ModifiedDate = DateTime.Now;

                        //Name
                        giamtru.TaxId = Tax.Id;
                        giamtru.StaffId = staff_in_tax[a].StaffId;
                        giamtru.TaxIncomePersonDetailId = staff_in_tax[a].Id;
                        giamtru.Name = string.Format("Người phụ thuộc [{0}]", gd[i].Name);
                        giamtru.Value = 3600000;
                        giamTruThueTNCNRepository.InsertGiamTruThueTNCN(giamtru);
                    }

                }
                //Thêm thu nhập chịu thuế

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var Tax = TaxRepository.GetTaxById(Id.Value);
            if (Tax != null && Tax.IsDeleted != true)
            {
                var model = new TaxViewModel();
                AutoMapper.Mapper.Map(Tax, model);

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
        public ActionResult Edit(TaxViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var Tax = TaxRepository.GetTaxById(model.Id);
                    AutoMapper.Mapper.Map(model, Tax);
                    Tax.ModifiedUserId = WebSecurity.CurrentUserId;
                    Tax.ModifiedDate = DateTime.Now;
                    TaxRepository.UpdateTax(Tax);

                    //Xóa 2 bảng
                    thuNhapChiuThueRepository.DeleteThuNhapChiuThueByTaxId(model.Id);
                    giamTruThueTNCNRepository.DeleteGiamTruThueTNCNByTaxId(model.Id);

                    //Hiện tại lấy hết nhân viên theo mã thuế
                    var staff_in_tax = taxIncomePersonDetailRepository.GetAllTaxIncomePersonDetail().Where(n => n.TaxIncomePersonId == model.TaxIncomePersonId).ToList();

                    //Lấy dữ liệu từ bảng lương
                    var salaryReport = salaryTableDetailReportRepository.GetSalaryTableDetailReportBySalaryTableId(model.SalaryTableId.Value).ToList();

                    //Lấy dữ liệu giảm trừ từ người thân
                    var staff_family = staffFamilyRepository.GetAllStaffFamily().Where(n => n.IsDependencies == true).ToList();

                    //Cate Thu nhập chịu thếu và Giảm trừ
                    var cate = categoryRepository.GetCategoryByCode("TaxCode").ToList();

                    for (int a = 0; a < staff_in_tax.Count(); a++)
                    {
                        var tnct = cate.Where(n => n.Description == "TNCT").ToList();
                        for (int i = 0; i < tnct.Count; i++)
                        {
                            var value = salaryReport.SingleOrDefault(n => n.StaffId == staff_in_tax[a].StaffId && n.ColumName.Equals(tnct[i].Value));
                            ThuNhapChiuThue thunhapchiuthue = new ThuNhapChiuThue();
                            thunhapchiuthue.IsDeleted = false;
                            thunhapchiuthue.CreatedUserId = WebSecurity.CurrentUserId;
                            thunhapchiuthue.ModifiedUserId = WebSecurity.CurrentUserId;
                            thunhapchiuthue.AssignedUserId = WebSecurity.CurrentUserId;
                            thunhapchiuthue.CreatedDate = DateTime.Now;
                            thunhapchiuthue.ModifiedDate = DateTime.Now;

                            //Name
                            thunhapchiuthue.TaxId = Tax.Id;
                            thunhapchiuthue.StaffId = staff_in_tax[a].StaffId;
                            thunhapchiuthue.TaxIncomePersonDetailId = staff_in_tax[a].Id;
                            thunhapchiuthue.Name = tnct[i].Name;
                            thunhapchiuthue.Value = value == null ? 0 : decimal.Parse(value.Value);
                            thuNhapChiuThueRepository.InsertThuNhapChiuThue(thunhapchiuthue);
                        }

                        //Thêm thu nhập giảm thuế
                        var gt = cate.Where(n => n.Description == "GT").ToList();
                        for (int i = 0; i < gt.Count; i++)
                        {
                            var value = salaryReport.SingleOrDefault(n => n.StaffId == staff_in_tax[a].StaffId && n.ColumName.Equals(gt[i].Value));
                            GiamTruThueTNCN giamtru = new GiamTruThueTNCN();
                            giamtru.IsDeleted = false;
                            giamtru.CreatedUserId = WebSecurity.CurrentUserId;
                            giamtru.ModifiedUserId = WebSecurity.CurrentUserId;
                            giamtru.AssignedUserId = WebSecurity.CurrentUserId;
                            giamtru.CreatedDate = DateTime.Now;
                            giamtru.ModifiedDate = DateTime.Now;

                            //Name
                            giamtru.TaxId = Tax.Id;
                            giamtru.StaffId = staff_in_tax[a].StaffId;
                            giamtru.TaxIncomePersonDetailId = staff_in_tax[a].Id;
                            giamtru.Name = gt[i].Name;
                            giamtru.Value = value == null ? 0 : decimal.Parse(value.Value);
                            giamTruThueTNCNRepository.InsertGiamTruThueTNCN(giamtru);
                        }

                        //Thêm giảm trừ từ gia đình có thành viên phụ thuộc
                        var gd = staff_family.Where(n => n.StaffId == staff_in_tax[a].StaffId).ToList();
                        for (int i = 0; i < gd.Count; i++)
                        {
                            GiamTruThueTNCN giamtru = new GiamTruThueTNCN();
                            giamtru.IsDeleted = false;
                            giamtru.CreatedUserId = WebSecurity.CurrentUserId;
                            giamtru.ModifiedUserId = WebSecurity.CurrentUserId;
                            giamtru.AssignedUserId = WebSecurity.CurrentUserId;
                            giamtru.CreatedDate = DateTime.Now;
                            giamtru.ModifiedDate = DateTime.Now;

                            //Name
                            giamtru.TaxId = Tax.Id;
                            giamtru.StaffId = staff_in_tax[a].StaffId;
                            giamtru.TaxIncomePersonDetailId = staff_in_tax[a].Id;
                            giamtru.Name = string.Format("Người phụ thuộc [{0}]", gd[i].Name);
                            giamtru.Value = 3600000;
                            giamTruThueTNCNRepository.InsertGiamTruThueTNCN(giamtru);
                        }

                    }
                    //Thêm thu nhập chịu thuế

                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                    return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
                }

                return View(model);
            }

            return View(model);

            //if (Request.UrlReferrer != null)
            //    return Redirect(Request.UrlReferrer.AbsoluteUri);
            //return RedirectToAction("Index");
        }

        #endregion

        public ActionResult Synch(int? Id)
        {
            var model = TaxRepository.GetTaxById(Id.Value);
            try
            {
                if (model != null)
                {

                    //Xóa 2 bảng
                    thuNhapChiuThueRepository.DeleteThuNhapChiuThueByTaxId(model.Id);
                    giamTruThueTNCNRepository.DeleteGiamTruThueTNCNByTaxId(model.Id);

                    //Hiện tại lấy hết nhân viên theo mã thuế
                    var staff_in_tax = taxIncomePersonDetailRepository.GetAllTaxIncomePersonDetail().Where(n => n.TaxIncomePersonId == model.TaxIncomePersonId).ToList();

                    //Lấy dữ liệu từ bảng lương
                    var salaryReport = salaryTableDetailReportRepository.GetSalaryTableDetailReportBySalaryTableId(model.SalaryTableId.Value).ToList();

                    //Lấy dữ liệu giảm trừ từ người thân
                    var staff_family = staffFamilyRepository.GetAllStaffFamily().Where(n => n.IsDependencies == true).ToList();

                    //Cate Thu nhập chịu thếu và Giảm trừ
                    var cate = categoryRepository.GetCategoryByCode("TaxCode").ToList();

                    for (int a = 0; a < staff_in_tax.Count(); a++)
                    {
                        var tnct = cate.Where(n => n.Description == "TNCT").ToList();
                        for (int i = 0; i < tnct.Count; i++)
                        {
                            var value = salaryReport.SingleOrDefault(n => n.StaffId == staff_in_tax[a].StaffId && n.ColumName.Equals(tnct[i].Value));
                            ThuNhapChiuThue thunhapchiuthue = new ThuNhapChiuThue();
                            thunhapchiuthue.IsDeleted = false;
                            thunhapchiuthue.CreatedUserId = WebSecurity.CurrentUserId;
                            thunhapchiuthue.ModifiedUserId = WebSecurity.CurrentUserId;
                            thunhapchiuthue.AssignedUserId = WebSecurity.CurrentUserId;
                            thunhapchiuthue.CreatedDate = DateTime.Now;
                            thunhapchiuthue.ModifiedDate = DateTime.Now;

                            //Name
                            thunhapchiuthue.TaxId = model.Id;
                            thunhapchiuthue.StaffId = staff_in_tax[a].StaffId;
                            thunhapchiuthue.TaxIncomePersonDetailId = staff_in_tax[a].Id;
                            thunhapchiuthue.Name = tnct[i].Name;
                            thunhapchiuthue.Value = value == null ? 0 : decimal.Parse(value.Value);
                            thuNhapChiuThueRepository.InsertThuNhapChiuThue(thunhapchiuthue);
                        }

                        //Thêm thu nhập giảm thuế
                        var gt = cate.Where(n => n.Description == "GT").ToList();
                        for (int i = 0; i < gt.Count; i++)
                        {
                            var value = salaryReport.SingleOrDefault(n => n.StaffId == staff_in_tax[a].StaffId && n.ColumName.Equals(gt[i].Value));
                            GiamTruThueTNCN giamtru = new GiamTruThueTNCN();
                            giamtru.IsDeleted = false;
                            giamtru.CreatedUserId = WebSecurity.CurrentUserId;
                            giamtru.ModifiedUserId = WebSecurity.CurrentUserId;
                            giamtru.AssignedUserId = WebSecurity.CurrentUserId;
                            giamtru.CreatedDate = DateTime.Now;
                            giamtru.ModifiedDate = DateTime.Now;

                            //Name
                            giamtru.TaxId = model.Id;
                            giamtru.StaffId = staff_in_tax[a].StaffId;
                            giamtru.TaxIncomePersonDetailId = staff_in_tax[a].Id;
                            giamtru.Name = gt[i].Name;
                            giamtru.Value = value == null ? 0 : decimal.Parse(value.Value);
                            giamTruThueTNCNRepository.InsertGiamTruThueTNCN(giamtru);
                        }

                        //Thêm giảm trừ từ gia đình có thành viên phụ thuộc
                        var gd = staff_family.Where(n => n.StaffId == staff_in_tax[a].StaffId).ToList();
                        for (int i = 0; i < gd.Count; i++)
                        {
                            GiamTruThueTNCN giamtru = new GiamTruThueTNCN();
                            giamtru.IsDeleted = false;
                            giamtru.CreatedUserId = WebSecurity.CurrentUserId;
                            giamtru.ModifiedUserId = WebSecurity.CurrentUserId;
                            giamtru.AssignedUserId = WebSecurity.CurrentUserId;
                            giamtru.CreatedDate = DateTime.Now;
                            giamtru.ModifiedDate = DateTime.Now;

                            //Name
                            giamtru.TaxId = model.Id;
                            giamtru.StaffId = staff_in_tax[a].StaffId;
                            giamtru.TaxIncomePersonDetailId = staff_in_tax[a].Id;
                            giamtru.Name = string.Format("Người phụ thuộc [{0}]", gd[i].Name);
                            giamtru.Value = 3600000;
                            giamTruThueTNCNRepository.InsertGiamTruThueTNCN(giamtru);
                        }

                    }
                    //Thêm thu nhập chịu thuế

                    TempData[Globals.SuccessMessageKey] = "Cập nhật thành công.";
                    return RedirectToAction("Detail", "Tax", new { area = "Staff", Id = model.Id });
                }
            }
            catch (Exception ex)
            {
                TempData[Globals.SuccessMessageKey] = "Cập nhật thất bại: "+ex.Message;
                return RedirectToAction("Detail", "Tax", new { area = "Staff", Id = model.Id });
            }
          
            return View();
            //if (Request.UrlReferrer != null)
            //    return Redirect(Request.UrlReferrer.AbsoluteUri);
            //return RedirectToAction("Index");
        }

        public void buildThueTNCN(int? TaxId, List<TaxIncomePersonDetailViewModel> model_satffs, List<ThuNhapChiuThue> danh_sach_chiu_thue, List<GiamTruThueTNCN> danh_sach_giam_thue, List<Category> cateTaxCode, List<TaxRate> thuesuat)
        {

            if (model_satffs != null && model_satffs.Count > 0)
                for (int i = 0; i < model_satffs.Count; i++)
                {
                    model_satffs[i].TongThuNhapChiuThue = 0;
                    model_satffs[i].TongGiamTru = 0;
                    model_satffs[i].ThuNhapTinhThue = 0;
                    model_satffs[i].ThueTamTinh = 0;

                    //Thu nhập chịu thuế của nhân viên i
                    var ct_nv = danh_sach_chiu_thue.Where(n => n.TaxIncomePersonDetailId == model_satffs[i].Id && n.TaxId == TaxId);
                    if (ct_nv != null && ct_nv.Count() > 0)
                    {
                        foreach (var item in ct_nv)
                        {
                            model_satffs[i].TongThuNhapChiuThue += item.Value.Value;
                        }
                    }

                    //Giảm trừ - bảng lương
                    var gt_nv = danh_sach_giam_thue.Where(n => n.TaxIncomePersonDetailId == model_satffs[i].Id && n.TaxId == TaxId);
                    if (gt_nv != null && gt_nv.Count() > 0)
                    {
                        foreach (var item in gt_nv)
                        {
                            model_satffs[i].TongGiamTru += gt_nv.Where(n => n.Name.Equals(item.Name)).FirstOrDefault().Value.Value;
                        }
                    }

                    //Giảm trừ gia đình


                    //Thu nhập tính thuế
                    model_satffs[i].ThuNhapTinhThue = model_satffs[i].TongThuNhapChiuThue - model_satffs[i].TongGiamTru;


                    if (model_satffs[i].ThuNhapTinhThue < 0) // Do giá trị âm nên không tính thuế
                        continue;

                    //Thuế suất tạm tính
                    decimal total = 0;
                    TinhThueSuat(model_satffs[i].ThuNhapTinhThue, thuesuat, ref total);

                    model_satffs[i].ThueTamTinh = total;
                }
        }

        #region Detail
        public ActionResult Detail(int? Id)
        {
            var Tax = TaxRepository.GetTaxById(Id.Value);
            if (Tax != null && Tax.IsDeleted != true)
            {
                var model = new TaxViewModel();
                AutoMapper.Mapper.Map(Tax, model);

                if (model.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                {
                    TempData["FailedMessage"] = "NotOwner";
                    return RedirectToAction("Index");
                }

                //Auto load danh sách nhân viên đã đăng ký thuế thu nhập cá nhân
                List<TaxIncomePersonDetailViewModel> model_satffs = taxIncomePersonDetailRepository.GetAllvwTaxIncomePersonDetail().Where(n => n.TaxIncomePersonId == model.TaxIncomePersonId)
               .Select(item => new TaxIncomePersonDetailViewModel
               {
                   Id = item.Id,
                   CreatedUserId = item.CreatedUserId,
                   //CreatedUserName = item.CreatedUserName,
                   CreatedDate = item.CreatedDate,
                   ModifiedUserId = item.ModifiedUserId,
                   //ModifiedUserName = item.ModifiedUserName,
                   ModifiedDate = item.ModifiedDate,
                   Name = item.Name,
                   BranchName = item.BranchName,
                   Code = item.Code,
                   CountryId = item.CountryId,
                   DistrictName = item.DistrictName,
                   StaffId = item.StaffId,
                   Sale_BranchId = item.Sale_BranchId,
                   Email = item.Email,
                   Gender = item.Gender,
                   GenderName = item.GenderName,
                   IdCardNumber = item.IdCardNumber,
                   PositionName = item.PositionName,
                   TaxIncomePersonId = item.TaxIncomePersonId,
                   WardName = item.WardName,
                   ProvinceName = item.ProvinceName
               }).OrderBy(n => n.Id).ToList();

                
                //MNV - Họ tên - Chức vụ - đơn vị cộn tác- Tổng thu thập chịu thuế - Tổng giảm trừ - Thu nhập tính thuế - Thuế tạm tính
                //Tính thu nhập chịu thuế từng nhân viên
                var danh_sach_chiu_thue = thuNhapChiuThueRepository.GetAllThuNhapChiuThue().ToList();

                //Thêm các khoảng giảm
                var danh_sach_giam_thue = giamTruThueTNCNRepository.GetAllGiamTruThueTNCN().ToList();

                //Cate Thu nhập chịu thếu và Giảm trừ
                var cate = categoryRepository.GetCategoryByCode("TaxCode").ToList();

                //Dữ liệu bảng thuế suất
                var thuesuat = taxRateRepository.GetAllTaxRate().ToList();


                //Lấy dữ liệu giảm trừ từ người thân
                var staff_family = staffFamilyRepository.GetAllStaffFamily().Where(n => n.IsDependencies == true).ToList();

                buildThueTNCN(Id,model_satffs, danh_sach_chiu_thue, danh_sach_giam_thue, cate, thuesuat);

                //gán
                model.Staffs = new List<TaxIncomePersonDetailViewModel>();
                model.Staffs.AddRange(model_satffs);
                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        private void TinhThueSuat(decimal thuNhapTinhThue, IEnumerable<TaxRate> thueSuat, ref decimal total)
        {
            int level_min = thueSuat.OrderBy(n => n.Level).FirstOrDefault().Level.Value;
            int level = thueSuat.Where(n => n.FromValue <= thuNhapTinhThue && n.ToValue >= thuNhapTinhThue).FirstOrDefault().Level.Value;

            //Tầng lớp thuế suất
            for (int i = level_min; i <= level; i++)
            {
                var thue_suat_level = thueSuat.FirstOrDefault(n => n.Level == i);
                total +=(decimal) (thue_suat_level.ToValue.Value - thue_suat_level.FromValue.Value) * (decimal.Parse(thue_suat_level.TaxRateValue.Value.ToString()) / 100);
            }
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
                    var item = TaxRepository.GetTaxById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        {
                            TempData["FailedMessage"] = "NotOwner";
                            return RedirectToAction("Index");
                        }

                        item.IsDeleted = true;
                        TaxRepository.UpdateTax(item);
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
