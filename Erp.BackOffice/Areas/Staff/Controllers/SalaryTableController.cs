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
using System.Data;
using org.mariuszgromada.math.mxparser;
using System.Reflection;
using System.ComponentModel;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.ComponentModel.DataAnnotations;
using Erp.BackOffice.Areas.Administration.Models;
using Erp.BackOffice.Areas.Staff.Models;
using Erp.Domain.Account.Entities;
using Erp.Domain.Account.Interfaces;
using System.Text.RegularExpressions;
using Erp.BackOffice.Account.Models;
using Erp.BackOffice.App_GlobalResources;

namespace Erp.BackOffice.Staff.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class SalaryTableController : Controller
    {
        private readonly ISalaryTableRepository SalaryTableRepository;
        private readonly IUserRepository userRepository;
        private readonly IBranchDepartmentRepository branchDepartmentRepository;
        private readonly IStaffsRepository staffsRepository;
        private readonly ISalarySettingRepository SalarySettingRepository;
        private readonly ISalarySettingDetailRepository salarySettingDetailRepository;
        private readonly ISalarySettingDetail_StaffRepository salarySettingDetail_StaffRepository;
        private readonly ISalaryTableDetailRepository salaryTableDetailRepository;
        private readonly ITimekeepingSynthesisRepository timekeepingSynthesisRepository;
        private readonly ISettingRepository settingRepository;
        private readonly IBranchRepository branchRepository;
        private readonly IPaymentRepository paymentRepository;
        private readonly ISalaryTableDetailReportRepository salaryTableDetailReportRepository;
        private readonly ISalaryTableDetail_StaffRepository SalaryTableDetail_StaffRepository;
        private readonly IProcessPayRepository processPayRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IPaymentDetailRepository paymentDetailRepository;
        private readonly ISalaryAdvanceRepository SalaryAdvanceRepository;
        private readonly IBankRepository bankRepository;
        private readonly IBonusDisciplineRepository bonusDisciplineRepository;
        private readonly IStaffAllowanceRepository staffAllowanceRepository;
        private readonly IStaffLuongGiuHopDongRepository staffLuongGiuHopDongRepository;
        public SalaryTableController(
            ISalaryTableRepository _SalaryTable
            , IUserRepository _user
            , IBranchDepartmentRepository branchDepartment
            , IStaffsRepository _Staffs
            , ISalarySettingDetailRepository _SalarySettingDetail
            , ISalarySettingRepository _SalarySetting
            , ISalarySettingDetail_StaffRepository _SalarySettingDetail_Staff
            , ISalaryTableDetailRepository _salaryTableDetail
            , ITimekeepingSynthesisRepository _timekeepingSynthesis
            , ISettingRepository _setting
            , IBranchRepository _branchRepository
            , IPaymentRepository _Payment
            , ISalaryTableDetailReportRepository _salaryTr
            , ISalaryTableDetail_StaffRepository _SalaryTableDetail_Staff
            , IProcessPayRepository _processPayRepository
            , ICategoryRepository category
            , IPaymentDetailRepository paymentDetail
            , ISalaryAdvanceRepository SalaryAdvance
            , IBankRepository bank
            , IBonusDisciplineRepository bound
            , IStaffAllowanceRepository allowan
            , IStaffLuongGiuHopDongRepository luonghd
            )
        {
            SalaryTableRepository = _SalaryTable;
            userRepository = _user;
            branchDepartmentRepository = branchDepartment;
            staffsRepository = _Staffs;
            SalarySettingRepository = _SalarySetting;
            salarySettingDetailRepository = _SalarySettingDetail;
            salarySettingDetail_StaffRepository = _SalarySettingDetail_Staff;
            salaryTableDetailRepository = _salaryTableDetail;
            timekeepingSynthesisRepository = _timekeepingSynthesis;
            settingRepository = _setting;
            branchRepository = _branchRepository;
            paymentRepository = _Payment;
            salaryTableDetailReportRepository = _salaryTr;
            SalaryTableDetail_StaffRepository = _SalaryTableDetail_Staff;
            processPayRepository = _processPayRepository;
            categoryRepository = category;
            paymentDetailRepository = paymentDetail;
            SalaryAdvanceRepository = SalaryAdvance;
            bankRepository = bank;
            bonusDisciplineRepository = bound;
            staffAllowanceRepository = allowan;
            staffLuongGiuHopDongRepository = luonghd;
        }

        #region Index


        public enum TreeStyle
        {
            ModelName,
            ModelYear,
            ModelMonth
        }

        //private TreeSalaryViewModel FormatTree()
        //{


        //    return FNode;

        //}

        public ViewResult Index(string txtSearch)
        {
            List<SalarySettingViewModel> q = SalarySettingRepository.GetAllSalarySetting()
                .Select(item => new SalarySettingViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    OrderNo = item.OrderNo,
                    SalaryApprovalType = item.SalaryApprovalType,
                    IsSend = item.IsSend
                }).OrderBy(m => m.OrderNo).ToList();

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];

            return View(q);
        }

        public ViewResult Table(int? Id)
        {
            //id : SalarySetting
            ViewBag.SalarySettingId = Id;

            var currentUser = Helpers.Common.CurrentUser;
            IQueryable<SalaryTableViewModel> q = SalaryTableRepository.GetAllSalaryTable()
                .Where(n => n.SalarySettingId == Id
                    && (("," + Helpers.Common.CurrentUser.DrugStore + ",").Contains("," + n.BranchId + ",") == true 
                    || (Helpers.Common.CurrentUser.UserTypeId == 1 && Helpers.Common.CurrentUser.UserTypeId == 21)))
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
                   HiddenForMonth = item.HiddenForMonth
               }).Where(x => ("," + Helpers.Common.CurrentUser.DrugStore + ",").Contains("," + x.BranchId + ",") == true).OrderBy(m => m.CreatedDate);

            return View(q);
        }

        #endregion

        #region Create
        public ViewResult Create(int SalarySettingId)
        {
            var model = new SalaryTableViewModel();
            //model.BranchId = Helpers.Common.CurrentUser.BranchId.Value;
            model.TargetMonth = DateTime.Now.Month;
            model.TargetYear = DateTime.Now.Year;

            var salarySetting = SalarySettingRepository.GetSalarySettingById(SalarySettingId);

            model.SalarySettingId = salarySetting.Id;
            model.Name = salarySetting.Name;
            ViewBag.Hiden = salarySetting.HiddenForMonth;
            //var currentUser = Helpers.Common.CurrentUser;

            ViewBag.Staffs = staffsRepository.GetvwAllStaffs().OrderBy(u => u.Id).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(int? SalarySettingId, int? TargetMonth, int? TargetYear, string Name)
        {
            if (ModelState.IsValid)
            {

                var salarysetting = SalarySettingRepository.GetSalarySettingById(SalarySettingId.Value);
                //var salarySettingDetail_BySalarySettingId = Helpers.Common.ByteArrayToObject(getListSalarySettingDetail(SalarySettingId.Value)) as List<SalarySettingDetailViewModel>;
                if (salarysetting.IsSend == true)
                {
                    var salary = SalaryTableRepository.GetAllSalaryTable().Where(u => ("," + Helpers.Common.CurrentUser.DrugStore + ",").Contains("," + u.BranchId + ",") == true && u.SalarySettingId == SalarySettingId && u.TargetYear == TargetYear).ToList();
                    if (salary.Count > 0)
                    {
                        TempData[Globals.SuccessMessageKey] = "Đã tạo " + salary.FirstOrDefault().Name + " năm " + TargetYear;
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    var salary = SalaryTableRepository.GetAllSalaryTable().Where(u => ("," + Helpers.Common.CurrentUser.DrugStore + ",").Contains("," + u.BranchId + ",") == true && u.TargetMonth == TargetMonth && u.SalarySettingId == SalarySettingId && u.TargetYear == TargetYear).ToList();
                    if (salary.Count > 0)
                    {
                        TempData[Globals.SuccessMessageKey] = "Đã tạo " + salary.FirstOrDefault().Name + " tháng " + TargetMonth + " năm " + TargetYear;
                        return RedirectToAction("Index");
                    }
                }
                var salaryTable = new SalaryTable();
                salaryTable.IsDeleted = false;
                salaryTable.CreatedUserId = WebSecurity.CurrentUserId;
                salaryTable.ModifiedUserId = WebSecurity.CurrentUserId;
                salaryTable.AssignedUserId = WebSecurity.CurrentUserId;
                salaryTable.CreatedDate = DateTime.Now;
                salaryTable.ModifiedDate = DateTime.Now;
                salaryTable.SalarySettingId = SalarySettingId;
                salaryTable.TargetMonth = TargetMonth;
                salaryTable.TargetYear = TargetYear;


                var salarySetting = SalarySettingRepository.GetSalarySettingById(SalarySettingId.Value);
                //SalaryTable.Name = string.Format("{0} {1}/{2} ({3})", salarySettingName.Name, SalaryTable.TargetMonth, SalaryTable.TargetYear, branch.Name);
                salaryTable.Name = Name;
                salaryTable.ListSalarySettingDetail = getListSalarySettingDetail(SalarySettingId.Value);
                salaryTable.Status = App_GlobalResources.Wording.SalaryTableStatus_InProcess;
                salaryTable.SalarySettingId = SalarySettingId;
                //salaryTable.BranchId = Helpers.Common.CurrentUser.BranchId.Value;
                salaryTable.IsSend = salarySetting.IsSend;
                salaryTable.SalaryApprovalType = salarySetting.SalaryApprovalType;
                salaryTable.TotalSalary = 0;
                salaryTable.HiddenForMonth = salarySetting.HiddenForMonth;
                SalaryTableRepository.InsertSalaryTable(salaryTable);

                string idStaffAll = Request["StaffId-checkbox"];
                List<string> arrStaffId = idStaffAll.Split(',').OrderBy(p => int.Parse(p)).ToList();
                for (int i = 0; i < arrStaffId.Count(); i++)
                {
                    var SalaryTableDetailStaff = new SalaryTableDetail_Staff();
                    SalaryTableDetailStaff.IsDeleted = false;
                    SalaryTableDetailStaff.CreatedUserId = WebSecurity.CurrentUserId;
                    SalaryTableDetailStaff.ModifiedUserId = WebSecurity.CurrentUserId;
                    SalaryTableDetailStaff.AssignedUserId = WebSecurity.CurrentUserId;
                    SalaryTableDetailStaff.CreatedDate = DateTime.Now;
                    SalaryTableDetailStaff.ModifiedDate = DateTime.Now;
                    SalaryTableDetailStaff.SalaryTableId = salaryTable.Id;
                    SalaryTableDetailStaff.StaffId = Int32.Parse(arrStaffId[i]);
                    SalaryTableDetail_StaffRepository.InsertSalaryTableDetail_Staff(SalaryTableDetailStaff);
                }
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;

                //Go to details
                return RedirectToAction("Detail", "SalaryTable", new { Id = salaryTable.Id });
            }
            return RedirectToAction("Create", new { SalarySettingId = SalarySettingId });
        }


        #region Action SalaryTableDetailReport
        private void InsertSalaryTableDetailReport(int staffId, int salaryTableId, List<SalarySettingDetailViewModel> salarySettingDetail_BySalarySettingId)
        {
            var model = new SalaryTableDetailReport();
            model.IsDeleted = false;
            model.CreatedUserId = WebSecurity.CurrentUserId;
            model.ModifiedUserId = model.CreatedUserId;
            model.AssignedUserId = model.CreatedUserId;
            model.CreatedDate = DateTime.Now;
            model.ModifiedDate = DateTime.Now;

            model.SalaryTableId = salaryTableId;
            model.StaffId = staffId;
            model.ColumName = "OrderNo";
            model.DataType = "string";
            salaryTableDetailReportRepository.InsertSalaryTableDetailReport(model);

            for (int i = 0; i < salarySettingDetail_BySalarySettingId.Count; i++)
            {
                model = new SalaryTableDetailReport();
                model.IsDeleted = false;
                model.CreatedUserId = WebSecurity.CurrentUserId;
                model.ModifiedUserId = model.CreatedUserId;
                model.AssignedUserId = model.CreatedUserId;
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;

                model.SalaryTableId = salaryTableId;
                model.StaffId = staffId;
                model.ColumName = salarySettingDetail_BySalarySettingId[i].Name;
                model.DataType = salarySettingDetail_BySalarySettingId[i].DataType;
                salaryTableDetailReportRepository.InsertSalaryTableDetailReport(model);
            }
        }

        private void UpdateSalaryTableDetailReport(List<SalaryTableDetailReport> ListSalaryTableDetailReportUpdate)
        {
            //Kiểm tra coi có chưa, có thì update, không có thì tạo mới
            var _tempcheck = salaryTableDetailReportRepository.GetAllSalaryTableDetailReport().ToList();
            //.Where(n => n.StaffId== ListSalaryTableDetailReportUpdate[0].StaffId && n.SalaryTableId == ListSalaryTableDetailReportUpdate[0].SalaryTableId).ToList();
            if (_tempcheck != null && _tempcheck.Count > 0)
            {
                var _tempcheck2 = _tempcheck.Where(n => n.StaffId == ListSalaryTableDetailReportUpdate[0].StaffId && n.SalaryTableId == ListSalaryTableDetailReportUpdate[0].SalaryTableId).ToList();
                if (_tempcheck2 != null && _tempcheck2.Count > 0)
                {
                    foreach (var item in ListSalaryTableDetailReportUpdate)
                    {
                        var model = salaryTableDetailReportRepository.GetSalaryTableDetailReportByStaffId_Name(item.SalaryTableId.Value, item.StaffId.Value, item.ColumName);
                        if (model != null)
                        {
                            //Nếu mà dữ liệu thay đổi (tên colum, value) thì update, còn không thì thôi
                            bool b_value = string.IsNullOrEmpty(model.Value) ? true : !model.Value.Equals(item.Value);
                            if (!model.ColumName.Equals(item.ColumName) || b_value)
                            {
                                model.ColumName = item.ColumName;
                                model.Value = item.Value;
                                model.ModifiedUserId = WebSecurity.CurrentUserId;
                                model.ModifiedDate = DateTime.Now;
                                salaryTableDetailReportRepository.UpdateSalaryTableDetailReport(model);
                            }
                        }
                    }
                }
                else
                    CreateListSalaryTableDetailReport_ForUpdate(ListSalaryTableDetailReportUpdate);
            }
            else
                CreateListSalaryTableDetailReport_ForUpdate(ListSalaryTableDetailReportUpdate);
        }

        private void CreateListSalaryTableDetailReport_ForUpdate(List<SalaryTableDetailReport> ListSalaryTableDetailReportUpdate)
        {
            for (int i = 0; i < ListSalaryTableDetailReportUpdate.Count; i++)
            {
                var model = new SalaryTableDetailReport();
                model = ListSalaryTableDetailReportUpdate[i];
                model.IsDeleted = false;
                model.CreatedUserId = WebSecurity.CurrentUserId;
                model.ModifiedUserId = model.CreatedUserId;
                model.AssignedUserId = model.CreatedUserId;
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                salaryTableDetailReportRepository.InsertSalaryTableDetailReport(model);
            }
        }

        public bool IsChange(string _old, string _new)
        {
            return _old.Equals(_new);
        }

        #endregion

        byte[] getListSalarySettingDetail(int Id)
        {
            //var salarySetting = SalarySettingRepository.GetAllSalarySetting().Where(item => item.DepartmentId == DepartmentId).FirstOrDefault();
            var salarySetting = SalarySettingRepository.GetAllSalarySetting().Where(item => item.Id == Id).FirstOrDefault();
            if (salarySetting != null && salarySetting.IsDeleted != true)
            {
                var ListAllColumns = new List<SalarySettingDetailViewModel>();

                //Danh sách tất cả columns
                var q = salarySettingDetailRepository.GetAllSalarySettingDetail()
                    .Where(item => item.SalarySettingId == salarySetting.Id && item.IsDisplay).OrderBy(u => u.OrderNo)
                    .ToList();
                AutoMapper.Mapper.Map(q, ListAllColumns);

                foreach (var item in ListAllColumns)
                {
                    if (item.ParentId == null && ListAllColumns.Any(i => i.ParentId == item.Id))
                    {
                        item.HasSubList = true;
                        item.NumberOfSubList = ListAllColumns.Count(i => i.ParentId == item.Id);
                    }
                }

                return Helpers.Common.ObjectToByteArray(ListAllColumns);
            }

            return null;
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id, string Status)
        {
            var SalaryTable = SalaryTableRepository.GetSalaryTableById(Id.Value);
            if (SalaryTable != null && SalaryTable.IsDeleted != true)
            {
                if (Status == App_GlobalResources.Wording.SalaryTableStatus_InProcess)
                {
                    SalaryTable.Status = App_GlobalResources.Wording.SalaryTableStatus_InProcess;
                    SalaryTableRepository.UpdateSalaryTable(SalaryTable);
                }

                return RedirectToAction("Detail", new { Id = SalaryTable.Id });
            }

            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(SalaryTableViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Lưu & Khóa bảng lương
                if (Request["Submit"] == "Save")
                {
                    var salaryTable = SalaryTableRepository.GetSalaryTableById(model.Id);
                    //Xóa chi tiết bảng lương
                    //salaryTableDetailRepository.DeleteSalaryTableDetail(salaryTable.Id);

                    //xóa bảng chi tiết report
                    salaryTableDetailReportRepository.DeleteSalaryTableDetailReport(salaryTable.Id);

                    //Lưu trữ cài đặt bảng lương
                    model.ListAllColumns = Helpers.Common.ByteArrayToObject(salaryTable.ListSalarySettingDetail) as List<SalarySettingDetailViewModel>;

                    //Tạo bảng tính lương
                    decimal total = 0;
                    //var ListSalaryTableReportUpdate = new List<SalaryTableDetailReport>();
                    var ListStaff = new List<vwStaffs>();
                    DataTable dataLink = new DataTable(); 
                    model.SalaryTableData = buildSalaryTable(model.ListAllColumns, salaryTable.Id, salaryTable.TargetYear.Value, salaryTable.TargetMonth.Value, ref total, ref dataLink);

                    //Dữ liệu kiểm tra TableSQL - IdTable | in cell
                    model.SalaryTableLink = dataLink;

                    //List<SalaryTableDetailReport> ListSalaryTableReport = new List<SalaryTableDetailReport>();
                    var listIdStaff = SalaryTableDetail_StaffRepository.GetAllSalaryTableDetail_Staff().Where(n => n.SalaryTableId == salaryTable.Id).Select(n => n.StaffId.Value).ToList();

                    //

                    //danh sách các khoản tiền lương của nhân viên
                    var list_category = categoryRepository.GetCategoryByCode("ListSettingMoneySalary" + salaryTable.SalarySettingId);

                    foreach (DataRow row in model.SalaryTableData.Rows)
                    {
                        #region Khai báo để tạo phiếu chi cho từng nhân viên
                        var model_Payment = new Payment();
                        model_Payment.IsDeleted = false;
                        model_Payment.AssignedUserId = WebSecurity.CurrentUserId;
                        model_Payment.CreatedUserId = WebSecurity.CurrentUserId;
                        model_Payment.CreatedDate = DateTime.Now;
                        model_Payment.ModifiedUserId = WebSecurity.CurrentUserId;
                        model_Payment.ModifiedDate = DateTime.Now;
                        var ListPaymentDetail = new List<PaymentDetail>();
                        #endregion

                        foreach (DataColumn col in model.SalaryTableData.Columns)
                        {
                            string colname = "";
                            if (col.Caption.Equals("OrderNo"))
                                colname = "OrderNo";
                            else
                                colname = model.ListAllColumns.SingleOrDefault(w => w.Id == int.Parse(col.Caption)).Name;

                            SalaryTableDetailReport salarytable_rp = new SalaryTableDetailReport();
                            salarytable_rp.IsDeleted = false;
                            salarytable_rp.AssignedUserId = WebSecurity.CurrentUserId;
                            salarytable_rp.CreatedUserId = WebSecurity.CurrentUserId;
                            salarytable_rp.CreatedDate = DateTime.Now;
                            salarytable_rp.ModifiedUserId = WebSecurity.CurrentUserId;
                            salarytable_rp.ModifiedDate = DateTime.Now;

                            salarytable_rp.StaffId = listIdStaff[model.SalaryTableData.Rows.IndexOf(row)];
                            salarytable_rp.ColumName = colname;
                            salarytable_rp.Value = row[col.ColumnName].ToString();
                            salarytable_rp.DataType = col.DataType.Name.ToLower();
                            salarytable_rp.SalaryTableId = salaryTable.Id;

                            //Insert và Update
                            salaryTableDetailReportRepository.InsertSalaryTableDetailReport(salarytable_rp);

                            #region tạo chi tiết phiếu chi tiền lương cho từng nhân viên

                            if (salarytable_rp.ColumName == "Họ và tên")
                            {
                                model_Payment.TargetId = listIdStaff[model.SalaryTableData.Rows.IndexOf(row)];
                                var staff = staffsRepository.GetvwStaffsById(model_Payment.TargetId.Value);
                                model_Payment.TargetName = "Staffs";
                                model_Payment.Name = "Chi trả lương tháng " + salaryTable.TargetMonth + "/" + salaryTable.TargetYear;
                                model_Payment.PaymentMethod = "Tiền mặt";
                                model_Payment.Receiver = row[col.ColumnName].ToString();
                                model_Payment.VoucherDate = DateTime.Now;
                                model_Payment.Address = staff.Address + ", " + staff.WardName + ", " + staff.DistrictName + ", " + staff.ProvinceName;
                                model_Payment.BranchId = staff.Sale_BranchId;

                            }
                            if (salarytable_rp.ColumName == "Thực lĩnh")
                            {
                                model_Payment.Amount = Convert.ToDouble(row[col.ColumnName].ToString());
                            }
                            if (list_category.Where(x => x.Name == colname).Count() > 0)
                            {
                                var GroupName = "";
                                var ParentId = model.ListAllColumns.SingleOrDefault(w => w.Id == int.Parse(col.Caption)).ParentId;
                                if (ParentId != null)
                                {
                                    GroupName = model.ListAllColumns.SingleOrDefault(w => w.Id == ParentId).Name;
                                }
                                var model_PaymentDetail = new PaymentDetail();
                                model_PaymentDetail.IsDeleted = false;
                                model_PaymentDetail.AssignedUserId = WebSecurity.CurrentUserId;
                                model_PaymentDetail.CreatedUserId = WebSecurity.CurrentUserId;
                                model_PaymentDetail.CreatedDate = DateTime.Now;
                                model_PaymentDetail.ModifiedUserId = WebSecurity.CurrentUserId;
                                model_PaymentDetail.ModifiedDate = DateTime.Now;
                                model_PaymentDetail.Amount = Convert.ToDouble(row[col.ColumnName].ToString());
                                model_PaymentDetail.Name = colname;
                                if (!string.IsNullOrEmpty(GroupName))
                                {
                                    model_PaymentDetail.GroupName = GroupName;
                                }
                                ListPaymentDetail.Add(model_PaymentDetail);
                            }
                            #endregion
                        }
                        #region tạo phiếu chi tiền lương cho từng nhân viên
                        paymentRepository.InsertPayment(model_Payment);
                        var prefix = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_PaymentSupplier");
                        model_Payment.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix, model_Payment.Id);
                        paymentRepository.UpdatePayment(model_Payment);
                        foreach (var item in ListPaymentDetail)
                        {
                            item.PaymentId = model_Payment.Id;
                            paymentDetailRepository.InsertPaymentDetail(item);
                        }
                        #endregion
                    }


                    //Lưu bảng dự toán lương cho từng nhân sự

                    //Cập nhật lại bảng lương
                    salaryTable.ModifiedUserId = WebSecurity.CurrentUserId;
                    salaryTable.ModifiedDate = DateTime.Now;
                    salaryTable.ListSalarySettingDetail = getListSalarySettingDetail(salaryTable.SalarySettingId.Value);
                    salaryTable.Status = "Đã khóa";
                    salaryTable.TotalSalary = total;
                    SalaryTableRepository.UpdateSalaryTable(salaryTable);

                    return RedirectToAction("Detail", new { Id = model.Id });
                }

                return View(model);
            }

            return View(model);
        }

        #endregion

        #region Detail
        public ActionResult Detail(int? Id, int? Year, int? BranchId)
        {
            ViewBag.Error = null;
            var salaryTable = new SalaryTable();
            if (Id == null)
            {
                salaryTable = SalaryTableRepository.GetAllSalaryTable()
                   .Where(item => item.BranchId == BranchId && item.TargetYear == Year).FirstOrDefault();
            }
            else
            {
                salaryTable = SalaryTableRepository.GetSalaryTableById(Id.Value);
            }

            if (salaryTable != null && salaryTable.IsDeleted != true)
            {
                if (("," + Helpers.Common.CurrentUser.DrugStore + ",").Contains("," + salaryTable.BranchId + ",") == true || (Helpers.Common.CurrentUser.UserTypeId == 1 || Helpers.Common.CurrentUser.UserTypeId == 21))
                {
                    //Được quyền truy cập nếu dữ liệu của đơn vị mình, hoặc là admin, hoặc là PGD
                }
                else
                {
                    TempData["FailedMessage"] = "NotOwner";
                    return RedirectToAction("Index");
                }

                var model = new SalaryTableViewModel();
                AutoMapper.Mapper.Map(salaryTable, model);

                decimal total = 0;
                if (salaryTable.Status == App_GlobalResources.Wording.SalaryTableStatus_InProcess)
                {
                    //up

                    model.ListAllColumns = Helpers.Common.ByteArrayToObject(getListSalarySettingDetail(salaryTable.SalarySettingId.Value)) as List<SalarySettingDetailViewModel>;
                    //Tạo bảng tính lương
                    DataTable dataLink = new DataTable();
                    model.SalaryTableData = buildSalaryTable(model.ListAllColumns, salaryTable.Id, salaryTable.TargetYear.Value, salaryTable.TargetMonth.Value, ref total, ref dataLink);
                    model.SalaryTableLink = dataLink;
                }
                else
                {
                    #region demo
                    //int colDefault = 1;
                    model.ListAllColumns = Helpers.Common.ByteArrayToObject(salaryTable.ListSalarySettingDetail) as List<SalarySettingDetailViewModel>;
                    var list = salaryTableDetailReportRepository.GetSalaryTableDetailReportBySalaryTableId(salaryTable.Id).OrderBy(n => n.Id).ToList();
                    int numberOfColumns = model.ListAllColumns.Where(item => item.IsGroup == false).Count();
                    DataTable table = new DataTable();
                    table.Columns.Add("OrderNo", typeof(string));
                    for (int i = 0; i < numberOfColumns; i++)
                    {
                        table.Columns.Add(model.ListAllColumns[i].Name, typeof(string));
                    }
                    table.Columns.Add("#", typeof(string));
                    table.Columns.Add("Trạng thái", typeof(string));
                    //Số dòng cần insert
                    object[] values = new object[table.Columns.Count];
                    int itemIndex = -1;
                    //add dữ liệu vào colum
                    try
                    {
                        for (int i = 0; i < list.Count(); i++)
                        {

                            //nếu mà đủ 20 cột thì reset + add
                            if (itemIndex == -1 || itemIndex < numberOfColumns)
                                itemIndex++;
                            else
                            {
                                values[numberOfColumns + 1] = "";
                                values[numberOfColumns + 2] = "";
                                //gán nội dung cột vào datatable
                                //values[numberOfColumns + 1] = Payment_Status(salaryTable.TargetMonth.Value, salaryTable.TargetYear.Value, list[i - 1].StaffId.Value);
                                //values[numberOfColumns + 2] = Payment_Action(salaryTable.TargetMonth.Value, salaryTable.TargetYear.Value, list[i - 1].StaffId.Value);
                                itemIndex = 0;
                                table.Rows.Add(values);
                            }
                            values[itemIndex] = list[i].Value;

                            if (list.IndexOf(list[i]) >= list.Count - 1)
                            {
                                values[numberOfColumns + 1] = "";
                                values[numberOfColumns + 2] = "";
                                //gán nội dung cột vào datatable
                                //values[numberOfColumns + 1] = Payment_Status(salaryTable.TargetMonth.Value, salaryTable.TargetYear.Value, list[i - 1].StaffId.Value);
                                //values[numberOfColumns + 2] = Payment_Action(salaryTable.TargetMonth.Value, salaryTable.TargetYear.Value, list[i - 1].StaffId.Value);
                                table.Rows.Add(values);
                            }
                        }

                    }
                    catch (Exception)
                    {
                        ViewBag.Error = "Đã xãy ra lỗi ở bảng lương bạn vừa khóa, vui lòng xóa bảng lương này, và tạo một bảng lương mới để đồng bộ dữ liệu cho các trường, các cột.";
                        return View(model);
                    }

                    model.SalaryTableData = table;
                    #endregion
                }

                ViewBag.SuccessMessage = TempData["SuccessMessage"];
                ViewBag.FailedMessage = TempData["FailedMessage"];
                ViewBag.AlertMessage = TempData["AlertMessage"];

                var branch = branchRepository.GetBranchById(salaryTable.BranchId.Value);
                ViewBag.BranchName = branch.Name;

                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        DataTable buildSalaryTable(List<SalarySettingDetailViewModel> ListAllColumns, int salaryTableId, int Year, int Month, ref decimal total, ref DataTable dataLink)
        {
            //Build struct table
            DataTable dt = buildSalaryTableStruct(ListAllColumns);
            dataLink = dt.Clone();

            foreach (DataColumn item in dataLink.Columns)
            {
                item.DataType = typeof(string);
            }

            //Lấy danh sách nhân viên của phòng ban cần tính lương - danh sách tính lương của người thuộc mãu bản lương
            var listIdStaff = SalaryTableDetail_StaffRepository.GetAllSalaryTableDetail_Staff().Where(n => n.SalaryTableId == salaryTableId).Select(n => n.StaffId.Value).ToList();

            //MODEL
            var staffModel = staffsRepository.GetvwAllStaffs();
            var salarySettingModel = salarySettingDetail_StaffRepository.GetAllSalarySettingDetail_Staff();
            var timekeepingSynthesisModel = timekeepingSynthesisRepository.GetAllTimekeepingSynthesis();
            var salaryProcessPayModel = processPayRepository.GetAllProcessPay();
            var salaryAdvanceModel = SalaryAdvanceRepository.GetAllSalaryAdvance();

            //var commercialCreditDetailModel = commercialCreditDetailRepository.GetViewAlls();
            //var salarySeniorityModel = salarySeniorityRepository.GetAllSalarySeniority();

            var listStaff = staffsRepository.GetvwAllStaffs().Where(n => listIdStaff.Contains(n.Id))
                .Select(item => new
                {
                    Id = item.Id,
                    Name = item.Name,
                    BranchId = item.Sale_BranchId,
                }).OrderBy(n => n.Id).ToList();

            var listColumnsNogroup = ListAllColumns.Where(item => !item.IsGroup).ToList();
            var salaryTable = SalaryTableRepository.GetSalaryTableById(salaryTableId);

            int OrderNo = 1;

            foreach (var staff in listStaff)
            {
                //dữ liệu lương data_staff
                //var du_lieu_luong_data_staff = salaryTableDetailReportRepository.GetSalaryTableDetailReportByStaffId(staff.Id).ToList();

                //Lấy dữ liệu lương của nhân viên
                var du_lieu_luong_cua_nhan_vien = salarySettingModel
                                .Where(item => item.StaffId == staff.Id).ToList();

                //Lấy dữ liệu chấm công của nhân viên
                var du_lieu_cham_cong_cua_nhan_vien = timekeepingSynthesisModel
                    .Where(item => item.StaffId == staff.Id && item.Year == Year && item.Month == Month).FirstOrDefault();

                //Lấy dữ liệu bậc lương theo tháng năm
                var du_lieu_bac_luong = salaryProcessPayModel
                   .Where(item => item.StaffId == staff.Id && item.DayEffective.Value.Year <= Year && item.DayEffective.Value.Month <= Month)
                   .OrderByDescending(n => n.DayEffective.Value.Month).ThenByDescending(n => n.DayEffective.Value.Year).FirstOrDefault();

                //Lấy dữ liệu tạm ứng lương theo tháng năm
                var tamp = salaryAdvanceModel
                    .Where(item => item.StaffId == staff.Id && item.DayAdvance.Value.Year == Year && item.DayAdvance.Value.Month == Month && item.Status.Contains("Đã trả tiền")).ToList();
                SalaryAdvance du_lieu_ung_luong;
                if (tamp.Count > 0)
                {
                    du_lieu_ung_luong = tamp
                       .GroupBy(x => x.StaffId).Select(item => new SalaryAdvance
                       {
                           StaffId = item.Key,
                           Pay = item.Sum(s => s.Pay)
                       }).FirstOrDefault();
                }
                else
                {
                    du_lieu_ung_luong = null;
                }

                //Lấy dữ liệu Khen - thưởng
                var du_lieu_khen_thuong_full = bonusDisciplineRepository.GetAllBonusDiscipline()
                    .Where(item => item.StaffId == staff.Id && item.DayEffective.Value.Year == Year && item.DayEffective.Value.Month == Month);
                //- Phạt
                var du_lieu_phat = new PhatModel();
                if (du_lieu_khen_thuong_full != null)
                {
                    var model = du_lieu_khen_thuong_full.Where(n => n.Category == "Discipline").FirstOrDefault();
                    if (model != null)
                    {
                        du_lieu_phat.StaffId = model.StaffId;
                        du_lieu_phat.MoneyPhat = model.Money;
                        du_lieu_phat.Id = model.Id;
                    }
                }
                    //AutoMapper.Mapper.Map(du_lieu_khen_thuong_full.Where(n => n.Code != "Bonus").FirstOrDefault(), du_lieu_phat);
                //-Khen thưởng
                var du_lieu_khen_thuong = new KhenThuongModel();
                if (du_lieu_khen_thuong_full != null)
                {
                    var model = du_lieu_khen_thuong_full.Where(n => n.Category == "Bonus").FirstOrDefault();
                    if (model != null)
                    {
                        du_lieu_khen_thuong.StaffId = model.StaffId;
                        du_lieu_khen_thuong.MoneyKhenThuong = model.Money;
                        du_lieu_khen_thuong.Id = model.Id;
                    }
                }
                    //AutoMapper.Mapper.Map(du_lieu_khen_thuong_full.Where(n => n.Code == "Bonus").FirstOrDefault(), du_lieu_khen_thuong);

                //Dữ liệu Phụ cấp
                var du_lieu_phu_cap = staffAllowanceRepository.GetAllStaffAllowance().Where(n => n.TargetMonth == Month && n.TargetYear == Year && n.StaffId == staff.Id).FirstOrDefault();

                //Dữ liệu giữ theo hợp đồng
                var du_lieu_giu_theo_hop_dong = staffLuongGiuHopDongRepository.GetAllStaffLuongGiuHopDong().Where(n => n.TargetMonth == Month && n.TargetYear == Year && n.StaffId == staff.Id).FirstOrDefault();


                //-> Nếu không có bậc lương thì ẩn đi
                if (du_lieu_bac_luong == null)
                    continue;


                //Lấy dữ ngân hàng
                var du_lieu_ngan_hang = bankRepository.GetAllBank()
                    .Where(n => n.StaffId == staff.Id).FirstOrDefault();

                ////Lấy dữ liệu bậc lương tham niên
                //var du_lieu_tham_nien = salarySeniorityModel
                //   .Where(item => item.StaffId == staff.Id && item.NgayTinhHuong.Value.Year <= Year && item.NgayTinhHuong.Value.Month <= Month)
                //   .OrderByDescending(n => n.NgayTinhHuong.Value.Month).ThenByDescending(n => n.NgayTinhHuong.Value.Year).FirstOrDefault();

                //Lây dữ liệu của nhân viên
                var du_lieu_nhan_vien = staffModel
                    .SingleOrDefault(item => item.Id == staff.Id);


                //Lấy dử liệu hệ thống
                var du_lieu_setting = settingRepository.GetAlls().Where(item => item.Code == "staff_salary")
                    .OrderBy(item => item.Note).ToList();

                DataRow row = dt.NewRow();
                DataRow row_link = dataLink.NewRow();
                string fontlink = "";
                foreach (DataColumn col in dt.Columns)
                {
                    // SalaryTableDetailReport salarytable_rp = new SalaryTableDetailReport();

                    string columnName = col.ColumnName;
                    switch (columnName)
                    {
                        case "OrderNo":
                            row[columnName] = OrderNo;
                            row_link[columnName] = Wording.NoneSelect;
                            break;
                        default:
                            var salarySettingDetail = ListAllColumns.Where(u => u.Id == Convert.ToInt32(col.ColumnName)).FirstOrDefault();
                            //Nếu là loại giá trị
                            if (salarySettingDetail.FormulaType == App_GlobalResources.Wording.FormulaType_Default)
                            {
                                //Đây là trường hợp lấy dữ liệu chung
                                if (salarySettingDetail.IsDefaultValue)
                                {
                                    row[col.ColumnName] = salarySettingDetail.DefaultValue;
                                }
                                else
                                {
                                    //Còn đây là dữ liệu của cá nhân
                                    var value = du_lieu_luong_cua_nhan_vien.Where(u => u.SalarySettingDetailId == salarySettingDetail.Id).FirstOrDefault();
                                    if (value != null)
                                    {
                                        row[col.ColumnName] = value.DefaultValue;
                                    }
                                    else
                                    {
                                        row[col.ColumnName] = 0;
                                    }
                                }
                            }
                            else
                            {
                                var buildExp = BuildFormula(
                                    listColumnsNogroup,
                                    du_lieu_setting,
                                    salarySettingDetail.Formula,
                                    ref fontlink,
                                    false,
                                    salarySettingDetail.IsChange,
                                    row,
                                    du_lieu_cham_cong_cua_nhan_vien,
                                    du_lieu_nhan_vien,
                                    du_lieu_bac_luong,
                                    du_lieu_ung_luong,
                                    du_lieu_ngan_hang,
                                    du_lieu_khen_thuong,
                                    du_lieu_phat,
                                    du_lieu_phu_cap,
                                    du_lieu_giu_theo_hop_dong
                                    );

                                //Ngược lại là loại công thức
                                Expression e = new Expression(buildExp);
                                if (e.checkSyntax())
                                {
                                    var value = e.calculate();
                                    if (value > 0)
                                    {
                                        row[col.ColumnName] = Convert.ToDecimal(e.calculate());
                                    }
                                    else
                                    {
                                        row[col.ColumnName] = 0;
                                    }
                                }
                                else
                                {
                                    if (col.DataType == typeof(string))
                                    {
                                        if (string.IsNullOrEmpty(buildExp))
                                            row[col.ColumnName] = "";
                                        else
                                            row[col.ColumnName] = buildExp;
                                    }
                                    else
                                        row[col.ColumnName] = 0;
                                }
                            }

                            //Tính tổng
                            if (salarySettingDetail.IsSum)
                            {
                                string rowValue = Convert.ToString(row[col.ColumnName]);
                                if (!string.IsNullOrEmpty(rowValue))
                                {
                                    decimal lastValue = Decimal.Parse(rowValue.Replace(",", "."), new CultureInfo("en-GB"));
                                    total += lastValue;
                                }
                            }
                            if(salarySettingDetail.IsChange!=null && salarySettingDetail.IsChange.Value)
                            {
                                //replace PhatModel, KhenThuongModel
                                fontlink = fontlink.Replace(typeof(PhatModel).Name, "BonusDiscipline");
                                fontlink = fontlink.Replace(typeof(KhenThuongModel).Name, "BonusDiscipline");
                                row_link[col.ColumnName] =string.Format("{0}:{1}", staff.Id,  fontlink);
                            }

                            break;
                    }

                }

                dt.Rows.Add(row);
                dataLink.Rows.Add(row_link);

                OrderNo++;
            }

            return dt;
        }

        public enum DataTypeString
        {
            [Display(Name = "string")]
            DataString,
            [Display(Name = "int32")]
            DataInt32,
            [Display(Name = "decimal")]
            DataDecimal,
            [Display(Name = "double")]
            DataDouble
        }


        DataTable buildSalaryTableStruct(List<SalarySettingDetailViewModel> ListAllColumns)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("OrderNo");

            foreach (var group in ListAllColumns.Where(g => g.ParentId == null).OrderBy(g => g.OrderNo))
            {
                var subList = ListAllColumns.Where(i => i.ParentId == group.Id).OrderBy(i => i.OrderNo).ToList();
                if (subList.Count > 0)
                {
                    foreach (var item in subList)
                    {
                        dt.Columns.Add(item.Id.ToString(), typeof(decimal));
                    }
                }
                else
                {
                    if (group.DataType == DataTypeString.DataString.GetName())
                        dt.Columns.Add(group.Id.ToString(), typeof(string));
                    else
                        dt.Columns.Add(group.Id.ToString(), typeof(decimal));
                }
            }

            return dt;
        }

        //Dữ liệu chấm công của nhân viên 


        public static void DienDuLieuChamCong<T>(ref string formula, ref string folink, T model, bool isCheck, bool? IsChange) where T : class, new()
        {
            //string Id = "";
            string _formula_temp = formula;
            if (model == null)
                model = new T();
            var pi = model.GetType().GetProperties();

            foreach (var p in pi)
            {
                if (!Helpers.Common.fieldsNoReplace.Contains(p.Name))
                {
                    if (isCheck)
                    {
                        replaceFormulaColumn(ref formula, p.Name, "1");
                    }
                    else
                    {
                        var value = p.GetValue(model);
                        if (value == null)
                        {
                            value = "0";
                        }
                        replaceFormulaColumn(ref formula, p.Name, value.ToString());
                        if (string.IsNullOrEmpty(_formula_temp.Replace("[" + p.Name + "]", "")))
                        {
                            var Id = pi.Where(n => n.Name.Equals("Id")).FirstOrDefault().GetValue(model);
                            folink = string.Format("{0}:{1}", model.GetType().Name, Id);
                        }
                    }
                }
               
            }
        }

        public static bool IsNumber(string pText)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(pText);
        }

        public static bool CongThucLaDuLieuLuong(string formula)
        {
            var value = Regex.Matches(formula.Replace("[", "{").Replace("]", "}"), @"{.*?}", RegexOptions.Singleline);
            //Kiểm coi công thức có lấy dữ liệu từ bảng lương không?
            for (int i = 0; i < value.Count; i++)
            {
                var _check = value[i].Value.Replace("{", "").Replace("}", "").Split('|');
                if (_check.Length > 1 && IsNumber(_check[0]))
                    return true;
            }
            return false;
        }

        public static string BuildFormula(
            List<SalarySettingDetailViewModel> ListAllColumns, 
            List<Setting> du_lieu_setting, 
            string formula, 
            ref string folink, 
            bool isCheck = false, 
            bool? IsChange = false,
            DataRow du_lieu_tinh_luong_da_xu_ly = null, 
            TimekeepingSynthesis du_lieu_cham_cong_cua_nhan_vien = null, 
            vwStaffs du_lieu_nhan_vien = null,
            ProcessPay du_lieu_bac_luong = null, 
            SalaryAdvance du_lieu_ung_luong = null,
            Bank du_lieu_ngan_hang = null,
            KhenThuongModel du_lieu_khen_thuong = null,
            PhatModel du_lieu_phat = null,
            StaffAllowance du_lieu_phu_cap = null,
            StaffLuongGiuHopDong du_lieu_giu_luong_theo_hd = null
            )
        {

            //Điền dữ liệu hệ thống
            //Điền dữ liệu của các cột đã xử lý
            foreach (var item in ListAllColumns)
            {
                if (isCheck)
                {
                    replaceFormulaColumn(ref formula, item.Name, "1");
                }
                else
                {
                    var value = Convert.ToString(du_lieu_tinh_luong_da_xu_ly[item.Id.ToString()]);
                    if (string.IsNullOrEmpty(value))
                    {
                        value = "0";
                    }

                    replaceFormulaColumn(ref formula, item.Name, value);
                }
            }

            //Điền dữ liệu chấm công của nhân viên
            DienDuLieuChamCong<TimekeepingSynthesis>(ref formula, ref folink, du_lieu_cham_cong_cua_nhan_vien, isCheck, IsChange);

            //Điền dử liệu bậc lương
            DienDuLieuChamCong<ProcessPay>(ref formula, ref folink, du_lieu_bac_luong, isCheck, IsChange);
            DienDuLieuChamCong<SalaryAdvance>(ref formula, ref folink, du_lieu_ung_luong, isCheck, IsChange);

            //Điền dữ liệu ngân hàng
            DienDuLieuChamCong<Bank>(ref formula, ref folink, du_lieu_ngan_hang, isCheck, IsChange);

            //Điền dữ liệu nhân viên
            DienDuLieuChamCong<vwStaffs>(ref formula, ref folink, du_lieu_nhan_vien, isCheck, IsChange);

            //Điền dữ liệu khen thưởng
            DienDuLieuChamCong<KhenThuongModel>(ref formula, ref folink, du_lieu_khen_thuong, isCheck, IsChange);

            //Điền dữ liệu phạt
            DienDuLieuChamCong<PhatModel>(ref formula, ref folink, du_lieu_phat, isCheck, IsChange);

            //Điền dữ giữ lương theo hợp đồng
            DienDuLieuChamCong<StaffLuongGiuHopDong>(ref formula, ref folink, du_lieu_giu_luong_theo_hd, isCheck, IsChange);
            
            //Điền dữ phụ cấp
            DienDuLieuChamCong<StaffAllowance>(ref formula, ref folink, du_lieu_phu_cap, isCheck, IsChange);


            //Điền dữ liệu hệ thống
            if (du_lieu_setting == null && du_lieu_setting.Count == 0)
                return formula.Replace(",", ".");
            foreach (var item in du_lieu_setting)
            {
                if (isCheck)
                {
                    replaceFormulaColumn(ref formula, item.Note, "1");
                }
                else
                {
                    var value = Convert.ToString(item.Value);
                    if (string.IsNullOrEmpty(value))
                    {
                        value = "0";
                    }

                    replaceFormulaColumn(ref formula, item.Note, value);
                }
            }


            formula = formula.Replace(",", ".");
            return formula;
        }

        public static void replaceFormulaColumn(ref string input, string columnName, string value)
        {
            input = input.Replace("[" + columnName + "]", value);
        }

        public static string Payment_Action(int TargetMonth, int TargetYear, int StaffId)
        {

            string Action = "";
            Erp.Domain.Account.Repositories.PaymentRepository paymentRepository = new Erp.Domain.Account.Repositories.PaymentRepository(new Domain.Account.ErpAccountDbContext());
            var name = "Chi trả lương tháng " + TargetMonth + "/" + TargetYear;
            var payment = paymentRepository.GetvwPaymentByTagetId(StaffId, "Staffs", name);
            if (payment.IsArchive)
            {
                string UnArchive = "<a class=\"btn btn-danger btn-mini\" title=\"Bỏ đã chi\" onclick=\"UnArchive(" + StaffId + "," + TargetMonth + "," + TargetYear + ")\"><i class=\"ace-icon fa fa-remove\"></i></a>";
                Action += UnArchive;
            }
            else
            {
                string Archive = "<a class=\"btn btn-success btn-mini\" title=\"Đã chi\" onclick=\"Archive(" + StaffId + "," + TargetMonth + "," + TargetYear + ")\"><i class=\"ace-icon fa fa-check\"></i></a>";
                Action += Archive;
            }
            string inphieu = "<a class=\"btn btn-primary btn-mini\" title=\"In phiếu chi\" href=\"/Payment/Print/?TargetId=" + StaffId + "&Month=" + TargetMonth + "&Year=" + TargetYear + "\"><i class=\"ace-icon fa fa-print\"></i></a>";
            Action += inphieu;
            return Action;
        }
        public static string Payment_Status(int TargetMonth, int TargetYear, int StaffId)
        {
            string Status = "";
            Erp.Domain.Account.Repositories.PaymentRepository paymentRepository = new Erp.Domain.Account.Repositories.PaymentRepository(new Domain.Account.ErpAccountDbContext());
            var name = "Chi trả lương tháng " + TargetMonth + "/" + TargetYear;
            var payment = paymentRepository.GetvwPaymentByTagetId(StaffId, "Staffs", name);

            Status = payment.IsArchive ? "<span style=\"color:green\"><b>Đã chi</b></span>" : "<span style=\"color:red\"><b>Chưa chi</b></span>";
            return Status;
        }
        #endregion

        #region Export

        [HttpPost]
        public ActionResult Export(int Id)
        {
            var salaryTable = SalaryTableRepository.GetSalaryTableById(Id);
            if (salaryTable != null && salaryTable.IsDeleted != true)
            {
                var ListAllColumns = Helpers.Common.ByteArrayToObject(salaryTable.ListSalarySettingDetail) as List<SalarySettingDetailViewModel>;

                var list = salaryTableDetailReportRepository.GetAllSalaryTableDetailReport()
                                .Where(item => item.SalaryTableId == salaryTable.Id).ToList();

                int numberOfColumns = ListAllColumns.Where(item => item.IsGroup == false).Count() + 1;
                DataTable table = new DataTable();
                for (int i = 1; i <= numberOfColumns; i++)
                {
                    table.Columns.Add("Col" + i);
                }

                object[] values = new object[table.Columns.Count];

                int itemIndex = -1;
                //add dữ liệu vào colum
                foreach (var item in list)
                {
                    //nếu mà đủ 20 cột thì reset + add
                    if (itemIndex == -1 || itemIndex < numberOfColumns - 1)
                        itemIndex++;

                    else
                    {
                        itemIndex = 0;
                        table.Rows.Add(values);
                    }
                    values[itemIndex] = item.Value;

                }


                var title = salaryTable.Name + " " + (salaryTable.HiddenForMonth ? "" : (salaryTable.TargetMonth.ToString() + "/")) + salaryTable.TargetYear.ToString();
                //export excel
                // Gọi lại hàm để tạo file excel
                var stream = CreateExcelFile(title, ListAllColumns, table);
                // Tạo buffer memory strean để hứng file excel
                var buffer = stream as MemoryStream;
                // Đây là content Type dành cho file excel, còn rất nhiều content-type khác nhưng cái này mình thấy okay nhất
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
                // File name của Excel này là ExcelDemo
                string fileName = title + ".xlsx";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
                // Lưu file excel của chúng ta như 1 mảng byte để trả về response
                Response.BinaryWrite(buffer.ToArray());
                // Send tất cả ouput bytes về phía clients
                Response.Flush();
                Response.End();
            }

            return RedirectToAction("Detail", new { Id = salaryTable.Id });
        }

        private Stream CreateExcelFile(string title, List<SalarySettingDetailViewModel> columns, DataTable dt, Stream stream = null)
        {
            using (var excelPackage = new ExcelPackage(stream ?? new MemoryStream()))
            {
                // Add Sheet vào file Excel
                excelPackage.Workbook.Worksheets.Add("First Sheet");
                // Lấy Sheet bạn vừa mới tạo ra để thao tác 
                var worksheet = excelPackage.Workbook.Worksheets[1];
                // Đổ data vào Excel file
                // Set default width cho tất cả column
                worksheet.DefaultColWidth = 15;
                // Tự động xuống hàng khi text quá dài
                worksheet.Cells.Style.WrapText = true;

                //Tiêu đề báo cáo
                worksheet.Cells[1, 1].Value = title;
                var rangeTitle = worksheet.Cells[1, 1, 1, columns.Count - 1];
                rangeTitle.Merge = true;
                rangeTitle.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                rangeTitle.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                rangeTitle.Style.Font.SetFromFont(new Font("Arial", 18, FontStyle.Bold));

                int rowHeader1 = 2;
                int rowHeader2 = rowHeader1 + 1;
                int rowStartItem = rowHeader2 + 1;

                // Tạo header
                worksheet.Cells[rowHeader1, 1].Value = "STT";
                worksheet.Cells[rowHeader1, 1, rowHeader2, 1].Merge = true;
                formatExcelRange(worksheet.Cells[rowHeader1, 1, rowHeader2, 1]);

                //worksheet.Cells[rowHeader1, 2].Value = "Tên giáo viên";
                //worksheet.Cells[rowHeader1, 2, rowHeader2, 2].Merge = true;
                //formatExcelRange(worksheet.Cells[rowHeader1, 2, rowHeader2, 2]);

                //worksheet.Cells[rowHeader1, 3].Value = "Mã ngạch lương";
                //worksheet.Cells[rowHeader1, 3, rowHeader2, 3].Merge = true;
                //formatExcelRange(worksheet.Cells[rowHeader1, 3, rowHeader2, 3]);

                //worksheet.Cells[rowHeader1, 4].Value = "Chức vụ";
                //worksheet.Cells[rowHeader1, 4, rowHeader2, 4].Merge = true;
                //formatExcelRange(worksheet.Cells[rowHeader1, 4, rowHeader2, 4]);

                int nCol = 2;
                int numberOfColumn = columns.Count + 2;
                foreach (var group in columns.Where(g => g.ParentId == null && g.IsDisplay).OrderBy(g => g.OrderNo))
                {
                    ExcelRange excelRange = null;
                    if (group.HasSubList)
                    {
                        worksheet.Cells[rowHeader1, nCol].Value = group.Name;
                        excelRange = worksheet.Cells[rowHeader1, nCol, rowHeader1, nCol + group.NumberOfSubList - 1];
                        excelRange.Merge = true;

                        int nSubCol = 0;
                        foreach (var item in columns.Where(i => i.ParentId == group.Id && i.IsDisplay).OrderBy(i => i.OrderNo))
                        {
                            worksheet.Cells[rowHeader2, nCol + nSubCol].Value = item.Name;
                            formatExcelRange(worksheet.Cells[rowHeader2, nCol + nSubCol]);
                            nSubCol++;
                        }

                        nCol += (group.NumberOfSubList - 1);
                    }
                    else
                    {
                        worksheet.Cells[rowHeader1, nCol].Value = group.Name;
                        excelRange = worksheet.Cells[rowHeader1, nCol, rowHeader2, nCol];
                        excelRange.Merge = true;
                    }

                    //Định dạng ô
                    formatExcelRange(excelRange);

                    nCol++;
                }

                // Đỗ dữ liệu từ list vào 
                int nRow = rowStartItem;
                foreach (DataRow row in dt.Rows)
                {
                    int i = 1;
                    foreach (DataColumn col in dt.Columns)
                    {
                        row[col.ColumnName] = row[col.ColumnName].ToString().Replace(",", ".");
                        worksheet.Cells[nRow, i].Value = row[col.ColumnName];
                        worksheet.Cells[nRow, i].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                        i++;
                    }

                    nRow++;
                }

                excelPackage.Save();
                return excelPackage.Stream;
            }
        }

        void formatExcelRange(ExcelRange excelRange)
        {
            excelRange.Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
            excelRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
            excelRange.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
            excelRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            excelRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            excelRange.Style.Font.SetFromFont(new Font("Arial", 10, FontStyle.Bold));
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
                string idDeleteAll = Request["DeleteId-checkbox"];
                string[] arrDeleteId = idDeleteAll.Split(',');
                for (int i = 0; i < arrDeleteId.Count(); i++)
                {
                    var item = SalaryTableRepository.GetSalaryTableById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        //if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        //{
                        //    TempData["FailedMessage"] = "NotOwner";
                        //    return RedirectToAction("Index");
                        //}

                        item.IsDeleted = true;
                        SalaryTableRepository.UpdateSalaryTable(item);
                        SalaryTableDetail_StaffRepository.DeleteSalaryTableDetail_StaffBySalaryTableId(item.Id);
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
