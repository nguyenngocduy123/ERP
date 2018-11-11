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
using Erp.Domain.Sale.Interfaces;
using Erp.BackOffice.Sale.Models;
using System.Web;
using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Repositories;
using Erp.Domain.Sale;
using Erp.Domain.Staff.Repositories;
using Erp.Domain.Staff;

namespace Erp.BackOffice.Staff.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class HistoryCommissionStaffController : Controller
    {
        private readonly IHistoryCommissionStaffRepository HistoryCommissionStaffRepository;
        private readonly IUserRepository userRepository;
        private readonly IProductInvoiceRepository productInvoiceRepository;
        private readonly IStaffsRepository staffRepository;
        private readonly ITemplatePrintRepository templatePrintRepository;
        public HistoryCommissionStaffController(
            IHistoryCommissionStaffRepository _HistoryCommissionStaff
            , IUserRepository _user
            , IProductInvoiceRepository invoice
            , IStaffsRepository staff
            , ITemplatePrintRepository templatePrint
            )
        {
            HistoryCommissionStaffRepository = _HistoryCommissionStaff;
            userRepository = _user;
            staffRepository = staff;
            productInvoiceRepository = invoice;
            templatePrintRepository = templatePrint;
        }

        //#region Index

        //public ViewResult Index(string txtSearch)
        //{

        //    IQueryable<HistoryCommissionStaffViewModel> q = HistoryCommissionStaffRepository.GetAllHistoryCommissionStaff()
        //        .Select(item => new HistoryCommissionStaffViewModel
        //        {
        //            Id = item.Id,
        //            CreatedUserId = item.CreatedUserId,
        //            //CreatedUserName = item.CreatedUserName,
        //            CreatedDate = item.CreatedDate,
        //            ModifiedUserId = item.ModifiedUserId,
        //            //ModifiedUserName = item.ModifiedUserName,
        //            ModifiedDate = item.ModifiedDate,
        //            Name = item.Name,
        //        }).OrderByDescending(m => m.ModifiedDate);

        //    ViewBag.SuccessMessage = TempData["SuccessMessage"];
        //    ViewBag.FailedMessage = TempData["FailedMessage"];
        //    ViewBag.AlertMessage = TempData["AlertMessage"];
        //    return View(q);
        //}
        //#endregion

        #region Create
        public ViewResult Create()
        {
            var model = new HistoryCommissionStaffViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(HistoryCommissionStaffViewModel model)
        {
            if (ModelState.IsValid)
            {
                var HistoryCommissionStaff = new HistoryCommissionStaff();
                AutoMapper.Mapper.Map(model, HistoryCommissionStaff);
                HistoryCommissionStaff.IsDeleted = false;
                HistoryCommissionStaff.CreatedUserId = WebSecurity.CurrentUserId;
                HistoryCommissionStaff.ModifiedUserId = WebSecurity.CurrentUserId;
                HistoryCommissionStaff.AssignedUserId = WebSecurity.CurrentUserId;
                HistoryCommissionStaff.CreatedDate = DateTime.Now;
                HistoryCommissionStaff.ModifiedDate = DateTime.Now;
                HistoryCommissionStaffRepository.InsertHistoryCommissionStaff(HistoryCommissionStaff);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var HistoryCommissionStaff = HistoryCommissionStaffRepository.GetHistoryCommissionStaffById(Id.Value);
            if (HistoryCommissionStaff != null && HistoryCommissionStaff.IsDeleted != true)
            {
                var model = new HistoryCommissionStaffViewModel();
                AutoMapper.Mapper.Map(HistoryCommissionStaff, model);

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
        public ActionResult Edit(HistoryCommissionStaffViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var HistoryCommissionStaff = HistoryCommissionStaffRepository.GetHistoryCommissionStaffById(model.Id);
                    AutoMapper.Mapper.Map(model, HistoryCommissionStaff);
                    HistoryCommissionStaff.ModifiedUserId = WebSecurity.CurrentUserId;
                    HistoryCommissionStaff.ModifiedDate = DateTime.Now;
                    HistoryCommissionStaffRepository.UpdateHistoryCommissionStaff(HistoryCommissionStaff);

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

        #region Detail
        public ActionResult Detail(int? Id)
        {
            var HistoryCommissionStaff = HistoryCommissionStaffRepository.GetHistoryCommissionStaffById(Id.Value);
            if (HistoryCommissionStaff != null && HistoryCommissionStaff.IsDeleted != true)
            {
                var model = new HistoryCommissionStaffViewModel();
                AutoMapper.Mapper.Map(HistoryCommissionStaff, model);

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
                    var item = HistoryCommissionStaffRepository.GetHistoryCommissionStaffById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        {
                            TempData["FailedMessage"] = "NotOwner";
                            return RedirectToAction("Index");
                        }

                        item.IsDeleted = true;
                        HistoryCommissionStaffRepository.UpdateHistoryCommissionStaff(item);
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

        #region Json
        public JsonResult Sync(int month, int year)
        {

            //lấy danh sách nhà thuốc ra để bắt đầu đồng bộ dữ liệu
            var list_staff = staffRepository.GetvwAllStaffs().Where(x => x.PositionCode == "ASM" || x.PositionCode == "TDV").ToList();
            //lấy toàn bộ danh sách hóa đơn trong 1 tháng của tất cả nhà thuốc ra để xử lý 1 lần.
            var list_productInvoice = productInvoiceRepository.GetAllvwProductInvoice().Where(x => x.Month == month && x.Year == year && x.IsArchive == true).ToList();
            //lấy list đã insert nếu có
            var list_old = HistoryCommissionStaffRepository.GetAllHistoryCommissionStaffFull().Where(x => x.Month == month && x.Year == year).ToList();
            foreach (var item in list_staff)
            {
                //lấy danh sách đơn bán hàng của nhà thuốc trong tháng
                var list_invoice_by_drugStore = list_productInvoice.Where(x => ("," + item.DrugStore + ",").Contains("," + x.BranchId.ToString() + ",") == true).ToList();

                decimal RevenueDS = list_invoice_by_drugStore.Sum(x => x.TotalAmount);
                decimal CommissionPercent = item.CommissionPercent == null ? 0 : item.CommissionPercent.Value;
                decimal MinimumRevenue = item.MinimumRevenue == null ? 0 : item.MinimumRevenue.Value;
                decimal AmountCommission = (CommissionPercent * RevenueDS) / 100;
                if (list_old.Where(x => x.StaffId == item.Id).Count() > 0)
                {
                    var update = list_old.Where(x => x.StaffId == item.Id).FirstOrDefault();
                    update.IsDeleted = MinimumRevenue > RevenueDS ? true : false;
                    update.ModifiedUserId = WebSecurity.CurrentUserId;
                    update.ModifiedDate = DateTime.Now;
                    update.StaffId = item.Id;
                    update.PositionName = item.PositionName;
                    update.Month = month;
                    update.Year = year;
                    update.CommissionPercent = CommissionPercent;
                    update.MinimumRevenue = MinimumRevenue;
                    update.RevenueDS = RevenueDS;
                    update.AmountCommission = AmountCommission;
                    update.StaffName = item.Name;
                    update.StaffParentId = item.StaffParentId;
                    HistoryCommissionStaffRepository.UpdateHistoryCommissionStaff(update);
                }
                else
                {
                    //thêm mới vào data.
                    var add = new HistoryCommissionStaff();
                    add.IsDeleted = MinimumRevenue > RevenueDS ? true : false;
                    add.CreatedUserId = WebSecurity.CurrentUserId;
                    add.ModifiedUserId = WebSecurity.CurrentUserId;
                    add.AssignedUserId = WebSecurity.CurrentUserId;
                    add.CreatedDate = DateTime.Now;
                    add.ModifiedDate = DateTime.Now;
                    add.StaffId = item.Id;
                    add.PositionName = item.PositionName;
                    add.Month = month;
                    add.Year = year;
                    add.CommissionPercent = item.CommissionPercent;
                    add.MinimumRevenue = item.MinimumRevenue;
                    add.RevenueDS = RevenueDS;
                    add.AmountCommission = AmountCommission;
                    add.StaffName = item.Name;
                    add.StaffParentId = item.StaffParentId;
                    HistoryCommissionStaffRepository.InsertHistoryCommissionStaff(add);
                }
            }
            return Json(new { Result = "success", Message = App_GlobalResources.Wording.UpdateSuccess },
                                 JsonRequestBehavior.AllowGet);
        }
        #endregion

        public ViewResult Index(int? month, int? year, string Status)
        {

            List<HistoryCommissionStaffViewModel> q = HistoryCommissionStaffRepository.GetAllHistoryCommissionStaffFull()
                .Select(item => new HistoryCommissionStaffViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Month = item.Month,
                    Year = item.Year,
                    AmountCommission = item.AmountCommission,
                    CommissionPercent = item.CommissionPercent,
                    MinimumRevenue = item.MinimumRevenue,
                    PositionName = item.PositionName,
                    RevenueDS = item.RevenueDS,
                    StaffId = item.StaffId,
                    StaffName = item.StaffName,
                    IsDeleted = item.IsDeleted,
                    StaffParentId = item.StaffParentId
                }).OrderByDescending(m => m.ModifiedDate).ToList();
            if (month != null && month.Value > 0)
            {
                q = q.Where(x => x.Month == month).ToList();
            }
            else
            {
                q = q.Where(x => x.Month == DateTime.Now.Month).ToList();
            }
            if (year != null && year.Value > 0)
            {
                q = q.Where(x => x.Year == year).ToList();
            }
            else
            {
                q = q.Where(x => x.Year == DateTime.Now.Year).ToList();
            }
            if (!string.IsNullOrEmpty(Status))
            {
                if (Status == "unsuccess")
                {
                    q = q.Where(x => x.IsDeleted == null || x.IsDeleted == true).ToList();
                }
                if (Status == "success")
                {
                    q = q.Where(x => x.IsDeleted == false).ToList();
                }
            }

            if (Erp.BackOffice.Filters.SecurityFilter.IsTrinhDuocVien())
            {
                var staff = staffRepository.GetvwStaffsByUser(WebSecurity.CurrentUserId);
                if (staff != null)
                {
                    q = q.Where(x => x.StaffId == staff.Id).ToList();
                }
                else
                {
                    q = null;
                }
            }
            if (Erp.BackOffice.Filters.SecurityFilter.IsAdminSystemManager())
            {
                var staff = staffRepository.GetvwStaffsByUser(WebSecurity.CurrentUserId);
                var item_staff = q.Where(x => x.StaffId == staff.Id).ToList();
                q = q.Where(x => x.StaffParentId != null && x.StaffParentId == staff.Id).ToList();
                q = q.Union(item_staff).OrderByDescending(x => x.ModifiedDate).ToList();
            }
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }

        public ActionResult Print(int? month, int? year, string Status, bool ExportExcel = false)
        {
            var model = new TemplatePrintViewModel();
            //lấy logo công ty
            var logo = Erp.BackOffice.Helpers.Common.GetSetting("LogoCompany");
            var company = Erp.BackOffice.Helpers.Common.GetSetting("companyName");
            var address = Erp.BackOffice.Helpers.Common.GetSetting("addresscompany");
            var phone = Erp.BackOffice.Helpers.Common.GetSetting("phonecompany");
            var fax = Erp.BackOffice.Helpers.Common.GetSetting("faxcompany");
            var bankcode = Erp.BackOffice.Helpers.Common.GetSetting("bankcode");
            var bankname = Erp.BackOffice.Helpers.Common.GetSetting("bankname");
            var ImgLogo = "<div class=\"logo\"><img src=" + logo + " height=\"73\" /></div>";
            List<HistoryCommissionStaffViewModel> q = HistoryCommissionStaffRepository.GetAllHistoryCommissionStaffFull()
             .Select(item => new HistoryCommissionStaffViewModel
             {
                 Id = item.Id,
                 CreatedUserId = item.CreatedUserId,
                 //CreatedUserName = item.CreatedUserName,
                 CreatedDate = item.CreatedDate,
                 ModifiedUserId = item.ModifiedUserId,
                 //ModifiedUserName = item.ModifiedUserName,
                 ModifiedDate = item.ModifiedDate,
                 Month = item.Month,
                 Year = item.Year,
                 AmountCommission = item.AmountCommission,
                 CommissionPercent = item.CommissionPercent,
                 MinimumRevenue = item.MinimumRevenue,
                 PositionName = item.PositionName,
                 RevenueDS = item.RevenueDS,
                 StaffId = item.StaffId,
                 StaffName = item.StaffName,
                 IsDeleted = item.IsDeleted
             }).OrderByDescending(m => m.ModifiedDate).ToList();
            if (month != null && month.Value > 0)
            {
                q = q.Where(x => x.Month == month).ToList();
            }
            else
            {
                q = q.Where(x => x.Month == DateTime.Now.Month).ToList();
            }
            if (year != null && year.Value > 0)
            {
                q = q.Where(x => x.Year == year).ToList();
            }
            else
            {
                q = q.Where(x => x.Year == DateTime.Now.Year).ToList();
            }
            if (!string.IsNullOrEmpty(Status))
            {
                if (Status == "unsuccess")
                {
                    q = q.Where(x => x.IsDeleted == null || x.IsDeleted == true).ToList();
                }
                if (Status == "success")
                {
                    q = q.Where(x => x.IsDeleted == false).ToList();
                }
            }
            if (Erp.BackOffice.Filters.SecurityFilter.IsTrinhDuocVien())
            {
                var staff = staffRepository.GetvwStaffsByUser(WebSecurity.CurrentUserId);
                if (staff != null)
                {
                    q = q.Where(x => x.StaffId == staff.Id).ToList();
                }
                else
                {
                    q = null;
                }
            }
            if (Erp.BackOffice.Filters.SecurityFilter.IsAdminSystemManager())
            {
                var staff = staffRepository.GetvwStaffsByUser(WebSecurity.CurrentUserId);
                var item_staff = q.Where(x => x.StaffId == staff.Id).ToList();
                q = q.Where(x => x.StaffParentId != null && x.StaffParentId == staff.Id).ToList();
                q = q.Union(item_staff).OrderByDescending(x => x.ModifiedDate).ToList();
            }
            //lấy template phiếu xuất.
            var template = templatePrintRepository.GetAllTemplatePrint().Where(x => x.Code.Contains("PrintHistoryCommissionStaff")).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            //truyền dữ liệu vào template.
            model.Content = template.Content;

            model.Content = model.Content.Replace("{DataTable}", buildHtmlDetailList_PrintBCNXT(q));
            model.Content = model.Content.Replace("{System.Logo}", ImgLogo);
            model.Content = model.Content.Replace("{System.CompanyName}", company);
            model.Content = model.Content.Replace("{System.AddressCompany}", address);
            model.Content = model.Content.Replace("{System.PhoneCompany}", phone);
            model.Content = model.Content.Replace("{System.Fax}", fax);
            model.Content = model.Content.Replace("{System.BankCodeCompany}", bankcode);
            model.Content = model.Content.Replace("{System.BankNameCompany}", bankname);
            model.Content = model.Content.Replace("{PrintDate}", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            model.Content = model.Content.Replace("{Title.month}", "Tháng " + month + " năm " + year);
            if (ExportExcel)
            {
                Response.AppendHeader("content-disposition", "attachment;filename=" + "BaoCaoHoaHongNhanVien" + DateTime.Now.ToString("yyyyMMdd") + ".xls");
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.Write(model.Content);
                Response.End();
            }
            return View(model);
        }
        string buildHtmlDetailList_PrintBCNXT(List<HistoryCommissionStaffViewModel> detailList)
        {
            //Tạo table html chi tiết phiếu xuất
            string detailLists = "<table class=\"invoice-detail\">\r\n";
            detailLists += "<thead>\r\n";
            detailLists += "	<tr>\r\n";
            detailLists += "		<th>STT</th>\r\n";
            detailLists += "		<th>Nhân viên</th>\r\n";
            detailLists += "		<th>Chức vụ</th>\r\n";
            detailLists += "		<th>Tỷ lệ hoa hồng (%)</th>\r\n";
            detailLists += "		<th>Doanh số tối thiểu</th>\r\n";
            detailLists += "		<th>Doanh số của nhà thuốc</th>\r\n";
            detailLists += "		<th>Hoa hồng được hưởng</th>\r\n";
            detailLists += "	</tr>\r\n";
            detailLists += "</thead>\r\n";
            detailLists += "<tbody>\r\n";
            var index = 1;

            foreach (var item in detailList)
            {

                detailLists += "<tr>\r\n"
                + "<td class=\"text-center orderNo\">" + (index++) + "</td>\r\n"
                + "<td class=\"text-left \">" + item.StaffName + "</td>\r\n"
                + "<td class=\"text-left \">" + item.PositionName + "</td>\r\n"

                + "<td class=\"text-right orderNo\">" + Erp.BackOffice.Helpers.Common.PhanCachHangNgan(item.CommissionPercent) + "</td>\r\n"
                + "<td class=\"text-right orderNo\">" + Erp.BackOffice.Helpers.Common.PhanCachHangNgan(item.MinimumRevenue) + "</td>\r\n"
                + "<td class=\"text-right orderNo\">" + Erp.BackOffice.Helpers.Common.PhanCachHangNgan(item.RevenueDS) + "</td>\r\n"
                + "<td class=\"text-right orderNo\">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(item.AmountCommission, null) + "</td>\r\n"

                + "</tr>\r\n";
            }
            detailLists += "</tbody>\r\n";
            detailLists += "<tfoot style=\"font-weight:bold\">\r\n";
            detailLists += "<tr><td colspan=\"6\" class=\"text-right\">Tổng cộng</td><td class=\"text-right\">"
                         + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(detailList.Sum(x => x.AmountCommission), null)
                         + "</tr>\r\n";
            detailLists += "</tfoot>\r\n</table>\r\n";

            return detailLists;
        }

        public static void SyncCommissionStaff(ProductInvoice model, int? CurrentUserId)
        {
            try
            {

                ProductInvoiceRepository productInvoiceRepository = new ProductInvoiceRepository(new ErpSaleDbContext());
                StaffsRepository staffRepository = new StaffsRepository(new ErpStaffDbContext());
                HistoryCommissionStaffRepository historyCommissionStaffRepository = new HistoryCommissionStaffRepository(new ErpStaffDbContext());
                //lấy danh sách nhà thuốc ra để bắt đầu đồng bộ dữ liệu
                var list_staff = staffRepository.GetvwAllStaffs().Where(x => (x.PositionCode == "ASM" || x.PositionCode == "TDV")).ToList();
                list_staff = list_staff.Where(x => ("," + x.DrugStore + ",").Contains("," + model.BranchId.ToString() + ",") == true).ToList();
                //lấy toàn bộ danh sách hóa đơn trong 1 tháng của tất cả nhà thuốc ra để xử lý 1 lần.
                var list_productInvoice = productInvoiceRepository.GetAllvwProductInvoice().Where(x => x.Month == model.CreatedDate.Value.Month && x.Year == model.CreatedDate.Value.Year && x.IsArchive == true).ToList();
                //lấy list đã insert nếu có
                var list_old = historyCommissionStaffRepository.GetAllHistoryCommissionStaffFull().Where(x => x.Month == model.CreatedDate.Value.Month && x.Year == model.CreatedDate.Value.Year).ToList();
                foreach (var item in list_staff)
                {
                    //lấy danh sách đơn bán hàng của nhà thuốc trong tháng
                    var list_invoice_by_drugStore = list_productInvoice.Where(x => ("," + item.DrugStore + ",").Contains("," + x.BranchId.ToString() + ",") == true).ToList();

                    decimal RevenueDS = list_invoice_by_drugStore.Sum(x => x.TotalAmount);
                    decimal CommissionPercent = item.CommissionPercent == null ? 0 : item.CommissionPercent.Value;
                    decimal MinimumRevenue = item.MinimumRevenue == null ? 0 : item.MinimumRevenue.Value;
                    decimal AmountCommission = (CommissionPercent * RevenueDS) / 100;
                    if (list_old.Where(x => x.StaffId == item.Id).Count() > 0)
                    {
                        var update = list_old.Where(x => x.StaffId == item.Id).FirstOrDefault();
                        update.IsDeleted = MinimumRevenue > RevenueDS ? true : false;
                        update.ModifiedUserId = CurrentUserId;
                        update.ModifiedDate = DateTime.Now;
                        update.StaffId = item.Id;
                        update.PositionName = item.PositionName;
                        update.Month = model.CreatedDate.Value.Month;
                        update.Year = model.CreatedDate.Value.Year;
                        update.CommissionPercent = CommissionPercent;
                        update.MinimumRevenue = MinimumRevenue;
                        update.RevenueDS = RevenueDS;
                        update.AmountCommission = AmountCommission;
                        update.StaffName = item.Name;
                        update.StaffParentId = item.StaffParentId;
                        historyCommissionStaffRepository.UpdateHistoryCommissionStaff(update);
                    }
                    else
                    {
                        //thêm mới vào data.
                        var add = new HistoryCommissionStaff();
                        add.IsDeleted = MinimumRevenue > RevenueDS ? true : false;
                        add.CreatedUserId = CurrentUserId;
                        add.ModifiedUserId = CurrentUserId;
                        add.AssignedUserId = CurrentUserId;
                        add.CreatedDate = DateTime.Now;
                        add.ModifiedDate = DateTime.Now;
                        add.StaffId = item.Id;
                        add.PositionName = item.PositionName;
                        add.Month = model.CreatedDate.Value.Month;
                        add.Year = model.CreatedDate.Value.Year;
                        add.CommissionPercent = item.CommissionPercent;
                        add.MinimumRevenue = item.MinimumRevenue;
                        add.RevenueDS = RevenueDS;
                        add.AmountCommission = AmountCommission;
                        add.StaffName = item.Name;
                        add.StaffParentId = item.StaffParentId;
                        historyCommissionStaffRepository.InsertHistoryCommissionStaff(add);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
