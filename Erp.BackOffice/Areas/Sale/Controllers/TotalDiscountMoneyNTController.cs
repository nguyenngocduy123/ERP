using System.Globalization;
using Erp.BackOffice.Sale.Models;
using Erp.BackOffice.Filters;
using Erp.Domain.Entities;
using Erp.Domain.Interfaces;
using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Erp.Utilities;
using WebMatrix.WebData;
using Erp.BackOffice.Helpers;
using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using Erp.BackOffice.Staff.Models;
using System.Web;
using Erp.Domain.Helper;
using System.Transactions;
using Erp.Domain.Sale.Repositories;
using Erp.Domain.Sale;
using Erp.Domain.Staff.Repositories;
using Erp.Domain.Staff;
namespace Erp.BackOffice.Sale.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class TotalDiscountMoneyNTController : Controller
    {
        private readonly ITotalDiscountMoneyNTRepository TotalDiscountMoneyNTRepository;
        private readonly IUserRepository userRepository;
        private readonly IBranchRepository branchRepository;
        private readonly IProductInvoiceRepository productInvoiceRepository;
        private readonly ITemplatePrintRepository templatePrintRepository;
        private readonly ISettingRepository settingRepository;
        public TotalDiscountMoneyNTController(
            ITotalDiscountMoneyNTRepository _TotalDiscountMoneyNT
            , IUserRepository _user
              , IBranchRepository branch
            , IProductInvoiceRepository invoice
             , ITemplatePrintRepository _templatePrint
            , ISettingRepository setting
            )
        {
            TotalDiscountMoneyNTRepository = _TotalDiscountMoneyNT;
            userRepository = _user;
            branchRepository = branch;
            productInvoiceRepository = invoice;
            templatePrintRepository = _templatePrint;
            settingRepository = setting;
        }

        #region Index

        public ViewResult Index(int? year, int? BranchId)
        {

            List<TotalDiscountMoneyNTViewModel> q = TotalDiscountMoneyNTRepository.GetvwAllTotalDiscountMoneyNT()
                .Select(item => new TotalDiscountMoneyNTViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    BranchName = item.BranchName,
                    BranchCode = item.BranchCode,
                    BranchId = item.BranchId,
                    DrugStoreCode = item.DrugStoreCode,
                    DrugStoreId = item.DrugStoreId,
                    DrugStoreName = item.DrugStoreName,
                    DecreaseAmount = item.DecreaseAmount,
                    DiscountAmount = item.DiscountAmount,
                    PercentDecrease = item.PercentDecrease,
                    Month = item.Month,
                    Year = item.Year,
                    QuantityDay = item.QuantityDay,
                    Status = item.Status,
                    RemainingAmount = item.RemainingAmount,
                    UserManagerCode = item.UserManagerCode,
                    UserManagerName = item.UserManagerCode,
                    UserManagerId = item.UserManagerId
                }).OrderByDescending(m => m.ModifiedDate).ToList();
            if (year != null && year.Value > 0)
            {
                q = q.Where(x => x.Year == year).ToList();
            }
            else
            {
                q = q.Where(x => x.Year == DateTime.Now.Year).ToList();
            }
            if (BranchId != null && BranchId.Value > 0)
            {
                q = q.Where(x => x.BranchId == BranchId).ToList();
            }
            var departmentList = branchRepository.GetAllBranch().Where(x => x.ParentId != null || x.ParentId > 0)
              .Select(item => new BranchViewModel
              {
                  Name = item.Name,
                  Id = item.Id,
                  ParentId = item.ParentId,
                  Code = item.Code
              }).ToList();
            ViewBag.departmentList = departmentList;
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        #endregion

        #region Create
        public ViewResult Create()
        {
            var model = new TotalDiscountMoneyNTViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(TotalDiscountMoneyNTViewModel model)
        {
            if (ModelState.IsValid)
            {
                var TotalDiscountMoneyNT = new TotalDiscountMoneyNT();
                AutoMapper.Mapper.Map(model, TotalDiscountMoneyNT);
                TotalDiscountMoneyNT.IsDeleted = false;
                TotalDiscountMoneyNT.CreatedUserId = WebSecurity.CurrentUserId;
                TotalDiscountMoneyNT.ModifiedUserId = WebSecurity.CurrentUserId;
                TotalDiscountMoneyNT.AssignedUserId = WebSecurity.CurrentUserId;
                TotalDiscountMoneyNT.CreatedDate = DateTime.Now;
                TotalDiscountMoneyNT.ModifiedDate = DateTime.Now;
                TotalDiscountMoneyNTRepository.InsertTotalDiscountMoneyNT(TotalDiscountMoneyNT);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var TotalDiscountMoneyNT = TotalDiscountMoneyNTRepository.GetTotalDiscountMoneyNTById(Id.Value);
            if (TotalDiscountMoneyNT != null && TotalDiscountMoneyNT.IsDeleted != true)
            {
                var model = new TotalDiscountMoneyNTViewModel();
                AutoMapper.Mapper.Map(TotalDiscountMoneyNT, model);

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
        public ActionResult Edit(TotalDiscountMoneyNTViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var TotalDiscountMoneyNT = TotalDiscountMoneyNTRepository.GetTotalDiscountMoneyNTById(model.Id);
                    AutoMapper.Mapper.Map(model, TotalDiscountMoneyNT);
                    TotalDiscountMoneyNT.ModifiedUserId = WebSecurity.CurrentUserId;
                    TotalDiscountMoneyNT.ModifiedDate = DateTime.Now;
                    TotalDiscountMoneyNTRepository.UpdateTotalDiscountMoneyNT(TotalDiscountMoneyNT);

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
            var TotalDiscountMoneyNT = TotalDiscountMoneyNTRepository.GetvwTotalDiscountMoneyNTById(Id.Value);
            if (TotalDiscountMoneyNT != null && TotalDiscountMoneyNT.IsDeleted != true)
            {
                var model = new TotalDiscountMoneyNTViewModel();
                AutoMapper.Mapper.Map(TotalDiscountMoneyNT, model);
                model.DetailList = new List<vwProductInvoiceDetail>();
                model.DetailList = productInvoiceRepository.GetAllvwInvoiceDetails()
                    .Where(x => x.IsArchive == true && x.BranchId == model.DrugStoreId
                   && x.Month == model.Month && x.Year == model.Year).ToList();

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
                    var item = TotalDiscountMoneyNTRepository.GetTotalDiscountMoneyNTById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        {
                            TempData["FailedMessage"] = "NotOwner";
                            return RedirectToAction("Index");
                        }

                        item.IsDeleted = true;
                        TotalDiscountMoneyNTRepository.UpdateTotalDiscountMoneyNT(item);
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
            using (var scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    //tìm ngày đầu tháng
                    DateTime aDateTime = new DateTime(year, month, 1);
                    //tìm ngày cuối tháng
                    DateTime retDateTime = aDateTime.AddMonths(1).AddDays(-1);
                    //tìm số ngày của 1 tháng
                    // var count_day_of_month = retDateTime.Day;
                    //lấy danh sách nhà thuốc ra để bắt đầu đồng bộ dữ liệu
                    var list_drugStore = branchRepository.GetAllBranch().Where(x => x.ParentId != null && x.ParentId.Value > 0).ToList();
                    //lấy toàn bộ danh sách hóa đơn trong 1 tháng của tất cả nhà thuốc ra để xử lý 1 lần.
                    var list_productInvoice = productInvoiceRepository.GetAllvwProductInvoice().Where(x => x.Month == month && x.Year == year && x.IsArchive == true);
                    //lấy list đã insert nếu có
                    var list_old = TotalDiscountMoneyNTRepository.GetAllTotalDiscountMoneyNT().Where(x => x.Month == month && x.Year == year).ToList();

                    var date_percent = Helpers.Common.GetSetting("date_percent_decrease_NT");
                    var percent_decrease = Helpers.Common.GetSetting("percent_decrease_NT");
                    foreach (var item in list_drugStore)
                    {
                        //lấy danh sách đơn bán hàng của nhà thuốc trong tháng
                        var list_invoice_by_drugStore = list_productInvoice.Where(x => x.BranchId == item.Id).ToList();
                        //đếm số ngày tạo đơn thuốc trong 1 tháng
                        var count_day = list_invoice_by_drugStore.GroupBy(z => z.Day).Count();

                        //nếu số ngày đăng nhập lớn hơn cài đặt thì lấy tổng ngày đăng nhập theo cài đặt
                        var count_day_setting = count_day > Convert.ToInt32(date_percent) ? Convert.ToInt32(date_percent) : count_day;
                        //tính số ngày không tạo đơn hàng trong tháng theo cài đặt
                        decimal count_day_off = Convert.ToInt32(date_percent) - count_day_setting;
                        //tinh % trừ chiết khấu
                        decimal percent = 0;
                        decimal amount = 0;
                        decimal DiscountAmount = list_invoice_by_drugStore.Sum(x => (x.FixedDiscount + x.IrregularDiscount));

                        if (count_day_off > 0)
                        {
                            decimal pe_countday = count_day_setting / 5;
                            var p_last = pe_countday / Convert.ToInt32(percent_decrease);

                            

                            string[] arrVal_percent_last = p_last.ToString().IndexOf(".") > 0 ? p_last.ToString().Split('.') : p_last.ToString().Split(',');

                            var vl = int.Parse(arrVal_percent_last[0], CultureInfo.InstalledUICulture);
                            if (vl == 0)
                            {
                                vl = 1;
                            }
                            decimal p_dayoff = count_day_off / 5;

                            
                            string[] arrVal = p_dayoff.ToString().IndexOf(".") > 0 ? p_dayoff.ToString().Split('.') : p_dayoff.ToString().Split(',');
                            var value = int.Parse(arrVal[0], CultureInfo.InstalledUICulture);
                            if (arrVal.Count() >= 2)
                            {
                                var aa = int.Parse(arrVal[1], CultureInfo.InstalledUICulture);
                                if (aa > 0)
                                {
                                    value = value + 1;
                                }
                            }

                            var percent_off = value * Convert.ToInt32(percent_decrease) / vl;

                            percent = percent_off;
                            //  amount = percent * DiscountAmount / 100;
                        }

                        var ds_ = list_invoice_by_drugStore.Sum(x => x.TotalAmount);
                        var ds_tru = ds_ * percent / 100;
                        decimal RemainingAmount = DiscountAmount - ds_tru;
                        if (list_old.Where(x => x.DrugStoreId == item.Id).Count() > 0)
                        {
                            var add = list_old.Where(x => x.DrugStoreId == item.Id).FirstOrDefault();
                            add.ModifiedUserId = WebSecurity.CurrentUserId;
                            add.ModifiedDate = DateTime.Now;
                            add.DrugStoreId = item.Id;
                            add.BranchId = item.ParentId;
                            add.UserManagerId = WebSecurity.CurrentUserId;
                            add.Month = month;
                            add.Year = year;
                            add.QuantityDay = count_day;
                            add.PercentDecrease = Math.Round(percent, 2);
                            add.DiscountAmount = DiscountAmount;
                            add.DecreaseAmount = ds_tru;
                            add.RemainingAmount = RemainingAmount;
                            add.Status = App_GlobalResources.Wording.New;
                            TotalDiscountMoneyNTRepository.UpdateTotalDiscountMoneyNT(add);
                        }
                        else
                        {
                            //thêm mới vào data.
                            var add = new TotalDiscountMoneyNT();
                            add.IsDeleted = false;
                            add.CreatedUserId = WebSecurity.CurrentUserId;
                            add.ModifiedUserId = WebSecurity.CurrentUserId;
                            add.AssignedUserId = WebSecurity.CurrentUserId;
                            add.CreatedDate = DateTime.Now;
                            add.ModifiedDate = DateTime.Now;
                            add.DrugStoreId = item.Id;
                            add.BranchId = item.ParentId;
                            add.UserManagerId = WebSecurity.CurrentUserId;
                            add.Month = month;
                            add.Year = year;
                            add.QuantityDay = count_day;
                            add.PercentDecrease = Math.Round(percent, 2);
                            add.DiscountAmount = DiscountAmount;
                            add.DecreaseAmount = ds_tru;
                            add.RemainingAmount = RemainingAmount;
                            add.Status = App_GlobalResources.Wording.New;
                            TotalDiscountMoneyNTRepository.InsertTotalDiscountMoneyNT(add);
                        }
                    }
                    scope.Complete();
                    return Json(new { Result = "success", Message = App_GlobalResources.Wording.UpdateSuccess },
                                         JsonRequestBehavior.AllowGet);
                }
                catch (DbUpdateException)
                {
                    return Json(new { Result = "error", Message = App_GlobalResources.Error.UpdateUnsuccess },
                                         JsonRequestBehavior.AllowGet);
                }
            }
        }
        #endregion

        public ViewResult List(int? month, int? year, string branchId, string CityId, string DistrictId)
        {
            year = year == null ? DateTime.Now.Year : year;
            month = month == null ? DateTime.Now.Month : month;
            List<TotalDiscountMoneyNTViewModel> q = TotalDiscountMoneyNTRepository.GetvwAllTotalDiscountMoneyNT()
                .Select(item => new TotalDiscountMoneyNTViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    BranchName = item.BranchName,
                    BranchCode = item.BranchCode,
                    BranchId = item.BranchId,
                    DrugStoreCode = item.DrugStoreCode,
                    DrugStoreId = item.DrugStoreId,
                    DrugStoreName = item.DrugStoreName,
                    DecreaseAmount = item.DecreaseAmount,
                    DiscountAmount = item.DiscountAmount,
                    PercentDecrease = item.PercentDecrease,
                    Month = item.Month,
                    Year = item.Year,
                    QuantityDay = item.QuantityDay,
                    Status = item.Status,
                    RemainingAmount = item.RemainingAmount,
                    UserManagerCode = item.UserManagerCode,
                    UserManagerName = item.UserManagerCode,
                    UserManagerId = item.UserManagerId,
                    DistrictId = item.DistrictId,
                    CityId = item.CityId
                }).OrderByDescending(m => m.ModifiedDate).ToList();
            if (!Filters.SecurityFilter.IsAdmin() && !Filters.SecurityFilter.IsKeToan() && string.IsNullOrEmpty(branchId))
            {
                branchId = Helpers.Common.CurrentUser.DrugStore;
            }
            if (!string.IsNullOrEmpty(branchId))
            {
                q = q.Where(item => (string.IsNullOrEmpty(branchId) || ("," + branchId + ",").Contains("," + item.DrugStoreId + ",") == true)).ToList();
            }
            if (month != null && month.Value > 0)
            {
                q = q.Where(x => x.Month == month).ToList();
            }

            if (year != null && year.Value > 0)
            {
                q = q.Where(x => x.Year == year).ToList();
            }
            if (!string.IsNullOrEmpty(CityId))
            {
                q = q.Where(item => item.CityId == CityId).ToList();
            }
            if (!string.IsNullOrEmpty(DistrictId))
            {
                q = q.Where(item => item.DistrictId == DistrictId).ToList();
            }
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }

        #region Print
        public ActionResult Print(int Id, bool ExportExcel = false)
        {
            try
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
            List<vwProductInvoiceDetail> detailList = new List<vwProductInvoiceDetail>();
            var TotalDiscountMoneyNT = TotalDiscountMoneyNTRepository.GetvwTotalDiscountMoneyNTById(Id);
            if (TotalDiscountMoneyNT != null && TotalDiscountMoneyNT.IsDeleted != true)
            {
                detailList = productInvoiceRepository.GetAllvwInvoiceDetails()
                    .Where(x => x.IsArchive == true && x.BranchId == TotalDiscountMoneyNT.DrugStoreId
                   && x.Month == TotalDiscountMoneyNT.Month && x.Year == TotalDiscountMoneyNT.Year).ToList();
            }


            //lấy template phiếu xuất.
            var template = templatePrintRepository.GetAllTemplatePrint().Where(x => x.Code.Contains("TotalDiscountMoneyNT")).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            //truyền dữ liệu vào template.
            model.Content = template.Content;
            model.Content = model.Content.Replace("{DrugStoreName}", TotalDiscountMoneyNT.DrugStoreName);
            model.Content = model.Content.Replace("{MonthYear}", TotalDiscountMoneyNT.Month + " NĂM " + TotalDiscountMoneyNT.Year);
            model.Content = model.Content.Replace("{PrintDate}", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            model.Content = model.Content.Replace("{QuantityDay}", Erp.BackOffice.Helpers.Common.PhanCachHangNgan2(TotalDiscountMoneyNT.QuantityDay));
            model.Content = model.Content.Replace("{PercentDecrease}", Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(TotalDiscountMoneyNT.PercentDecrease, null));
            model.Content = model.Content.Replace("{DecreaseAmount}", Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(TotalDiscountMoneyNT.DecreaseAmount, null));
            model.Content = model.Content.Replace("{DiscountAmount}", Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(TotalDiscountMoneyNT.DiscountAmount, null));
            model.Content = model.Content.Replace("{RemainingAmount}", Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(TotalDiscountMoneyNT.RemainingAmount, null));

            model.Content = model.Content.Replace("{Amount}", Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(detailList.Sum(x => x.Amount), null));
            model.Content = model.Content.Replace("{DataTable}", buildHtmlDetailList(detailList));
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
                Response.AppendHeader("content-disposition", "attachment;filename=" + DateTime.Now.ToString("yyyyMMdd") + "ChietKhauNhaThuoc" + ".xls");
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.Write(model.Content);
                Response.End();
            }
            return View(model);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        string buildHtmlDetailList(List<vwProductInvoiceDetail> detailList)
        {
            //Tạo table html chi tiết phiếu xuất
            string detailLists = "<table class=\"invoice-detail\">\r\n";
            detailLists += "<thead>\r\n";
            detailLists += "	<tr>\r\n";
            detailLists += "		<th>STT</th>\r\n";
            detailLists += "		<th>Mã SP</th>\r\n";
            detailLists += "		<th>Tên SP</th>\r\n";
            detailLists += "		<th>ĐVT</th>\r\n";
            detailLists += "		<th>Số lượng</th>\r\n";
            detailLists += "		<th>Doanh số</th>\r\n";
            detailLists += "		<th>Chiết khấu cố định</th>\r\n";
            detailLists += "		<th>Chiết khấu đột xuất</th>\r\n";
            detailLists += "	</tr>\r\n";
            detailLists += "</thead>\r\n";
            detailLists += "<tbody>\r\n";
            var index = 1;

            foreach (var item in detailList.GroupBy(x => x.ProductId))
            {
                detailLists += "<tr>\r\n"
                + "<td class=\"text-center orderNo\">" + (index++) + "</td>\r\n"
                + "<td class=\"text-left code_product\">" + item.FirstOrDefault().ProductCode + "</td>\r\n"
                + "<td class=\"text-left \">" + item.FirstOrDefault().ProductName + "</td>\r\n"
                + "<td class=\"text-center orderNo\">" + item.FirstOrDefault().Unit + "</td>\r\n"
                + "<td class=\"text-right orderNo\">" + item.Sum(x => x.Quantity) + "</td>\r\n"
                + "<td class=\"text-right chiet_khau\">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(item.Sum(x => x.Amount), null) + "</td>\r\n"
                + "<td class=\"text-right chiet_khau\">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(item.Sum(x => x.FixedDiscountAmount), null) + "</td>\r\n"
                + "<td class=\"text-right chiet_khau\">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(item.Sum(x => x.IrregularDiscountAmount), null) + "</td>\r\n"
                + "</tr>\r\n";
            }
            detailLists += "</tbody>\r\n";
            detailLists += "<tfoot style=\"font-weight:bold\">\r\n";
            detailLists += "<tr><td colspan=\"4\" class=\"text-right\">Tổng cộng</td><td class=\"text-right\">"
                         + Erp.BackOffice.Helpers.Common.PhanCachHangNgan(detailList.Sum(x => x.Quantity))
                         + "</td><td  class=\"text-right\">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(detailList.Sum(x => x.Amount), null) + "</td><td  class=\"text-right\">"
                         + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(detailList.Sum(x => x.FixedDiscountAmount), null)
                         + "</td><td  class=\"text-right\">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(detailList.Sum(x => x.IrregularDiscountAmount), null)
                         + "</td></tr>\r\n";

            detailLists += "</tfoot>\r\n</table>\r\n";

            return detailLists;
        }

        #endregion

        public static void SyncTotalDisCountMoneyNT(ProductInvoice model, int? CreatedUserId)
        {
            try
            {
                ProductInvoiceRepository productInvoiceRepository = new ProductInvoiceRepository(new ErpSaleDbContext());
                BranchRepository branchRepository = new BranchRepository(new ErpStaffDbContext());
                TotalDiscountMoneyNTRepository totalDiscountMoneyNTRepository = new TotalDiscountMoneyNTRepository(new ErpSaleDbContext());
                //tìm ngày đầu tháng
                DateTime aDateTime = new DateTime(model.CreatedDate.Value.Year, model.CreatedDate.Value.Month, 1);
                //tìm ngày cuối tháng
                DateTime retDateTime = aDateTime.AddMonths(1).AddDays(-1);
                //tìm số ngày của 1 tháng
                var count_day_of_month = retDateTime.Day;
                //lấy toàn bộ danh sách hóa đơn trong 1 tháng của tất cả nhà thuốc ra để xử lý 1 lần.
                var list_invoice_by_drugStore = productInvoiceRepository.GetAllvwProductInvoice().Where(x => x.BranchId == model.BranchId && x.Month == model.CreatedDate.Value.Month && x.Year == model.CreatedDate.Value.Year && x.IsArchive == true).ToList();
                //lấy list đã insert nếu có
                var list_old = totalDiscountMoneyNTRepository.GetAllTotalDiscountMoneyNT().Where(x => x.Month == model.CreatedDate.Value.Month && x.Year == model.CreatedDate.Value.Year).ToList();

                var date_percent = Helpers.Common.GetSetting("date_percent_decrease_NT");
                var percent_decrease = Helpers.Common.GetSetting("percent_decrease_NT");

                //đếm số ngày tạo đơn thuốc trong 1 tháng
                var count_day = list_invoice_by_drugStore.GroupBy(z => z.Day).Count();

                //nếu số ngày đăng nhập lớn hơn cài đặt thì lấy tổng ngày đăng nhập theo cài đặt
                var count_day_setting = count_day > Convert.ToInt32(date_percent) ? Convert.ToInt32(date_percent) : count_day;
                //tính số ngày không tạo đơn hàng trong tháng theo cài đặt
                decimal count_day_off = Convert.ToInt32(date_percent) - count_day_setting;
                //tinh % trừ chiết khấu
                decimal percent = 0;
                decimal DiscountAmount = list_invoice_by_drugStore.Sum(x => (x.FixedDiscount + x.IrregularDiscount));

                if (count_day_off > 0)
                {
                    decimal pe_countday = count_day_setting / 5;
                    var p_last = pe_countday / Convert.ToInt32(percent_decrease);
                    string[] arrVal_percent_last = p_last.ToString().IndexOf(".") > 0 ? p_last.ToString().Split('.') : p_last.ToString().Split(',');

                    var vl = int.Parse(arrVal_percent_last[0], CultureInfo.InstalledUICulture);
                    if (vl == 0)
                    {
                        vl = 1;
                    }
                    decimal p_dayoff = count_day_off / 5;
                    string[] arrVal = p_dayoff.ToString().IndexOf(".") > 0 ? p_dayoff.ToString().Split('.') : p_dayoff.ToString().Split(',');
                    var value = int.Parse(arrVal[0], CultureInfo.InstalledUICulture);
                    if (arrVal.Count() >= 2)
                    {
                        var aa = int.Parse(arrVal[1], CultureInfo.InstalledUICulture);
                        if (aa > 0)
                        {
                            value = value + 1;
                        }
                    }

                    var percent_off = value * Convert.ToInt32(percent_decrease) / vl;

                    percent = percent_off;
                }

                var ds_ = list_invoice_by_drugStore.Sum(x => x.TotalAmount);
                var ds_tru = ds_ * percent / 100;
                decimal RemainingAmount = DiscountAmount - ds_tru;

                var branch_parent = branchRepository.GetBranchById(model.BranchId);
                if (list_old.Where(x => x.DrugStoreId == model.BranchId).Count() > 0)
                {
                    var add = list_old.Where(x => x.DrugStoreId == model.BranchId).FirstOrDefault();
                    add.ModifiedUserId = CreatedUserId;
                    add.ModifiedDate = DateTime.Now;
                    add.DrugStoreId = model.BranchId;
                    add.BranchId = branch_parent.ParentId;
                    add.UserManagerId = CreatedUserId;
                    add.Month = model.CreatedDate.Value.Month;
                    add.Year = model.CreatedDate.Value.Year;
                    add.QuantityDay = count_day;
                    add.PercentDecrease = Math.Round(percent, 2);
                    add.DiscountAmount = DiscountAmount;
                    add.DecreaseAmount = ds_tru;
                    add.RemainingAmount = RemainingAmount;
                    add.Status = App_GlobalResources.Wording.New;
                    totalDiscountMoneyNTRepository.UpdateTotalDiscountMoneyNT(add);
                }
                else
                {
                    //thêm mới vào data.
                    var add = new TotalDiscountMoneyNT();
                    add.IsDeleted = false;
                    add.CreatedUserId = CreatedUserId;
                    add.ModifiedUserId = CreatedUserId;
                    add.AssignedUserId = CreatedUserId;
                    add.CreatedDate = DateTime.Now;
                    add.ModifiedDate = DateTime.Now;
                    add.DrugStoreId = model.BranchId;
                    add.BranchId = branch_parent.ParentId;
                    add.UserManagerId = WebSecurity.CurrentUserId;
                    add.Month = model.CreatedDate.Value.Month;
                    add.Year = model.CreatedDate.Value.Year;
                    add.QuantityDay = count_day;
                    add.PercentDecrease = Math.Round(percent, 2);
                    add.DiscountAmount = DiscountAmount;
                    add.DecreaseAmount = ds_tru;
                    add.RemainingAmount = RemainingAmount;
                    add.Status = App_GlobalResources.Wording.New;
                    totalDiscountMoneyNTRepository.InsertTotalDiscountMoneyNT(add);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
