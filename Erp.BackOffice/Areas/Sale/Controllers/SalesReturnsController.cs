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
using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Account.Interfaces;
using Erp.Domain.Account.Entities;
using Erp.BackOffice.Account.Controllers;
using Erp.BackOffice.Account.Models;
using System.Web;

namespace Erp.BackOffice.Sale.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class SalesReturnsController : Controller
    {
        private readonly ITransactionLiabilitiesRepository transactionRepository;
        private readonly ISalesReturnsRepository SalesReturnsRepository;
        private readonly IUserRepository userRepository;
        private readonly IProductInvoiceRepository productInvoiceRepository;
        private readonly IProductOrServiceRepository productRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly IPaymentRepository paymentRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IProductInboundRepository productInboundRepository;
        private readonly IWarehouseLocationItemRepository warehouseLocationItemRepository;
        private readonly IWarehouseRepository WarehouseRepository;
        private readonly ITemplatePrintRepository templatePrintRepository;
        public SalesReturnsController(
            ITransactionLiabilitiesRepository _transaction
            , ISalesReturnsRepository _SalesReturns
            , IUserRepository _user
            , IProductInvoiceRepository _ProductInvoice
            , IProductOrServiceRepository product
            , ICustomerRepository customer
            , IPaymentRepository payment
            , ICategoryRepository category
            , IProductInboundRepository _ProductInbound
            , IWarehouseLocationItemRepository _WarehouseLocationItem
            , IWarehouseRepository _Warehouse
            , ITemplatePrintRepository _templatePrint
            )
        {
            SalesReturnsRepository = _SalesReturns;
            userRepository = _user;
            productInvoiceRepository = _ProductInvoice;
            productRepository = product;
            customerRepository = customer;
            transactionRepository = _transaction;
            paymentRepository = payment;
            categoryRepository = category;
            productInboundRepository = _ProductInbound;
            warehouseLocationItemRepository = _WarehouseLocationItem;
            WarehouseRepository = _Warehouse;
            templatePrintRepository = _templatePrint;
        }

        #region Index

        public ViewResult Index(string txtCode, string txtMinAmount, string txtMaxAmount, string txtCusName, string startDate, string endDate)
        {
            IEnumerable<SalesReturnsViewModel> q = SalesReturnsRepository.GetAllvwSalesReturns().Where(x => ("," + Helpers.Common.CurrentUser.DrugStore + ",").Contains("," + x.BranchId + ",") == true).AsEnumerable()
                .Select(item => new SalesReturnsViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    ModifiedDate = item.ModifiedDate,
                    Code = item.Code,
                    CustomerCode = item.CustomerCode,
                    CustomerName = item.CustomerName,
                    TotalAmount = item.TotalAmount,
                    Discount = item.Discount,
                    TaxFee = item.TaxFee,
                    BranchId = item.BranchId,
                    CustomerId = item.CustomerId,
                    PaymentMethod = item.PaymentMethod,
                    Status = item.Status
                }).OrderByDescending(m => m.ModifiedDate);

            if (string.IsNullOrEmpty(txtCode) == false || string.IsNullOrEmpty(txtCusName) == false)
            {
                txtCode = txtCode == "" ? "~" : txtCode.ToLower();

                txtCusName = txtCusName == "" ? "~" : Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(txtCusName);
                q = q.Where(x => x.Code.ToLowerOrEmpty().Contains(txtCode)
                    || Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(x.CustomerName).Contains(txtCusName));
            }

            // lọc theo ngày
            DateTime d_startDate, d_endDate;
            if (DateTime.TryParseExact(startDate, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out d_startDate))
            {
                if (DateTime.TryParseExact(endDate, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out d_endDate))
                {
                    d_endDate = d_endDate.AddHours(23).AddMinutes(59);
                    q = q.Where(x => x.CreatedDate >= d_startDate && x.CreatedDate <= d_endDate);
                }
            }

            decimal minAmount;
            if (decimal.TryParse(txtMinAmount, out minAmount))
            {
                q = q.Where(x => x.TotalAmount >= minAmount);
            }

            decimal maxAmount;
            if (decimal.TryParse(txtMaxAmount, out maxAmount))
            {
                q = q.Where(x => x.TotalAmount <= maxAmount);
            }

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        #endregion

        //#region Create
        //public ActionResult Create(int? ProductInvoiceId)
        //{
        //    if (ProductInvoiceId == null || ProductInvoiceId == 0)
        //        return RedirectToAction("Index");

        //    var productinvoice = productInvoiceRepository.GetvwProductInvoiceById(ProductInvoiceId.Value);
        //    if (productinvoice != null && productinvoice.IsDeleted != true && productinvoice.IsReturn.Value == false)
        //    {
        //        int BranchId = Helpers.Common.CurrentUser.BranchId.Value;
        //        var model = new SalesReturnsViewModel();

        //        int taxfee = 0;
        //        int.TryParse(Helpers.Common.GetSetting("vat"), out taxfee);
        //        model.TaxFee = taxfee;


        //        AutoMapper.Mapper.Map(productinvoice, model);
        //        model.ProductInvoiceCode = productinvoice.Code;

        //        // lấy danh sách invoice detail
        //        var detailList = productInvoiceRepository.GetAllvwInvoiceDetailsByInvoiceId(ProductInvoiceId.Value).ToList();
        //        model.DetailList = new List<SalesReturnsDetailViewModel>();
        //        AutoMapper.Mapper.Map(detailList, model.DetailList);

        //        var warehouseList = WarehouseRepository.GetAllWarehouse().Where(x => x.BranchId == Helpers.Common.CurrentUser.BranchId).AsEnumerable()
        //           .Select(item => new SelectListItem
        //           {
        //               Text = item.Name,
        //               Value = item.Id.ToString()
        //           });
        //        ViewBag.warehouseList = warehouseList;
        //        return View(model);
        //    }

        //    return RedirectToAction("Index");
        //}

        //[HttpPost]
        //public ActionResult Create(SalesReturnsViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var productinvoice = productInvoiceRepository.GetProductInvoiceById(model.ProductInvoiceId.Value);
        //        productinvoice.IsReturn = true;
        //        productInvoiceRepository.UpdateProductInvoice(productinvoice);

        //        //Lấy danh sách chi tiết hóa đơn để chút nữa update IsReturn = 1
        //        var listProductInvoiceDetail = productInvoiceRepository.GetAllInvoiceDetailsByInvoiceId(productinvoice.Id).ToList();

        //        var salesReturns = new Domain.Sale.Entities.SalesReturns();
        //        AutoMapper.Mapper.Map(model, salesReturns);
        //        salesReturns.IsDeleted = false;
        //        salesReturns.CreatedUserId = WebSecurity.CurrentUserId;
        //        salesReturns.ModifiedUserId = WebSecurity.CurrentUserId;
        //        salesReturns.CreatedDate = DateTime.Now;
        //        salesReturns.ModifiedDate = DateTime.Now;
        //        salesReturns.Status = Wording.OrderStatus_pending;

        //        var listSalesReturnsDetail = new List<SalesReturnsDetail>();
        //        AutoMapper.Mapper.Map(model.DetailList, listSalesReturnsDetail);
        //        foreach (var item in listSalesReturnsDetail)
        //        {
        //            item.IsDeleted = false;
        //            item.CreatedUserId = WebSecurity.CurrentUserId;
        //            item.ModifiedUserId = WebSecurity.CurrentUserId;
        //            item.CreatedDate = DateTime.Now;
        //            item.ModifiedDate = DateTime.Now;

        //            var productInvoiceDetail = listProductInvoiceDetail.Where(i => i.ProductId == item.ProductId).FirstOrDefault();
        //            if (productInvoiceDetail != null)
        //            {
        //                productInvoiceDetail.IsReturn = true;
        //                productInvoiceRepository.UpdateProductInvoiceDetail(productInvoiceDetail);
        //            }
        //        }

        //        int returnsId = SalesReturnsRepository.InsertSalesReturns(salesReturns, listSalesReturnsDetail);

        //        string prefix = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_SalesReturns");
        //        salesReturns.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix, salesReturns.Id);
        //        SalesReturnsRepository.UpdateSalesReturns(salesReturns);

        //        //Thêm vào quản lý chứng từ
        //        TransactionController.Create(new TransactionViewModel
        //        {
        //            TransactionModule = "SalesReturns",
        //            TransactionCode = salesReturns.Code,
        //            TransactionName = "Hàng bán trả lại"
        //        });

        //        //Thêm chứng từ liên quan
        //        TransactionController.CreateRelationship(new TransactionRelationshipViewModel
        //        {
        //            TransactionA = salesReturns.Code,
        //            TransactionB = productinvoice.Code
        //        });

        //        //Lấy mã KH
        //        var customer = customerRepository.GetCustomerById(model.CustomerId.Value);

        //        if (model.PaymentMethod == "Trả lại tiền mặt")
        //        {
        //            //Nếu chọn là "Trả lại tiền mặt" thì tạo chứng từ chi
        //            var payment = new Payment();
        //            AutoMapper.Mapper.Map(model.paymentViewModel, payment);
        //            payment.IsDeleted = false;
        //            payment.CreatedUserId = WebSecurity.CurrentUserId;
        //            payment.ModifiedUserId = WebSecurity.CurrentUserId;
        //            payment.AssignedUserId = WebSecurity.CurrentUserId;
        //            payment.CreatedDate = DateTime.Now;
        //            payment.ModifiedDate = DateTime.Now;
        //            payment.PaymentMethod = "Tiền mặt";
        //            payment.Amount = Convert.ToDouble(model.TotalAmount);
        //            payment.Name = "Hàng bán trả lại - Chi tiền mặt";

        //            paymentRepository.InsertPayment(payment);

        //            var prefixPayment = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_SalesReturnsAndPayment");
        //            payment.Code = Erp.BackOffice.Helpers.Common.GetCode(prefixPayment, payment.Id);
        //            paymentRepository.UpdatePayment(payment);

        //            //Thêm vào quản lý chứng từ
        //            TransactionController.Create(new TransactionViewModel
        //            {
        //                TransactionModule = "Payment",
        //                TransactionCode = payment.Code,
        //                TransactionName = "Hàng bán trả lại - Chi tiền mặt"
        //            });

        //            //Thêm chứng từ liên quan
        //            TransactionController.CreateRelationship(new TransactionRelationshipViewModel
        //            {
        //                TransactionA = payment.Code,
        //                TransactionB = salesReturns.Code
        //            });
        //        }
        //        else
        //        {
        //            //Ghi Có TK 131 - Phải thu của khách hàng.
        //            Erp.BackOffice.Account.Controllers.TransactionLiabilitiesController.Create(
        //                salesReturns.Code,
        //                "SalesReturns",
        //                "Hàng bán trả lại - Bù trừ công nợ",
        //                customer.Code,
        //                "Customer",
        //                0,
        //                model.TotalAmount.Value,
        //                 productinvoice.Code,
        //                "ProductInvoice",
        //                null,
        //                null,
        //                null);
        //        }

        //        //Tạo phiếu nhập kho
        //        var productInbound = new Domain.Sale.Entities.ProductInbound();
        //        productInbound.IsDeleted = false;
        //        productInbound.CreatedUserId = WebSecurity.CurrentUserId;
        //        productInbound.ModifiedUserId = WebSecurity.CurrentUserId;
        //        productInbound.CreatedDate = DateTime.Now;
        //        productInbound.ModifiedDate = DateTime.Now;
        //        productInbound.BranchId = Helpers.Common.CurrentUser.BranchId;
        //        productInbound.Type = "SalesReturns";
        //        productInbound.TotalAmount = listSalesReturnsDetail.Sum(item => (item.Price * item.Quantity));
        //        productInbound.WarehouseDestinationId = model.WarehouseDestinationId;
        //        productInbound.Note = string.Format("Hàng bán trả lại từ đơn hàng {0}", productinvoice.Code);
        //        productInbound.SalesReturnsId = salesReturns.Id;

        //        //Thêm mới phiếu nhập và chi tiết phiếu nhập
        //        productInboundRepository.InsertProductInbound(productInbound);

        //        //Cập nhật lại mã nhập kho
        //        string prefixProductInbound = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_Inbound");
        //        productInbound.Code = Erp.BackOffice.Helpers.Common.GetCode(prefixProductInbound, productInbound.Id);
        //        productInboundRepository.UpdateProductInbound(productInbound);

        //        //Thêm vào quản lý chứng từ
        //        TransactionController.Create(new TransactionViewModel
        //        {
        //            TransactionModule = "ProductInbound",
        //            TransactionCode = productInbound.Code,
        //            TransactionName = "Nhập kho"
        //        });

        //        //Thêm chứng từ liên quan
        //        TransactionController.CreateRelationship(new TransactionRelationshipViewModel
        //        {
        //            TransactionA = productInbound.Code,
        //            TransactionB = salesReturns.Code
        //        });

        //        //Thêm chi tiết phiếu nhập
        //        foreach (var item in listSalesReturnsDetail)
        //        {
        //            var productInboundDetail = new ProductInboundDetail();
        //            productInboundDetail.IsDeleted = false;
        //            productInboundDetail.CreatedUserId = WebSecurity.CurrentUserId;
        //            productInboundDetail.ModifiedUserId = WebSecurity.CurrentUserId;
        //            productInboundDetail.CreatedDate = DateTime.Now;
        //            productInboundDetail.ModifiedDate = DateTime.Now;
        //            productInboundDetail.ProductInboundId = productInbound.Id;
        //            productInboundDetail.ProductId = item.ProductId;
        //            productInboundDetail.Quantity = item.Quantity;
        //            productInboundDetail.Price = item.Price;
        //            productInboundRepository.InsertProductInboundDetail(productInboundDetail);

        //            //Cập nhật vị trí sản phẩm thêm vào kho cho từng sản phẩm riêng biệt
        //            for (int i = 1; i <= productInboundDetail.Quantity; i++)
        //            {
        //                var warehouseLocationItem = new WarehouseLocationItem();
        //                warehouseLocationItem.IsDeleted = false;
        //                warehouseLocationItem.CreatedUserId = WebSecurity.CurrentUserId;
        //                warehouseLocationItem.ModifiedUserId = WebSecurity.CurrentUserId;
        //                warehouseLocationItem.CreatedDate = DateTime.Now;
        //                warehouseLocationItem.ModifiedDate = DateTime.Now;
        //                warehouseLocationItem.ProductId = productInboundDetail.ProductId;
        //                warehouseLocationItem.ProductInboundId = productInboundDetail.ProductInboundId;
        //                warehouseLocationItem.ProductInboundDetailId = productInboundDetail.Id;
        //                warehouseLocationItem.IsOut = false;
        //                warehouseLocationItem.Shelf = "1";
        //                warehouseLocationItem.Floor = "1";
        //                warehouseLocationItem.WarehouseId = productInbound.WarehouseDestinationId;
        //                warehouseLocationItemRepository.InsertWarehouseLocationItem(warehouseLocationItem);
        //            }
        //        }

        //        TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
        //        return RedirectToAction("Detail", new { Id = salesReturns.Id });
        //    }

        //    return RedirectToAction("Create", new { ProductinvoiceId = model.ProductInvoiceId });
        //}

        //#endregion

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
                    var item = SalesReturnsRepository.GetSalesReturnsById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        //if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        //{
                        //    TempData["FailedMessage"] = "NotOwner";
                        //    return RedirectToAction("Index");
                        //}

                        item.IsDeleted = true;
                        SalesReturnsRepository.UpdateSalesReturns(item);
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

        #region Detail

        public ViewResult DetailChart(int? month, int? year)
        {
            IEnumerable<SalesReturnsViewModel> q = SalesReturnsRepository.GetAllvwSalesReturns().Where(x => ("," + Helpers.Common.CurrentUser.DrugStore + ",").Contains("," + x.BranchId + ",") == true).AsEnumerable()
                .Select(item => new SalesReturnsViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    ModifiedDate = item.ModifiedDate,
                    Code = item.Code,
                    CustomerCode = item.CustomerCode,
                    CustomerName = item.CustomerName,
                    TotalAmount = item.TotalAmount,
                    Discount = item.Discount,
                    TaxFee = item.TaxFee,
                    BranchId = item.BranchId,
                    CustomerId = item.CustomerId,
                    PaymentMethod = item.PaymentMethod,
                    Status = item.Status
                }).OrderByDescending(m => m.ModifiedDate);

           
            if (month!=null)
            {
                q = q.Where(x => x.CreatedDate.Value.Month == month);
            }

            if (year!=null)
            {
                q = q.Where(x => x.CreatedDate.Value.Year == year);
            }

            return View(q);
        }

        public ActionResult Detail(int? Id, string TransactionCode)
        {

            var saleReturn = new vwSalesReturns();
            if (Id != null)
                saleReturn = SalesReturnsRepository.GetvwSalesReturnsById(Id.Value);
            if (!string.IsNullOrEmpty(TransactionCode))
                saleReturn = SalesReturnsRepository.GetvwSalesReturnsByTransactionCode(TransactionCode);
            if (saleReturn != null && saleReturn.IsDeleted != true)
            {
                var model = new SalesReturnsViewModel();
                AutoMapper.Mapper.Map(saleReturn, model);

                //Lấy danh sách invoice detail
                var detailList = SalesReturnsRepository.GetAllReturnsDetailsByReturnId(saleReturn.Id).ToList();
                model.DetailList = new List<SalesReturnsDetailViewModel>();
                AutoMapper.Mapper.Map(detailList, model.DetailList);

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
                        var ProductGroupName = categoryRepository.GetCategoryByCode("ProductGroup").Where(x => x.Value == item.ProductGroup).FirstOrDefault();
                        item.ProductGroupName = ProductGroupName.Name;
                    }
                }

                //Lấy thông tin phiếu nhập kho
                var productInbound = productInboundRepository.GetAllvwProductInbound()
                        .Where(item => item.SalesReturnsId == saleReturn.Id).FirstOrDefault();
                if (productInbound != null)
                {
                    model.ProductInboundViewModel = new ProductInboundViewModel();
                    AutoMapper.Mapper.Map(productInbound, model.ProductInboundViewModel);
                }

                ViewBag.SuccessMessage = TempData["SuccessMessage"];
                ViewBag.FailedMessage = TempData["FailedMessage"];
                ViewBag.AlertMessage = TempData["AlertMessage"];

                return View(model);
            }

            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }
        #endregion

        public PartialViewResult LoadProductItem(int OrderNo, int ProductId, string ProductName, string Unit, int Quantity, decimal? Price, string ProductCode, string ProductType, int QuantityInventory)
        {
            var model = new SalesReturnsDetailViewModel();
            model.OrderNo = OrderNo;
            model.ProductId = ProductId;
            model.ProductName = ProductName;
           // model.Unit = Unit;
            model.Quantity = Quantity;
            model.Price = Price;
            model.ProductCode = ProductCode;
           // model.ProductType = ProductType;
            model.QuantityInInventory = QuantityInventory;
            model.DisCount = 0;
            model.DisCountAmount = 0;
         //   model.PriceTest = Price;
            return PartialView(model);
        }

        #region Create

        public ActionResult Create(int? Id,int? CustomerId)
        {
            SalesReturnsViewModel model = new SalesReturnsViewModel();
            model.DetailList = new List<SalesReturnsDetailViewModel>();

            var saleDepartmentCode = Erp.BackOffice.Helpers.Common.GetSetting("SaleDepartmentCode");
         
                  //Thêm số lượng tồn kho cho chi tiết đơn hàng đã được thêm
            //if (model.DetailList != null && model.DetailList.Count > 0)
            //{
            //    foreach (var item in model.DetailList)
            //    {
            //        var product = productList.Where(i => i.Id == item.ProductId).FirstOrDefault();
            //        if (product != null)
            //        {
            //         //   item.QuantityInInventory = product.;
            //            item.ProductCode = product.Code;
            //        }
            //        else
            //        {
            //            item.Id = 0;
            //        }
            //    }

            //    model.DetailList.RemoveAll(x => x.Id == 0);

            //    int n = 0;
            //    foreach (var item in model.DetailList)
            //    {
            //        item.OrderNo = n;
            //        n++;
            //    }
            //}

            //Danh sách nhân viên sale
            var SaleList = userRepository.GetUserbyUserType("Sales Excutive").AsEnumerable().Select(x => new SelectListItem
            {
                Text = x.FullName,
                Value = x.Id + ""
            });
            ViewBag.SaleList = SaleList;

            //Danh sách khách hàng
            var customerList = customerRepository.GetAllCustomer().Where(x => ("," + Helpers.Common.CurrentUser.DrugStore + ",").Contains("," + x.BranchId + ",") == true)
               .Select(item => new CustomerViewModel
               {
                   Id = item.Id,
                   Code = item.Code,
                   CompanyName = item.CompanyName
               });
            ViewBag.customerList = customerList;
            var warehouseList = WarehouseRepository.GetAllWarehouse().Where(x => ("," + Helpers.Common.CurrentUser.DrugStore + ",").Contains("," + x.BranchId + ",") == true).AsEnumerable()
                  .Select(item => new SelectListItem
                  {
                      Text = item.Name,
                      Value = item.Id.ToString()
                  });
            ViewBag.warehouseList = warehouseList;
            model.CreatedUserName = Erp.BackOffice.Helpers.Common.CurrentUser.FullName;
            //  model.ReceiptViewModel = new ReceiptViewModel();
            return View(model);
        }


        [HttpPost]
        public ActionResult Create(SalesReturnsViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.DetailList.Count > 0)
                {
                    //Lấy mã KH để lát thanh toán công nợ
                    var customer = customerRepository.GetCustomerById(model.CustomerId.Value);

                    #region Tạo phiếu hàng bán trả lại
                    var salesReturns = new Domain.Sale.Entities.SalesReturns();
                    AutoMapper.Mapper.Map(model, salesReturns);
                    salesReturns.IsDeleted = false;
                    salesReturns.CreatedUserId = WebSecurity.CurrentUserId;
                    salesReturns.ModifiedUserId = WebSecurity.CurrentUserId;
                    salesReturns.CreatedDate = DateTime.Now;
                    salesReturns.ModifiedDate = DateTime.Now;
                    salesReturns.Status = Wording.OrderStatus_pending;
                    //salesReturns.BranchId = Helpers.Common.CurrentUser.BranchId;
                    //thêm 
                    SalesReturnsRepository.InsertSalesReturn(salesReturns);
                    //cập nhật mã hóa đơn hàng bán trả lại
                    string prefix = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_SalesReturns");
                    salesReturns.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix, salesReturns.Id);
                    SalesReturnsRepository.UpdateSalesReturns(salesReturns);
                    #endregion

                    #region Tạo phiếu nhập kho và chứng từ nhập kho
                    //Tạo phiếu nhập kho để khi thê chi tiết hàng bán trả lại thì nhập kho chung 1 lần luôn.
                    var productInbound = new Domain.Sale.Entities.ProductInbound();
                    productInbound.IsDeleted = false;
                    productInbound.CreatedUserId = WebSecurity.CurrentUserId;
                    productInbound.ModifiedUserId = WebSecurity.CurrentUserId;
                    productInbound.CreatedDate = DateTime.Now;
                    productInbound.ModifiedDate = DateTime.Now;
                    //productInbound.BranchId = Helpers.Common.CurrentUser.BranchId;
                    productInbound.Type = "SalesReturns";
                    productInbound.TotalAmount = 0;
                    productInbound.WarehouseDestinationId = model.WarehouseDestinationId;
                    productInbound.Note = string.Format("Hàng bán trả lại");
                    productInbound.SalesReturnsId = salesReturns.Id;

                    //Thêm mới phiếu nhập 
                    productInboundRepository.InsertProductInbound(productInbound);

                    //Cập nhật lại mã nhập kho
                    string prefixProductInbound = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_Inbound");
                    productInbound.Code = Erp.BackOffice.Helpers.Common.GetCode(prefixProductInbound, productInbound.Id);
                    productInboundRepository.UpdateProductInbound(productInbound);

                    //Thêm vào quản lý chứng từ
                    TransactionController.Create(new TransactionViewModel
                    {
                        TransactionModule = "ProductInbound",
                        TransactionCode = productInbound.Code,
                        TransactionName = "Nhập kho"
                    });

                    //Thêm chứng từ liên quan
                    TransactionController.CreateRelationship(new TransactionRelationshipViewModel
                    {
                        TransactionA = productInbound.Code,
                        TransactionB = salesReturns.Code
                    });
                    #endregion

                    #region lưu chi tiết phiếu nhập kho và chi tiết hàng bán trả lại

                    //duyệt danh sách hàng bán trả lại lưu vào database
                    var listSalesReturnsDetail = new List<SalesReturnsDetail>();
                    AutoMapper.Mapper.Map(model.DetailList, listSalesReturnsDetail);
                    //list dùng để lưu mã hóa đơn
                    List<string> list = new List<string>();
                    //list dùng để lưu số tiền thanh toán của 1 hóa đơn
                    List<ProductInvoiceDetailViewModel> listinvoice = new List<ProductInvoiceDetailViewModel>();
                    //list dùng để lưu phiếu nhập
                    List<ProductInboundDetail> list_inbound = new List<ProductInboundDetail>();
                    foreach (var item in listSalesReturnsDetail)
                    {

                        //lấy danh sách tất cả hàng hóa của khách hàng đã mua sắp xếp theo thứ tự ngày mua gần nhất đến xa nhất.
                        //duyệt danh sách sản phẩm của khách hàng đến khi nào đáp ứng đủ số lượng của số lượng hàng bán trả lại thì dừng.
                        var listProductInvoiceDetail = productInvoiceRepository.GetAllvwInvoiceDetails().Where(x => x.ProductId == item.ProductId && x.CustomerId == salesReturns.CustomerId && x.IsArchive == true).OrderByDescending(x => x.ProductInvoiceDate).ToList();
                        if (listProductInvoiceDetail.Count() > 0)
                        {
                            int quantityInvoice = 0;
                            foreach (var ii in listProductInvoiceDetail)
                            {

                                quantityInvoice += ii.QuantitySaleReturn.Value;
                                var sl_con_thieu = 0;
                                //cộng dồn hóa đơn cho tới khi lớn hơn hàng trả lại
                                if (quantityInvoice > item.Quantity)
                                {
                                    //sl con thieu la sl cần phải trừ trong hóa đơn
                                    sl_con_thieu = item.Quantity.Value - (quantityInvoice - ii.QuantitySaleReturn.Value);
                                    //sau khi trừ đi số lượng hàng trả lại thì sl_du_sau_khi_tra_hang_trong_hoa_don là số dư của hóa đơn bán hàng sau khi tính.
                                    var sl_du_sau_khi_tra_hang_trong_hoa_don = ii.QuantitySaleReturn - sl_con_thieu;
                                    if (sl_con_thieu > 0)
                                    {
                                        // tao danh sách hàng bán trả lại và danh sách hàng nhập vào kho
                                        //quản lý chứng từ và chứng từ liên quan
                                        CreateDetailReturnAndDetailInbound(ii.ProductId.Value
                                            , item.DisCount
                                            , sl_con_thieu
                                            , sl_du_sau_khi_tra_hang_trong_hoa_don
                                            , ii.Price.Value
                                            , ii.ProductInvoiceId.Value
                                            , ii.Id
                                            , salesReturns.Id
                                            , productInbound.Id
                                            , salesReturns.Code
                                            , ii.ProductInvoiceCode
                                            , productInbound.WarehouseDestinationId
                                            , model.PaymentMethod
                                            , customer.Code
                                            , list
                                            , listinvoice
                                            , list_inbound);
                                    }
                                    break;
                                }
                                else
                                {
                                    sl_con_thieu = ii.QuantitySaleReturn.Value;
                                    if (sl_con_thieu > 0)
                                    {
                                        // tao danh sách hàng bán trả lại và danh sách hàng nhập vào kho
                                        //quản lý chứng từ và chứng từ liên quan
                                        CreateDetailReturnAndDetailInbound(ii.ProductId.Value
                                            , item.DisCount
                                            , sl_con_thieu
                                            , 0
                                            , ii.Price.Value
                                            , ii.ProductInvoiceId.Value
                                            , ii.Id
                                            , salesReturns.Id
                                            , productInbound.Id
                                            , salesReturns.Code
                                            , ii.ProductInvoiceCode
                                            , productInbound.WarehouseDestinationId
                                            , model.PaymentMethod
                                            , customer.Code
                                            , list
                                            , listinvoice
                                            , list_inbound);
                                    }
                                }
                            }
                        }
                    }

                    //Cập nhật total productInbound
                    var totalAmount = productInboundRepository.GetAllProductInboundDetailByInboundId(productInbound.Id).Sum(item => item.Price * item.Quantity);
                    productInbound.TotalAmount = totalAmount;
                    productInboundRepository.UpdateProductInbound(productInbound);
                    #endregion
                    List<ProductInboundDetail> list_inbound_2 = new List<ProductInboundDetail>();
                    foreach (var group in list_inbound.GroupBy(x => x.ProductId))
                    {
                        list_inbound_2.Add(new Domain.Sale.Entities.ProductInboundDetail
                        {
                            ProductId = group.Key,
                            Quantity = group.Sum(x => x.Quantity),
                            Unit = group.FirstOrDefault().Unit,
                            Price = group.FirstOrDefault().Price,
                            IsDeleted = false,
                            CreatedUserId = WebSecurity.CurrentUserId,
                            ModifiedUserId = WebSecurity.CurrentUserId,
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now,
                            ProductInboundId = productInbound.Id
                        });
                    }

                    //Cập nhật vị trí sản phẩm thêm vào kho cho từng sản phẩm riêng biệt
                    for (int ii = 0; ii < list_inbound_2.Count(); ii++)
                    {
                        productInboundRepository.InsertProductInboundDetail(list_inbound_2[ii]);
                        for (int i = 1; i <= list_inbound_2[ii].Quantity; i++)
                        {
                            var warehouseLocationItem = new WarehouseLocationItem();
                            warehouseLocationItem.IsDeleted = false;
                            warehouseLocationItem.CreatedUserId = WebSecurity.CurrentUserId;
                            warehouseLocationItem.ModifiedUserId = WebSecurity.CurrentUserId;
                            warehouseLocationItem.CreatedDate = DateTime.Now;
                            warehouseLocationItem.ModifiedDate = DateTime.Now;
                            warehouseLocationItem.ProductId = list_inbound_2[ii].ProductId;
                            warehouseLocationItem.ProductInboundId = list_inbound_2[ii].ProductInboundId;
                            warehouseLocationItem.ProductInboundDetailId = list_inbound_2[ii].Id;
                            warehouseLocationItem.IsOut = false;
                            warehouseLocationItem.Shelf = "1";
                            warehouseLocationItem.Floor = "1";
                            warehouseLocationItem.WarehouseId = productInbound.WarehouseDestinationId;
                            warehouseLocationItemRepository.InsertWarehouseLocationItem(warehouseLocationItem);
                        }
                    }
                    //cập naht65 tổng tiền cho phiếu nhập
                    if (list_inbound_2.Count() > 0)
                    {
                        productInbound.TotalAmount = list_inbound_2.Sum(item => (item.Price * item.Quantity));
                        productInboundRepository.UpdateProductInbound(productInbound);
                    }
                    #region thanh toán công nợ
                    if (model.PaymentMethod == "Trả lại tiền mặt")
                    {
                        //Nếu chọn là "Trả lại tiền mặt" thì tạo chứng từ chi
                        var payment = new Payment();
                        AutoMapper.Mapper.Map(model.paymentViewModel, payment);
                        payment.IsDeleted = false;
                        payment.CreatedUserId = WebSecurity.CurrentUserId;
                        payment.ModifiedUserId = WebSecurity.CurrentUserId;
                        payment.AssignedUserId = WebSecurity.CurrentUserId;
                        payment.CreatedDate = DateTime.Now;
                        payment.ModifiedDate = DateTime.Now;
                        payment.PaymentMethod = "Tiền mặt";
                        payment.Amount = model.TotalAmount;
                        payment.Name = "Hàng bán trả lại - Chi tiền mặt";

                        paymentRepository.InsertPayment(payment);

                        var prefixPayment = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_SalesReturnsAndPayment");
                        payment.Code = Erp.BackOffice.Helpers.Common.GetCode(prefixPayment, payment.Id);
                        paymentRepository.UpdatePayment(payment);

                        //Thêm vào quản lý chứng từ
                        TransactionController.Create(new TransactionViewModel
                        {
                            TransactionModule = "Payment",
                            TransactionCode = payment.Code,
                            TransactionName = "Hàng bán trả lại - Chi tiền mặt"
                        });

                        //Thêm chứng từ liên quan
                        TransactionController.CreateRelationship(new TransactionRelationshipViewModel
                        {
                            TransactionA = payment.Code,
                            TransactionB = salesReturns.Code
                        });
                    }
                    //nếu là bù trừ công nợ thì làm
                    else
                    {
                        foreach (var item in listinvoice)
                        {
                            //Ghi Có TK 131 - Phải thu của khách hàng.
                            Erp.BackOffice.Account.Controllers.TransactionLiabilitiesController.Create(
                                salesReturns.Code,
                                "SalesReturns",
                                "Hàng bán trả lại - Bù trừ công nợ",
                               customer.Code,
                                "Customer",
                                0,
                               item.Amount.Value,
                               item.ProductInvoiceCode,
                                "ProductInvoice",
                                null,
                                null,
                                null);

                        }
                    }
                    //update số tiền đơn hàng trả lại
                    var Return = SalesReturnsRepository.GetSalesReturnsById(salesReturns.Id);
                    Return.TotalAmount = listinvoice.Sum(x => x.Amount);
                    SalesReturnsRepository.UpdateSalesReturns(Return);
                    #endregion

                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                    return RedirectToAction("Detail", new { Id = salesReturns.Id });
                }
                else
                {
                    TempData[Globals.FailedMessageKey] = "Không có sản phẩm. Bạn không thể trả hàng. Vui lòng bạn kiểm tra lại!!";
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Create");
        }

        #region CreateDetailReturnAndDetailInbound
        public void CreateDetailReturnAndDetailInbound(int ProductId,int? DisCount, int Quantity, int? quantity_return, decimal Price,
                                 int ProductInvoiceId, int ProductInvoiceDetailId, int SaleReturnId, int ProductInboundId, string SaleReturnCode,
                                 string ProductInvoiceCode, int? WarehouseDestinationId, string PaymentMethod, string customerCode, List<string> list,
                                 List<ProductInvoiceDetailViewModel> listinvoice, List<ProductInboundDetail> list_inbound)
        {
            //đánh đấu vào hóa đơn là có trả hàng hóa
            var invoiceDetail = productInvoiceRepository.GetProductInvoiceDetailById(ProductInvoiceDetailId);
            invoiceDetail.IsReturn = true;
            invoiceDetail.QuantitySaleReturn = quantity_return;
            productInvoiceRepository.UpdateProductInvoiceDetail(invoiceDetail);
            decimal total_Amount = Convert.ToDecimal((Quantity * invoiceDetail.Price));
            decimal discount = (total_Amount * (DisCount.HasValue ? DisCount.Value : 0)) / 100;
            total_Amount = total_Amount - discount;
            var SalesReturnsDetail = new SalesReturnsDetail();
            //hàng bán trả lại
            SalesReturnsDetail.IsDeleted = false;
            SalesReturnsDetail.CreatedUserId = WebSecurity.CurrentUserId;
            SalesReturnsDetail.ModifiedUserId = WebSecurity.CurrentUserId;
            SalesReturnsDetail.CreatedDate = DateTime.Now;
            SalesReturnsDetail.ModifiedDate = DateTime.Now;
            SalesReturnsDetail.ProductInvoiceDetailId = ProductInvoiceDetailId;
            SalesReturnsDetail.ProductInvoiceId = ProductInvoiceId;
            SalesReturnsDetail.SalesReturnsId = SaleReturnId;
            SalesReturnsDetail.Quantity = Quantity;
            SalesReturnsDetail.ProductId = ProductId;
            SalesReturnsDetail.DisCount = DisCount;
            SalesReturnsDetail.DisCountAmount = Convert.ToInt32(discount);
            SalesReturnsRepository.InsertSalesReturnsDetail(SalesReturnsDetail);
            TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;

            //phiếu nhập
            var productInboundDetail = new ProductInboundDetail();
            productInboundDetail.IsDeleted = false;
            productInboundDetail.CreatedUserId = WebSecurity.CurrentUserId;
            productInboundDetail.ModifiedUserId = WebSecurity.CurrentUserId;
            productInboundDetail.CreatedDate = DateTime.Now;
            productInboundDetail.ModifiedDate = DateTime.Now;
            productInboundDetail.ProductInboundId = ProductInboundId;
            productInboundDetail.ProductId = SalesReturnsDetail.ProductId;
            productInboundDetail.Quantity = SalesReturnsDetail.Quantity;
            productInboundDetail.Price = Price;
            list_inbound.Add(productInboundDetail);
           

           
            //Thêm vào quản lý chứng từ
            TransactionController.Create(new TransactionViewModel
            {
                TransactionModule = "SalesReturns",
                TransactionCode = SaleReturnCode,
                TransactionName = "Hàng bán trả lại"
            });

            //lưu lại mã hóa đơn để kiểm tra, trùng thì ko lưu chứng từ liên quan
            if (list.Where(x => x.ToString() == ProductInvoiceCode.ToString()).Count() <= 0)
            {
                list.Add(ProductInvoiceCode);
                //Thêm chứng từ liên quan
                TransactionController.CreateRelationship(new TransactionRelationshipViewModel
                {
                    TransactionA = SaleReturnCode,
                    TransactionB = ProductInvoiceCode
                });
            }

       
            //dựa vào mã hóa đơn nếu trùng mã hóa đơn thì lấy ra model trùng đang lưu số tiền cập nhật lại, không trùng mã thì thêm vào bình thường
            var listproduct = listinvoice.Where(x => x.ProductInvoiceCode == ProductInvoiceCode);
            //list lưu lại số tiền của sản phẩm để tính giảm trừ công nợ của từng hóa đơn

            var model = new ProductInvoiceDetailViewModel();
            model.ProductInvoiceCode = ProductInvoiceCode;
            if (listproduct.Count() > 0)
            {
                model.Amount = total_Amount + listproduct.FirstOrDefault().Amount;
                listinvoice.Remove(listproduct.FirstOrDefault());
            }
            else
            {
                model.Amount = total_Amount;
            }

            listinvoice.Add(model);

        }
        #endregion


        public ViewResult SearchProductInvoice(int? CustomerId)
        {
            var saleDepartmentCode = Erp.BackOffice.Helpers.Common.GetSetting("SaleDepartmentCode");
            //string image_folder = Helpers.Common.GetSetting("product-image-folder");
            List<ProductViewModel> model = new List<ProductViewModel>();

            if (CustomerId != null && CustomerId > 0)
            { 
                var productinvoice=productInvoiceRepository.GetAllvwInvoiceDetails().Where(x=>x.CustomerId==CustomerId&&x.ProductType=="product"&&x.QuantitySaleReturn>0).ToList();
              
                    foreach (var group in productinvoice.GroupBy(x => x.ProductId))
                    {
                          var product = productRepository.GetvwProductById(group.Key.Value);
                          model.Add(new ProductViewModel
                        {
                            Id = group.Key.Value,
                            QuantityTotalInventory = group.Sum(x => x.QuantitySaleReturn),
                            Unit = group.FirstOrDefault().Unit,
                            PriceOutbound = product.PriceOutbound,
                            Type = product.Type,
                            Code = product.Code,
                            Barcode = product.Barcode,
                            Name = product.Name,
                            CategoryCode = product.CategoryCode,
                            Image_Name = Erp.BackOffice.Helpers.Common.KiemTraTonTaiHinhAnh(product.Image_Name, "product-image-folder","product")
                        });
                    }
            }
            //ViewBag.productList = list_inbound_2;
            return View(model);
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
            var salesReturns = SalesReturnsRepository.GetvwSalesReturnsById(Id);
            //lấy thông tin khách hàng
            var customer = customerRepository.GetvwCustomerByCode(salesReturns.CustomerCode);
            //lấy người lập phiếu xuất kho
            var user = userRepository.GetUserById(salesReturns.SalerId.Value);
            List<SalesReturnsDetailViewModel> detailList = new List<SalesReturnsDetailViewModel>();
            if (salesReturns != null && salesReturns.IsDeleted != true)
            {
                //lấy danh sách sản phẩm xuất kho
                detailList = SalesReturnsRepository.GetAllReturnsDetailsByReturnId(Id)
                        .Select(x => new SalesReturnsDetailViewModel
                        {
                            Id = x.Id,
                            Price = x.Price,
                            ProductId = x.ProductId,
                            ProductName = x.ProductName,
                            ProductCode = x.ProductCode,
                            Quantity = x.Quantity,
                            UnitProduct = x.UnitProduct,
                            DisCount = x.DisCount.HasValue ? x.DisCount.Value : 0,
                            DisCountAmount = x.DisCountAmount.HasValue ? x.DisCountAmount : 0,
                            ProductGroup = x.ProductGroup,
                            ProductInvoiceCode=x.ProductInvoiceCode,
                            ProductInvoiceDate=x.ProductInvoiceDate,
                            ProductInvoiceDetailId=x.ProductInvoiceDetailId,
                            ProductInvoiceId=x.ProductInvoiceId,
                            SalesReturnsId=x.SalesReturnsId,
                            SalesReturnDate=x.SalesReturnDate,
                            SalerInvoiceName=x.SalerInvoiceName
                        }).ToList();
            }

            //tạo dòng của table html danh sách sản phẩm.
            var ListRow = "";
            int tong_tien = 0;
            int da_thanh_toan = 0;
            int con_lai = 0;
            var groupProduct = detailList.GroupBy(x => new { x.ProductGroup }, (key, group) => new ProductInvoiceDetailViewModel
            {
                ProductGroup = key.ProductGroup,
                ProductId = group.FirstOrDefault().ProductId,
                Id = group.FirstOrDefault().Id
            }).ToList();
            var Rows = "";
            var ProductGroupName = new Category();
            foreach (var i in groupProduct)
            {
                var count = detailList.Where(x => x.ProductGroup == i.ProductGroup).ToList();
                var chiet_khau1 = count.Sum(x => x.DisCountAmount.HasValue ? x.DisCountAmount.Value : 0);
                decimal? subTotal1 = count.Sum(x => (x.Quantity) * (x.Price));
                var thanh_tien1 = subTotal1 - chiet_khau1;
                if (!string.IsNullOrEmpty(i.ProductGroup))
                {
                    ProductGroupName = categoryRepository.GetCategoryByCode("ProductGroup").Where(x => x.Value == i.ProductGroup).FirstOrDefault();

                    Rows = "<tr style=\"background:#eee;font-weight:bold\"><td colspan=\"6\" class=\"text-left\">" + (i.ProductGroup == null ? "" : i.ProductGroup) + ": " + (ProductGroupName.Name == null ? "" : ProductGroupName.Name) + "</td><td class=\"text-right\">"
                         + Erp.BackOffice.Helpers.Common.PhanCachHangNgan(count.Sum(x => x.Quantity))
                         + "</td><td colspan=\"2\" class=\"text-right\"></td><td class=\"text-right\">"
                         + Erp.BackOffice.Helpers.Common.PhanCachHangNgan(thanh_tien1)
                         + "</td></tr>";
                }
                ListRow += Rows;
                int index = 1;
                foreach (var item in detailList.Where(x => x.ProductGroup == i.ProductGroup))
                {
                    decimal? subTotal = item.Quantity * item.Price.Value;
                    var chiet_khau = item.DisCountAmount.HasValue ? item.DisCountAmount.Value : 0;
                    var thanh_tien = subTotal - chiet_khau;

                    var Row = "<tr>"
                     + "<td class=\"text-center\">" + (index++) + "</td>"
                     + "<td class=\"text-right\">" + item.ProductCode + "</td>"
                     + "<td class=\"text-left\">" + item.ProductName + "</td>"
                     + "<td class=\"text-left\">" + item.ProductInvoiceCode+" ("+item.ProductInvoiceDate.Value.ToString("dd/MM/yyyy HH:mm")+")" + "</td>"
                    // + "<td class=\"text-center\">" + (item.LoCode == null ? "" : item.LoCode) + "</td>"
                  //   + "<td class=\"text-center\">" + (item.ExpiryDate == null ? "" : item.ExpiryDate.Value.ToShortDateString()) + "</td>"
                     + "<td class=\"text-center\">" + item.UnitProduct + "</td>"
                     + "<td class=\"text-right\">" + item.Quantity.Value + "</td>"
                     + "<td class=\"text-right\">" + Erp.BackOffice.Helpers.Common.PhanCachHangNgan(item.Price) + "</td>"
                     + "<td class=\"text-right\">" + (item.DisCount.HasValue ? item.DisCount.Value : 0) + "</td>"
                     + "<td class=\"text-right\">" + Erp.BackOffice.Helpers.Common.PhanCachHangNgan(chiet_khau) + "</td>"
                     + "<td class=\"text-right\">" + Erp.BackOffice.Helpers.Common.PhanCachHangNgan(thanh_tien) + "</td></tr>";
                    ListRow += Row;
                }
            }

            //khởi tạo table html.                
            var table = "<table class=\"invoice-detail\"><thead><tr> <th>STT</th> <th>Mã hàng</th><th>Tên mặt hàng</th><th>Hóa đơn BH</th><th>ĐVT</th><th>Số lượng</th><th>Đơn giá</th><th>% CK</th><th>Trị giá chiết khấu</th><th>Thành tiền</th></tr></thead><tbody>"
                         + ListRow
                //+ "<tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>"
                //+ "<tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>"
                         + "</tbody><tfoot>"
                         + "<tr><td colspan=\"6\" class=\"text-right\"></td><td class=\"text-right\">"
                         + Erp.BackOffice.Helpers.Common.PhanCachHangNgan(detailList.Sum(x => x.Quantity))
                         + "</td><td colspan=\"2\" class=\"text-right\">Tổng cộng</td><td class=\"text-right\">"
                         + Erp.BackOffice.Helpers.Common.PhanCachHangNgan(salesReturns.TotalAmount.Value)
                         + "</td></tr>"
                //  + "<tr><td colspan=\"10\" class=\"text-right\">VAT (" + vat + "%)</td><td class=\"text-right\">"
                //+ Erp.BackOffice.Helpers.Common.PhanCachHangNgan(totalVAT)
                //+ "</td></tr>"
                // + "<tr><td colspan=\"10\" class=\"text-right\">Tổng tiền phải thanh toán</td><td class=\"text-right\">"
                //+ Erp.BackOffice.Helpers.Common.PhanCachHangNgan(total)
                //+ "</td></tr>"
         
                         + "</tfoot></table>";

            //lấy template phiếu xuất.
            var template = templatePrintRepository.GetAllTemplatePrint().Where(x => x.Code == "SalesReturns").OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            //truyền dữ liệu vào template.
            model.Content = template.Content;
            model.Content = model.Content.Replace("{InvoiceCode}", salesReturns.Code);
            model.Content = model.Content.Replace("{Day}", salesReturns.CreatedDate.Value.Day.ToString());
            model.Content = model.Content.Replace("{Month}", salesReturns.CreatedDate.Value.Month.ToString());
            model.Content = model.Content.Replace("{Year}", salesReturns.CreatedDate.Value.Year.ToString());
            model.Content = model.Content.Replace("{CustomerName}", customer.LastName + " " + customer.FirstName);
            model.Content = model.Content.Replace("{CustomerPhone}", customer.Phone);
            model.Content = model.Content.Replace("{CompanyName}", customer.CompanyName);

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

            model.Content = model.Content.Replace("{Note}", salesReturns.Note);
          //  model.Content = model.Content.Replace("{InvoiceCode}", productInvoice.Code);
            model.Content = model.Content.Replace("{SaleName}", user.FullName);
          //  model.Content = model.Content.Replace("{CodeInvoiceRed}", productInvoice.CodeInvoiceRed);
            model.Content = model.Content.Replace("{PaymentMethod}", salesReturns.PaymentMethod);
            model.Content = model.Content.Replace("{MoneyText}", Erp.BackOffice.Helpers.Common.ChuyenSoThanhChu_2(Convert.ToInt32(salesReturns.TotalAmount.Value)));

            model.Content = model.Content.Replace("{DataTable}", table);
            model.Content = model.Content.Replace("{System.Logo}", ImgLogo);
            model.Content = model.Content.Replace("{System.CompanyName}", company);
            model.Content = model.Content.Replace("{System.AddressCompany}", address);
            model.Content = model.Content.Replace("{System.PhoneCompany}", phone);
            model.Content = model.Content.Replace("{System.Fax}", fax);
            model.Content = model.Content.Replace("{System.BankCodeCompany}", bankcode);
            model.Content = model.Content.Replace("{System.BankNameCompany}", bankname);

            if (ExportExcel)
            {
                Response.AppendHeader("content-disposition", "attachment;filename=" + salesReturns.CreatedDate.Value.ToString("yyyyMMdd") + salesReturns.Code + ".xls");
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.Write(model.Content);
                Response.End();
            }

            return View(model);
        }
        #endregion


    }
}
