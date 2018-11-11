using System.Globalization;
using Erp.BackOffice.Sale.Models;
using Erp.BackOffice.Filters;
using Erp.Domain.Sale.Entities;
using Erp.Domain.Interfaces;
using Erp.Domain.Sale.Interfaces;
using Erp.Domain.Sale.Repositories;
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
using Erp.BackOffice.Account.Controllers;
using Erp.BackOffice.Account.Models;
using Erp.Domain.Account.Interfaces;
using Erp.Domain.Account.Entities;
using Erp.Domain.Entities;
using System.Web;
using System.Transactions;

namespace Erp.BackOffice.Sale.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class PurchaseOrderController : Controller
    {
        private readonly IWarehouseRepository WarehouseRepository;
        private readonly IPurchaseOrderRepository PurchaseOrderRepository;
        private readonly IProductOrServiceRepository productRepository;
        private readonly ISupplierRepository SupplierRepository;
        private readonly IInventoryRepository InventoryRepository;
        private readonly IProductOutboundRepository ProductOutboundRepository;
        private readonly IProductInboundRepository productInboundRepository;
        private readonly IUserRepository userRepository;
        private readonly IQueryHelper QueryHelper;
        private readonly ITransactionLiabilitiesRepository transactionLiabilitiesRepository;
        private readonly ITransactionRepository transactionRepository;
        private readonly IPaymentRepository paymentRepository;
        private readonly IPaymentDetailRepository paymentDetailRepository;
        private readonly IWarehouseLocationItemRepository warehouseLocationItemRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly ITemplatePrintRepository templatePrintRepository;
        public PurchaseOrderController(
            IPurchaseOrderRepository _PurchaseOrder
            , ITransactionLiabilitiesRepository _transactionLiabilities
            , IWarehouseRepository _Warehouse
            , IProductOrServiceRepository _Product
            , ISupplierRepository _Supplier
            , IInventoryRepository _Inventory
            , IProductOutboundRepository _ProductOutbound
            , IUserRepository _user
            , IQueryHelper _qsueryHelper
            , IProductInboundRepository productInbound
            , ITransactionRepository _transaction
            , IPaymentRepository payment
            , IPaymentDetailRepository paymentDetail
            , IWarehouseLocationItemRepository warehouseLocationItem
            , ICategoryRepository category
            , ITemplatePrintRepository templatePrint
            )
        {
            PurchaseOrderRepository = _PurchaseOrder;
            WarehouseRepository = _Warehouse;
            productRepository = _Product;
            InventoryRepository = _Inventory;
            ProductOutboundRepository = _ProductOutbound;
            SupplierRepository = _Supplier;
            userRepository = _user;
            QueryHelper = _qsueryHelper;
            productInboundRepository = productInbound;
            transactionLiabilitiesRepository = _transactionLiabilities;
            transactionRepository = _transaction;
            paymentRepository = payment;
            paymentDetailRepository = paymentDetail;
            warehouseLocationItemRepository = warehouseLocationItem;
            categoryRepository = category;
            templatePrintRepository = templatePrint;
        }

        #region Index

        public ViewResult Index(string txtCode, string txtSupplierName, string txtMinAmount, string txtMaxAmount, int? warehouseDestinationId, string startDate, string endDate)
        {
            IEnumerable<PurchaseOrderViewModel> q = PurchaseOrderRepository.GetAllvwPurchaseOrder().AsEnumerable().Where(x => x.SupplierId != null)
                .Select(item => new PurchaseOrderViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    ModifiedDate = item.ModifiedDate,
                    Code = item.Code,
                    TotalAmount = item.TotalAmount,
                    Discount = item.Discount,
                    TaxFee = item.TaxFee,
                    Status = item.Status,
                    SupplierName = item.SupplierName,
                    WarehouseDestinationName = item.WarehouseDestinationName,
                    WarehouseSourceName = item.WarehouseSourceName,
                    ProductInboundId = item.ProductInboundId,
                    ProductInboundCode = item.ProductInboundCode,
                    CancelReason = item.CancelReason,
                    BarCode = item.BarCode,
                    IsArchive = item.IsArchive.Value,
                    Note = item.Note,
                    SupplierId = item.SupplierId,
                    WarehouseDestinationId = item.WarehouseDestinationId,
                    IsDeleted = item.IsDeleted
                }).OrderByDescending(m => m.Id);

            if (string.IsNullOrEmpty(txtCode) == false || string.IsNullOrEmpty(txtSupplierName) == false || warehouseDestinationId != null)
            {
                txtCode = txtCode == "" ? "~" : txtCode.ToLower();
                txtSupplierName = txtSupplierName == "" ? "~" : Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(txtSupplierName);
                q = q.Where(x => x.Code.ToLowerOrEmpty().Contains(txtCode)
                    || Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(x.SupplierName).Contains(txtSupplierName));
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
            var user = userRepository.GetByvwUserName(Helpers.Common.CurrentUser.UserName);
            if (!Erp.BackOffice.Filters.SecurityFilter.IsAdmin())
            {
                q = q.Where(x => ("," + user.WarehouseId + ",").Contains("," + x.WarehouseDestinationId + ",") == true);
            }
            if (warehouseDestinationId != null && warehouseDestinationId.Value > 0)
            {
                q = q.Where(x => x.WarehouseDestinationId == warehouseDestinationId);
            }
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];

            var warehouseList = WarehouseRepository.GetAllWarehouse().AsEnumerable()
               .Select(item => new SelectListItem
               {
                   Text = item.Name,
                   Value = item.Id.ToString()
               });
            ViewBag.warehouseList = warehouseList;

            return View(q);
        }
        public ViewResult IndexWH(string txtCode, int? warehouseDestinationId, int? warehouseSourceId, string txtMinAmount, string txtMaxAmount, string startDate, string endDate)
        {
            IEnumerable<PurchaseOrderViewModel> q = PurchaseOrderRepository.GetAllvwPurchaseOrder().Where(x => x.SupplierId == null && ("," + Helpers.Common.CurrentUser.DrugStore + ",").Contains("," + x.BranchId + ",") == true)
                .Select(item => new PurchaseOrderViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    ModifiedDate = item.ModifiedDate,
                    Code = item.Code,
                    TotalAmount = item.TotalAmount,
                    Discount = item.Discount,
                    Status = item.Status,
                    WarehouseDestinationName = item.WarehouseDestinationName,
                    WarehouseSourceName = item.WarehouseSourceName,
                    WarehouseDestinationId = item.WarehouseDestinationId,
                    WarehouseSourceId = item.WarehouseSourceId

                }).OrderByDescending(m => m.ModifiedDate);

            if (string.IsNullOrEmpty(txtCode) == false || warehouseDestinationId != null || warehouseSourceId != null)
            {
                txtCode = txtCode == "" ? "~" : txtCode.ToLower();
                q = q.Where(x => x.Code.ToLowerOrEmpty().Contains(txtCode)
                    || x.WarehouseDestinationId == warehouseDestinationId
                    || x.WarehouseSourceId == warehouseSourceId);
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

            var warehouseList = WarehouseRepository.GetAllWarehouse().AsEnumerable()
               .Select(item => new SelectListItem
               {
                   Text = item.Name,
                   Value = item.Id.ToString()
               });
            ViewBag.warehouseList = warehouseList;

            return View(q);
        }
        #endregion

        //#region Create
        //public ActionResult Create(PurchaseOrderViewModel model)
        //{
        //    if(model == null)
        //        model = new PurchaseOrderViewModel();

        //    var supplierList = SupplierRepository.GetAllSupplier().AsEnumerable()
        //       .Select(item => new SelectListItem
        //       {
        //           Text = item.Name,
        //           Value = item.Id.ToString()
        //       });
        //    ViewBag.supplierList = supplierList;

        //    model.CreatedUserName = Erp.BackOffice.Helpers.Common.CurrentUser.FullName;
        //    model.CreatedDate = DateTime.Now;

        //    return View(model);
        //}

        //public ActionResult CreateWH()
        //{
        //    //foreach (var modelValue in ModelState.Values)
        //    //{
        //    //    if (modelValue.Value == null)
        //    //        modelValue.Errors.Clear();
        //    //}
        //    PurchaseOrderViewModel model = new PurchaseOrderViewModel();

        //    var warehouseList = WarehouseRepository.GetAllWarehouse().Where(x => x.BranchId == Helpers.Common.CurrentUser.BranchId).AsEnumerable()
        //       .Select(item => new SelectListItem
        //       {
        //           Text = item.Name,
        //           Value = item.Id.ToString()
        //       });
        //    ViewBag.warehouseList = warehouseList;

        //    var productList = ProductRepository.GetAllProduct()
        //       .Select(item => new ProductViewModel
        //       {
        //           Code = item.Code,
        //           Barcode = item.Barcode,
        //           Name = item.Name,
        //           Id = item.Id,
        //           CategoryCode = item.CategoryCode,
        //           PriceInbound = item.PriceInbound,
        //           Unit = item.Unit
        //       });
        //    ViewBag.productList = productList;

        //    model.CreatedUserName = Erp.BackOffice.Helpers.Common.CurrentUser.FullName;
        //    model.CreatedDate = DateTime.Now;
        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult Create(PurchaseOrderViewModel model, FormCollection fc)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var PurchaseOrder = new Domain.Sale.Entities.PurchaseOrder();
        //        AutoMapper.Mapper.Map(model, PurchaseOrder);
        //        PurchaseOrder.IsDeleted = false;
        //        PurchaseOrder.CreatedUserId = WebSecurity.CurrentUserId;
        //        PurchaseOrder.ModifiedUserId = WebSecurity.CurrentUserId;
        //        PurchaseOrder.AssignedUserId = WebSecurity.CurrentUserId;
        //        PurchaseOrder.CreatedDate = model.CreatedDate == null ? DateTime.Now : model.CreatedDate;
        //        PurchaseOrder.ModifiedDate = DateTime.Now;

        //        PurchaseOrder.BranchId = Helpers.Common.CurrentUser.BranchId;

        //        //5 trạng thái: pending - chờ duyệt, outbound pending - chờ xuất kho, outbound done - đã xuất kho, shipping - đang chuyển hàng, complete - đã hoàn thành, cancel - hủy
        //        PurchaseOrder.Status = Wording.OrderStatus_pending;
        //        //đối với đơn hàng đặt từ nhà cung cấp thì người theo dõi phải tự chuyển trạng thái

        //        //duyệt qua danh sách sản phẩm mới xử lý tình huống user chọn 2 sản phầm cùng id
        //        List<PurchaseOrderDetail> orderDetails = new List<PurchaseOrderDetail>();
        //        foreach (var group in model.DetailList.GroupBy(x => x.ProductId))
        //        {
        //            var product = ProductRepository.GetProductById(group.Key.Value);

        //            orderDetails.Add(new PurchaseOrderDetail
        //            {
        //                ProductId = product.Id,
        //                Quantity = group.Sum(x => x.Quantity),
        //                Unit = product.Unit,
        //                Price = group.FirstOrDefault().Price,
        //                IsDeleted = false,
        //                CreatedUserId = WebSecurity.CurrentUserId,
        //                ModifiedUserId = WebSecurity.CurrentUserId,
        //                CreatedDate = DateTime.Now,
        //                ModifiedDate = DateTime.Now
        //            });
        //        }

        //        int orderId = PurchaseOrderRepository.InsertPurchaseOrder(PurchaseOrder, orderDetails);

        //        string prefix = string.Empty;
        //        if (model.SupplierId != null)
        //        {
        //            prefix = Helpers.Common.GetSetting("prefixOrderNo_PurchaseOrder");
        //        }
        //        else
        //        {
        //            prefix = Helpers.Common.GetSetting("prefixOrderNo_PurchaseOrderWH");
        //        }

        //        //cập nhật lại mã cho đơn đặt hàng
        //        PurchaseOrder.Code = Helpers.Common.GetCode(prefix, PurchaseOrder.Id);
        //        PurchaseOrderRepository.UpdatePurchaseOrder(PurchaseOrder);


        //        //Apply business process flow
        //        //Crm.Controllers.ProcessController.Run(Crm.Controllers.ProcessController.ActionName.Create, PurchaseOrder);
        //        Crm.Controllers.ProcessController.Run("Process", "Create", model.Id, model.ModifiedUserId, null, model);

        //        if (Request["IsPopup"] == "true")
        //        {
        //            ViewBag.closePopup = "close and append to page parent";
        //            model.Id = PurchaseOrder.Id;
        //            return View(model);
        //        }

        //        if (model.SupplierId == null)
        //            return RedirectToAction("IndexWH");


        //        TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
        //        return RedirectToAction("Index");
        //    }

        //    string error_message = string.Empty;
        //    foreach(var modelState in ViewData.ModelState.Values)
        //    {
        //        foreach(var error in modelState.Errors)
        //        {
        //            error_message += error.ErrorMessage;
        //        }
        //    }


        //    // modealState is vaild
        //    var supplierList = SupplierRepository.GetAllSupplier().AsEnumerable()
        //       .Select(item => new SelectListItem
        //       {
        //           Text = item.Name,
        //           Value = item.Id.ToString()
        //       });
        //    ViewBag.supplierList = supplierList;

        //    var warehouseList = WarehouseRepository.GetAllWarehouse().Where(x => x.BranchId == Helpers.Common.CurrentUser.BranchId).AsEnumerable()
        //       .Select(item => new SelectListItem
        //       {
        //           Text = item.Name,
        //           Value = item.Id.ToString()
        //       });
        //    ViewBag.warehouseList = warehouseList;

        //    if (model.SupplierId == null)
        //        return View("CreateWH", model);

        //    return View("Create", model);
        //}

        //#endregion

        //public ActionResult View(int? Id) 
        //{
        //    var PurchaseOrder = PurchaseOrderRepository.GetvwPurchaseOrderById(Id.Value);
        //    if (PurchaseOrder != null && PurchaseOrder.IsDeleted != true)
        //    {
        //        var model = new PurchaseOrderViewModel();
        //        AutoMapper.Mapper.Map(PurchaseOrder, model);

        //        //if (model.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
        //        //{
        //        //    TempData["FailedMessage"] = "NotOwner";
        //        //    return RedirectToAction("Index");
        //        //}

        //        // lấy danh sách order detail
        //        var orderDetails = PurchaseOrderRepository.GetAllOrderDetailsByOrderId(Id.Value)
        //            .Select(x => new PurchaseOrderDetailViewModel
        //            {
        //                Id = x.Id,
        //                Price = x.Price,
        //                ProductId = x.ProductId,
        //                PurchaseOrderId = x.PurchaseOrderId,
        //                Quantity = x.Quantity,
        //                Unit = x.Unit
        //            }).ToList();

        //        foreach (var item in orderDetails)
        //        {
        //            var product = ProductRepository.GetProductById(item.ProductId.Value);
        //            item.ProductName = product != null ? product.Name : "";
        //        }

        //        //gán danh sách sản phẩm vào model
        //        model.DetailList = orderDetails;


        //        ViewBag.flagNotEnoughInInventory = false;
        //        ViewBag.flagEdit = false;


        //        //lấy danh sách kho hàng
        //        var warehouseList = WarehouseRepository.GetAllWarehouse().Where(x => x.BranchId == Helpers.Common.CurrentUser.BranchId).AsEnumerable()
        //        .Select(item => new SelectListItem
        //        {
        //            Text = item.Name,
        //            Value = item.Id.ToString()
        //        });
        //        ViewBag.warehouseList = warehouseList;

        //        var user = userRepository.GetUserById(model.CreatedUserId.Value);
        //        model.CreatedUserName = user.FullName;

        //        return View(model);
        //    }

        //    if (Request.UrlReferrer != null)
        //        return Redirect(Request.UrlReferrer.AbsoluteUri);
        //    return RedirectToAction("Index");
        //}

        //public ActionResult Next(int? Id)
        //{
        //    var PurchaseOrder = PurchaseOrderRepository.GetPurchaseOrderById(Id.Value);
        //    if (PurchaseOrder != null && PurchaseOrder.IsDeleted != true)
        //    {
        //        var model = new PurchaseOrderViewModel();
        //        AutoMapper.Mapper.Map(PurchaseOrder, model);

        //        //if (model.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
        //        //{
        //        //    TempData["FailedMessage"] = "NotOwner";
        //        //    return RedirectToAction("Index");
        //        //}

        //        // lấy danh sách order detail
        //        var orderDetails = PurchaseOrderRepository.GetAllOrderDetailsByOrderId(Id.Value)
        //            .Select(x => new PurchaseOrderDetailViewModel
        //            {
        //                Id = x.Id,
        //                Price = x.Price,
        //                ProductId = x.ProductId,
        //                PurchaseOrderId = x.PurchaseOrderId,
        //                Quantity = x.Quantity,
        //                Unit = x.Unit
        //            }).ToList();

        //        //kiểm tra số lượng sản phẩm đa chọn trong tồn kho
        //        bool flagNotEnoughInInventory = true;
        //        foreach (var detailItem in orderDetails)
        //        {
        //            var inventoryOfItem = InventoryRepository.GetAllInventoryByProductId(detailItem.ProductId.Value);
        //            if (inventoryOfItem != null)
        //            {
        //                detailItem.QuantityInInventory = inventoryOfItem.Sum(x => x.Quantity);
        //                if (detailItem.QuantityInInventory < detailItem.Quantity)
        //                {
        //                    flagNotEnoughInInventory = false;
        //                    break;
        //                }
        //            }
        //            else
        //            {
        //                flagNotEnoughInInventory = false;
        //                break;
        //            }

        //            var product = ProductRepository.GetProductById(detailItem.ProductId.Value);
        //            detailItem.ProductName = product.Name;

        //        }
        //        ViewBag.flagNotEnoughInInventory = flagNotEnoughInInventory;

        //        //gán danh sách sản phẩm vào model
        //        model.DetailList = orderDetails;


        //        var productList = ProductRepository.GetAllProduct()
        //        .Select(item => new ProductViewModel
        //        {
        //            Name = item.Name,
        //            Id = item.Id,
        //            CategoryCode = item.CategoryCode,
        //            PriceInbound = item.PriceInbound,
        //            Unit = item.Unit
        //        });

        //        ViewBag.productList = productList;

        //        //lấy danh sách nhà cung cấp
        //        var supplierList = SupplierRepository.GetAllSupplier().AsEnumerable()
        //        .Select(item => new SelectListItem
        //        {
        //           Text = item.Name,
        //           Value = item.Id.ToString()
        //        });
        //        ViewBag.supplierList = supplierList;

        //        //lấy danh sách kho hàng
        //        var warehouseList = WarehouseRepository.GetAllWarehouse().Where(x => x.BranchId == Helpers.Common.CurrentUser.BranchId).AsEnumerable()
        //        .Select(item => new SelectListItem
        //        {
        //           Text = item.Name,
        //           Value = item.Id.ToString()
        //        });
        //        ViewBag.warehouseList = warehouseList;

        //        //cho phép edit
        //        ViewBag.flagEdit = true;

        //        var user = userRepository.GetUserById(model.CreatedUserId.Value);
        //        model.CreatedUserName = user.FullName;

        //        return View("View", model);
        //    }
        //    if (Request.UrlReferrer != null)
        //        return Redirect(Request.UrlReferrer.AbsoluteUri);
        //    return RedirectToAction("Index");
        //}

        //[HttpPost]
        //public ActionResult UpdateStatus(string Status, int? Id)
        //{
        //    var PurchaseOrder = PurchaseOrderRepository.GetPurchaseOrderById(Id.Value);
        //    PurchaseOrder.Status = Status;
        //    PurchaseOrder.ModifiedUserId = WebSecurity.CurrentUserId;
        //    PurchaseOrder.ModifiedDate = DateTime.Now;
        //    PurchaseOrderRepository.UpdatePurchaseOrder(PurchaseOrder);

        //    if (PurchaseOrder.SupplierId == null)
        //        return RedirectToAction("IndexWH");

        //    return RedirectToAction("Index");
        //}

        //#region Edit
        //public ActionResult Edit(int? Id)
        //{
        //    var PurchaseOrder = PurchaseOrderRepository.GetPurchaseOrderById(Id.Value);
        //    if (PurchaseOrder != null && PurchaseOrder.IsDeleted != true)
        //    {
        //        var model = new PurchaseOrderViewModel();
        //        AutoMapper.Mapper.Map(PurchaseOrder, model);

        //        //if (model.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id 
        //        //    && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
        //        //{
        //        //    TempData["FailedMessage"] = "NotOwner";
        //        //    return RedirectToAction("Index");
        //        //}

        //        if (model.Status != Wording.OrderStatus_pending)
        //        {
        //            TempData["FailedMessage"] = Wording.PurchaseOrder + " " +  Wording.NotUpdateByChangeStatus;
        //            return RedirectToAction("Index");
        //        }


        //        // lấy danh sách order detail
        //        var orderDetails = PurchaseOrderRepository.GetAllOrderDetailsByOrderId(Id.Value)
        //            .Select(x => new PurchaseOrderDetailViewModel { 
        //                Id = x.Id,
        //                Price = x.Price,
        //                ProductId = x.ProductId,
        //                PurchaseOrderId = x.PurchaseOrderId,
        //                Quantity = x.Quantity,
        //                Unit = x.Unit
        //            }).ToList();


        //        //gán danh sách sản phẩm vào model
        //        model.DetailList = orderDetails;

        //        var productList = ProductRepository.GetAllProduct()
        //        .Select(item => new ProductViewModel
        //        {
        //            Name = item.Name,
        //            Id = item.Id,
        //            CategoryCode = item.CategoryCode,
        //            PriceInbound = item.PriceInbound,
        //            Unit = item.Unit
        //        });

        //        ViewBag.productList = productList;

        //        var supplierList = SupplierRepository.GetAllSupplier().AsEnumerable()
        //       .Select(item => new SelectListItem
        //       {
        //           Text = item.Name,
        //           Value = item.Id.ToString()
        //       });
        //        ViewBag.supplierList = supplierList;

        //        var user = userRepository.GetUserById(model.CreatedUserId.Value);
        //        model.CreatedUserName = user.FullName;

        //        return View(model);
        //    }
        //    if (Request.UrlReferrer != null)
        //        return Redirect(Request.UrlReferrer.AbsoluteUri);
        //    return RedirectToAction("Index");
        //}

        //[HttpPost]
        //public ActionResult Edit(PurchaseOrderViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (Request["Submit"] == "Save")
        //        {
        //            var PurchaseOrder = PurchaseOrderRepository.GetPurchaseOrderById(model.Id);
        //            var beforeEntityData = PurchaseOrder;                    

        //            AutoMapper.Mapper.Map(model, PurchaseOrder);
        //            PurchaseOrder.ModifiedUserId = WebSecurity.CurrentUserId;
        //            PurchaseOrder.ModifiedDate = DateTime.Now;

        //            // lấy danh sách order detail từ database 
        //            var listOldDetail = PurchaseOrderRepository.GetAllOrderDetailsByOrderId(PurchaseOrder.Id).ToList();

        //            //lấy danh sách các sản phẩm sẽ xóa từ danh sách mới so với danh sách cũ
        //            var listDelete = listOldDetail.Where(x => model.DetailList.Any(y => y.ProductId == x.ProductId) == false).ToList();

        //            //duyệt qua danh sách sản phẩm mới xử lý tình huống user chọn 2 sản phầm cùng id
        //            List<PurchaseOrderDetailViewModel> listNewCheckSameId = new List<PurchaseOrderDetailViewModel>();
        //            foreach (var group in model.DetailList.GroupBy(x => x.ProductId))
        //            {
        //                listNewCheckSameId.Add(new PurchaseOrderDetailViewModel { 
        //                    ProductId = group.Key,
        //                    PurchaseOrderId = PurchaseOrder.Id,
        //                    Quantity = group.Sum(x => x.Quantity),
        //                    Unit = group.FirstOrDefault().Unit,
        //                    Price = group.FirstOrDefault().Price
        //                });
        //            }

        //            //kiểm tra thêm mới sản phẩm nếu được chọn mới, hoặc cập nhật lại số lượng nếu có thay đổi
        //            foreach (var detaiItem in listNewCheckSameId)
        //            {
        //                var oldDetail = listOldDetail.Where(x => x.ProductId == detaiItem.ProductId).FirstOrDefault();
        //                if (oldDetail != null)
        //                {
        //                    //nếu như có sự thay đổi về số lượng
        //                    if(oldDetail.Quantity != detaiItem.Quantity)
        //                    {
        //                        oldDetail.Quantity = detaiItem.Quantity;
        //                        oldDetail.Price = detaiItem.Price;
        //                        oldDetail.ModifiedDate = DateTime.Now;
        //                        oldDetail.ModifiedUserId = WebSecurity.CurrentUserId;
        //                        PurchaseOrderRepository.UpdatePurchaseOrderDetail(oldDetail);
        //                    }
        //                    detaiItem.Price = oldDetail.Price;
        //                }
        //                else
        //                {
        //                    //thêm mới nếu danh sách cũ không có
        //                    var detailItemNew = new Domain.Sale.Entities.PurchaseOrderDetail
        //                    {
        //                        Unit = detaiItem.Unit,
        //                        Price = detaiItem.Price,
        //                        ProductId = detaiItem.ProductId,
        //                        Quantity = detaiItem.Quantity,
        //                        PurchaseOrderId = model.Id,
        //                        IsDeleted = false,
        //                        CreatedUserId = WebSecurity.CurrentUserId,
        //                        ModifiedUserId = WebSecurity.CurrentUserId,
        //                        CreatedDate = DateTime.Now,
        //                        ModifiedDate = DateTime.Now,
        //                    };
        //                    PurchaseOrderRepository.InsertPurchaseOrderDetail(detailItemNew);
        //                }
        //            }

        //            // thực hiện xóa các sản phẩm không nằm trong đơn hàng nữa;
        //            if (listDelete.Count != 0)
        //                PurchaseOrderRepository.DeletePurchaseOrderDetail(listDelete);


        //            PurchaseOrderRepository.UpdatePurchaseOrder(PurchaseOrder);

        //            ////Apply business process flow
        //            //Crm.Controllers.ProcessController.Run(Crm.Controllers.ProcessController.ActionName.Edit, beforeEntityData, PurchaseOrder);

        //            TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;

        //            return RedirectToAction("View", new { Id = model.Id});
        //        }

        //        var productList = ProductRepository.GetAllProduct()
        //        .Select(item => new ProductViewModel
        //        {
        //            Name = item.Name,
        //            Id = item.Id,
        //            CategoryCode = item.CategoryCode,
        //            PriceInbound = item.PriceInbound,
        //            Unit = item.Unit
        //        });

        //        ViewBag.productList = productList;

        //        var supplierList = SupplierRepository.GetAllSupplier().AsEnumerable()
        //        .Select(item => new SelectListItem
        //        {
        //           Text = item.Name,
        //           Value = item.Id.ToString()
        //        });
        //        ViewBag.supplierList = supplierList;

        //        return View(model);
        //    }
        //    return View(model);

        //    //if (Request.UrlReferrer != null)
        //    //    return Redirect(Request.UrlReferrer.AbsoluteUri);
        //    //return RedirectToAction("Index");
        //}

        //#endregion

        #region Delete
        [HttpPost]
        public ActionResult Delete(int Id, string CancelReason)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    var purchaseOrder = PurchaseOrderRepository.GetPurchaseOrderById(Id);
                    if (purchaseOrder != null)
                    {
                        purchaseOrder.ModifiedUserId = WebSecurity.CurrentUserId;
                        purchaseOrder.ModifiedDate = DateTime.Now;
                        purchaseOrder.IsDeleted = true;
                        purchaseOrder.IsArchive = false;
                        purchaseOrder.CancelReason = CancelReason;
                        purchaseOrder.Status = Wording.OrderStatus_deleted;
                        PurchaseOrderRepository.UpdatePurchaseOrder(purchaseOrder);
                        var vwpurchaseOrder = PurchaseOrderRepository.GetvwPurchaseOrderById(Id);
                        Erp.BackOffice.Crm.Controllers.ProcessController.Run("PurchaseOrder", "Delete", purchaseOrder.Id, null, null, vwpurchaseOrder);
                        scope.Complete();
                    }
                    return RedirectToAction("Detail", new { Id = purchaseOrder.Id });
                }
                catch (DbUpdateException)
                {
                    TempData[Globals.FailedMessageKey] = App_GlobalResources.Wording.Error;
                    return RedirectToAction("Detail", new { Id = Id });
                }
            }

            return RedirectToAction("Index");
        }
        #endregion

        public ActionResult GetOrderDetailsByOrderId(int? orderId)
        {
            if (orderId == null)
                return Content("");

            var order = PurchaseOrderRepository.GetPurchaseOrderById(orderId.Value);
            var list = PurchaseOrderRepository.GetAllOrderDetailsByOrderId(orderId.Value);

            List<PurchaseOrderDetailViewModel> model = new List<PurchaseOrderDetailViewModel>();
            if (order.ProductInboundId == null) // đơn đặt hàng nhà cung cấp
            {
                foreach (var item in list)
                {
                    model.Add(new PurchaseOrderDetailViewModel
                    {
                        Id = item.Id,
                        Price = item.Price,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        QuantityRemaining = item.QuantityRemaining,
                        Unit = item.Unit,
                        ProductName = productRepository.GetProductById(item.ProductId.Value).Name
                    });
                }
            }
            else // đơn đặt hàng từ đại lý hoặc chuyển kho thì lấy chi tiết của phiếu chuyển kho
            {
                var outboudnDetail = ProductOutboundRepository.GetAllProductOutboundDetailByOutboundId(order.ProductInboundId.Value);
                foreach (var item in outboudnDetail)
                {
                    var orderDetailItem = list.Where(x => x.ProductId == item.ProductId).FirstOrDefault();
                    model.Add(new PurchaseOrderDetailViewModel
                    {
                        Id = item.Id,
                        Price = item.Price,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        QuantityRemaining = orderDetailItem != null ? orderDetailItem.QuantityRemaining : 0,
                        Unit = item.Unit,
                        ProductName = productRepository.GetProductById(item.ProductId.Value).Name
                    });
                }
            }


            var productList = productRepository.GetAllProduct()
              .Select(item => new ProductViewModel
              {
                  Code = item.Code,
                  Barcode = item.Barcode,
                  Name = item.Name,
                  Id = item.Id,
                  CategoryCode = item.CategoryCode,
                  PriceInbound = item.PriceInbound,
                  Unit = item.Unit
              });
            ViewBag.productList = productList;

            return View(model);
        }


        #region - Json -
        public JsonResult GetListJsonOrderDetailById(int? orderId)
        {
            if (orderId == null)
                return Json(new List<int>(), JsonRequestBehavior.AllowGet);

            var list = PurchaseOrderRepository.GetAllOrderDetailsByOrderId(orderId.Value);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditInline(int? Id, string fieldName, string value)
        {
            Dictionary<string, object> field_value = new Dictionary<string, object>();
            field_value.Add(fieldName, value);
            field_value.Add("ModifiedDate", DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"));
            field_value.Add("ModifiedUserId", WebSecurity.CurrentUserId);

            var flag = QueryHelper.UpdateFields("Sale_PurchaseOrder", field_value, Id.Value);
            if (flag == true)
                return Json(new { status = "success", id = Id, fieldName = fieldName, value = value }, JsonRequestBehavior.AllowGet);

            return Json(new { status = "error", id = Id, fieldName = fieldName, value = value }, JsonRequestBehavior.AllowGet);
        }

        #endregion


        public PartialViewResult LoadProductItem(int OrderNo, int ProductId, string ProductName, string Unit, int Quantity, decimal? Price, string ProductCode, string ProductType, int QuantityInventory)
        {
            var model = new PurchaseOrderDetailViewModel();
            model.OrderNo = OrderNo;
            model.ProductId = ProductId;
            model.ProductName = ProductName;
            model.Unit = Unit;
            model.Quantity = Quantity;
            model.Price = Price;
            model.ProductCode = ProductCode;
            model.QuantityInInventory = QuantityInventory;
            model.DisCount = 0;
            model.DisCountAmount = 0;
            return PartialView(model);
        }

        #region Create

        public ActionResult Create(int? Id)
        {
            PurchaseOrderViewModel model = new PurchaseOrderViewModel();
            model.DetailList = new List<PurchaseOrderDetailViewModel>();
            if (Id.HasValue && Id > 0)
            {
                var purchaseOrder = PurchaseOrderRepository.GetPurchaseOrderById(Id.Value);

                //Nếu đã ghi sổ rồi thì không được sửa
                if (purchaseOrder.IsArchive.Value)
                {
                    return RedirectToAction("Detail", new { Id = purchaseOrder.Id });
                }

                //Kiểm tra xem nếu có xuất kho rồi thì return
                var checkProductInbound = productInboundRepository.GetAllProductInbound()
                        .Where(item => item.PurchaseOrderId == purchaseOrder.Id).FirstOrDefault();
                if (checkProductInbound != null)
                {
                    return RedirectToAction("Detail", new { Id = purchaseOrder.Id });
                }

                AutoMapper.Mapper.Map(purchaseOrder, model);

                var detailList = PurchaseOrderRepository.GetAllOrderDetailsByOrderId(purchaseOrder.Id).ToList();
                AutoMapper.Mapper.Map(detailList, model.DetailList);
            }
            //var saleDepartmentCode = Erp.BackOffice.Helpers.Common.GetSetting("SaleDepartmentCode");

            var productList = productRepository.GetAllvwProduct().Where(x => x.Type == "product")
              .Select(item => new ProductViewModel
              {
                  Type = item.Type,
                  Code = item.Code,
                  Barcode = item.Barcode,
                  Name = item.Name,
                  Id = item.Id,
                  CategoryCode = item.CategoryCode,
                  PriceOutbound = item.PriceOutbound,
                  Unit = item.Unit,
                  QuantityTotalInventory = item.QuantityTotalInventory == null ? 0 : item.QuantityTotalInventory,
                  Image_Name = item.Image_Name,
                  ProductGroup = item.ProductGroup
              }).ToList();
            ViewBag.productList = productList;
            //Thêm số lượng tồn kho cho chi tiết đơn hàng đã được thêm
            if (model.DetailList != null && model.DetailList.Count > 0)
            {
                foreach (var item in model.DetailList)
                {
                    var product = productList.Where(i => i.Id == item.ProductId).FirstOrDefault();
                    if (product != null)
                    {
                        //   item.QuantityInInventory = product.;
                        item.ProductCode = product.Code;
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

            var warehouseList = WarehouseRepository.GetvwAllWarehouse().AsEnumerable();
            var user = userRepository.GetByvwUserName(Helpers.Common.CurrentUser.UserName);
            List<string> listKeeperID = new List<string>();
            //if (!string.IsNullOrEmpty(Erp.BackOffice.Helpers.Common.CurrentUser.WarehouseId))
            //{
            //    listKeeperID = Erp.BackOffice.Helpers.Common.CurrentUser.WarehouseId.Split(',').ToList();
            //}
            //warehouseList = warehouseList.Where(id1 => listKeeperID.Any(id2 => id2 == id1.Value));
            if (!Erp.BackOffice.Filters.SecurityFilter.IsAdmin() && !Erp.BackOffice.Filters.SecurityFilter.IsKeToan())
            {
                warehouseList = warehouseList.Where(x => ("," + user.WarehouseId + ",").Contains("," + x.Id + ",") == true);
            }
            var _warehouseList = warehouseList.Select(item => new SelectListItem
            {
                Text = item.Name + " (" + item.BranchName + ")",
                Value = item.Id.ToString()
            });
            ViewBag.warehouseList = _warehouseList;
            model.CreatedUserName = Erp.BackOffice.Helpers.Common.CurrentUser.FullName;
            return View(model);
        }


        [HttpPost]
        public ActionResult Create(PurchaseOrderViewModel model)
        {
            if (ModelState.IsValid && model.DetailList.Count != 0)
            {
                PurchaseOrder purchaseOrder = null;
                if (model.Id > 0)
                {
                    purchaseOrder = PurchaseOrderRepository.GetPurchaseOrderById(model.Id);
                }
                using (var scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        if (purchaseOrder != null)
                        {
                            //Nếu đã ghi sổ rồi thì không được sửa
                            if (purchaseOrder.IsArchive.Value)
                            {
                                return RedirectToAction("Detail", new { Id = purchaseOrder.Id });
                            }

                            //Kiểm tra xem nếu có xuất kho rồi thì return
                            var checkProductInbound = productInboundRepository.GetAllProductInbound()
                               .Where(item => item.PurchaseOrderId == purchaseOrder.Id).FirstOrDefault();
                            if (checkProductInbound != null)
                            {
                                return RedirectToAction("Detail", new { Id = purchaseOrder.Id });
                            }

                            AutoMapper.Mapper.Map(model, purchaseOrder);
                        }
                        else
                        {
                            purchaseOrder = new PurchaseOrder();
                            AutoMapper.Mapper.Map(model, purchaseOrder);
                            purchaseOrder.IsDeleted = false;
                            purchaseOrder.CreatedUserId = WebSecurity.CurrentUserId;
                            purchaseOrder.CreatedDate = DateTime.Now;
                            purchaseOrder.Status = Wording.OrderStatus_pending;
                            //purchaseOrder.BranchId = Helpers.Common.CurrentUser.BranchId;
                            purchaseOrder.IsArchive = false;
                        }
                        //Duyệt qua danh sách sản phẩm mới xử lý tình huống user chọn 2 sản phầm cùng id
                        List<PurchaseOrderDetail> listNewCheckSameId = new List<PurchaseOrderDetail>();
                        foreach (var group in model.DetailList)
                        {
                            var product = productRepository.GetProductById(group.ProductId.Value);
                            listNewCheckSameId.Add(new PurchaseOrderDetail
                            {
                                ProductId = product.Id,
                                Quantity = group.Quantity,
                                Unit = product.Unit,
                                Price = group.Price,
                                IsDeleted = false,
                                CreatedUserId = WebSecurity.CurrentUserId,
                                ModifiedUserId = WebSecurity.CurrentUserId,
                                CreatedDate = DateTime.Now,
                                ModifiedDate = DateTime.Now,
                                DisCount = group.DisCount
                            });
                        }
                        //Tính lại chiết khấu
                        foreach (var item in listNewCheckSameId)
                        {
                            var thanh_tien = item.Quantity * item.Price;
                            item.DisCountAmount = Convert.ToInt32(item.DisCount * thanh_tien / 100);
                        }

                        purchaseOrder.TotalAmount = listNewCheckSameId.Sum(item => item.Quantity * item.Price - item.DisCountAmount);
                        purchaseOrder.ModifiedUserId = WebSecurity.CurrentUserId;
                        purchaseOrder.ModifiedDate = DateTime.Now;
                        purchaseOrder.PaidAmount = 0;
                        purchaseOrder.RemainingAmount = purchaseOrder.TotalAmount;

                        if (model.Id > 0)
                        {
                            PurchaseOrderRepository.UpdatePurchaseOrder(purchaseOrder);
                            var listDetail = PurchaseOrderRepository.GetAllOrderDetailsByOrderId(purchaseOrder.Id).AsEnumerable();
                            PurchaseOrderRepository.DeletePurchaseOrderDetail(listDetail);

                            foreach (var item in listNewCheckSameId)
                            {
                                item.PurchaseOrderId = purchaseOrder.Id;
                                PurchaseOrderRepository.InsertPurchaseOrderDetail(item);
                            }

                            //Thêm vào quản lý chứng từ
                            TransactionController.Create(new TransactionViewModel
                            {
                                TransactionModule = "PurchaseOrder",
                                TransactionCode = purchaseOrder.Code,
                                TransactionName = "Hóa đơn mua hàng"
                            });
                        }
                        else
                        {
                            PurchaseOrderRepository.InsertPurchaseOrder(purchaseOrder, listNewCheckSameId);

                            //Cập nhật lại mã hóa đơn
                            string prefix = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_PurchaseOrder");
                            purchaseOrder.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix, purchaseOrder.Id);
                            PurchaseOrderRepository.UpdatePurchaseOrder(purchaseOrder);

                            //Thêm vào quản lý chứng từ
                            TransactionController.Create(new TransactionViewModel
                            {
                                TransactionModule = "PurchaseOrder",
                                TransactionCode = purchaseOrder.Code,
                                TransactionName = "Hóa đơn mua hàng"
                            });



                        }
                        scope.Complete();
                    }
                    catch (DbUpdateException)
                    {
                        TempData[Globals.FailedMessageKey] = App_GlobalResources.Wording.Error;
                        return RedirectToAction("Detail", new { Id = purchaseOrder.Id });
                    }
                }
                return RedirectToAction("Detail", new { Id = purchaseOrder.Id });

            }
            return RedirectToAction("Index");
        }

        public ViewResult SearchProductInvoice(int? WarehouseDestinationId)
        {
            List<ProductViewModel> model = new List<ProductViewModel>();
            if (WarehouseDestinationId != null && WarehouseDestinationId > 0)
            {
                var warehouse = WarehouseRepository.GetWarehouseById(WarehouseDestinationId.Value);
                List<string> listCategories = new List<string>();
                if (!string.IsNullOrEmpty(warehouse.Categories))
                {
                    listCategories = warehouse.Categories.Split(',').ToList();
                }
                var productList = productRepository.GetAllvwProduct()
                    .Select(item => new ProductViewModel
                    {
                        Type = item.Type,
                        Code = item.Code,
                        Barcode = item.Barcode,
                        Name = item.Name,
                        Id = item.Id,
                        CategoryCode = item.CategoryCode,
                        PriceInbound = item.PriceInbound,
                        Unit = item.Unit,
                        QuantityTotalInventory = item.QuantityTotalInventory == null ? 0 : item.QuantityTotalInventory,
                        Image_Name = item.Image_Name,
                        ProductGroup = item.ProductGroup
                    }).ToList();
                model = productList.Where(id1 => listCategories.Any(id2 => id1.ProductGroup != null && id1.ProductGroup.Contains(id2.ToString()))).ToList();
            }
            //ViewBag.productList = list_inbound_2;
            return View(model);
        }
        #endregion

        #region Detail
        public ActionResult Detail(int? Id, string TransactionCode)
        {
            var purchaseOrder = new vwPurchaseOrder();
            if (Id != null && Id.Value > 0)
            {
                purchaseOrder = PurchaseOrderRepository.GetvwPurchaseOrderById(Id.Value);
            }

            if (!string.IsNullOrEmpty(TransactionCode))
            {
                purchaseOrder = PurchaseOrderRepository.GetvwPurchaseOrderByCode(TransactionCode);
            }

            if (purchaseOrder == null)
            {
                return RedirectToAction("Index");
            }

            var model = new PurchaseOrderViewModel();
            AutoMapper.Mapper.Map(purchaseOrder, model);

            model.PaymentViewModel = new PaymentViewModel();
            model.NextPaymentDate_Temp = DateTime.Now.AddDays(30);
            model.PaymentViewModel.Name = "Mua hàng - Trả tiền mặt";
            model.PaymentViewModel.Amount = purchaseOrder.TotalAmount;

            //Lấy thông tin kiểm tra cho phép sửa chứng từ này hay không
            model.AllowEdit = Helpers.Common.KiemTraNgaySuaChungTu(model.CreatedDate.Value);

            //Lấy lịch sử giao dịch thanh toán
            var listTransaction = transactionLiabilitiesRepository.GetAllvwTransaction()
                        .Where(item => item.MaChungTuGoc == purchaseOrder.Code)
                        .OrderByDescending(item => item.CreatedDate)
                        .ToList();

            model.ListTransactionLiabilities = new List<TransactionLiabilitiesViewModel>();
            AutoMapper.Mapper.Map(listTransaction, model.ListTransactionLiabilities);

            model.Code = purchaseOrder.Code;
            //model.SalerId = purchaseOrder.SalerId;
            //model.SalerName = purchaseOrder.SalerFullName;
            model.CreatedUserName = Helpers.Common.CurrentUser.FullName;
            //var saleDepartmentCode = Erp.BackOffice.Helpers.Common.GetSetting("SaleDepartmentCode");

            //Lấy danh sách chi tiết đơn hàng
            model.DetailList = PurchaseOrderRepository.GetvwAllOrderDetailsByOrderId(purchaseOrder.Id).Select(x =>
                new PurchaseOrderDetailViewModel
                {
                    Id = x.Id,
                    Price = x.Price,
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    Unit = x.Unit,
                    DisCount = x.DisCount,
                    DisCountAmount = x.DisCountAmount,
                    CategoryCode = x.CategoryCode,
                    ProductName = x.ProductName,
                    ProductCode = x.ProductCode,
                    PurchaseOrderId = x.PurchaseOrderId,
                    Amount = x.Amount,
                    IsArchive = x.IsArchive,
                    Description = x.Description,
                    Manufacturer = x.Manufacturer,
                    ProductGroup = x.ProductGroup,
                    PurchaseOrderCode = x.PurchaseOrderCode,
                    PurchaseOrderDate = x.PurchaseOrderDate,
                    SupplierId = x.SupplierId,
                    SupplierName = x.SupplierName
                }).OrderBy(x => x.Id).ToList();

            model.GroupProduct = model.DetailList.GroupBy(x => new { x.CategoryCode }, (key, group) => new PurchaseOrderDetailViewModel
            {
                CategoryCode = key.CategoryCode,
                ProductId = group.FirstOrDefault().ProductId,
                Id = group.FirstOrDefault().Id
            }).OrderBy(item => item.CategoryCode).ToList();

            //Lấy thông tin phiếu nhập kho
            if (purchaseOrder.ProductInboundId != null && purchaseOrder.ProductInboundId > 0)
            {
                var ProductInbound = productInboundRepository.GetvwProductInboundById(purchaseOrder.ProductInboundId.Value);
                model.ProductInboundViewModel = new ProductInboundViewModel();
                AutoMapper.Mapper.Map(ProductInbound, model.ProductInboundViewModel);
            }

            //Lấy danh sách chứng từ liên quan
            model.ListTransactionRelationship = new List<TransactionRelationshipViewModel>();
            var listTransactionRelationship = transactionRepository.GetAllvwTransactionRelationship()
                .Where(item => item.TransactionA == purchaseOrder.Code
                || item.TransactionB == purchaseOrder.Code).OrderByDescending(item => item.CreatedDate)
                .ToList();

            AutoMapper.Mapper.Map(listTransactionRelationship, model.ListTransactionRelationship);

            //int taxfee = 0;
            //int.TryParse(Helpers.Common.GetSetting("vat"), out taxfee);
            //model.TaxFee = taxfee;

            ViewBag.isAdmin = Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId == 1 ? true : false;

            model.ModifiedUserName = userRepository.GetUserById(model.ModifiedUserId.Value).FullName;

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(model);
        }
        #endregion

        #region Update
        [HttpPost]
        public ActionResult Update(int PurchaseOrderDetailId, int Discount)
        {
            var purchaseOrderDetail = PurchaseOrderRepository.GetPurchaseOrderDetailById(PurchaseOrderDetailId);
            purchaseOrderDetail.DisCount = Discount;
            var thanh_tien = purchaseOrderDetail.Quantity * purchaseOrderDetail.Price;
            purchaseOrderDetail.DisCountAmount = Convert.ToInt32(purchaseOrderDetail.DisCount * thanh_tien / 100);
            PurchaseOrderRepository.UpdatePurchaseOrderDetail(purchaseOrderDetail);

            var purchaseOrder = PurchaseOrderRepository.GetPurchaseOrderById(purchaseOrderDetail.PurchaseOrderId.Value);
            purchaseOrder.TotalAmount = PurchaseOrderRepository.GetvwAllOrderDetailsByOrderId(purchaseOrder.Id).Sum(item => (item.Price * item.Quantity) - item.DisCountAmount);
            PurchaseOrderRepository.UpdatePurchaseOrder(purchaseOrder);
            return Content("success");
        }
        #endregion

        #region Archive
        [HttpPost]
        public ActionResult Archive(PurchaseOrderViewModel model)
        {
            //if (Request["Submit"] == "Save")
            //{
            var purchaseOrder = PurchaseOrderRepository.GetPurchaseOrderById(model.Id);

            //Kiểm tra cho phép sửa chứng từ này hay không
            if (Helpers.Common.KiemTraNgaySuaChungTu(purchaseOrder.CreatedDate.Value) == false)
            {
                return RedirectToAction("Detail", new { Id = purchaseOrder.Id });
            }

            //Coi thử đã xuất kho chưa mới cho ghi sổ
            bool hasProductOutbound = productInboundRepository.GetAllvwProductInbound().Any(item => item.PurchaseOrderId == purchaseOrder.Id);

            if (!hasProductOutbound)
            {
                TempData[Globals.FailedMessageKey] = "Chưa lập phiếu nhập kho!";
                return RedirectToAction("Detail", new { Id = purchaseOrder.Id });
            }

            if (!purchaseOrder.IsArchive.Value)
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        //Cập nhật thông tin thanh toán cho đơn hàng
                        purchaseOrder.PaymentMethod = model.PaymentViewModel.PaymentMethod;
                        purchaseOrder.PaidAmount = Convert.ToDecimal(model.PaymentViewModel.Amount);
                        purchaseOrder.RemainingAmount = purchaseOrder.TotalAmount - purchaseOrder.PaidAmount;
                        purchaseOrder.NextPaymentDate = model.NextPaymentDate_Temp;

                        purchaseOrder.ModifiedDate = DateTime.Now;
                        purchaseOrder.ModifiedUserId = WebSecurity.CurrentUserId;
                        PurchaseOrderRepository.UpdatePurchaseOrder(purchaseOrder);

                        ////Tạo chiết khấu cho nhân viên nếu có
                        //CommisionSaleController.Create(productInvoice.Id, productInvoice.TotalAmount);

                        //Lấy mã KH
                        var supplier = SupplierRepository.GetSupplierById(purchaseOrder.SupplierId.Value);

                        var remain = purchaseOrder.TotalAmount.Value - Convert.ToDecimal(model.PaymentViewModel.Amount.Value);
                        if (remain > 0)
                        {

                        }
                        else
                        {
                            purchaseOrder.NextPaymentDate = null;
                            model.NextPaymentDate_Temp = null;
                        }

                        //Ghi Nợ TK 131 - Phải trả cho nhà cung cấp(tổng giá thanh toán)
                        Erp.BackOffice.Account.Controllers.TransactionLiabilitiesController.Create(
                            purchaseOrder.Code,
                            "PurchaseOrder",
                            "Hóa đơn mua hàng",
                            supplier.Code,
                            "Supplier",
                            purchaseOrder.TotalAmount.Value,
                            0,
                            purchaseOrder.Code,
                            "PurchaseOrder",
                            null,
                            model.NextPaymentDate_Temp,
                            null);

                        //Khách thanh toán ngay
                        if (model.PaymentViewModel.Amount > 0)
                        {
                            //Lập phiếu thu
                            var payment = new Payment();
                            AutoMapper.Mapper.Map(model.PaymentViewModel, payment);
                            payment.IsDeleted = false;
                            payment.CreatedUserId = WebSecurity.CurrentUserId;
                            payment.ModifiedUserId = WebSecurity.CurrentUserId;
                            payment.AssignedUserId = WebSecurity.CurrentUserId;
                            payment.CreatedDate = DateTime.Now;
                            payment.ModifiedDate = DateTime.Now;
                            payment.VoucherDate = DateTime.Now;
                            payment.TargetId = supplier.Id;
                            payment.TargetName = "Supplier";
                            payment.Receiver = supplier.CompanyName;
                            payment.PaymentMethod = purchaseOrder.PaymentMethod;
                            payment.Address = supplier.Address;
                            //payment.MaChungTuGoc = purchaseOrder.Code;
                            //payment.LoaiChungTuGoc = "PurchaseOrder";
                            payment.Note = payment.Name;

                            if (payment.Amount > purchaseOrder.TotalAmount)
                                payment.Amount = purchaseOrder.TotalAmount;

                            paymentRepository.InsertPayment(payment);

                            var prefixPayment = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_PaymentSupplier");
                            payment.Code = Erp.BackOffice.Helpers.Common.GetCode(prefixPayment, payment.Id);
                            paymentRepository.UpdatePayment(payment);
                            //Lập chi tiết phiếu thu

                            var paymentDetail = new PaymentDetail();
                            paymentDetail.IsDeleted = false;
                            paymentDetail.CreatedUserId = WebSecurity.CurrentUserId;
                            paymentDetail.ModifiedUserId = WebSecurity.CurrentUserId;
                            paymentDetail.AssignedUserId = WebSecurity.CurrentUserId;
                            paymentDetail.CreatedDate = DateTime.Now;
                            paymentDetail.ModifiedDate = DateTime.Now;

                            paymentDetail.Name = "Trả tiền cho nhà cung cấp";
                            paymentDetail.Amount = payment.Amount;
                            paymentDetail.PaymentId = payment.Id;
                            paymentDetail.MaChungTuGoc = purchaseOrder.Code;
                            paymentDetail.LoaiChungTuGoc = "PurchaseOrder";

                            paymentDetailRepository.InsertPaymentDetail(paymentDetail);
                            //Thêm vào quản lý chứng từ
                            TransactionController.Create(new TransactionViewModel
                            {
                                TransactionModule = "Payment",
                                TransactionCode = payment.Code,
                                TransactionName = "Trả tiền cho nhà cung cấp"
                            });

                            //Thêm chứng từ liên quan
                            TransactionController.CreateRelationship(new TransactionRelationshipViewModel
                            {
                                TransactionA = payment.Code,
                                TransactionB = purchaseOrder.Code
                            });

                            //Ghi Có TK 131 - Phải thu của khách hàng.
                            Erp.BackOffice.Account.Controllers.TransactionLiabilitiesController.Create(
                                payment.Code,
                                "Payment",
                                "Trả tiền cho nhà cung cấp",
                                supplier.Code,
                                "Supplier",
                                0,
                                Convert.ToDecimal(model.PaymentViewModel.Amount),
                                purchaseOrder.Code,
                                "PurchaseOrder",
                                model.PaymentViewModel.PaymentMethod,
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
            purchaseOrder.ModifiedUserId = WebSecurity.CurrentUserId;
            purchaseOrder.ModifiedDate = DateTime.Now;
            purchaseOrder.IsArchive = true;
            purchaseOrder.Status = Wording.OrderStatus_complete;
            PurchaseOrderRepository.UpdatePurchaseOrder(purchaseOrder);
            TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.ArchiveSuccess;

            //Cảnh báo cập nhật phiếu xuất kho
            if (hasProductOutbound)
            {
                TempData[Globals.SuccessMessageKey] += "<br/>Đơn hàng này đã được nhập vào kho! Vui lòng kiểm tra lại chứng từ nhập kho để tránh sai xót dữ liệu!";
            }
            //}

            return RedirectToAction("Detail", new { Id = model.Id });
        }
        #endregion

        #region UnArchive
        [HttpPost]
        public ActionResult UnArchive(int Id)
        {
            //if (Request["Submit"] == "Save")
            //{
            var purchaseOrder = PurchaseOrderRepository.GetPurchaseOrderById(Id);

            //Kiểm tra cho phép sửa chứng từ này hay không
            if (Helpers.Common.KiemTraNgaySuaChungTu(purchaseOrder.CreatedDate.Value) == false)
            {
                TempData[Globals.FailedMessageKey] = "Quá hạn sửa chứng từ!";
            }
            else
            {
                //Kiểm tra nếu có phiếu thu rồi thì không cho bỏ ghi sổ
                var payment = paymentRepository.GetAllvwPayment()
                    .Where(item => item.MaChungTuGoc == purchaseOrder.Code).FirstOrDefault();

                if (payment != null)
                {
                    TempData[Globals.FailedMessageKey] = "Đơn hàng đã phát sinh phiếu chi!";
                }
                else
                {
                    using (var scope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        try
                        {
                            //Xóa lịch sử giao dịch
                            var listTransaction = transactionLiabilitiesRepository.GetAllTransaction()
                            .Where(item => item.MaChungTuGoc == purchaseOrder.Code)
                            .Select(item => item.Id)
                            .ToList();

                            foreach (var item in listTransaction)
                            {
                                transactionLiabilitiesRepository.DeleteTransaction(item);
                            }

                            purchaseOrder.PaidAmount = 0;
                            purchaseOrder.RemainingAmount = purchaseOrder.TotalAmount;
                            purchaseOrder.NextPaymentDate = null;

                            purchaseOrder.ModifiedUserId = WebSecurity.CurrentUserId;
                            purchaseOrder.ModifiedDate = DateTime.Now;
                            purchaseOrder.IsArchive = false;
                            purchaseOrder.Status = Wording.OrderStatus_inprogress;
                            PurchaseOrderRepository.UpdatePurchaseOrder(purchaseOrder);
                            TempData[Globals.SuccessMessageKey] = "Đã bỏ ghi sổ";
                            scope.Complete();
                        }
                        catch (DbUpdateException)
                        {
                            return Content("Fail");
                        }
                    }
                }
            }
            //}

            return RedirectToAction("Detail", new { Id = Id });
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
            var purchaseOrder = PurchaseOrderRepository.GetvwPurchaseOrderById(Id);
            //lấy thông tin khách hàng
            var supplier = SupplierRepository.GetAllvwSupplier().Where(x => x.Code == purchaseOrder.SupplierCode).FirstOrDefault();
            //lấy người lập phiếu xuất kho
            var user = userRepository.GetUserById(purchaseOrder.CreatedUserId.Value);
            List<ProductInvoiceDetailViewModel> detailList = new List<ProductInvoiceDetailViewModel>();
            if (purchaseOrder != null && purchaseOrder.IsDeleted != true)
            {
                //lấy danh sách sản phẩm xuất kho
                detailList = PurchaseOrderRepository.GetvwAllOrderDetailsByOrderId(Id)
                        .Select(x => new ProductInvoiceDetailViewModel
                        {
                            Id = x.Id,
                            Price = x.Price,
                            ProductId = x.ProductId,
                            ProductName = x.ProductName,
                            ProductCode = x.ProductCode,
                            Quantity = x.Quantity,
                            Unit = x.Unit,
                            FixedDiscount = x.DisCount.HasValue ? x.DisCount.Value : 0,
                            FixedDiscountAmount = x.DisCountAmount.HasValue ? x.DisCountAmount : 0,
                            ProductGroup = x.ProductGroup,
                            Description = x.Description
                        }).ToList();
            }
            if (purchaseOrder.ProductInboundId != null && purchaseOrder.ProductInboundId.Value > 0)
            {
                var ProductInbound = productInboundRepository.GetvwProductInboundById(purchaseOrder.ProductInboundId.Value);
                var listProductInboundDetail = productInboundRepository.GetAllvwProductInboundDetailByInboundId(ProductInbound.Id).ToList();
                foreach (var item in detailList)
                {
                    foreach (var i in listProductInboundDetail.Where(x => x.ProductId == item.ProductId))
                    {
                        var locationItem = warehouseLocationItemRepository.GetAllLocationItem().Where(x => x.ProductInboundDetailId == i.Id && x.ProductId == i.ProductId).OrderBy(x => x.ExpiryDate).FirstOrDefault();
                        if (locationItem != null)
                        {
                            item.ExpiryDate = locationItem.ExpiryDate;
                            item.LoCode = locationItem.LoCode;
                        }
                    }

                }
            }
            //tạo dòng của table html danh sách sản phẩm.
            var ListRow = "";
            int tong_tien = 0;
            int da_thanh_toan = 0;
            int con_lai = 0;
            var groupProduct = detailList.GroupBy(x => new { x.ProductGroup }, (key, group) => new PurchaseOrderDetailViewModel
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
                //var chiet_khau1 = count.Sum(x => x.DisCountAmount.HasValue ? x.DisCountAmount.Value : 0);
                decimal? subTotal1 = count.Sum(x => (x.Quantity) * (x.Price));
                var thanh_tien1 = subTotal1;//- chiet_khau1;
                if (!string.IsNullOrEmpty(i.ProductGroup))
                {
                    ProductGroupName = categoryRepository.GetCategoryByCode("Categories_product").Where(x => x.Value == i.ProductGroup).FirstOrDefault();

                    Rows = "<tr style=\"background:#eee;font-weight:bold\"><td colspan=\"6\" class=\"text-left\">" + (i.ProductGroup == null ? "" : i.ProductGroup) + ": " + (ProductGroupName.Name == null ? "" : ProductGroupName.Name) + "</td><td class=\"text-right\">"
                         + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(count.Sum(x => x.Quantity), null)
                         + "</td><td colspan=\"3\" class=\"text-right\"></td><td class=\"text-right\">"
                         + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(thanh_tien1, null)
                         + "</td></tr>";
                }
                ListRow += Rows;
                int index = 1;
                foreach (var item in detailList.Where(x => x.ProductGroup == i.ProductGroup))
                {
                    decimal? subTotal = item.Quantity * item.Price.Value;
                    var chiet_khau = item.FixedDiscountAmount.HasValue ? item.FixedDiscountAmount.Value : 0;
                    var thanh_tien = subTotal - chiet_khau;

                    var Row = "<tr>"
                     + "<td class=\"text-center\">" + (index++) + "</td>"
                     + "<td class=\"text-right\">" + item.ProductCode + "</td>"
                        //+ "<td class=\"text-left\">" + item.ProductName + "<p><em>" + item.Description + "</em></p>" + (item.CheckPromotion == true ? " (Khuyến mãi)" : "") + "</td>"
                     + "<td class=\"text-left\">" + item.ProductName + " " + Helpers.Common.StripHTML(item.Description) + (item.CheckPromotion == true ? " (Khuyến mãi)" : "") + "</td>"
                     + "<td class=\"text-center\">" + (item.LoCode == null ? "" : item.LoCode) + "</td>"
                     + "<td class=\"text-center\">" + (item.ExpiryDate == null ? "" : item.ExpiryDate.Value.ToShortDateString()) + "</td>"
                     + "<td class=\"text-center\">" + item.Unit + "</td>"
                     + "<td class=\"text-right\">" + Erp.BackOffice.Helpers.Common.PhanCachHangNgan(item.Quantity) + "</td>"
                     + "<td class=\"text-right\">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(item.Price, null) + "</td>"
                     + "<td class=\"text-right\">" + (item.FixedDiscount.HasValue ? item.FixedDiscount.Value : 0) + "</td>"
                     + "<td class=\"text-right\">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(chiet_khau, null) + "</td>"
                     + "<td class=\"text-right\">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(thanh_tien, null) + "</td></tr>";
                    ListRow += Row;
                }
            }

            //khởi tạo table html.                
            var table = "<table class=\"invoice-detail\"><thead><tr> <th>STT</th> <th>Mã hàng</th><th>Tên mặt hàng</th><th>Lô sản xuất</th><th>Hạn dùng</th><th>ĐVT</th><th>Số lượng</th><th>Đơn giá</th><th>% CK</th><th>Trị giá chiết khấu</th><th>Thành tiền</th></tr></thead><tbody>"
                         + ListRow
                //+ "<tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>"
                //+ "<tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>"
                         + "</tbody><tfoot>"
                         + "<tr><td colspan=\"6\" class=\"text-right\"></td><td class=\"text-right\">"
                         + Erp.BackOffice.Helpers.Common.PhanCachHangNgan(detailList.Sum(x => x.Quantity))
                         + "</td><td colspan=\"3\" class=\"text-right\">Tổng cộng</td><td class=\"text-right\">"
                         + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(purchaseOrder.TotalAmount, null)
                         + "</td></tr>"
                //  + "<tr><td colspan=\"10\" class=\"text-right\">VAT (" + vat + "%)</td><td class=\"text-right\">"
                //+ Erp.BackOffice.Helpers.Common.PhanCachHangNgan(totalVAT)
                //+ "</td></tr>"
                // + "<tr><td colspan=\"10\" class=\"text-right\">Tổng tiền phải thanh toán</td><td class=\"text-right\">"
                //+ Erp.BackOffice.Helpers.Common.PhanCachHangNgan(total)
                //+ "</td></tr>"
                          + "<tr><td colspan=\"10\" class=\"text-right\">Đã thanh toán</td><td class=\"text-right\">"
                          + (purchaseOrder.PaidAmount != null && purchaseOrder.PaidAmount.Value > 0 ? Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(purchaseOrder.PaidAmount, null) : "0")
                         + "</td></tr>"
                          + "<tr><td colspan=\"10\" class=\"text-right\">Còn lại phải trả</td><td class=\"text-right\">"
                         + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(purchaseOrder.TotalAmount.Value - (purchaseOrder.PaidAmount != null && purchaseOrder.PaidAmount.Value > 0 ? purchaseOrder.PaidAmount.Value : 0), null)
                         + "</td></tr>"
                         + "</tfoot></table>";

            //lấy template phiếu xuất.
            var template = templatePrintRepository.GetAllTemplatePrint().Where(x => x.Code == "PurchaseOrder").OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            //truyền dữ liệu vào template.
            model.Content = template.Content;
            model.Content = model.Content.Replace("{Code}", purchaseOrder.Code);
            model.Content = model.Content.Replace("{Day}", purchaseOrder.CreatedDate.Value.Day.ToString());
            model.Content = model.Content.Replace("{Month}", purchaseOrder.CreatedDate.Value.Month.ToString());
            model.Content = model.Content.Replace("{Year}", purchaseOrder.CreatedDate.Value.Year.ToString());
            model.Content = model.Content.Replace("{SupplierName}", supplier.Name);
            model.Content = model.Content.Replace("{SupplierPhone}", supplier.Phone);
            model.Content = model.Content.Replace("{CompanyName}", supplier.CompanyName);

            if (!string.IsNullOrEmpty(supplier.Address))
            {
                model.Content = model.Content.Replace("{Address}", supplier.Address + ", ");
            }
            else
            {
                model.Content = model.Content.Replace("{Address}", "");
            }
            if (!string.IsNullOrEmpty(supplier.DistrictName))
            {
                model.Content = model.Content.Replace("{District}", supplier.DistrictName + ", ");
            }
            else
            {
                model.Content = model.Content.Replace("{District}", "");
            }
            if (!string.IsNullOrEmpty(supplier.WardName))
            {
                model.Content = model.Content.Replace("{Ward}", supplier.WardName + ", ");
            }
            else
            {
                model.Content = model.Content.Replace("{Ward}", "");
            }
            if (!string.IsNullOrEmpty(supplier.ProvinceName))
            {
                model.Content = model.Content.Replace("{Province}", supplier.ProvinceName);
            }
            else
            {
                model.Content = model.Content.Replace("{Province}", "");
            }

            model.Content = model.Content.Replace("{Note}", purchaseOrder.Note);
            model.Content = model.Content.Replace("{PurchaseOrderCode}", purchaseOrder.Code);
            model.Content = model.Content.Replace("{SaleName}", user.FullName);
            model.Content = model.Content.Replace("{PaymentMethod}", purchaseOrder.PaymentMethod);
            model.Content = model.Content.Replace("{MoneyText}", Erp.BackOffice.Helpers.Common.ChuyenSoThanhChu_2(purchaseOrder.TotalAmount.ToString()));

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
                Response.AppendHeader("content-disposition", "attachment;filename=" + purchaseOrder.CreatedDate.Value.ToString("yyyyMMdd") + purchaseOrder.Code + ".xls");
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
