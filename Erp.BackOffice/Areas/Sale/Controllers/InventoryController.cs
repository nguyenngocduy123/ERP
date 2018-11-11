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
using System.Web.Script.Serialization;
using Erp.Domain.Sale.Repositories;
using System.Data.Entity;
using System.Transactions;

namespace Erp.BackOffice.Sale.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class InventoryController : Controller
    {
        private readonly IWarehouseRepository WarehouseRepository;
        private readonly IProductOrServiceRepository ProductRepository;
        private readonly IInventoryRepository InventoryRepository;
        private readonly IProductInboundRepository ProductInboundRepository;
        private readonly IProductOutboundRepository ProductOutboundRepository;
        private readonly IPhysicalInventoryRepository PhysicalInventoryRepository;
        private readonly IUserRepository userRepository;
        public InventoryController(
            IInventoryRepository _Inventory
            , IProductOrServiceRepository _Product
            , IWarehouseRepository _Warehouse
            , IProductInboundRepository _ProductInbound
            , IProductOutboundRepository _ProductOutbound
            , IPhysicalInventoryRepository _PhysicalInventory
            , IUserRepository _user
            )
        {
            WarehouseRepository = _Warehouse;
            ProductRepository = _Product;
            InventoryRepository = _Inventory;
            ProductInboundRepository = _ProductInbound;
            ProductOutboundRepository = _ProductOutbound;
            userRepository = _user;
            PhysicalInventoryRepository = _PhysicalInventory;

        }

        #region Index
        public ViewResult Index(string WarehouseId, string branchId, string conHang, string group, string category, string txtSearch, string txtCode, string CityId, string DistrictId, int? page)
        {
            branchId = branchId == null ? "" : branchId;
            WarehouseId = WarehouseId == null ? "" : WarehouseId;
            group = group == null ? "" : group;
            category = category == null ? "" : category;
            txtSearch = txtSearch == null ? "" : txtSearch;
            txtCode = txtCode == null ? "" : txtCode;
            CityId = CityId == null ? "" : CityId;
            DistrictId = DistrictId == null ? "" : DistrictId;
            //Nếu là CN thì chỉ thấy của nó
            //string WarehouseId = "";
            var user = userRepository.GetByvwUserName(Helpers.Common.CurrentUser.UserName);
            if (!Filters.SecurityFilter.IsAdmin() && !Filters.SecurityFilter.IsKeToan() && !Filters.SecurityFilter.IsQLKhoTong() && string.IsNullOrEmpty(branchId))
            {
                branchId = user.DrugStore;
            }

            if (string.IsNullOrEmpty(conHang))
            {
                conHang = "1";
            }
            var listInventory = Domain.Helper.SqlHelper.QuerySP<InventoryViewModel>("spSale_Get_Inventory", new { WarehouseId = WarehouseId, HasQuantity = conHang, ProductCode = txtCode, ProductName = txtSearch, CategoryCode = category, ProductGroup = group, BranchId = branchId, LoCode = "", ProductId = "", ExpiryDate = "" });
            var listProduct = Domain.Helper.SqlHelper.QuerySP<InventoryViewModel>("spSale_Get_ListProductFromInventory", new { WarehouseId = WarehouseId, HasQuantity = conHang, ProductCode = txtCode, ProductName = txtSearch, CategoryCode = category, ProductGroup = group, BranchId = branchId, CityId = CityId, DistrictId = DistrictId });

            //var q = ProductRepository.GetAllvwProductByType("product").ToList();

            if (string.IsNullOrEmpty(CityId) == false)
            {
                listInventory = listInventory.Where(x => x.CityId == CityId).ToList();
                //listProduct = listProduct.Where(x => x.CityId == CityId).ToList();
            }

            if (string.IsNullOrEmpty(DistrictId) == false)
            {
                listInventory = listInventory.Where(x => x.DistrictId == DistrictId).ToList();
                //listProduct = listProduct.Where(x => x.DistrictId == DistrictId).ToList();
            }

            //if (string.IsNullOrEmpty(txtSearch) == false)
            //{
            //    txtSearch = Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(txtSearch);
            //    q = q.Where(x => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(x.Name).Contains(txtSearch)).ToList();
            //}

            //if (string.IsNullOrEmpty(txtCode) == false)
            //{
            //    txtCode = txtCode.ToLower();
            //    q = q.Where(x => x.Code.ToLower().Contains(txtCode)).ToList();
            //}
            var warehouseList = new List<WarehouseViewModel>();
            //Lấy danh sách kho
            //if (Filters.SecurityFilter.IsAdmin() || user.UserTypeCode == "KT")
            //{
            warehouseList = WarehouseRepository.GetAllWarehouse()
                .Select(item => new WarehouseViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Code = item.Code,
                    Address = item.Address,
                    Note = item.Note,
                    BranchId = item.BranchId
                }).OrderBy(x => x.Name).ToList();

            //ViewBag.warehouseList = warehouseList;
            //}
            //else
            //{
            //    warehouseList = WarehouseRepository.GetAllWarehouse().AsEnumerable()
            //        .Where(x => ("," + user.WarehouseId + ",").Contains("," + x.Id + ",") == true)
            //        .Select(item => new WarehouseViewModel
            //        {
            //            Id = item.Id,
            //            Name = item.Name,
            //            Code = item.Code,
            //            Address = item.Address,
            //            Note = item.Note,
            //            BranchId = item.BranchId
            //        }).OrderBy(x => x.Note).ToList();
            //    //ViewBag.warehouseList = warehouseList;
            //}
            if (!string.IsNullOrEmpty(branchId))
            {
                warehouseList = warehouseList.Where(x => x.BranchId != null && ("," + branchId + ",").Contains("," + x.BranchId.Value.ToString() + ",") == true).ToList();
            }
            if (!string.IsNullOrEmpty(WarehouseId))
            {
                warehouseList = warehouseList.Where(x => ("," + WarehouseId + ",").Contains("," + x.Id + ",") == true).ToList();
            }
            ViewBag.inventoryList = listInventory.ToList();
            warehouseList = warehouseList.Where(id1 => listInventory.Any(id2 => id2.WarehouseId == id1.Id)).ToList();
            var pager = new Pager(warehouseList.Count(), page, 20);

            var pageIndexViewModel = new IndexViewModel<WarehouseViewModel>
            {
                Items = warehouseList.OrderBy(m => m.Code).Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize)
                .Select(item => new WarehouseViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Code = item.Code,
                    Address = item.Address,
                }).ToList(),
                Pager = pager
            };

            ViewBag.ProductList = listProduct.ToList();
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];

            return View(pageIndexViewModel);
        }
        #endregion

        #region Detail
        public ActionResult Detail(int? Id, int? WarehouseId, string LoCode, int? day, int? month, int? year)
        {
            var Product = ProductRepository.GetvwProductById(Id.Value);
            if (Product != null && Product.IsDeleted != true)
            {
                var model = new ProductViewModel();
                AutoMapper.Mapper.Map(Product, model);

                var inboundDetails = ProductInboundRepository.GetAllvwProductInboundDetailByProductId(Id.Value).AsEnumerable()
                    .Where(item => item.IsArchive && item.LoCode == LoCode);
                var outboundDetails = ProductOutboundRepository.GetAllvwProductOutboundDetailByProductId(Id.Value).AsEnumerable()
                     .Where(item => item.IsArchive && item.LoCode == LoCode);

                if (WarehouseId != null && WarehouseId > 0)
                {
                    inboundDetails = inboundDetails.Where(x => x.WarehouseDestinationId == WarehouseId);
                    outboundDetails = outboundDetails.Where(x => x.WarehouseSourceId == WarehouseId);
                }
                if (day != null && month != null && year != null)
                {
                    inboundDetails = inboundDetails.Where(x => x.ExpiryDate != null && x.ExpiryDate.Value.Day == day && x.ExpiryDate.Value.Month == month && x.ExpiryDate.Value.Year == year);
                    outboundDetails = outboundDetails.Where(x => x.ExpiryDate != null && x.ExpiryDate.Value.Day == day && x.ExpiryDate.Value.Month == month && x.ExpiryDate.Value.Year == year);
                }
                else
                {
                    inboundDetails = inboundDetails.Where(x => x.ExpiryDate == null);
                    outboundDetails = outboundDetails.Where(x => x.ExpiryDate == null);
                }
                inboundDetails = inboundDetails.OrderBy(x => x.ProductInboundId);
                outboundDetails = outboundDetails.OrderBy(x => x.ProductOutboundId);
                ViewBag.inboundDetails = inboundDetails;
                ViewBag.outboundDetails = outboundDetails;

                return View(model);
            }

            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        #endregion

        #region Json
        public JsonResult GetListProductJsonByWarehouseId(int? warehouseId)
        {
            if (warehouseId == null)
                return Json(new List<int>(), JsonRequestBehavior.AllowGet);

            var list = Erp.Domain.Helper.SqlHelper.QuerySP<InventoryViewModel>("spSale_Get_Inventory", new { WarehouseId = warehouseId, HasQuantity = "1", ProductCode = "", ProductName = "", CategoryCode = "", ProductGroup = "", BranchId = "", LoCode = "", ProductId = "", ExpiryDate = "" }).ToList();
            foreach (var item in list)
            {
                item.strExpiryDate = item.ExpiryDate == null ? "" : item.ExpiryDate.Value.ToString("dd/MM/yyyy");
            }
            //var list = InventoryRepository.GetAllvwInventoryByWarehouseId(warehouseId.Value).AsEnumerable()
            //    .Select(item => new InventoryViewModel
            //    {
            //        Id=item.id
            //        CategoryCode = item.CategoryCode,
            //        ProductId = item.ProductId,
            //        ProductCode = item.ProductCode,
            //        ProductName = item.ProductName,
            //        Quantity = item.Quantity,
            //        LoCode = item.LoCode,
            //        strExpiryDate = item.ExpiryDate==null?"":item.ExpiryDate.Value.ToString("dd/MM/yyyy")
            //    }).OrderBy(item => item.CategoryCode).ThenBy(item => item.ProductCode).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Check or Update
        public static string Check(string productName, int productId, string LoCode, DateTime? ExpiryDate, int warehouseId, int quantityIn, int quantityOut)
        {

            return Update(productName, productId, LoCode, ExpiryDate, warehouseId, quantityIn, quantityOut, false);

        }


        public static string Check_mobile(string productName, int productId, string LoCode, DateTime? ExpiryDate, int warehouseId, int quantityIn, int quantityOut, int pCurrentUserId)
        {
            return Update_mobile(productName, productId, LoCode, ExpiryDate, warehouseId, quantityIn, quantityOut, pCurrentUserId, false);
        }

        public static string Update(string productName, int productId, string LoCode, DateTime? ExpiryDate, int warehouseId, int quantityIn, int quantityOut, bool isArchive = true)
        {
            string error = "";
            using (var scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    ProductInboundRepository productInboundRepository = new ProductInboundRepository(new Domain.Sale.ErpSaleDbContext());
                    ProductOutboundRepository productOutboundRepository = new Domain.Sale.Repositories.ProductOutboundRepository(new Domain.Sale.ErpSaleDbContext());
                    InventoryRepository inventoryRepository = new Domain.Sale.Repositories.InventoryRepository(new Domain.Sale.ErpSaleDbContext());
                    LoCode = LoCode == null ? "" : LoCode;
                    var d_ExpiryDate = (ExpiryDate != null ? DateTime.ParseExact(ExpiryDate.Value.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null).ToString("yyyy-MM-dd HH:mm:ss") : "");
                    var inbound = Domain.Helper.SqlHelper.QuerySP<vwProductInboundDetail>("spSale_GetInboundDetail", new
                    {
                        ProductId = productId,
                        LoCode = LoCode,
                        ExpiryDate = d_ExpiryDate,
                        WarehouseDestinationId = warehouseId
                    }).ToList();

                    var outbound = Domain.Helper.SqlHelper.QuerySP<vwProductOutboundDetail>("spSale_GetOutboundDetail", new
                    {
                        ProductId = productId,
                        LoCode = LoCode,
                        ExpiryDate = d_ExpiryDate,
                        WarehouseSourceId = warehouseId
                    });
                    var inventoryCurrent_List = Erp.Domain.Helper.SqlHelper.QuerySP<Inventory>("spSale_Get_Inventory",
                             new
                             {
                                 WarehouseId = warehouseId,
                                 HasQuantity = "1",
                                 ProductCode = "",
                                 ProductName = "",
                                 CategoryCode = "",
                                 ProductGroup = "",
                                 BranchId = "",
                                 LoCode = LoCode,
                                 ProductId = productId,
                                 ExpiryDate = ExpiryDate
                             }).ToList();
                    if (string.IsNullOrEmpty(LoCode))
                    {
                        inbound = inbound.Where(x => string.IsNullOrEmpty(x.LoCode)).ToList();
                        outbound = outbound.Where(x => string.IsNullOrEmpty(x.LoCode)).ToList();
                        inventoryCurrent_List = inventoryCurrent_List.Where(x => string.IsNullOrEmpty(x.LoCode)).ToList();
                    }
                    else
                    {
                        inbound = inbound.Where(x => x.LoCode == LoCode).ToList();
                        outbound = outbound.Where(x => x.LoCode == LoCode).ToList();
                        inventoryCurrent_List = inventoryCurrent_List.Where(x => x.LoCode == LoCode).ToList();
                    }
                    if (ExpiryDate == null)
                    {
                        inbound = inbound.Where(x => x.ExpiryDate == null).ToList();
                        outbound = outbound.Where(x => x.ExpiryDate == null).ToList();
                        inventoryCurrent_List = inventoryCurrent_List.Where(x => x.ExpiryDate == null).ToList();
                    }
                    else
                    {
                        inbound = inbound.Where(x => x.ExpiryDate == ExpiryDate).ToList();
                        outbound = outbound.Where(x => x.ExpiryDate == ExpiryDate).ToList();
                        inventoryCurrent_List = inventoryCurrent_List.Where(x => x.ExpiryDate == ExpiryDate).ToList();
                    }
                    var qty_inbound = inbound.Sum(x => x.Quantity);
                    var qty_outbound = outbound.Sum(x => x.Quantity);
                    var inventory = (inbound.Count() > 0 ? qty_inbound : 0) - (outbound.Count() > 0 ? qty_outbound : 0) + quantityIn - quantityOut;


                    for (int i = 0; i < inventoryCurrent_List.Count(); i++)
                    {
                        if (i > 0)
                        {
                            var id = inventoryCurrent_List[i].Id;
                            inventoryRepository.DeleteInventory(id);
                        }
                    }
                    //Sau khi thay đổi dữ liệu phải đảm bảo tồn kho >= 0
                    if (inventory >= 0)//inventory >= 0)
                    {
                        if (isArchive)
                        {
                            if (inventoryCurrent_List.Count() > 0)
                            {
                                if (inventoryCurrent_List[0].Quantity != inventory)
                                {
                                    inventoryCurrent_List[0].Quantity = inventory;
                                    inventoryRepository.UpdateInventory(inventoryCurrent_List[0]);
                                }
                            }
                            else
                            {
                                var insert = new Inventory();
                                insert.IsDeleted = false;
                                insert.CreatedUserId = WebSecurity.CurrentUserId;
                                insert.ModifiedUserId = WebSecurity.CurrentUserId;
                                insert.CreatedDate = DateTime.Now;
                                insert.ModifiedDate = DateTime.Now;
                                insert.WarehouseId = warehouseId;
                                insert.ProductId = productId;
                                insert.Quantity = inventory;
                                insert.LoCode = LoCode;
                                insert.ExpiryDate = ExpiryDate;
                                inventoryRepository.InsertInventory(insert);
                            }
                        }
                    }
                    else
                    {
                        error += string.Format("<br/>Dữ liệu tồn kho của sản phẩm \"{0}\" là {1}: không hợp lệ!", productName, inventory);
                    }

                    scope.Complete();
                }
                catch (DbUpdateException)
                {
                }
            }
            return error;
        }



        public static string Update_mobile(string productName, int productId, string LoCode, DateTime? ExpiryDate, int warehouseId, int quantityIn, int quantityOut, int pCurrentUserId, bool isArchive = true)
        {
            string error = "";
            using (var scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {

                    ProductInboundRepository productInboundRepository = new ProductInboundRepository(new Domain.Sale.ErpSaleDbContext());
                    ProductOutboundRepository productOutboundRepository = new Domain.Sale.Repositories.ProductOutboundRepository(new Domain.Sale.ErpSaleDbContext());
                    InventoryRepository inventoryRepository = new Domain.Sale.Repositories.InventoryRepository(new Domain.Sale.ErpSaleDbContext());
                    LoCode = LoCode == null ? "" : LoCode;
                    var d_ExpiryDate = (ExpiryDate != null ? DateTime.ParseExact(ExpiryDate.Value.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null).ToString("yyyy-MM-dd HH:mm:ss") : "");

                    //lấy tất cả các phiếu nhập
                    var inbound = Domain.Helper.SqlHelper.QuerySP<vwProductInboundDetail>("spSale_GetInboundDetail", new
                    {
                        ProductId = productId,
                        LoCode = LoCode,
                        ExpiryDate = d_ExpiryDate,
                        WarehouseDestinationId = warehouseId
                    }).ToList();

                    //lất tất cả các phiếu xuất
                    var outbound = Domain.Helper.SqlHelper.QuerySP<vwProductOutboundDetail>("spSale_GetOutboundDetail", new
                    {
                        ProductId = productId,
                        LoCode = LoCode,
                        ExpiryDate = d_ExpiryDate,
                        WarehouseSourceId = warehouseId
                    });

                    //lấy tồn kho hiện 
                    var inventoryCurrent_List = Erp.Domain.Helper.SqlHelper.QuerySP<Inventory>("spSale_Get_Inventory",
                        new
                        {
                            WarehouseId = warehouseId,
                            HasQuantity = "1",
                            ProductCode = "",
                            ProductName = "",
                            CategoryCode = "",
                            ProductGroup = "",
                            BranchId = "",
                            LoCode = LoCode,
                            ProductId = productId,
                            ExpiryDate = ExpiryDate
                        }).ToList();


                    //lấy các sản phẩm nhập, xuất, tồn kho theo lô
                    if (string.IsNullOrEmpty(LoCode))
                    {
                        inbound = inbound.Where(x => string.IsNullOrEmpty(x.LoCode)).ToList();
                        outbound = outbound.Where(x => string.IsNullOrEmpty(x.LoCode)).ToList();
                        inventoryCurrent_List = inventoryCurrent_List.Where(x => string.IsNullOrEmpty(x.LoCode)).ToList();
                    }
                    else
                    {
                        inbound = inbound.Where(x => x.LoCode == LoCode).ToList();
                        outbound = outbound.Where(x => x.LoCode == LoCode).ToList();
                        inventoryCurrent_List = inventoryCurrent_List.Where(x => x.LoCode == LoCode).ToList();
                    }

                    //lấy các sản phẩm nhập, xuất, tồn kho theo lô và ngày hết hạn
                    if (ExpiryDate == null)
                    {
                        inbound = inbound.Where(x => x.ExpiryDate == null).ToList();
                        outbound = outbound.Where(x => x.ExpiryDate == null).ToList();
                        inventoryCurrent_List = inventoryCurrent_List.Where(x => x.ExpiryDate == null).ToList();
                    }
                    else
                    {
                        inbound = inbound.Where(x => x.ExpiryDate == ExpiryDate).ToList();
                        outbound = outbound.Where(x => x.ExpiryDate == ExpiryDate).ToList();
                        inventoryCurrent_List = inventoryCurrent_List.Where(x => x.ExpiryDate == ExpiryDate).ToList();
                    }


                    var qty_inbound = inbound.Sum(x => x.Quantity);
                    var qty_outbound = outbound.Sum(x => x.Quantity);
                    //tính lại tồn kho dựa vào nhập và xuất
                    var inventory = (inbound.Count() > 0 ? qty_inbound : 0) - (outbound.Count() > 0 ? qty_outbound : 0) + quantityIn - quantityOut;


                    //begin xóa các dòng double của sản phẩm và lô của sản phẩm đó, chỉ giữ lại 1 dòng đầu tiên
                    for (int i = 0; i < inventoryCurrent_List.Count(); i++)
                    {
                        if (i > 0)
                        {
                            var id = inventoryCurrent_List[i].Id;
                            inventoryRepository.DeleteInventory(id);
                        }
                    }
                    //end xóa các dòng double của sản phẩm và lô của sản phẩm đó, chỉ giữ lại 1 dòng đầu tiên

                    //Sau khi thay đổi dữ liệu phải đảm bảo tồn kho >= 0
                    if (true)//inventory >= 0)
                    {
                        if (isArchive)
                        {
                            if (inventoryCurrent_List.Count() > 0)
                            {
                                if (inventoryCurrent_List[0].Quantity != inventory)
                                {
                                    inventoryCurrent_List[0].Quantity = inventory;
                                    inventoryRepository.UpdateInventory(inventoryCurrent_List[0]);
                                }
                            }
                            else
                            {
                                var insert = new Inventory();
                                insert.IsDeleted = false;
                                insert.CreatedUserId = pCurrentUserId;
                                insert.ModifiedUserId = pCurrentUserId;
                                insert.CreatedDate = DateTime.Now;
                                insert.ModifiedDate = DateTime.Now;
                                insert.WarehouseId = warehouseId;
                                insert.ProductId = productId;
                                insert.Quantity = inventory;
                                insert.LoCode = LoCode;
                                insert.ExpiryDate = ExpiryDate;
                                inventoryRepository.InsertInventory(insert);
                            }
                        }
                    }
                    else
                    {
                        error += string.Format("<br/>Dữ liệu tồn kho của sản phẩm \"{0}\" là {1}: không hợp lệ!", productName, inventory);
                    }
                    scope.Complete();
                    return error;

                }
                catch (Exception ex)
                {

                    return ex.Message;
                }
            }

        }
        #endregion

        #region UpdateAll
        public ActionResult UpdateAll(string url)
        {
            string rs = "";
            using (var scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    var inventoryList = InventoryRepository.GetAllInventory().ToList();
                    var inbound = ProductInboundRepository.GetAllvwProductInboundDetail().ToList();
                    var outbound = ProductOutboundRepository.GetAllvwProductOutboundDetail().ToList();
                    foreach (var item in inventoryList)
                    {
                        var warehouseId = item.WarehouseId.Value;
                        var productId = item.ProductId.Value;

                        var _inbound = inbound.Where(x => x.IsArchive
                                && x.ProductId == productId
                                && x.WarehouseDestinationId == warehouseId
                                && x.LoCode == item.LoCode
                                && x.ExpiryDate.Value.ToShortDateString() == item.ExpiryDate.Value.ToShortDateString()
                                ).ToList().Sum(x => x.Quantity);

                        var _outbound = outbound.Where(x => x.IsArchive
                            && x.ProductId == productId
                            && x.WarehouseSourceId == warehouseId
                            && x.LoCode == item.LoCode
                            && x.ExpiryDate.Value.ToShortDateString() == item.ExpiryDate.Value.ToShortDateString()
                            ).ToList().Sum(x => x.Quantity);

                        var inventory = (_inbound == null ? 0 : _inbound) - (_outbound == null ? 0 : _outbound);

                        if (item.Quantity != inventory)
                        {
                            rs += "<br/>" + item.ProductId + " | " + item.Quantity + " => " + inventory;
                            item.Quantity = inventory;
                            InventoryRepository.UpdateInventory(item);
                        }

                    }
                    scope.Complete();
                }
                catch (DbUpdateException)
                {
                    TempData[Globals.FailedMessageKey] = App_GlobalResources.Wording.Error;
                    return Redirect(url);
                }
            }
            TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess + rs;
            return Redirect(url);
        }
        #endregion
    }
}
