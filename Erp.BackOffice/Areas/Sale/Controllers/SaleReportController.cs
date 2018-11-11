using System.Globalization;
using Erp.BackOffice.Filters;
using Erp.Domain.Staff.Entities;
using Erp.Domain.Interfaces;
using Erp.Domain.Sale.Interfaces;
using Erp.Domain.Sale.Entities;
using Erp.Domain.Staff.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Erp.Utilities;
using WebMatrix.WebData;
using Erp.BackOffice.Staff.Models;
using Erp.BackOffice.Sale.Models;
using Erp.BackOffice.Helpers;
using Erp.Domain.Helper;
using Newtonsoft.Json;
using Erp.Domain.Account.Interfaces;
using Erp.BackOffice.Account.Models;
using System.Data;
using System.Web;
namespace Erp.BackOffice.Sale.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class SaleReportController : Controller
    {
        private readonly IBranchRepository BranchRepository;
        private readonly IUserRepository userRepository;
        private readonly IBranchDepartmentRepository branchDepartmentRepository;
        private readonly IStaffsRepository staffRepository;
        private readonly ISaleReportRepository saleReportRepository;
        private readonly IProductInvoiceRepository invoiceRepository;
        private readonly IPurchaseOrderRepository purchaseOrderRepository;
        private readonly IWarehouseRepository warehouseRepository;
        private readonly IProductInboundRepository inboundRepository;
        private readonly IProductOutboundRepository outboundRepository;
        private readonly ISalesReturnsRepository salesReturnsRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly IQueryHelper QueryHelper;
        private readonly IRequestInboundRepository requestInboundRepository;
        private readonly IProductOrServiceRepository ProductRepository;
        private readonly IInventoryRepository inventoryRepository;
        private readonly ICommisionStaffRepository commisionStaffRepository;
        private readonly ITemplatePrintRepository templatePrintRepository;
        private readonly ITotalDiscountMoneyNTRepository TotalDiscountMoneyNTRepository;
        private readonly ISettingRepository settingRepository;
        public SaleReportController(
            IBranchRepository _Branch
            , IUserRepository _user
            , IBranchDepartmentRepository branchDepartment
            , IStaffsRepository staff
            , ISaleReportRepository saleReport
            , IProductInvoiceRepository invoice
            , IPurchaseOrderRepository purchaseOrder
            , IWarehouseRepository warehouse
            , IProductInboundRepository inbound
            , IProductOutboundRepository outbound
            , ISalesReturnsRepository salesReturns
            , IQueryHelper _QueryHelper
             , ICustomerRepository _Customer
            , IRequestInboundRepository requestInbound
            , IProductOrServiceRepository product
            , IInventoryRepository inventory
            , ICommisionStaffRepository commisionStaff
            , ITemplatePrintRepository templatePrint
            , ITotalDiscountMoneyNTRepository totalDiscountMoneyNT
            , ISettingRepository setting
            )
        {
            BranchRepository = _Branch;
            userRepository = _user;
            branchDepartmentRepository = branchDepartment;
            staffRepository = staff;
            saleReportRepository = saleReport;
            invoiceRepository = invoice;
            purchaseOrderRepository = purchaseOrder;
            warehouseRepository = warehouse;
            inboundRepository = inbound;
            outboundRepository = outbound;
            salesReturnsRepository = salesReturns;
            customerRepository = _Customer;
            QueryHelper = _QueryHelper;
            requestInboundRepository = requestInbound;
            ProductRepository = product;
            inventoryRepository = inventory;
            commisionStaffRepository = commisionStaff;
            templatePrintRepository = templatePrint;
            TotalDiscountMoneyNTRepository = totalDiscountMoneyNT;
            settingRepository = setting;
        }

        #region Báo cáo kho
        public ActionResult Inventory()
        {
            var warehouseList = warehouseRepository.GetAllWarehouse().Where(x => ("," + Helpers.Common.CurrentUser.DrugStore + ",").Contains("," + x.BranchId + ",") == true);
            ViewBag.warehouseList = warehouseList.AsEnumerable().Select(x => new SelectListItem { Value = x.Id + "", Text = x.Name });
            return View();
        }
        public ActionResult BaoCaoNhapXuatTon()
        {
            var warehouseList = warehouseRepository.GetAllWarehouse().Where(x => ("," + Helpers.Common.CurrentUser.DrugStore + ",").Contains("," + x.BranchId + ",") == true);
            ViewBag.warehouseList = warehouseList.AsEnumerable().Select(x => new SelectListItem { Value = x.Id + "", Text = x.Name });
            return View();

        }
        public ActionResult BaoCaoXuatKho()
        {
            var warehouseList = warehouseRepository.GetAllWarehouse().Where(x => ("," + Helpers.Common.CurrentUser.DrugStore + ",").Contains("," + x.BranchId + ",") == true);
            ViewBag.warehouseList = warehouseList.AsEnumerable().Select(x => new SelectListItem { Value = x.Id + "", Text = x.Name });
            return View();
        }
        public ActionResult ChartInboundAndOutboundInMonth(string single, int? year, int? month, int? quarter, int? week, string group, string branchId)
        {
            var model = new ChartInboundAndOutboundInMonthViewModel();
            single = single == null ? "month" : single;
            year = year == null ? DateTime.Now.Year : year;
            month = month == null ? DateTime.Now.Month : month;
            quarter = quarter == null ? 1 : quarter;
            group = string.IsNullOrEmpty(group) ? "" : group;
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;
            var weekdefault = calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            week = week == null ? weekdefault : week;
            branchId = branchId == null ? "" : branchId.ToString();
            if (!Filters.SecurityFilter.IsAdmin() && !Filters.SecurityFilter.IsKeToan() && string.IsNullOrEmpty(branchId))
            {
                branchId = Helpers.Common.CurrentUser.DrugStore;
            }

            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;
            ViewBag.DateRangeText = Helpers.Common.ConvertToDateRange(ref StartDate, ref EndDate, single, year.Value, month.Value, quarter.Value, ref week);
            var d_startDate = DateTime.ParseExact(StartDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null).ToString("yyyy-MM-dd HH:mm:ss");
            var d_endDate = DateTime.ParseExact(EndDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null).ToString("yyyy-MM-dd HH:mm:ss");

            var dataInbound = SqlHelper.QuerySP<ChartItem>("spSale_ReportProductInbound", new
            {
                StartDate = d_startDate,
                EndDate = d_endDate,
                ProductGroup = group,
                branchId = branchId
            });

            var dataOutbound = SqlHelper.QuerySP<ChartItem>("spSale_ReportProductOutbound", new
            {
                StartDate = d_startDate,
                EndDate = d_endDate,
                ProductGroup = group,
                branchId = branchId
            });

            //Xử lý dữ liệu
            if (dataInbound.Count() > 0)
            {
                foreach (var item in dataInbound)
                {
                    if (!string.IsNullOrEmpty(item.label))
                    {
                        item.label = item.label.Trim().Replace("\t", "");
                    }
                }
            }

            if (dataOutbound.Count() > 0)
            {
                foreach (var item in dataOutbound)
                {
                    if (!string.IsNullOrEmpty(item.label))
                    {
                        item.label = item.label.Trim().Replace("\t", "");
                    }
                }
            }

            var category = dataInbound.Select(item => item.label).Union(dataOutbound.Select(item => item.label));
            var qGroupTemp = dataInbound.Select(item => new { GroupName = item.group, NumberOfInbound = Convert.ToInt32(item.data), NumberOfOutbound = 0 })
                .Union(dataOutbound.Select(item => new { GroupName = item.group, NumberOfInbound = 0, NumberOfOutbound = Convert.ToInt32(item.data) }));

            //Thống kế theo nhóm sản phẩm
            var qGroup = qGroupTemp.GroupBy(
                item => item.GroupName,
                (key, g) => new
                {
                    GroupName = key,
                    NumberOfInbound = g.Sum(i => i.NumberOfInbound),
                    NumberOfOutbound = g.Sum(i => i.NumberOfOutbound)
                }
            );

            model.jsonInbound = JsonConvert.SerializeObject(dataInbound);

            model.jsonOutbound = JsonConvert.SerializeObject(dataOutbound);

            model.jsonCategory = JsonConvert.SerializeObject(category);

            model.GroupList = qGroup.ToDataTable();

            if (dataInbound.Count() > 0)
            {
                model.TongNhap = dataInbound.Sum(item => Convert.ToInt32(item.data));
            }
            else
            {
                model.TongNhap = 0;
            }

            if (dataOutbound.Count() > 0)
            {
                model.TongXuat = dataOutbound.Sum(item => Convert.ToInt32(item.data));
            }
            else
            {
                model.TongXuat = 0;
            }

            model.Year = year.Value;
            model.Month = month.Value;
            model.Single = single;
            model.Week = week.Value;
            return View(model);
        }
        public ActionResult RequestInbound(string single, int? year, int? month, int? quarter, int? week, string CityId, string DistrictId, string branchId, int? WarehouseId)
        {
            SaleReportRequestInboundViewModel model = new SaleReportRequestInboundViewModel();
            year = year == null ? DateTime.Now.Year : year;
            month = month == null ? DateTime.Now.Month : month;
            single = single == null ? "month" : single;
            quarter = quarter == null ? 1 : quarter;
            CityId = string.IsNullOrEmpty(CityId) ? "" : CityId;
            DistrictId = string.IsNullOrEmpty(DistrictId) ? "" : DistrictId;
            branchId = string.IsNullOrEmpty(branchId) ? "" : branchId;
            WarehouseId = WarehouseId == null ? 0 : WarehouseId;
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;
            var weekdefault = calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            week = week == null ? weekdefault : week;
            if (!Filters.SecurityFilter.IsAdmin() && !Filters.SecurityFilter.IsKeToan() && string.IsNullOrEmpty(branchId))
            {
                branchId = Helpers.Common.CurrentUser.DrugStore;
            }
            var jsonData = new List<ChartItem>();
            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;

            ViewBag.DateRangeText = Helpers.Common.ConvertToDateRange(ref StartDate, ref EndDate, single, year.Value, month.Value, quarter.Value, ref week);

            var qThongKeYeuCau = requestInboundRepository.GetAllvwRequestInbound()
                   .Where(x => x.CreatedDate > StartDate
                       && x.CreatedDate < EndDate).ToList();
            var user = userRepository.GetByvwUserName(Helpers.Common.CurrentUser.UserName);
            if (!string.IsNullOrEmpty(branchId))
            {
                qThongKeYeuCau = qThongKeYeuCau.Where(item => ("," + branchId + ",").Contains("," + item.BranchId + ",") == true).ToList();
            }

            //Thống kê đơn hàng khởi tạo/đang xử lý
            model.NumberOfRequestInbound = qThongKeYeuCau.Count();
            model.NumberOfRequestInbound_Error_success = qThongKeYeuCau.Where(item => item.Error == true && (item.ErrorQuantity.Value <= 0)).Count();
            model.NumberOfRequestInbound_Error_no_success = qThongKeYeuCau.Where(item => item.Error == true && item.ErrorQuantity.Value > 0).Count();
            model.NumberOfRequestInbound_Error = qThongKeYeuCau.Where(item => item.Error == true).Count();
            model.NumberOfRequestInbound_inbound_complete = qThongKeYeuCau.Where(x => x.Status == "inbound_complete").Count();
            model.NumberOfRequestInbound_InProgress = qThongKeYeuCau.Where(x => x.Status == "ApprovedASM").Count();
            model.NumberOfRequestInbound_new = qThongKeYeuCau.Where(x => x.Status == "new").Count();
            model.NumberOfRequestInbound_refure = qThongKeYeuCau.Where(x => x.Status == "refure").Count();
            model.NumberOfRequestInbound_shipping = qThongKeYeuCau.Where(x => x.Status == "shipping").Count();
            model.NumberOfRequestInbound_wait_shipping = qThongKeYeuCau.Where(x => x.Status == "ApprovedKT").Count();
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;
            ViewBag.branchId = branchId;
            return View(model);
        }
        #endregion

        #region Báo cáo bán hàng/doanh thu
        public ActionResult Summary(string single, int? year, int? month, int? quarter, int? week, string CityId, string DistrictId, int? branchId)
        {
            SaleReportSumaryViewModel model = new SaleReportSumaryViewModel();

            single = single == null ? "month" : single;
            year = year == null ? DateTime.Now.Year : year;
            month = month == null ? DateTime.Now.Month : month;
            quarter = quarter == null ? 1 : quarter;
            CityId = string.IsNullOrEmpty(CityId) ? "" : CityId;
            DistrictId = string.IsNullOrEmpty(DistrictId) ? "" : DistrictId;
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;
            var weekdefault = calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            week = week == null ? weekdefault : week;
            string BranchId = branchId == null ? "" : branchId.Value.ToString();

            if (!Filters.SecurityFilter.IsAdmin() && !Filters.SecurityFilter.IsKeToan() && string.IsNullOrEmpty(BranchId))
            {
                BranchId = Helpers.Common.CurrentUser.DrugStore;
            }

            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;

            ViewBag.DateRangeText = Helpers.Common.ConvertToDateRange(ref StartDate, ref EndDate, single, year.Value, month.Value, quarter.Value, ref week);

            //var qThongKeBanHang = invoiceRepository.GetAllvwInvoiceDetails().AsEnumerable()
            //        .Where(x => (string.IsNullOrEmpty(BranchId) || ("," + BranchId + ",").Contains("," + x.BranchId + ",") == true) && x.IsArchive &&x.ProductInvoiceDate > StartDate && x.ProductInvoiceDate < EndDate);

            var invoice = invoiceRepository.GetAllvwProductInvoice().Where(x => x.IsArchive == true).AsEnumerable();

            //Thống kê đơn hàng khởi tạo/đang xử lý
            model.NumberOfProductInvoice_Pendding = invoice.AsEnumerable()
             .Where(x => (string.IsNullOrEmpty(BranchId) || ("," + BranchId + ",").Contains("," + x.BranchId + ",") == true) && x.Status == App_GlobalResources.Wording.OrderStatus_pending).Count();
            model.NumberOfProductInvoice_InProgress = invoice.AsEnumerable()
             .Where(x => (string.IsNullOrEmpty(BranchId) || ("," + BranchId + ",").Contains("," + x.BranchId + ",") == true) && x.Status == App_GlobalResources.Wording.OrderStatus_inprogress).Count();

            //Thống kê doanh thu bán hàng

            #region Hoapd tinh lai VIP

            var qProductInvoiceVIP = invoiceRepository.GetAllvwProductInvoice().AsEnumerable()
                .Where(item => (string.IsNullOrEmpty(BranchId) || ("," + BranchId + ",").Contains("," + item.BranchId + ",") == true && item.IsArchive == true));

            if (year != null && year.Value > 0)
            {
                qProductInvoiceVIP = qProductInvoiceVIP.Where(x => x.Year <= year).ToList();
                //   hasSearch = true;
            }
            if (month != null && month.Value > 0)
            {
                //   hasSearch = true;
                qProductInvoiceVIP = qProductInvoiceVIP.Where(x => ((x.Month <= month && x.Year == year) || (x.Year < year))).ToList();
            }

            decimal pDoansoVip = qProductInvoiceVIP.Sum(x => x.TotalAmount);
            var setting = settingRepository.GetSettingByKey("setting_point").Value;
            setting = string.IsNullOrEmpty(setting) ? "0" : setting;
            var rf = pDoansoVip / Convert.ToDecimal(setting);
            string[] arrVal = rf.ToString().Split(',');
            var value = int.Parse(arrVal[0], CultureInfo.InstalledUICulture);
            model.SoVIP = value;

            #endregion


            var qProductInvoice = invoiceRepository.GetAllvwProductInvoice().AsEnumerable()
                .Where(x => x.IsArchive && x.CreatedDate > StartDate && x.CreatedDate < EndDate);
            if (!string.IsNullOrEmpty(CityId))
            {
                qProductInvoice = qProductInvoice.Where(item => item.CityId == CityId);
                //invoice = invoice.Where(item => item.CityId == CityId);
            }
            if (!string.IsNullOrEmpty(DistrictId))
            {
                qProductInvoice = qProductInvoice.Where(item => item.DistrictId == DistrictId);
                //invoice = invoice.Where(item => item.DistrictId == DistrictId);
            }
            if (!string.IsNullOrEmpty(BranchId))
            {
                qProductInvoice = qProductInvoice.Where(item => (string.IsNullOrEmpty(BranchId) || ("," + BranchId + ",").Contains("," + item.BranchId + ",") == true));
                //invoice = invoice.Where(item => (string.IsNullOrEmpty(BranchId) || ("," + BranchId + ",").Contains("," + item.BranchId + ",") == true));
            }
            if (qProductInvoice.Count() > 0)
            {

                decimal AmountCommissionStaff = 0;
                if (Erp.BackOffice.Filters.SecurityFilter.IsAdminSystemManager() || Erp.BackOffice.Filters.SecurityFilter.IsTrinhDuocVien())
                {
                    var list_staff = staffRepository.GetvwAllStaffs().Where(x => x.UserId == Erp.BackOffice.Helpers.Common.CurrentUser.Id).ToList();
                    if (list_staff.Count() > 0)
                    {
                        decimal RevenueDS = qProductInvoice.Sum(x => x.TotalAmount);
                        var staff = list_staff.FirstOrDefault();
                        decimal CommissionPercent = staff.CommissionPercent == null ? 0 : staff.CommissionPercent.Value;
                        AmountCommissionStaff = (CommissionPercent * RevenueDS) / 100;
                    }
                }
                var Revenue = qProductInvoice.Sum(item => item.TotalAmount);
                var NumberOfProductInvoice = qProductInvoice.Count();
                var TotalFixedDiscount = qProductInvoice.Sum(item => item.FixedDiscount);
                var TotalIrregularDiscount = qProductInvoice.Sum(item => item.IrregularDiscount);
                var TotalDayInvoice = qProductInvoice.GroupBy(x => new { x.Day, x.Month, x.Year }).ToList();
                model.Revenue = Revenue;
                model.NumberOfProductInvoice = NumberOfProductInvoice;
                model.TotalFixedDiscount = TotalFixedDiscount;
                model.TotalIrregularDiscount = TotalIrregularDiscount;
                model.TotalDayInvoice = TotalDayInvoice.Count() > 0 ? TotalDayInvoice.Count() : 0;
                model.AmountCommissionStaff = AmountCommissionStaff;


            }
            model.ProductInvoiceList = new List<ProductInvoiceViewModel>();
            model.ProductInvoiceList = SqlHelper.QuerySP<ProductInvoiceViewModel>("spSale_LiabilitiesDrugStore_haopd", new
            {
                StartDate = StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                branchId = BranchId,
                CityId = CityId,
                DistrictId = DistrictId
            }).ToList();
            //var week1 = week - 3;
            //var invoice_week1=invoice.Where(x => x.WeekOfYear == week1).ToList();
            //model.AmountWeek1 = invoice_week1.Count()>0?invoice_week1.Sum(x => x.TotalAmount):0;
            //model.Week1 = week1;
            //if(invoice_week1.Count()>0)
            //    model.Status1 = invoice_week1.FirstOrDefault().DrugStoreUserId == null ? "Chưa chuyển" : (invoice_week1.FirstOrDefault().AccountancyUserId == null ? "Đã chuyển":"Kế toán đã duyệt");

            //var week2 = week - 2;
            //var invoice_week2 = invoice.Where(x => x.WeekOfYear == week2).ToList();
            //model.AmountWeek2 = invoice_week2.Count()>0?invoice_week2.Sum(x => x.TotalAmount):0;
            //model.Week2 = week2;
            //if (invoice_week2.Count() > 0)
            //    model.Status2 = invoice_week2.FirstOrDefault().DrugStoreUserId == null ? "Chưa chuyển" : (invoice_week2.FirstOrDefault().AccountancyUserId == null ? "Đã chuyển" : "Kế toán đã duyệt");

            //var week3 = week - 1;
            //var invoice_week3 = invoice.Where(x => x.WeekOfYear == week3).ToList();
            //model.AmountWeek3 =invoice_week3.Count()>0?invoice_week3.Sum(x => x.TotalAmount):0;
            //model.Week3 = week3;
            //if (invoice_week3.Count() > 0)
            //    model.Status3 = invoice_week3.FirstOrDefault().DrugStoreUserId == null ? "Chưa chuyển" : (invoice_week3.FirstOrDefault().AccountancyUserId == null ? "Đã chuyển" : "Kế toán đã duyệt");

            //var invoice_week4 = invoice.Where(x => x.WeekOfYear == week).ToList();
            //model.AmountWeek4 = invoice_week4.Count()>0?invoice_week4.Sum(x => x.TotalAmount):0;
            //model.Week4 = week;
            //if (invoice_week4.Count() > 0)
            //    model.Status4 = invoice_week4.FirstOrDefault().DrugStoreUserId == null ? "Chưa chuyển" : (invoice_week4.FirstOrDefault().AccountancyUserId == null ? "Đã chuyển" : "Kế toán đã duyệt");

            var qThongKeBanHang_TheoKhachHang = new List<ChartItem>();
            var title = "";



            if (branchId == null)
            {
                if (string.IsNullOrEmpty(DistrictId))
                {
                    if (string.IsNullOrEmpty(CityId))
                    {
                        //Thống kê bán hàng theo khách hàng
                        qThongKeBanHang_TheoKhachHang = qProductInvoice.Where(item => item.TotalAmount > 0).Select(item => new { item.CityId, item.ProvinceName, item.TotalAmount })
                           .ToList()
                           .GroupBy(l => new { l.CityId, l.ProvinceName })
                           .Select(cl => new ChartItem
                           {
                               label = cl.Key.ProvinceName,
                               data = cl.Sum(i => i.TotalAmount),
                               id = cl.Key.CityId,
                           }).ToList();
                        title = "Thống kê doanh số theo Tỉnh/Thành phố";
                    }
                    else
                    {
                        //Thống kê bán hàng theo khách hàng
                        qThongKeBanHang_TheoKhachHang = qProductInvoice.Where(item => item.TotalAmount > 0).Select(item => new { item.DistrictId, item.DistrictName, item.TotalAmount })
                           .ToList()
                           .GroupBy(l => new { l.DistrictId, l.DistrictName })
                           .Select(cl => new ChartItem
                           {
                               label = cl.Key.DistrictName,
                               data = cl.Sum(i => i.TotalAmount),
                               id = cl.Key.DistrictId,
                           }).ToList();
                        title = "Thống kê doanh số theo Quận/Huyện";
                    }

                }
                else
                {
                    //Thống kê bán hàng theo khách hàng
                    qThongKeBanHang_TheoKhachHang = qProductInvoice.Where(item => item.TotalAmount > 0).Select(item => new { item.BranchId, item.BranchName, item.TotalAmount })
                       .ToList()
                       .GroupBy(l => new { l.BranchId, l.BranchName })
                       .Select(cl => new ChartItem
                       {
                           label = cl.Key.BranchName,
                           data = cl.Sum(i => i.TotalAmount),
                           id = cl.Key.BranchId.ToString(),
                       }).ToList();
                    title = "Thống kê doanh số theo Nhà thuốc";
                }

            }
            else
            {
                //Thống kê bán hàng theo khách hàng
                qThongKeBanHang_TheoKhachHang = qProductInvoice.Where(item => item.TotalAmount > 0).Select(item => new { item.CreatedUserId, item.SalerFullName, item.TotalAmount })
                   .ToList()
                   .GroupBy(l => new { l.CreatedUserId, l.SalerFullName })
                   .Select(cl => new ChartItem
                   {
                       label = cl.Key.SalerFullName,
                       data = cl.Sum(i => i.TotalAmount),
                       id = cl.Key.CreatedUserId.Value.ToString(),
                   }).ToList();
                title = "Thống kê doanh số theo nhân viên nhà thuốc";
            }
            ViewBag.jsonThongKeBanHang_TheoKhachHang = JsonConvert.SerializeObject(qThongKeBanHang_TheoKhachHang);
            ViewBag.Title_qThongKeBanHang_TheoKhachHang = title;
            ViewBag.single = single;
            ViewBag.Year = year;
            ViewBag.StartDate = StartDate.ToString("dd/MM/yyyy");
            ViewBag.EndDate = EndDate.ToString("dd/MM/yyyy");
            return View(model);
        }

        public ActionResult ChartInvoiceDayInMonth(string single, int? year, int? month, int? quarter, int? week, string CityId, string DistrictId, int? branchId)
        {
            single = single == null ? "month" : single;
            year = year == null ? DateTime.Now.Year : year;
            month = month == null ? DateTime.Now.Month : month;
            quarter = quarter == null ? 1 : quarter;
            CityId = string.IsNullOrEmpty(CityId) ? "" : CityId;
            DistrictId = string.IsNullOrEmpty(DistrictId) ? "" : DistrictId;
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;
            var weekdefault = calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            week = week == null ? weekdefault : week;
            string BranchId = branchId == null ? "" : branchId.Value.ToString();

            if (!Filters.SecurityFilter.IsAdmin() && !Filters.SecurityFilter.IsKeToan() && string.IsNullOrEmpty(BranchId))
            {
                BranchId = Helpers.Common.CurrentUser.DrugStore;
            }

            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;

            ViewBag.DateRangeText = Helpers.Common.ConvertToDateRange(ref StartDate, ref EndDate, single, year.Value, month.Value, quarter.Value, ref week);
            var d_startDate = DateTime.ParseExact(StartDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null).ToString("yyyy-MM-dd HH:mm:ss");
            var d_endDate = DateTime.ParseExact(EndDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null).ToString("yyyy-MM-dd HH:mm:ss");

            if (single == "month" || single == "week")
            {
                var data = SqlHelper.QuerySP<ChartItem>("spSale_ChartInvoiceDayInMonth", new
                {
                    StartDate = d_startDate,
                    EndDate = d_endDate,
                    branchId = BranchId,
                    CityId = CityId,
                    DistrictId = DistrictId
                });

                var jsonData = new List<ChartItem>();
                for (DateTime dt = StartDate; dt <= EndDate; dt = dt.AddDays(1))
                {
                    string label = dt.Day + "/" + dt.Month;
                    var obj = data.Where(item => item.label == label).FirstOrDefault();
                    if (obj == null)
                    {
                        obj = new ChartItem();
                        obj.label = label;
                        obj.data = 0;
                    }

                    jsonData.Add(obj);
                }

                string json = JsonConvert.SerializeObject(jsonData);
                ViewBag.json = json;
            }
            else
            {
                var data = SqlHelper.QuerySP<ChartItem>("spSale_ChartInvoiceDayInMonth2", new
                {
                    StartDate = d_startDate,
                    EndDate = d_endDate,
                    branchId = BranchId,
                    CityId = CityId,
                    DistrictId = DistrictId
                });

                var jsonData = new List<ChartItem>();
                for (int i = StartDate.Month; i <= EndDate.Month; i++)
                {
                    string label = i + "/" + year.Value;
                    var obj = data.Where(item => item.label == label).FirstOrDefault();
                    if (obj == null)
                    {
                        obj = new ChartItem();
                        obj.label = label;
                        obj.data = 0;
                    }

                    jsonData.Add(obj);
                }

                string json = JsonConvert.SerializeObject(jsonData);
                ViewBag.json = json;
            }

            return View();
        }

        public ActionResult ChartProductSaleInMonth(string single, int? year, int? month, int? quarter, int? week, string CityId, string DistrictId, int? branchId)
        {
            single = single == null ? "month" : single;
            year = year == null ? DateTime.Now.Year : year;
            month = month == null ? DateTime.Now.Month : month;
            quarter = quarter == null ? 1 : quarter;
            CityId = string.IsNullOrEmpty(CityId) ? "" : CityId;
            DistrictId = string.IsNullOrEmpty(DistrictId) ? "" : DistrictId;
            string BranchId = branchId == null ? "" : branchId.Value.ToString();
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;
            var weekdefault = calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            week = week == null ? weekdefault : week;
            if (!Filters.SecurityFilter.IsAdmin() && !Filters.SecurityFilter.IsKeToan() && string.IsNullOrEmpty(BranchId))
            {
                BranchId = Helpers.Common.CurrentUser.DrugStore;
            }

            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;

            ViewBag.DateRangeText = Helpers.Common.ConvertToDateRange(ref StartDate, ref EndDate, single, year.Value, month.Value, quarter.Value, ref week);
            var d_startDate = DateTime.ParseExact(StartDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null).ToString("yyyy-MM-dd HH:mm:ss");
            var d_endDate = DateTime.ParseExact(EndDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null).ToString("yyyy-MM-dd HH:mm:ss");

            var data = SqlHelper.QuerySP<ChartItem>("spSale_ChartProductSaleInMonth", new
            {
                StartDate = d_startDate,
                EndDate = d_endDate,
                branchId = BranchId,
                CityId = CityId,
                DistrictId = DistrictId
            });

            string json = JsonConvert.SerializeObject(data);
            ViewBag.json = json;

            return View();
        }

        public ActionResult ChartServiceSaleInMonth(string single, int? year, int? month, int? quarter, int? week, string branchId)
        {
            single = single == null ? "month" : single;
            year = year == null ? DateTime.Now.Year : year;
            month = month == null ? DateTime.Now.Month : month;
            quarter = quarter == null ? 1 : quarter;
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;
            var weekdefault = calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            week = week == null ? weekdefault : week;
            branchId = branchId == null ? "" : branchId;

            if (!Filters.SecurityFilter.IsAdmin() && !Filters.SecurityFilter.IsKeToan() && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeName != "CSKH")
            {
                branchId = Helpers.Common.CurrentUser.DrugStore;
            }

            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;

            ViewBag.DateRangeText = Helpers.Common.ConvertToDateRange(ref StartDate, ref EndDate, single, year.Value, month.Value, quarter.Value, ref week);
            var data = SqlHelper.QuerySP<ChartItem>("spSale_ChartServiceSaleInMonth", new
            {
                StartDate = StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                branchId = branchId
            });
            //            var data = SqlHelper.QuerySQL<ChartItem>(string.Format(@"SELECT TOP 10 *
            //                                                                    FROM 
            //                                                                    (
            //	                                                                    SELECT vwSale_ProductAndService.Code as label, vwSale_ProductAndService.Name as label2, sum(Quantity * Price) As data
            //	                                                                    FROM [vwSale_ProductInvoiceDetail] as PInD
            //                                                                        left outer join vwSale_ProductAndService on vwSale_ProductAndService.Id = PInD.ProductId
            //	                                                                    WHERE ('{2}' is null or ',' + '{2}' + ','  like '%,'+PInD.BranchId+',%') and PInD.IsArchive = 1 and vwSale_ProductAndService.Type = 'service' and PInD.CreatedDate > '{0}' and PInD.CreatedDate < '{1}'
            //	                                                                    GROUP BY ProductId, vwSale_ProductAndService.Code, vwSale_ProductAndService.Name
            //                                                                    ) as Tbx
            //                                                                    order by data desc", StartDate.ToString("yyyy-MM-dd HH:mm:ss"), EndDate.ToString("yyyy-MM-dd HH:mm:ss"), branchId));

            string json = JsonConvert.SerializeObject(data);
            ViewBag.json = json;
            return View();
        }

        public ActionResult Commision()
        {
            ViewBag.branchList = BranchRepository.GetAllBranch().AsEnumerable().Select(x => new
                SelectListItem
            { Text = x.Name, Value = x.Id + "" });

            return View();
        }

        public ActionResult BaoCaoBanHang()
        {
            ViewBag.branchList = BranchRepository.GetAllBranch().AsEnumerable().Select(x => new
               SelectListItem
            { Text = x.Name, Value = x.Id + "" });
            return View();
        }
        public ActionResult InvoiceByBranch()
        {
            ViewBag.branchList = BranchRepository.GetAllBranch().AsEnumerable().Select(x => new
                SelectListItem
            { Text = x.Name, Value = x.Id + "" });

            return View();
        }

        public ActionResult InvoiceByCustomer()
        {
            ViewBag.branchList = BranchRepository.GetAllBranch().AsEnumerable().Select(x => new
                SelectListItem
            { Text = x.Name, Value = x.Id + "" });

            return View();
        }

        public ActionResult BaoCaoDonHang()
        {
            ViewBag.branchList = BranchRepository.GetAllBranch().AsEnumerable().Select(x => new
               SelectListItem
            { Text = x.Name, Value = x.Id + "" });
            return View();
        }
        public ActionResult BaoCaoSoLuongBan()
        {
            ViewBag.branchList = BranchRepository.GetAllBranch().AsEnumerable().Select(x => new
               SelectListItem
            { Text = x.Name, Value = x.Id + "" });
            return View();
        }
        public ActionResult BaoCaoHangBanTraLai()
        {
            ViewBag.branchList = BranchRepository.GetAllBranch().AsEnumerable().Select(x => new
               SelectListItem
            { Text = x.Name, Value = x.Id + "" });
            return View();
        }

        #endregion

        #region Báo cáo thu/chi/công nợ
        public ActionResult ReceiptList()
        {
            ViewBag.branchList = BranchRepository.GetAllBranch().AsEnumerable().Select(x => new
               SelectListItem
            { Text = x.Name, Value = x.Id + "" });

            return View();
        }
        public ActionResult ReceiptListCustomer()
        {
            ViewBag.branchList = BranchRepository.GetAllBranch().AsEnumerable().Select(x => new
               SelectListItem
            { Text = x.Name, Value = x.Id + "" });
            //Danh sách nhân viên sale
            var SaleList = userRepository.GetUserbyUserType("Sales Excutive").Select(item => new { item.FullName, item.Id }).ToList()
                .Select(x => new SelectListItem
                {
                    Text = x.FullName,
                    Value = x.Id + ""
                });

            ViewBag.SaleList = SaleList;
            //Danh sách khách hàng
            ViewBag.customerList = SelectListHelper.GetSelectList_Customer(null, null);//customerRepository.GetAllCustomer().Where(x => x.BranchId == Helpers.Common.CurrentUser.BranchId.Value)
            //.Select(item => new SelectListItem
            //{
            //    Value = item.Id + "",
            //    Text = item.CompanyName
            //}).ToList();

            //ViewBag.customerList = customerList;
            return View();
        }
        public ActionResult PaymentList()
        {
            ViewBag.branchList = BranchRepository.GetAllBranch().AsEnumerable().Select(x => new
               SelectListItem
            { Text = x.Name, Value = x.Id + "" });

            return View();
        }

        public ActionResult BaoCaoCongNoTongHop()
        {
            ViewBag.branchList = BranchRepository.GetAllBranch().AsEnumerable().Select(x => new
               SelectListItem
            { Text = x.Name, Value = x.Id + "" });
            //Danh sách nhân viên sale
            var SaleList = userRepository.GetUserbyUserType("Sales Excutive").Select(item => new { item.FullName, item.Id }).ToList()
                .Select(x => new SelectListItem
                {
                    Text = x.FullName,
                    Value = x.Id + ""
                });

            ViewBag.SaleList = SaleList;
            //Danh sách khách hàng
            ViewBag.customerList = SelectListHelper.GetSelectList_Customer(null, null);
            return View();
        }

        public ActionResult BaoCaoCongNoChiTiet()
        {
            ViewBag.branchList = BranchRepository.GetAllBranch().AsEnumerable().Select(x => new
               SelectListItem
            { Text = x.Name, Value = x.Id + "" });
            return View();
        }

        public ActionResult BaoCaoDoanhThu()
        {
            ViewBag.branchList = BranchRepository.GetAllBranch().AsEnumerable().Select(x => new
               SelectListItem
            { Text = x.Name, Value = x.Id + "" });
            //Danh sách nhân viên sale
            var SaleList = userRepository.GetUserbyUserType("Sales Excutive").Select(item => new { item.FullName, item.Id }).ToList()
                .Select(x => new SelectListItem
                {
                    Text = x.FullName,
                    Value = x.Id + ""
                });

            ViewBag.SaleList = SaleList;
            //Danh sách khách hàng
            ViewBag.customerList = SelectListHelper.GetSelectList_Customer(null, null);
            return View();
        }

        public ActionResult BaoCaoCongNoPhaiThu()
        {
            ViewBag.branchList = BranchRepository.GetAllBranch().AsEnumerable().Select(x => new
               SelectListItem
            { Text = x.Name, Value = x.Id + "" });
            //Danh sách khách hàng
            ViewBag.customerList = SelectListHelper.GetSelectList_Customer(null, null);
            return View();
        }
        public ActionResult BaoCaoTongChi()
        {
            ViewBag.branchList = BranchRepository.GetAllBranch().AsEnumerable().Select(x => new
               SelectListItem
            { Text = x.Name, Value = x.Id + "" });
            return View();
        }

        public ActionResult BaoCaoCongNoTongHopNCC()
        {
            ViewBag.branchList = BranchRepository.GetAllBranch().AsEnumerable().Select(x => new
               SelectListItem
            { Text = x.Name, Value = x.Id + "" });
            ////Danh sách nhân viên sale
            //var SaleList = userRepository.GetUserbyUserType("Sales Excutive").Select(item => new { item.FullName, item.Id }).ToList()
            //    .Select(x => new SelectListItem
            //    {
            //        Text = x.FullName,
            //        Value = x.Id + ""
            //    });

            //ViewBag.SaleList = SaleList;
            //Danh sách khách hàng
            //ViewBag.customerList = SelectListHelper.GetSelectList_Customer(null, null);
            return View();
        }

        public ActionResult BaoCaoCongNoPhaiTraNCC()
        {
            ViewBag.branchList = BranchRepository.GetAllBranch().AsEnumerable().Select(x => new
               SelectListItem
            { Text = x.Name, Value = x.Id + "" });
            ////Danh sách khách hàng
            //ViewBag.customerList = SelectListHelper.GetSelectList_Customer(null, null);
            return View();
        }
        #endregion
        public ActionResult BaoCaoKhoTheoNgay(string group, string category, string manufacturer, int? page, string StartDate, string EndDate, string WarehouseId)
        {
            group = group == null ? "" : group;
            category = category == null ? "" : category;
            manufacturer = manufacturer == null ? "" : manufacturer;
            WarehouseId = WarehouseId == null ? "" : WarehouseId;
            EndDate = string.IsNullOrEmpty(EndDate) ? "" : EndDate;
            StartDate = string.IsNullOrEmpty(StartDate) ? "" : StartDate;

            if (!Filters.SecurityFilter.IsAdmin() && !Filters.SecurityFilter.IsKeToan() && string.IsNullOrEmpty(WarehouseId))
            {
                WarehouseId = Helpers.Common.CurrentUser.WarehouseId;
            }


            DateTime aDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            // Cộng thêm 1 tháng và trừ đi một ngày.
            DateTime retDateTime = aDateTime.AddMonths(1).AddDays(-1).AddHours(23).AddMinutes(59);

            var d_startDate = (!string.IsNullOrEmpty(StartDate) == true ? DateTime.ParseExact(StartDate, "dd/MM/yyyy", null).ToString("yyyy-MM-dd HH:mm:ss") : aDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            var d_endDate = (!string.IsNullOrEmpty(EndDate) == true ? DateTime.ParseExact(EndDate, "dd/MM/yyyy", null).AddHours(23).AddMinutes(59).ToString("yyyy-MM-dd HH:mm:ss") : retDateTime.ToString("yyyy-MM-dd HH:mm:ss"));

            var data = SqlHelper.QuerySP<spBaoCaoNhapXuatTon_TuanViewModel>("spSale_BaoCaoNhapXuatTon_Tuan", new
            {
                StartDate = d_startDate,
                EndDate = d_endDate,
                WarehouseId = WarehouseId,
                CategoryCode = category,
                ProductGroup = group,
                Manufacturer = manufacturer
            }).ToList();
            var product_outbound = SqlHelper.QuerySP<spBaoCaoXuatViewModel>("spSale_BaoCaoXuat", new
            {
                StartDate = d_startDate,
                EndDate = d_endDate,
                WarehouseId = WarehouseId,
                CategoryCode = category,
                ProductGroup = group,
                Manufacturer = manufacturer
            }).ToList();
            //var pager = new Pager(data.Count(), page, 20);

            var Items = data.OrderBy(m => m.ProductCode)
              //.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize)
              .Select(item => new spBaoCaoNhapXuatTon_TuanViewModel
              {
                  CategoryCode = item.CategoryCode,
                  First_InboundQuantity = item.First_InboundQuantity,
                  First_OutboundQuantity = item.First_OutboundQuantity,
                  First_Remain = item.First_Remain,
                  Last_InboundQuantity = item.Last_InboundQuantity,
                  Last_OutboundQuantity = item.Last_OutboundQuantity,
                  ProductCode = item.ProductCode,
                  ProductId = item.ProductId,
                  ProductName = item.ProductName,
                  ProductUnit = item.ProductUnit,
                  Remain = item.Remain,
                  ProductMinInventory = item.ProductMinInventory,
                  LoCode = item.LoCode,
                  ExpiryDate = item.ExpiryDate,
                  WarehouseName = item.WarehouseName,
                  WarehouseId = item.WarehouseId
              }).ToList();

            ViewBag.productInvoiceDetailList = product_outbound;
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];


            return View(Items);
        }
        public ViewResult InventoryWarning(int? WarehouseId)
        {
            var listProduct = ProductRepository.GetAllvwProductByType("product");
            var viewModel = new IndexViewModel<ProductViewModel>
            {
                Items = listProduct.Where(m => m.QuantityTotalInventory < m.MinInventory)
                .Select(item => new ProductViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Code = item.Code,
                    PriceInbound = item.PriceInbound,
                    Type = item.Type,
                    Unit = item.Unit,
                    ModifiedDate = item.ModifiedDate,
                    MinInventory = item.MinInventory,
                    QuantityTotalInventory = item.QuantityTotalInventory,
                    CategoryCode = item.CategoryCode,

                }).ToList(),
                //Pager = pager
            };

            List<InventoryViewModel> inventoryList = new List<InventoryViewModel>();
            foreach (var item in viewModel.Items)
            {
                List<InventoryViewModel> inventoryP = inventoryRepository.GetAllvwInventoryByProductId(item.Id)
                .Select(itemV => new InventoryViewModel
                {

                    ProductId = itemV.ProductId,
                    Quantity = itemV.Quantity,
                    WarehouseId = itemV.WarehouseId,
                    ProductCode = itemV.ProductCode,
                    ProductName = itemV.ProductName,
                    WarehouseName = itemV.WarehouseName,
                    CBTK = itemV.CBTK,

                }).Where(u => u.CBTK > 0).ToList();
                if (WarehouseId != null)
                {
                    inventoryP = inventoryP.Where(u => u.WarehouseId == WarehouseId).ToList();
                    if (inventoryP.Count() > 0)
                    {
                        inventoryList.AddRange(inventoryP);
                    }
                }
                else
                {
                    inventoryP = inventoryP.Where(u => u.WarehouseId == 8).ToList();
                    if (inventoryP.Count() > 0)
                    {
                        inventoryList.AddRange(inventoryP);
                    }
                }
                //if (inventoryP.Count > 0)
                //inventoryList.AddRange(inventoryP);
            }
            inventoryList = inventoryList.OrderBy(u => u.Quantity).ToList();
            ViewBag.inventoryList = inventoryList;

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];

            return View(viewModel);
        }
        public ActionResult ProductInvoiceList(string ProductGroup, string manufacturer, string CategoryCode, string BranchId, string startDate, string endDate, int? ProductId)
        {
            var q = invoiceRepository.GetAllvwInvoiceDetails().Where(x => x.ProductType == "product" && x.IsArchive == true);

            if (startDate == null && endDate == null)
            {
                DateTime aDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                // Cộng thêm 1 tháng và trừ đi một ngày.
                DateTime retDateTime = aDateTime.AddMonths(1).AddDays(-1);
                startDate = aDateTime.ToString();
                endDate = retDateTime.ToString();
            }

            //Lọc theo ngày
            DateTime d_startDate, d_endDate;
            if (DateTime.TryParseExact(startDate, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out d_startDate))
            {
                if (DateTime.TryParseExact(endDate, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out d_endDate))
                {
                    d_endDate = d_endDate.AddHours(23).AddMinutes(59);
                    q = q.Where(x => x.CreatedDate >= d_startDate && x.CreatedDate <= d_endDate);
                }
            }

            if (string.IsNullOrEmpty(BranchId))
            {
                q = q.Where(x => ("," + BranchId + ",").Contains("," + x.BranchId + ",") == true);
            }
            if (!string.IsNullOrEmpty(ProductGroup))
            {
                q = q.Where(x => x.ProductGroup == ProductGroup);
            }
            if (!string.IsNullOrEmpty(manufacturer))
            {
                q = q.Where(x => x.Manufacturer == manufacturer);
            }
            if (!string.IsNullOrEmpty(CategoryCode))
            {
                q = q.Where(x => x.CategoryCode == CategoryCode);
            }
            if (ProductId != null && ProductId.Value > 0)
            {
                q = q.Where(x => x.ProductId == ProductId);
            }
            var model = q.Select(item => new ProductInvoiceDetailViewModel
            {
                Id = item.Id,
                IsDeleted = item.IsDeleted,
                CreatedUserId = item.CreatedUserId,
                CreatedDate = item.CreatedDate,
                ModifiedUserId = item.ModifiedUserId,
                ModifiedDate = item.ModifiedDate,
                Amount = item.Amount,
                CategoryCode = item.CategoryCode,
                IsReturn = item.IsReturn,
                FixedDiscount = item.FixedDiscount,
                FixedDiscountAmount = item.FixedDiscountAmount,
                IrregularDiscountAmount = item.IrregularDiscountAmount,
                IrregularDiscount = item.IrregularDiscount,
                CheckPromotion = item.CheckPromotion,
                ProductType = item.ProductType,
                ProductCode = item.ProductCode,
                ProductGroup = item.ProductGroup,
                ProductInvoiceCode = item.ProductInvoiceCode,
                Price = item.Price,
                ProductId = item.ProductId,
                ProductInvoiceDate = item.ProductInvoiceDate,
                ProductInvoiceId = item.ProductInvoiceId,
                ProductName = item.ProductName,
                Quantity = item.Quantity,
                CustomerId = item.CustomerId,
                CustomerCode = item.CustomerCode,
                CustomerName = item.CustomerName,
                BranchId = item.BranchId,
                BranchName = item.BranchName
            }).OrderByDescending(m => m.ModifiedDate).ToList();
            return View(model);
        }

        public ActionResult XuatExcel(string html)
        {
            Response.AppendHeader("content-disposition", "attachment;filename=" + "BaoCaoTonKho" + DateTime.Now.ToString("dd_MM_yyyy") + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.Write(html);
            Response.End();
            return Content("success");
        }
        #region Báo cáo mua hàng
        public ActionResult PurchaseOrderBySupplier()
        {
            ViewBag.branchList = BranchRepository.GetAllBranch().AsEnumerable().Select(x => new
                SelectListItem
            { Text = x.Name, Value = x.Id + "" });

            return View();
        }
        #endregion

        public ActionResult InventoryQueryExpiryDate()
        {
            var warehouseList = warehouseRepository.GetAllWarehouse().Where(x => ("," + Helpers.Common.CurrentUser.DrugStore + ",").Contains("," + x.BranchId + ",") == true);
            ViewBag.warehouseList = warehouseList.AsEnumerable().Select(x => new SelectListItem { Value = x.Id + "", Text = x.Name });
            return View();
        }


        public ActionResult CommisionStaff(string BranchId, int? year, int? month, int? StaffId)
        {
            var q = commisionStaffRepository.GetAllvwCommisionStaff();
            if (year != null && year.Value > 0)
            {
                q = q.Where(x => x.year == year);
            }
            else
            {
                q = q.Where(x => x.year == DateTime.Now.Year);
            }
            if (!string.IsNullOrEmpty(BranchId))
            {
                q = q.Where(x => ("," + BranchId + ",").Contains("," + x.BranchId + ",") == true);
            }
            if (year != null && year.Value > 0)
            {
                q = q.Where(x => x.year == year);
            }
            if (month != null && month.Value > 0)
            {
                q = q.Where(x => x.month == month);
            }
            if (StaffId != null && StaffId.Value > 0)
            {
                q = q.Where(x => x.StaffId == StaffId);
            }
            var model = q.Select(item => new CommisionStaffViewModel
            {
                Id = item.Id,
                IsDeleted = item.IsDeleted,
                CreatedUserId = item.CreatedUserId,
                CreatedDate = item.CreatedDate,
                ModifiedUserId = item.ModifiedUserId,
                ModifiedDate = item.ModifiedDate,
                AmountOfCommision = item.AmountOfCommision,
                BranchId = item.BranchId,
                BranchName = item.BranchName,
                InvoiceDetailId = item.InvoiceDetailId,
                InvoiceId = item.InvoiceId,
                InvoiceType = item.InvoiceType,
                IsResolved = item.IsResolved,
                month = item.month,
                Note = item.Note,
                PercentOfCommision = item.PercentOfCommision,
                ProductCode = item.ProductCode,
                ProductImage = item.ProductImage,
                ProductInvoiceCode = item.ProductInvoiceCode,
                ProductName = item.ProductName,
                StaffCode = item.StaffCode,
                StaffId = item.StaffId,
                StaffName = item.StaffName,
                StaffProfileImage = item.StaffProfileImage,
                year = item.year
            }).OrderByDescending(m => m.CreatedDate).ToList();
            return View(model);
        }
        public ActionResult ListCommisionStaff(int? StaffId)
        {
            var q = commisionStaffRepository.GetAllvwCommisionStaff();
            q = q.Where(x => x.year == DateTime.Now.Year);
            if (StaffId != null && StaffId.Value > 0)
            {
                q = q.Where(x => x.StaffId == StaffId);
            }
            var model = q.Select(item => new CommisionStaffViewModel
            {
                Id = item.Id,
                IsDeleted = item.IsDeleted,
                CreatedUserId = item.CreatedUserId,
                CreatedDate = item.CreatedDate,
                ModifiedUserId = item.ModifiedUserId,
                ModifiedDate = item.ModifiedDate,
                AmountOfCommision = item.AmountOfCommision,
                BranchId = item.BranchId,
                BranchName = item.BranchName,
                InvoiceDetailId = item.InvoiceDetailId,
                InvoiceId = item.InvoiceId,
                InvoiceType = item.InvoiceType,
                IsResolved = item.IsResolved,
                month = item.month,
                Note = item.Note,
                PercentOfCommision = item.PercentOfCommision,
                ProductCode = item.ProductCode,
                ProductImage = item.ProductImage,
                ProductInvoiceCode = item.ProductInvoiceCode,
                ProductName = item.ProductName,
                StaffCode = item.StaffCode,
                StaffId = item.StaffId,
                StaffName = item.StaffName,
                StaffProfileImage = item.StaffProfileImage,
                year = item.year

            }).OrderByDescending(m => m.CreatedDate).ToList();
            return View(model);
        }

        public ActionResult BaoCaoDoanhThuTheoNgay(string startDate, string endDate, string CityId, string DistrictId, int? branchId)
        {
            var q = invoiceRepository.GetAllvwProductInvoice().Where(x => x.IsArchive == true);
            var user = userRepository.GetByvwUserName(Helpers.Common.CurrentUser.UserName);
            if (startDate == null && endDate == null)
            {
                DateTime aDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                // Cộng thêm 1 tháng và trừ đi một ngày.
                DateTime retDateTime = aDateTime.AddMonths(1).AddDays(-1);
                startDate = aDateTime.ToString("dd/MM/yyyy");
                endDate = retDateTime.ToString("dd/MM/yyyy");
            }
            //Lọc theo ngày
            DateTime d_startDate, d_endDate;
            if (DateTime.TryParseExact(startDate, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out d_startDate))
            {
                if (DateTime.TryParseExact(endDate, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out d_endDate))
                {
                    d_endDate = d_endDate.AddHours(23).AddMinutes(59);
                    ViewBag.retDateTime = d_endDate;
                    ViewBag.aDateTime = d_startDate;
                    q = q.Where(x => x.CreatedDate >= d_startDate && x.CreatedDate <= d_endDate);
                }
            }
            var branch_list = BranchRepository.GetAllBranch().Where(x => x.ParentId != null).Select(item => new BranchViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Code = item.Code,
                DistrictId = item.DistrictId,
                CityId = item.CityId
            }).ToList();
            if (branchId != null && branchId.Value > 0)
            {
                q = q.Where(x => x.BranchId == branchId);
                branch_list = branch_list.Where(x => x.Id == branchId).ToList();
            }
            if (!string.IsNullOrEmpty(CityId))
            {
                q = q.Where(x => x.CityId == CityId);
                branch_list = branch_list.Where(x => x.CityId == CityId).ToList();
            }
            if (!string.IsNullOrEmpty(DistrictId))
            {
                q = q.Where(x => x.DistrictId == DistrictId);
                branch_list = branch_list.Where(x => x.DistrictId == DistrictId).ToList();
            }
            var model = q.Select(item => new ProductInvoiceViewModel
            {
                Id = item.Id,
                IsDeleted = item.IsDeleted,
                CreatedDate = item.CreatedDate,
                BranchId = item.BranchId,
                TotalAmount = item.TotalAmount,
                RemainingAmount = item.RemainingAmount,
                PaidAmount = item.PaidAmount,
                Month = item.Month,
                Day = item.Day,
                Year = item.Year
            }).OrderByDescending(m => m.CreatedDate).ToList();
            if (!Filters.SecurityFilter.IsAdmin() && !Filters.SecurityFilter.IsKeToan())
            {
                branch_list = branch_list.Where(x => ("," + user.DrugStore + ",").Contains("," + x.Id + ",") == true).ToList();
                model = model.Where(x => ("," + user.DrugStore + ",").Contains("," + x.BranchId + ",") == true).ToList();
            }
            ViewBag.branchList = branch_list;
            return View(model);
        }

        public ActionResult DoanhThuNgay(string single, int? year, int? month, int? quarter, int? week, string CityId, string DistrictId, string branchId, int? CreatedUserId, int? productId, string startDate, string endDate)
        {
            var q = invoiceRepository.GetAllvwInvoiceDetails().AsEnumerable().Where(x => x.IsArchive == true);
            var user = userRepository.GetByvwUserName(Helpers.Common.CurrentUser.UserName);
            year = year == null ? DateTime.Now.Year : year;
            month = month == null ? DateTime.Now.Month : month;
            single = single == null ? "month" : single;
            quarter = quarter == null ? 1 : quarter;
            CityId = string.IsNullOrEmpty(CityId) ? "" : CityId;
            DistrictId = string.IsNullOrEmpty(DistrictId) ? "" : DistrictId;
            branchId = string.IsNullOrEmpty(branchId) ? "" : branchId;
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;
            var weekdefault = calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            week = week == null ? weekdefault : week;
            if (!Filters.SecurityFilter.IsAdmin() && !Filters.SecurityFilter.IsKeToan() && string.IsNullOrEmpty(branchId))
            {
                branchId = Helpers.Common.CurrentUser.DrugStore;
            }

            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;

            ViewBag.DateRangeText = Helpers.Common.ConvertToDateRange(ref StartDate, ref EndDate, single, year.Value, month.Value, quarter.Value, ref week);
            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(startDate))
            {
                StartDate = Convert.ToDateTime(DateTime.ParseExact(startDate, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                EndDate = Convert.ToDateTime(DateTime.ParseExact(endDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)).AddHours(23).AddMinutes(59);
            }
            q = q.Where(x => x.ProductInvoiceDate >= StartDate && x.ProductInvoiceDate <= EndDate);
            if (!string.IsNullOrEmpty(branchId))
            {
                q = q.Where(item => (string.IsNullOrEmpty(branchId) || ("," + branchId + ",").Contains("," + item.BranchId + ",") == true));
            }
            if (CreatedUserId != null && CreatedUserId.Value > 0)
            {
                q = q.Where(x => x.CreatedUserId == CreatedUserId);
            }
            if (productId != null && productId.Value > 0)
            {
                q = q.Where(x => x.ProductId == productId);
            }
            if (!string.IsNullOrEmpty(CityId))
            {
                q = q.Where(x => x.CityId == CityId);
            }
            if (!string.IsNullOrEmpty(DistrictId))
            {
                q = q.Where(x => x.DistrictId == DistrictId);
            }
            var model = q.Select(item => new ProductInvoiceDetailViewModel
            {
                Id = item.Id,
                IsDeleted = item.IsDeleted,
                CreatedUserId = item.CreatedUserId,
                CreatedDate = item.CreatedDate,
                ModifiedUserId = item.ModifiedUserId,
                ModifiedDate = item.ModifiedDate,
                Amount = item.Amount,
                CategoryCode = item.CategoryCode,
                IsReturn = item.IsReturn,
                FixedDiscount = item.FixedDiscount,
                FixedDiscountAmount = item.FixedDiscountAmount,
                IrregularDiscountAmount = item.IrregularDiscountAmount,
                IrregularDiscount = item.IrregularDiscount,
                CheckPromotion = item.CheckPromotion,
                ProductType = item.ProductType,
                ProductCode = item.ProductCode,
                ProductGroup = item.ProductGroup,
                ProductInvoiceCode = item.ProductInvoiceCode,
                Price = item.Price,
                ProductId = item.ProductId,
                ProductInvoiceDate = item.ProductInvoiceDate,
                ProductInvoiceId = item.ProductInvoiceId,
                ProductName = item.ProductName,
                Quantity = item.Quantity,
                CustomerId = item.CustomerId,
                CustomerCode = item.CustomerCode,
                CustomerName = item.CustomerName,
                BranchId = item.BranchId,
                BranchName = item.BranchName,
                StaffName = item.StaffName,
                CustomerPhone = item.CustomerPhone,
                //    StaffId = item.StaffId
            }).OrderByDescending(m => m.CreatedDate).ToList();
            return View(model);
        }
        public ActionResult DoanhSoThang(string single, int? year, int? month, int? quarter, int? week, string CityId, string DistrictId, string branchId)
        {
            var q = invoiceRepository.GetAllvwProductInvoice().AsEnumerable().Where(x => x.IsArchive == true);
            var user = userRepository.GetByvwUserName(Helpers.Common.CurrentUser.UserName);
            year = year == null ? DateTime.Now.Year : year;
            month = month == null ? DateTime.Now.Month : month;
            single = single == null ? "month" : single;
            quarter = quarter == null ? 1 : quarter;
            CityId = string.IsNullOrEmpty(CityId) ? "" : CityId;
            DistrictId = string.IsNullOrEmpty(DistrictId) ? "" : DistrictId;
            branchId = string.IsNullOrEmpty(branchId) ? "" : branchId;
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;
            var weekdefault = calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            week = week == null ? weekdefault : week;
            if (!Filters.SecurityFilter.IsAdmin() && !Filters.SecurityFilter.IsKeToan() && string.IsNullOrEmpty(branchId))
            {
                branchId = Helpers.Common.CurrentUser.DrugStore;
            }

            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;

            ViewBag.DateRangeText = Helpers.Common.ConvertToDateRange(ref StartDate, ref EndDate, single, year.Value, month.Value, quarter.Value, ref week);

            q = q.Where(x => x.CreatedDate >= StartDate && x.CreatedDate <= EndDate);
            if (!string.IsNullOrEmpty(branchId))
            {
                q = q.Where(item => (string.IsNullOrEmpty(branchId) || ("," + branchId + ",").Contains("," + item.BranchId + ",") == true));
            }
            if (!string.IsNullOrEmpty(CityId))
            {
                q = q.Where(x => x.CityId == CityId);
            }
            if (!string.IsNullOrEmpty(DistrictId))
            {
                q = q.Where(x => x.DistrictId == DistrictId);
            }
            var model = q.Select(item => new ProductInvoiceViewModel
            {
                Id = item.Id,
                IsDeleted = item.IsDeleted,
                CreatedUserId = item.CreatedUserId,
                CreatedDate = item.CreatedDate,
                ModifiedUserId = item.ModifiedUserId,
                ModifiedDate = item.ModifiedDate,
                FixedDiscount = item.FixedDiscount,
                IrregularDiscount = item.IrregularDiscount,
                CityId = item.CityId,
                Code = item.Code,
                DistrictId = item.DistrictId,
                TotalAmount = item.TotalAmount,

                CustomerId = item.CustomerId,
                CustomerCode = item.CustomerCode,
                CustomerName = item.CustomerName,
                BranchId = item.BranchId,
                BranchName = item.BranchName,
                CustomerPhone = item.CustomerPhone,
                //    StaffId = item.StaffId
            }).OrderByDescending(m => m.CreatedDate).ToList();
            return View(model);
        }
        public ActionResult Sale_BaoCaoXuatKho()
        {

            return View();
        }
        public PartialViewResult _GetListSale_BCXK(string single, int? year, int? month, int? quarter, int? week, string CityId, string DistrictId, string branchId, int? WarehouseId)
        {
            year = year == null ? DateTime.Now.Year : year;
            month = month == null ? DateTime.Now.Month : month;
            single = single == null ? "month" : single;
            quarter = quarter == null ? 1 : quarter;
            CityId = string.IsNullOrEmpty(CityId) ? "" : CityId;
            DistrictId = string.IsNullOrEmpty(DistrictId) ? "" : DistrictId;
            branchId = string.IsNullOrEmpty(branchId) ? "" : branchId;
            WarehouseId = WarehouseId == null ? 0 : WarehouseId;
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;
            var weekdefault = calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            week = week == null ? weekdefault : week;
            if (!Filters.SecurityFilter.IsAdmin() && !Filters.SecurityFilter.IsKeToan() && string.IsNullOrEmpty(branchId))
            {
                branchId = Helpers.Common.CurrentUser.DrugStore;
            }

            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;

            ViewBag.DateRangeText = Helpers.Common.ConvertToDateRange(ref StartDate, ref EndDate, single, year.Value, month.Value, quarter.Value, ref week);

            var data = new List<Sale_BaoCaoXuatKhoViewModel>();
            data = SqlHelper.QuerySP<Erp.BackOffice.Sale.Models.Sale_BaoCaoXuatKhoViewModel>("spSale_BaoCaoXuatKho", new
            {
                StartDate = StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                WarehouseId = WarehouseId,
                branchId = branchId,
                CityId = CityId,
                DistrictId = DistrictId
            }).ToList();
            ViewBag.StartDate = StartDate.ToString("dd/MM/yyyy");
            ViewBag.EndDate = EndDate.ToString("dd/MM/yyyy");
            return PartialView(data);
        }

        public ActionResult Sale_BaoCaoNhapXuatTon()
        {
            return View();
        }

        public PartialViewResult _GetListSale_BCNXT(string single, int? year, int? month, int? quarter, int? week, string CityId, string DistrictId, string branchId, int? WarehouseId)
        {
            year = year == null ? DateTime.Now.Year : year;
            month = month == null ? DateTime.Now.Month : month;
            single = single == null ? "month" : single;
            quarter = quarter == null ? 1 : quarter;
            CityId = string.IsNullOrEmpty(CityId) ? "" : CityId;
            DistrictId = string.IsNullOrEmpty(DistrictId) ? "" : DistrictId;
            branchId = string.IsNullOrEmpty(branchId) ? "" : branchId;
            WarehouseId = WarehouseId == null ? 0 : WarehouseId;
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;
            var weekdefault = calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            week = week == null ? weekdefault : week;
            if (!Filters.SecurityFilter.IsAdmin() && !Filters.SecurityFilter.IsKeToan() && string.IsNullOrEmpty(branchId))
            {
                branchId = Helpers.Common.CurrentUser.DrugStore;
            }

            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;

            ViewBag.DateRangeText = Helpers.Common.ConvertToDateRange(ref StartDate, ref EndDate, single, year.Value, month.Value, quarter.Value, ref week);

            var data = SqlHelper.QuerySP<Sale_BaoCaoNhapXuatTonViewModel>("spSale_BaoCaoNhapXuatTon", new
            {
                StartDate = StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                WarehouseId = WarehouseId,
                branchId = branchId,
                CityId = CityId,
                DistrictId = DistrictId
            }).ToList();
            ViewBag.StartDate = StartDate.ToString("dd/MM/yyyy");
            ViewBag.EndDate = EndDate.ToString("dd/MM/yyyy");
            return PartialView(data);
        }

        public ActionResult Sale_BaoCaoTonKho()
        {
            return View();
        }

        public PartialViewResult _GetListSale_BCTK(string CityId, string DistrictId, string BranchId, int? WarehouseId, string ProductGroup, string CategoryCode, string Manufacturer)
        {
            BranchId = BranchId == null ? "" : BranchId;
            CityId = CityId == null ? "" : CityId;
            DistrictId = DistrictId == null ? "" : DistrictId;
            WarehouseId = WarehouseId == null ? 0 : WarehouseId;
            ProductGroup = ProductGroup == null ? "" : ProductGroup;
            CategoryCode = CategoryCode == null ? "" : CategoryCode;
            Manufacturer = Manufacturer == null ? "" : Manufacturer;
            if (!Filters.SecurityFilter.IsAdmin() && !Filters.SecurityFilter.IsKeToan() && string.IsNullOrEmpty(BranchId))
            {
                BranchId = Helpers.Common.CurrentUser.DrugStore;
            }

            var data = SqlHelper.QuerySP<Sale_BaoCaoTonKhoViewModel>("spSale_BaoCaoTonKho", new
            {
                BranchId = BranchId,
                WarehouseId = WarehouseId,
                ProductGroup = ProductGroup,
                CategoryCode = CategoryCode,
                Manufacturer = Manufacturer,
                CityId = CityId,
                DistrictId = DistrictId
            }).ToList();
            return PartialView(data);
        }
        public ActionResult Sale_BaoCaoCongNoTongHop()
        {
            return View();
        }

        public PartialViewResult _GetListSale_BCCNTH(string StartDate, string EndDate, string BranchId, int? SalerId)
        {
            StartDate = StartDate == null ? "" : StartDate;
            EndDate = EndDate == null ? "" : EndDate;
            BranchId = BranchId == null ? "" : BranchId;
            SalerId = SalerId == null ? 0 : SalerId;

            DateTime aDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime retDateTime = aDateTime.AddMonths(1).AddDays(-1);
            if (StartDate == null && EndDate == null && string.IsNullOrEmpty(BranchId))
            {
                StartDate = aDateTime.ToString("dd/MM/yyyy");
                EndDate = retDateTime.ToString("dd/MM/yyyy");
            }
            var d_startDate = (!string.IsNullOrEmpty(StartDate) ? DateTime.ParseExact(StartDate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd HH:mm:ss") : "");
            var d_endDate = (!string.IsNullOrEmpty(EndDate) ? DateTime.ParseExact(EndDate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd HH:mm:ss") : "");

            var data = SqlHelper.QuerySP<Sale_BaoCaoCongNoTongHopViewModel>("spSale_BaoCaoCongNoTongHop", new
            {
                StartDate = d_startDate,
                EndDate = d_endDate,
                BranchId = BranchId,
                SalerId = SalerId
            }).ToList();
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;
            return PartialView(data);
        }

        public ActionResult Sale_LiabilitiesDrugStore(string single, int? year, int? month, int? quarter, int? week, int? branchId, string CityId, string DistrictId)
        {
            single = single == null ? "month" : single;
            year = year == null ? DateTime.Now.Year : year;
            month = month == null ? DateTime.Now.Month : month;
            quarter = quarter == null ? 1 : quarter;
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;
            var weekdefault = calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            week = week == null ? weekdefault : week;
            string BranchId = branchId == null ? "" : branchId.Value.ToString();
            CityId = CityId == null ? "" : CityId;
            DistrictId = DistrictId == null ? "" : DistrictId;
            if (!Filters.SecurityFilter.IsAdmin() && !Filters.SecurityFilter.IsKeToan() && string.IsNullOrEmpty(BranchId))
            {
                BranchId = Helpers.Common.CurrentUser.DrugStore;
            }

            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;

            ViewBag.DateRangeText = Helpers.Common.ConvertToDateRange(ref StartDate, ref EndDate, single, year.Value, month.Value, quarter.Value, ref week);

            var d_startDate = DateTime.ParseExact(StartDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null).ToString("yyyy-MM-dd HH:mm:ss");
            var d_endDate = DateTime.ParseExact(EndDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null).ToString("yyyy-MM-dd HH:mm:ss");

            var data = SqlHelper.QuerySP<ProductInvoiceViewModel>("spSale_LiabilitiesDrugStore_haopd", new
            {
                StartDate = d_startDate,
                EndDate = d_endDate,
                branchId = BranchId,
                CityId = CityId,
                DistrictId = DistrictId
            }).ToList();
            return View(data);
        }

        public ActionResult Sale_BCDoanhSoTheoSP(string single, int? year, int? month, int? quarter, int? week, string CityId, string DistrictId, string branchId, int? CreatedUserId, int? productId)
        {
            year = year == null ? DateTime.Now.Year : year;
            month = month == null ? DateTime.Now.Month : month;
            single = single == null ? "month" : single;
            quarter = quarter == null ? 1 : quarter;
            CityId = string.IsNullOrEmpty(CityId) ? "" : CityId;
            DistrictId = string.IsNullOrEmpty(DistrictId) ? "" : DistrictId;
            branchId = string.IsNullOrEmpty(branchId) ? "" : branchId;
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;
            var weekdefault = calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            week = week == null ? weekdefault : week;
            if (!Filters.SecurityFilter.IsAdmin() && !Filters.SecurityFilter.IsKeToan() && string.IsNullOrEmpty(branchId))
            {
                branchId = Helpers.Common.CurrentUser.DrugStore;
            }

            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;

            ViewBag.DateRangeText = Helpers.Common.ConvertToDateRange(ref StartDate, ref EndDate, single, year.Value, month.Value, quarter.Value, ref week);

            var q = invoiceRepository.GetAllvwInvoiceDetails().AsEnumerable().Where(x => x.IsArchive == true);
            if (!string.IsNullOrEmpty(branchId))
            {
                q = q.Where(item => (string.IsNullOrEmpty(branchId) || ("," + branchId + ",").Contains("," + item.BranchId + ",") == true));
            }
            if (CreatedUserId != null && CreatedUserId.Value > 0)
            {
                q = q.Where(x => x.CreatedUserId == CreatedUserId);
            }
            if (productId != null && productId.Value > 0)
            {
                q = q.Where(x => x.ProductId == productId);
            }
            if (!string.IsNullOrEmpty(CityId))
            {
                q = q.Where(x => x.CityId == CityId);
            }
            if (!string.IsNullOrEmpty(DistrictId))
            {
                q = q.Where(x => x.DistrictId == DistrictId);
            }
            //Lọc theo ngày
            q = q.Where(x => x.ProductInvoiceDate >= StartDate && x.ProductInvoiceDate <= EndDate);

            var model = q.Select(item => new ProductInvoiceDetailViewModel
            {
                Id = item.Id,
                BranchId = item.BranchId,
                ProductId = item.ProductId,
                Amount = item.Amount,
                BranchName = item.BranchName,
                ProductName = item.ProductName,
                ProductCode = item.ProductCode
            }).ToList();
            //if (branchId != null && branchId.Value > 0)
            //{
            //    var drug_store = SelectListHelper.GetSelectList_DepartmentbyBranch(branchId, null, null);
            //    model = model.Where(id1 => drug_store.Any(id2 => id2.Value == id1.BranchId.ToString())).ToList();
            //}
            var list_branch = model.GroupBy(x => new { x.BranchId, x.BranchName })
                .Select(x => new BranchViewModel
                {
                    Id = x.Key.BranchId,
                    Name = x.Key.BranchName
                }).ToList();
            ViewBag.branchList = list_branch;

            var product_list = model.GroupBy(x => new { x.ProductId, x.ProductCode, x.ProductName })
                .Select(x => new ProductViewModel
                {
                    Id = x.Key.ProductId.Value,
                    Name = x.Key.ProductName,
                    Code = x.Key.ProductCode
                }).ToList();
            ViewBag.productList = product_list;
            return View(model);
        }

        public PartialViewResult ChartInboundAndOutbound(string single, int? year, int? month, int? quarter, int? week, string CityId, string DistrictId, string branchId, int? WarehouseId)
        {
            year = year == null ? DateTime.Now.Year : year;
            month = month == null ? DateTime.Now.Month : month;
            single = single == null ? "month" : single;
            quarter = quarter == null ? 1 : quarter;
            CityId = string.IsNullOrEmpty(CityId) ? "" : CityId;
            DistrictId = string.IsNullOrEmpty(DistrictId) ? "" : DistrictId;
            branchId = string.IsNullOrEmpty(branchId) ? "" : branchId;
            WarehouseId = WarehouseId == null ? 0 : WarehouseId;
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;
            var weekdefault = calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            week = week == null ? weekdefault : week;
            if (!Filters.SecurityFilter.IsAdmin() && !Filters.SecurityFilter.IsKeToan() && string.IsNullOrEmpty(branchId))
            {
                branchId = Helpers.Common.CurrentUser.DrugStore;
            }
            var jsonData = new List<ChartItem>();
            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;

            ViewBag.DateRangeText = Helpers.Common.ConvertToDateRange(ref StartDate, ref EndDate, single, year.Value, month.Value, quarter.Value, ref week);

            var data = SqlHelper.QuerySP<Sale_BaoCaoNhapXuatTonViewModel>("spSale_BaoCaoNhapXuatTon", new
            {
                StartDate = StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                WarehouseId = WarehouseId,
                branchId = branchId,
                CityId = CityId,
                DistrictId = DistrictId
            }).ToList();

            foreach (var item in data.GroupBy(x => x.ProductCode))
            {
                var obj = new Erp.BackOffice.Sale.Models.ChartItem();
                obj.label = item.Key;
                obj.data = item.Sum(x => x.Last_InboundQuantity);
                obj.data2 = item.Sum(x => x.Last_OutboundQuantity);
                jsonData.Add(obj);
            }
            //ViewBag.StartDate = StartDate.ToString("dd/MM/yyyy");
            //ViewBag.EndDate = EndDate.ToString("dd/MM/yyyy");
            string json = JsonConvert.SerializeObject(jsonData);
            ViewBag.json = json;
            return PartialView(data);
        }

        //public ActionResult PrintDTNgay(string startDate, string endDate, string CityId, string DistrictId, int? branchId, bool ExportExcel = false)
        //{
        //    var model = new TemplatePrintViewModel();
        //    //lấy logo công ty
        //    var logo = Erp.BackOffice.Helpers.Common.GetSetting("LogoCompany");
        //    var company = Erp.BackOffice.Helpers.Common.GetSetting("companyName");
        //    var address = Erp.BackOffice.Helpers.Common.GetSetting("addresscompany");
        //    var phone = Erp.BackOffice.Helpers.Common.GetSetting("phonecompany");
        //    var fax = Erp.BackOffice.Helpers.Common.GetSetting("faxcompany");
        //    var bankcode = Erp.BackOffice.Helpers.Common.GetSetting("bankcode");
        //    var bankname = Erp.BackOffice.Helpers.Common.GetSetting("bankname");
        //    var ImgLogo = "<div class=\"logo\"><img src=" + logo + " height=\"73\" /></div>";
        //    //lấy hóa đơn.
        //    var q = invoiceRepository.GetAllvwProductInvoice().Where(x => x.IsArchive == true);
        //    var user = userRepository.GetByvwUserName(Helpers.Common.CurrentUser.UserName);
        //    if (startDate == null && endDate == null)
        //    {
        //        DateTime aDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        //        // Cộng thêm 1 tháng và trừ đi một ngày.
        //        DateTime retDateTime = aDateTime.AddMonths(1).AddDays(-1);
        //        startDate = aDateTime.ToString("dd/MM/yyyy");
        //        endDate = retDateTime.ToString("dd/MM/yyyy");
        //    }
        //    //Lọc theo ngày
        //    DateTime d_startDate, d_endDate;
        //    if (DateTime.TryParseExact(startDate, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out d_startDate))
        //    {
        //        if (DateTime.TryParseExact(endDate, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out d_endDate))
        //        {
        //            d_endDate = d_endDate.AddHours(23).AddMinutes(59);
        //            ViewBag.retDateTime = d_endDate;
        //            ViewBag.aDateTime = d_startDate;
        //            q = q.Where(x => x.CreatedDate >= d_startDate && x.CreatedDate <= d_endDate);
        //        }
        //    }
        //    var branch_list = BranchRepository.GetAllBranch().Where(x => x.ParentId != null).Select(item => new BranchViewModel
        //    {
        //        Id = item.Id,
        //        Name = item.Name,
        //        Code = item.Code,
        //        DistrictId = item.DistrictId,
        //        CityId = item.CityId
        //    }).ToList();
        //    if (branchId != null && branchId.Value > 0)
        //    {
        //        q = q.Where(x => x.BranchId == branchId);
        //        branch_list = branch_list.Where(x => x.Id == branchId).ToList();
        //    }
        //    if (!string.IsNullOrEmpty(CityId))
        //    {
        //        q = q.Where(x => x.CityId == CityId);
        //        branch_list = branch_list.Where(x => x.CityId == CityId).ToList();
        //    }
        //    if (!string.IsNullOrEmpty(DistrictId))
        //    {
        //        q = q.Where(x => x.DistrictId == DistrictId);
        //        branch_list = branch_list.Where(x => x.DistrictId == DistrictId).ToList();
        //    }
        //    var detail = q.Select(item => new ProductInvoiceViewModel
        //    {
        //        Id = item.Id,
        //        IsDeleted = item.IsDeleted,
        //        CreatedDate = item.CreatedDate,
        //        BranchId = item.BranchId,
        //        TotalAmount = item.TotalAmount,
        //        RemainingAmount = item.RemainingAmount,
        //        PaidAmount = item.PaidAmount,
        //        Month = item.Month,
        //        Day = item.Day,
        //        Year = item.Year
        //    }).OrderByDescending(m => m.CreatedDate).ToList();
        //    if (!Filters.SecurityFilter.IsAdmin() && !Filters.SecurityFilter.IsKeToan())
        //    {
        //        branch_list = branch_list.Where(x => ("," + user.DrugStore + ",").Contains("," + x.Id + ",") == true).ToList();
        //    }

        //    //lấy template phiếu xuất.
        //    var template = templatePrintRepository.GetAllTemplatePrint().Where(x => x.Code.Contains("ProductInvoice")).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
        //    //truyền dữ liệu vào template.
        //    model.Content = template.Content;

        //    model.Content = model.Content.Replace("{DataTable}", buildHtmlDetailList_PrintDTNgay(detail, d_startDate, d_endDate));
        //    model.Content = model.Content.Replace("{System.Logo}", ImgLogo);
        //    model.Content = model.Content.Replace("{System.CompanyName}", company);
        //    model.Content = model.Content.Replace("{System.AddressCompany}", address);
        //    model.Content = model.Content.Replace("{System.PhoneCompany}", phone);
        //    model.Content = model.Content.Replace("{System.Fax}", fax);
        //    model.Content = model.Content.Replace("{System.BankCodeCompany}", bankcode);
        //    model.Content = model.Content.Replace("{System.BankNameCompany}", bankname);
        //    model.Content = model.Content.Replace("{PrintDate}", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));

        //    if (ExportExcel)
        //    {
        //        Response.AppendHeader("content-disposition", "attachment;filename=" + "DoanhThuTheoNgay" + DateTime.Now.ToString("yyyyMMdd") + ".xls");
        //        Response.Charset = "";
        //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //        Response.Write(model.Content);
        //        Response.End();
        //    }
        //    return View(model);
        //}

        //string buildHtmlDetailList_PrintDTNgay(List<ProductInvoiceViewModel> detailList, DateTime d_startDate, DateTime d_endDate)
        //{
        //    decimal? tong_tien = 0;

        //    //Tạo table html chi tiết phiếu xuất
        //    string detailLists = "<table class=\"invoice-detail\">\r\n";
        //    detailLists += "<thead>\r\n";
        //    detailLists += "	<tr>\r\n";

        //    detailLists += "		<th>STT</th>\r\n";
        //    detailLists += "		<th>Nhà thuốc</th>\r\n";
        //    for (DateTime dt = d_startDate; dt <= d_endDate; dt = dt.AddDays(1))
        //    {
        //        detailLists += "		<th>" + dt.ToString("dd/MM/yyyy") + "</th>\r\n";
        //    }
        //    detailLists += "		<th>Số ngày</th>\r\n";
        //    detailLists += "		<th>Tổng tiền</th>\r\n";
        //    detailLists += "	</tr>\r\n";
        //    detailLists += "</thead>\r\n";
        //    detailLists += "<tbody>\r\n";
        //    var index = 1;

        //    foreach (var item in detailList)
        //    {
        //        decimal? subTotal = item.Quantity * item.Price.Value;

        //        tong_tien += subTotal;
        //        detailLists += "<tr>\r\n"
        //        + "<td class=\"text-center orderNo\">" + (index++) + "</td>\r\n"
        //        + "<td class=\"text-left code_product\">" + item.BranchName + "</td>\r\n";
        //        for (DateTime dt = d_startDate; dt <= d_endDate; dt = dt.AddDays(1))
        //        {
        //            detailLists += "<td class=\"text-left \">" + item.ProductName + "</td>\r\n";
        //        }
        //        detailLists += "<td class=\"text-right\">" + item.LoCode + "</td>\r\n"
        //          + "<td class=\"text-right\">" + (item.ExpiryDate.HasValue ? item.ExpiryDate.Value.ToString("dd-MM-yyyy") : "") + "</td>\r\n"
        //          + "<td class=\"text-center\">" + item.Unit + "</td>\r\n"
        //          + "<td class=\"text-right orderNo\">" + Erp.BackOffice.Helpers.Common.PhanCachHangNgan(item.Quantity) + "</td>\r\n"
        //          + "<td class=\"text-right code_product\">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(item.Price, null) + "</td>\r\n"
        //          + "<td class=\"text-right chiet_khau\">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(subTotal, null) + "</td>\r\n"
        //          + "</tr>\r\n";
        //    }
        //    detailLists += "</tbody>\r\n";
        //    detailLists += "<tfoot style=\"font-weight:bold\">\r\n";
        //    detailLists += "<tr><td colspan=\"8\" class=\"text-right\">Tổng cộng</td><td class=\"text-right\">"
        //                 + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(tong_tien, null)
        //                 + "</td></tr>\r\n";
        //    if (model.TaxFee > 0)
        //    {
        //        var vat = tong_tien * Convert.ToDecimal(model.TaxFee);
        //        var tong = tong_tien + vat;
        //        detailLists += "<tr><td colspan=\"8\" class=\"text-right\">VAT (" + model.TaxFee + "%)</td><td class=\"text-right\">"
        //                + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(vat, null)
        //                + "</td></tr>\r\n";
        //        detailLists += "<tr><td colspan=\"8\" class=\"text-right\">Tổng tiền cần thanh toán</td><td class=\"text-right\">"
        //            + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(tong, null)
        //            + "</td></tr>\r\n";
        //    }


        //    detailLists += "<tr><td colspan=\"8\" class=\"text-right\">Đã thanh toán</td><td class=\"text-right\">"
        //                + (model.PaidAmount > 0 ? Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(model.PaidAmount, null) : "0")
        //                + "</td></tr>\r\n";
        //    detailLists += "<tr><td colspan=\"8\" class=\"text-right\">Còn lại phải thu</td><td class=\"text-right\">"
        //                + (model.RemainingAmount > 0 ? Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(model.RemainingAmount, null) : "0")
        //                + "</td></tr>\r\n";
        //    detailLists += "</tfoot>\r\n</table>\r\n";

        //    return detailLists;
        //}

        public ActionResult PrintBCXK(string single, int? year, int? month, int? quarter, int? week, string CityId, string DistrictId, string branchId, int? WarehouseId, bool ExportExcel = false)
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
            year = year == null ? DateTime.Now.Year : year;
            month = month == null ? DateTime.Now.Month : month;
            single = single == null ? "month" : single;
            quarter = quarter == null ? 1 : quarter;
            CityId = string.IsNullOrEmpty(CityId) ? "" : CityId;
            DistrictId = string.IsNullOrEmpty(DistrictId) ? "" : DistrictId;
            branchId = string.IsNullOrEmpty(branchId) ? "" : branchId;
            WarehouseId = WarehouseId == null ? 0 : WarehouseId;
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;
            var weekdefault = calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            week = week == null ? weekdefault : week;
            if (!Filters.SecurityFilter.IsAdmin() && !Filters.SecurityFilter.IsKeToan() && string.IsNullOrEmpty(branchId))
            {
                branchId = Helpers.Common.CurrentUser.DrugStore;
            }

            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;

            ViewBag.DateRangeText = Helpers.Common.ConvertToDateRange(ref StartDate, ref EndDate, single, year.Value, month.Value, quarter.Value, ref week);

            var data = new List<Sale_BaoCaoXuatKhoViewModel>();
            data = SqlHelper.QuerySP<Erp.BackOffice.Sale.Models.Sale_BaoCaoXuatKhoViewModel>("spSale_BaoCaoXuatKho", new
            {
                StartDate = StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                WarehouseId = WarehouseId,
                branchId = branchId,
                CityId = CityId,
                DistrictId = DistrictId
            }).ToList();
            ViewBag.StartDate = StartDate.ToString("dd/MM/yyyy");
            ViewBag.EndDate = EndDate.ToString("dd/MM/yyyy");

            //lấy template phiếu xuất.
            var template = templatePrintRepository.GetAllTemplatePrint().Where(x => x.Code.Contains("PrintBCXK")).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            //truyền dữ liệu vào template.
            model.Content = template.Content;

            model.Content = model.Content.Replace("{DataTable}", buildHtmlDetailList_PrintBCXK(data));
            model.Content = model.Content.Replace("{System.Logo}", ImgLogo);
            model.Content = model.Content.Replace("{System.CompanyName}", company);
            model.Content = model.Content.Replace("{System.AddressCompany}", address);
            model.Content = model.Content.Replace("{System.PhoneCompany}", phone);
            model.Content = model.Content.Replace("{System.Fax}", fax);
            model.Content = model.Content.Replace("{System.BankCodeCompany}", bankcode);
            model.Content = model.Content.Replace("{System.BankNameCompany}", bankname);
            model.Content = model.Content.Replace("{PrintDate}", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));

            if (ExportExcel)
            {
                Response.AppendHeader("content-disposition", "attachment;filename=" + "BaoCaoXuatKho" + DateTime.Now.ToString("yyyyMMdd") + ".xls");
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.Write(model.Content);
                Response.End();
            }
            return View(model);
        }
        string buildHtmlDetailList_PrintBCXK(List<Sale_BaoCaoXuatKhoViewModel> detailList)
        {
            //Tạo table html chi tiết phiếu xuất
            string detailLists = "<table class=\"invoice-detail\">\r\n";
            detailLists += "<thead>\r\n";
            detailLists += "	<tr>\r\n";
            detailLists += "		<th>STT</th>\r\n";
            detailLists += "		<th>Mã SP</th>\r\n";
            detailLists += "		<th>Tên SP</th>\r\n";
            detailLists += "		<th>Danh mục</th>\r\n";
            detailLists += "		<th>Nhà sản xuất</th>\r\n";
            detailLists += "		<th>Kho</th>\r\n";
            detailLists += "		<th>Số Lô</th>\r\n";
            detailLists += "		<th>Hạn dùng</th>\r\n";
            detailLists += "		<th>Đơn giá</th>\r\n";
            detailLists += "		<th>ĐVT</th>\r\n";
            detailLists += "		<th>Xuất bán</th>\r\n";
            detailLists += "		<th>Xuất vận chuyển</th>\r\n";
            detailLists += "		<th>Xuất sử dụng</th>\r\n";
            detailLists += "		<th>Tổng xuất</th>\r\n";
            detailLists += "	</tr>\r\n";
            detailLists += "</thead>\r\n";
            detailLists += "<tbody>\r\n";
            var index = 1;

            foreach (var item in detailList)
            {
                var subTotal = item.invoice + item._internal + item.service;
                detailLists += "<tr>\r\n"
                + "<td class=\"text-center orderNo\">" + (index++) + "</td>\r\n"
                + "<td class=\"text-left code_product\">" + item.ProductCode + "</td>\r\n"
                + "<td class=\"text-left \">" + item.ProductName + "</td>\r\n"
                + "<td class=\"text-left \">" + item.CategoryCode + "</td>\r\n"
                + "<td class=\"text-left \">" + item.Manufacturer + "</td>\r\n"
                + "<td class=\"text-left \">" + item.WarehouseName + "</td>\r\n"
                + "<td class=\"text-right\">" + item.LoCode + "</td>\r\n"
                + "<td class=\"text-right\">" + (item.ExpiryDate.HasValue ? item.ExpiryDate.Value.ToString("dd-MM-yyyy") : "") + "</td>\r\n"
                + "<td class=\"text-right orderNo\">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(item.Price, null) + "</td>\r\n"
                + "<td class=\"text-center\">" + item.Unit + "</td>\r\n"
                + "<td class=\"text-right orderNo\">" + Erp.BackOffice.Helpers.Common.PhanCachHangNgan(item.invoice) + "</td>\r\n"
            + "<td class=\"text-right orderNo\">" + Erp.BackOffice.Helpers.Common.PhanCachHangNgan(item._internal) + "</td>\r\n"
              + "<td class=\"text-right orderNo\">" + Erp.BackOffice.Helpers.Common.PhanCachHangNgan(item.service) + "</td>\r\n"
                + "<td class=\"text-right orderNo\">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(subTotal, null) + "</td>\r\n"
                + "</tr>\r\n";
            }
            detailLists += "</tbody>\r\n";
            detailLists += "<tfoot style=\"font-weight:bold\">\r\n";
            detailLists += "<tr><td colspan=\"10\" class=\"text-right\">Tổng cộng</td><td class=\"text-right\">"
                         + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(detailList.Sum(x => x.invoice), null)
                         + "</td><td class=\"text-right\">"
                         + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(detailList.Sum(x => x._internal), null)
                         + "</td><td class=\"text-right\">"
                         + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(detailList.Sum(x => x.service), null)
                         + "</td><td class=\"text-right\">"
                         + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(detailList.Sum(x => x.service + x._internal + x.invoice), null)
                         + "</tr>\r\n";
            detailLists += "</tfoot>\r\n</table>\r\n";

            return detailLists;
        }

        public ActionResult PrintBCTK(string CityId, string DistrictId, string BranchId, int? WarehouseId, string ProductGroup, string CategoryCode, string Manufacturer, bool ExportExcel = false)
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
            BranchId = BranchId == null ? "" : BranchId;
            CityId = CityId == null ? "" : CityId;
            DistrictId = DistrictId == null ? "" : DistrictId;
            WarehouseId = WarehouseId == null ? 0 : WarehouseId;
            ProductGroup = ProductGroup == null ? "" : ProductGroup;
            CategoryCode = CategoryCode == null ? "" : CategoryCode;
            Manufacturer = Manufacturer == null ? "" : Manufacturer;
            if (!Filters.SecurityFilter.IsAdmin() && !Filters.SecurityFilter.IsKeToan() && string.IsNullOrEmpty(BranchId))
            {
                BranchId = Helpers.Common.CurrentUser.DrugStore;
            }

            var data = SqlHelper.QuerySP<Sale_BaoCaoTonKhoViewModel>("spSale_BaoCaoTonKho", new
            {
                BranchId = BranchId,
                WarehouseId = WarehouseId,
                ProductGroup = ProductGroup,
                CategoryCode = CategoryCode,
                Manufacturer = Manufacturer,
                CityId = CityId,
                DistrictId = DistrictId
            }).ToList();

            //lấy template phiếu xuất.
            var template = templatePrintRepository.GetAllTemplatePrint().Where(x => x.Code.Contains("PrintBCTK")).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            //truyền dữ liệu vào template.
            model.Content = template.Content;

            model.Content = model.Content.Replace("{DataTable}", buildHtmlDetailList_PrintBCTK(data));
            model.Content = model.Content.Replace("{System.Logo}", ImgLogo);
            model.Content = model.Content.Replace("{System.CompanyName}", company);
            model.Content = model.Content.Replace("{System.AddressCompany}", address);
            model.Content = model.Content.Replace("{System.PhoneCompany}", phone);
            model.Content = model.Content.Replace("{System.Fax}", fax);
            model.Content = model.Content.Replace("{System.BankCodeCompany}", bankcode);
            model.Content = model.Content.Replace("{System.BankNameCompany}", bankname);
            model.Content = model.Content.Replace("{PrintDate}", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));

            if (ExportExcel)
            {
                Response.AppendHeader("content-disposition", "attachment;filename=" + "BaoCaoTonKho" + DateTime.Now.ToString("yyyyMMdd") + ".xls");
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.Write(model.Content);
                Response.End();
            }
            return View(model);
        }
        string buildHtmlDetailList_PrintBCTK(List<Sale_BaoCaoTonKhoViewModel> detailList)
        {
            //Tạo table html chi tiết phiếu xuất
            string detailLists = "<table class=\"invoice-detail\">\r\n";
            detailLists += "<thead>\r\n";
            detailLists += "	<tr>\r\n";
            detailLists += "		<th>STT</th>\r\n";
            detailLists += "		<th>Mã SP</th>\r\n";
            detailLists += "		<th>Tên SP</th>\r\n";
            detailLists += "		<th>Danh mục</th>\r\n";
            detailLists += "		<th>Nhà sản xuất</th>\r\n";
            detailLists += "		<th>Kho</th>\r\n";
            detailLists += "		<th>Số Lô</th>\r\n";
            detailLists += "		<th>Hạn dùng</th>\r\n";
            detailLists += "		<th>ĐVT</th>\r\n";
            detailLists += "		<th>Số lượng</th>\r\n";
            detailLists += "	</tr>\r\n";
            detailLists += "</thead>\r\n";
            detailLists += "<tbody>\r\n";
            var index = 1;

            foreach (var item in detailList)
            {
                detailLists += "<tr>\r\n"
                + "<td class=\"text-center orderNo\">" + (index++) + "</td>\r\n"
                + "<td class=\"text-left code_product\">" + item.ProductCode + "</td>\r\n"
                + "<td class=\"text-left \">" + item.ProductName + "</td>\r\n"
                + "<td class=\"text-left \">" + item.CategoryCode + "</td>\r\n"
                + "<td class=\"text-left \">" + item.Manufacturer + "</td>\r\n"
                + "<td class=\"text-left \">" + item.WarehouseName + "</td>\r\n"
                + "<td class=\"text-right\">" + item.LoCode + "</td>\r\n"
                + "<td class=\"text-right\">" + (item.ExpiryDate.HasValue ? item.ExpiryDate.Value.ToString("dd-MM-yyyy") : "") + "</td>\r\n"
                + "<td class=\"text-center\">" + item.ProductUnit + "</td>\r\n"
                + "<td class=\"text-right orderNo\">" + Erp.BackOffice.Helpers.Common.PhanCachHangNgan(item.Quantity) + "</td>\r\n"
                + "</tr>\r\n";
            }
            detailLists += "</tbody>\r\n";
            detailLists += "<tfoot style=\"font-weight:bold\">\r\n";
            detailLists += "<tr><td colspan=\"9\" class=\"text-right\">Tổng cộng</td><td class=\"text-right\">"
                         + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(detailList.Sum(x => x.Quantity), null)
                         + "</tr>\r\n";
            detailLists += "</tfoot>\r\n</table>\r\n";

            return detailLists;
        }

        public ActionResult PrintBCNXT(string single, int? year, int? month, int? quarter, int? week, string CityId, string DistrictId, string branchId, int? WarehouseId, bool ExportExcel = false)
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
            year = year == null ? DateTime.Now.Year : year;
            month = month == null ? DateTime.Now.Month : month;
            single = single == null ? "month" : single;
            quarter = quarter == null ? 1 : quarter;
            CityId = string.IsNullOrEmpty(CityId) ? "" : CityId;
            DistrictId = string.IsNullOrEmpty(DistrictId) ? "" : DistrictId;
            branchId = string.IsNullOrEmpty(branchId) ? "" : branchId;
            WarehouseId = WarehouseId == null ? 0 : WarehouseId;
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;
            var weekdefault = calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            week = week == null ? weekdefault : week;
            if (!Filters.SecurityFilter.IsAdmin() && !Filters.SecurityFilter.IsKeToan() && string.IsNullOrEmpty(branchId))
            {
                branchId = Helpers.Common.CurrentUser.DrugStore;
            }

            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;

            ViewBag.DateRangeText = Helpers.Common.ConvertToDateRange(ref StartDate, ref EndDate, single, year.Value, month.Value, quarter.Value, ref week);

            var data = SqlHelper.QuerySP<Sale_BaoCaoNhapXuatTonViewModel>("spSale_BaoCaoNhapXuatTon", new
            {
                StartDate = StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                WarehouseId = WarehouseId,
                branchId = branchId,
                CityId = CityId,
                DistrictId = DistrictId
            }).ToList();
            //lấy template phiếu xuất.
            var template = templatePrintRepository.GetAllTemplatePrint().Where(x => x.Code.Contains("PrintBCNXT")).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            //truyền dữ liệu vào template.
            model.Content = template.Content;

            model.Content = model.Content.Replace("{DataTable}", buildHtmlDetailList_PrintBCNXT(data));
            model.Content = model.Content.Replace("{System.Logo}", ImgLogo);
            model.Content = model.Content.Replace("{System.CompanyName}", company);
            model.Content = model.Content.Replace("{System.AddressCompany}", address);
            model.Content = model.Content.Replace("{System.PhoneCompany}", phone);
            model.Content = model.Content.Replace("{System.Fax}", fax);
            model.Content = model.Content.Replace("{System.BankCodeCompany}", bankcode);
            model.Content = model.Content.Replace("{System.BankNameCompany}", bankname);
            model.Content = model.Content.Replace("{PrintDate}", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));

            if (ExportExcel)
            {
                Response.AppendHeader("content-disposition", "attachment;filename=" + "BaoCaoXuatNhapTon" + DateTime.Now.ToString("yyyyMMdd") + ".xls");
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.Write(model.Content);
                Response.End();
            }
            return View(model);
        }
        string buildHtmlDetailList_PrintBCNXT(List<Sale_BaoCaoNhapXuatTonViewModel> detailList)
        {
            //Tạo table html chi tiết phiếu xuất
            string detailLists = "<table class=\"invoice-detail\">\r\n";
            detailLists += "<thead>\r\n";
            detailLists += "	<tr>\r\n";
            detailLists += "		<th>STT</th>\r\n";
            detailLists += "		<th>Mã SP</th>\r\n";
            detailLists += "		<th>Tên SP</th>\r\n";
            detailLists += "		<th>Kho</th>\r\n";
            detailLists += "		<th>Số Lô</th>\r\n";
            detailLists += "		<th>Hạn dùng</th>\r\n";
            detailLists += "		<th>Đơn giá nhập</th>\r\n";
            detailLists += "		<th>Đơn giá xuất</th>\r\n";
            detailLists += "		<th>ĐVT</th>\r\n";
            detailLists += "		<th>Tồn đầu kỳ</th>\r\n";

            detailLists += "		<th>Nhập trong kỳ</th>\r\n";
            detailLists += "		<th>Xuất trong kỳ</th>\r\n";
            detailLists += "		<th>Tồn cuối kỳ</th>\r\n";
            detailLists += "		<th>Tổng tiền tồn</th>\r\n";
            detailLists += "	</tr>\r\n";
            detailLists += "</thead>\r\n";
            detailLists += "<tbody>\r\n";
            var index = 1;

            foreach (var item in detailList)
            {

                detailLists += "<tr>\r\n"
                + "<td class=\"text-center orderNo\">" + (index++) + "</td>\r\n"
                + "<td class=\"text-left code_product\">" + item.ProductCode + "</td>\r\n"
                + "<td class=\"text-left \">" + item.ProductName + "</td>\r\n"
                + "<td class=\"text-left \">" + item.WarehouseName + "</td>\r\n"
                + "<td class=\"text-right\">" + item.LoCode + "</td>\r\n"
                + "<td class=\"text-right code_product\">" + (item.ExpiryDate.HasValue ? item.ExpiryDate.Value.ToString("dd-MM-yyyy") : "") + "</td>\r\n"
                + "<td class=\"text-right \">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(item.PriceInbound, null) + "</td>\r\n"
                + "<td class=\"text-right \">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(item.PriceOutbound, null) + "</td>\r\n"
                + "<td class=\"text-center\">" + item.ProductUnit + "</td>\r\n"
                + "<td class=\"text-right orderNo\">" + Erp.BackOffice.Helpers.Common.PhanCachHangNgan(item.First_Remain) + "</td>\r\n"
                + "<td class=\"text-right orderNo\">" + Erp.BackOffice.Helpers.Common.PhanCachHangNgan(item.Last_InboundQuantity) + "</td>\r\n"
                + "<td class=\"text-right orderNo\">" + Erp.BackOffice.Helpers.Common.PhanCachHangNgan(item.Last_OutboundQuantity) + "</td>\r\n"
                + "<td class=\"text-right orderNo\">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(item.Remain, null) + "</td>\r\n"
                + "<td class=\"text-right chiet_khau\">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(item.PriceInbound * item.Remain, null) + "</td>\r\n"
                + "</tr>\r\n";
            }
            detailLists += "</tbody>\r\n";
            detailLists += "<tfoot style=\"font-weight:bold\">\r\n";
            detailLists += "<tr><td colspan=\"9\" class=\"text-right\">Tổng cộng</td><td class=\"text-right\">"
                         + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(detailList.Sum(x => x.First_Remain), null)
                         + "</td><td class=\"text-right\">"
                         + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(detailList.Sum(x => x.Last_InboundQuantity), null)
                         + "</td><td class=\"text-right\">"
                         + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(detailList.Sum(x => x.Last_OutboundQuantity), null)
                         + "</td><td class=\"text-right\">"
                         + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(detailList.Sum(x => x.Remain), null)
                          + "</td><td class=\"text-right\">"
                         + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(detailList.Sum(x => x.Remain * x.PriceInbound), null)
                         + "</tr>\r\n";
            detailLists += "</tfoot>\r\n</table>\r\n";

            return detailLists;
        }


        public ActionResult Sale_BCTong(int? year, int? month, string CityId, string DistrictId, string branchId)
        {
            year = year == null ? DateTime.Now.Year : year;
            month = month == null ? DateTime.Now.Month : month;
            CityId = string.IsNullOrEmpty(CityId) ? "" : CityId;
            DistrictId = string.IsNullOrEmpty(DistrictId) ? "" : DistrictId;
            branchId = string.IsNullOrEmpty(branchId) ? "" : branchId;
            int? week = 1;
            int quarter = 1;
            if (!Filters.SecurityFilter.IsAdmin() && !Filters.SecurityFilter.IsKeToan() && string.IsNullOrEmpty(branchId))
            {
                branchId = Helpers.Common.CurrentUser.DrugStore;
            }

            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;

            ViewBag.DateRangeText = Helpers.Common.ConvertToDateRange(ref StartDate, ref EndDate, "month", year.Value, month.Value, quarter, ref week);
            bool hasSearch = false;

            var q = TotalDiscountMoneyNTRepository.GetvwAllTotalDiscountMoneyNT().ToList();
            if (year != null && year.Value > 0)
            {
                q = q.Where(x => x.Year == year).ToList();
                //   hasSearch = true;
            }
            if (!string.IsNullOrEmpty(branchId))
            {
                hasSearch = true;
                q = q.Where(item => (string.IsNullOrEmpty(branchId) || ("," + branchId + ",").Contains("," + item.DrugStoreId + ",") == true)).ToList();
            }
            if (month != null && month.Value > 0)
            {
                //   hasSearch = true;
                q = q.Where(x => x.Month == month).ToList();
            }
            if (hasSearch)
            {
                if (q.Count() > 0)
                {
                    decimal pDoanhsoTong = 0;
                    var TotalDiscountMoneyNT = q.FirstOrDefault();
                    if (TotalDiscountMoneyNT != null && TotalDiscountMoneyNT.IsDeleted != true)
                    {
                        var model = new TotalDiscountMoneyNTViewModel();
                        AutoMapper.Mapper.Map(TotalDiscountMoneyNT, model);
                        model.ListNXT = new List<Sale_BaoCaoNhapXuatTonViewModel>();
                        var invoice = invoiceRepository.GetAllvwProductInvoice().ToList().Where(x => ("," + branchId + ",").Contains("," + x.BranchId + ",") == true && x.IsArchive == true);
                        pDoanhsoTong = invoice.Sum(x => x.TotalAmount);
                        if (year != null && year.Value > 0)
                        {
                            invoice = invoice.Where(x => x.Year == year).ToList();
                            //   hasSearch = true;
                        }
                        if (month != null && month.Value > 0)
                        {
                            //   hasSearch = true;
                            invoice = invoice.Where(x => x.Month == month).ToList();
                        }


                        model.DoanhSo = invoice.Sum(x => x.TotalAmount);
                        model.ChietKhauCoDinh = invoice.Sum(x => x.FixedDiscount);
                        model.ChietKhauDotXuat = invoice.Sum(x => x.IrregularDiscount);


                        #region Hoapd tinh lai VIP

                        var qProductInvoiceVIP = invoiceRepository.GetAllvwProductInvoice().AsEnumerable()
                            .Where(item => (string.IsNullOrEmpty(branchId) || ("," + branchId + ",").Contains("," + item.BranchId + ",") == true && item.IsArchive == true));

                        if (year != null && year.Value > 0)
                        {
                            qProductInvoiceVIP = qProductInvoiceVIP.Where(x => x.Year <= year).ToList();
                            //   hasSearch = true;
                        }
                        if (month != null && month.Value > 0)
                        {
                            //   hasSearch = true;
                            qProductInvoiceVIP = qProductInvoiceVIP.Where(x => ((x.Month <= month && x.Year == year) || (x.Year < year))).ToList();
                        }

                        decimal pDoansoVip = qProductInvoiceVIP.Sum(x => x.TotalAmount);
                        var setting = settingRepository.GetSettingByKey("setting_point").Value;
                        setting = string.IsNullOrEmpty(setting) ? "0" : setting;
                        var rf = pDoansoVip / Convert.ToDecimal(setting);
                        string[] arrVal = rf.ToString().Split(',');
                        var value = int.Parse(arrVal[0], CultureInfo.InstalledUICulture);
                        model.PointVIP = value;

                        #endregion








                        model.ListNXT = SqlHelper.QuerySP<Sale_BaoCaoNhapXuatTonViewModel>("spSale_BaoCaoNhapXuatTon", new
                        {
                            StartDate = StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                            EndDate = EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                            WarehouseId = "",
                            branchId = branchId,
                            CityId = CityId,
                            DistrictId = DistrictId
                        }).ToList();
                        return View(model);
                    }
                }
                else
                {
                    var model = new TotalDiscountMoneyNTViewModel();
                    model.ListNXT = new List<Sale_BaoCaoNhapXuatTonViewModel>();
                    ViewBag.FailedMessage = "Không tìm thấy dữ liệu";
                    return View(model);
                }
            }
            else
            {
                var model = new TotalDiscountMoneyNTViewModel();
                model.ListNXT = new List<Sale_BaoCaoNhapXuatTonViewModel>();
                ViewBag.FailedMessage = "Chưa chọn nhà thuốc";
                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Sale_BCTong");
        }

        #region PrintBCTong
        public ActionResult PrintBCTong(int? year, int? month, string CityId, string DistrictId, string branchId, bool ExportExcel = false)
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
            year = year == null ? DateTime.Now.Year : year;
            month = month == null ? DateTime.Now.Month : month;
            CityId = string.IsNullOrEmpty(CityId) ? "" : CityId;
            DistrictId = string.IsNullOrEmpty(DistrictId) ? "" : DistrictId;
            branchId = string.IsNullOrEmpty(branchId) ? "" : branchId;
            int? week = 1;
            int quarter = 1;
            if (!Filters.SecurityFilter.IsAdmin() && !Filters.SecurityFilter.IsKeToan() && string.IsNullOrEmpty(branchId))
            {
                branchId = Helpers.Common.CurrentUser.DrugStore;
            }

            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;

            ViewBag.DateRangeText = Helpers.Common.ConvertToDateRange(ref StartDate, ref EndDate, "month", year.Value, month.Value, quarter, ref week);
            bool hasSearch = false;

            var q = TotalDiscountMoneyNTRepository.GetvwAllTotalDiscountMoneyNT().ToList();
            if (year != null && year.Value > 0)
            {
                q = q.Where(x => x.Year == year).ToList();
            }
            if (!string.IsNullOrEmpty(branchId))
            {
                hasSearch = true;
                q = q.Where(item => (string.IsNullOrEmpty(branchId) || ("," + branchId + ",").Contains("," + item.DrugStoreId + ",") == true)).ToList();
            }
            if (month != null && month.Value > 0)
            {
                q = q.Where(x => x.Month == month).ToList();
            }
            if (hasSearch)
            {
                if (q.Count() > 0)
                {
                    var TotalDiscountMoneyNT = q.FirstOrDefault();
                    if (TotalDiscountMoneyNT != null && TotalDiscountMoneyNT.IsDeleted != true)
                    {
                        var model2 = new TotalDiscountMoneyNTViewModel();
                        AutoMapper.Mapper.Map(TotalDiscountMoneyNT, model2);
                        model2.ListNXT = new List<Sale_BaoCaoNhapXuatTonViewModel>();
                        var invoice = invoiceRepository.GetAllvwProductInvoice().ToList().Where(x => ("," + branchId + ",").Contains("," + x.BranchId + ",") == true && x.IsArchive == true);
                        decimal pDoansotong = invoice.Sum(x => x.TotalAmount);

                        if (year != null && year.Value > 0)
                        {
                            invoice = invoice.Where(x => x.Year == year).ToList();
                        }
                        if (month != null && month.Value > 0)
                        {
                            invoice = invoice.Where(x => x.Month == month).ToList();
                        }

                        model2.DoanhSo = invoice.Sum(x => x.TotalAmount);
                        model2.ChietKhauCoDinh = invoice.Sum(x => x.FixedDiscount);
                        model2.ChietKhauDotXuat = invoice.Sum(x => x.IrregularDiscount);




                        #region Hoapd tinh lai VIP

                        var qProductInvoiceVIP = invoiceRepository.GetAllvwProductInvoice().AsEnumerable()
                            .Where(item => (string.IsNullOrEmpty(branchId) || ("," + branchId + ",").Contains("," + item.BranchId + ",") == true && item.IsArchive == true));

                        if (year != null && year.Value > 0)
                        {
                            qProductInvoiceVIP = qProductInvoiceVIP.Where(x => x.Year <= year).ToList();
                            //   hasSearch = true;
                        }
                        if (month != null && month.Value > 0)
                        {
                            //   hasSearch = true;
                            qProductInvoiceVIP = qProductInvoiceVIP.Where(x => ((x.Month <= month && x.Year == year) || (x.Year < year))).ToList();
                        }

                        decimal pDoansoVip = qProductInvoiceVIP.Sum(x => x.TotalAmount);
                        var setting = settingRepository.GetSettingByKey("setting_point").Value;
                        setting = string.IsNullOrEmpty(setting) ? "0" : setting;
                        var rf = pDoansoVip / Convert.ToDecimal(setting);
                        string[] arrVal = rf.ToString().Split(',');
                        var value = int.Parse(arrVal[0], CultureInfo.InstalledUICulture);
                        model2.PointVIP = value;

                        #endregion















                        model2.ListNXT = SqlHelper.QuerySP<Sale_BaoCaoNhapXuatTonViewModel>("spSale_BaoCaoNhapXuatTon", new
                        {
                            StartDate = StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                            EndDate = EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                            WarehouseId = "",
                            branchId = branchId,
                            CityId = CityId,
                            DistrictId = DistrictId
                        }).ToList();
                        //lấy template phiếu xuất.
                        var template = templatePrintRepository.GetAllTemplatePrint().Where(x => x.Code.Contains("PrintBCTong")).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
                        //truyền dữ liệu vào template.
                        model.Content = template.Content;
                        model.Content = model.Content.Replace("{DrugStoreName}", model2.DrugStoreName);
                        model.Content = model.Content.Replace("{Address}", model2.Address + ", " + model2.WardName + ", " + model2.DistrictName + ", " + model2.ProvinceName);
                        model.Content = model.Content.Replace("{DoanhSo}", model2.DoanhSo.ToCurrencyStr(null));
                        model.Content = model.Content.Replace("{CKCD}", model2.ChietKhauCoDinh.ToCurrencyStr(null));
                        model.Content = model.Content.Replace("{CKDX}", model2.ChietKhauDotXuat.ToCurrencyStr(null));
                        model.Content = model.Content.Replace("{MonthYear}", model2.Month + " NĂM " + model2.Year);
                        model.Content = model.Content.Replace("{PrintDate}", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                        model.Content = model.Content.Replace("{QuantityDay}", Erp.BackOffice.Helpers.Common.PhanCachHangNgan2(model2.QuantityDay));
                        model.Content = model.Content.Replace("{PercentDecrease}", Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(model2.PercentDecrease, null));
                        model.Content = model.Content.Replace("{DecreaseAmount}", Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(model2.DecreaseAmount, null));
                        model.Content = model.Content.Replace("{DiscountAmount}", Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(model2.DiscountAmount, null));
                        model.Content = model.Content.Replace("{RemainingAmount}", Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(model2.RemainingAmount, null));
                        model.Content = model.Content.Replace("{PointVIP}", Erp.BackOffice.Helpers.Common.PhanCachHangNgan(model2.PointVIP));
                        model.Content = model.Content.Replace("{DataTable}", buildHtmlDetailList_PrintBCTong(model2.ListNXT));
                        model.Content = model.Content.Replace("{System.Logo}", ImgLogo);
                        model.Content = model.Content.Replace("{System.CompanyName}", company);
                        model.Content = model.Content.Replace("{System.AddressCompany}", address);
                        model.Content = model.Content.Replace("{System.PhoneCompany}", phone);
                        model.Content = model.Content.Replace("{System.Fax}", fax);
                        model.Content = model.Content.Replace("{System.BankCodeCompany}", bankcode);
                        model.Content = model.Content.Replace("{System.BankNameCompany}", bankname);
                        model.Content = model.Content.Replace("{MonthYearDS}", model2.Month + "/" + model2.Year);

                        if (ExportExcel)
                        {
                            Response.AppendHeader("content-disposition", "attachment;filename=" + DateTime.Now.ToString("yyyyMMdd") + "BCTong" + ".xls");
                            Response.Charset = "";
                            Response.Cache.SetCacheability(HttpCacheability.NoCache);
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.Write(model.Content);
                            Response.End();
                        }
                    }
                }
                else
                {
                    ViewBag.FailedMessage = "Không tìm thấy dữ liệu";
                    return View(model);
                }
            }
            else
            {
                ViewBag.FailedMessage = "Chưa chọn nhà thuốc";
                return View(model);
            }

            return View(model);
        }




        string buildHtmlDetailList_PrintBCTong(List<Sale_BaoCaoNhapXuatTonViewModel> detailList)
        {
            //Tạo table html chi tiết phiếu xuất
            string detailLists = "<table class=\"invoice-detail\">\r\n";
            detailLists += "<thead>\r\n";
            detailLists += "	<tr>\r\n";
            detailLists += "		<th>STT</th>\r\n";
            detailLists += "		<th>Mã SP</th>\r\n";
            detailLists += "		<th>Tên SP</th>\r\n";
            //detailLists += "		<th>Kho</th>\r\n";
            detailLists += "		<th>Số Lô</th>\r\n";
            detailLists += "		<th>Hạn dùng</th>\r\n";
            detailLists += "		<th>Đơn giá nhập</th>\r\n";
            detailLists += "		<th>Đơn giá xuất</th>\r\n";
            detailLists += "		<th>ĐVT</th>\r\n";
            detailLists += "		<th>Tồn đầu kỳ</th>\r\n";

            detailLists += "		<th>Nhập trong kỳ</th>\r\n";
            detailLists += "		<th>Xuất trong kỳ</th>\r\n";
            detailLists += "		<th>Tồn cuối kỳ</th>\r\n";
            detailLists += "		<th>Tổng tiền tồn</th>\r\n";
            detailLists += "	</tr>\r\n";
            detailLists += "</thead>\r\n";
            detailLists += "<tbody>\r\n";
            var index = 1;

            foreach (var item in detailList)
            {

                detailLists += "<tr>\r\n"
                + "<td class=\"text-center orderNo\">" + (index++) + "</td>\r\n"
                + "<td class=\"text-left code_product\">" + item.ProductCode + "</td>\r\n"
                + "<td class=\"text-left \">" + item.ProductName + "</td>\r\n"
                //+ "<td class=\"text-left \">" + item.WarehouseName + "</td>\r\n"
                + "<td class=\"text-right\">" + item.LoCode + "</td>\r\n"
                + "<td class=\"text-right expiry_date\">" + (item.ExpiryDate.HasValue ? item.ExpiryDate.Value.ToString("dd-MM-yyyy") : "") + "</td>\r\n"
                + "<td class=\"text-right \">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(item.PriceInbound, null) + "</td>\r\n"
                + "<td class=\"text-right \">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(item.PriceOutbound, null) + "</td>\r\n"
                + "<td class=\"text-center\">" + item.ProductUnit + "</td>\r\n"
                + "<td class=\"text-right orderNo\">" + Erp.BackOffice.Helpers.Common.PhanCachHangNgan(item.First_Remain) + "</td>\r\n"
                + "<td class=\"text-right orderNo\">" + Erp.BackOffice.Helpers.Common.PhanCachHangNgan(item.Last_InboundQuantity) + "</td>\r\n"
                + "<td class=\"text-right orderNo\">" + Erp.BackOffice.Helpers.Common.PhanCachHangNgan(item.Last_OutboundQuantity) + "</td>\r\n"
                + "<td class=\"text-right orderNo\">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(item.Remain, null) + "</td>\r\n"
                + "<td class=\"text-right chiet_khau\">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(item.PriceInbound * item.Remain, null) + "</td>\r\n"
                + "</tr>\r\n";
            }
            detailLists += "</tbody>\r\n";
            detailLists += "<tfoot style=\"font-weight:bold\">\r\n";
            detailLists += "<tr><td colspan=\"8\" class=\"text-right\">Tổng cộng</td><td class=\"text-right\">"
                         + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(detailList.Sum(x => x.First_Remain), null)
                         + "</td><td class=\"text-right\">"
                         + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(detailList.Sum(x => x.Last_InboundQuantity), null)
                         + "</td><td class=\"text-right\">"
                         + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(detailList.Sum(x => x.Last_OutboundQuantity), null)
                         + "</td><td class=\"text-right\">"
                         + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(detailList.Sum(x => x.Remain), null)
                          + "</td><td class=\"text-right\">"
                         + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(detailList.Sum(x => x.Remain * x.PriceInbound), null)
                         + "</tr>\r\n";
            detailLists += "</tfoot>\r\n</table>\r\n";

            return detailLists;
        }

        #endregion
        //<append_content_action_here>
    }
}
