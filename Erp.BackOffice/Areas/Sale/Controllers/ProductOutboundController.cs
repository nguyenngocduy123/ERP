using System.Globalization;
using Erp.BackOffice.Sale.Models;
using Erp.BackOffice.Filters;
using Erp.Domain.Sale.Entities;
using Erp.Domain.Interfaces;
using Erp.Domain.Sale.Interfaces;
using Erp.Domain.Sale.Repositories;
using Erp.Domain.Staff.Interfaces;
using Erp.Domain.Staff.Repositories;
using Erp.Domain.Account.Interfaces;
using Erp.Domain.Account.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Erp.Utilities;
using WebMatrix.WebData;
using Erp.BackOffice.Helpers;
using System.Web.Script.Serialization;
using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System.Xml.Linq;
using Erp.BackOffice.Account.Controllers;
using Erp.BackOffice.Account.Models;
using System.Drawing;
using GenCode128;
using System.IO;
using Erp.Domain.Helper;
using System.Transactions;

namespace Erp.BackOffice.Sale.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class ProductOutboundController : Controller
    {
        private readonly IWarehouseRepository WarehouseRepository;
        private readonly IWarehouseLocationItemRepository warehouseLocationItemRepository;
        private readonly IProductInvoiceRepository productInvoiceRepository;
        private readonly IPurchaseOrderRepository PurchaseOrderRepository;
        private readonly IInventoryRepository inventoryRepository;
        private readonly IProductOrServiceRepository ProductRepository;
        private readonly IProductOutboundRepository productOutboundRepository;
        private readonly IProductInboundRepository ProductInboundRepository;
        private readonly IStaffsRepository staffRepository;
        private readonly IUserRepository userRepository;
        private readonly ITemplatePrintRepository templatePrintRepository;
        private readonly IQueryHelper QueryHelper;
        private readonly ICustomerRepository customerRepository;
        private readonly ISettingRepository settingRepository;
        private readonly IRequestInboundRepository requestInboundRepository;
        private readonly IBranchRepository branchRepository;
        private readonly ITransactionLiabilitiesRepository transactionLiabilitiesRepository;
        public ProductOutboundController(
             IWarehouseRepository _Warehouse
            , IWarehouseLocationItemRepository _WarehouseLocationItem
            , IInventoryRepository _Inventory
            , IProductInvoiceRepository _ProductInvoice
            , IPurchaseOrderRepository _PurchaseOrder
            , IProductOrServiceRepository _Product
            , IProductOutboundRepository _ProductOutbound
            , IProductInboundRepository _ProductInbound
            , IStaffsRepository _staff
            , IUserRepository _user
            , IQueryHelper _QueryHelper
            , ITemplatePrintRepository _templatePrint
            , ICustomerRepository customer
             , ISettingRepository _Setting
            , IRequestInboundRepository requestInbound
            , IBranchRepository branch
            )
        {
            WarehouseRepository = _Warehouse;
            warehouseLocationItemRepository = _WarehouseLocationItem;
            inventoryRepository = _Inventory;
            productInvoiceRepository = _ProductInvoice;
            PurchaseOrderRepository = _PurchaseOrder;
            ProductRepository = _Product;
            productOutboundRepository = _ProductOutbound;
            ProductInboundRepository = _ProductInbound;
            staffRepository = _staff;
            userRepository = _user;
            QueryHelper = _QueryHelper;
            templatePrintRepository = _templatePrint;
            customerRepository = customer;
            settingRepository = _Setting;
            requestInboundRepository = requestInbound;
            branchRepository = branch;
        }

        #region Index
        public ViewResult Index(string txtCode, string txtMinAmount, string txtMaxAmount, string txtWarehouseDestination, int? warehouseSourceId, string startDate, string endDate, string txtProductCode, string Status)
        {
            var user = userRepository.GetByvwUserName(Helpers.Common.CurrentUser.UserName);
            IEnumerable<ProductOutboundViewModel> q = productOutboundRepository.GetAllvwProductOutboundFull()
                //.Where(x => x.BranchId == Helpers.Common.CurrentUser.BranchId)
                .Select(item => new ProductOutboundViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    ModifiedDate = item.ModifiedDate,
                    Code = item.Code,
                    TotalAmount = item.TotalAmount,
                    InvoiceCode = item.InvoiceCode,
                    PurchaseOrderCode = item.PurchaseOrderCode,
                    WarehouseDestinationName = item.WarehouseDestinationName,
                    WarehouseSourceName = item.WarehouseSourceName,
                    CustomerName = item.CustomerName,
                    Type = item.Type,
                    IsArchive = item.IsArchive,
                    WarehouseSourceId = item.WarehouseSourceId,
                    InvoiceIsDeleted = item.InvoiceIsDeleted,
                    IsDeleted = item.IsDeleted,
                    BranchId = item.BranchId
                }).OrderByDescending(m => m.Id);

            //Tìm những phiếu xuất có chứa mã sp
            if (!string.IsNullOrEmpty(txtProductCode))
            {
                txtProductCode = txtProductCode.Trim();
                var productListId = ProductRepository.GetAllvwProduct()
                    .Where(item => item.Code == txtProductCode).Select(item => item.Id).ToList();

                if (productListId.Count > 0)
                {
                    List<int> listProductOutboundId = new List<int>();
                    foreach (var id in productListId)
                    {
                        var list = productOutboundRepository.GetAllvwProductOutboundDetailByProductId(id)
                            .Select(item => item.ProductOutboundId.Value).Distinct().ToList();

                        listProductOutboundId.AddRange(list);
                    }

                    q = q.Where(item => listProductOutboundId.Contains(item.Id));
                }
            }

            if (string.IsNullOrEmpty(txtCode) == false || string.IsNullOrEmpty(txtWarehouseDestination) == false || warehouseSourceId != null)
            {
                txtCode = txtCode == "" ? "~" : txtCode.ToLower();
                txtWarehouseDestination = txtWarehouseDestination == "" ? "~" : Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(txtWarehouseDestination);
                q = q.Where(x => x.Code.ToLowerOrEmpty().Contains(txtCode)
                    || Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(x.WarehouseDestinationName).Contains(txtWarehouseDestination)
                    || x.PurchaseOrderCode.ToLowerOrEmpty().Contains(txtWarehouseDestination)
                    || x.InvoiceCode.ToLowerOrEmpty().Contains(txtWarehouseDestination)
                    //|| x.WarehouseSourceId == warehouseSourceId
                    );
            }
            if (warehouseSourceId != null && warehouseSourceId.Value > 0)
            {
                q = q.Where(x => x.WarehouseSourceId == warehouseSourceId);
            }
            if (!Erp.BackOffice.Filters.SecurityFilter.IsAdmin() && !Erp.BackOffice.Filters.SecurityFilter.IsKeToan() && !Erp.BackOffice.Filters.SecurityFilter.IsQLKhoTong())
            {
                q = q.Where(x => ("," + user.DrugStore + ",").Contains("," + x.BranchId + ",") == true);
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

        #region CreateFromRequest
        public ActionResult CreateFromRequest(int? WarehouseSourceId, int? RequestInboundId)
        {
            //string image_folder = Helpers.Common.GetSetting("product-image-folder");
            var warehouseList = WarehouseRepository.GetvwAllWarehouse().AsEnumerable();
            var user = userRepository.GetByvwUserName(Helpers.Common.CurrentUser.UserName);
            if (!Erp.BackOffice.Filters.SecurityFilter.IsAdmin() && !Erp.BackOffice.Filters.SecurityFilter.IsKeToan())
            {
                warehouseList = warehouseList.Where(x => ("," + user.WarehouseId + ",").Contains("," + x.Id + ",") == true);
            }
            var _warehouseList = warehouseList.Select(item => new SelectListItem
            {
                Text = item.Name + " (" + item.BranchName + ")",
                Value = item.Id.ToString()
            });
            ViewBag.warehouseSourceList = _warehouseList;
            ViewBag.warehouseDestinationList = warehouseList
                .Select(item => new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                });

            ProductOutboundViewModel model = new ProductOutboundViewModel();
            model.requestInboundViewModel = new RequestInboundViewModel();
            model.CreatedUserName = Erp.BackOffice.Helpers.Common.CurrentUser.FullName;
            model.CreatedDate = DateTime.Now;
            model.TotalAmount = 0;
            //model.CreatedStaffId = CreatedStaffId;
            model.DetailList = new List<ProductOutboundDetailViewModel>();


            if (WarehouseSourceId != null && WarehouseSourceId > 0)
            {
                model.WarehouseSourceId = WarehouseSourceId;
                var productList = Domain.Helper.SqlHelper.QuerySP<InventoryViewModel>("spSale_Get_Inventory", new { WarehouseId = WarehouseSourceId, HasQuantity = "1", ProductCode = "", ProductName = "", CategoryCode = "", ProductGroup = "", BranchId = "0", LoCode = "", ProductId = "", ExpiryDate = "" });
                ViewBag.productList = productList;
                if (RequestInboundId != null)
                {
                    //var RequestInboundView = new RequestInboundViewModel();
                    var RequestInbound = requestInboundRepository.GetvwRequestInboundById(RequestInboundId.Value);

                    if (RequestInbound == null)
                    {
                        TempData[Globals.FailedMessageKey] = Wording.NotfoundObject;
                        return RedirectToAction("Create");
                    }

                    model.WarehouseDestinationId = RequestInbound.WarehouseDestinationId;
                    var list_request_detail = requestInboundRepository.GetAllvwRequestInboundDetailsByInvoiceId(RequestInbound.Id).ToList();
                    int i = -1;
                    foreach (var item in list_request_detail)
                    {
                        var inventory = productList.Where(x => x.ProductId == item.ProductId).OrderBy(x => x.ExpiryDate);
                        int? quantity = 0;
                        foreach (var inv in inventory)
                        {
                            i++;
                            quantity += inv.Quantity;
                            if (item.Quantity >= quantity)
                            {
                                var add_item = new ProductOutboundDetailViewModel();
                                add_item.ProductId = item.ProductId;
                                add_item.Price = item.Price;
                                add_item.Quantity = inv.Quantity;
                                add_item.QuantityInInventory = 0;
                                add_item.ProductName = item.ProductName;
                                add_item.ProductCode = item.ProductCode;
                                add_item.Unit = item.Unit;
                                add_item.OrderNo = i;
                                add_item.LoCode = inv.LoCode;
                                add_item.ExpiryDate = inv.ExpiryDate;
                                model.DetailList.Add(add_item);
                                if (item.Quantity == quantity)
                                    break;
                            }
                            else
                            {
                                var sl_cu = quantity - inv.Quantity;
                                var sl_can = item.Quantity - sl_cu;
                                var add_item = new ProductOutboundDetailViewModel();
                                add_item.ProductId = item.ProductId;
                                add_item.Price = item.Price;
                                add_item.Quantity = sl_can;
                                add_item.QuantityInInventory = inv.Quantity - sl_can;
                                add_item.ProductName = item.ProductName;
                                add_item.ProductCode = item.ProductCode;
                                add_item.Unit = item.Unit;
                                add_item.OrderNo = i;
                                add_item.LoCode = inv.LoCode;
                                add_item.ExpiryDate = inv.ExpiryDate;
                                model.DetailList.Add(add_item);
                                break;
                            }

                        }
                    }

                    var q = requestInboundRepository.GetRequestInboundById(RequestInboundId.Value);
                    if (q.Status == "ApprovedASM")
                    {
                        q.Status = "ApprovedKT";
                        q.ModifiedUserId = WebSecurity.CurrentUserId;
                        requestInboundRepository.UpdateRequestInbound(q);
                        //run process
                        Crm.Controllers.ProcessController.Run("RequestInbound", "ApprovedKT", q.Id, q.CreatedUserId, null, q, q.BranchId.Value.ToString());
                    }
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateFromRequest(ProductOutboundViewModel model)
        {
            if (ModelState.IsValid)
            {
                //duyệt qua danh sách sản phẩm mới xử lý tình huống user chọn 2 sản phầm cùng id
                List<ProductOutboundDetail> outboundDetails = new List<ProductOutboundDetail>();
                model.DetailList.RemoveAll(x => x.Quantity <= 0);
                foreach (var group in model.DetailList)
                {
                    //var product = ProductRepository.GetProductById(group.Key.Value);

                    outboundDetails.Add(new ProductOutboundDetail
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
                        LoCode = group.LoCode,
                        ExpiryDate = group.ExpiryDate
                    });
                }
                using (var scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        //thêm mới phiếu xuất kho theo đơn xuất kho ở trên
                        var ProductOutbound = new Domain.Sale.Entities.ProductOutbound();
                        AutoMapper.Mapper.Map(model, ProductOutbound);
                        ProductOutbound.IsDeleted = false;
                        ProductOutbound.CreatedUserId = WebSecurity.CurrentUserId;
                        ProductOutbound.ModifiedUserId = WebSecurity.CurrentUserId;
                        ProductOutbound.CreatedDate = DateTime.Now;
                        ProductOutbound.ModifiedDate = DateTime.Now;
                        if (model.RequestInboundId != null)
                        {
                            ProductOutbound.Type = "internal";
                        }
                        var branch = WarehouseRepository.GetWarehouseById(ProductOutbound.WarehouseSourceId.Value);
                        ProductOutbound.BranchId = branch.BranchId;

                        ProductOutbound.IsDone = false;
                        ProductOutbound.TotalAmount = outboundDetails.Sum(item => item.Quantity * item.Price);

                        productOutboundRepository.InsertProductOutbound(ProductOutbound);

                        foreach (var item in outboundDetails)
                        {
                            ProductOutboundDetail productOutboundDetail = new ProductOutboundDetail();
                            productOutboundDetail.ProductId = item.ProductId;
                            productOutboundDetail.Price = item.Price;
                            productOutboundDetail.Quantity = item.Quantity;
                            productOutboundDetail.Unit = item.Unit;
                            productOutboundDetail.LoCode = item.LoCode;
                            productOutboundDetail.ExpiryDate = item.ExpiryDate;
                            productOutboundDetail.IsDeleted = false;
                            productOutboundDetail.CreatedUserId = WebSecurity.CurrentUserId;
                            productOutboundDetail.ModifiedUserId = WebSecurity.CurrentUserId;
                            productOutboundDetail.CreatedDate = DateTime.Now;
                            productOutboundDetail.ModifiedDate = DateTime.Now;
                            productOutboundDetail.ProductOutboundId = ProductOutbound.Id;
                            productOutboundRepository.InsertProductOutboundDetail(productOutboundDetail);

                        }
                        if (model.RequestInboundId != null)
                        {
                            var order = requestInboundRepository.GetRequestInboundById(model.RequestInboundId.Value);
                            order.OutboundId = ProductOutbound.Id;
                            requestInboundRepository.UpdateRequestInbound(order);
                        }
                        if (model.RequestInboundId != null)
                        {
                            var order = requestInboundRepository.GetRequestInboundById(model.RequestInboundId.Value);
                            order.Status = "shipping";
                            order.ModifiedDate = DateTime.Now;
                            order.WarehouseSourceId = ProductOutbound.WarehouseSourceId;
                            order.ModifiedUserId = WebSecurity.CurrentUserId;
                            order.ShipName = model.requestInboundViewModel.ShipName;
                            order.ShipPhone = model.requestInboundViewModel.ShipPhone;
                            requestInboundRepository.UpdateRequestInbound(order);
                            //Thêm vào quản lý chứng từ
                            TransactionController.Create(new TransactionViewModel
                            {
                                TransactionModule = "ProductOutbound",
                                TransactionCode = ProductOutbound.Code,
                                TransactionName = "Xuất kho"
                            });
                            //gửi notifications cho người được phân quyền.
                            Crm.Controllers.ProcessController.Run("RequestInbound", "shipping", order.Id, order.CreatedUserId, null, order, order.BranchId.Value.ToString());
                        }
                        //cập nhật lại mã xuất kho
                        string prefixOutbound = Helpers.Common.GetSetting("prefixOrderNo_Outbound");
                        ProductOutbound.Code = Helpers.Common.GetCode(prefixOutbound, ProductOutbound.Id);
                        productOutboundRepository.UpdateProductOutbound(ProductOutbound);
                        //Thêm vào quản lý chứng từ
                        TransactionController.Create(new TransactionViewModel
                        {
                            TransactionModule = "ProductOutbound",
                            TransactionCode = ProductOutbound.Code,
                            TransactionName = "Xuất kho"
                        });
                        scope.Complete();
                    }
                    catch (DbUpdateException)
                    {
                        TempData[Globals.FailedMessageKey] = App_GlobalResources.Wording.Error;
                        return Content("Fail");
                    }
                }
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                {
                    return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
                }
                //  return RedirectToAction("Index");
            }

            // phần xử lý cho việc valid sai
            var warehouseList = WarehouseRepository.GetAllWarehouse().AsEnumerable();

            ViewBag.warehouseSourceList = warehouseList.Where(x => ("," + Helpers.Common.CurrentUser.WarehouseId + ",").Contains("," + x.Id + ",") == true)
                   .Select(item => new SelectListItem
                   {
                       Text = item.Name,
                       Value = item.Id.ToString()
                   });

            ViewBag.warehouseDestinationList = warehouseList
                .Select(item => new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                });

            return View(model);
        }
        #endregion

        #region Create
        public ActionResult Create(int? WarehouseSourceId)
        {
            // phần xử lý cho việc valid sai
            var warehouseList = WarehouseRepository.GetvwAllWarehouse().AsEnumerable();
            var user = userRepository.GetByvwUserName(Helpers.Common.CurrentUser.UserName);
            if (!Erp.BackOffice.Filters.SecurityFilter.IsAdmin() && !Erp.BackOffice.Filters.SecurityFilter.IsKeToan())
            {
                warehouseList = warehouseList.Where(x => ("," + user.WarehouseId + ",").Contains("," + x.Id + ",") == true);
            }
            var _warehouseList = warehouseList.Select(item => new SelectListItem
                   {
                       Text = item.Name + " (" + item.BranchName + ")",
                       Value = item.Id.ToString()
                   });
            ViewBag.warehouseSourceList = _warehouseList;
            ViewBag.warehouseDestinationList = warehouseList
                .Select(item => new SelectListItem
                {
                    Text = item.Name + " (" + item.BranchName + ")",
                    Value = item.Id.ToString()
                });

            ProductOutboundTransferViewModel model = new ProductOutboundTransferViewModel();

            //model.CreatedUserName = Erp.BackOffice.Helpers.Common.CurrentUser.FullName;
            //model.CreatedDate = DateTime.Now;
            model.TotalAmount = 0;
            //model.CreatedStaffId = CreatedStaffId;
            //if (CreatedStaffId != null)
            //{
            //    var staff = staffRepository.GetStaffsById(CreatedStaffId.Value);
            //    model.CreatedStaffName = staff.Name;
            //}
            if (WarehouseSourceId != null && WarehouseSourceId > 0)
            {
                model.WarehouseSourceId = WarehouseSourceId;
                var productList = Domain.Helper.SqlHelper.QuerySP<InventoryViewModel>("spSale_Get_Inventory", new { WarehouseId = WarehouseSourceId, HasQuantity = "1", ProductCode = "", ProductName = "", CategoryCode = "", ProductGroup = "", BranchId = "0", LoCode = "", ProductId = "", ExpiryDate = "" });
                ViewBag.productList = productList;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ProductOutboundTransferViewModel model)
        {
            // phần xử lý cho việc valid sai
            var warehouseList = WarehouseRepository.GetAllWarehouse().AsEnumerable();

            ViewBag.warehouseSourceList = warehouseList.Where(x => ("," + Helpers.Common.CurrentUser.WarehouseId + ",").Contains("," + x.Id + ",") == true)
                   .Select(item => new SelectListItem
                   {
                       Text = item.Name,
                       Value = item.Id.ToString()
                   });

            ViewBag.warehouseDestinationList = warehouseList
                .Select(item => new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                });
            if (ModelState.IsValid)
            {
                var ProductOutbound = new Domain.Sale.Entities.ProductOutbound();
                var check = Request["group_choice"];
                //duyệt qua danh sách sản phẩm mới xử lý tình huống user chọn 2 sản phầm cùng id
                List<ProductOutboundDetail> outboundDetails = new List<ProductOutboundDetail>();
                model.DetailList.RemoveAll(x => x.Quantity <= 0);
                using (var scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (var group in model.DetailList)
                        {
                            outboundDetails.Add(new ProductOutboundDetail
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
                                LoCode = group.LoCode,
                                ExpiryDate = group.ExpiryDate
                            });
                        }

                        //thêm mới phiếu xuất kho theo đơn xuất kho ở trên

                        AutoMapper.Mapper.Map(model, ProductOutbound);
                        ProductOutbound.IsDeleted = false;
                        ProductOutbound.CreatedUserId = WebSecurity.CurrentUserId;
                        ProductOutbound.ModifiedUserId = WebSecurity.CurrentUserId;
                        ProductOutbound.CreatedDate = DateTime.Now;
                        ProductOutbound.ModifiedDate = DateTime.Now;
                        ProductOutbound.Type = check;
                        if (check == "service")
                        {
                            ProductOutbound.WarehouseDestinationId = null;
                        }
                        var branch = WarehouseRepository.GetWarehouseById(ProductOutbound.WarehouseSourceId.Value);
                        ProductOutbound.BranchId = branch.BranchId;
                        ProductOutbound.IsDone = false;
                        ProductOutbound.TotalAmount = outboundDetails.Sum(item => item.Quantity * item.Price);

                        productOutboundRepository.InsertProductOutbound(ProductOutbound);

                        foreach (var item in outboundDetails)
                        {
                            ProductOutboundDetail productOutboundDetail = new ProductOutboundDetail();
                            productOutboundDetail.ProductId = item.ProductId;
                            productOutboundDetail.Price = item.Price;
                            productOutboundDetail.Quantity = item.Quantity;
                            productOutboundDetail.Unit = item.Unit;
                            productOutboundDetail.LoCode = item.LoCode;
                            productOutboundDetail.ExpiryDate = item.ExpiryDate;
                            productOutboundDetail.IsDeleted = false;
                            productOutboundDetail.CreatedUserId = WebSecurity.CurrentUserId;
                            productOutboundDetail.ModifiedUserId = WebSecurity.CurrentUserId;
                            productOutboundDetail.CreatedDate = DateTime.Now;
                            productOutboundDetail.ModifiedDate = DateTime.Now;
                            productOutboundDetail.ProductOutboundId = ProductOutbound.Id;
                            productOutboundRepository.InsertProductOutboundDetail(productOutboundDetail);

                        }

                        //cập nhật lại mã xuất kho
                        string prefixOutbound = Helpers.Common.GetSetting("prefixOrderNo_Outbound");
                        ProductOutbound.Code = Helpers.Common.GetCode(prefixOutbound, ProductOutbound.Id);
                        productOutboundRepository.UpdateProductOutbound(ProductOutbound);
                        //Thêm vào quản lý chứng từ
                        TransactionController.Create(new TransactionViewModel
                        {
                            TransactionModule = "ProductOutbound",
                            TransactionCode = ProductOutbound.Code,
                            TransactionName = "Xuất kho"
                        });
                        if (ProductOutbound.WarehouseDestinationId != null && ProductOutbound.WarehouseDestinationId.Value>0)
                        {
                            Erp.BackOffice.Sale.Controllers.ProductInboundController.CreateFromProductOutbound(ProductOutbound, outboundDetails);
                        }
                        scope.Complete();
                    
                    }
                    catch (DbUpdateException)
                    {
                        TempData[Globals.FailedMessageKey] = App_GlobalResources.Wording.Error;
                        return View(model);
                    }
                }
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Detail", "ProductOutbound", new { area = "Sale", Id = ProductOutbound.Id });
            }
            return View(model);
        }
        #endregion

        #region Json
        public JsonResult EditInline(int? Id, string fieldName, string value)
        {
            Dictionary<string, object> field_value = new Dictionary<string, object>();
            field_value.Add(fieldName, value);
            field_value.Add("ModifiedDate", DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"));
            field_value.Add("ModifiedUserId", WebSecurity.CurrentUserId);

            var flag = QueryHelper.UpdateFields("Sale_ProductOutbound", field_value, Id.Value);
            if (flag == true)
                return Json(new { status = "success", id = Id, fieldName = fieldName, value = value }, JsonRequestBehavior.AllowGet);

            return Json(new { status = "error", id = Id, fieldName = fieldName, value = value }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetWarehouseLocationItem(int? warehouseId, int? productId, string serialNumber)
        {
            if (warehouseId == null || productId == null || string.IsNullOrEmpty(serialNumber) == true)
                return Json(new WarehouseLocationItemViewModel(), JsonRequestBehavior.AllowGet);

            var item = warehouseLocationItemRepository.GetWarehouseLocationItemBySerialNumber(warehouseId.Value, productId.Value, serialNumber);
            return Json(item, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Print
        public ActionResult Print(int? Id)
        {
            //Lấy thông tin phiếu xuất kho
            var ProductOutbound = productOutboundRepository.GetvwProductOutboundById(Id.Value);

            if (ProductOutbound != null)
            {

                //Lấy template và replace dữ liệu vào phiếu xuất.
                TemplatePrint template = null;
                if (ProductOutbound.Type == "internal")
                    template = templatePrintRepository.GetAllTemplatePrint().Where(x => x.Code.Contains("ProductTransfer")).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
                else
                    template = templatePrintRepository.GetAllTemplatePrint().Where(x => x.Code.Contains("ProductOutbound")).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
                if (template == null)
                    return Content("No template!");

                string output = template.Content;


                var company = "";
                var address = "";
                var phone = "";
                if (ProductOutbound.BranchId != null)
                {
                    var branch = branchRepository.GetvwBranchById(ProductOutbound.BranchId.Value);
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

                string day = ProductOutbound.CreatedDate.Value.Day.ToString();
                string month = ProductOutbound.CreatedDate.Value.Month.ToString();
                string year = ProductOutbound.CreatedDate.Value.Year.ToString();
                string customerName = "";
                string customerPhone = "";
                string companyName = "";
                string fullAddress = "";
                string note = ProductOutbound.Note;
                string code = ProductOutbound.Code;
                string saleName = "";
                string VAT_InvoiceCode = "";
                string paymentMethod = "";

                //Thông tin hóa đơn nếu xuất kho bán hàng
                if (ProductOutbound.InvoiceId != null)
                {
                    var productInvoice = productInvoiceRepository.GetvwProductInvoiceById(ProductOutbound.InvoiceId.Value);
                    //Thông tin khách hàng
                    var customer = customerRepository.GetvwCustomerByCode(productInvoice.CustomerCode);
                    if (customer != null)
                    {
                        customerName = customer.FirstName + " " + customer.LastName;
                        customerPhone = customer.Phone;
                        companyName = customer.CompanyName;
                        fullAddress = customer.Address + (!string.IsNullOrEmpty(customer.WardName) ? ", " + customer.WardName : "") + (!string.IsNullOrEmpty(customer.DistrictName) ? ", " + customer.DistrictName : "") + customer.ProvinceName;
                        saleName = productInvoice.SalerFullName;
                        VAT_InvoiceCode = productInvoice.CodeInvoiceRed;
                        paymentMethod = productInvoice.PaymentMethod;
                    }
                }
                if (ProductOutbound.Type == "internal")
                {
                    output = output.Replace("{WarehouseSourceName}", ProductOutbound.WarehouseSourceName);
                    output = output.Replace("{WarehouseDestinationName}", ProductOutbound.WarehouseDestinationName);
                    output = output.Replace("{ProductOutboundDate}", ProductOutbound.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm"));
                }
                else
                {
                    output = output.Replace("{CustomerName}", customerName);
                    output = output.Replace("{CustomerPhone}", customerPhone);
                    output = output.Replace("{Day}", day);
                    output = output.Replace("{Month}", month);
                    output = output.Replace("{Year}", year);
                    output = output.Replace("{CompanyName}", companyName);
                    output = output.Replace("{FullAddress}", fullAddress);
                    output = output.Replace("{SaleName}", saleName);
                    output = output.Replace("{VAT_InvoiceCode}", VAT_InvoiceCode);
                    output = output.Replace("{PaymentMethod}", paymentMethod);
                }
                //Tạo barcode
                Image barcode = Code128Rendering.MakeBarcodeImage(ProductOutbound.Code, 1, true);
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
            var outboundDetails = productOutboundRepository.GetAllProductOutboundDetailByOutboundId(Id).AsEnumerable()
                    .Select(x => new ProductOutboundDetailViewModel
                    {
                        Id = x.Id,
                        Price = x.Price,
                        ProductId = x.ProductId,
                        ProductName = ProductRepository.GetProductById(x.ProductId.Value).Name,
                        ProductCode = ProductRepository.GetProductById(x.ProductId.Value).Code,
                        ProductOutboundId = x.ProductOutboundId,
                        Quantity = x.Quantity,
                        Unit = x.Unit,
                        LoCode = x.LoCode,
                        ExpiryDate = x.ExpiryDate
                    }).ToList();

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

            foreach (var item in outboundDetails)
            {
                decimal thanh_tien = item.Quantity.Value * item.Price.Value;
                tong_tien += thanh_tien;

                detailList += "<tr>\r\n"
                 + "<td class=\"text-center\">" + (outboundDetails.ToList().IndexOf(item) + 1) + "</td>\r\n"
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
                         + Erp.BackOffice.Helpers.Common.PhanCachHangNgan(outboundDetails.Sum(x => x.Quantity))
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

        #region Config
        public ViewResult Config()
        {
            var q = settingRepository.GetAlls()
                .Where(item => item.Code == "settingprint")
                .OrderBy(item => item.Note).ToList();

            List<Erp.BackOffice.Areas.Administration.Models.SettingViewModel> model = new List<Areas.Administration.Models.SettingViewModel>();
            AutoMapper.Mapper.Map(q, model);

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(model);
        }
        #endregion

        #region CreateFromInvoice
        public ActionResult CreateFromInvoice(int? InvoiceId, int? WarehouseSourceId)
        {
            var model = new ProductOutboundViewModel();
            model.CreatedDate = DateTime.Now;
            model.DetailList = new List<ProductOutboundDetailViewModel>();
            model.InvoiceId = InvoiceId;
            model.WarehouseSourceId = WarehouseSourceId;

            model.SelectList_WarehouseSource = WarehouseRepository.GetAllWarehouse()
                .Where(x => ("," + Helpers.Common.CurrentUser.WarehouseId + ",").Contains("," + x.Id + ",") == true)
                .Select(item => new
                {
                    Id = item.Id,
                    Name = item.Name
                }).ToList()
                .Select(item => new SelectListItem
            {
                Text = item.Name,
                Value = item.Id + ""
            }).AsEnumerable();

            //Load phiếu xuất theo đơn hàng & Kho hàng
            if (InvoiceId != null)
            {
                var productInvoice = productInvoiceRepository.GetvwProductInvoiceById(InvoiceId.Value);
                if (productInvoice != null && WarehouseSourceId != null)
                {
                    //Kiểm tra xem nếu có xuất kho ghi sổ rồi thì return
                    var productOutbound = productOutboundRepository.GetAllProductOutbound()
                            .Where(item => item.InvoiceId == productInvoice.Id).FirstOrDefault();

                    if (productOutbound != null && productOutbound.IsArchive)
                        return Content("Đã xuất kho cho đơn hàng!");

                    model.InvoiceCode = productInvoice.Code;
                    model.CreatedStaffId = productInvoice.StaffCreateId;
                    model.CreatedStaffName = productInvoice.StaffCreateName;
                    //Lấy danh sách chi tiết đơn hàng
                    var listProductInvoiceDetail = productInvoiceRepository.GetAllvwInvoiceDetailsByInvoiceId(productInvoice.Id).Where(x => x.ProductType == "product")
                        .Select(item => new
                        {
                            ProductId = item.ProductId,
                            Price = item.Price,
                            ProductCode = item.ProductCode,
                            ProductName = item.ProductName,
                            Quantity = item.Quantity
                        }).ToList();

                    //Tạo danh sách chi tiết phiếu xuất tương ứng                    
                    foreach (var item in listProductInvoiceDetail)
                    {
                        var productOutboundDetailViewModel = model.DetailList.Where(i => i.ProductId == item.ProductId).FirstOrDefault();
                        if (productOutboundDetailViewModel == null)
                        {
                            productOutboundDetailViewModel = new ProductOutboundDetailViewModel();
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

                    //Lấy lô/date. Mặc định lấy theo phương pháp nhập trước xuất trước
                    foreach (var item in model.DetailList)
                    {
                        item.ListWarehouseLocationItemViewModel = new List<WarehouseLocationItemViewModel>();
                        var listLocationItemExits = warehouseLocationItemRepository.GetAllWarehouseLocationItem()
                            .Where(q => q.ProductId == item.ProductId && q.WarehouseId == WarehouseSourceId && q.IsOut == false)
                            .OrderBy(x => x.ExpiryDate)
                            .Take(item.Quantity.Value);

                        AutoMapper.Mapper.Map(listLocationItemExits, item.ListWarehouseLocationItemViewModel);
                    }

                    model.TotalAmount = model.DetailList.Sum(item => item.Quantity * item.Price);
                }
            }

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateFromInvoice(ProductOutboundViewModel model)
        {
            ProductOutbound productOutbound = null;
            if (model.InvoiceId != null)
            {
                productOutbound = productOutboundRepository.GetAllProductOutbound()
                    .Where(x => ("," + Helpers.Common.CurrentUser.DrugStore + ",").Contains("," + x.BranchId + ",") == true && x.InvoiceId == model.InvoiceId).FirstOrDefault();

                if (productOutbound != null && productOutbound.IsArchive)
                    return Content("Phiếu xuất kho cho đơn hàng này đã được ghi sổ!");
            }

            if (ModelState.IsValid)
            {
                //Lấy danh sách inventory các sản phẩm đang có trong phiếu xuất của kho nguồn nếu có
                var list_InventoryWHSource = inventoryRepository.GetAllInventoryByWarehouseId(model.WarehouseSourceId.Value).ToList();

                //Duyệt qua hết chi tiết phiếu xuất, kiểm tra số lượng tồn kho
                foreach (var item in model.DetailList)
                {
                    //Kiểm tra tình trạng đảm bảo SP và SL của SP trong kho đáp ứng cho đơn hàng. Xảy ra trong tình huống client có tình can thiệp làm sai các thông số đầu vào
                    var product = list_InventoryWHSource.Where(x => x.ProductId == item.ProductId).FirstOrDefault();
                    if (product == null || product.Quantity < item.Quantity)
                    {
                        TempData[Globals.FailedMessageKey] = item.ProductCode + " - " + item.ProductName + ": không đáp ứng đủ số lượng";
                        if (model.InvoiceId != null)
                            return RedirectToAction("CreateFromInvoice", new { InvoiceId = model.InvoiceId, WarehouseSourceId = model.WarehouseSourceId });
                        else
                            return RedirectToAction("CreateFromInvoice");
                    }
                }

                //Nếu đã tạo phiếu xuất rồi thì cập nhật
                if (productOutbound != null)//Cập nhật
                {
                    productOutbound.CreatedStaffId = model.CreatedStaffId;
                    productOutbound.ModifiedUserId = WebSecurity.CurrentUserId;
                    productOutbound.ModifiedDate = DateTime.Now;
                    productOutbound.TotalAmount = model.DetailList.Sum(x => x.Price * x.Quantity);
                    productOutboundRepository.UpdateProductOutbound(productOutbound);

                    //Xóa chi tiết cũ và thêm chi tiết mới
                    var listProductOutboundDetail_old = productOutboundRepository.GetAllProductOutboundDetailByOutboundId(productOutbound.Id)
                        .Select(item => item.Id).ToList();

                    foreach (var id in listProductOutboundDetail_old)
                    {
                        productOutboundRepository.DeleteProductOutboundDetail(id);
                    }

                    foreach (var item in model.DetailList)
                    {
                        ProductOutboundDetail productOutboundDetail = new ProductOutboundDetail();
                        AutoMapper.Mapper.Map(item, productOutboundDetail);
                        productOutboundDetail.ProductOutboundId = productOutbound.Id;
                        productOutboundDetail.IsDeleted = false;
                        productOutboundDetail.CreatedUserId = WebSecurity.CurrentUserId;
                        productOutboundDetail.ModifiedUserId = WebSecurity.CurrentUserId;
                        productOutboundDetail.CreatedDate = DateTime.Now;
                        productOutboundDetail.ModifiedDate = DateTime.Now;
                        productOutboundRepository.InsertProductOutboundDetail(productOutboundDetail);
                    }

                }
                else//Thêm mới
                {
                    CreateFromInvoice(productOutboundRepository, model, model.InvoiceCode);
                }
                //Chọn lô/date cho mỗi chi tiết phiếu xuất
                UpdateLote(productOutboundRepository, warehouseLocationItemRepository, productOutbound.Id, model.LocationItemList);


                //Ghi sổ chứng từ
                Archive(productOutbound, TempData);

                return RedirectToAction("Detail", new { Id = productOutbound.Id });
            }

            return RedirectToAction("Index", new { Id = productOutbound.Id });
        }


        public static ProductOutbound CreateFromInvoice(IProductOutboundRepository productOutboundRepository,
            ProductOutboundViewModel model,
            string productInvoiceCode)
        {
            Erp.Domain.Sale.Repositories.WarehouseLocationItemRepository warehouseLocationItemRepository = new Erp.Domain.Sale.Repositories.WarehouseLocationItemRepository(new Domain.Sale.ErpSaleDbContext());
            Erp.Domain.Sale.Repositories.WarehouseRepository warehouseRepository = new Erp.Domain.Sale.Repositories.WarehouseRepository(new Domain.Sale.ErpSaleDbContext());
            var productOutbound = new Domain.Sale.Entities.ProductOutbound();
            AutoMapper.Mapper.Map(model, productOutbound);
            productOutbound.IsDeleted = false;
            productOutbound.CreatedUserId = WebSecurity.CurrentUserId;
            productOutbound.ModifiedUserId = WebSecurity.CurrentUserId;
            productOutbound.CreatedDate = DateTime.Now;
            productOutbound.ModifiedDate = DateTime.Now;
            productOutbound.Type = "invoice";
            var branch = warehouseRepository.GetWarehouseById(productOutbound.WarehouseSourceId.Value);
            productOutbound.BranchId = branch.BranchId;
            productOutbound.TotalAmount = model.DetailList.Sum(x => x.Price * x.Quantity);
            productOutboundRepository.InsertProductOutbound(productOutbound);

            //Cập nhật lại mã xuất kho
            string prefix = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_Outbound");
            productOutbound.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix, productOutbound.Id);
            productOutboundRepository.UpdateProductOutbound(productOutbound);
            // chi tiết phiếu xuất
            foreach (var item in model.DetailList)
            {
                ProductOutboundDetail productOutboundDetail = new ProductOutboundDetail();
                AutoMapper.Mapper.Map(item, productOutboundDetail);
                productOutboundDetail.IsDeleted = false;
                productOutboundDetail.CreatedUserId = WebSecurity.CurrentUserId;
                productOutboundDetail.ModifiedUserId = WebSecurity.CurrentUserId;
                productOutboundDetail.CreatedDate = DateTime.Now;
                productOutboundDetail.ModifiedDate = DateTime.Now;
                productOutboundDetail.ProductOutboundId = productOutbound.Id;
                productOutboundRepository.InsertProductOutboundDetail(productOutboundDetail);

            }
            //cập nhật vị trí các sản phẩm đã xuất kho
            //
            //Thêm vào quản lý chứng từ
            TransactionController.Create(new TransactionViewModel
            {
                TransactionModule = "ProductOutbound",
                TransactionCode = productOutbound.Code,
                TransactionName = "Xuất kho bán hàng"
            });

            //Thêm chứng từ liên quan
            TransactionController.CreateRelationship(new TransactionRelationshipViewModel
            {
                TransactionA = productOutbound.Code,
                TransactionB = productInvoiceCode
            });

            return productOutbound;
        }


        public static ProductOutbound AutoCreateProductOutboundFromProductInvoice(IProductOutboundRepository productOutboundRepository,
         ProductOutboundViewModel model,
         string productInvoiceCode)
        {
            Erp.Domain.Sale.Repositories.WarehouseRepository warehouseRepository = new Erp.Domain.Sale.Repositories.WarehouseRepository(new Domain.Sale.ErpSaleDbContext());
            //Erp.Domain.Sale.Repositories.WarehouseLocationItemRepository warehouseLocationItemRepository = new Erp.Domain.Sale.Repositories.WarehouseLocationItemRepository(new Domain.Sale.ErpSaleDbContext());
            //AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.vwWarehouseLocationItem, Domain.Sale.Entities.WarehouseLocationItem>();


            var productOutbound = new Domain.Sale.Entities.ProductOutbound();
            AutoMapper.Mapper.Map(model, productOutbound);
            productOutbound.IsDeleted = false;
            productOutbound.CreatedUserId = WebSecurity.CurrentUserId;
            productOutbound.ModifiedUserId = WebSecurity.CurrentUserId;
            productOutbound.CreatedDate = DateTime.Now;
            productOutbound.ModifiedDate = DateTime.Now;
            productOutbound.Type = "invoice";
            var branch = warehouseRepository.GetWarehouseById(productOutbound.WarehouseSourceId.Value);
            productOutbound.BranchId = branch.BranchId;
            productOutbound.TotalAmount = model.DetailList.Sum(x => x.Price * x.Quantity);
            productOutboundRepository.InsertProductOutbound(productOutbound);

            //Cập nhật lại mã xuất kho
            string prefix = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_Outbound");
            productOutbound.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix, productOutbound.Id);
            productOutboundRepository.UpdateProductOutbound(productOutbound);
            // chi tiết phiếu xuất
            foreach (var item in model.DetailList)
            {
                ProductOutboundDetail productOutboundDetail = new ProductOutboundDetail();
                AutoMapper.Mapper.Map(item, productOutboundDetail);
                productOutboundDetail.IsDeleted = false;
                productOutboundDetail.CreatedUserId = WebSecurity.CurrentUserId;
                productOutboundDetail.ModifiedUserId = WebSecurity.CurrentUserId;
                productOutboundDetail.CreatedDate = DateTime.Now;
                productOutboundDetail.ModifiedDate = DateTime.Now;
                productOutboundDetail.ProductOutboundId = productOutbound.Id;
                productOutboundRepository.InsertProductOutboundDetail(productOutboundDetail);
                //var list=new List<WarehouseLocationItem>();
                //    var listLocationItemExits = warehouseLocationItemRepository.GetAllvwWarehouseLocationItem()
                //        .Where(q => q.ProductId == item.ProductId && q.BranchId == productOutbound.BranchId&&q.IsSale==true && q.IsOut == false&&q.ProductOutboundId==null&&q.ProductOutboundDetailId==null)
                //        .OrderBy(x => x.ExpiryDate)
                //        .Take(item.Quantity.Value).ToList();
                //    AutoMapper.Mapper.Map(listLocationItemExits, list);
                //    for (int i = 0; i < list.Count(); i++)
                //    {
                //        list[i].ProductOutboundId = productOutbound.Id;
                //        list[i].ProductOutboundDetailId = productOutboundDetail.Id;
                //        list[i].ModifiedDate = DateTime.Now;
                //        list[i].ModifiedUserId = WebSecurity.CurrentUserId;
                //        warehouseLocationItemRepository.UpdateWarehouseLocationItem(list[i]);
                //    }
            }
            //cập nhật vị trí các sản phẩm đã xuất kho
            //
            //Thêm vào quản lý chứng từ
            TransactionController.Create(new TransactionViewModel
            {
                TransactionModule = "ProductOutbound",
                TransactionCode = productOutbound.Code,
                TransactionName = "Xuất kho bán hàng"
            });

            //Thêm chứng từ liên quan
            TransactionController.CreateRelationship(new TransactionRelationshipViewModel
            {
                TransactionA = productOutbound.Code,
                TransactionB = productInvoiceCode
            });

            return productOutbound;
        }

        public static void UpdateLote(IProductOutboundRepository productOutboundRepository,
            IWarehouseLocationItemRepository warehouseLocationItemRepository,
            int productOutboundId,
            List<WarehouseLocationItemViewModel> LocationItemList)
        {

            if (LocationItemList != null)
            {
                var listProductOutboundDetail = productOutboundRepository.GetAllProductOutboundDetailByOutboundId(productOutboundId)
                    .Select(item => new { item.Id, item.ProductId }).ToList();

                LocationItemList = LocationItemList.Where(x => x.Id != null && x.Id != 0).ToList();

                if (LocationItemList.Count != 0)
                {
                    foreach (var item in LocationItemList)
                    {
                        var ProductOutboundDetail = listProductOutboundDetail.Where(x => x.ProductId == item.ProductId).FirstOrDefault();
                        var LocationItem = warehouseLocationItemRepository.GetWarehouseLocationItemById(item.Id.Value);
                        if (LocationItem != null)
                        {
                            LocationItem.ProductOutboundId = productOutboundId;
                            LocationItem.ProductOutboundDetailId = ProductOutboundDetail != null ? ProductOutboundDetail.Id : 0;
                            LocationItem.ModifiedDate = DateTime.Now;
                            LocationItem.ModifiedUserId = WebSecurity.CurrentUserId;
                            warehouseLocationItemRepository.UpdateWarehouseLocationItem(LocationItem);
                        }
                    }
                }
            }
        }

        public PartialViewResult LoadProductItem(int OrderNo, int ProductId, string ProductName, string Unit, int Quantity, decimal Price, string ProductCode, string ProductType, int QuantityInventory, int WarehouseId, string LoCode, string ExpiryDate)
        {
            var model = new ProductOutboundDetailViewModel();
            model.OrderNo = OrderNo;
            model.ProductId = ProductId;
            model.ProductName = ProductName;
            model.Unit = Unit;
            model.Quantity = Quantity;
            model.Price = Price;
            model.ProductCode = ProductCode;
            model.ProductType = ProductType;
            model.QuantityInInventory = QuantityInventory;
            model.LoCode = LoCode;
            if (!string.IsNullOrEmpty(ExpiryDate))
                model.ExpiryDate = Convert.ToDateTime(ExpiryDate);
            model.ListWarehouseLocationItemViewModel = new List<WarehouseLocationItemViewModel>();

            var listLocationItemExits = warehouseLocationItemRepository.GetAllWarehouseLocationItem()
                .Where(q => q.ProductId == ProductId && q.WarehouseId == WarehouseId && q.IsOut == false)
                .OrderBy(x => x.ExpiryDate)
                .Take(Quantity)
                .ToList();

            AutoMapper.Mapper.Map(listLocationItemExits, model.ListWarehouseLocationItemViewModel);

            return PartialView(model);
        }
        #endregion

        #region Detail
        public ActionResult Detail(int? Id, string TransactionCode)
        {
            var ProductOutbound = new vwProductOutbound();
            if (Id != null)
                ProductOutbound = productOutboundRepository.GetvwProductOutboundFullById(Id.Value);

            if (!string.IsNullOrEmpty(TransactionCode))
                ProductOutbound = productOutboundRepository.GetAllvwProductOutboundFull().Where(item => item.Code == TransactionCode).FirstOrDefault();

            if (ProductOutbound != null)
            {
                var model = new ProductOutboundViewModel();
                AutoMapper.Mapper.Map(ProductOutbound, model);

                //Kiểm tra cho phép sửa chứng từ này hay không
                model.AllowEdit = Helpers.Common.KiemTraNgaySuaChungTu(model.CreatedDate.Value);

                var wh_source = WarehouseRepository.GetWarehouseById(model.WarehouseSourceId.Value);
                model.WarehouseSourceName = wh_source.Name;

                //Lấy hóa đơn để hiện thị
                if (model.InvoiceId != null)
                {
                    var order = productInvoiceRepository.GetProductInvoiceById(model.InvoiceId.Value);
                    if (order != null)
                    {
                        model.InvoiceCode = order.Code;
                    }
                }
                else
                {
                    if (model.WarehouseDestinationId != null)
                    {
                        var wh_destination = WarehouseRepository.GetWarehouseById(model.WarehouseDestinationId.Value);
                        if (wh_destination != null)
                            model.WarehouseDestinationName = wh_destination.Name;
                        else
                            model.WarehouseDestinationName = "Khác";
                    }
                }

                var outboundDetails = productOutboundRepository.GetAllvwProductOutboundDetailByOutboundId(model.Id).AsEnumerable()
                    .Select(x => new ProductOutboundDetailViewModel
                    {
                        ProductId = x.ProductId,
                        Price = x.Price,
                        Quantity = x.Quantity,
                        Unit = x.Unit,
                        ProductName = x.ProductName,
                        ProductCode = x.ProductCode,
                        LoCode = x.LoCode,
                        ExpiryDate = x.ExpiryDate,
                        Id = x.Id
                    }).OrderBy(x => x.Id).ToList();

                model.DetailList = outboundDetails;

                model.CreatedUserName = userRepository.GetUserById(model.CreatedUserId.Value).FullName;

                ViewBag.SuccessMessage = TempData["SuccessMessage"];
                ViewBag.FailedMessage = TempData["FailedMessage"];

                return View(model);
            }

            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }
        #endregion

        #region Archive
        [HttpPost]
        public ActionResult Archive(int Id, bool? IsPopup)
        {
            if (Request["Submit"] == "Save")
            {
                var productOutbound = productOutboundRepository.GetProductOutboundById(Id);

                //Kiểm tra cho phép sửa chứng từ này hay không
                if (Helpers.Common.KiemTraNgaySuaChungTu(productOutbound.CreatedDate.Value) == false)
                {
                    return RedirectToAction("Detail", new { Id = productOutbound.Id });
                }
                using (var scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        Archive(productOutbound, TempData);
                        scope.Complete();
                    }
                    catch (DbUpdateException)
                    {
                        TempData[Globals.FailedMessageKey] = App_GlobalResources.Wording.Error;
                        return RedirectToAction("Detail", new { Id = Id });
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

        public static void Archive(ProductOutbound productOutbound,
            TempDataDictionary TempData)
        {
            ProductOutboundRepository productOutboundRepository = new Domain.Sale.Repositories.ProductOutboundRepository(new Domain.Sale.ErpSaleDbContext());
            //Cập nhật lại tồn kho cho những sp trong phiếu này
            var detailList = productOutboundRepository.GetAllvwProductOutboundDetailByOutboundId(productOutbound.Id)
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
            foreach (var item in detailList)
            {
                var error = InventoryController.Check(item.ProductName, item.ProductId, item.LoCode, item.ExpiryDate, productOutbound.WarehouseSourceId.Value, 0, item.Quantity);
                check += error;
            }

            if (string.IsNullOrEmpty(check))
            {
                //Khi đã hợp lệ thì mới update
                foreach (var item in detailList)
                {
                    InventoryController.Update(item.ProductName, item.ProductId, item.LoCode, item.ExpiryDate, productOutbound.WarehouseSourceId.Value, 0, item.Quantity);
                }

                productOutbound.IsArchive = true;
                productOutboundRepository.UpdateProductOutbound(productOutbound);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.ArchiveSuccess;
            }
            else
            {
                TempData[Globals.FailedMessageKey] = App_GlobalResources.Wording.ArchiveFail + check;
            }
        }


        public static void Archive(ProductOutbound productOutbound)
        {
            ProductOutboundRepository productOutboundRepository = new Domain.Sale.Repositories.ProductOutboundRepository(new Domain.Sale.ErpSaleDbContext());
            //Cập nhật lại tồn kho cho những sp trong phiếu này
            var detailList = productOutboundRepository.GetAllvwProductOutboundDetailByOutboundId(productOutbound.Id)
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
            foreach (var item in detailList)
            {
                var error = InventoryController.Check(item.ProductName, item.ProductId, item.LoCode, item.ExpiryDate, productOutbound.WarehouseSourceId.Value, 0, item.Quantity);
                check += error;
            }

            if (string.IsNullOrEmpty(check))
            {
                //Khi đã hợp lệ thì mới update
                foreach (var item in detailList)
                {
                    InventoryController.Update(item.ProductName, item.ProductId, item.LoCode, item.ExpiryDate, productOutbound.WarehouseSourceId.Value, 0, item.Quantity);
                }

                productOutbound.IsArchive = true;
                productOutboundRepository.UpdateProductOutbound(productOutbound);

            }
        }



        public static void Archive_mobile(ProductOutbound productOutbound, int CurrentUserId)
        {
            ProductOutboundRepository productOutboundRepository = new Domain.Sale.Repositories.ProductOutboundRepository(new Domain.Sale.ErpSaleDbContext());
            //WarehouseLocationItemRepository warehouseLocationItemRepository = new Domain.Sale.Repositories.WarehouseLocationItemRepository(new Domain.Sale.ErpSaleDbContext());
            //Lấy lại chi tiết của phiếu kho từ db
            var detailList = productOutboundRepository.GetAllvwProductOutboundDetailByOutboundId(productOutbound.Id)
                .Select(item => new
                {
                    ProductName = item.ProductCode + " - " + item.ProductName,
                    ProductId = item.ProductId.Value,
                    Quantity = item.Quantity.Value,
                    LoCode = item.LoCode,
                    ExpiryDate = item.ExpiryDate
                }).ToList();


            //Kiểm tra dữ liệu sau khi ghi sổ có hợp lệ không
            string check = "";
            foreach (var item in detailList)
            {
                var error = InventoryController.Check_mobile(item.ProductName, item.ProductId, item.LoCode, item.ExpiryDate, productOutbound.WarehouseSourceId.Value, 0, item.Quantity, CurrentUserId);
                check += error;
            }

            if (string.IsNullOrEmpty(check))
            {
                //Khi đã hợp lệ thì mới update tồn kho
                foreach (var item in detailList)
                {
                    InventoryController.Update_mobile(item.ProductName, item.ProductId, item.LoCode, item.ExpiryDate, productOutbound.WarehouseSourceId.Value, 0, item.Quantity, CurrentUserId);
                }

                productOutbound.IsArchive = true;
                productOutboundRepository.UpdateProductOutbound(productOutbound);

            }
        }
        #endregion

        #region UnArchive
        [HttpPost]
        public ActionResult UnArchive(int Id, bool? IsPopup)
        {
            if (Request["Submit"] == "Save")
            {
                var productOutbound = productOutboundRepository.GetProductOutboundById(Id);

                //Kiểm tra cho phép sửa chứng từ này hay không
                if (Helpers.Common.KiemTraNgaySuaChungTu(productOutbound.CreatedDate.Value) == false)
                {
                    return RedirectToAction("Detail", new { Id = productOutbound.Id });
                }

                //Cập nhật lại tồn kho cho những sp trong phiếu này
                var detailList = productOutboundRepository.GetAllvwProductOutboundDetailByOutboundId(productOutbound.Id)
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
                foreach (var item in detailList)
                {
                    var error = InventoryController.Check(item.ProductName, item.ProductId, item.LoCode, item.ExpiryDate, productOutbound.WarehouseSourceId.Value, item.Quantity, 0);
                    check += error;
                }

                if (string.IsNullOrEmpty(check))
                {
                    //Khi đã hợp lệ thì mới update
                    foreach (var item in detailList)
                    {
                        InventoryController.Update(item.ProductName, item.ProductId, item.LoCode, item.ExpiryDate, productOutbound.WarehouseSourceId.Value, item.Quantity, 0);
                    }

                    productOutbound.IsArchive = false;
                    productOutboundRepository.UpdateProductOutbound(productOutbound);
                    TempData[Globals.SuccessMessageKey] = "Đã bỏ ghi sổ";
                }
                else
                {
                    TempData[Globals.FailedMessageKey] = App_GlobalResources.Wording.ArchiveFail + check;
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

        public static void UnArchiveAndDelete(ProductOutbound productOutbound)
        {
            ProductOutboundRepository productOutboundRepository = new Domain.Sale.Repositories.ProductOutboundRepository(new Domain.Sale.ErpSaleDbContext());
            WarehouseLocationItemRepository warehouseLocationItemRepository = new Domain.Sale.Repositories.WarehouseLocationItemRepository(new Domain.Sale.ErpSaleDbContext());
            //Cập nhật lại tồn kho cho những sp trong phiếu này
            var detailList = productOutboundRepository.GetAllvwProductOutboundDetailByOutboundId(productOutbound.Id)
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
            foreach (var item in detailList)
            {
                var error = InventoryController.Check(item.ProductName, item.ProductId, item.LoCode, item.ExpiryDate, productOutbound.WarehouseSourceId.Value, item.Quantity, 0);
                check += error;
            }

            if (string.IsNullOrEmpty(check))
            {
                //Khi đã hợp lệ thì mới update
                foreach (var item in detailList)
                {
                    InventoryController.Update(item.ProductName, item.ProductId, item.LoCode, item.ExpiryDate, productOutbound.WarehouseSourceId.Value, item.Quantity, 0);
                }

                productOutbound.IsArchive = false;
                productOutbound.IsDeleted = true;
                productOutboundRepository.UpdateProductOutbound(productOutbound);
                //TempData[Globals.SuccessMessageKey] = "Đã bỏ ghi sổ";
            }
            else
            {
                //TempData[Globals.FailedMessageKey] = App_GlobalResources.Wording.ArchiveFail + check;
            }
        }

        //public PartialViewResult LoadProductItem(int OrderNo, int ProductId, string ProductName, string Unit, int Quantity, decimal Price, string ProductCode, string ProductType, int QuantityInventory, int WarehouseId)
        //{
        //    var model = new ProductOutboundDetailViewModel();
        //    model.OrderNo = OrderNo;
        //    model.ProductId = ProductId;
        //    model.ProductName = ProductName;
        //    model.Unit = Unit;
        //    model.Quantity = Quantity;
        //    model.Price = Price;
        //    model.ProductCode = ProductCode;
        //    model.ProductType = ProductType;
        //    model.QuantityInInventory = QuantityInventory;

        //    model.ListWarehouseLocationItemViewModel = new List<WarehouseLocationItemViewModel>();

        //    var listLocationItemExits = warehouseLocationItemRepository.GetAllWarehouseLocationItem()
        //        .Where(q => q.ProductId == ProductId && q.WarehouseId == WarehouseId && q.IsOut == false)
        //        .OrderBy(x => x.ExpiryDate)
        //        .Take(Quantity)
        //        .ToList();

        //    AutoMapper.Mapper.Map(listLocationItemExits, model.ListWarehouseLocationItemViewModel);

        //    return PartialView(model);
        //}
        #endregion

        #region UpdateData
        //public ActionResult UpdateData()
        //{
        //AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.vwProductOutboundDetail, Domain.Sale.Entities.ProductOutboundDetail>();
        //var productInboundDetail = productOutboundRepository.GetAllvwProductOutboundDetail().Where(x=>x.ProductType=="service").ToList();
        //List<ProductOutboundDetail> list = new List<ProductOutboundDetail>();
        //AutoMapper.Mapper.Map(productInboundDetail, list);
        //for (int i = 0; i < list.Count(); i++)
        //{
        //    productOutboundRepository.DeleteProductOutboundDetail(list[i].Id);
        //}

        //var productInboundDetail = productOutboundRepository.GetAllvwProductOutboundDetail().ToList();
        //var group = productInboundDetail.GroupBy(x => x.ProductOutboundId).ToList();
        //for (int i = 0; i < group.Count(); i++)
        //{
        //    var aa = productOutboundRepository.GetProductOutboundById(group[i].Key.Value);
        //    aa.TotalAmount = group[i].Sum(x => (x.Price * x.Quantity));
        //    productOutboundRepository.UpdateProductOutbound(aa);
        //}
        //return RedirectToAction("Index");
        //}
        #endregion


        #region Edit
        public ActionResult Edit(int Id)
        {
            var productOutbound = productOutboundRepository.GetvwProductOutboundById(Id);
            ProductOutboundTransferViewModel model = new ProductOutboundTransferViewModel();
            model.DetailList = new List<ProductOutboundDetailViewModel>();
            if (productOutbound != null && productOutbound.IsDeleted == false)
            {
                AutoMapper.Mapper.Map(productOutbound, model);
                var productList = inventoryRepository.GetAllvwInventoryByWarehouseId(productOutbound.WarehouseSourceId.Value)
                    .Where(x => x.Quantity != null && x.Quantity > 0).AsEnumerable()
                   .Select(item => new ProductViewModel
                   {
                       Id = item.ProductId,
                       Code = item.ProductCode,
                       Name = item.ProductName,
                       CategoryCode = item.CategoryCode,
                       PriceOutbound = item.ProductPriceOutbound,
                       Unit = item.ProductUnit,
                       QuantityTotalInventory = item.Quantity,
                       LoCode = item.LoCode,
                       ExpiryDate = item.ExpiryDate,
                       Image_Name = item.Image_Name
                   }).ToList();

                ViewBag.productList = productList;
                var detailList = productOutboundRepository.GetAllvwProductOutboundDetailByOutboundId(productOutbound.Id).ToList();
                AutoMapper.Mapper.Map(detailList, model.DetailList);
                //Thêm số lượng tồn kho cho chi tiết đơn hàng đã được thêm
                if (model.DetailList != null && model.DetailList.Count > 0)
                {
                    int n = 0;
                    foreach (var item in model.DetailList)
                    {
                        var a = productList.Where(x => x.Id == item.ProductId && x.LoCode == item.LoCode && x.ExpiryDate == item.ExpiryDate);
                        item.QuantityInInventory = a.Count() > 0 ? a.FirstOrDefault().QuantityTotalInventory : 0;
                        item.OrderNo = n;
                        n++;
                    }
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ProductOutboundTransferViewModel model)
        {
            if (ModelState.IsValid)
            {
                //duyệt qua danh sách sản phẩm mới xử lý tình huống user chọn 2 sản phầm cùng id
                List<ProductOutboundDetail> outboundDetails = new List<ProductOutboundDetail>();
                foreach (var group in model.DetailList)
                {
                    //var product = ProductRepository.GetProductById(group.Key.Value);

                    outboundDetails.Add(new ProductOutboundDetail
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
                        LoCode = group.LoCode,
                        ExpiryDate = group.ExpiryDate
                    });
                }
                var productOutbound = productOutboundRepository.GetProductOutboundById(model.Id);
                //cập nhật phiếu xuất kho theo đơn xuất kho ở trên
                productOutbound.ModifiedUserId = WebSecurity.CurrentUserId;
                productOutbound.ModifiedDate = DateTime.Now;
                productOutbound.IsDone = false;
                productOutbound.IsArchive = false;
                productOutbound.CreatedStaffId = model.CreatedUserId;
                productOutbound.Note = model.Note;
                productOutbound.TotalAmount = outboundDetails.Sum(item => item.Quantity * item.Price);
                productOutboundRepository.UpdateProductOutbound(productOutbound);
                var detail_delete = productOutboundRepository.GetAllProductOutboundDetailByOutboundId(model.Id).Select(x => x.Id).ToList();
                //xóa chi tiết phiếu xuất cũ
                for (int i = 0; i < detail_delete.Count(); i++)
                {
                    productOutboundRepository.DeleteProductOutboundDetail(detail_delete[i]);
                }
                //Update các lô/date đã xuất = false
                //var listWarehouseLocationItem = warehouseLocationItemRepository.GetAllWarehouseLocationItem()
                //    .Where(item => item.ProductOutboundId == productOutbound.Id).ToList();

                //foreach (var item in listWarehouseLocationItem)
                //{
                //    item.IsOut = false;
                //    item.ProductOutboundId = null;
                //    item.ProductOutboundDetailId = null;
                //    item.ModifiedUserId = WebSecurity.CurrentUserId;
                //    item.ModifiedDate = DateTime.Now;
                //    warehouseLocationItemRepository.UpdateWarehouseLocationItem(item);
                //}
                //insert chi tiết phiếu xuất mới
                foreach (var item in outboundDetails)
                {
                    ProductOutboundDetail productOutboundDetail = new ProductOutboundDetail();
                    productOutboundDetail.ProductId = item.ProductId;
                    productOutboundDetail.Price = item.Price;
                    productOutboundDetail.Quantity = item.Quantity;
                    productOutboundDetail.Unit = item.Unit;
                    productOutboundDetail.LoCode = item.LoCode;
                    productOutboundDetail.ExpiryDate = item.ExpiryDate;
                    productOutboundDetail.IsDeleted = false;
                    productOutboundDetail.CreatedUserId = WebSecurity.CurrentUserId;
                    productOutboundDetail.ModifiedUserId = WebSecurity.CurrentUserId;
                    productOutboundDetail.CreatedDate = DateTime.Now;
                    productOutboundDetail.ModifiedDate = DateTime.Now;
                    productOutboundDetail.ProductOutboundId = productOutbound.Id;
                    productOutboundRepository.InsertProductOutboundDetail(productOutboundDetail);


                    //var listLocationItemExits = warehouseLocationItemRepository.GetAllWarehouseLocationItem()
                    //    .Where(q => q.ProductId == item.ProductId && q.WarehouseId == productOutbound.WarehouseSourceId && q.IsOut == false)
                    //    .OrderBy(x => x.ExpiryDate)
                    //    .Take(item.Quantity.Value).ToList();
                    //for (int i = 0; i < listLocationItemExits.Count(); i++)
                    //{
                    //    listLocationItemExits[i].ProductOutboundId = productOutbound.Id;
                    //    listLocationItemExits[i].ProductOutboundDetailId = productOutboundDetail != null ? productOutboundDetail.Id : 0;
                    //    listLocationItemExits[i].ModifiedDate = DateTime.Now;
                    //    listLocationItemExits[i].ModifiedUserId = WebSecurity.CurrentUserId;
                    //    warehouseLocationItemRepository.UpdateWarehouseLocationItem(listLocationItemExits[i]);
                    //}
                }

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                return RedirectToAction("Detail", "ProductOutbound", new { area = "Sale", Id = model.Id });
            }
            return View(model);
        }
        #endregion

        #region Delete
        [HttpPost]
        public ActionResult Delete(int Id, string Note)
        {
            var productOutbound = productOutboundRepository.GetProductOutboundById(Id);
            if (productOutbound != null)
            {
                //Kiểm tra phân quyền Chi nhánh
                if (!(Filters.SecurityFilter.IsAdmin() || ("," + Helpers.Common.CurrentUser.DrugStore + ",").Contains("," + productOutbound.BranchId + ",") == true))
                {
                    return Content("Mẫu tin không tồn tại! Không có quyền truy cập!");
                }

                productOutbound.ModifiedUserId = WebSecurity.CurrentUserId;
                productOutbound.ModifiedDate = DateTime.Now;
                productOutbound.IsDeleted = true;
                productOutbound.IsArchive = false;
                productOutbound.Note = Note;
                productOutboundRepository.UpdateProductOutbound(productOutbound);

                return RedirectToAction("Detail", new { Id = productOutbound.Id });
            }

            return RedirectToAction("Index");
        }
        #endregion
        [HttpPost]
        public JsonResult ValidMaxDebitAmount(int WarehouseId, decimal? amount)
        {
            try
            {
                decimal? DinhMucKho = 0;
                decimal? MaxDebitAmount = 0;
                bool Valid = Erp.BackOffice.Sale.Controllers.ProductOutboundController.ValidMaxDebit(WarehouseId, amount, ref MaxDebitAmount, ref DinhMucKho);
                if (Valid == false)
                {
                    var message = "Số tiền nhà thuốc nợ vượt quá giới hạn cho phép (" + Erp.BackOffice.Helpers.Common.PhanCachHangNgan2(DinhMucKho) + "/" + Erp.BackOffice.Helpers.Common.PhanCachHangNgan2(MaxDebitAmount) + "). Không thể tạo phiếu";
                    return Json(new { Result = "error", Message = message },
                            JsonRequestBehavior.AllowGet);
                }
                return Json(new { Result = "success", Message = App_GlobalResources.Wording.UpdateSuccess },
                                   JsonRequestBehavior.AllowGet);
            }
            catch (DbUpdateException)
            {
                return Json(new { Result = "error", Message = "Lỗi không xác định!!!" },
                                JsonRequestBehavior.AllowGet);
            }
        }

        public static bool ValidMaxDebit(int WarehouseId, decimal? amount, ref decimal? MaxDebitAmount, ref decimal? DinhMucKho)
        {
            try
            {
                Erp.Domain.Sale.Repositories.WarehouseRepository warehouseRepository = new Erp.Domain.Sale.Repositories.WarehouseRepository(new Erp.Domain.Sale.ErpSaleDbContext());
                Erp.Domain.Sale.Repositories.RequestInboundRepository requestInboundRepository = new Erp.Domain.Sale.Repositories.RequestInboundRepository(new Erp.Domain.Sale.ErpSaleDbContext());
                Erp.Domain.Staff.Repositories.BranchRepository branchRepository = new Erp.Domain.Staff.Repositories.BranchRepository(new Erp.Domain.Staff.ErpStaffDbContext());
                var wh = warehouseRepository.GetWarehouseById(WarehouseId);
                var branch = branchRepository.GetBranchById(wh.BranchId.Value);
                var rquest = requestInboundRepository.GetAllRequestInbound().Where(x => x.BranchId == branch.Id && (x.Status == "new" || x.Status == "ApprovedASM" || x.Status == "ApprovedKT" || x.Status == "shipping")).ToList();
                var ProductInvoiceList = SqlHelper.QuerySP<ProductInvoiceViewModel>("spSale_LiabilitiesDrugStore", new
                {
                    StartDate = "",
                    EndDate = "",
                    branchId = wh.BranchId,
                    CityId = "",
                    DistrictId = ""
                }).ToList();
                decimal Liabilities = 0;
                var invoice = ProductInvoiceList.Where(x => x.AccountancyUserId == null).Sum(x => x.TotalAmount);
                Liabilities = invoice;
                MaxDebitAmount = branch.MaxDebitAmount;
                var tonkho = Domain.Helper.SqlHelper.QuerySP<InventoryViewModel>("spSale_Get_Inventory", new { WarehouseId = wh.Id, HasQuantity = "1", ProductCode = "", ProductName = "", CategoryCode = "", ProductGroup = "", BranchId = "", LoCode = "", ProductId = "", ExpiryDate = "" }).ToList();
                amount = amount == null ? 0 : amount;
                decimal amount_request = rquest.Sum(x => x.TotalAmount.Value);
                DinhMucKho = tonkho.Sum(x => x.Quantity * x.ProductPriceOutbound) + invoice + amount + amount_request;
                if (MaxDebitAmount < DinhMucKho)
                {
                    return false;
                }
            }
            catch (DbUpdateException)
            {
                return false;
            }
            return true;
        }
    }
}
