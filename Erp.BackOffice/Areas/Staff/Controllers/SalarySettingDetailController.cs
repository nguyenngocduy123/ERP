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
using System.Data;
using Erp.BackOffice.Areas.Administration.Models;

namespace Erp.BackOffice.Staff.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class SalarySettingDetailController : Controller
    {
        private readonly ISalarySettingDetailRepository salarySettingDetailRepository;
        private readonly IUserRepository userRepository;
        private readonly IStaffsRepository staffsRepository;
        private readonly ISettingRepository settingRepository;
        private readonly ISalaryTableRepository salaryTableRepository;
        private readonly ISalaryTableDetailReportRepository salaryTableDetailReportRepository;
        private readonly ISalarySettingRepository salarySettingRepository;
        private readonly ICategoryRepository categoryRepository;
        public SalarySettingDetailController(
            ISalarySettingDetailRepository _SalarySettingDetail
            , IUserRepository _user
            , IStaffsRepository _Staffs
            , ISettingRepository _setting
            , ISalaryTableRepository _salarytable
            , ISalaryTableDetailReportRepository _srp
            , ISalarySettingRepository _salarySetting
            ,ICategoryRepository category
            )
        {
            salarySettingDetailRepository = _SalarySettingDetail;
            userRepository = _user;
            staffsRepository = _Staffs;
            settingRepository = _setting;
            salaryTableRepository = _salarytable;
            salaryTableDetailReportRepository = _srp;
            salarySettingRepository = _salarySetting;
            categoryRepository = category;
        }

        #region Index
        public ViewResult Index(int SalarySettingId)
        {
            SalarySettingEditViewModel model = new SalarySettingEditViewModel();
            model.Id = SalarySettingId;
            model.ListAllColumns = new List<SalarySettingDetailViewModel>();

            //Danh sách tất cả columns
            var q = salarySettingDetailRepository.GetAllSalarySettingDetail()
                .Where(item => item.SalarySettingId == SalarySettingId)
                .ToList();

            AutoMapper.Mapper.Map(q, model.ListAllColumns);

            //Danh sách nhóm
            model.SelectList_Group = q.Where(item => item.ParentId == null)
                .Select(item => new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name
                }).ToList();

            model.SelectListGroupName = SelectListHelper.GetSelectList_Category("SalarySettingDetail_GroupName", null, null);
            model.SelectListFormulaType = SelectListHelper.GetSelectList_Category("SalarySettingDetail_FormulaType", null, null);
            model.SelectListDataType = SelectListHelper.GetSelectList_Category("SalarySettingDetail_DataType", null, null);



            //Cột dữ liệu chấm công của nhân viên
            var _pTimekeepingSynthesis = XuLyDuLieuTuModel<TimekeepingSynthesis>(new TimekeepingSynthesis());
            model.ListColumnsTimekeepingSynthesis = new List<string>();
            model.ListColumnsTimekeepingSynthesis.AddRange(_pTimekeepingSynthesis);

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            var list_category = categoryRepository.GetCategoryByCode("ListSettingMoneySalary" + SalarySettingId);
            for (int i = 0; i < model.ListAllColumns.Count(); i++)
            {
                if (list_category.Where(x => x.OrderNo == model.ListAllColumns[i].Id).Count() > 0)
                {
                    model.ListAllColumns[i].IsMoney = true;
                }
                else
                {
                    model.ListAllColumns[i].IsMoney = false;
                }
            }
            return View(model);
        }
        #endregion

        #region FormulaEditor

        private List<SalaryTableViewModel> GetDataSalaryTable()
        {
            IQueryable<SalaryTableViewModel> q = salaryTableRepository.GetAllSalaryTable()
                 .Where(n => (("," + Helpers.Common.CurrentUser.DrugStore + ",").Contains("," + n.BranchId + ",") == true || (Helpers.Common.CurrentUser.UserTypeId == 1 && Helpers.Common.CurrentUser.UserTypeId == 21)))
               .Select(item => new SalaryTableViewModel
               {
                   Id = item.Id,
                   CreatedUserId = item.CreatedUserId,
                   CreatedDate = item.CreatedDate,
                   ModifiedUserId = item.ModifiedUserId,
                   ModifiedDate = item.ModifiedDate,
                   Name = item.Name,
                   BranchId = item.BranchId,
                   TargetMonth = item.TargetMonth,
                   TargetYear = item.TargetYear,
                   Status = item.Status,
                   Submitted = item.Submitted,
                   HiddenForMonth = item.HiddenForMonth,
                   SalarySettingId = item.SalarySettingId.Value,
               }).OrderByDescending(item => item.Id);

            var model = q.ToList();
            for (int i = 0; i < model.Count; i++)
            {
                model[i].Name = string.Format("{0} {1}/{2}", model[i].Name, model[i].TargetMonth, model[i].TargetYear);
                var model_rp = salaryTableDetailReportRepository.GetSalaryTableDetailReportBySalaryTableId(model[i].Id).ToList();
                model[i].ListalaryTableDetailReport = model_rp;
            }

            return model;
        }

        public ViewResult FormulaEditor(int SalarySettingId, int TargetId)
        {
            FormulaEditorViewModel model = new FormulaEditorViewModel();
            model.SalarySettingId = SalarySettingId;
            model.ListAllColumns = new List<SalarySettingDetailViewModel>();
            //model.ListSalaryTable = new List<SalaryTableViewModel>();

            //Danh sách các bản mẫu salarySetting
            model.ListSalarySetting = new List<SalarySetting>();
            model.ListSalarySetting = salarySettingRepository.GetAllSalarySetting().ToList();

            //Danh sách tất cả columns
            var q = salarySettingDetailRepository.GetAllSalarySettingDetail()
                .Where(item => item.SalarySettingId == SalarySettingId)
                .ToList();

            AutoMapper.Mapper.Map(q, model.ListAllColumns);
            model.FormulaEditor_Value = model.ListAllColumns.Where(item => item.Id == TargetId).FirstOrDefault().Formula;

            //cột dữ liệu Salarytable
            //model.ListSalaryTable = GetDataSalaryTable();

            //Cột dữ liệu chấm công của nhân viên
            var _pTimekeepingSynthesis = XuLyDuLieuTuModel<TimekeepingSynthesis>(new TimekeepingSynthesis());
            model.ListColumnsTimekeepingSynthesis = new List<string>();
            model.ListColumnsTimekeepingSynthesis.AddRange(_pTimekeepingSynthesis);

            //cột dữ liệu bạc lương
            var _ProcessPay = XuLyDuLieuTuModel<ProcessPay>(new ProcessPay());
            model.ListColumnsProcessPay = new List<string>();
            model.ListColumnsProcessPay.AddRange(_ProcessPay);

            //cột dữ liệu tạm ứng lương
            var _SalaryAdvance = XuLyDuLieuTuModel<SalaryAdvance>(new SalaryAdvance());
            model.ListColumnsSalaryAdvance = new List<string>();
            model.ListColumnsSalaryAdvance.AddRange(_SalaryAdvance);

            //cột dữ liệu tài khoản ngân hàng
            var _bank = XuLyDuLieuTuModel<Bank>(new Bank());
            model.ListColumnsBank = new List<string>();
            model.ListColumnsBank.AddRange(_bank);

            //Dữ liệu thâm niên
            //var _pSeniorit = XuLyDuLieuTuModel<SalarySeniority>(new SalarySeniority());
            //model.ListAllColumnsSalarySeniorit = new List<string>();
            //model.ListAllColumnsSalarySeniorit.AddRange(_pSeniorit);

            //Dữ liệu nhân viên - đoàn đảng
            var _pStaff = XuLyDuLieuTuModel<vwStaffs>(new vwStaffs());
            model.ListColumnsStaff = new List<string>();
            model.ListColumnsStaff.AddRange(_pStaff);

            //Dữ liệu phạt 
            var _phat = XuLyDuLieuTuModel<PhatModel>(new PhatModel());
            model.ListAllColumnsPhat = new List<string>();
            model.ListAllColumnsPhat.AddRange(_phat);

            //Dữ liệu thưởng
            var _thuong = XuLyDuLieuTuModel<KhenThuongModel>(new KhenThuongModel());
            model.ListAllColumnsKhenThuong = new List<string>();
            model.ListAllColumnsKhenThuong.AddRange(_thuong);

            //Dữ liệu Phụ cấp
            var _PhuCap = XuLyDuLieuTuModel<StaffAllowance>(new StaffAllowance());
            model.ListAllColumnsPhuCap = new List<string>();
            model.ListAllColumnsPhuCap.AddRange(_PhuCap);

            //Dữ liệu giữ lương theo hợp đồng
            var _giuluong = XuLyDuLieuTuModel<StaffLuongGiuHopDong>(new StaffLuongGiuHopDong());
            model.ListAllColumnsGiuLuongTheoHopDong = new List<string>();
            model.ListAllColumnsGiuLuongTheoHopDong.AddRange(_giuluong);

            //Cột dữ liệu
            //cột dữ liệu hệ thống
            model.ListColumnsSetting = new List<Setting>();

            var du_lieu_ht = settingRepository.GetAlls()
                .Where(item => item.Code == "staff_salary")
                .OrderBy(item => item.Note).ToList();

            model.ListColumnsSetting.AddRange(du_lieu_ht);

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];

            return View(model);
        }

        //Add cột dữ liệu Properties của Class
        public List<string> XuLyDuLieuTuModel<T>(T newmodel)
        {
            List<string> list = new List<string>();
            var pi = newmodel.GetType().GetProperties();
            foreach (var p in pi)
            {
                if (!Helpers.Common.fieldsNoReplace.Contains(p.Name))
                {
                    list.Add(p.Name);
                }
            }

            return list;
        }

        #endregion

        #region Create
        public ViewResult Create(int SalarySettingId)
        {
            var model = new SalarySettingDetailViewModel();
            model.SalarySettingId = SalarySettingId;

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(SalarySettingDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var salarySettingDetail = new SalarySettingDetail();
                AutoMapper.Mapper.Map(model, salarySettingDetail);
                salarySettingDetail.IsDeleted = false;
                salarySettingDetail.CreatedUserId = WebSecurity.CurrentUserId;
                salarySettingDetail.ModifiedUserId = WebSecurity.CurrentUserId;
                salarySettingDetail.AssignedUserId = WebSecurity.CurrentUserId;
                salarySettingDetail.CreatedDate = DateTime.Now;
                salarySettingDetail.ModifiedDate = DateTime.Now;
                salarySettingDetailRepository.InsertSalarySettingDetail(salarySettingDetail);

                return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var SalarySettingDetail = salarySettingDetailRepository.GetSalarySettingDetailById(Id.Value);
            if (SalarySettingDetail != null && SalarySettingDetail.IsDeleted != true)
            {
                var model = new SalarySettingDetailViewModel();
                AutoMapper.Mapper.Map(SalarySettingDetail, model);

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
        public ActionResult Edit(int SalarySettingId, int Id, int? ParentId, int OrderNo, string Name, string FormulaType, string Formula, double DefaultValue, bool IsDefaultValue, string GroupName, bool IsGroup, bool IsDisplay, string DataType, bool IsSum, bool IsMoney,bool IsChange)
        {
            var q = salarySettingDetailRepository.GetAllSalarySettingDetail()
                .Where(item => item.SalarySettingId == SalarySettingId);

            if (q.Any(item => item.Id != Id && item.Name == Name))
                return Content("exsits");

            var salarySettingDetail = salarySettingDetailRepository.GetSalarySettingDetailById(Id);

            if (salarySettingDetail != null)
            {
                var list = q.ToList();
                foreach (var item in list)
                {
                    if (!string.IsNullOrEmpty(item.Formula))
                    {
                        item.Formula = item.Formula.Replace(salarySettingDetail.Name, Name);
                        salarySettingDetailRepository.UpdateSalarySettingDetail(item);
                    }
                }

                salarySettingDetail.ModifiedUserId = WebSecurity.CurrentUserId;
                salarySettingDetail.ModifiedDate = DateTime.Now;

                salarySettingDetail.OrderNo = OrderNo;
                salarySettingDetail.Name = Name;
                salarySettingDetail.ParentId = ParentId;
                salarySettingDetail.FormulaType = FormulaType;
                salarySettingDetail.DefaultValue = DefaultValue;
                salarySettingDetail.Formula = Formula;
                salarySettingDetail.IsDefaultValue = IsDefaultValue;
                salarySettingDetail.GroupName = GroupName;
                salarySettingDetail.IsGroup = IsGroup;
                salarySettingDetail.IsDisplay = IsDisplay;
                salarySettingDetail.DataType = DataType;
                salarySettingDetail.IsSum = IsSum;
                salarySettingDetail.IsChange = IsChange;
                salarySettingDetailRepository.UpdateSalarySettingDetail(salarySettingDetail);
                if (IsMoney == true)
                {
                    Erp.BackOffice.Cms.Controllers.CategoryController.CreateAndEditCategory(salarySettingDetail.Id.ToString(), salarySettingDetail.Name, "ListSettingMoneySalary" + salarySettingDetail.SalarySettingId, null, salarySettingDetail.Id);
                }
                else
                {
                    Erp.BackOffice.Cms.Controllers.CategoryController.DeleteCategory(salarySettingDetail.Id.ToString(), "ListSettingMoneySalary" + salarySettingDetail.SalarySettingId);
                }
            }
            else
            {
                salarySettingDetail = new SalarySettingDetail();
                salarySettingDetail.IsDeleted = false;
                salarySettingDetail.CreatedUserId = WebSecurity.CurrentUserId;
                salarySettingDetail.ModifiedUserId = WebSecurity.CurrentUserId;
                salarySettingDetail.AssignedUserId = WebSecurity.CurrentUserId;
                salarySettingDetail.CreatedDate = DateTime.Now;
                salarySettingDetail.ModifiedDate = DateTime.Now;

                salarySettingDetail.SalarySettingId = SalarySettingId;
                salarySettingDetail.OrderNo = OrderNo;
                salarySettingDetail.Name = Name;
                salarySettingDetail.ParentId = ParentId;
                salarySettingDetail.FormulaType = FormulaType;
                salarySettingDetail.DefaultValue = DefaultValue;
                salarySettingDetail.Formula = Formula;
                salarySettingDetail.IsDefaultValue = IsDefaultValue;
                salarySettingDetail.GroupName = GroupName;
                salarySettingDetail.IsGroup = IsGroup;
                salarySettingDetail.IsDisplay = IsDisplay;
                salarySettingDetail.DataType = DataType;
                salarySettingDetail.IsSum = IsSum;
                salarySettingDetail.IsChange = IsChange;
                salarySettingDetailRepository.InsertSalarySettingDetail(salarySettingDetail);
                if (IsMoney == true)
                {
                    Erp.BackOffice.Cms.Controllers.CategoryController.CreateAndEditCategory(salarySettingDetail.Id.ToString(), salarySettingDetail.Name, "ListSettingMoneySalary" + salarySettingDetail.SalarySettingId, null, salarySettingDetail.Id);
                }
                 else
                {
                    Erp.BackOffice.Cms.Controllers.CategoryController.DeleteCategory(salarySettingDetail.Id.ToString(), "ListSettingMoneySalary" + salarySettingDetail.SalarySettingId);
                }
            }

            return Content("success");
        }

        [HttpPost]
        public ActionResult Update_OrderNo(int Id, int OrderNo_First, int OrderNo_Last)
        {
            var salarySettingDetail = salarySettingDetailRepository.GetSalarySettingDetailById(Id);
            salarySettingDetail.ModifiedUserId = WebSecurity.CurrentUserId;
            salarySettingDetail.ModifiedDate = DateTime.Now;
            salarySettingDetail.OrderNo = OrderNo_Last;
            salarySettingDetailRepository.UpdateSalarySettingDetail(salarySettingDetail);

            var salarySettingDetail2 = salarySettingDetailRepository.GetAllSalarySettingDetail()
                .Where(item => item.OrderNo == OrderNo_First).FirstOrDefault();

            if (salarySettingDetail2 != null)
            {
                salarySettingDetail2.ModifiedUserId = WebSecurity.CurrentUserId;
                salarySettingDetail2.ModifiedDate = DateTime.Now;
                salarySettingDetail2.OrderNo = OrderNo_First;
                salarySettingDetailRepository.UpdateSalarySettingDetail(salarySettingDetail2);
            }

            return Content("success");
        }

        private string GetExcelColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }

        
        #endregion

        #region Delete
        [HttpPost]
        public ActionResult Delete()
        {
            try
            {
                int SalarySettingId = 0;
                string idDeleteAll = Request["DeleteId-checkbox"];
                string[] arrDeleteId = idDeleteAll.Split(',');
                for (int i = 0; i < arrDeleteId.Count(); i++)
                {
                    var item = salarySettingDetailRepository.GetSalarySettingDetailById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        SalarySettingId = item.SalarySettingId.Value;
                        salarySettingDetailRepository.DeleteSalarySettingDetail(item.Id);
                    }
                }
                return RedirectToAction("Edit", "SalarySetting", new { Id = SalarySettingId });
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
