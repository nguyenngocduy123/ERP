using System.Globalization;
using Erp.BackOffice.Sale.Models;
using Erp.BackOffice.Account.Models;
using Erp.BackOffice.Filters;
using Erp.Domain.Sale.Entities;
using Erp.Domain.Interfaces;
using Erp.Domain.Sale.Interfaces;
using Erp.Domain.Sale.Repositories;
using Erp.Domain.Account.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Erp.Utilities;
using WebMatrix.WebData;
using Erp.BackOffice.Helpers;
using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Staff.Interfaces;
using Erp.BackOffice.Account.Controllers;
using Erp.Domain.Account.Entities;
using System.Xml.Linq;
using Erp.BackOffice.Areas.Cms.Models;
using Erp.Domain.Entities;
using Newtonsoft.Json;
using GenCode128;
using System.Drawing;
using System.IO;
using Erp.Domain.Crm.Interfaces;
using Erp.Domain.Staff.Entities;
using System.Web;
using Erp.Domain.Repositories;
using Erp.Domain;
using Erp.Domain.Sale;
using System.Web.Script.Serialization;
using System.Data.Entity;
using System.Transactions;

namespace Erp.BackOffice.Sale.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class ProductInvoiceController : Controller
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly IReceiptRepository ReceiptRepository;
        private readonly IProductInvoiceRepository productInvoiceRepository;
        private readonly ICommisionRepository commisionRepository;
        private readonly IProductOrServiceRepository ProductRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly IInventoryRepository inventoryRepository;
        private readonly IProductOutboundRepository productOutboundRepository;
        private readonly IStaffsRepository staffRepository;
        private readonly IUserRepository userRepository;
        private readonly ITemplatePrintRepository templatePrintRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IWarehouseLocationItemRepository warehouseLocationItemRepository;
        private readonly ICommisionStaffRepository commisionStaffRepository;
        private readonly IServiceComboRepository servicecomboRepository;
        private readonly IUsingServiceLogRepository usingServiceLogRepository;
        private readonly ILogServiceRemminderRepository logServiceReminderRepository;
        private readonly IServiceReminderGroupRepository ServiceReminderGroupRepository;
        private readonly ITransactionLiabilitiesRepository transactionLiabilitiesRepository;
        private readonly IWarehouseRepository warehouseRepository;
        private readonly IUsingServiceLogDetailRepository usingServiceLogDetailRepository;
        private readonly ICommisionCustomerRepository commisionCustomerRepository;
        private readonly IServiceScheduleRepository serviceScheduleRepository;
        private readonly ITaskRepository taskRepository;
        private readonly IWorkSchedulesRepository WorkSchedulesRepository;
        private readonly IRegisterForOvertimeRepository RegisterForOvertimeRepository;
        private readonly IReceiptDetailRepository receiptDetailRepository;
        public ProductInvoiceController(
            ITransactionRepository _transaction
            , IReceiptRepository _Receipt
            , IProductInvoiceRepository _ProductInvoice
            , ICommisionRepository _Commision
            , IProductOrServiceRepository _Product
            , ICustomerRepository _Customer
            , IInventoryRepository _Inventory
            , IProductOutboundRepository _ProductOutbound
            , IStaffsRepository _staff
            , IUserRepository _user
            , ITemplatePrintRepository _templatePrint
            , ICategoryRepository category
            , IWarehouseLocationItemRepository _WarehouseLocationItem
            , ICommisionStaffRepository _commisionStaff
            , IServiceComboRepository servicecombo
            , IUsingServiceLogRepository usingServiceLog
            , ILogServiceRemminderRepository logServiceReminder
            , IServiceReminderGroupRepository serviceReminderGroup
            , ITransactionLiabilitiesRepository _transactionLiabilities
            , IWarehouseRepository _Warehouse
            , IUsingServiceLogDetailRepository usingServiceLogDetail
            , ICommisionCustomerRepository _Commision_Customer
            , IServiceScheduleRepository schedule
            , ITaskRepository task
            , IWorkSchedulesRepository _WorkSchedules
            , IRegisterForOvertimeRepository _RegisterForOvertime
            , IReceiptDetailRepository receiptDetail
            )
        {
            transactionRepository = _transaction;
            ReceiptRepository = _Receipt;
            productInvoiceRepository = _ProductInvoice;
            commisionRepository = _Commision;
            ProductRepository = _Product;
            inventoryRepository = _Inventory;
            productOutboundRepository = _ProductOutbound;
            customerRepository = _Customer;
            staffRepository = _staff;
            userRepository = _user;
            templatePrintRepository = _templatePrint;
            categoryRepository = category;
            warehouseLocationItemRepository = _WarehouseLocationItem;
            commisionStaffRepository = _commisionStaff;
            usingServiceLogRepository = usingServiceLog;
            servicecomboRepository = servicecombo;
            logServiceReminderRepository = logServiceReminder;
            ServiceReminderGroupRepository = serviceReminderGroup;
            transactionLiabilitiesRepository = _transactionLiabilities;
            warehouseRepository = _Warehouse;
            usingServiceLogDetailRepository = usingServiceLogDetail;
            commisionCustomerRepository = _Commision_Customer;
            serviceScheduleRepository = schedule;
            taskRepository = task;
            WorkSchedulesRepository = _WorkSchedules;
            RegisterForOvertimeRepository = _RegisterForOvertime;
            receiptDetailRepository = receiptDetail;
        }

        #region Index

        public ViewResult Index(string txtCode, string txtMinAmount, string txtMaxAmount, string txtCusName, string startDate, string endDate, int? BranchId, string Status)
        {
            var user = userRepository.GetByvwUserName(Helpers.Common.CurrentUser.UserName);
            var q = productInvoiceRepository.GetAllvwProductInvoiceFull();
            //.Where(x => x.BranchId == Helpers.Common.CurrentUser.BranchId);
            var model = q.Select(item => new ProductInvoiceViewModel
            {
                Id = item.Id,
                IsDeleted = item.IsDeleted,
                CreatedUserId = item.CreatedUserId,
                CreatedDate = item.CreatedDate,
                ModifiedUserId = item.ModifiedUserId,
                ModifiedDate = item.ModifiedDate,
                Code = item.Code,
                CustomerCode = item.CustomerCode,
                CustomerName = item.CustomerName,
                ShipCityName = item.ShipCityName,
                TotalAmount = item.TotalAmount,
                //FixedDiscount = item.FixedDiscount,
                TaxFee = item.TaxFee,
                CodeInvoiceRed = item.CodeInvoiceRed,
                Status = item.Status,
                IsArchive = item.IsArchive,
                ProductOutboundId = item.ProductOutboundId,
                ProductOutboundCode = item.ProductOutboundCode,
                Note = item.Note,
                CancelReason = item.CancelReason,
                BranchId = item.BranchId,
                CustomerId = item.CustomerId,
                BranchName = item.BranchName
            }).OrderByDescending(m => m.Id).ToList();
            if (!string.IsNullOrEmpty(txtCode))
            {
                var item_quet_barcode = model.Where(x => Helpers.Common.ChuyenThanhKhongDau(x.Code).Contains(Helpers.Common.ChuyenThanhKhongDau(txtCode))).ToList().FirstOrDefault();
                model = model.Where(x => x.CustomerId == item_quet_barcode.CustomerId).ToList();
                int index = model.IndexOf(item_quet_barcode);
                model.RemoveAt(index);
                model.Insert(0, item_quet_barcode);
            }

            if (!string.IsNullOrEmpty(txtCusName))
            {
                txtCusName = Helpers.Common.ChuyenThanhKhongDau(txtCusName);
                model = model.Where(x => Helpers.Common.ChuyenThanhKhongDau(x.CustomerName).Contains(txtCusName)).ToList();
            }

            if (!Erp.BackOffice.Filters.SecurityFilter.IsAdmin())
            {
                model = model.Where(x => ("," + user.DrugStore + ",").Contains("," + x.BranchId + ",") == true).ToList();
            }

            if (!string.IsNullOrEmpty(Status))
            {
                if (Status == "delete")
                {
                    model = model.Where(x => x.IsDeleted == true).ToList();
                }
                if (Status == "complete")
                {
                    model = model.Where(x => x.Status == "complete").ToList();
                }
                if (Status == "pending")
                {
                    model = model.Where(x => x.Status == "pending").ToList();
                }
                if (Status == "dc")
                {
                    var productListId = ProductRepository.GetAllvwProductAndService()
                        .Where(item => item.Code == "ĐC").Select(item => item.Id).ToList();

                    if (productListId.Count > 0)
                    {
                        List<int> listProductInboundId = new List<int>();
                        foreach (var id in productListId)
                        {
                            var list = productInvoiceRepository.GetAllvwInvoiceDetails().Where(x => x.ProductCode == "ĐC")
                                .Select(item => item.ProductInvoiceId.Value).Distinct().ToList();

                            listProductInboundId.AddRange(list);
                        }

                        model = model.Where(item => listProductInboundId.Contains(item.Id)).ToList();
                    }
                }
            }

            //Lọc theo ngày
            DateTime d_startDate, d_endDate;
            if (DateTime.TryParseExact(startDate, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out d_startDate))
            {
                if (DateTime.TryParseExact(endDate, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out d_endDate))
                {
                    d_endDate = d_endDate.AddHours(23).AddMinutes(59);
                    model = model.Where(x => x.CreatedDate >= d_startDate && x.CreatedDate <= d_endDate).ToList();
                }
            }

            decimal minAmount;
            if (decimal.TryParse(txtMinAmount, out minAmount))
            {
                model = model.Where(x => x.TotalAmount >= minAmount).ToList();
            }

            decimal maxAmount;
            if (decimal.TryParse(txtMaxAmount, out maxAmount))
            {
                if (maxAmount > 0)
                {
                    model = model.Where(x => x.TotalAmount <= maxAmount).ToList();
                }
            }
            if (BranchId != null && BranchId.Value > 0)
            {
                model = model.Where(x => x.BranchId == BranchId).ToList();
            }



            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];

            return View(model);
        }
        #endregion

        #region Create

        public ActionResult Create(int? Id, int? CustomerId)
        {

            ProductInvoiceViewModel model = new ProductInvoiceViewModel();
            model.DetailList = new List<ProductInvoiceDetailViewModel>();
            if (!Filters.SecurityFilter.IsDrugStore())
            {
                return Content("Mẫu tin không tồn tại! Không có quyền truy cập!");
            }
            if (Id.HasValue && Id > 0)
            {
                var productInvoice = productInvoiceRepository.GetvwProductInvoiceById(Id.Value);
                //Kiểm tra phân quyền Chi nhánh
                if (!(Filters.SecurityFilter.IsAdmin() || productInvoice.BranchId.ToString() == Helpers.Common.CurrentUser.DrugStore))
                {
                    return Content("Mẫu tin không tồn tại! Không có quyền truy cập!");
                }

                AutoMapper.Mapper.Map(productInvoice, model);

                var detailList = productInvoiceRepository.GetAllvwInvoiceDetailsByInvoiceId(productInvoice.Id).ToList();
                AutoMapper.Mapper.Map(detailList, model.DetailList);
            }

            model.ReceiptViewModel = new ReceiptViewModel();
            model.NextPaymentDate_Temp = DateTime.Now.AddDays(30);
            model.ReceiptViewModel.Name = "Bán hàng - Thu tiền mặt";
            model.ReceiptViewModel.Amount = 0;

            var saleDepartmentCode = Erp.BackOffice.Helpers.Common.GetSetting("SaleDepartmentCode");
            string image_folder_product = Helpers.Common.GetSetting("product-image-folder");
            //  string image_folder_service = Helpers.Common.GetSetting("service-image-folder");

            int branchId = Convert.ToInt32(Helpers.Common.CurrentUser.DrugStore);
            var productList = inventoryRepository.GetAllvwInventoryByBranchId(branchId).Where(x => x.Quantity > 0 && x.IsSale == true)
               .Select(item => new ProductViewModel
               {
                   Type = "product",
                   Code = item.ProductCode,
                   Name = item.ProductName,
                   Id = item.ProductId,
                   CategoryCode = string.IsNullOrEmpty(item.CategoryCode) ? "Sản phẩm khác" : item.CategoryCode,
                   PriceOutbound = item.ProductPriceOutbound,
                   Unit = item.ProductUnit,
                   QuantityTotalInventory = item.Quantity
               }).OrderBy(item => item.CategoryCode).ToList();
            var jsonProductInvoiceItem = productList.Select(item => new ProductInvoiceItemViewModel
            {
                Id = item.Id,
                Type = item.Type,
                Code = item.Code,
                Name = item.Name.Replace("\"", "\\\""),
                CategoryCode = item.CategoryCode,
                PriceOutbound = item.PriceOutbound,
                Unit = item.Unit,
                InventoryQuantity = item.QuantityTotalInventory.Value,
                Image_Name = item.Image_Name,
                IsCombo = item.IsCombo
            }).ToList();

            ViewBag.json = JsonConvert.SerializeObject(jsonProductInvoiceItem);

            List<SelectListItem> categoryList = new List<SelectListItem>();
            categoryList.Add(new SelectListItem { Text = "TẤT CẢ", Value = "0" });
            categoryList.Add(new SelectListItem { Text = "I. SẢN PHẨM", Value = "1" });
            foreach (var item in productList.GroupBy(i => i.CategoryCode).Select(i => new { Name = i.Key, NumberOfItem = i.Count() }))
            {
                categoryList.Add(new SelectListItem { Text = "---- " + item.Name + " (" + item.NumberOfItem + ")", Value = item.Name });
            }
            ViewBag.CategoryList = categoryList;

            //Thêm số lượng tồn kho cho chi tiết đơn hàng đã được thêm
            if (model.DetailList != null && model.DetailList.Count > 0)
            {
                foreach (var item in model.DetailList)
                {
                    var product = productList.Where(i => i.Id == item.ProductId).FirstOrDefault();
                    if (product != null)
                    {
                        item.QuantityInInventory = product.QuantityTotalInventory;
                        item.PriceTest = product.PriceOutbound;
                    }
                    else
                    {
                        item.Id = 0;
                    }
                }

                model.DetailList.RemoveAll(x => x.Id == 0);

                int n = 0;
                foreach (var item in model.DetailList)
                {
                    item.OrderNo = n;
                    n++;
                }
            }
            model.CreatedUserName = Erp.BackOffice.Helpers.Common.CurrentUser.FullName;

            int taxfee = 0;
            int.TryParse(Helpers.Common.GetSetting("vat"), out taxfee);
            model.TaxFee = taxfee;

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ProductInvoiceViewModel model)
        {
            if (ModelState.IsValid && model.DetailList.Count != 0)
            {
                ProductInvoice productInvoice = null;
                if (model.Id > 0)
                {
                    productInvoice = productInvoiceRepository.GetProductInvoiceById(model.Id);
                    //Kiểm tra phân quyền Chi nhánh
                    if (!(Filters.SecurityFilter.IsAdmin() || ("," + Helpers.Common.CurrentUser.DrugStore + ",").Contains("," + productInvoice.BranchId + ",") == true))
                    {
                        return Content("Mẫu tin không tồn tại! Không có quyền truy cập!");
                    }
                }
                using (var scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        if (productInvoice != null)
                        {
                            //Nếu đã ghi sổ rồi thì không được sửa
                            if (productInvoice.IsArchive)
                            {
                                return RedirectToAction("Detail", new { Id = productInvoice.Id });
                            }

                            AutoMapper.Mapper.Map(model, productInvoice);
                        }
                        else
                        {
                            productInvoice = new ProductInvoice();
                            AutoMapper.Mapper.Map(model, productInvoice);
                            productInvoice.IsDeleted = false;
                            productInvoice.CreatedUserId = WebSecurity.CurrentUserId;
                            productInvoice.CreatedDate = DateTime.Now;
                            productInvoice.Status = Wording.OrderStatus_pending;
                            productInvoice.BranchId = model.BranchId.Value;
                            productInvoice.IsArchive = false;
                            productInvoice.IsReturn = false;
                        }

                        //Duyệt qua danh sách sản phẩm mới xử lý tình huống user chọn 2 sản phầm cùng id
                        List<ProductInvoiceDetail> listNewCheckSameId = new List<ProductInvoiceDetail>();
                        foreach (var group in model.DetailList)
                        {
                            var product = ProductRepository.GetProductById(group.ProductId.Value);
                            listNewCheckSameId.Add(new ProductInvoiceDetail
                            {
                                ProductId = product.Id,
                                ProductType = product.Type,
                                Quantity = group.Quantity,
                                Unit = product.Unit,
                                Price = group.Price,
                                PromotionDetailId = group.PromotionDetailId,
                                PromotionId = group.PromotionId,
                                PromotionValue = group.PromotionValue,
                                IsDeleted = false,
                                CreatedUserId = WebSecurity.CurrentUserId,
                                ModifiedUserId = WebSecurity.CurrentUserId,
                                CreatedDate = DateTime.Now,
                                ModifiedDate = DateTime.Now,
                                FixedDiscount = group.FixedDiscount,
                                FixedDiscountAmount = group.FixedDiscountAmount,
                                IrregularDiscount = group.IrregularDiscount,
                                IrregularDiscountAmount = group.IrregularDiscountAmount,
                                QuantitySaleReturn = group.Quantity,
                                CheckPromotion = (group.CheckPromotion.HasValue ? group.CheckPromotion.Value : false),
                                IsReturn = false,
                                //StaffId = group.StaffId,
                                //Status = group.Status

                            });
                        }

                        //Tính lại chiết khấu
                        foreach (var item in listNewCheckSameId)
                        {
                            var thanh_tien = item.Quantity * item.Price;
                            item.FixedDiscountAmount = Convert.ToInt32(item.FixedDiscount * thanh_tien / 100);
                            item.IrregularDiscountAmount = Convert.ToInt32(item.IrregularDiscount * thanh_tien / 100);
                            //tổng Point

                        }
                        var total = listNewCheckSameId.Sum(item => item.Quantity.Value * item.Price.Value - item.FixedDiscountAmount.Value - item.IrregularDiscountAmount);
                        productInvoice.TotalAmount = total.Value + (total.Value * Convert.ToInt32(Erp.BackOffice.Helpers.Common.GetSetting("VAT")));

                        productInvoice.IsReturn = false;
                        productInvoice.ModifiedUserId = WebSecurity.CurrentUserId;
                        productInvoice.ModifiedDate = DateTime.Now;
                        productInvoice.PaidAmount = 0;
                        productInvoice.RemainingAmount = productInvoice.TotalAmount;
                        //hàm edit 
                        if (model.Id > 0)
                        {
                            productInvoiceRepository.UpdateProductInvoice(productInvoice);
                            var listDetail = productInvoiceRepository.GetAllInvoiceDetailsByInvoiceId(productInvoice.Id).ToList();

                            //xóa danh sách dữ liệu cũ dưới database gồm product invoice, productInvoiceDetail, UsingServiceLog,UsingServiceLogDetail,ServiceReminder
                            productInvoiceRepository.DeleteProductInvoiceDetail(listDetail);

                            //thêm mới toàn bộ database
                            foreach (var item in listNewCheckSameId)
                            {
                                item.ProductInvoiceId = productInvoice.Id;
                                productInvoiceRepository.InsertProductInvoiceDetail(item);

                                //Kiểm tra xem KH có mua DV nào hay không, nếu có thì tạo dữ liệu cho bảng UsingService, ServiceReminder
                                //if (item.ProductType == "service")
                                //{
                                //    CreateUsingLogAndReminder(item, model.DetailList, productInvoice.CreatedDate);
                                //}
                            }

                            //Thêm vào quản lý chứng từ
                            TransactionController.Create(new TransactionViewModel
                            {
                                TransactionModule = "ProductInvoice",
                                TransactionCode = productInvoice.Code,
                                TransactionName = "Bán hàng"
                            });
                        }
                        else
                        {
                            //hàm thêm mới
                            productInvoiceRepository.InsertProductInvoice(productInvoice, listNewCheckSameId);

                            //cập nhật lại mã hóa đơn
                            string prefix = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_Invoice");
                            productInvoice.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix, productInvoice.Id);
                            productInvoiceRepository.UpdateProductInvoice(productInvoice);


                            //Thêm vào quản lý chứng từ
                            TransactionController.Create(new TransactionViewModel
                            {
                                TransactionModule = "ProductInvoice",
                                TransactionCode = productInvoice.Code,
                                TransactionName = "Bán hàng"
                            });
                        }

                        //Tạo phiếu nhập, nếu là tự động
                        string sale_tu_dong_tao_chung_tu = Erp.BackOffice.Helpers.Common.GetSetting("sale_tu_dong_tao_chung_tu");
                        if (listNewCheckSameId.Where(x => x.ProductType == "product").Count() <= 0)
                        {
                            if (sale_tu_dong_tao_chung_tu == "true")
                            {
                                ProductOutboundViewModel productOutboundViewModel = new ProductOutboundViewModel();
                                var warehouseDefault = warehouseRepository.GetAllWarehouse().Where(x => ("," + Helpers.Common.CurrentUser.DrugStore + ",").Contains("," + x.BranchId + ",") == true && x.IsSale == true).FirstOrDefault();

                                //Nếu trong đơn hàng có sản phẩm thì xuất kho
                                if (model.DetailList.Any(item => item.ProductType == "product") && warehouseDefault != null)
                                {
                                    productOutboundViewModel.InvoiceId = productInvoice.Id;
                                    productOutboundViewModel.InvoiceCode = productInvoice.Code;
                                    productOutboundViewModel.WarehouseSourceId = warehouseDefault.Id;
                                    productOutboundViewModel.Note = "Xuất kho cho đơn hàng " + productInvoice.Code;
                                    var DetailList = model.DetailList.Where(x => x.ProductType == "product").ToList();
                                    //Lấy dữ liệu cho chi tiết
                                    productOutboundViewModel.DetailList = new List<ProductOutboundDetailViewModel>();
                                    AutoMapper.Mapper.Map(DetailList, productOutboundViewModel.DetailList);

                                    var productOutbound = ProductOutboundController.AutoCreateProductOutboundFromProductInvoice(productOutboundRepository, productOutboundViewModel, productInvoice.Code);

                                    //Ghi sổ chứng từ phiếu xuất
                                    ProductOutboundController.Archive(productOutbound, TempData);
                                }

                                //Ghi sổ chứng từ bán hàng
                                model.Id = productInvoice.Id;
                                Archive(model);
                            }
                        }
                        scope.Complete();
                    }
                    catch (DbUpdateException)
                    {
                        return Content("Fail");
                    }
                }
                return RedirectToAction("Detail", new { Id = productInvoice.Id });
            }

            return View();
        }
        #endregion

        //public void CreateUsingLogAndReminder(ProductInvoiceDetail item, List<ProductInvoiceDetailViewModel> model, DateTime? CreatedDate)
        //{
        //    //Kiểm tra xem nếu là dịch vụ, gói combo thì thêm số lần sử dụng .
        //    var product = ProductRepository.GetProductById(item.ProductId);
        //    var aa = model.Where(x => x.ProductId == item.ProductId).FirstOrDefault();
        //    if (product.IsCombo == true)
        //    {
        //        var combo = servicecomboRepository.GetAllServiceCombo().Where(x => x.ComboId == item.ProductId).ToList();

        //        foreach (var x in combo)
        //        {
        //            var qty = x.Quantity * item.Quantity;
        //            //Tao số lần sử dụng dịch vụ
        //            CreateUsingServiceLog(item.Id, x.ServiceId, qty, aa.StaffId, aa.Status);
        //            //Tạo lịch sử lời nhắc nhở của từng dịch vụ
        //            CreateLogServiceReminder(item.Id, x.ServiceId, CreatedDate);
        //        }
        //    }
        //    else
        //    {
        //        CreateUsingServiceLog(item.Id, item.ProductId, item.Quantity, aa.StaffId, aa.Status);
        //        CreateLogServiceReminder(item.Id, item.ProductId, CreatedDate);
        //    }

        //}

        //#region SỐ lần sử dụng dịch vụ
        //public static void CreateUsingServiceLog(int? ServiceInvoiceDetailId, int? ServiceId, int? Quantity, int? StaffId, string Status)
        //{
        //    Erp.Domain.Sale.Repositories.UsingServiceLogRepository usingServiceLogRepository = new Erp.Domain.Sale.Repositories.UsingServiceLogRepository(new Domain.Sale.ErpSaleDbContext());
        //    Erp.Domain.Sale.Repositories.UsingServiceLogDetailRepository usingServiceLogDetailRepository = new Erp.Domain.Sale.Repositories.UsingServiceLogDetailRepository(new Domain.Sale.ErpSaleDbContext());
        //    var usingServiceLog = new UsingServiceLog();
        //    usingServiceLog.IsDeleted = false;
        //    usingServiceLog.CreatedUserId = WebSecurity.CurrentUserId;
        //    usingServiceLog.ModifiedUserId = WebSecurity.CurrentUserId;
        //    usingServiceLog.AssignedUserId = WebSecurity.CurrentUserId;
        //    usingServiceLog.CreatedDate = DateTime.Now;
        //    usingServiceLog.ModifiedDate = DateTime.Now;
        //    usingServiceLog.ServiceInvoiceDetailId = ServiceInvoiceDetailId;
        //    usingServiceLog.ServiceId = ServiceId;
        //    usingServiceLog.Quantity = Quantity;
        //    usingServiceLog.QuantityUsed = 1;
        //    usingServiceLogRepository.InsertUsingServiceLog(usingServiceLog);

        //    var usingServiceLogDetail = new UsingServiceLogDetail();
        //    usingServiceLogDetail.IsDeleted = false;
        //    usingServiceLogDetail.CreatedUserId = WebSecurity.CurrentUserId;
        //    usingServiceLogDetail.ModifiedUserId = WebSecurity.CurrentUserId;
        //    usingServiceLogDetail.AssignedUserId = WebSecurity.CurrentUserId;
        //    usingServiceLogDetail.CreatedDate = DateTime.Now;
        //    usingServiceLogDetail.ModifiedDate = DateTime.Now;
        //    usingServiceLogDetail.UsingServiceId = usingServiceLog.Id;
        //    usingServiceLogDetail.Type = "usedservice";
        //    usingServiceLogDetail.StaffId = StaffId;
        //    usingServiceLogDetail.Status = Status;
        //    usingServiceLogDetailRepository.InsertUsingServiceLogDetail(usingServiceLogDetail);

        //}
        //#endregion

        //#region Lịch sử lời nhắc nhở của từng dịch vụ
        //public static void CreateLogServiceReminder(int? ServiceInvoiceDetailId, int? ServiceId, DateTime? InvoiceDate)
        //{
        //    Erp.Domain.Crm.Repositories.TaskRepository TaskRepository = new Erp.Domain.Crm.Repositories.TaskRepository(new Domain.Crm.ErpCrmDbContext());
        //    Erp.Domain.Sale.Repositories.ServiceReminderGroupRepository ServiceReminderGroupRepository = new Erp.Domain.Sale.Repositories.ServiceReminderGroupRepository(new Domain.Sale.ErpSaleDbContext());
        //    Erp.Domain.Sale.Repositories.ProductInvoiceRepository productInvoiceRepository = new Erp.Domain.Sale.Repositories.ProductInvoiceRepository(new Domain.Sale.ErpSaleDbContext());
        //    Erp.Domain.Sale.Repositories.LogServiceRemminderRepository logServiceReminderRepository = new Erp.Domain.Sale.Repositories.LogServiceRemminderRepository(new Domain.Sale.ErpSaleDbContext());
        //    var reminder = ServiceReminderGroupRepository.GetAllvwServiceReminderGroup().Where(q => q.ServiceId.Value == ServiceId).ToList();
        //    var invoicedetail = productInvoiceRepository.GetvwProductInvoiceDetailById(ServiceInvoiceDetailId.Value);
        //    foreach (var ii in reminder)
        //    {
        //        var logReminder = new LogServiceRemminder();
        //        logReminder.IsDeleted = false;
        //        logReminder.CreatedUserId = WebSecurity.CurrentUserId;
        //        logReminder.ModifiedUserId = WebSecurity.CurrentUserId;
        //        logReminder.AssignedUserId = WebSecurity.CurrentUserId;
        //        logReminder.CreatedDate = DateTime.Now;
        //        logReminder.ModifiedDate = DateTime.Now;
        //        logReminder.ProductInvoiceDetailId = ServiceInvoiceDetailId;
        //        logReminder.ReminderId = ii.ServiceReminderId;
        //        logReminder.ReminderName = ii.Name;
        //        logReminder.ServiceId = ServiceId;

        //        if (ii.Reminder == true)
        //        {
        //            var date = InvoiceDate.Value.AddDays(ii.QuantityDate.Value);
        //            logReminder.ReminderDate = date;
        //        }
        //        logServiceReminderRepository.InsertLogServiceRemminder(logReminder);

        //        if (ii.Reminder == true)
        //        {
        //            var date = InvoiceDate.Value.AddDays(ii.QuantityDate.Value);
        //            var Task = new Erp.Domain.Crm.Entities.Task();
        //            Task.IsDeleted = false;
        //            Task.CreatedUserId = WebSecurity.CurrentUserId;
        //            Task.ModifiedUserId = WebSecurity.CurrentUserId;
        //            Task.CreatedDate = DateTime.Now;
        //            Task.ModifiedDate = DateTime.Now;
        //            Task.Type = "task";
        //            Task.ParentType = "LogServiceRemminder";
        //            Task.ParentId = logReminder.Id;
        //            Task.Status = "pending";
        //            Task.StartDate = InvoiceDate;
        //            Task.DueDate = Convert.ToDateTime(date);
        //            Task.Subject = "Khách hàng " + invoicedetail.CustomerCode + " - " + invoicedetail.CustomerName + " hẹn tái khám ngày: " + Task.DueDate.Value.ToString("dd/MM/yyyy");
        //            Task.Description = Task.Subject;
        //            TaskRepository.InsertTask(Task);
        //        }
        //    }

        //    //ghi lịch sử lời nhắc vào task.
        //    // Crm.Controllers.ProcessController.Run("ProductInvoice", "Create", null, null, null, logReminder);
        //}
        //#endregion

        #region LoadProductItem

        public PartialViewResult LoadProductItem(int? OrderNo, int? ProductId, string ProductName, string Unit, int Quantity, decimal Price, string ProductCode, string ProductType, int QuantityInventory, bool? CheckPromotion, int? CustomerId)
        {
            var model = new ProductInvoiceDetailViewModel();
            model.OrderNo = OrderNo.Value;
            model.ProductId = ProductId;
            model.ProductName = ProductName;
            model.Unit = Unit;
            model.Quantity = Quantity;
            model.Price = Price;
            model.ProductCode = ProductCode;
            model.ProductType = ProductType;
            model.QuantityInInventory = QuantityInventory;

            model.FixedDiscountAmount = 0;
            model.IrregularDiscountAmount = 0;
            model.FixedDiscount = 0;
            model.IrregularDiscount = 0;
            model.CheckPromotion = CheckPromotion;
            model.PriceTest = Price;

            //var commision = commisionCustomerRepository.GetAllCommisionCustomer()
            //    .Where(item => item.ProductId == ProductId && item.CustomerId == CustomerId && item.IsMoney != true).FirstOrDefault();
            //if (commision == null)
            //{
            //    model.DisCount = 0;
            //}
            //else
            //{
            //    model.DisCount = Convert.ToInt32(commision.CommissionValue);

            //}

            return PartialView(model);
        }
        #endregion

        #region Print
        public ActionResult Print(int Id, bool ExportExcel = false)
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
            //lấy hóa đơn.
            var productInvoice = productInvoiceRepository.GetvwProductInvoiceById(Id);
            //lấy thông tin khách hàng
            var customer = customerRepository.GetvwCustomerByCode(productInvoice.CustomerCode);

            List<ProductInvoiceDetailViewModel> detailList = new List<ProductInvoiceDetailViewModel>();
            if (productInvoice != null && productInvoice.IsDeleted != true)
            {
                //lấy danh sách sản phẩm xuất kho
                detailList = productInvoiceRepository.GetAllvwInvoiceDetailsByInvoiceId(Id)
                        .Select(x => new ProductInvoiceDetailViewModel
                        {
                            Id = x.Id,
                            Price = x.Price,
                            ProductId = x.ProductId,
                            ProductName = x.ProductName,
                            ProductCode = x.ProductCode,
                            Quantity = x.Quantity,
                            Unit = x.Unit,
                            //FixedDiscount = x.FixedDiscount.HasValue ? x.FixedDiscount.Value : 0,
                            //FixedDiscountAmount = x.FixedDiscountAmount.HasValue ? x.FixedDiscountAmount : 0,
                            //IrregularDiscount = x.IrregularDiscount.HasValue ? x.IrregularDiscount.Value : 0,
                            //IrregularDiscountAmount = x.IrregularDiscountAmount.HasValue ? x.IrregularDiscountAmount : 0,
                            ProductGroup = x.ProductGroup,
                            CheckPromotion = x.CheckPromotion,
                            ProductType = x.ProductType,
                            CategoryCode = x.CategoryCode,
                            StaffName = x.StaffName,
                            LoCode = x.LoCode,
                            ExpiryDate = x.ExpiryDate
                            //Status = x.Status
                        }).ToList();
            }

            //lấy template phiếu xuất.
            var template = templatePrintRepository.GetAllTemplatePrint().Where(x => x.Code.Contains("ProductInvoice")).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            //truyền dữ liệu vào template.
            model.Content = template.Content;
            model.Content = model.Content.Replace("{Code}", productInvoice.Code);
            model.Content = model.Content.Replace("{CreateDate}", productInvoice.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm"));
            model.Content = model.Content.Replace("{Hours}", productInvoice.CreatedDate.Value.ToString("HH:mm"));
            model.Content = model.Content.Replace("{CustomerName}", customer.LastName + " " + customer.FirstName);
            model.Content = model.Content.Replace("{CustomerPhone}", customer.Phone);
            model.Content = model.Content.Replace("{CompanyName}", customer.CompanyName);
            model.Content = model.Content.Replace("{StaffCreateName}", productInvoice.StaffCreateName);
            if (!string.IsNullOrEmpty(customer.Address))
            {
                model.Content = model.Content.Replace("{Address}", customer.Address + ", ");
            }
            else
            {
                model.Content = model.Content.Replace("{Address}", "");
            }
            if (!string.IsNullOrEmpty(customer.DistrictName))
            {
                model.Content = model.Content.Replace("{District}", customer.DistrictName + ", ");
            }
            else
            {
                model.Content = model.Content.Replace("{District}", "");
            }
            if (!string.IsNullOrEmpty(customer.WardName))
            {
                model.Content = model.Content.Replace("{Ward}", customer.WardName + ", ");
            }
            else
            {
                model.Content = model.Content.Replace("{Ward}", "");
            }
            if (!string.IsNullOrEmpty(customer.ProvinceName))
            {
                model.Content = model.Content.Replace("{Province}", customer.ProvinceName);
            }
            else
            {
                model.Content = model.Content.Replace("{Province}", "");
            }

            model.Content = model.Content.Replace("{Note}", productInvoice.Note);
            if (!string.IsNullOrEmpty(productInvoice.CodeInvoiceRed))
            {
                model.Content = model.Content.Replace("{InvoiceCode}", productInvoice.Code + " - " + productInvoice.CodeInvoiceRed);
            }
            else
            {
                model.Content = model.Content.Replace("{InvoiceCode}", productInvoice.Code);
            }
            model.Content = model.Content.Replace("{PaymentMethod}", productInvoice.PaymentMethod);
            model.Content = model.Content.Replace("{MoneyText}", Erp.BackOffice.Helpers.Common.ChuyenSoThanhChu_2(productInvoice.TotalAmount.ToString()));

            model.Content = model.Content.Replace("{DataTable}", buildHtmlDetailList(detailList, productInvoice));
            model.Content = model.Content.Replace("{System.Logo}", ImgLogo);
            model.Content = model.Content.Replace("{System.CompanyName}", company);
            model.Content = model.Content.Replace("{System.AddressCompany}", address);
            model.Content = model.Content.Replace("{System.PhoneCompany}", phone);
            model.Content = model.Content.Replace("{System.Fax}", fax);
            model.Content = model.Content.Replace("{System.BankCodeCompany}", bankcode);
            model.Content = model.Content.Replace("{System.BankNameCompany}", bankname);
            model.Content = model.Content.Replace("{PrintDate}", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            //Tạo barcode
            Image barcode = Code128Rendering.MakeBarcodeImage(productInvoice.Code, 1, true);
            model.Content = model.Content.Replace("{BarcodeImgSource}", ImageToBase64(barcode, System.Drawing.Imaging.ImageFormat.Png));

            if (ExportExcel)
            {
                Response.AppendHeader("content-disposition", "attachment;filename=" + productInvoice.CreatedDate.Value.ToString("yyyyMMdd") + productInvoice.Code + ".xls");
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.Write(model.Content);
                Response.End();
            }
            return View(model);
        }

        string buildHtmlDetailList(List<ProductInvoiceDetailViewModel> detailList, vwProductInvoice model)
        {
            decimal? tong_tien = 0;
            //int da_thanh_toan = 0;
            //int con_lai = 0;
            //var productInvoice = productInvoiceRepository.GetvwProductInvoiceById(Id.Value);

            //Tạo table html chi tiết phiếu xuất
            string detailLists = "<table class=\"invoice-detail\">\r\n";
            detailLists += "<thead>\r\n";
            detailLists += "	<tr>\r\n";
            detailLists += "		<th>STT</th>\r\n";
            detailLists += "		<th>Mã SP</th>\r\n";
            detailLists += "		<th>Tên SP</th>\r\n";
            detailLists += "		<th>Lô sản xuất</th>\r\n";
            detailLists += "		<th>Hạn dùng</th>\r\n";
            detailLists += "		<th>ĐVT</th>\r\n";
            detailLists += "		<th>SL</th>\r\n";
            detailLists += "		<th>Đơn giá</th>\r\n";
            detailLists += "		<th>Thành tiền</th>\r\n";
            detailLists += "	</tr>\r\n";
            detailLists += "</thead>\r\n";
            detailLists += "<tbody>\r\n";
            var index = 1;

            foreach (var item in detailList)
            {
                decimal? subTotal = item.Quantity * item.Price.Value;

                tong_tien += subTotal;
                detailLists += "<tr>\r\n"
                + "<td class=\"text-center orderNo\">" + (index++) + "</td>\r\n"
                + "<td class=\"text-left code_product\">" + item.ProductCode + "</td>\r\n"
                + "<td class=\"text-left \">" + item.ProductName + "</td>\r\n"
                + "<td class=\"text-right\">" + item.LoCode + "</td>\r\n"
                + "<td class=\"text-right\">" + (item.ExpiryDate.HasValue ? item.ExpiryDate.Value.ToString("dd-MM-yyyy") : "") + "</td>\r\n"
                + "<td class=\"text-center\">" + item.Unit + "</td>\r\n"
                + "<td class=\"text-right orderNo\">" + Erp.BackOffice.Helpers.Common.PhanCachHangNgan(item.Quantity) + "</td>\r\n"
                + "<td class=\"text-right code_product\">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(item.Price, null) + "</td>\r\n"
                + "<td class=\"text-right chiet_khau\">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(subTotal, null) + "</td>\r\n"
                + "</tr>\r\n";
            }
            detailLists += "</tbody>\r\n";
            detailLists += "<tfoot style=\"font-weight:bold\">\r\n";
            detailLists += "<tr><td colspan=\"8\" class=\"text-right\">Tổng cộng</td><td class=\"text-right\">"
                         + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(tong_tien, null)
                         + "</td></tr>\r\n";
            if (model.TaxFee > 0)
            {
                var vat = tong_tien * Convert.ToDecimal(model.TaxFee);
                var tong = tong_tien + vat;
                detailLists += "<tr><td colspan=\"8\" class=\"text-right\">VAT (" + model.TaxFee + "%)</td><td class=\"text-right\">"
                        + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(vat, null)
                        + "</td></tr>\r\n";
                detailLists += "<tr><td colspan=\"8\" class=\"text-right\">Tổng tiền cần thanh toán</td><td class=\"text-right\">"
                    + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(tong, null)
                    + "</td></tr>\r\n";
            }


            detailLists += "<tr><td colspan=\"8\" class=\"text-right\">Đã thanh toán</td><td class=\"text-right\">"
                        + (model.PaidAmount > 0 ? Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(model.PaidAmount, null) : "0")
                        + "</td></tr>\r\n";
            detailLists += "<tr><td colspan=\"8\" class=\"text-right\">Còn lại phải thu</td><td class=\"text-right\">"
                        + (model.RemainingAmount > 0 ? Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(model.RemainingAmount, null) : "0")
                        + "</td></tr>\r\n";
            detailLists += "</tfoot>\r\n</table>\r\n";

            return detailLists;
        }

        public string ImageToBase64(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }
        #endregion

        #region Delete
        [HttpPost]
        public ActionResult Delete(int Id, string CancelReason)
        {
            var productInvoice = productInvoiceRepository.GetProductInvoiceById(Id);
            if (productInvoice != null)
            {
                //Kiểm tra phân quyền Chi nhánh
                if (!(Filters.SecurityFilter.IsAdmin() || ("," + Helpers.Common.CurrentUser.DrugStore + ",").Contains("," + productInvoice.BranchId + ",") == true))
                {
                    return Content("Mẫu tin không tồn tại! Không có quyền truy cập!");
                }

                productInvoice.ModifiedUserId = WebSecurity.CurrentUserId;
                productInvoice.ModifiedDate = DateTime.Now;
                productInvoice.IsDeleted = true;
                productInvoice.IsArchive = false;
                productInvoice.CancelReason = CancelReason;
                productInvoice.Status = Wording.OrderStatus_deleted;
                productInvoiceRepository.UpdateProductInvoice(productInvoice);

                return RedirectToAction("Detail", new { Id = productInvoice.Id });
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region Detail

        public ActionResult DetailByChart(string single, int? day, int? month, int? year, string CityId, string DistrictId, string branchId, int? quarter, int? week)
        {
            var user = userRepository.GetByvwUserName(Helpers.Common.CurrentUser.UserName);
            single = single == null ? "month" : single;
            year = year == null ? DateTime.Now.Year : year;
            month = month == null ? DateTime.Now.Month : month;
            quarter = quarter == null ? 1 : quarter;
            CityId = string.IsNullOrEmpty(CityId) ? "" : CityId;
            DistrictId = string.IsNullOrEmpty(DistrictId) ? "" : DistrictId;
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;
            var weekdefault = calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            week = week == null ? weekdefault : week;
            branchId = branchId == null ? "" : branchId.ToString();

            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;

            ViewBag.DateRangeText = Helpers.Common.ConvertToDateRange(ref StartDate, ref EndDate, single, year.Value, month.Value, quarter.Value, ref week);


            var q = productInvoiceRepository.GetAllvwProductInvoice().AsEnumerable().Where(x => x.IsArchive == true);
            if (!Filters.SecurityFilter.IsAdmin() && !Filters.SecurityFilter.IsKeToan() && string.IsNullOrEmpty(branchId))
            {
                q = q.Where(x => ("," + user.DrugStore + ",").Contains("," + x.BranchId + ",") == true);
            }
            if (!string.IsNullOrEmpty(CityId))
            {
                q = q.Where(item => !string.IsNullOrEmpty(item.CityId) && item.CityId == CityId);
            }
            if (!string.IsNullOrEmpty(DistrictId))
            {
                q = q.Where(item => !string.IsNullOrEmpty(item.DistrictId) && item.DistrictId == DistrictId);
            }
            if (!string.IsNullOrEmpty(branchId))
            {
                q = q.Where(item => !string.IsNullOrEmpty(branchId) && ("," + branchId + ",").Contains("," + item.BranchId + ",") == true);
            }
            if (year != null)
            {
                q = q.Where(n => n.Year == year);
            }
            if (month != null)
            {
                q = q.Where(n => n.Month == month);
            }
            if (day != null)
            {
                q = q.Where(n => n.Day == day);
            }

            q = q.Where(x => x.IsArchive && x.CreatedDate > StartDate && x.CreatedDate < EndDate);

            var model = q.Select(item => new ProductInvoiceViewModel
            {
                Id = item.Id,
                IsDeleted = item.IsDeleted,
                CreatedUserId = item.CreatedUserId,
                CreatedDate = item.CreatedDate,
                ModifiedUserId = item.ModifiedUserId,
                ModifiedDate = item.ModifiedDate,
                Code = item.Code,
                CustomerCode = item.CustomerCode,
                CustomerName = item.CustomerName,
                ShipCityName = item.ShipCityName,
                TotalAmount = item.TotalAmount,
                //Discount = item.Discount,
                TaxFee = item.TaxFee,
                CodeInvoiceRed = item.CodeInvoiceRed,
                Status = item.Status,
                IsArchive = item.IsArchive,
                ProductOutboundId = item.ProductOutboundId,
                ProductOutboundCode = item.ProductOutboundCode,
                Note = item.Note,
                CancelReason = item.CancelReason,
                PaidAmount = item.PaidAmount,
                RemainingAmount = item.RemainingAmount,
                BranchName = item.BranchName,
                IrregularDiscount = item.IrregularDiscount,
                FixedDiscount = item.FixedDiscount
            }).OrderByDescending(m => m.CreatedDate);

            return View(model);
        }

        public ActionResult Detail(int? Id, string TransactionCode)
        {
            var productInvoice = new vwProductInvoice();

            if (Id != null && Id.Value > 0)
            {
                productInvoice = productInvoiceRepository.GetvwProductInvoiceFullById(Id.Value);
            }

            if (!string.IsNullOrEmpty(TransactionCode))
            {
                productInvoice = productInvoiceRepository.GetvwProductInvoiceByCode(TransactionCode);
            }

            if (productInvoice == null)
            {
                return RedirectToAction("Index");
            }

            var model = new ProductInvoiceViewModel();
            AutoMapper.Mapper.Map(productInvoice, model);

            model.ReceiptViewModel = new ReceiptViewModel();
            model.NextPaymentDate_Temp = DateTime.Now.AddDays(30);
            model.ReceiptViewModel.Name = "Bán hàng - Thu tiền mặt";
            model.ReceiptViewModel.Amount = productInvoice.TotalAmount;

            //Lấy thông tin kiểm tra cho phép sửa chứng từ này hay không
            model.AllowEdit = Helpers.Common.KiemTraNgaySuaChungTu
                (model.CreatedDate.Value)
                && (Filters.SecurityFilter.IsAdmin() || ("," + Helpers.Common.CurrentUser.DrugStore + ",").Contains("," + productInvoice.BranchId + ",") == true);

            //Lấy lịch sử giao dịch thanh toán
            var listTransaction = transactionLiabilitiesRepository.GetAllvwTransaction()
                        .Where(item => item.MaChungTuGoc == productInvoice.Code)
                        .OrderByDescending(item => item.CreatedDate)
                        .ToList();

            model.ListTransactionLiabilities = new List<TransactionLiabilitiesViewModel>();
            AutoMapper.Mapper.Map(listTransaction, model.ListTransactionLiabilities);

            model.Code = productInvoice.Code;
            //model.SalerId = productInvoice.SalerId;
            //model.SalerName = productInvoice.SalerFullName;
            model.CreatedUserName = Helpers.Common.CurrentUser.FullName;
            var saleDepartmentCode = Erp.BackOffice.Helpers.Common.GetSetting("SaleDepartmentCode");

            //Lấy danh sách chi tiết đơn hàng
            model.DetailList = productInvoiceRepository.GetAllvwInvoiceDetailsByInvoiceId(productInvoice.Id).Select(x =>
                new ProductInvoiceDetailViewModel
                {
                    Id = x.Id,
                    Price = x.Price,
                    ProductId = x.ProductId,
                    PromotionDetailId = x.PromotionDetailId,
                    PromotionId = x.PromotionId,
                    PromotionValue = x.PromotionValue,
                    Quantity = x.Quantity,
                    Unit = x.Unit,
                    ProductType = x.ProductType,
                    FixedDiscount = x.FixedDiscount,
                    FixedDiscountAmount = x.FixedDiscountAmount,
                    IrregularDiscountAmount = x.IrregularDiscountAmount,
                    IrregularDiscount = x.IrregularDiscount,
                    CategoryCode = x.CategoryCode,
                    ProductInvoiceCode = x.ProductInvoiceCode,
                    ProductName = x.ProductName,
                    ProductCode = x.ProductCode,
                    ProductInvoiceId = x.ProductInvoiceId,
                    ProductGroup = x.ProductGroup,
                    CheckPromotion = x.CheckPromotion,
                    IsReturn = x.IsReturn,
                    //Status = x.Status,
                    Amount = x.Amount,
                    LoCode = x.LoCode,
                    ExpiryDate = x.ExpiryDate
                }).OrderBy(x => x.Id).ToList();

            model.GroupProduct = model.DetailList.GroupBy(x => new { x.ProductGroup }, (key, group) => new ProductInvoiceDetailViewModel
            {
                ProductGroup = key.ProductGroup,
                ProductId = group.FirstOrDefault().ProductId,
                Id = group.FirstOrDefault().Id
            }).ToList();

            foreach (var item in model.GroupProduct)
            {
                if (!string.IsNullOrEmpty(item.ProductGroup))
                {
                    var ProductGroupName = categoryRepository.GetCategoryByCode("Categories_product").Where(x => x.Value == item.ProductGroup).FirstOrDefault();
                    item.ProductGroupName = ProductGroupName.Name;
                }
            }

            //Lấy thông tin phiếu xuất kho
            if (productInvoice.ProductOutboundId != null && productInvoice.ProductOutboundId > 0)
            {
                var productOutbound = productOutboundRepository.GetvwProductOutboundById(productInvoice.ProductOutboundId.Value);
                model.ProductOutboundViewModel = new ProductOutboundViewModel();
                AutoMapper.Mapper.Map(productOutbound, model.ProductOutboundViewModel);
            }

            ViewBag.isAdmin = Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId == 1 ? true : false;

            // model.ModifiedUserName = userRepository.GetUserById(model.ModifiedUserId.Value).FullName;

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];

            return View(model);
        }
        #endregion

        #region Archive
        [HttpPost]
        public ActionResult Archive(ProductInvoiceViewModel model)
        {
            if (Request["Submit"] == "Save")
            {
                var productInvoice = productInvoiceRepository.GetProductInvoiceById(model.Id);

                //Kiểm tra cho phép sửa chứng từ này hay không
                if (Helpers.Common.KiemTraNgaySuaChungTu(productInvoice.CreatedDate.Value) == false)
                {
                    return RedirectToAction("Detail", new { Id = productInvoice.Id });
                }

                //Kiểm tra phân quyền Chi nhánh
                if (!(Filters.SecurityFilter.IsAdmin() || ("," + Helpers.Common.CurrentUser.DrugStore + ",").Contains("," + productInvoice.BranchId + ",") == true))
                {
                    return Content("Mẫu tin không tồn tại! Không có quyền truy cập!");
                }

                //Coi thử đã xuất kho chưa mới cho ghi sổ
                bool hasProductOutbound = productOutboundRepository.GetAllvwProductOutbound().Any(item => item.InvoiceId == productInvoice.Id);
                bool hasProduct = productInvoiceRepository.GetAllInvoiceDetailsByInvoiceId(productInvoice.Id).Any(item => item.ProductType == "product");

                if (!hasProductOutbound && hasProduct)
                {
                    TempData[Globals.FailedMessageKey] = "Chưa lập phiếu xuất kho!";
                    return RedirectToAction("Detail", new { Id = productInvoice.Id });
                }

                if (!productInvoice.IsArchive)
                {
                    using (var scope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        try
                        {
                            //Cập nhật thông tin thanh toán cho đơn hàng
                            productInvoice.PaymentMethod = model.ReceiptViewModel.PaymentMethod;
                            productInvoice.PaidAmount = Convert.ToDecimal(model.ReceiptViewModel.Amount);
                            productInvoice.RemainingAmount = productInvoice.TotalAmount - productInvoice.PaidAmount;
                            productInvoice.NextPaymentDate = model.NextPaymentDate_Temp;

                            productInvoice.ModifiedDate = DateTime.Now;
                            productInvoice.ModifiedUserId = WebSecurity.CurrentUserId;
                            productInvoiceRepository.UpdateProductInvoice(productInvoice);

                            //Lấy mã KH
                            var customer = customerRepository.GetCustomerById(productInvoice.CustomerId.Value);

                            var remain = productInvoice.TotalAmount - Convert.ToDecimal(model.ReceiptViewModel.Amount.Value);
                            if (remain > 0)
                            {

                            }
                            else
                            {
                                productInvoice.NextPaymentDate = null;
                                model.NextPaymentDate_Temp = null;
                            }

                            //Ghi Nợ TK 131 - Phải thu của khách hàng (tổng giá thanh toán)
                            Erp.BackOffice.Account.Controllers.TransactionLiabilitiesController.Create(
                                    productInvoice.Code,
                                    "ProductInvoice",
                                    "Bán hàng",
                                    customer.Code,
                                    "Customer",
                                    productInvoice.TotalAmount,
                                    0,
                                    productInvoice.Code,
                                    "ProductInvoice",
                                    null,
                                    model.NextPaymentDate_Temp,
                                    null);

                            //Khách thanh toán ngay
                            if (model.ReceiptViewModel.Amount > 0)
                            {
                                //Lập phiếu thu
                                var receipt = new Receipt();
                                AutoMapper.Mapper.Map(model.ReceiptViewModel, receipt);
                                receipt.IsDeleted = false;
                                receipt.CreatedUserId = WebSecurity.CurrentUserId;
                                receipt.ModifiedUserId = WebSecurity.CurrentUserId;
                                receipt.AssignedUserId = WebSecurity.CurrentUserId;
                                receipt.CreatedDate = DateTime.Now;
                                receipt.ModifiedDate = DateTime.Now;
                                receipt.VoucherDate = DateTime.Now;
                                receipt.CustomerId = customer.Id;
                                receipt.Payer = customer.LastName + " " + customer.FirstName;
                                receipt.PaymentMethod = productInvoice.PaymentMethod;
                                receipt.Address = customer.Address;
                                receipt.MaChungTuGoc = productInvoice.Code;
                                receipt.LoaiChungTuGoc = "ProductInvoice";
                                receipt.Note = receipt.Name;
                                //receipt.BranchId = Helpers.Common.CurrentUser.BranchId.Value;

                                if (receipt.Amount > productInvoice.TotalAmount)
                                    receipt.Amount = productInvoice.TotalAmount;

                                ReceiptRepository.InsertReceipt(receipt);

                                var prefixReceipt = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_ReceiptCustomer");
                                receipt.Code = Erp.BackOffice.Helpers.Common.GetCode(prefixReceipt, receipt.Id);
                                ReceiptRepository.UpdateReceipt(receipt);

                                //Thêm vào quản lý chứng từ
                                TransactionController.Create(new TransactionViewModel
                                {
                                    TransactionModule = "Receipt",
                                    TransactionCode = receipt.Code,
                                    TransactionName = "Thu tiền khách hàng"
                                });

                                //Thêm chứng từ liên quan
                                TransactionController.CreateRelationship(new TransactionRelationshipViewModel
                                {
                                    TransactionA = receipt.Code,
                                    TransactionB = productInvoice.Code
                                });

                                //Ghi Có TK 131 - Phải thu của khách hàng.
                                Erp.BackOffice.Account.Controllers.TransactionLiabilitiesController.Create(
                                    receipt.Code,
                                    "Receipt",
                                    "Thu tiền khách hàng",
                                    customer.Code,
                                    "Customer",
                                    0,
                                    Convert.ToDecimal(model.ReceiptViewModel.Amount),
                                    productInvoice.Code,
                                    "ProductInvoice",
                                    model.ReceiptViewModel.PaymentMethod,
                                    null,
                                    null);
                            }
                            scope.Complete();
                        }
                        catch (DbUpdateException)
                        {
                            return Content("Fail");
                        }
                    }
                }

                //Cập nhật đơn hàng
                productInvoice.ModifiedUserId = WebSecurity.CurrentUserId;
                productInvoice.ModifiedDate = DateTime.Now;
                productInvoice.IsArchive = true;
                productInvoice.Status = Wording.OrderStatus_complete;
                productInvoiceRepository.UpdateProductInvoice(productInvoice);
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.ArchiveSuccess;
                //Tạo chiết khấu cho nhân viên nếu có
                //CommisionStaffController.CreateCommission(productInvoice.Id);
                //tính điểm tích lũy hóa đơn cho khách hàng
                //Erp.BackOffice.Sale.Controllers.LogLoyaltyPointController.CreateLogLoyaltyPoint(productInvoice.CustomerId, productInvoice.TotalAmount, productInvoice.Id);
                //Cảnh báo cập nhật phiếu xuất kho
                if (hasProductOutbound)
                {
                    TempData[Globals.SuccessMessageKey] += "<br/>Đơn hàng này đã được xuất kho! Vui lòng kiểm tra lại chứng từ xuất kho để tránh sai xót dữ liệu!";
                }
            }

            return RedirectToAction("Detail", new { Id = model.Id });
        }
        #endregion

        #region UnArchive
        [HttpPost]
        public ActionResult UnArchive(int Id, bool IsPopup)
        {
            if (Request["Submit"] == "Save")
            {
                var productInvoice = productInvoiceRepository.GetProductInvoiceById(Id);

                //Kiểm tra phân quyền Chi nhánh
                if (!(Filters.SecurityFilter.IsAdmin() || ("," + Helpers.Common.CurrentUser.DrugStore + ",").Contains("," + productInvoice.BranchId + ",") == true))
                {
                    return Content("Mẫu tin không tồn tại! Không có quyền truy cập!");
                }

                //Kiểm tra cho phép sửa chứng từ này hay không
                if (Helpers.Common.KiemTraNgaySuaChungTu(productInvoice.CreatedDate.Value) == false)
                {
                    TempData[Globals.FailedMessageKey] = "Quá hạn sửa chứng từ!";
                }
                else
                {
                    using (var scope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        try
                        {
                            #region isDelete receipt
                            var receipt = ReceiptRepository.GetAllReceipts()
                                .Where(item => item.MaChungTuGoc == productInvoice.Code).ToList();
                            var receipt_detail = receiptDetailRepository.GetAllReceiptDetail().Where(x => x.MaChungTuGoc == productInvoice.Code).ToList();
                            if (receipt_detail.Count() > 0)
                            {
                                // isDelete chi tiết phiếu thu
                                for (int i = 0; i < receipt_detail.Count(); i++)
                                {
                                    receiptDetailRepository.DeleteReceiptDetailRs(receipt_detail[i].Id);
                                }
                            }
                            if (receipt.Count() > 0)
                            {
                                // isDelete phiếu thu
                                for (int i = 0; i < receipt.Count(); i++)
                                {
                                    ReceiptRepository.DeleteReceiptRs(receipt[i].Id);
                                }
                            }
                            #endregion

                            #region isDelete listTransaction
                            //isDelete lịch sử giao dịch
                            var listTransaction = transactionLiabilitiesRepository.GetAllTransaction()
                            .Where(item => item.MaChungTuGoc == productInvoice.Code)
                            .Select(item => item.Id)
                            .ToList();

                            foreach (var item in listTransaction)
                            {
                                transactionLiabilitiesRepository.DeleteTransactionRs(item);
                            }
                            #endregion

                            #region bỏ ghi sổ ProductOutbound
                            var productOutbound = productOutboundRepository.GetAllProductOutbound().Where(x => x.InvoiceId == productInvoice.Id).ToList();
                            foreach (var item in productOutbound)
                            {
                                //Update các lô/date đã xuất = false
                                var listWarehouseLocationItem = warehouseLocationItemRepository.GetAllWarehouseLocationItem()
                                    .Where(x => x.ProductOutboundId == item.Id).ToList();
                                foreach (var items in listWarehouseLocationItem)
                                {
                                    items.IsOut = false;
                                    items.ProductOutboundId = null;
                                    items.ProductOutboundDetailId = null;
                                    items.ModifiedUserId = WebSecurity.CurrentUserId;
                                    items.ModifiedDate = DateTime.Now;
                                    warehouseLocationItemRepository.UpdateWarehouseLocationItem(items);
                                }
                                string check = "";
                                var detail = productOutboundRepository.GetAllvwProductOutboundDetailByOutboundId(item.Id).ToList();
                                foreach (var item2 in detail)
                                {
                                    var error = InventoryController.Check(item2.ProductName, item2.ProductId.Value, item2.LoCode, item2.ExpiryDate, item.WarehouseSourceId.Value, item2.Quantity.Value, 0);
                                    check += error;
                                }
                                if (string.IsNullOrEmpty(check))
                                {
                                    //Khi đã hợp lệ thì mới update
                                    foreach (var i in detail)
                                    {
                                        InventoryController.Update(i.ProductName, i.ProductId.Value, i.LoCode, i.ExpiryDate, item.WarehouseSourceId.Value, i.Quantity.Value, 0);
                                    }

                                    item.IsArchive = false;
                                    productOutboundRepository.UpdateProductOutbound(item);
                                    TempData[Globals.SuccessMessageKey] = "Đã bỏ ghi sổ";
                                }
                                else
                                {
                                    TempData[Globals.FailedMessageKey] = App_GlobalResources.Wording.ArchiveFail + check;
                                }
                                //productOutboundRepository.DeleteProductOutboundRs(item.Id);

                            }
                            #endregion

                            productInvoice.PaidAmount = 0;
                            productInvoice.RemainingAmount = productInvoice.TotalAmount;
                            productInvoice.NextPaymentDate = null;

                            productInvoice.ModifiedUserId = WebSecurity.CurrentUserId;
                            productInvoice.ModifiedDate = DateTime.Now;
                            productInvoice.IsArchive = false;
                            productInvoice.Status = Wording.OrderStatus_inprogress;
                            productInvoiceRepository.UpdateProductInvoice(productInvoice);
                            TempData[Globals.SuccessMessageKey] = "Đã bỏ ghi sổ";
                            //cap nhat chiet khau nha thuoc
                            Erp.BackOffice.Sale.Controllers.TotalDiscountMoneyNTController.SyncTotalDisCountMoneyNT(productInvoice,WebSecurity.CurrentUserId);
                            //cap nhat hoa hong nhan vien
                            Erp.BackOffice.Staff.Controllers.HistoryCommissionStaffController.SyncCommissionStaff(productInvoice,WebSecurity.CurrentUserId);
                            scope.Complete();
                        }
                        catch (DbUpdateException)
                        {
                            return Content("Fail");
                        }
                    }
                }
            }

            return RedirectToAction("Detail", new { Id = Id, IsPopup = IsPopup });
        }
        #endregion

        string bill(int Id)
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
            var ImgLogo = "<div class=\"logo\"><img src=http://ngochuong.osales.vn/" + logo + " height=\"73\" /></div>";
            //lấy hóa đơn.
            var productInvoice = productInvoiceRepository.GetvwProductInvoiceById(Id);
            //lấy thông tin khách hàng
            var customer = customerRepository.GetvwCustomerByCode(productInvoice.CustomerCode);

            List<ProductInvoiceDetailViewModel> detailList = new List<ProductInvoiceDetailViewModel>();
            if (productInvoice != null && productInvoice.IsDeleted != true)
            {
                //lấy danh sách sản phẩm xuất kho
                detailList = productInvoiceRepository.GetAllvwInvoiceDetailsByInvoiceId(Id)
                        .Select(x => new ProductInvoiceDetailViewModel
                        {
                            Id = x.Id,
                            Price = x.Price,
                            ProductId = x.ProductId,
                            ProductName = x.ProductName,
                            ProductCode = x.ProductCode,
                            Quantity = x.Quantity,
                            Unit = x.Unit,
                            ProductGroup = x.ProductGroup,
                            CheckPromotion = x.CheckPromotion,
                            ProductType = x.ProductType,
                            CategoryCode = x.CategoryCode
                        }).ToList();
            }
            //lấy template phiếu xuất.
            var template = templatePrintRepository.GetAllTemplatePrint().Where(x => x.Code.Contains("ProductInvoice")).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            //truyền dữ liệu vào template.
            model.Content = template.Content;
            model.Content = model.Content.Replace("{Code}", productInvoice.Code);
            model.Content = model.Content.Replace("{CreateDate}", productInvoice.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm"));
            model.Content = model.Content.Replace("{Hours}", productInvoice.CreatedDate.Value.ToString("HH:mm"));
            model.Content = model.Content.Replace("{CustomerName}", customer.LastName + " " + customer.FirstName);
            model.Content = model.Content.Replace("{CustomerPhone}", customer.Phone);
            model.Content = model.Content.Replace("{CompanyName}", customer.CompanyName);
            model.Content = model.Content.Replace("{StaffCreateName}", productInvoice.StaffCreateName);
            if (!string.IsNullOrEmpty(customer.Address))
            {
                model.Content = model.Content.Replace("{Address}", customer.Address + ", ");
            }
            else
            {
                model.Content = model.Content.Replace("{Address}", "");
            }
            if (!string.IsNullOrEmpty(customer.DistrictName))
            {
                model.Content = model.Content.Replace("{District}", customer.DistrictName + ", ");
            }
            else
            {
                model.Content = model.Content.Replace("{District}", "");
            }
            if (!string.IsNullOrEmpty(customer.WardName))
            {
                model.Content = model.Content.Replace("{Ward}", customer.WardName + ", ");
            }
            else
            {
                model.Content = model.Content.Replace("{Ward}", "");
            }
            if (!string.IsNullOrEmpty(customer.ProvinceName))
            {
                model.Content = model.Content.Replace("{Province}", customer.ProvinceName);
            }
            else
            {
                model.Content = model.Content.Replace("{Province}", "");
            }

            model.Content = model.Content.Replace("{Note}", productInvoice.Note);
            if (!string.IsNullOrEmpty(productInvoice.CodeInvoiceRed))
            {
                model.Content = model.Content.Replace("{InvoiceCode}", productInvoice.Code + " - " + productInvoice.CodeInvoiceRed);
            }
            else
            {
                model.Content = model.Content.Replace("{InvoiceCode}", productInvoice.Code);
            }
            model.Content = model.Content.Replace("{PaymentMethod}", productInvoice.PaymentMethod);
            model.Content = model.Content.Replace("{MoneyText}", Erp.BackOffice.Helpers.Common.ChuyenSoThanhChu_2(Convert.ToInt32(productInvoice.TotalAmount)));

            model.Content = model.Content.Replace("{DataTable}", buildHtmlDetailList(detailList, productInvoice));
            model.Content = model.Content.Replace("{System.Logo}", ImgLogo);
            model.Content = model.Content.Replace("{System.CompanyName}", company);
            model.Content = model.Content.Replace("{System.AddressCompany}", address);
            model.Content = model.Content.Replace("{System.PhoneCompany}", phone);
            model.Content = model.Content.Replace("{System.Fax}", fax);
            model.Content = model.Content.Replace("{System.BankCodeCompany}", bankcode);
            model.Content = model.Content.Replace("{System.BankNameCompany}", bankname);
            //model.Content = model.Content.Replace("{Reminder}", NoteReminder);
            model.Content = model.Content.Replace("{PrintDate}", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            //Tạo barcode
            Image barcode = Code128Rendering.MakeBarcodeImage(productInvoice.Code, 1, true);
            model.Content = model.Content.Replace("{BarcodeImgSource}", ImageToBase64(barcode, System.Drawing.Imaging.ImageFormat.Png));

            return model.Content;
        }


        //#region CreateFromSchedule

        //public ActionResult CreateFromSchedule()
        //{

        //    ProductInvoiceViewModel model = new ProductInvoiceViewModel();
        //    model.DetailList = new List<ProductInvoiceDetailViewModel>();
        //    var service_schedule = Request["service_schedule"];
        //    if (!string.IsNullOrEmpty(service_schedule))
        //    {
        //        string[] list = service_schedule.Split(',');
        //        List<vwServiceSchedule> scheduleList = new List<vwServiceSchedule>();
        //        int n = 0;
        //        for (int i = 0; i < list.Count(); i++)
        //        {
        //            if (list[i] != "")
        //            {
        //                var schedule = serviceScheduleRepository.GetvwServiceScheduleById(int.Parse(list[i], CultureInfo.InvariantCulture));
        //                model.CustomerId = schedule.CustomerId;
        //                model.CustomerName = schedule.CustomerName;
        //                var nhan_vien_phuc_vu = taskRepository.GetAllvwTask().Where(x => x.ParentType == "ServiceSchedule" && x.ParentId == schedule.Id).OrderByDescending(x => x.CreatedDate).ToList();
        //                var staff_user = 0;
        //                var staff_name = "";
        //                if (nhan_vien_phuc_vu.Count > 0)
        //                {
        //                    staff_user = nhan_vien_phuc_vu.FirstOrDefault().AssignedUserId.Value;
        //                    var staff = staffRepository.GetvwAllStaffs().Where(x => x.UserId.Value == staff_user).ToList();
        //                    if (staff.Count() > 0)
        //                    {
        //                        staff_user = staff.FirstOrDefault().Id;
        //                        staff_name = staff.FirstOrDefault().Name;
        //                    }

        //                }
        //                var product = ProductRepository.GetProductById(schedule.ServiceId.Value);
        //                model.DetailList.Add(new ProductInvoiceDetailViewModel
        //                {
        //                    OrderNo = n,
        //                    ProductId = product.Id,
        //                    Quantity = 1,
        //                    Unit = product.Unit,
        //                    Price = product.PriceOutbound,
        //                    ProductCode = product.Code,
        //                    ProductName = product.Name,
        //                    IsCombo = product.IsCombo,
        //                    ProductType = product.Type,
        //                    StaffId = staff_user,
        //                    SalerName = staff_name,
        //                    CategoryCode = product.CategoryCode
        //                });
        //                n++;
        //            }
        //        }
        //    }
        //    model.ReceiptViewModel = new ReceiptViewModel();
        //    model.NextPaymentDate_Temp = DateTime.Now.AddDays(30);
        //    model.ReceiptViewModel.Name = "Bán hàng - Thu tiền mặt";
        //    model.ReceiptViewModel.Amount = 0;

        //    var saleDepartmentCode = Erp.BackOffice.Helpers.Common.GetSetting("SaleDepartmentCode");
        //    string image_folder_product = Helpers.Common.GetSetting("product-image-folder");
        //    string image_folder_service = Helpers.Common.GetSetting("service-image-folder");

        //    var branchId = Helpers.Common.CurrentUser.BranchId.Value;
        //    var productList = inventoryRepository.GetAllvwInventoryByBranchId(branchId).Where(x => x.Quantity > 0)
        //       .Select(item => new ProductViewModel
        //       {
        //           Type = "product",
        //           Code = item.ProductCode,
        //           Name = item.ProductName,
        //           Id = item.ProductId,
        //           CategoryCode = string.IsNullOrEmpty(item.CategoryCode) ? "Sản phẩm khác" : item.CategoryCode,
        //           PriceOutbound = item.ProductPriceOutbound,
        //           Unit = item.ProductUnit,
        //           QuantityTotalInventory = item.Quantity
        //       }).OrderBy(item => item.CategoryCode).ToList();

        //    var serviceList = ProductRepository.GetAllvwProductAndService()
        //        .Where(item => item.Type == "service")
        //        .Select(item => new ProductViewModel
        //        {
        //            Type = item.Type,
        //            Code = item.Code,
        //            Name = item.Name,
        //            Id = item.Id,
        //            CategoryCode = string.IsNullOrEmpty(item.CategoryCode) ? "Dịch vụ khác" : item.CategoryCode,
        //            PriceOutbound = item.PriceOutbound,
        //            Unit = item.Unit,
        //            QuantityTotalInventory = 0,
        //            Image_Name = item.Image_Name,
        //            IsCombo = item.IsCombo
        //        }).OrderBy(item => item.CategoryCode).ToList();

        //    var unionList = productList.Union(serviceList).OrderBy(item => item.Type).ThenBy(item => item.CategoryCode).ThenBy(item => item.Code);
        //    ViewBag.productList = unionList;

        //    var jsonProductInvoiceItem = unionList.Select(item => new ProductInvoiceItemViewModel
        //    {
        //        Id = item.Id,
        //        Type = item.Type,
        //        Code = item.Code,
        //        Name = item.Name.Replace("\"", "\\\""),
        //        CategoryCode = item.CategoryCode,
        //        PriceOutbound = item.PriceOutbound,
        //        Unit = item.Unit,
        //        InventoryQuantity = item.QuantityTotalInventory.Value,
        //        Image_Name = item.Image_Name,
        //        IsCombo = item.IsCombo
        //    }).ToList();

        //    ViewBag.json = JsonConvert.SerializeObject(jsonProductInvoiceItem);

        //    List<SelectListItem> categoryList = new List<SelectListItem>();
        //    categoryList.Add(new SelectListItem { Text = "TẤT CẢ", Value = "0" });
        //    categoryList.Add(new SelectListItem { Text = "I. SẢN PHẨM", Value = "1" });
        //    foreach (var item in productList.GroupBy(i => i.CategoryCode).Select(i => new { Name = i.Key, NumberOfItem = i.Count() }))
        //    {
        //        categoryList.Add(new SelectListItem { Text = "---- " + item.Name + " (" + item.NumberOfItem + ")", Value = item.Name });
        //    }
        //    categoryList.Add(new SelectListItem { Text = "II. DỊCH VỤ", Value = "2" });
        //    foreach (var item in serviceList.GroupBy(i => i.CategoryCode).Select(i => new { Name = i.Key, NumberOfItem = i.Count() }))
        //    {
        //        categoryList.Add(new SelectListItem { Text = "---- " + item.Name + " (" + item.NumberOfItem + ")", Value = item.Name });
        //    }

        //    ViewBag.CategoryList = categoryList;

        //    model.CreatedUserName = Erp.BackOffice.Helpers.Common.CurrentUser.FullName;

        //    int taxfee = 0;
        //    int.TryParse(Helpers.Common.GetSetting("vat"), out taxfee);
        //    model.TaxFee = taxfee;

        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult CreateFromSchedule(ProductInvoiceViewModel model)
        //{
        //    if (ModelState.IsValid && model.DetailList.Count != 0)
        //    {

        //        ProductInvoice productInvoice = new ProductInvoice();
        //        AutoMapper.Mapper.Map(model, productInvoice);
        //        productInvoice.IsDeleted = false;
        //        productInvoice.CreatedUserId = WebSecurity.CurrentUserId;
        //        productInvoice.CreatedDate = DateTime.Now;
        //        productInvoice.Status = Wording.OrderStatus_pending;
        //        productInvoice.BranchId = Helpers.Common.CurrentUser.BranchId.Value;
        //        productInvoice.IsArchive = false;
        //        productInvoice.IsReturn = false;
        //        //Duyệt qua danh sách sản phẩm mới xử lý tình huống user chọn 2 sản phầm cùng id
        //        List<ProductInvoiceDetail> listNewCheckSameId = new List<ProductInvoiceDetail>();
        //        foreach (var group in model.DetailList)
        //        {
        //            var product = ProductRepository.GetProductById(group.ProductId.Value);
        //            listNewCheckSameId.Add(new ProductInvoiceDetail
        //            {
        //                ProductId = product.Id,
        //                ProductType = product.Type,
        //                Quantity = group.Quantity,
        //                Unit = product.Unit,
        //                Price = group.Price,
        //                PromotionDetailId = group.PromotionDetailId,
        //                PromotionId = group.PromotionId,
        //                PromotionValue = group.PromotionValue,
        //                IsDeleted = false,
        //                CreatedUserId = WebSecurity.CurrentUserId,
        //                ModifiedUserId = WebSecurity.CurrentUserId,
        //                CreatedDate = DateTime.Now,
        //                ModifiedDate = DateTime.Now,
        //                FixedDiscount = group.FixedDiscount,
        //                IrregularDiscount = group.IrregularDiscount,
        //                CheckPromotion = (group.CheckPromotion.HasValue ? group.CheckPromotion.Value : false),
        //                IsReturn = false
        //            });
        //        }

        //        //Tính lại chiết khấu
        //        foreach (var item in listNewCheckSameId)
        //        {
        //            var thanh_tien = item.Quantity * item.Price;
        //            item.FixedDiscountAmount = Convert.ToInt32(item.FixedDiscount * thanh_tien / 100);
        //            item.IrregularDiscountAmount = Convert.ToInt32(item.IrregularDiscount * thanh_tien / 100);
        //            //tổng Point

        //        }
        //        var total = listNewCheckSameId.Sum(item => item.Quantity.Value * item.Price.Value - item.FixedDiscountAmount.Value-item.IrregularDiscountAmount.Value);
        //        productInvoice.TotalAmount = total + (total * Convert.ToInt32(Erp.BackOffice.Helpers.Common.GetSetting("VAT")));

        //        productInvoice.IsReturn = false;
        //        productInvoice.ModifiedUserId = WebSecurity.CurrentUserId;
        //        productInvoice.ModifiedDate = DateTime.Now;
        //        productInvoice.PaidAmount = 0;
        //        productInvoice.RemainingAmount = productInvoice.TotalAmount;

        //        //hàm thêm mới
        //        productInvoiceRepository.InsertProductInvoice(productInvoice, listNewCheckSameId);

        //        //cập nhật lại mã hóa đơn
        //        string prefix = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_Invoice");
        //        productInvoice.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix, productInvoice.Id);
        //        productInvoiceRepository.UpdateProductInvoice(productInvoice);

        //        //Kiểm tra xem KH có mua DV nào hay không, nếu có thì tạo dữ liệu cho bảng UsingService, ServiceReminder
        //        var listService = listNewCheckSameId.Where(x => x.ProductType == "service").ToList();
        //        if (listService.Count != 0)
        //        {
        //            foreach (var item in listService)
        //            {
        //                CreateUsingLogAndReminder(item, model.DetailList, productInvoice.CreatedDate);
        //            }
        //        }

        //        //Thêm vào quản lý chứng từ
        //        TransactionController.Create(new TransactionViewModel
        //        {
        //            TransactionModule = "ProductInvoice",
        //            TransactionCode = productInvoice.Code,
        //            TransactionName = "Bán hàng"
        //        });
        //        //Tạo phiếu nhập, nếu là tự động
        //        string sale_tu_dong_tao_chung_tu = Erp.BackOffice.Helpers.Common.GetSetting("sale_tu_dong_tao_chung_tu");
        //        if (sale_tu_dong_tao_chung_tu == "true")
        //        {
        //            ProductOutboundViewModel productOutboundViewModel = new ProductOutboundViewModel();
        //            var warehouseDefault = warehouseRepository.GetAllWarehouse().Where(x => x.BranchId == Helpers.Common.CurrentUser.BranchId).FirstOrDefault();

        //            //Nếu trong đơn hàng có sản phẩm thì xuất kho
        //            if (model.DetailList.Any(item => item.ProductType == "product") && warehouseDefault != null)
        //            {
        //                productOutboundViewModel.InvoiceId = productInvoice.Id;
        //                productOutboundViewModel.InvoiceCode = productInvoice.Code;
        //                productOutboundViewModel.WarehouseSourceId = warehouseDefault.Id;
        //                productOutboundViewModel.Note = "Xuất kho cho đơn hàng " + productInvoice.Code;

        //                //Lấy dữ liệu cho chi tiết
        //                productOutboundViewModel.DetailList = new List<ProductOutboundDetailViewModel>();
        //                AutoMapper.Mapper.Map(model.DetailList, productOutboundViewModel.DetailList);

        //                var productOutbound = ProductOutboundController.CreateFromInvoice(productOutboundRepository, productOutboundViewModel, productInvoice.Code);

        //                //Ghi sổ chứng từ phiếu xuất
        //                ProductOutboundController.Archive(productOutboundRepository, warehouseLocationItemRepository, productOutbound, TempData);
        //            }

        //            //Ghi sổ chứng từ bán hàng
        //            model.Id = productInvoice.Id;
        //            Archive(model);
        //        }

        //        //Run process
        //        //var dataSource = null;
        //        //Crm.Controllers.ProcessController.Run("ProductInvoice", "Create", null, null, null, dataSource);

        //        //send email
        //        //var ii = productInvoiceRepository.GetvwProductInvoiceById(model.Id);
        //        //Erp.BackOffice.Helpers.Common.SendEmail(ii.Email, ii.BranchName, bill(ii.Id));
        //        ////lưu lịch sử email
        //        //Erp.BackOffice.Crm.Controllers.EmailLogController.SaveEmail(ii.Email, bill(ii.Id), ii.CustomerId, ii.Id, "ProductInvoice", ii.BranchName);
        //        ////send sms
        //        //var body = Erp.BackOffice.Helpers.Common.GetSetting("SMSSetting_Body") + "-" + ii.Code + "-" + ii.TotalAmount;
        //        //Erp.BackOffice.Helpers.Common.SendSMS(ii.Phone, body);
        //        ////lưu lịch sử sms
        //        //Erp.BackOffice.Crm.Controllers.SMSLogController.SaveSMS(ii.Phone, body, ii.CustomerId, ii.Id, "ProductInvoice", ii.BranchName);

        //        //var prID = productInvoiceRepository.GetvwProductInvoiceById(model.Id);
        //        // //lưu điểm vào Customer
        //        //var point = productInvoiceRepository.GetvwProductInvoiceDetailById(productInvoice.Id);
        //        //Erp.BackOffice.Account.Controllers.CustomerController.SavePoint(prID.CustomerId, item.P);
        //        return RedirectToAction("Detail", new { Id = productInvoice.Id });
        //    }

        //    return View();
        //}
        //#endregion

        #region Edit

        public ActionResult Edit(int? Id)
        {
            ProductInvoiceViewModel model = new ProductInvoiceViewModel();
            var invoice = productInvoiceRepository.GetProductInvoiceById(Id.Value);
            if (invoice != null && invoice.IsDeleted != true)
            {
                AutoMapper.Mapper.Map(invoice, model);
                return View(model);
            }

            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(ProductInvoiceViewModel model)
        {
            if (Request["Submit"] == "Save")
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        var invoice = productInvoiceRepository.GetProductInvoiceById(model.Id);

                        invoice.ModifiedUserId = WebSecurity.CurrentUserId;
                        invoice.ModifiedDate = DateTime.Now;
                        invoice.Note = model.Note;
                        invoice.CodeInvoiceRed = model.CodeInvoiceRed;

                        productInvoiceRepository.UpdateProductInvoice(invoice);
                        scope.Complete();
                    }
                    catch (DbUpdateException)
                    {
                        return Content("Fail");
                    }
                }
                if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                {
                    return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
                }
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Success
        [HttpPost]
        public ActionResult Success(ProductInvoiceViewModel model)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    var productInvoice = productInvoiceRepository.GetProductInvoiceById(model.Id);
                    var invoice_detail_list = productInvoiceRepository.GetAllvwInvoiceDetailsByInvoiceId(productInvoice.Id).ToList();
                    ProductOutboundViewModel productOutboundViewModel = new ProductOutboundViewModel();
                    var warehouseDefault = warehouseRepository.GetAllWarehouse().Where(x => x.BranchId == productInvoice.BranchId && x.IsSale == true).FirstOrDefault();
                    model.ReceiptViewModel = new ReceiptViewModel();
                    model.NextPaymentDate_Temp = DateTime.Now;
                    model.ReceiptViewModel.Amount = productInvoice.TotalAmount;
                    model.ReceiptViewModel.Name = string.Empty;
                    model.ReceiptViewModel.PaymentMethod = SelectListHelper.GetSelectList_Category("FormPayment", null, "Name", null).FirstOrDefault().Value;

                    if (warehouseDefault == null)
                    {
                        TempData[Globals.FailedMessageKey] += "<br/>Nhà thuốc này không tìm thấy kho xuất bán! Vui lòng kiểm tra lại!";
                        return RedirectToAction("Detail", new { Id = model.Id });
                    }
                    string check = "";
                    foreach (var item in invoice_detail_list)
                    {
                        var error = InventoryController.Check(item.ProductName, item.ProductId.Value, item.LoCode, item.ExpiryDate, warehouseDefault.Id, 0, item.Quantity.Value);
                        check += error;
                    }
                    if (!string.IsNullOrEmpty(check))
                    {
                        TempData[Globals.FailedMessageKey] = App_GlobalResources.Wording.ArchiveFail + check;
                        return RedirectToAction("Detail", new { Id = model.Id });
                    }
                    #region  phiếu xuất ok
                    var product_outbound = productOutboundRepository.GetAllProductOutboundFull().Where(x => x.InvoiceId == productInvoice.Id).ToList();

                    //insert mới
                    if (product_outbound.Count() <= 0)
                    {
                        #region  thêm mới phiếu xuất

                        //Nếu trong đơn hàng có sản phẩm thì xuất kho
                        if (warehouseDefault != null)
                        {
                            productOutboundViewModel.InvoiceId = productInvoice.Id;
                            productOutboundViewModel.InvoiceCode = productInvoice.Code;
                            productOutboundViewModel.WarehouseSourceId = warehouseDefault.Id;
                            productOutboundViewModel.Note = "Xuất kho cho đơn hàng " + productInvoice.Code;
                            var DetailList = invoice_detail_list.Select(x =>
                                  new ProductInvoiceDetailViewModel
                                  {
                                      Id = x.Id,
                                      Price = x.Price,
                                      ProductId = x.ProductId,
                                      PromotionDetailId = x.PromotionDetailId,
                                      PromotionId = x.PromotionId,
                                      PromotionValue = x.PromotionValue,
                                      Quantity = x.Quantity,
                                      Unit = x.Unit,
                                      ProductType = x.ProductType,
                                      FixedDiscount = x.FixedDiscount,
                                      FixedDiscountAmount = x.FixedDiscountAmount,
                                      IrregularDiscount = x.IrregularDiscount,
                                      IrregularDiscountAmount = x.IrregularDiscountAmount,
                                      CategoryCode = x.CategoryCode,
                                      ProductInvoiceCode = x.ProductInvoiceCode,
                                      ProductName = x.ProductName,
                                      ProductCode = x.ProductCode,
                                      ProductInvoiceId = x.ProductInvoiceId,
                                      ProductGroup = x.ProductGroup,
                                      CheckPromotion = x.CheckPromotion,
                                      IsReturn = x.IsReturn,
                                      //Status = x.Status
                                      LoCode = x.LoCode,
                                      ExpiryDate = x.ExpiryDate,
                                      Amount = x.Amount,
                                      Type = x.Type
                                  }).ToList();
                            //Lấy dữ liệu cho chi tiết
                            productOutboundViewModel.DetailList = new List<ProductOutboundDetailViewModel>();
                            AutoMapper.Mapper.Map(DetailList, productOutboundViewModel.DetailList);

                            var productOutbound = ProductOutboundController.AutoCreateProductOutboundFromProductInvoice(productOutboundRepository, productOutboundViewModel, productInvoice.Code);
                            //ProductOutboundController.Archive_mobile(productOutbound, model.CreatedUserId.GetValueOrDefault());
                            //Ghi sổ chứng từ phiếu xuất
                            ProductOutboundController.Archive(productOutbound, TempData);
                        }
                        #endregion
                    }
                    else
                    {
                        #region   cập nhật phiếu xuất kho
                        //xóa chi tiết phiếu xuất, insert chi tiết mới
                        //cập nhật lại tổng tiền, trạng thái phiếu xuất
                        for (int i = 0; i < product_outbound.Count(); i++)
                        {
                            var outbound_detail = productOutboundRepository.GetAllProductOutboundDetailByOutboundId(product_outbound[i].Id).ToList();
                            //xóa
                            for (int ii = 0; ii < outbound_detail.Count(); ii++)
                            {
                                productOutboundRepository.DeleteProductOutboundDetail(outbound_detail[ii].Id);
                            }
                            //insert mới
                            for (int iii = 0; iii < invoice_detail_list.Count(); iii++)
                            {
                                ProductOutboundDetail productOutboundDetail = new ProductOutboundDetail();
                                productOutboundDetail.Price = invoice_detail_list[iii].Price;
                                productOutboundDetail.ProductId = invoice_detail_list[iii].ProductId;
                                productOutboundDetail.Quantity = invoice_detail_list[iii].Quantity;
                                productOutboundDetail.Unit = invoice_detail_list[iii].Unit;
                                productOutboundDetail.LoCode = invoice_detail_list[iii].LoCode;
                                productOutboundDetail.ExpiryDate = invoice_detail_list[iii].ExpiryDate;
                                productOutboundDetail.IsDeleted = false;
                                productOutboundDetail.CreatedUserId = WebSecurity.CurrentUserId;
                                productOutboundDetail.ModifiedUserId = WebSecurity.CurrentUserId;
                                productOutboundDetail.CreatedDate = DateTime.Now;
                                productOutboundDetail.ModifiedDate = DateTime.Now;
                                productOutboundDetail.ProductOutboundId = product_outbound[i].Id;
                                productOutboundRepository.InsertProductOutboundDetail(productOutboundDetail);

                            }
                            product_outbound[i].TotalAmount = invoice_detail_list.Sum(x => (x.Price * x.Quantity));

                            //Ghi sổ chứng từ phiếu xuất
                            ProductOutboundController.Archive(product_outbound[i], TempData);
                        }
                        #endregion
                    }
                    #endregion

                    #region  xóa hết lịch sử giao dịch
                    //xóa lịch sử giao dịch có liên quan đến đơn hàng, gồm: 1 dòng giao dịch bán hàng, 1 dòng thu tiền.
                    var transaction_Liablities = transactionLiabilitiesRepository.GetAllTransaction().Where(x => x.MaChungTuGoc == productInvoice.Code && x.LoaiChungTuGoc == "ProductInvoice").ToList();
                    if (transaction_Liablities.Count() > 0)
                    {
                        for (int i = 0; i < transaction_Liablities.Count(); i++)
                        {
                            transactionLiabilitiesRepository.DeleteTransaction(transaction_Liablities[i].Id);
                        }
                    }
                    #endregion

                    if (!productInvoice.IsArchive)
                    {
                        #region  Cập nhật thông tin thanh toán cho đơn hàng
                        //Cập nhật thông tin thanh toán cho đơn hàng
                        productInvoice.PaymentMethod = model.ReceiptViewModel.PaymentMethod;
                        productInvoice.PaidAmount = Convert.ToDecimal(model.ReceiptViewModel.Amount);
                        productInvoice.RemainingAmount = productInvoice.TotalAmount - productInvoice.PaidAmount;
                        productInvoice.NextPaymentDate = model.NextPaymentDate_Temp;

                        productInvoice.ModifiedDate = DateTime.Now;
                        productInvoice.ModifiedUserId = WebSecurity.CurrentUserId;
                        productInvoiceRepository.UpdateProductInvoice(productInvoice);

                        //Lấy mã KH
                        var customer = customerRepository.GetCustomerById(productInvoice.CustomerId.Value);

                        var remain = productInvoice.TotalAmount - Convert.ToDecimal(model.ReceiptViewModel.Amount.Value);
                        if (remain > 0)
                        {
                        }
                        else
                        {
                            productInvoice.NextPaymentDate = null;
                            model.NextPaymentDate_Temp = null;
                        }
                        #endregion

                        #region thêm lịch sử bán hàng
                        //Ghi Nợ TK 131 - Phải thu của khách hàng (tổng giá thanh toán)
                        Erp.BackOffice.Account.Controllers.TransactionLiabilitiesController.Create(
                                productInvoice.Code,
                                "ProductInvoice",
                                "Bán hàng",
                                customer.Code,
                                "Customer",
                                productInvoice.TotalAmount,
                                0,
                                productInvoice.Code,
                                "ProductInvoice",
                                null,
                                model.NextPaymentDate_Temp,
                                null);
                        #endregion

                        #region phiếu thu
                        //Khách thanh toán ngay
                        if (model.ReceiptViewModel.Amount > 0)
                        {
                            #region xóa phiếu thu cũ (nếu có)
                            var receipt = ReceiptRepository.GetAllReceiptFull()
                                .Where(item => item.MaChungTuGoc == productInvoice.Code).ToList();
                            var receipt_detail = receiptDetailRepository.GetAllReceiptDetailFull().ToList();
                            receipt_detail = receipt_detail.Where(x => x.MaChungTuGoc == productInvoice.Code).ToList();
                            if (receipt_detail.Count() > 0)
                            {
                                // isDelete chi tiết phiếu thu
                                for (int i = 0; i < receipt_detail.Count(); i++)
                                {
                                    receiptDetailRepository.DeleteReceiptDetail(receipt_detail[i].Id);
                                }
                            }
                            #endregion
                            if (receipt.Count() > 0)
                            {
                                #region cập nhật phiếu thu cũ
                                // isDelete phiếu thu
                                var receipts = receipt.FirstOrDefault();
                                receipts.IsDeleted = false;
                                receipts.Payer = customer.LastName + " " + customer.FirstName;
                                receipts.PaymentMethod = productInvoice.PaymentMethod;
                                receipts.ModifiedDate = DateTime.Now;
                                receipts.VoucherDate = DateTime.Now;
                                receipts.Amount = model.ReceiptViewModel.Amount;
                                if (receipts.Amount > productInvoice.TotalAmount)
                                    receipts.Amount = productInvoice.TotalAmount;

                                ReceiptRepository.UpdateReceipt(receipts);

                                //Thêm vào quản lý chứng từ
                                //TransactionController.Create(new TransactionViewModel
                                //{
                                //    TransactionModule = "Receipt",
                                //    TransactionCode = receipts.Code,
                                //    TransactionName = "Thu tiền khách hàng"
                                //});

                                //Thêm chứng từ liên quan
                                //TransactionController.CreateRelationship(new TransactionRelationshipViewModel
                                //{
                                //    TransactionA = receipts.Code,
                                //    TransactionB = productInvoice.Code
                                //});

                                //Ghi Có TK 131 - Phải thu của khách hàng.
                                Erp.BackOffice.Account.Controllers.TransactionLiabilitiesController.Create(
                                    receipts.Code,
                                    "Receipt",
                                    "Thu tiền khách hàng",
                                    customer.Code,
                                    "Customer",
                                    0,
                                    Convert.ToDecimal(model.ReceiptViewModel.Amount),
                                    productInvoice.Code,
                                    "ProductInvoice",
                                    model.ReceiptViewModel.PaymentMethod,
                                    null,
                                    null);
                                #endregion
                            }
                            else
                            {
                                #region thêm mới phiếu thu
                                //Lập phiếu thu
                                var receipt_inser = new Receipt();
                                AutoMapper.Mapper.Map(model.ReceiptViewModel, receipt_inser);
                                receipt_inser.IsDeleted = false;
                                receipt_inser.CreatedUserId = WebSecurity.CurrentUserId;
                                receipt_inser.ModifiedUserId = WebSecurity.CurrentUserId;
                                receipt_inser.AssignedUserId = WebSecurity.CurrentUserId;
                                receipt_inser.CreatedDate = DateTime.Now;
                                receipt_inser.ModifiedDate = DateTime.Now;
                                receipt_inser.VoucherDate = DateTime.Now;
                                receipt_inser.CustomerId = customer.Id;
                                receipt_inser.Payer = customer.LastName + " " + customer.FirstName;
                                receipt_inser.PaymentMethod = productInvoice.PaymentMethod;
                                receipt_inser.Address = customer.Address;
                                receipt_inser.MaChungTuGoc = productInvoice.Code;
                                receipt_inser.LoaiChungTuGoc = "ProductInvoice";
                                receipt_inser.Note = receipt_inser.Name;
                                //receipt_inser.BranchId = Helpers.Common.CurrentUser.BranchId.Value;

                                if (receipt_inser.Amount > productInvoice.TotalAmount)
                                    receipt_inser.Amount = productInvoice.TotalAmount;

                                ReceiptRepository.InsertReceipt(receipt_inser);

                                var prefixReceipt = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_ReceiptCustomer");
                                receipt_inser.Code = Erp.BackOffice.Helpers.Common.GetCode(prefixReceipt, receipt_inser.Id);
                                ReceiptRepository.UpdateReceipt(receipt_inser);
                                ////Thêm vào quản lý chứng từ
                                //TransactionController.Create(new TransactionViewModel
                                //{
                                //    TransactionModule = "Receipt",
                                //    TransactionCode = receipt_inser.Code,
                                //    TransactionName = "Thu tiền khách hàng"
                                //});

                                //Thêm chứng từ liên quan
                                //TransactionController.CreateRelationship(new TransactionRelationshipViewModel
                                //{
                                //    TransactionA = receipt_inser.Code,
                                //    TransactionB = productInvoice.Code
                                //});

                                //Ghi Có TK 131 - Phải thu của khách hàng.
                                Erp.BackOffice.Account.Controllers.TransactionLiabilitiesController.Create(
                                    receipt_inser.Code,
                                    "Receipt",
                                    "Thu tiền khách hàng",
                                    customer.Code,
                                    "Customer",
                                    0,
                                    Convert.ToDecimal(model.ReceiptViewModel.Amount),
                                    productInvoice.Code,
                                    "ProductInvoice",
                                    model.ReceiptViewModel.PaymentMethod,
                                    null,
                                    null);

                                #endregion
                            }
                        }
                        #endregion

                        #region cập nhật đơn bán hàng
                        //Cập nhật đơn hàng
                        productInvoice.ModifiedUserId = WebSecurity.CurrentUserId;
                        productInvoice.ModifiedDate = DateTime.Now;
                        productInvoice.IsArchive = true;
                        productInvoice.Status = Wording.OrderStatus_complete;
                        productInvoiceRepository.UpdateProductInvoice(productInvoice);
                        TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.ArchiveSuccess;

                        TempData[Globals.SuccessMessageKey] += "<br/>Đơn hàng này đã được xuất kho! Vui lòng kiểm tra lại chứng từ xuất kho để tránh sai xót dữ liệu!";
                        #endregion
                        //cap nhat chiet khau nha thuoc
                        Erp.BackOffice.Sale.Controllers.TotalDiscountMoneyNTController.SyncTotalDisCountMoneyNT(productInvoice,WebSecurity.CurrentUserId);
                        //cap nhat hoa hong nhan vien
                        Erp.BackOffice.Staff.Controllers.HistoryCommissionStaffController.SyncCommissionStaff(productInvoice, WebSecurity.CurrentUserId);

                    }
                    scope.Complete();
                }
                catch (DbUpdateException)
                {
                    return Content("Fail");
                }
            }
            return RedirectToAction("Detail", new { Id = model.Id });
        }
        #endregion

        #region UpdateAll
        //   [HttpPost]
        //public ActionResult UpdateAll(string url)
        //{
        //     DateTime aDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        //     // Cộng thêm 1 tháng và trừ đi một ngày.
        //    DateTime retDateTime = aDateTime.AddDays(18);
        //    var product_invoice = productInvoiceRepository.GetAllProductInvoice().Where(x => x.IsArchive == true && x.CreatedDate >= aDateTime && x.CreatedDate <= retDateTime).ToList();
        //    foreach (var item in product_invoice)
        //    {
        //          CommisionStaffController.CreateCommission(item.Id);
        //    }   
        //    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
        //    return Redirect(url);
        //}
        #endregion


        #region CreateNT

        public ActionResult CreateNT(int? Id, int? BranchId)
        {

            ProductInvoiceViewModel model = new ProductInvoiceViewModel();
            model.DetailList = new List<ProductInvoiceDetailViewModel>();
            var user = userRepository.GetByvwUserName(Helpers.Common.CurrentUser.UserName);
            if (BranchId == null)
            {
                var _drugStore = Erp.BackOffice.Helpers.SelectListHelper.GetSelectList_DepartmentAllNew(null, null);
                if (_drugStore.Count() > 0)
                {
                    model.BranchId = Convert.ToInt32(_drugStore.FirstOrDefault().Value);
                }
            }
            else
            {
                model.BranchId = BranchId;
            }
            if (Id.HasValue && Id > 0)
            {
                var productInvoice = productInvoiceRepository.GetvwProductInvoiceById(Id.Value);
                //Kiểm tra phân quyền Chi nhánh
                if ((Filters.SecurityFilter.IsTrinhDuocVien() || Filters.SecurityFilter.IsAdminSystemManager() || Filters.SecurityFilter.IsStaffDrugStore() || Filters.SecurityFilter.IsAdminDrugStore()) && ("," + Helpers.Common.CurrentUser.DrugStore + ",").Contains("," + productInvoice.BranchId + ",") == false)
                {
                    return Content("Mẫu tin không tồn tại! Không có quyền truy cập!");
                }

                AutoMapper.Mapper.Map(productInvoice, model);

                model.DetailList = productInvoiceRepository.GetAllvwInvoiceDetailsByInvoiceId(productInvoice.Id).Select(x => new ProductInvoiceDetailViewModel
                {
                    Id = x.Id,
                    Amount = x.Amount,
                    FixedDiscountAmount = x.FixedDiscountAmount,
                    IrregularDiscountAmount = x.IrregularDiscountAmount,
                    BranchId = x.BranchId,
                    CategoryCode = x.CategoryCode,
                    ExpiryDate = x.ExpiryDate,
                    FixedDiscount = x.FixedDiscount,
                    IrregularDiscount = x.IrregularDiscount,
                    Unit = x.Unit,
                    LoCode = x.LoCode,
                    Price = x.Price,
                    ProductCode = x.ProductCode,
                    ProductGroup = x.ProductGroup,
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    Quantity = x.Quantity,
                    Type = x.Type
                }).ToList();
                //AutoMapper.Mapper.Map(detailList, model.DetailList);
            }
            else
            {
                model.Id = 0;
            }
            model.ReceiptViewModel = new ReceiptViewModel();
            model.NextPaymentDate_Temp = DateTime.Now.AddDays(30);
            model.ReceiptViewModel.Name = "Bán hàng - Thu tiền mặt";
            model.ReceiptViewModel.Amount = 0;
            var productList = Domain.Helper.SqlHelper.QuerySP<InventoryViewModel>("spSale_Get_Inventory", new { WarehouseId = "", HasQuantity = "1", ProductCode = "", ProductName = "", CategoryCode = "", ProductGroup = "", BranchId = model.BranchId, LoCode = "", ProductId = "", ExpiryDate = "" });
            productList = productList.Where(x => x.IsSale != null && x.IsSale == true);
            ViewBag.productList = productList;
            //  var saleDepartmentCode = Erp.BackOffice.Helpers.Common.GetSetting("SaleDepartmentCode");
            string image_folder_product = Helpers.Common.GetSetting("product-image-folder");

            //Thêm số lượng tồn kho cho chi tiết đơn hàng đã được thêm
            if (model.DetailList != null && model.DetailList.Count > 0)
            {
                foreach (var item in model.DetailList)
                {
                    var product = productList.Where(i => i.ProductId == item.ProductId && i.LoCode == item.LoCode && i.ExpiryDate == item.ExpiryDate).FirstOrDefault();
                    if (product != null)
                    {
                        item.QuantityInInventory = product.Quantity;
                        item.PriceTest = product.ProductPriceOutbound;
                    }
                    else
                    {
                        item.Id = 0;
                    }
                }

                model.DetailList.RemoveAll(x => x.Id == 0);

                int n = 0;
                foreach (var item in model.DetailList)
                {
                    item.OrderNo = n;
                    n++;
                }
            }

            model.CreatedUserName = Erp.BackOffice.Helpers.Common.CurrentUser.FullName;

            int taxfee = 0;
            int.TryParse(Helpers.Common.GetSetting("vat"), out taxfee);
            model.TaxFee = taxfee;

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateNT(ProductInvoiceViewModel model)
        {

              var json = new JavaScriptSerializer().Serialize(model);
            var check = Request["group_choice"];
            if (ModelState.IsValid && model.DetailList.Count != 0)
            {
                ProductInvoice productInvoice = null;
                //    string output = JsonConvert.SerializeObject(model);

                if (model.Id > 0)
                {
                    productInvoice = productInvoiceRepository.GetProductInvoiceById(model.Id);
                    //Kiểm tra phân quyền Chi nhánh
                    if ((Filters.SecurityFilter.IsTrinhDuocVien() || Filters.SecurityFilter.IsAdminSystemManager() || Filters.SecurityFilter.IsStaffDrugStore() || Filters.SecurityFilter.IsAdminDrugStore()) && ("," + Helpers.Common.CurrentUser.DrugStore + ",").Contains("," + productInvoice.BranchId + ",") == false)
                    {
                        return Content("Mẫu tin không tồn tại! Không có quyền truy cập!");
                    }
                }
                using (var scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        if (productInvoice != null)
                        {
                            //Nếu đã ghi sổ rồi thì không được sửa
                            if (productInvoice.IsArchive)
                            {
                                return RedirectToAction("Detail", new { Id = productInvoice.Id });
                            }
                            productInvoice.Type = check;
                            AutoMapper.Mapper.Map(model, productInvoice);
                        }
                        else
                        {
                            productInvoice = new ProductInvoice();
                            AutoMapper.Mapper.Map(model, productInvoice);
                            productInvoice.IsDeleted = false;
                            productInvoice.CreatedUserId = WebSecurity.CurrentUserId;
                            productInvoice.CreatedDate = DateTime.Now;
                            productInvoice.Status = Wording.OrderStatus_pending;
                            productInvoice.BranchId = model.BranchId.Value;
                            productInvoice.IsArchive = false;
                            productInvoice.IsReturn = false;
                            productInvoice.Type = check;
                            productInvoice.RemainingAmount = 0;
                            productInvoice.PaidAmount = model.TotalAmount;
                            productInvoice.PaymentMethod = SelectListHelper.GetSelectList_Category("FormPayment", null, "Name", null).FirstOrDefault().Value;
                        }

                        #region kiểm tra thông tin khách hàng
                        if (check == "Customer")
                        {
                            var customer = customerRepository.GetCustomerByPhone(model.CustomerPhone);
                            if (customer == null)
                            {
                                //chưa có khách hàng thì tạo mới
                                var insert_cus = new Customer();
                                insert_cus.IsDeleted = false;
                                insert_cus.CreatedUserId = WebSecurity.CurrentUserId;
                                insert_cus.CreatedDate = DateTime.Now;
                                insert_cus.ModifiedUserId = WebSecurity.CurrentUserId;
                                insert_cus.ModifiedDate = DateTime.Now;
                                insert_cus.Phone = model.CustomerPhone;
                                insert_cus.CompanyName = model.CustomerName;
                                insert_cus.BranchId = productInvoice.BranchId;
                                insert_cus.CustomerType = "Customer";
                                customerRepository.InsertCustomer(insert_cus);
                                //tạo mã khách hàng tự động
                                var prefix1 = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_Customer");
                                insert_cus.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix1, insert_cus.Id);
                                //cập nhật mã khách hàng
                                customerRepository.UpdateCustomer(insert_cus);
                                //cập nhật lại id customer cho đơn hàng.. để sau này tính công nợ, báo cáo
                                productInvoice.CustomerId = insert_cus.Id;
                            }
                            else
                            {
                                productInvoice.CustomerId = customer.Id;
                            }
                        }
                        else
                        {
                            //lấy id khách vãng lai mặc định trong customer.. để sau này tiện thống kê
                            var customer_guest = customerRepository.GetCustomerByGuestsCustomer(model.BranchId);
                            if (customer_guest != null)
                            {
                                productInvoice.CustomerId = customer_guest.Id;
                                productInvoice.CustomerName = customer_guest.CompanyName;
                            }
                            else
                            {
                                var cus = new Customer();
                                cus.IsDeleted = false;
                                cus.CreatedUserId = WebSecurity.CurrentUserId;
                                cus.ModifiedUserId = WebSecurity.CurrentUserId;
                                cus.CreatedDate = DateTime.Now;
                                cus.ModifiedDate = DateTime.Now;
                                cus.CompanyName = "Khách vãng lai";
                                cus.BranchId = model.BranchId;
                                cus.Phone = model.CustomerPhone;
                                cus.CustomerType = "Guests";
                                //lấy mã.
                                customerRepository.InsertCustomer(cus);
                                var prefix1 = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_Customer");
                                cus.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix1, cus.Id);
                                customerRepository.UpdateCustomer(cus);
                                productInvoice.CustomerId = cus.Id;
                                productInvoice.CustomerName = cus.CompanyName;
                            }
                        }
                        #endregion
                        //Duyệt qua danh sách sản phẩm mới xử lý tình huống user chọn 2 sản phầm cùng id
                        List<ProductInvoiceDetail> listNewCheckSameId = new List<ProductInvoiceDetail>();
                        model.DetailList.RemoveAll(x => x.Quantity <= 0);
                        foreach (var group in model.DetailList)
                        {
                            var product = ProductRepository.GetProductById(group.ProductId.Value);
                            listNewCheckSameId.Add(new ProductInvoiceDetail
                            {
                                ProductId = product.Id,
                                ProductType = product.Type,
                                Quantity = group.Quantity,
                                Unit = product.Unit,
                                Price = group.Price,
                                PromotionDetailId = group.PromotionDetailId,
                                PromotionId = group.PromotionId,
                                PromotionValue = group.PromotionValue,
                                IsDeleted = false,
                                CreatedUserId = WebSecurity.CurrentUserId,
                                ModifiedUserId = WebSecurity.CurrentUserId,
                                CreatedDate = DateTime.Now,
                                ModifiedDate = DateTime.Now,
                                FixedDiscount = group.FixedDiscount,
                                FixedDiscountAmount = group.FixedDiscountAmount,
                                IrregularDiscount = group.IrregularDiscount,
                                IrregularDiscountAmount = group.IrregularDiscountAmount,
                                QuantitySaleReturn = group.Quantity,
                                CheckPromotion = (group.CheckPromotion.HasValue ? group.CheckPromotion.Value : false),
                                IsReturn = false,
                                LoCode = group.LoCode,
                                ExpiryDate = group.ExpiryDate
                                //StaffId = group.StaffId,
                                //Status = group.Status

                            });
                        }
                        var commision = commisionCustomerRepository.GetAllCommisionCustomer().ToList();
                        commision = commision.Where(item => ("," + item.ApplyFor + ",").Contains("," + model.BranchId + ",") == true).ToList();
                        var setting_hide = Erp.BackOffice.Helpers.Common.GetSetting("hide_discount_product_invoice");
                        var setting_readonly = Erp.BackOffice.Helpers.Common.GetSetting("readonly_discount_product_invoice");
                        var user_type_onl = Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeCode;
                        //Tính lại chiết khấu
                        foreach (var item in listNewCheckSameId)
                        {
                            var thanh_tien = item.Quantity * item.Price;
                            if (("," + setting_hide + ",").Contains("," + user_type_onl + ",") == false)
                            {
                                item.FixedDiscountAmount = Convert.ToInt32(item.FixedDiscount * thanh_tien / 100);
                                item.IrregularDiscountAmount = Convert.ToInt32(item.IrregularDiscount * thanh_tien / 100);
                            }
                            else
                            {
                                var FixedDiscount = commision.Where(x => x.ProductId == item.ProductId && x.Type == "FixedDiscount").ToList();
                                if (FixedDiscount.Count() <= 0)
                                {
                                    item.FixedDiscount = 0;  //%giảm giá
                                    item.FixedDiscountAmount = 0; // số tiền giảm
                                }
                                else
                                {
                                    var item_FixedDiscount = FixedDiscount.FirstOrDefault();
                                    if (item_FixedDiscount.IsMoney == true) //nếu CommissionValue là số tiền thì tính ra % giảm giá ngược lại
                                    {
                                        item.FixedDiscountAmount = Convert.ToInt32(item_FixedDiscount.CommissionValue);
                                        item.FixedDiscount = Convert.ToInt32(item_FixedDiscount.CommissionValue / thanh_tien * 100);
                                    }
                                    else
                                    {
                                        item.FixedDiscount = Convert.ToInt32(item_FixedDiscount.CommissionValue);
                                        item.FixedDiscountAmount = Convert.ToInt32(thanh_tien * item_FixedDiscount.CommissionValue / 100);
                                    }
                                }
                                //giảm giá đột xuất
                                var IrregularDiscount = commision.Where(x => x.ProductId == item.ProductId
                                                                                && x.Type == "IrregularDiscount"
                                                                                && x.StartDate <= UrlCommon.EndOfDay(DateTime.Now) && x.EndDate >= UrlCommon.StartOfDay(DateTime.Now)).ToList();


                                if (IrregularDiscount.Count() <= 0)
                                {
                                    item.IrregularDiscount = 0;
                                    item.IrregularDiscountAmount = 0;
                                }
                                else
                                {
                                    var item_IrregularDiscount = IrregularDiscount.FirstOrDefault();
                                    if (item_IrregularDiscount.IsMoney == true)
                                    {
                                        item.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                        item.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / thanh_tien * 100);
                                    }
                                    else
                                    {
                                        item.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                        item.IrregularDiscountAmount = Convert.ToInt32(thanh_tien * item_IrregularDiscount.CommissionValue / 100);
                                    }
                                }
                            }
                        }
                        var total = listNewCheckSameId.Sum(item => item.Quantity.Value * item.Price.Value);
                        productInvoice.TotalAmount = total + (total * Convert.ToDecimal(model.TaxFee));
                        productInvoice.IrregularDiscount = Convert.ToDecimal(listNewCheckSameId.Sum(x => x.IrregularDiscountAmount));
                        productInvoice.FixedDiscount = Convert.ToDecimal(listNewCheckSameId.Sum(x => x.FixedDiscountAmount));
                        productInvoice.IsReturn = false;
                        productInvoice.ModifiedUserId = WebSecurity.CurrentUserId;
                        productInvoice.ModifiedDate = DateTime.Now;
                        productInvoice.PaidAmount = 0;
                        productInvoice.RemainingAmount = productInvoice.TotalAmount;
                        //hàm edit 
                        if (model.Id > 0)
                        {
                            productInvoiceRepository.UpdateProductInvoice(productInvoice);
                            var listDetail = productInvoiceRepository.GetAllInvoiceDetailsByInvoiceId(productInvoice.Id).ToList();

                            //xóa danh sách dữ liệu cũ dưới database gồm product invoice, productInvoiceDetail, UsingServiceLog,UsingServiceLogDetail,ServiceReminder
                            productInvoiceRepository.DeleteProductInvoiceDetail(listDetail);

                            //thêm mới toàn bộ database
                            foreach (var item in listNewCheckSameId)
                            {
                                item.ProductInvoiceId = productInvoice.Id;
                                productInvoiceRepository.InsertProductInvoiceDetail(item);
                            }

                            //Thêm vào quản lý chứng từ
                            TransactionController.Create(new TransactionViewModel
                            {
                                TransactionModule = "ProductInvoice",
                                TransactionCode = productInvoice.Code,
                                TransactionName = "Bán hàng"
                            });
                        }
                        else
                        {
                            //hàm thêm mới
                            productInvoiceRepository.InsertProductInvoice(productInvoice, listNewCheckSameId);

                            //cập nhật lại mã hóa đơn
                            string prefix = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_Invoice");
                            productInvoice.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix, productInvoice.Id);
                            productInvoiceRepository.UpdateProductInvoice(productInvoice);

                            //Thêm vào quản lý chứng từ
                            TransactionController.Create(new TransactionViewModel
                            {
                                TransactionModule = "ProductInvoice",
                                TransactionCode = productInvoice.Code,
                                TransactionName = "Bán hàng"
                            });
                        }
                        scope.Complete();
                    }
                    catch (DbUpdateException)
                    {
                        TempData[Globals.FailedMessageKey] = App_GlobalResources.Wording.Error;
                        return View(model);
                    }
                }
                return RedirectToAction("Detail", new { Id = productInvoice.Id });
            }

            return View();
        }

        #region LoadProductItemNT


        public PartialViewResult LoadProductItemNT(int? OrderNo, int? ProductId, string ProductName, string Unit, int Quantity, decimal Price, string ProductCode, string ProductType, int QuantityInventory,
            string LoCode, string ExpiryDate, string DrugStore)
        {
            var model = new ProductInvoiceDetailViewModel();
            model.OrderNo = OrderNo.Value;
            model.ProductId = ProductId;
            model.ProductName = ProductName;
            model.Unit = Unit;
            model.Quantity = Quantity;
            model.Price = Price;
            model.ProductCode = ProductCode;
            model.ProductType = ProductType;
            model.QuantityInInventory = QuantityInventory;
            model.PriceTest = Price;
            model.LoCode = LoCode;
            if (!string.IsNullOrEmpty(ExpiryDate))
                model.ExpiryDate = Convert.ToDateTime(DateTime.ParseExact(ExpiryDate, "dd/MM/yyyy", CultureInfo.InvariantCulture));
            //giảm giá cố định
            var commision = commisionCustomerRepository.GetAllCommisionCustomer().ToList();
            commision = commision.Where(item => ("," + item.ApplyFor + ",").Contains("," + DrugStore + ",") == true).ToList();
            var FixedDiscount = commision.Where(item => item.ProductId == ProductId
                                                        && item.Type == "FixedDiscount").ToList();

            if (FixedDiscount.Count() <= 0)
            {
                model.FixedDiscount = 0;  //%giảm giá
                model.FixedDiscountAmount = 0; // số tiền giảm
            }
            else
            {
                var item_FixedDiscount = FixedDiscount.FirstOrDefault();
                if (item_FixedDiscount.IsMoney == true) //nếu CommissionValue là số tiền thì tính ra % giảm giá ngược lại
                {
                    model.FixedDiscountAmount = Convert.ToInt32(item_FixedDiscount.CommissionValue);
                    model.FixedDiscount = Convert.ToInt32(item_FixedDiscount.CommissionValue / (model.Price * model.Quantity) * 100);
                }
                else
                {
                    model.FixedDiscount = Convert.ToInt32(item_FixedDiscount.CommissionValue);
                    model.FixedDiscountAmount = Convert.ToInt32((model.Price * model.Quantity) * item_FixedDiscount.CommissionValue / 100);
                }
            }
            //giảm giá đột xuất
            var IrregularDiscount = commision.Where(item => item.ProductId == ProductId
                                                            && item.Type == "IrregularDiscount"
                                                            && item.StartDate <= UrlCommon.EndOfDay(DateTime.Now) && item.EndDate >= UrlCommon.StartOfDay(DateTime.Now)).ToList();
            if (IrregularDiscount.Count() <= 0)
            {
                model.IrregularDiscount = 0;
                model.IrregularDiscountAmount = 0;
            }
            else
            {
                var item_IrregularDiscount = IrregularDiscount.FirstOrDefault();
                if (item_IrregularDiscount.IsMoney == true)
                {
                    model.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                    model.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (model.Price * model.Quantity) * 100);
                }
                else
                {
                    model.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                    model.IrregularDiscountAmount = Convert.ToInt32((model.Price * model.Quantity) * item_IrregularDiscount.CommissionValue / 100);
                }

            }
            return PartialView(model);
        }
        #endregion
        #endregion

        //public static void AutoCreateProductInvoice(IProductInvoiceRepository productInvoiceRepository, Customer cus, ProductOutbound model, List<ProductOutboundDetail> detail)
        //{
        //    var insert_detail = detail.Select(x => new ProductInvoiceDetail
        //    {
        //        CreatedUserId = WebSecurity.CurrentUserId,
        //        ModifiedUserId = WebSecurity.CurrentUserId,
        //        CreatedDate = DateTime.Now,
        //        ModifiedDate = DateTime.Now,
        //        FixedDiscountAmount = 0,
        //        IrregularDiscountAmount = 0,
        //        IrregularDiscount = 0,
        //        FixedDiscount = 0,
        //        IsDeleted = false,
        //        Price = x.Price,
        //        ProductId = x.ProductId.Value,
        //        ExpiryDate = x.ExpiryDate,
        //        LoCode = x.LoCode,
        //        Quantity = x.Quantity,
        //        Unit = x.Unit
        //    }).ToList();
        //    var insert = new Domain.Sale.Entities.ProductInvoice();
        //    //AutoMapper.Mapper.Map(model, productInvoice);
        //    insert.IsDeleted = false;
        //    insert.CreatedUserId = WebSecurity.CurrentUserId;
        //    insert.CreatedDate = DateTime.Now;
        //    insert.Status = Wording.OrderStatus_pending;
        //    insert.BranchId = model.BranchId.Value;
        //    insert.IsArchive = false;
        //    insert.IsReturn = false;
        //    insert.CustomerId = cus.Id;
        //    insert.CustomerPhone = cus.Phone;
        //    insert.CustomerPhone = cus.Phone;
        //    insert.Type = "invoiceDS";
        //    int taxfee = 0;
        //    int.TryParse(Helpers.Common.GetSetting("vat"), out taxfee);
        //    insert.TaxFee = taxfee;
        //    insert.StaffCreateId = model.CreatedStaffId;
        //    insert.Status = "pending";
        //    insert.FixedDiscount = 0;
        //    insert.IrregularDiscount = 0;
        //    insert.RemainingAmount = model.TotalAmount.Value;
        //    insert.PaidAmount = 0;
        //    productInvoiceRepository.InsertProductInvoice(insert, insert_detail);
        //    //cập nhật lại mã hóa đơn
        //    string prefix = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_Invoice");
        //    insert.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix, insert.Id);
        //    productInvoiceRepository.UpdateProductInvoice(insert);

        //    //Thêm vào quản lý chứng từ
        //    TransactionController.Create(new TransactionViewModel
        //    {
        //        TransactionModule = "ProductInvoice",
        //        TransactionCode = insert.Code,
        //        TransactionName = "Bán hàng"
        //    });
        //}

        public static int GetPointVIP()
        {
            try
            {
                SettingRepository settingRepository = new SettingRepository(new ErpDbContext());
                ProductInvoiceRepository productInvoiceRepository = new ProductInvoiceRepository(new ErpSaleDbContext());
                var setting = settingRepository.GetSettingByKey("setting_point").Value;
                if (!Erp.BackOffice.Filters.SecurityFilter.IsAdminDrugStore())
                    return 0;
                var branch = Erp.BackOffice.Helpers.Common.CurrentUser.DrugStore;
                setting = string.IsNullOrEmpty(setting) ? "0" : setting;
                var list = productInvoiceRepository.GetAllProductInvoice().AsEnumerable().Where(x => x.BranchId != null && x.IsArchive == true && x.BranchId == Convert.ToInt32(branch)).ToList().Sum(x => x.TotalAmount);
                var rf = list / Convert.ToDecimal(setting);
                string[] arrVal = rf.ToString().Split(',');
                var value = int.Parse(arrVal[0], CultureInfo.InstalledUICulture);
                return value;
            }
            catch { }

            return 0;
        }

        #region ApprovedDrugStore

        [HttpPost]
        public JsonResult ApprovedDrugStore(int? week, int? year, string branchId)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    AutoMapper.Mapper.CreateMap<vwProductInvoice, ProductInvoice>();
                    var user = userRepository.GetByvwUserName(Helpers.Common.CurrentUser.UserName);
                    var invoice_list = productInvoiceRepository.GetAllvwProductInvoice().AsEnumerable().Where(x => x.Year == year && x.WeekOfYear == week && ("," + branchId + ",").Contains("," + x.BranchId + ",") == true).ToList();
                    foreach (var item in invoice_list)
                    {
                        var update = new ProductInvoice();

                        AutoMapper.Mapper.Map(item, update);
                        update.DrugStoreUserId = WebSecurity.CurrentUserId;
                        update.DrugStoreDate = DateTime.Now;
                        productInvoiceRepository.UpdateProductInvoice(update);
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
        #region Approved Kế toán

        [HttpPost]
        public JsonResult ApprovedAccountancy(int? week, int? year, string branchId)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    AutoMapper.Mapper.CreateMap<vwProductInvoice, ProductInvoice>();
                    //var user = userRepository.GetByvwUserName(Helpers.Common.CurrentUser.UserName);
                    var invoice_list = productInvoiceRepository.GetAllvwProductInvoice().AsEnumerable()
                        .Where(x => x.Year == year && x.WeekOfYear == week
                            && ("," + branchId + ",").Contains("," + x.BranchId + ",") == true).ToList();
                    foreach (var item in invoice_list)
                    {
                        var update = new ProductInvoice();

                        AutoMapper.Mapper.Map(item, update);
                        update.AccountancyUserId = WebSecurity.CurrentUserId;
                        update.AccountancyDate = DateTime.Now;
                        productInvoiceRepository.UpdateProductInvoice(update);
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
    }
}
