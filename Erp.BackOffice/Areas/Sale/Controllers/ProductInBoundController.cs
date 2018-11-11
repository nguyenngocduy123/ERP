using System.Globalization;
using Erp.BackOffice.Sale.Models;
using Erp.BackOffice.Filters;
using Erp.Domain.Sale.Entities;
using Erp.Domain.Interfaces;
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
using Erp.BackOffice.Account.Models;
using Erp.BackOffice.Account.Controllers;
using Erp.Domain.Account.Entities;
using Erp.Domain.Account.Interfaces;
using Erp.Domain.Sale.Repositories;
using Erp.Domain.Staff.Interfaces;
using GenCode128;
using System.Drawing;
using System.IO;
using System.Transactions;

namespace Erp.BackOffice.Sale.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class ProductInboundController : Controller
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly IWarehouseRepository WarehouseRepository;
        private readonly IWarehouseLocationItemRepository WarehouseLocationItemRepository;
        private readonly IInventoryRepository InventoryRepository;
        private readonly IPurchaseOrderRepository PurchaseOrderRepository;
        private readonly IProductOrServiceRepository ProductRepository;
        private readonly IProductInboundRepository ProductInboundRepository;
        private readonly IProductOutboundRepository ProductOutboundRepository;
        private readonly ISupplierRepository SupplierRepository;
        private readonly IUserRepository userRepository;
        private readonly IPaymentRepository paymentRepository;
        private readonly IQueryHelper QueryHelper;
        private readonly ITemplatePrintRepository templatePrintRepository;
        private readonly IRequestInboundRepository requestInboundRepository;
        private readonly IProductDamagedRepository productDamagesRepository;
        private readonly IBranchRepository branchRepository;
        private readonly ISalesReturnsRepository saleReturnRepository;
        public ProductInboundController(
              ITransactionRepository _transaction
            , IWarehouseRepository _Warehouse
            , IWarehouseLocationItemRepository _WarehouseLocationItem
            , IInventoryRepository _Inventory
            , IPurchaseOrderRepository _PurchaseOrder
            , IProductOrServiceRepository _Product
            , IProductInboundRepository _ProductInbound
            , IProductOutboundRepository _ProductOutbound
            , ISupplierRepository _Supplier
            , IUserRepository _user
            , IQueryHelper _QueryHelper
            , IPaymentRepository payment
            , ITemplatePrintRepository _templatePrint
            , IRequestInboundRepository requestInbound
            , IProductDamagedRepository productdamages
               , IBranchRepository branch
            , ISalesReturnsRepository saleReturn
            )
        {
            transactionRepository = _transaction;
            WarehouseRepository = _Warehouse;
            WarehouseLocationItemRepository = _WarehouseLocationItem;
            InventoryRepository = _Inventory;
            PurchaseOrderRepository = _PurchaseOrder;
            ProductRepository = _Product;
            ProductInboundRepository = _ProductInbound;
            ProductOutboundRepository = _ProductOutbound;
            SupplierRepository = _Supplier;
            userRepository = _user;
            QueryHelper = _QueryHelper;
            paymentRepository = payment;
            templatePrintRepository = _templatePrint;
            requestInboundRepository = requestInbound;
            productDamagesRepository = productdamages;
            branchRepository = branch;
            saleReturnRepository = saleReturn;
        }

        #region Index
        public ViewResult Index(string txtCode, string txtMinAmount, string txtMaxAmount, string txtPurchaseOrder, int? warehouseDestinationId, string txtWarehouseSource, string startDate, string endDate, string txtProductCode, int? supplierId, string Status)
        {
            var user = userRepository.GetByvwUserName(Helpers.Common.CurrentUser.UserName);
            IEnumerable<ProductInboundViewModel> q = ProductInboundRepository.GetAllvwProductInboundFull()
                //.Where(x => x.BranchId == Helpers.Common.CurrentUser.BranchId)
                .Select(item => new ProductInboundViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    ModifiedDate = item.ModifiedDate,
                    Code = item.Code,
                    TotalAmount = item.TotalAmount,
                    WarehouseDestinationName = item.WarehouseDestinationName,
                    IsDone = item.IsDone,
                    PurchaseOrderId = item.PurchaseOrderId,
                    SupplierId = item.SupplierId,
                    SupplierName = item.SupplierName,
                    WarehouseDestinationId = item.WarehouseDestinationId,
                    Note = item.Note,
                    IsArchive = item.IsArchive,
                    IsDeleted = item.IsDeleted,
                    BranchId = item.BranchId
                }).OrderByDescending(m => m.Id);

            //Tìm những phiếu nhập có chứa mã sp
            if (!string.IsNullOrEmpty(txtProductCode))
            {
                txtProductCode = txtProductCode.Trim();
                var productListId = ProductRepository.GetAllvwProduct()
                    .Where(item => item.Code == txtProductCode).Select(item => item.Id).ToList();

                if (productListId.Count > 0)
                {
                    List<int> listProductInboundId = new List<int>();
                    foreach (var id in productListId)
                    {
                        var list = ProductInboundRepository.GetAllvwProductInboundDetailByProductId(id)
                            .Select(item => item.ProductInboundId.Value).Distinct().ToList();

                        listProductInboundId.AddRange(list);
                    }

                    q = q.Where(item => listProductInboundId.Contains(item.Id));
                }
            }

            if (string.IsNullOrEmpty(txtCode) == false || string.IsNullOrEmpty(txtPurchaseOrder) == false || warehouseDestinationId != null || string.IsNullOrEmpty(txtWarehouseSource) == false)
            {
                txtCode = txtCode == "" ? "~" : txtCode.ToLower();

                txtWarehouseSource = txtWarehouseSource == "" ? "~" : Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(txtWarehouseSource);
                txtPurchaseOrder = txtPurchaseOrder == "" ? "~" : txtPurchaseOrder.ToLower();

                q = q.Where(x => x.Code.ToLowerOrEmpty().Contains(txtCode)
                    //|| x.WarehouseDestinationId == warehouseDestinationId
                    || x.PurchaseOrderCode.ToLowerOrEmpty().Contains(txtPurchaseOrder)
                    || Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(x.SupplierName).Contains(txtWarehouseSource));
            }
            if (warehouseDestinationId != null && warehouseDestinationId.Value > 0)
            {
                q = q.Where(x => x.WarehouseDestinationId == warehouseDestinationId);
            }

            if (!Erp.BackOffice.Filters.SecurityFilter.IsAdmin() && !Erp.BackOffice.Filters.SecurityFilter.IsKeToan() && !Erp.BackOffice.Filters.SecurityFilter.IsQLKhoTong())
            {
                q = q.Where(x => ("," + user.DrugStore + ",").Contains("," + x.BranchId + ",") == true);
            }
            if (supplierId != null && supplierId.Value > 0)
            {
                q = q.Where(x => x.SupplierId == supplierId);
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
                if (maxAmount > 0)
                {
                    q = q.Where(x => x.TotalAmount <= maxAmount);
                }
            }
            if (!string.IsNullOrEmpty(Status))
            {
                if (Status == "success")
                {
                    q = q.Where(x => x.IsArchive == true);
                }
                else if (Status == "pending")
                {
                    q = q.Where(x => x.IsArchive == false);
                }
                else if (Status == "delete")
                {
                    q = q.Where(x => x.IsDeleted == true);
                }
            }
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        #endregion

        #region LoadProductItem
        public PartialViewResult LoadProductItem(int OrderNo, int ProductId, string ProductName, string Unit, int Quantity, decimal Price, string ProductCode, string ProductType, string LoCode, string ExpiryDate)
        {
            var model = new ProductInboundDetailViewModel();
            model.OrderNo = OrderNo;
            model.ProductId = ProductId;
            model.ProductName = ProductName;
            model.Unit = Unit;
            model.Quantity = Quantity;
            model.Price = Price;
            model.ProductCode = ProductCode;
            model.ProductType = ProductType;
            model.LoCode = LoCode;
            if (!string.IsNullOrEmpty(ExpiryDate))
                model.ExpiryDate = Convert.ToDateTime(ExpiryDate);
            return PartialView(model);
        }
        #endregion

        #region Create
        public ActionResult Create(int? PurchaseOrderId)
        {

            var model = new ProductInboundViewModel();
            model.CreatedDate = DateTime.Now;
            model.DetailList = new List<ProductInboundDetailViewModel>();
            var user = userRepository.GetByvwUserName(Helpers.Common.CurrentUser.UserName);
            if (PurchaseOrderId != null && PurchaseOrderId.Value > 0)
            {
                model.PurchaseOrderId = PurchaseOrderId;
                var aaa = PurchaseOrderRepository.GetvwPurchaseOrderById(PurchaseOrderId.Value);
                model.PurchaseOrderId = aaa.Id;
                model.WarehouseDestinationId = aaa.WarehouseDestinationId;
                model.SupplierId = aaa.SupplierId;
                model.PurchaseOrderCode = aaa.Code;
                var listProductInvoiceDetail = PurchaseOrderRepository.GetvwAllOrderDetailsByOrderId(PurchaseOrderId.Value).Select(i => new
                {
                    Amount = i.Amount,
                    DisCountAmount = i.DisCountAmount,
                    IsArchive = i.IsArchive,
                    CategoryCode = i.CategoryCode,
                    DisCount = i.DisCount,
                    Description = i.Description,
                    Manufacturer = i.Manufacturer,
                    Price = i.Price,
                    ProductCode = i.ProductCode,
                    ProductGroup = i.ProductGroup,
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    ProductId = i.ProductId
                }).OrderByDescending(m => m.ProductCode).ToList();

                //Tạo danh sách chi tiết phiếu xuất tương ứng                    
                foreach (var item in listProductInvoiceDetail)
                {
                    var productOutboundDetailViewModel = model.DetailList.Where(i => i.ProductId == item.ProductId).FirstOrDefault();
                    if (productOutboundDetailViewModel == null)
                    {
                        productOutboundDetailViewModel = new ProductInboundDetailViewModel();
                        productOutboundDetailViewModel.ProductId = item.ProductId;
                        productOutboundDetailViewModel.ProductCode = item.ProductCode;
                        productOutboundDetailViewModel.ProductName = item.ProductName;
                        productOutboundDetailViewModel.Quantity = item.Quantity;
                        productOutboundDetailViewModel.Price = item.Price;
                        model.DetailList.Add(productOutboundDetailViewModel);
                    }
                    else
                    {
                        productOutboundDetailViewModel.Quantity += item.Quantity;
                    }
                }

                int n = 0;
                foreach (var item in model.DetailList)
                {
                    item.OrderNo = n;
                    n++;
                }
            }


            var orderList = PurchaseOrderRepository.GetAllPurchaseOrder().AsEnumerable()
                .Where(x => ("," + user.DrugStore + ",").Contains("," + x.BranchId + ",") == true
                    && x.SupplierId != null
                    && (x.Status == Wording.OrderStatus_pending || x.Status == Wording.OrderStatus_shipping))
               .Select(item => new PurchaseOrderViewModel
               {
                   Code = item.Code,
                   Id = item.Id,
                   WarehouseDestinationId = item.WarehouseDestinationId,
                   SupplierId = item.SupplierId
               });
            ViewBag.orderList = orderList;

            var supplierList = SupplierRepository.GetAllSupplier().AsEnumerable()
               .Select(item => new SelectListItem
               {
                   Text = item.Name,
                   Value = item.Id.ToString()
               });
            ViewBag.supplierList = supplierList;

            var productList = ProductRepository.GetAllvwProduct()
               .Select(item => new ProductViewModel
               {
                   Code = item.Code,
                   Barcode = item.Barcode,
                   Name = item.Name,
                   Id = item.Id,
                   CategoryCode = item.CategoryCode,
                   PriceInbound = item.PriceInbound,
                   Unit = item.Unit,
                   Image_Name = item.Image_Name
               });
            ViewBag.productList = productList;

            model.CreatedDate = DateTime.Now;
            model.CreatedUserName = Helpers.Common.CurrentUser.FullName;
            model.PaymentViewModel = new PaymentViewModel();
            model.NextPaymentDate = DateTime.Now.AddDays(30);
            //model.PaymentViewModel.Name = TransactionController.TransactionType.ProductInbound.GetName();

            ViewBag.isAdmin = Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId == 1 ? true : false;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ProductInboundViewModel model)
        {
            ProductInbound productInbound = null;

            if (model.PurchaseOrderId != null)
            {
                productInbound = ProductInboundRepository.GetAllProductInbound()
                    .Where(item => item.PurchaseOrderId == model.PurchaseOrderId).FirstOrDefault();

                if (productInbound != null && productInbound.IsArchive)
                    return Content("Phiếu nhập kho cho đơn hàng này đã được ghi sổ!");
            }
            if (ModelState.IsValid)
            {
                var ProductInbound = new Domain.Sale.Entities.ProductInbound();
                using (var scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        AutoMapper.Mapper.Map(model, ProductInbound);
                        ProductInbound.IsDeleted = false;
                        ProductInbound.CreatedUserId = WebSecurity.CurrentUserId;
                        ProductInbound.ModifiedUserId = WebSecurity.CurrentUserId;
                        ProductInbound.CreatedDate = DateTime.Now;
                        ProductInbound.ModifiedDate = DateTime.Now;
                        var branch = WarehouseRepository.GetWarehouseById(ProductInbound.WarehouseDestinationId.Value);
                        ProductInbound.BranchId = branch.BranchId;

                        //duyệt qua danh sách sản phẩm mới xử lý tình huống user chọn 2 sản phầm cùng id
                        List<Domain.Sale.Entities.ProductInboundDetail> listNewCheckSameId = new List<Domain.Sale.Entities.ProductInboundDetail>();
                        foreach (var group in model.DetailList)
                        {
                            //var product = ProductRepository.GetProductById(group.Key.Value);

                            listNewCheckSameId.Add(new Domain.Sale.Entities.ProductInboundDetail
                            {
                                ProductId = group.ProductId,
                                Quantity = group.Quantity,
                                Unit = group.Unit,
                                Price = group.Price,
                                IsDeleted = false,
                                CreatedUserId = WebSecurity.CurrentUserId,
                                ModifiedUserId = WebSecurity.CurrentUserId,
                                CreatedDate = DateTime.Now,
                                ModifiedDate = DateTime.Now,
                                ExpiryDate = group.ExpiryDate,
                                LoCode = group.LoCode
                            });
                        }

                        ProductInbound.TotalAmount = listNewCheckSameId.Sum(x => x.Price * x.Quantity);

                        ////lấy thông tin đơn đặt hàng nếu có chọn
                        var order = new PurchaseOrder();
                        //var orderDetails = new List<PurchaseOrderDetail>();
                        if (model.PurchaseOrderId != null)
                        {
                            order = PurchaseOrderRepository.GetPurchaseOrderById(model.PurchaseOrderId.Value);
                        }
                        ProductInbound.Type = (order.Id == 0 ? "manual" : (order.SupplierId != null ? "external" : "internal"));
                        //thêm mới phiếu nhập và chi tiết phiếu nhập
                        ProductInboundRepository.InsertProductInbound(ProductInbound);
                        //cập nhật thông tin đơn nhập hàng nếu có chọn
                        if (model.PurchaseOrderId != null)
                        {
                            //Cập nhật hóa đơn là đang xử lý
                            var purchaseOrder = PurchaseOrderRepository.GetPurchaseOrderById(model.PurchaseOrderId.Value);
                            purchaseOrder.Status = Wording.OrderStatus_inprogress;
                            purchaseOrder.ModifiedDate = DateTime.Now;
                            purchaseOrder.ModifiedUserId = WebSecurity.CurrentUserId;
                            purchaseOrder.ProductInboundId = ProductInbound.Id;
                            PurchaseOrderRepository.UpdatePurchaseOrder(purchaseOrder);
                        }
                        //cập nhật lại mã nhập kho
                        string prefix = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_Inbound");
                        ProductInbound.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix, ProductInbound.Id);
                        ProductInboundRepository.UpdateProductInbound(ProductInbound);

                        //Thêm chi tiết phiếu nhập
                        foreach (var item in listNewCheckSameId)
                        {
                            item.ProductInboundId = ProductInbound.Id;
                            ProductInboundRepository.InsertProductInboundDetail(item);
                        }

                        //Thêm vào quản lý chứng từ
                        TransactionController.Create(new TransactionViewModel
                        {
                            TransactionModule = "ProductInbound",
                            TransactionCode = ProductInbound.Code,
                            TransactionName = "Nhập kho"
                        });
                        scope.Complete();
                    }
                    catch (DbUpdateException)
                    {
                        TempData[Globals.FailedMessageKey] = App_GlobalResources.Wording.Error;
                        return View(model);
                    }
                }
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Index", new { Id = ProductInbound.Id });
            }
            return View(model);
        }
        #endregion


        public ActionResult Edit(int? Id, string TransactionCode)
        {
            var ProductInbound = new vwProductInbound();
            if (Id != null)
                ProductInbound = ProductInboundRepository.GetvwProductInboundById(Id.Value);
            if (!string.IsNullOrEmpty(TransactionCode))
                ProductInbound = ProductInboundRepository.GetvwProductInboundByTransactionCode(TransactionCode);

            //  var ProductInbound = ProductInboundRepository.GetvwProductInboundById(Id.Value);
            if (ProductInbound != null && ProductInbound.IsDeleted != true)
            {
                //Nếu đã ghi sổ rồi thì không được sửa
                if (ProductInbound.IsArchive)
                {
                    return RedirectToAction("Detail", new { Id = ProductInbound.Id });
                }

                var model = new ProductInboundViewModel();

                AutoMapper.Mapper.Map(ProductInbound, model);

                //lấy thông tin mã đơn hàng nếu có và kho đích đến theo mã đơn hàng
                if (model.PurchaseOrderId != null)
                {
                    var purchaseOrder = PurchaseOrderRepository.GetPurchaseOrderById(model.PurchaseOrderId.Value);
                    if (purchaseOrder != null)
                    {
                        model.PurchaseOrderCode = purchaseOrder != null ? purchaseOrder.Code : "";
                        var warehouseDestination = WarehouseRepository.GetWarehouseById(purchaseOrder.WarehouseDestinationId.Value);
                        model.WarehouseDestinationName = warehouseDestination != null ? warehouseDestination.Name : "";
                    }
                }

                // lấy danh sách detail
                var Details = ProductInboundRepository.GetAllProductInboundDetailByInboundId(ProductInbound.Id)
                    .Select(x => new ProductInboundDetailViewModel
                    {
                        Id = x.Id,
                        Price = x.Price,
                        ProductId = x.ProductId,
                        ProductInboundId = x.ProductInboundId,
                        Quantity = x.Quantity,
                        ProductCode = "",
                        ExpiryDate = x.ExpiryDate,
                        LoCode = x.LoCode
                    }).ToList();
                model.DetailList = Details;

                foreach (var item in Details)
                {
                    var product = ProductRepository.GetProductById(item.ProductId.Value);
                    item.ProductName = product != null ? product.Name : "";
                    item.ProductCode = product != null ? product.Code : "";
                    var usedQuantity = WarehouseLocationItemRepository.GetAllLocationItem().Where(x => x.ProductId == item.ProductId && x.ProductInboundId == item.ProductInboundId && x.IsOut == true).ToList().Count();
                    item.QuantityUsed = usedQuantity;
                }

                model.CreatedUserName = userRepository.GetUserById(model.CreatedUserId.Value).FullName;
                return View(model);
            }

            return View();
        }

        [HttpPost]
        public ActionResult Edit(ProductInboundViewModel model)
        {

            var productInbound = ProductInboundRepository.GetProductInboundById(model.Id);
            decimal total = 0;
            using (var scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    foreach (var item in model.DetailList)
                    {
                        var productInboundDetail = ProductInboundRepository.GetProductInboundDetailById(item.Id);
                        if (productInboundDetail != null)
                        {
                            //int first = productInboundDetail.Quantity.Value;
                            int last = item.Quantity.Value;
                            //int qIn = 0;
                            //int qOut = 0;
                            //int qLocationIn = 0;
                            //int qLocationOut = 0;

                            productInboundDetail.ExpiryDate = item.ExpiryDate;
                            productInboundDetail.LoCode = item.LoCode;
                            productInboundDetail.Price = item.Price;
                            productInboundDetail.Quantity = last;

                            ProductInboundRepository.UpdateProductInboundDetail(productInboundDetail);
                            var amount = productInboundDetail.Price * productInboundDetail.Quantity;
                            total += (amount.HasValue ? amount.Value : 0);
                        }
                    }

                    productInbound.Note = model.Note;
                    productInbound.TotalAmount = total;
                    productInbound.CreatedStaffId = model.CreatedStaffId;
                    ProductInboundRepository.UpdateProductInbound(productInbound);
                    scope.Complete();
                }
                catch (DbUpdateException)
                {
                    TempData[Globals.FailedMessageKey] = App_GlobalResources.Wording.Error;
                    return View(model);
                }
            }
            return RedirectToAction("Detail", new { Id = model.Id });
        }

        #region Detail
        public ActionResult Detail(int? Id, string TransactionCode)
        {
            var ProductInbound = new vwProductInbound();
            if (Id != null)
                ProductInbound = ProductInboundRepository.GetvwProductInboundFullById(Id.Value);
            if (!string.IsNullOrEmpty(TransactionCode))
                ProductInbound = ProductInboundRepository.GetvwProductInboundByTransactionCode(TransactionCode);

            if (ProductInbound != null)
            {
                var model = new ProductInboundViewModel();

                AutoMapper.Mapper.Map(ProductInbound, model);
                if (model.PurchaseOrderId != null)
                {
                    var purchase = PurchaseOrderRepository.GetPurchaseOrderById(model.PurchaseOrderId.Value);
                    model.PurchaseOrderCode = purchase.Code;
                }
                //Kiểm tra cho phép sửa chứng từ này hay không
                model.AllowEdit = Helpers.Common.KiemTraNgaySuaChungTu(model.CreatedDate.Value);

                // lấy danh sách detail
                var Details = ProductInboundRepository.GetAllvwProductInboundDetailByInboundId(ProductInbound.Id)
                    .Select(x => new ProductInboundDetailViewModel
                    {
                        Id = x.Id,
                        Price = x.Price,
                        ProductId = x.ProductId,
                        ProductInboundId = x.ProductInboundId,
                        Quantity = x.Quantity,
                        ProductCode = "",
                        ExpiryDate = x.ExpiryDate,
                        LoCode = x.LoCode,
                        ProductDamagedId = x.ProductDamagedId,
                        Reason = x.Reason,
                        NumberAmount = x.NumberAmount,
                        Status = x.Status
                    }).OrderBy(x => x.Id).ToList();
                model.DetailList = Details;

                foreach (var item in Details)
                {
                    var product = ProductRepository.GetProductById(item.ProductId.Value);
                    item.ProductName = product != null ? product.Name : "";
                    item.ProductCode = product != null ? product.Code : "";
                    var usedQuantity = WarehouseLocationItemRepository.GetAllLocationItem().Where(x => x.ProductId == item.ProductId && x.ProductInboundId == item.ProductInboundId && x.IsOut == true).ToList().Count();
                    item.QuantityUsed = usedQuantity;
                }

                model.CreatedUserName = userRepository.GetUserById(model.CreatedUserId.Value).FullName;

                ViewBag.SuccessMessage = TempData["SuccessMessage"];
                ViewBag.FailedMessage = TempData["FailedMessage"];
                return View(model);
            }

            return View();
        }
        #endregion

        #region Archive
        [HttpPost]
        public ActionResult Archive(int Id, bool? IsPopup)
        {
            if (Request["Submit"] == "Save")
            {
                var productInbound = ProductInboundRepository.GetProductInboundById(Id);

                //Kiểm tra cho phép sửa chứng từ này hay không
                if (Helpers.Common.KiemTraNgaySuaChungTu(productInbound.CreatedDate.Value) == false)
                {
                    return RedirectToAction("Detail", new { Id = productInbound.Id });
                }
                using (var scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        Archive(productInbound);
                        TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.ArchiveSuccess;
                        var request = requestInboundRepository.GetAllRequestInbound().Where(x => x.InboundId == productInbound.Id);
                        if (request.Count() > 0)
                        {
                            var item = request.FirstOrDefault();

                            //cập nhật lại yêu cầu nhập kho
                            item.Status = "inbound_complete";
                            requestInboundRepository.UpdateRequestInbound(item);
                            var qq = ProductOutboundRepository.GetProductOutboundById(item.OutboundId.Value);
                            //gửi notifications cho người được phân quyền.
                            Crm.Controllers.ProcessController.Run("RequestInbound", "inbound_complete", item.Id, qq.CreatedUserId, null, item);
                        }
                        scope.Complete();
                    }
                    catch (DbUpdateException)
                    {
                        TempData[Globals.FailedMessageKey] = App_GlobalResources.Wording.Error;
                    }
                }
            }
            if (IsPopup != null && IsPopup == true)
            {
                return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
            }
            else
            {
                return RedirectToAction("Detail", new { Id = Id });
            }

        }

        public static void Archive(ProductInbound productInbound)
        {
            ProductInboundRepository ProductInboundRepository = new Domain.Sale.Repositories.ProductInboundRepository(new Domain.Sale.ErpSaleDbContext());
            //Cập nhật lại tồn kho cho những sp trong phiếu nhập này
            var detailList = ProductInboundRepository.GetAllvwProductInboundDetailByInboundId(productInbound.Id)
                .Select(item => new
                {
                    ProductName = item.ProductCode + " - " + item.ProductName,
                    ProductId = item.ProductId.Value,
                    Quantity = item.Quantity.Value,
                    LoCode = item.LoCode,
                    ExpiryDate = item.ExpiryDate
                }).ToList();

            //Kiểm tra dữ liệu sau khi bỏ ghi sổ có hợp lệ không
            string check = "";
            foreach (var item in detailList.Where(x => x.Quantity > 0))
            {
                var error = InventoryController.Check(item.ProductName, item.ProductId, item.LoCode, item.ExpiryDate, productInbound.WarehouseDestinationId.Value, item.Quantity, 0);
                check += error;
            }

            if (string.IsNullOrEmpty(check))
            {
                //Khi đã hợp lệ thì mới update
                foreach (var item in detailList.Where(x => x.Quantity > 0))
                {
                    InventoryController.Update(item.ProductName, item.ProductId, item.LoCode, item.ExpiryDate, productInbound.WarehouseDestinationId.Value, item.Quantity, 0);
                }

                productInbound.IsArchive = true;
                ProductInboundRepository.UpdateProductInbound(productInbound);
            }
            //else
            //{
            //    TempData[Globals.FailedMessageKey] = App_GlobalResources.Wording.ArchiveFail + check;
            //}
        }
        #endregion

        #region UnArchive
        [HttpPost]
        public ActionResult UnArchive(int Id, bool? IsPopup)
        {
            if (Request["Submit"] == "Save")
            {
                var productInbound = ProductInboundRepository.GetProductInboundById(Id);

                //Kiểm tra cho phép sửa chứng từ này hay không
                if (Helpers.Common.KiemTraNgaySuaChungTu(productInbound.CreatedDate.Value) == false)
                {
                    return RedirectToAction("Detail", new { Id = productInbound.Id });
                }
                using (var scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        //Cập nhật lại tồn kho cho những sp trong phiếu nhập này
                        var detailList = ProductInboundRepository.GetAllvwProductInboundDetailByInboundId(productInbound.Id)
                            .Select(item => new
                            {
                                ProductName = item.ProductCode + " - " + item.ProductName,
                                ProductId = item.ProductId.Value,
                                Quantity = item.Quantity.Value,
                                LoCode = item.LoCode,
                                ExpiryDate = item.ExpiryDate
                            }).ToList();

                        //Kiểm tra dữ liệu sau khi bỏ ghi sổ có hợp lệ không
                        string check = "";
                        foreach (var item in detailList.Where(x => x.Quantity > 0))
                        {
                            var error = InventoryController.Check(item.ProductName, item.ProductId, item.LoCode, item.ExpiryDate, productInbound.WarehouseDestinationId.Value, 0, item.Quantity);
                            check += error;
                        }

                        if (string.IsNullOrEmpty(check))
                        {
                            //Khi đã hợp lệ thì mới update
                            foreach (var item in detailList.Where(x => x.Quantity > 0))
                            {
                                InventoryController.Update(item.ProductName, item.ProductId, item.LoCode, item.ExpiryDate, productInbound.WarehouseDestinationId.Value, 0, item.Quantity);
                            }

                            productInbound.IsArchive = false;
                            ProductInboundRepository.UpdateProductInbound(productInbound);
                            TempData[Globals.SuccessMessageKey] = "Đã bỏ ghi sổ";
                        }
                        else
                        {
                            TempData[Globals.FailedMessageKey] = App_GlobalResources.Wording.ArchiveFail + check;
                        }
                        scope.Complete();
                    }
                    catch (DbUpdateException)
                    {
                        return Content("Fail");
                    }
                }
            }
            if (IsPopup != null && IsPopup == true)
            {
                return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
            }
            else
            {
                return RedirectToAction("Detail", new { Id = Id });
            }
        }

        public static void UnArchiveAndDelete(ProductInbound productInbound)
        {
            ProductInboundRepository ProductInboundRepository = new Domain.Sale.Repositories.ProductInboundRepository(new Domain.Sale.ErpSaleDbContext());
            //Cập nhật lại tồn kho cho những sp trong phiếu nhập này
            var detailList = ProductInboundRepository.GetAllvwProductInboundDetailByInboundId(productInbound.Id)
                .Select(item => new
                {
                    ProductName = item.ProductCode + " - " + item.ProductName,
                    ProductId = item.ProductId.Value,
                    Quantity = item.Quantity.Value,
                    LoCode = item.LoCode,
                    ExpiryDate = item.ExpiryDate
                }).ToList();

            //Kiểm tra dữ liệu sau khi bỏ ghi sổ có hợp lệ không
            string check = "";
            foreach (var item in detailList.Where(x => x.Quantity > 0))
            {
                var error = InventoryController.Check(item.ProductName, item.ProductId, item.LoCode, item.ExpiryDate, productInbound.WarehouseDestinationId.Value, 0, item.Quantity);
                check += error;
            }

            if (string.IsNullOrEmpty(check))
            {
                //Khi đã hợp lệ thì mới update
                foreach (var item in detailList.Where(x => x.Quantity > 0))
                {
                    InventoryController.Update(item.ProductName, item.ProductId, item.LoCode, item.ExpiryDate, productInbound.WarehouseDestinationId.Value, 0, item.Quantity);
                }

                productInbound.IsArchive = false;
                productInbound.IsDeleted = true;
                ProductInboundRepository.UpdateProductInbound(productInbound);
                //TempData[Globals.SuccessMessageKey] = "Đã bỏ ghi sổ";
            }
            else
            {
                //TempData[Globals.FailedMessageKey] = App_GlobalResources.Wording.ArchiveFail + check;
            }
        }
        #endregion

        #region UpdateLocationItem
        public ActionResult UpdateLocationItem(int Id)
        {
            ProductInboundViewModel model = new ProductInboundViewModel();
            var item = ProductInboundRepository.GetAllvwProductInbound().Where(x => x.Id == Id).FirstOrDefault();
            if (item != null)
            {
                AutoMapper.Mapper.Map(item, model);
                model.CreatedUserName = userRepository.GetUserById(model.CreatedUserId.Value).FullName;

                var detailList = ProductInboundRepository.GetAllvwProductInboundDetailByInboundId(item.Id);
                model.DetailList = detailList.Select(x => new ProductInboundDetailViewModel
                {
                    Id = x.Id,
                    Price = x.Price,
                    ProductId = x.ProductId,
                    ProductInboundId = x.ProductInboundId,
                    ProductName = x.ProductName,
                    Quantity = x.Quantity,
                    Unit = x.Unit,
                    ProductCode = x.ProductCode
                }).ToList();

                foreach (var i in model.DetailList)
                {
                    i.ListWarehouseLocationItemViewModel = new List<WarehouseLocationItemViewModel>();
                    var listLocationItemExits = WarehouseLocationItemRepository.GetAllWarehouseLocationItem().Where(q => q.ProductInboundDetailId == i.Id).ToList();
                    AutoMapper.Mapper.Map(listLocationItemExits, i.ListWarehouseLocationItemViewModel);
                }

                //foreach(var g in listLocationItemExits.GroupBy(x => x.ProductId))
                //{
                //    int qty = g.Count();
                //    var itemDetail = model.DetailList.Where(x => x.ProductId == g.FirstOrDefault().ProductId).FirstOrDefault();
                //    if (itemDetail.Quantity == qty)
                //    {
                //        model.DetailList.RemoveAll(x => x.ProductId == g.FirstOrDefault().ProductId);
                //    }
                //    else
                //    {
                //        model.DetailList.Where(x => x.ProductId == g.FirstOrDefault().ProductId).FirstOrDefault().Quantity = itemDetail.Quantity - qty;
                //    }
                //}
            }

            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];

            if (model.DetailList.Count == 0)
            {
                TempData[Globals.FailedMessageKey] = "Danh sách sản phẩm trong phiếu nhập [" + model.Code + "] đã được nhập vị trí đầy đủ.";
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateLocationItem(InboundLocationItemsViewModel model)
        {
            //var list = model.LocationItemList
            //    .Where(item => string.IsNullOrEmpty(item.Shelf)
            //        || string.IsNullOrEmpty(item.Floor)
            //        || item.ExpiryDate == null
            //    );
            foreach (var item in model.LocationItemList)
            {
                var warehouseLocationItem = WarehouseLocationItemRepository.GetWarehouseLocationItemById(item.Id.Value);
                warehouseLocationItem.Shelf = item.Shelf;
                warehouseLocationItem.Floor = item.Floor;
                warehouseLocationItem.ExpiryDate = item.ExpiryDate;
                warehouseLocationItem.LoCode = item.LoCode;
                if (!string.IsNullOrEmpty(item.LoCode) && item.ExpiryDate != null)
                {
                    warehouseLocationItem.SN = item.ProductCode + item.LoCode + item.ExpiryDate.Value.ToString("yyyyMMdd") + warehouseLocationItem.Id;
                }
                WarehouseLocationItemRepository.UpdateWarehouseLocationItem(warehouseLocationItem);
            }

            //WarehouseLocationItemRepository.InsertWarehouseLocationItem(list);

            //TempData[Globals.SuccessMessageKey] = "Thêm thành công " + list.Count + " sản phẩm có dữ liệu vị trí.";
            return Redirect("/ProductInbound/Detail/" + model.ProductInboundId);

            //TempData[Globals.FailedMessageKey] = "Bạn chưa điền dữ liệu vị trí cho tất cả dòng.";
            //return RedirectToAction("UpdateLocationItem", new { Id = model.ProductInboundId });
        }
        #endregion

        //#region Print
        //public ActionResult Print(int? Id)
        //{
        //    var ProductInbound = ProductInboundRepository.GetvwProductInboundById(Id.Value);
        //    var logo = Erp.BackOffice.Helpers.Common.GetSetting("LogoCompany");
        //    var ImgLogo = "<div class=\"logo\"><img src=" + logo + " /></div>";
        //    var company = Erp.BackOffice.Helpers.Common.GetSetting("companyName");
        //    var address = Erp.BackOffice.Helpers.Common.GetSetting("addresscompany");
        //    var phone = Erp.BackOffice.Helpers.Common.GetSetting("phonecompany");
        //    var fax = Erp.BackOffice.Helpers.Common.GetSetting("faxcompany");
        //    var bankcode = Erp.BackOffice.Helpers.Common.GetSetting("bankcode");
        //    var bankname = Erp.BackOffice.Helpers.Common.GetSetting("bankname");
        //    if (ProductInbound != null && ProductInbound.IsDeleted != true)
        //    {
        //        var model = new TemplatePrintViewModel();
        //        var user = userRepository.GetUserById(ProductInbound.CreatedUserId.Value);
        //        var inboundDetails = ProductInboundRepository.GetAllProductInboundDetailByInboundId(Id.Value).AsEnumerable()
        //                .Select(x => new ProductInboundDetailViewModel
        //                {
        //                    Id = x.Id,
        //                    Price = x.Price,
        //                    ProductId = x.ProductId,
        //                    ProductName = ProductRepository.GetProductById(x.ProductId.Value).Name,
        //                    ProductCode = ProductRepository.GetProductById(x.ProductId.Value).Code,
        //                    ProductInboundId = x.ProductInboundId,
        //                    Quantity = x.Quantity,
        //                    Unit = x.Unit

        //                }).ToList();
        //        var ListRow = "";
        //        foreach (var item in inboundDetails)
        //        {
        //            decimal? subTotal = item.Quantity * item.Price.Value;
        //            var Row = "<tr>"
        //             + "<td class=\"text-center\">" + (inboundDetails.ToList().IndexOf(item) + 1) + "</td>"
        //             + "<td>" + item.ProductCode + "</td>"
        //             + "<td>" + item.ProductName + "</td>"
        //             + "<td class=\"text-center\">" + item.Unit + "</td>"
        //             + "<td class=\"text-right\">" + item.Quantity + "</td>"
        //             + "<td class=\"text-right\">" + Erp.BackOffice.Helpers.Common.PhanCachHangNgan(item.Price) + "</td>"
        //             + "<td class=\"text-right\">" + Erp.BackOffice.Helpers.Common.PhanCachHangNgan(subTotal) + "</td></tr>";
        //            ListRow += Row;
        //        }
        //        var style = "<style>.invoice-detail{ width:100%;margin-top: 10px;border-spacing: 0px;}"
        //            + ".invoice-detail th{border: 1px solid #000;border-right: none;padding: 5px;}"
        //            + " .invoice-detail tr th:last-child {border-right: 1px solid #000;}"
        //            + " .invoice-detail td{padding: 5px 5px; border-bottom: 1px solid #000; border-left: 1px solid #000; height: 15px;font-size: 12px;}"
        //            + " .invoice-detail tr td:last-child {border-right: 1px solid #000;}"
        //            + ".invoice-detail tbody tr:last-child td {border-bottom: 1px solid #000;}"
        //            + " .invoice-detail tfoot td{font-weight:bold;border-bottom: 1px solid #000;}"
        //            + " .invoice-detail tfoot tr:first-child td{border-top: 1px solid #000;}"
        //            + ".text-center{text-align:center;}"
        //            + ".text-right{text-align:right;}"
        //            + " .logo{ width: 100px;float: left; margin: 0 0px;height: 100px;line-height: 100px;}"
        //            + ".logo img{ width:100%;vertical-align: middle;}"
        //            + "</style>";
        //        var table = style + "<table class=\"invoice-detail\"><thead><tr> <th>STT</th> <th>Mã hàng</th><th>Tên mặt hàng</th><th>ĐVT</th><th>Số lượng</th><th>Đơn giá</th><th>Thành tiền</th></tr></thead><tbody>"
        //                     + ListRow
        //                     + "</tbody> <tfoot><tr><td colspan=\"6\" class=\"text-center\">Tổng cộng</td><td class=\"text-right\">"
        //                     + Erp.BackOffice.Helpers.Common.PhanCachHangNgan(ProductInbound.TotalAmount.Value)
        //                     + "</td></tr></tfoot></table>";
        //        var template = templatePrintRepository.GetAllTemplatePrint().Where(x => x.Code.Contains("ProductInbound")).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
        //        model.Content = template.Content;
        //        model.Content = model.Content.Replace("{Code}", ProductInbound.Code);
        //        model.Content = model.Content.Replace("{Day}", ProductInbound.CreatedDate.Value.Day.ToString());
        //        model.Content = model.Content.Replace("{Month}", ProductInbound.CreatedDate.Value.Month.ToString());
        //        model.Content = model.Content.Replace("{Year}", ProductInbound.CreatedDate.Value.Year.ToString());
        //        model.Content = model.Content.Replace("{ShipperName}", ProductInbound.ShipperName);
        //        model.Content = model.Content.Replace("{Phone}", ProductInbound.ShipperPhone);
        //        model.Content = model.Content.Replace("{Supplier}", ProductInbound.SupplierName);
        //        model.Content = model.Content.Replace("{DataTable}", table);
        //        model.Content = model.Content.Replace("{Logo}", ImgLogo);
        //        model.Content = model.Content.Replace("{System.CompanyName}", company);
        //        model.Content = model.Content.Replace("{System.AddressCompany}", address);
        //        model.Content = model.Content.Replace("{System.PhoneCompany}", phone);
        //        model.Content = model.Content.Replace("{System.Fax}", fax);
        //        model.Content = model.Content.Replace("{System.BankCodeCompany}", bankcode);
        //        model.Content = model.Content.Replace("{System.BankNameCompany}", bankname);
        //        return View(model);
        //    }

        //    if (Request.UrlReferrer != null)
        //        return Redirect(Request.UrlReferrer.AbsoluteUri);
        //    return RedirectToAction("Index");
        //}
        //#endregion

        #region UpdateData
        public ActionResult UpdateData()
        {
            var productInboundDetail = ProductInboundRepository.GetAllvwProductInboundDetail();
            foreach (var item in productInboundDetail)
            {
                var locationItem = WarehouseLocationItemRepository.GetAllLocationItem().Where(x => x.ProductId == item.ProductId && x.ProductInboundDetailId == item.Id).ToList();

                for (int i = 0; i < locationItem.Count(); i++)
                {
                    locationItem[i].ExpiryDate = item.ExpiryDate;
                    locationItem[i].LoCode = item.LoCode;
                    if (!string.IsNullOrEmpty(item.LoCode) && item.ExpiryDate.HasValue)
                    {
                        locationItem[i].SN = item.ProductCode + item.LoCode + item.ExpiryDate.Value.ToString("yyyyMMdd") + locationItem[i].Id;
                    }
                    WarehouseLocationItemRepository.UpdateWarehouseLocationItem(locationItem[i]);
                }
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region CreateFromOutbound
        public ActionResult CreateFromOutbound(int? Id)
        {
            var ProductInbound = new vwProductOutbound();
            var Request = new vwRequestInbound();
            if (Id != null)
                Request = requestInboundRepository.GetvwRequestInboundById(Id.Value);
            ProductInbound = ProductOutboundRepository.GetvwProductOutboundById(Request.OutboundId.Value);
            if (ProductInbound != null && ProductInbound.IsDeleted != true)
            {

                var model = new ProductInboundViewModel();
                //AutoMapper.Mapper.Map(ProductInbound, model);
                // lấy danh sách detail
                var Details = ProductOutboundRepository.GetAllvwProductOutboundDetailByOutboundId(ProductInbound.Id)
                    .Select(x => new ProductInboundDetailViewModel
                    {
                        Id = 0,
                        Price = x.Price,
                        ProductId = x.ProductId,
                        //ProductInboundId = x.ProductInboundId,
                        Quantity = x.Quantity,
                        ProductCode = x.ProductCode,
                        ProductName = x.ProductName,
                        Unit = x.Unit,
                        ExpiryDate = x.ExpiryDate,
                        LoCode = x.LoCode,
                        QuantityTest = x.Quantity
                    }).ToList();
                model.DetailList = Details;
                model.WarehouseDestinationId = ProductInbound.WarehouseDestinationId;
                model.CreatedDate = DateTime.Now;
                model.CreatedUserName = Helpers.Common.CurrentUser.FullName;
                model.ShipperName = Request.ShipName;
                model.ShipperPhone = Request.ShipPhone;
                model.WarehouseDestinationName = Request.WarehouseDestinationName;
                model.ProductOutboundId = Request.OutboundId;
                model.BranchId = Request.BranchId;
                ViewBag.RequestInbound = Id;
                return View(model);
            }

            return View();
        }

        [HttpPost]
        public ActionResult CreateFromOutbound(ProductInboundViewModel model)
        {
            var urlRefer = Request["UrlReferrer"];
            if (ModelState.IsValid)
            {
                model.DetailList.RemoveAll(x => x.Quantity <= 0);
                var ProductInbound = new Domain.Sale.Entities.ProductInbound();
                using (var scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        AutoMapper.Mapper.Map(model, ProductInbound);
                        ProductInbound.IsDeleted = false;
                        ProductInbound.CreatedUserId = WebSecurity.CurrentUserId;
                        ProductInbound.ModifiedUserId = WebSecurity.CurrentUserId;
                        ProductInbound.CreatedDate = DateTime.Now;
                        ProductInbound.ModifiedDate = DateTime.Now;
                        ProductInbound.BranchId = model.BranchId;
                        ProductInbound.TotalAmount = model.DetailList.Sum(x => x.Price * x.Quantity);
                        ProductInbound.ProductOutboundId = model.ProductOutboundId;

                        ProductInbound.Type = "internal";
                        //thêm mới phiếu nhập và chi tiết phiếu nhập
                        ProductInboundRepository.InsertProductInbound(ProductInbound);

                        //cập nhật lại mã nhập kho
                        string prefix = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_Inbound");
                        ProductInbound.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix, ProductInbound.Id);
                        ProductInboundRepository.UpdateProductInbound(ProductInbound);
                        int number_error = 0;

                        //Thêm chi tiết phiếu nhập
                        foreach (var item in model.DetailList)
                        {
                            var productInboundDetail = new ProductInboundDetail();
                            productInboundDetail.IsDeleted = false;
                            productInboundDetail.CreatedUserId = WebSecurity.CurrentUserId;
                            productInboundDetail.ModifiedUserId = WebSecurity.CurrentUserId;
                            productInboundDetail.CreatedDate = DateTime.Now;
                            productInboundDetail.ModifiedDate = DateTime.Now;
                            productInboundDetail.ProductInboundId = ProductInbound.Id;
                            productInboundDetail.ProductId = item.ProductId;
                            productInboundDetail.Quantity = item.Quantity;
                            productInboundDetail.Unit = item.Unit;
                            productInboundDetail.ExpiryDate = item.ExpiryDate;
                            productInboundDetail.LoCode = item.LoCode;
                            productInboundDetail.Price = item.Price;
                            ProductInboundRepository.InsertProductInboundDetail(productInboundDetail);

                            if (item.NumberAmount > 0)
                            {
                                number_error++;
                                var productdamages = new ProductDamaged();
                                productdamages.IsDeleted = false;
                                productdamages.CreatedUserId = WebSecurity.CurrentUserId;
                                productdamages.ModifiedUserId = WebSecurity.CurrentUserId;
                                productdamages.CreatedDate = DateTime.Now;
                                productdamages.ModifiedDate = DateTime.Now;
                                productdamages.Quantity = item.NumberAmount;
                                productdamages.Reason = item.Reason;
                                productdamages.Status = "pendding";
                                productdamages.ProductInboundId = ProductInbound.Id;
                                productdamages.ProductInboundDetailId = productInboundDetail.Id;
                                productDamagesRepository.InsertProductDamaged(productdamages);
                            }
                            //if (item.Quantity > 0)

                        }

                        //Cập nhật lại phiếu xuất
                        if (model.ProductOutboundId != null && model.ProductOutboundId > 0)
                        {
                            var productOutbound = ProductOutboundRepository.GetProductOutboundById(model.ProductOutboundId.Value);
                            productOutbound.IsDone = true;
                        }
                        //cập nhật lại yêu cầu nhập kho
                        var requestInbound = Request["RequestInbound"];
                        var request = requestInboundRepository.GetRequestInboundById(Convert.ToInt32(requestInbound));
                        request.InboundId = ProductInbound.Id;
                        if (number_error > 0)
                        {
                            request.Error = true;
                            request.ErrorQuantity = number_error;
                        }
                        requestInboundRepository.UpdateRequestInbound(request);
                        //Thêm vào quản lý chứng từ
                        TransactionController.Create(new TransactionViewModel
                        {
                            TransactionModule = "ProductInbound",
                            TransactionCode = ProductInbound.Code,
                            TransactionName = "Nhập kho"
                        });
                        scope.Complete();
                    }
                    catch (DbUpdateException)
                    {
                        return Content("Fail");
                    }
                }
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                //return RedirectToAction("Index", new { Id = ProductInbound.Id });
                if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                {
                    ViewBag.closePopup = "true";
                    ViewBag.urlRefer = urlRefer;
                    return View(model);
                }
                return Redirect(urlRefer);
            }
            return View(model);
        }
        #endregion

        #region Approval chấp thuận chênh lệch giữa phiếu xuất và phiếu nhập.
        public ActionResult Approval(int Id, int? ProductInboundId)
        {
            // nếu trạng thái của quyết định điều chuyển là chờ duyệt thì chuyển thành đã duyệt
            // ngược lại thì chuyển thành Đã chuyển và cập nhật lại dữ liệu nhân viên trong bảng staff
            var q = productDamagesRepository.GetProductDamagedById(Id);
            q.Status = "success";

            productDamagesRepository.UpdateProductDamaged(q);
            var requestinbound = requestInboundRepository.GetAllRequestInbound().Where(x => x.InboundId == ProductInboundId).FirstOrDefault();
            requestinbound.ErrorQuantity = requestinbound.ErrorQuantity - 1;
            requestInboundRepository.UpdateRequestInbound(requestinbound);
            //gửi notifications cho người được phân quyền.
            Crm.Controllers.ProcessController.Run("ProductInbound", "Approval", q.Id, q.CreatedUserId, null, q, requestinbound.BranchId.Value.ToString());

            return RedirectToAction("Detail", "ProductInbound", new { area = "Sale", Id = ProductInboundId });
        }
        #endregion

        #region Print
        public ActionResult Print(int? Id)
        {
            //Lấy thông tin phiếu xuất kho
            var ProductInbound = ProductInboundRepository.GetvwProductInboundById(Id.Value);

            if (ProductInbound != null)
            {

                //Lấy template và replace dữ liệu vào phiếu xuất.
                TemplatePrint template = null;

                template = templatePrintRepository.GetAllTemplatePrint().Where(x => x.Code.Contains("ProductInbound")).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
                if (template == null)
                    return Content("No template!");

                string output = template.Content;


                var company = "";
                var address = "";
                var phone = "";
                if (ProductInbound.BranchId != null)
                {
                    var branch = branchRepository.GetvwBranchById(ProductInbound.BranchId.Value);
                    company = Erp.BackOffice.Helpers.Common.GetSetting("companyName") + " - " + branch.Name;
                    address = branch.Address + " - " + branch.WardName + " - " + branch.DistrictName + " - " + branch.ProvinceName;
                    phone = branch.Phone;
                }
                else
                {
                    company = Erp.BackOffice.Helpers.Common.GetSetting("companyName");
                    address = Erp.BackOffice.Helpers.Common.GetSetting("addresscompany");
                    phone = Erp.BackOffice.Helpers.Common.GetSetting("phonecompany");
                }
                //Header
                //Lấy thông tin cài đặt cho phiếu in
                var logo = Erp.BackOffice.Helpers.Common.GetSetting("LogoCompany");
                var fax = Erp.BackOffice.Helpers.Common.GetSetting("faxcompany");
                var bankcode = Erp.BackOffice.Helpers.Common.GetSetting("bankcode");
                var bankname = Erp.BackOffice.Helpers.Common.GetSetting("bankname");
                var ImgLogo = "<div class=\"logo\"><img src=" + logo + " height=\"73\" /></div>";

                output = output.Replace("{System.Logo}", ImgLogo);
                output = output.Replace("{System.CompanyName}", company);
                output = output.Replace("{System.AddressCompany}", address);
                output = output.Replace("{System.PhoneCompany}", phone);
                output = output.Replace("{System.Fax}", fax);
                output = output.Replace("{System.BankCodeCompany}", bankcode);
                output = output.Replace("{System.BankNameCompany}", bankname);

                string note = ProductInbound.Note;
                string code = ProductInbound.Code;
                //manual nhập từ kho
                //external nhập từ đơn đặt hàng
                //SalesReturns nhập từ đơn hàng trả lại
                if (ProductInbound.Type == "internal")//nhập từ phiếu xuất
                {
                    var productOutbound = ProductOutboundRepository.GetvwProductOutboundById(ProductInbound.ProductOutboundId.Value);
                    output = output.Replace("{WarehouseSourceName}", "phiếu xuất: " + productOutbound.Code);
                }
                if (ProductInbound.Type == "manual")
                {
                    //var supplier=SupplierRepository.GetvwSupplierById(ProductInbound.SupplierId)
                    output = output.Replace("{WarehouseSourceName}", ": " + ProductInbound.SupplierName);
                }
                if (ProductInbound.Type == "external")
                {
                    var purchaseOrder = PurchaseOrderRepository.GetPurchaseOrderById(ProductInbound.PurchaseOrderId.Value);
                    output = output.Replace("{WarehouseSourceName}", "đơn đặt hàng: " + purchaseOrder.Code);
                }
                if (ProductInbound.Type == "SalesReturns")
                {
                    output = output.Replace("{WarehouseSourceName}", "đơn hàng trả lại: " + ProductInbound.SalesReturnsCode);
                }
                output = output.Replace("{WarehouseDestinationName}", ProductInbound.WarehouseDestinationName);
                output = output.Replace("{ProductInboundDate}", ProductInbound.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm"));
                //Tạo barcode
                Image barcode = Code128Rendering.MakeBarcodeImage(ProductInbound.Code, 1, true);
                output = output.Replace("{BarcodeImgSource}", ImageToBase64(barcode, System.Drawing.Imaging.ImageFormat.Png));

                output = output.Replace("{Note}", note);
                output = output.Replace("{Code}", code);


                //Mid
                output = output.Replace("{DetailList}", buildHtmlDetailList(Id.Value));

                //Footer
                ViewBag.PrintContent = output;
                return View();
            }

            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        string buildHtmlDetailList(int Id)
        {
            decimal tong_tien = 0;
            //Lấy danh sách sản phẩm xuất kho
            var inboundDetails = ProductInboundRepository.GetAllvwProductInboundDetailByInboundId(Id).AsEnumerable()
                    .Select(x => new ProductInboundDetailViewModel
                    {
                        Id = x.Id,
                        Price = x.Price,
                        ProductId = x.ProductId,
                        ProductName = x.ProductName,
                        ProductCode = x.ProductCode,
                        ProductInboundId = x.ProductInboundId,
                        Quantity = x.Quantity,
                        Unit = x.Unit,
                        LoCode = x.LoCode,
                        ExpiryDate = x.ExpiryDate
                    }).ToList();

            //Điền lô/date
            //Tạo table html chi tiết phiếu xuất
            string detailList = "<table class=\"invoice-detail\">\r\n";
            detailList += "<thead>\r\n";
            detailList += "	<tr>\r\n";
            detailList += "		<th>STT</th>\r\n";
            detailList += "		<th>M&atilde; h&agrave;ng</th>\r\n";
            detailList += "		<th>T&ecirc;n mặt h&agrave;ng</th>\r\n";
            detailList += "		<th>L&ocirc; sản xuất</th>\r\n";
            detailList += "		<th>Hạn d&ugrave;ng</th>\r\n";
            detailList += "		<th>ĐVT</th>\r\n";
            detailList += "		<th>Số lượng</th>\r\n";
            detailList += "		<th>Đơn gi&aacute;</th>\r\n";
            detailList += "		<th>Th&agrave;nh tiền</th>\r\n";
            detailList += "	</tr>\r\n";
            detailList += "</thead>\r\n";
            detailList += "<tbody><tbody>\r\n";

            foreach (var item in inboundDetails)
            {
                decimal thanh_tien = item.Quantity.Value * item.Price.Value;
                tong_tien += thanh_tien;

                detailList += "<tr>\r\n"
                 + "<td class=\"text-center\">" + (inboundDetails.ToList().IndexOf(item) + 1) + "</td>\r\n"
                 + "<td class=\"text-left\">" + item.ProductCode + "</td>\r\n"
                 + "<td class=\"text-left\">" + item.ProductName + "</td>\r\n"
                 + "<td class=\"text-right\">" + item.LoCode + "</td>\r\n"
                 + "<td class=\"text-right\">" + (item.ExpiryDate.HasValue ? item.ExpiryDate.Value.ToString("dd-MM-yyyy") : "") + "</td>\r\n"
                 + "<td class=\"text-center\">" + item.Unit + "</td>\r\n"
                 + "<td class=\"text-right\">" + Erp.BackOffice.Helpers.Common.PhanCachHangNgan(item.Quantity) + "</td>\r\n"
                 + "<td class=\"text-right\">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(item.Price, null) + "</td>\r\n"
                 + "<td class=\"text-right\">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(thanh_tien, null) + "</td>\r\n"
                 + "</tr>\r\n";
            }

            detailList += "</tbody>\r\n";
            detailList += "<tfoot>\r\n";
            detailList += "<tr><td colspan=\"6\" class=\"text-right\"></td><td class=\"text-right\">"
                         + Erp.BackOffice.Helpers.Common.PhanCachHangNgan(inboundDetails.Sum(x => x.Quantity))
                         + "</td><td class=\"text-right\">Tổng cộng</td><td class=\"text-right\">"
                         + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(tong_tien, null)
                         + "</td></tr>\r\n";
            detailList += "<tr><td colspan=\"9\"><strong>Tiền bằng chữ: " + Erp.BackOffice.Helpers.Common.ChuyenSoThanhChu_2(tong_tien.ToString()) + "</strong></td></tr>\r\n";
            detailList += "</tfoot>\r\n</table>\r\n";

            return detailList;
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
        public ActionResult Delete(int Id, string Note)
        {
            var productInbound = ProductInboundRepository.GetProductInboundById(Id);
            if (productInbound != null)
            {
                //Kiểm tra phân quyền Chi nhánh
                if (!(Filters.SecurityFilter.IsAdmin() || ("," + Helpers.Common.CurrentUser.DrugStore + ",").Contains("," + productInbound.BranchId + ",") == true))
                {
                    return Content("Mẫu tin không tồn tại! Không có quyền truy cập!");
                }

                productInbound.ModifiedUserId = WebSecurity.CurrentUserId;
                productInbound.ModifiedDate = DateTime.Now;
                productInbound.IsDeleted = true;
                productInbound.IsArchive = false;
                productInbound.Note = Note;
                ProductInboundRepository.UpdateProductInbound(productInbound);

                return RedirectToAction("Detail", new { Id = productInbound.Id });
            }

            return RedirectToAction("Index");
        }
        #endregion

        public static void CreateFromProductOutbound(ProductOutbound outbound, List<ProductOutboundDetail> ListDetail)
        {

            Erp.Domain.Sale.Repositories.ProductInboundRepository productInboundRepository = new Erp.Domain.Sale.Repositories.ProductInboundRepository(new Domain.Sale.ErpSaleDbContext());
            var productInbound = new Domain.Sale.Entities.ProductInbound();
            productInbound.IsDeleted = false;
            productInbound.CreatedUserId = WebSecurity.CurrentUserId;
            productInbound.ModifiedUserId = WebSecurity.CurrentUserId;
            productInbound.CreatedDate = DateTime.Now;
            productInbound.ModifiedDate = DateTime.Now;
            productInbound.BranchId = outbound.BranchId;
            productInbound.TotalAmount = outbound.TotalAmount;
            productInbound.IsArchive = false;
            productInbound.ProductOutboundId = outbound.Id;
            productInbound.WarehouseDestinationId = outbound.WarehouseDestinationId;
            productInbound.Type = "internal";
            productInboundRepository.InsertProductInbound(productInbound);

            //Cập nhật lại mã xuất kho
            string prefix = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_Inbound");
            productInbound.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix, productInbound.Id);
            productInboundRepository.UpdateProductInbound(productInbound);
            // chi tiết phiếu xuất
            foreach (var item in ListDetail)
            {
                ProductInboundDetail inboundDetail = new ProductInboundDetail();
                inboundDetail.IsDeleted = false;
                inboundDetail.CreatedUserId = WebSecurity.CurrentUserId;
                inboundDetail.ModifiedUserId = WebSecurity.CurrentUserId;
                inboundDetail.CreatedDate = DateTime.Now;
                inboundDetail.ModifiedDate = DateTime.Now;
                inboundDetail.ProductInboundId = productInbound.Id;
                inboundDetail.ExpiryDate = item.ExpiryDate;
                inboundDetail.LoCode = item.LoCode;
                inboundDetail.Price = item.Price;
                inboundDetail.ProductId = item.ProductId;
                inboundDetail.Quantity = item.Quantity;
                inboundDetail.Unit = item.Unit;
                productInboundRepository.InsertProductInboundDetail(inboundDetail);
            }
            //cập nhật vị trí các sản phẩm đã xuất kho
            //
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
                TransactionB = outbound.Code
            });
        }
    }
}
