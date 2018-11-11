using AutoMapper;
using DSofT.Framework.Helpers;
using DSofT.Framework.Internal.ApiListener.Controllers;
using DSofT.Framework.Logging;
using Erp.BackOffice.Account.Models;
using Erp.BackOffice.App_GlobalResources;
using Erp.BackOffice.Sale.Controllers;
using Erp.BackOffice.Sale.Models;
using Erp.Domain;
using Erp.Domain.Account;
using Erp.Domain.Account.Entities;
using Erp.Domain.Account.Interfaces;
using Erp.Domain.Account.Repositories;
using Erp.Domain.Crm.Interfaces;
using Erp.Domain.Entities;
using Erp.Domain.Interfaces;
using Erp.Domain.Repositories;
using Erp.Domain.Sale;
using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using Erp.Domain.Sale.Repositories;
using Erp.Domain.Staff.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using WebMatrix.WebData;
using System.Globalization;
using System.Transactions;
namespace DSofT.Warehouse.ApiListener.Controllers
{
    [System.Web.Http.RoutePrefix(UrlCommon.U_UserManager)]
    public class UserAPIController : BaseApiController
    {
        private readonly IUserRepository _iUserRepository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly ICommisionCustomerRepository _icommisionCustomerRepository;
        private readonly IProductInvoiceRepository _iproductInvoiceRepository;
        private readonly ICustomerRepository _iCustomerRepository;
        private readonly IProductOrServiceRepository _iProductRepository;
        private readonly IWarehouseRepository _iWarehouseRepository;
        private readonly IProductOutboundRepository _iproductOutboundRepository;
        private readonly IWarehouseLocationItemRepository _iwarehouseLocationItemRepository;
        private readonly IReceiptRepository _iReceiptRepository;
        private readonly ITransactionLiabilitiesRepository _itransactionLiabilitiesRepository;
        private readonly IReceiptDetailRepository _ireceiptDetailRepository;
        private readonly ICommisionCustomerRepository _commisionCustomerRepository;




        //private readonly ISystemBussiness _systemBussiness ;
        public UserAPIController()
        {
            _iUserRepository = new UserRepository(new ErpDbContext());
            _inventoryRepository = new InventoryRepository(new ErpSaleDbContext());
            _icommisionCustomerRepository = new CommisionCustomerRepository(new ErpSaleDbContext());
            _iproductInvoiceRepository = new ProductInvoiceRepository(new ErpSaleDbContext());
            _iCustomerRepository = new CustomerRepository(new ErpAccountDbContext());
            _iProductRepository = new ProductOrServiceRepository(new ErpSaleDbContext());
            _iWarehouseRepository = new WarehouseRepository(new ErpSaleDbContext());
            _iproductOutboundRepository = new ProductOutboundRepository(new ErpSaleDbContext());
            _iwarehouseLocationItemRepository = new WarehouseLocationItemRepository(new ErpSaleDbContext());
            _iReceiptRepository = new ReceiptRepository(new ErpAccountDbContext());
            _itransactionLiabilitiesRepository = new TransactionLiabilitiesRepository(new ErpAccountDbContext());
            _ireceiptDetailRepository = new ReceiptDetailRepository(new ErpAccountDbContext());
            _commisionCustomerRepository = new CommisionCustomerRepository(new ErpSaleDbContext());
        }

        //#region Publish API   U_UserManager_ChangePassword

        [System.Web.Http.HttpPost, System.Web.Http.Route(UrlCommon.U_UserManager_CheckLogin)]
        public IHttpActionResult CheckLoginForAPI()
        {
            var userRequestModel = GetRequestData<User>();
            string pUserName = userRequestModel.UserName;
            string pPassWord = userRequestModel.UserCode;

            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("ErpDbContext", "System_User", "Id", "UserName", autoCreateTables: true);
            }

            if (WebSecurity.Login(pUserName, pPassWord) == true)
            {
                var result = _iUserRepository.GetByvwUserName(userRequestModel.UserName);
                return DSofTResult(result);
            }
            else
            {
                return null;
            }




        }



        [System.Web.Http.HttpPost, System.Web.Http.Route(UrlCommon.U_UserManager_ChangePassword)]
        public IHttpActionResult ChangePassword()
        {
            var userRequestModel = GetRequestData<User>();
            string pUserName = userRequestModel.UserName;
            string pPassWord_newpass = userRequestModel.UserCode;
            string pPassWord_current = userRequestModel.Email;


            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("ErpDbContext", "System_User", "Id", "UserName", autoCreateTables: true);
            }

            if (WebSecurity.ChangePassword(pUserName, pPassWord_current, pPassWord_newpass) == true)
            {
                var result = _iUserRepository.GetByUserName(userRequestModel.UserName);
                return DSofTResult(result);
            }
            else
            {
                return null;
            }




        }






        [System.Web.Http.HttpPost, System.Web.Http.Route(UrlCommon.GetCustomerbyPhone)]
        public IHttpActionResult GetCustomerbyPhone()
        {
            var userRequestModel = GetRequestData<User>();
            string pMobile = userRequestModel.Mobile;
            var cus = _iCustomerRepository.GetCustomerByPhone(pMobile);
            if (cus == null)
                return null;
            return DSofTResult(cus);
        }








        //lay danh sach sản phẩm theo brandid
        //HasQuantity = "1" chỉ lấy sản phẩm còn tồn kho, HasQuantity = "0" là lấy toàn bộ danh mục sản phẩm loại trừ sản phẩm đang còn tồn
        [System.Web.Http.HttpPost, System.Web.Http.Route(UrlCommon.P_Product_Getlist)]
        public IHttpActionResult GetListProduct()
        {
            var userRequestModel = GetRequestData<User>();
            int pBranchId = 0;
            if (userRequestModel.BranchId.HasValue)
            {
                pBranchId = userRequestModel.BranchId.Value;
            }
            string pHost = Erp.BackOffice.Helpers.Common.GetPathImageMobile();
            string image_folder_product = Erp.BackOffice.Helpers.Common.GetSetting("product-image-folder");
            image_folder_product = pHost + image_folder_product;

            if (pBranchId > 0)
            {

                var result = Erp.Domain.Helper.SqlHelper.QuerySP<InventoryViewModel>("spSale_Get_Inventory", new { WarehouseId = "", HasQuantity = "1", ProductCode = "", ProductName = "", CategoryCode = "", ProductGroup = "", BranchId = pBranchId, LoCode = "", ProductId = "", ExpiryDate = "" }).ToList();

                foreach (var item in result)
                {
                    item.Image_Name = (item.Image_Name == null ? pHost + "/assets/css/images/noimage.gif" : image_folder_product + item.Image_Name);
                }

                return DSofTResult(result);
            }
            else
            {
                return null;
            }
        }


        //lấy tỷ lệ chiết khấu cố định và đột xuất
        [System.Web.Http.HttpPost, System.Web.Http.Route(UrlCommon.DiscountBy_productID)]
        public IHttpActionResult GetDiscountBy_productID()
        {
            var userRequestModel = GetRequestData<ProductInvoiceDetailViewModel>();
            int? OrderNo = userRequestModel.OrderNo;
            int? ProductId = userRequestModel.ProductId;
            string ProductName = userRequestModel.ProductName;
            string Unit = userRequestModel.Unit;
            int? Quantity = userRequestModel.Quantity;
            decimal? Price = userRequestModel.Price;
            string ProductCode = userRequestModel.ProductCode;
            string ProductType = userRequestModel.ProductType;
            int? QuantityInventory = userRequestModel.QuantityInInventory;
            string Type = userRequestModel.Type;
            int BranchId = 0;
            try
            {
                BranchId = userRequestModel.BranchId;
            }
            catch
            {

            }

            //
            string LoCode = userRequestModel.LoCode;
            var strExpiryDate = userRequestModel.strExpiryDate;
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
            model.strExpiryDate = strExpiryDate;
            model.BranchId = BranchId;
            //get id nha thuoc vao day
            //var view = new ProductInvoiceViewModel();

            //giảm giá cố định
            //lấy danh sách cài đặt chiết khấu theo chi nhánh
            var commision = _icommisionCustomerRepository.GetAllCommisionCustomer().ToList();
            commision = commision.Where(item => ("," + item.ApplyFor + ",").Contains("," + BranchId + ",") == true).ToList();


            //lấy chiết khấu cố định của sản phẩm
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
            //lấy chiết khấu đột xuất theo sản phẩm
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
            return DSofTResult(model);
        }





        //lấy danh sách đơn bán hàng theo chi nhánh
        [System.Web.Http.HttpPost, System.Web.Http.Route(UrlCommon.List_Invoice)]
        public IHttpActionResult GetList_Invoice()
        {
            var userRequestModel = GetRequestData<ProductInvoiceViewModel>();
            int? BranchId = userRequestModel.BranchId;

            var q = _iproductInvoiceRepository.GetAllvwProductInvoiceFull().Where(x => x.BranchId == BranchId).ToList();
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
                strCreatedDate = item.CreatedDate.ToStringFormat("dd/MM/yyyy-HH:MM:ss")
            }).OrderByDescending(m => m.ModifiedDate).ToList();


            return DSofTResult(model);
        }





        //lưu đơn hàng đồng thời ghi sổ luôn
        [System.Web.Http.HttpPost, System.Web.Http.Route(UrlCommon.Save_Invoice)]
        public IHttpActionResult Save_Invoice()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    //int a=0;
                    //int b=1/a;
                    ProductInvoiceViewModel model = GetRequestData<ProductInvoiceViewModel>();
                    int BranchId = model.BranchId.GetValueOrDefault();
                    int CreatedUserId = model.CreatedUserId.GetValueOrDefault();
                    int id_phieu = 0;
                    string pUserTypeCode = model.UserTypeCode;
                    //nếu đơn hàng có chi tiết thì mới thực hiện
                    if (model.DetailList.Count > 0)
                    {
                        ProductInvoice productInvoice = null;
                        if (model.Id > 0)
                        {
                            //lấy tất thông tin chung của phiếu bán hàng dựa vào id phiếu
                            productInvoice = _iproductInvoiceRepository.GetProductInvoiceById(model.Id);
                            ////Kiểm tra phân quyền Chi nhánh xem chi nhánh đăng nhập vào có đúng là chi nhánh của phiếu đang sửa kg
                            if (productInvoice.BranchId != BranchId)
                            {
                                return null;
                            }
                        }

                        if (productInvoice != null)
                        {
                            //Nếu đã ghi sổ rồi thì không được sửa
                            if (productInvoice.IsArchive)
                            {
                                return null;
                            }
                            AutoMapper.Mapper.CreateMap<ProductInvoiceViewModel, ProductInvoice>();
                            AutoMapper.Mapper.Map(model, productInvoice);
                        }
                        else
                        {
                            //nếu chưa có đầu phiếu thì tạo model cho đầu phiếu
                            productInvoice = new ProductInvoice();

                            AutoMapper.Mapper.CreateMap<ProductInvoiceViewModel, ProductInvoice>();
                            AutoMapper.Mapper.Map(model, productInvoice);


                            productInvoice.IsDeleted = false;
                            productInvoice.CreatedUserId = CreatedUserId;
                            productInvoice.CreatedDate = DateTime.Now;
                            //productInvoice.Status = Wording.OrderStatus_pending;//hoapd tam bo
                            productInvoice.BranchId = BranchId;
                            productInvoice.IsArchive = false;
                            productInvoice.IsReturn = false;


                        }

                        #region kiểm tra thông tin khách hàng
                        if (!string.IsNullOrEmpty(model.CustomerPhone))
                        {
                            //lấy thông tin khách hàng dựa vào số phone
                            var customer = _iCustomerRepository.GetCustomerByPhone(model.CustomerPhone);
                            if (customer == null)
                            {
                                //chưa có khách hàng thì tạo mới
                                var insert_cus = new Customer();
                                insert_cus.IsDeleted = false;
                                insert_cus.CreatedUserId = CreatedUserId;
                                insert_cus.CreatedDate = DateTime.Now;
                                insert_cus.ModifiedUserId = CreatedUserId;
                                insert_cus.ModifiedDate = DateTime.Now;
                                insert_cus.Phone = model.CustomerPhone;
                                insert_cus.CompanyName = model.CustomerName;
                                insert_cus.BranchId = BranchId;
                                insert_cus.CustomerType = "Customer";
                                _iCustomerRepository.InsertCustomer(insert_cus);
                                //tạo mã khách hàng tự động
                                var prefix1 = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_Customer");
                                insert_cus.Code = Erp.BackOffice.Helpers.Common.GetCode_mobile(prefix1, insert_cus.Id, "");
                                //cập nhật mã khách hàng
                                _iCustomerRepository.UpdateCustomer(insert_cus);
                                //cập nhật lại id customer cho đơn hàng.. để sau này tính công nợ, báo cáo
                                productInvoice.CustomerId = insert_cus.Id;

                            }
                            else
                            {
                                productInvoice.CustomerId = customer.Id;
                            }
                            productInvoice.Type = "Customer";
                        }
                        else //nếu không nhập số phone khách hàng
                        {
                            //lấy id khách vãng lai mặc định trong customer với CustomerType = "Guests".. để sau này tiện thống kê
                            var customer_guest = _iCustomerRepository.GetCustomerByGuestsCustomer(BranchId);

                            if (customer_guest != null)
                            {
                                productInvoice.CustomerId = customer_guest.Id;
                                productInvoice.CustomerName = customer_guest.CompanyName;
                            }
                            else
                            {
                                var cus = new Customer();
                                AutoMapper.Mapper.Map(model, cus);
                                cus.IsDeleted = false;
                                cus.CreatedUserId = WebSecurity.CurrentUserId;
                                cus.ModifiedUserId = WebSecurity.CurrentUserId;
                                cus.CreatedDate = DateTime.Now;
                                cus.ModifiedDate = DateTime.Now;
                                cus.CompanyName = "Khách vãng lai";
                                cus.BranchId = model.BranchId;
                                cus.CustomerType = "Guests";
                                cus.Phone = model.CustomerPhone;
                                //lấy mã.
                                _iCustomerRepository.InsertCustomer(cus);
                                var prefix1 = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_Customer");
                                cus.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix1, cus.Id);
                                _iCustomerRepository.UpdateCustomer(cus);
                                productInvoice.CustomerId = cus.Id;
                                productInvoice.CustomerName = cus.CompanyName;
                            }
                            productInvoice.Type = "Guests";
                        }
                        #endregion

                        //lấy thông tin chiết khấu theo chi nhánh
                        var commision = _commisionCustomerRepository.GetAllCommisionCustomer().ToList();
                        commision = commision.Where(item => ("," + item.ApplyFor + ",").Contains("," + model.BranchId + ",") == true).ToList();

                        //Tính lại chiết khấu
                        List<ProductInvoiceDetail> listNewCheckSameId = new List<ProductInvoiceDetail>();
                        foreach (var item in model.DetailList)
                        {



                            //convert lại  ExpiryDate ra datetime
                            if (!string.IsNullOrEmpty(item.strExpiryDate))
                            {
                                var ExpiryDate = Convert.ToDateTime(DateTime.ParseExact(item.strExpiryDate, "dd/MM/yyyy", CultureInfo.InvariantCulture));// Convert.ToDateTime(group.strExpiryDate);
                                item.ExpiryDate = ExpiryDate;
                            }


                            //begin tính chiết khấu
                            var thanh_tien = item.Quantity * item.Price;
                            //lấy chiết khấu cố định theo từng sản phẩm và tính
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
                            //lấy chiết khấu đột xuất theo từng sản phẩm và tính
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

                            //end tính chiết khấu

                            listNewCheckSameId.Add(new ProductInvoiceDetail
                            {
                                ProductId = item.ProductId.Value,
                                ProductType = item.Type,
                                Quantity = item.Quantity,
                                Unit = item.Unit,
                                Price = item.Price,
                                PromotionDetailId = item.PromotionDetailId,
                                PromotionId = item.PromotionId,
                                PromotionValue = item.PromotionValue,
                                IsDeleted = false,
                                CreatedUserId = CreatedUserId,
                                ModifiedUserId = CreatedUserId,
                                CreatedDate = DateTime.Now,
                                ModifiedDate = DateTime.Now,
                                FixedDiscount = item.FixedDiscount,
                                FixedDiscountAmount = item.FixedDiscountAmount,
                                IrregularDiscount = item.IrregularDiscount,
                                IrregularDiscountAmount = item.IrregularDiscountAmount,
                                QuantitySaleReturn = item.Quantity,
                                CheckPromotion = (item.CheckPromotion.HasValue ? item.CheckPromotion.Value : false),
                                IsReturn = false,
                                //StaffId = group.StaffId,
                                //Status = group.Status
                                LoCode = item.LoCode,
                                ExpiryDate = item.ExpiryDate
                            });



                        }

                        //begin cập nhật thông tin tổng tiền và tổng chiết khấu lên thông tin chung của đơn hàng
                        var total = listNewCheckSameId.Sum(item => item.Quantity.Value * item.Price.Value);
                        productInvoice.TotalAmount = total + (total * Convert.ToDecimal(model.TaxFee));
                        productInvoice.IrregularDiscount = Convert.ToDecimal(listNewCheckSameId.Sum(x => x.IrregularDiscountAmount));
                        productInvoice.FixedDiscount = Convert.ToDecimal(listNewCheckSameId.Sum(x => x.FixedDiscountAmount));
                        productInvoice.IsReturn = false;
                        productInvoice.ModifiedUserId = CreatedUserId;
                        productInvoice.ModifiedDate = DateTime.Now;
                        productInvoice.PaidAmount = 0;
                        productInvoice.RemainingAmount = productInvoice.TotalAmount;

                        //end cập nhật thông tin tổng tiền và tổng chiết khấu lên thông tin chung của đơn hàng
                        //hàm edit 
                        //nếu có id phiếu >0 là đang sửa phiếu, tuy nhiên Mobile sẽ không chạy vào trường hợp model.Id>0
                        if (model.Id > 0)
                        {
                            _iproductInvoiceRepository.UpdateProductInvoice(productInvoice);
                            var listDetail = _iproductInvoiceRepository.GetAllInvoiceDetailsByInvoiceId(productInvoice.Id).ToList();

                            //xóa danh sách dữ liệu cũ dưới database gồm product invoice, productInvoiceDetail
                            _iproductInvoiceRepository.DeleteProductInvoiceDetail(listDetail);

                            //thêm mới toàn bộ database
                            foreach (var item in listNewCheckSameId)
                            {
                                item.ProductInvoiceId = productInvoice.Id;
                                _iproductInvoiceRepository.InsertProductInvoiceDetail(item);
                            }

                            //Thêm vào 1 dòng trong table  Acount_Transaction để lưu lại thông tin của giao dịch này
                            Create_transaction(new TransactionViewModel
                            {
                                TransactionModule = "ProductInvoice",
                                TransactionCode = productInvoice.Code,
                                TransactionName = "Bán hàng"
                            }, CreatedUserId);
                        }
                        else
                        {
                            //hàm thêm mới
                            //thêm mới phiếu bán hàng
                            id_phieu = _iproductInvoiceRepository.InsertProductInvoice(productInvoice, listNewCheckSameId);

                            //cập nhật lại mã hóa đơn
                            string prefix = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_Invoice");

                            productInvoice.Code = Erp.BackOffice.Helpers.Common.GetCode_mobile(prefix, productInvoice.Id, "");
                            //vì mã đơn hàng được ghép với  ID phiếu nên sau khi có id phiếu mới tạo được mã đơn hàng
                            _iproductInvoiceRepository.UpdateProductInvoice(productInvoice);

                            //Thêm vào quản lý chứng từ
                            Create_transaction(new TransactionViewModel
                            {
                                TransactionModule = "ProductInvoice",
                                TransactionCode = productInvoice.Code,
                                TransactionName = "Bán hàng"
                            }, CreatedUserId);
                        }


                        model.Id = id_phieu;
                        //thực hiện hàm ghi sổ tự động
                        model = Success(model);
                        scope.Complete();
                        return DSofTResult(model);
                    }
                   

                    return null;

                }
                catch (System.Exception ex)
                {
                    CommonLogger.WriteLog(ex, SystemLogSource.APIListener);
                    return DSofTResult(ex.Message);
                }
            }
        }


        public void Archive(ProductInvoiceViewModel model, int BranchId, int CurrentUserId)
        {
            var productInvoice = _iproductInvoiceRepository.GetProductInvoiceById(model.Id);

            //Kiểm tra cho phép sửa chứng từ này hay không
            if (Erp.BackOffice.Helpers.Common.KiemTraNgaySuaChungTu(productInvoice.CreatedDate.Value) == false)
            {
                return;
            }

            //Kiểm tra phân quyền Chi nhánh
            if (productInvoice.BranchId != BranchId)
            {
                return;
            }

            //Coi thử đã xuất kho chưa mới cho ghi sổ
            bool hasProductOutbound = _iproductOutboundRepository.GetAllvwProductOutbound().Any(item => item.InvoiceId == productInvoice.Id);
            bool hasProduct = _iproductInvoiceRepository.GetAllInvoiceDetailsByInvoiceId(productInvoice.Id).Any(item => item.ProductType == "product");

            if (!hasProductOutbound && hasProduct)
            {
                return;
            }

            if (!productInvoice.IsArchive)
            {
                //Cập nhật thông tin thanh toán cho đơn hàng
                productInvoice.PaymentMethod = model.ReceiptViewModel.PaymentMethod;
                productInvoice.PaidAmount = Convert.ToDecimal(model.ReceiptViewModel.Amount);
                productInvoice.RemainingAmount = productInvoice.TotalAmount - productInvoice.PaidAmount;
                productInvoice.NextPaymentDate = model.NextPaymentDate_Temp;

                productInvoice.ModifiedDate = DateTime.Now;
                productInvoice.ModifiedUserId = CurrentUserId;
                _iproductInvoiceRepository.UpdateProductInvoice(productInvoice);

                //Lấy mã KH
                var customer = _iCustomerRepository.GetCustomerById(productInvoice.CustomerId.Value);

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
                Erp.BackOffice.Account.Controllers.TransactionLiabilitiesController.Create_mobile(
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
                        null, CurrentUserId);

                //Khách thanh toán ngay
                if (model.ReceiptViewModel.Amount > 0)
                {
                    //Lập phiếu thu
                    var receipt = new Receipt();

                    AutoMapper.Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<ReceiptViewModel, Receipt>();
                        /* etc */
                    });


                    AutoMapper.Mapper.Map(model.ReceiptViewModel, receipt);
                    receipt.IsDeleted = false;
                    receipt.CreatedUserId = CurrentUserId;
                    receipt.ModifiedUserId = CurrentUserId;
                    receipt.AssignedUserId = CurrentUserId;
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
                    receipt.BranchId = BranchId;

                    if (receipt.Amount > Convert.ToDecimal(productInvoice.TotalAmount))
                        receipt.Amount = Convert.ToDecimal(productInvoice.TotalAmount);

                    _iReceiptRepository.InsertReceipt(receipt);

                    var prefixReceipt = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_ReceiptCustomer");
                    receipt.Code = Erp.BackOffice.Helpers.Common.GetCode_mobile(prefixReceipt, receipt.Id, "");
                    _iReceiptRepository.UpdateReceipt(receipt);

                    //Thêm vào quản lý chứng từ
                    Create_transaction(new TransactionViewModel
                    {
                        TransactionModule = "Receipt",
                        TransactionCode = receipt.Code,
                        TransactionName = "Thu tiền khách hàng"
                    }, CurrentUserId);

                    //Thêm chứng từ liên quan
                    CreateRelationship(new TransactionRelationshipViewModel
                    {
                        TransactionA = receipt.Code,
                        TransactionB = productInvoice.Code
                    }, CurrentUserId);

                    //Ghi Có TK 131 - Phải thu của khách hàng.
                    Erp.BackOffice.Account.Controllers.TransactionLiabilitiesController.Create_mobile(
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
                        null, CurrentUserId);
                }
            }

            //Cập nhật đơn hàng
            productInvoice.ModifiedUserId = CurrentUserId;
            productInvoice.ModifiedDate = DateTime.Now;
            productInvoice.IsArchive = true;
            productInvoice.Status = Wording.OrderStatus_complete;
            _iproductInvoiceRepository.UpdateProductInvoice(productInvoice);
            //Tạo chiết khấu cho nhân viên nếu có
            //CommisionStaffController.CreateCommission(productInvoice.Id);
            //tính điểm tích lũy hóa đơn cho khách hàng
            //Erp.BackOffice.Sale.Controllers.LogLoyaltyPointController.CreateLogLoyaltyPoint(productInvoice.CustomerId, productInvoice.TotalAmount, productInvoice.Id);
            //Cảnh báo cập nhật phiếu xuất kho

        }



        public void Create_transaction(TransactionViewModel model, int CreatedUserId)
        {
            TransactionRepository transactionRepository = new TransactionRepository(new ErpAccountDbContext());
            //lấy transation code xem có trong db chưa
            var transaction = transactionRepository.GetAllTransaction()
                .Where(item => item.TransactionCode == model.TransactionCode).FirstOrDefault();

            if (transaction == null)
            {
                transaction = new Erp.Domain.Account.Entities.Transaction();

                AutoMapper.Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<TransactionViewModel, Erp.Domain.Account.Entities.Transaction>();
                    /* etc */
                });

                AutoMapper.Mapper.Map(model, transaction);
                transaction.IsDeleted = false;
                transaction.CreatedUserId = CreatedUserId;
                transaction.ModifiedUserId = CreatedUserId;
                transaction.AssignedUserId = CreatedUserId;
                transaction.CreatedDate = DateTime.Now;
                transaction.ModifiedDate = DateTime.Now;
                transactionRepository.InsertTransaction(transaction);
            }
            else
            {
                transaction.ModifiedUserId = CreatedUserId;
                transaction.ModifiedDate = DateTime.Now;
                transaction.TransactionName = model.TransactionName;
                transactionRepository.UpdateTransaction(transaction);
            }

        }

        public void CreateRelationship(TransactionRelationshipViewModel model, int CreatedUserId)
        {
            TransactionRepository transactionRelationshipRepository = new TransactionRepository(new ErpAccountDbContext());
            var transactionRelationship = new TransactionRelationship();


            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<TransactionRelationshipViewModel, TransactionRelationship>();
                /* etc */
            });

            AutoMapper.Mapper.Map(model, transactionRelationship);
            transactionRelationship.IsDeleted = false;
            transactionRelationship.CreatedUserId = CreatedUserId;
            transactionRelationship.ModifiedUserId = CreatedUserId;
            transactionRelationship.AssignedUserId = CreatedUserId;
            transactionRelationship.CreatedDate = DateTime.Now;
            transactionRelationship.ModifiedDate = DateTime.Now;
            transactionRelationshipRepository.InsertTransactionRelationship(transactionRelationship);
        }



        public ProductOutbound AutoCreateProductOutboundFromProductInvoice(ProductOutboundViewModel model, string productInvoiceCode, int BranchId, int CreatedUserId)
        {
            Erp.Domain.Sale.Repositories.ProductOutboundRepository productOutboundRepository = new Erp.Domain.Sale.Repositories.ProductOutboundRepository(new ErpSaleDbContext());
            Erp.Domain.Sale.Repositories.WarehouseLocationItemRepository warehouseLocationItemRepository = new Erp.Domain.Sale.Repositories.WarehouseLocationItemRepository(new ErpSaleDbContext());
            AutoMapper.Mapper.CreateMap<vwWarehouseLocationItem, WarehouseLocationItem>();
            var productOutbound = new ProductOutbound();

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ProductOutboundViewModel, ProductOutbound>();
            });


            AutoMapper.Mapper.Map(model, productOutbound);
            productOutbound.IsDeleted = false;
            productOutbound.CreatedUserId = CreatedUserId;
            productOutbound.ModifiedUserId = CreatedUserId;
            productOutbound.CreatedDate = DateTime.Now;
            productOutbound.ModifiedDate = DateTime.Now;
            productOutbound.Type = "invoice";
            productOutbound.BranchId = BranchId;
            productOutbound.TotalAmount = model.DetailList.Sum(x => x.Price * x.Quantity);
            productOutboundRepository.InsertProductOutbound(productOutbound);

            //Cập nhật lại mã xuất kho
            string prefix = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_Outbound");
            productOutbound.Code = Erp.BackOffice.Helpers.Common.GetCode_mobile(prefix, productOutbound.Id, "001");
            productOutboundRepository.UpdateProductOutbound(productOutbound);
            // chi tiết phiếu xuất
            foreach (var item in model.DetailList)
            {
                ProductOutboundDetail productOutboundDetail = new ProductOutboundDetail();


                AutoMapper.Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<ProductOutboundDetailViewModel, ProductOutboundDetail>();
                });


                AutoMapper.Mapper.Map(item, productOutboundDetail);
                productOutboundDetail.IsDeleted = false;
                productOutboundDetail.CreatedUserId = CreatedUserId;
                productOutboundDetail.ModifiedUserId = CreatedUserId;
                productOutboundDetail.CreatedDate = DateTime.Now;
                productOutboundDetail.ModifiedDate = DateTime.Now;
                productOutboundDetail.ProductOutboundId = productOutbound.Id;
                productOutboundRepository.InsertProductOutboundDetail(productOutboundDetail);

            }
            //Thêm vào quản lý chứng từ
            Create_transaction(new TransactionViewModel
            {
                TransactionModule = "ProductOutbound",
                TransactionCode = productOutbound.Code,
                TransactionName = "Xuất kho bán hàng"
            }, CreatedUserId);

            //Thêm chứng từ liên quan
            CreateRelationship(new TransactionRelationshipViewModel
            {
                TransactionA = productOutbound.Code,
                TransactionB = productInvoiceCode
            }, CreatedUserId);

            return productOutbound;
        }



        public ProductInvoiceViewModel Success(ProductInvoiceViewModel model)
        {
            //lấy lại phiếu bán hàng trong db lên
            var productInvoice = _iproductInvoiceRepository.GetProductInvoiceById(model.Id);
            //lấy lại sản phẩm chi tiết của phiếu bán hàng trong db lên
            var invoice_detail_list = _iproductInvoiceRepository.GetAllvwInvoiceDetailsByInvoiceId(productInvoice.Id).ToList();

            //tạo phiếu xuất kho
            ProductOutboundViewModel productOutboundViewModel = new ProductOutboundViewModel();

            #region  phiếu xuất ok
            //kiểm tra xem có phiếu xuất chưa
            var product_outbound = _iproductOutboundRepository.GetAllProductOutboundFull().Where(x => x.InvoiceId == productInvoice.Id).ToList();

            //insert mới
            if (product_outbound.Count() <= 0)
            {
                #region  thêm mới phiếu xuất
                //lấy 1 kho bán của chi nhánh
                var warehouseDefault = _iWarehouseRepository.GetAllWarehouse().Where(x => x.BranchId == productInvoice.BranchId && x.IsSale == true).FirstOrDefault();
                //Nếu có kho bán thì thực hiện xuất kho
                if (warehouseDefault != null)
                {
                    productOutboundViewModel.InvoiceId = productInvoice.Id;
                    productOutboundViewModel.InvoiceCode = productInvoice.Code;
                    productOutboundViewModel.WarehouseSourceId = warehouseDefault.Id;
                    productOutboundViewModel.Note = "Xuất kho cho đơn hàng " + productInvoice.Code;

                    //begin đoạn này dùng để lấy danh sách chi tiết của đơn hàng đưa sang chi tiết của phiếu xuất
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
                                  ExpiryDate = x.ExpiryDate
                              }).ToList();
                    //Lấy dữ liệu cho chi tiết
                    productOutboundViewModel.DetailList = new List<ProductOutboundDetailViewModel>();
                    AutoMapper.Mapper.CreateMap<ProductInvoiceDetailViewModel, ProductOutboundDetailViewModel>();
                    AutoMapper.Mapper.Map(DetailList, productOutboundViewModel.DetailList);
                    //begin đoạn này dùng để lấy danh sách chi tiết của đơn hàng đưa sang chi tiết của phiếu xuất
                    //Insert phiếu xuất vào db
                    var productOutbound = AutoCreateProductOutboundFromProductInvoice(productOutboundViewModel, productInvoice.Code, model.BranchId.GetValueOrDefault(), model.CreatedUserId.GetValueOrDefault());

                    //Ghi sổ chứng từ phiếu xuất
                    ProductOutboundController.Archive_mobile(productOutbound, model.CreatedUserId.GetValueOrDefault());
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
                    var outbound_detail = _iproductOutboundRepository.GetAllProductOutboundDetailByOutboundId(product_outbound[i].Id).ToList();
                    //xóa
                    for (int ii = 0; ii < outbound_detail.Count(); ii++)
                    {
                        _iproductOutboundRepository.DeleteProductOutboundDetail(outbound_detail[ii].Id);
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
                        productOutboundDetail.CreatedUserId = model.CreatedUserId;
                        productOutboundDetail.ModifiedUserId = model.CreatedUserId;
                        productOutboundDetail.CreatedDate = DateTime.Now;
                        productOutboundDetail.ModifiedDate = DateTime.Now;
                        productOutboundDetail.ProductOutboundId = product_outbound[i].Id;
                        _iproductOutboundRepository.InsertProductOutboundDetail(productOutboundDetail);
                        //lấy vị trí sản phẩm xuất kho mới
                        //var list = new List<WarehouseLocationItem>();
                        //var product = invoice_detail_list[iii].ProductId;
                        //var branch = product_outbound[i].BranchId;
                        //var quantity = invoice_detail_list[iii].Quantity.Value;
                        //var listLocationItemExits = _iwarehouseLocationItemRepository.GetAllvwWarehouseLocationItem()
                        //    .Where(q => q.ProductId == product && q.BranchId == branch && q.IsSale == true && q.IsOut == false && q.ProductOutboundId == null && q.ProductOutboundDetailId == null)
                        //    .OrderBy(x => x.ExpiryDate)
                        //    .Take(quantity).ToList();
                        //AutoMapper.Mapper.CreateMap<vwWarehouseLocationItem, WarehouseLocationItem>();
                        //AutoMapper.Mapper.Initialize(cfg =>
                        //{
                        //    cfg.CreateMap<vwWarehouseLocationItem, WarehouseLocationItem>();
                        //    /* etc */
                        //});


                        //AutoMapper.Mapper.Map(listLocationItemExits, list);
                        //for (int location = 0; location < list.Count(); location++)
                        //{
                        //    list[location].ProductOutboundId = product_outbound[i].Id;
                        //    list[location].ProductOutboundDetailId = productOutboundDetail.Id;
                        //    list[location].ModifiedDate = DateTime.Now;
                        //    list[location].ModifiedUserId = model.CreatedUserId;
                        //    _iwarehouseLocationItemRepository.UpdateWarehouseLocationItem(list[location]);
                        //}
                    }
                    product_outbound[i].TotalAmount = invoice_detail_list.Sum(x => (x.Price * x.Quantity));
                    //Ghi sổ chứng từ phiếu xuất
                    ProductOutboundController.Archive(product_outbound[i]);
                }
                #endregion
            }
            #endregion

            #region  xóa hết lịch sử giao dịch
            //xóa lịch sử giao dịch có liên quan đến đơn hàng, gồm: 1 dòng giao dịch bán hàng, 1 dòng thu tiền.
            var transaction_Liablities = _itransactionLiabilitiesRepository.GetAllTransaction().Where(x => x.MaChungTuGoc == productInvoice.Code && x.LoaiChungTuGoc == "ProductInvoice").ToList();
            if (transaction_Liablities.Count() > 0)
            {
                for (int i = 0; i < transaction_Liablities.Count(); i++)
                {
                    _itransactionLiabilitiesRepository.DeleteTransaction(transaction_Liablities[i].Id);
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
                productInvoice.ModifiedUserId = model.CreatedUserId;
                _iproductInvoiceRepository.UpdateProductInvoice(productInvoice);

                //Lấy mã KH
                var customer = _iCustomerRepository.GetCustomerById(productInvoice.CustomerId.Value);

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
                Erp.BackOffice.Account.Controllers.TransactionLiabilitiesController.Create_mobile(
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
                        null, model.CreatedUserId.GetValueOrDefault());
                #endregion

                #region phiếu thu
                //Khách thanh toán ngay
                if (model.ReceiptViewModel.Amount > 0)
                {
                    #region xóa phiếu thu cũ (nếu có)
                    var receipt = _iReceiptRepository.GetAllReceiptFull()
                        .Where(item => item.MaChungTuGoc == productInvoice.Code).ToList();
                    var receipt_detail = _ireceiptDetailRepository.GetAllReceiptDetailFull().ToList();
                    receipt_detail = receipt_detail.Where(x => x.MaChungTuGoc == productInvoice.Code).ToList();
                    if (receipt_detail.Count() > 0)
                    {
                        // isDelete chi tiết phiếu thu
                        for (int i = 0; i < receipt_detail.Count(); i++)
                        {
                            _ireceiptDetailRepository.DeleteReceiptDetail(receipt_detail[i].Id);
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
                        if (receipts.Amount > Convert.ToDecimal(productInvoice.TotalAmount))
                            receipts.Amount = Convert.ToDecimal(productInvoice.TotalAmount);

                        _iReceiptRepository.UpdateReceipt(receipts);

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
                        Erp.BackOffice.Account.Controllers.TransactionLiabilitiesController.Create_mobile(
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
                            null, model.CreatedUserId.GetValueOrDefault());
                        #endregion
                    }
                    else
                    {
                        #region thêm mới phiếu thu
                        //Lập phiếu thu
                        var receipt_inser = new Receipt();

                        //AutoMapper.Mapper.Initialize(cfg =>
                        //{
                        //    cfg.CreateMap<ReceiptViewModel, Receipt>();
                        //    /* etc */
                        //});
                        AutoMapper.Mapper.CreateMap<ReceiptViewModel, Receipt>();
                        AutoMapper.Mapper.Map(model.ReceiptViewModel, receipt_inser);
                        receipt_inser.IsDeleted = false;
                        receipt_inser.CreatedUserId = model.CreatedUserId;
                        receipt_inser.ModifiedUserId = model.CreatedUserId;
                        receipt_inser.AssignedUserId = model.CreatedUserId;
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
                        receipt_inser.BranchId = model.BranchId.GetValueOrDefault();

                        if (receipt_inser.Amount > Convert.ToDecimal(productInvoice.TotalAmount))
                            receipt_inser.Amount = Convert.ToDecimal(productInvoice.TotalAmount);

                        _iReceiptRepository.InsertReceipt(receipt_inser);

                        var prefixReceipt = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_ReceiptCustomer");
                        receipt_inser.Code = Erp.BackOffice.Helpers.Common.GetCode_mobile(prefixReceipt, receipt_inser.Id, "");
                        _iReceiptRepository.UpdateReceipt(receipt_inser);
                        ////Thêm vào quản lý chứng từ

                        //Ghi Có TK 131 - Phải thu của khách hàng.
                        Erp.BackOffice.Account.Controllers.TransactionLiabilitiesController.Create_mobile(
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
                            null, model.CreatedUserId.GetValueOrDefault());

                        #endregion
                    }
                }
                #endregion

                #region cập nhật đơn bán hàng
                //Cập nhật đơn hàng
                productInvoice.ModifiedUserId = model.CreatedUserId;
                productInvoice.ModifiedDate = DateTime.Now;
                productInvoice.IsArchive = true;
                productInvoice.Status = Wording.OrderStatus_complete;
                _iproductInvoiceRepository.UpdateProductInvoice(productInvoice);
                #endregion
                //cap nhat chiet khau nha thuoc
                Erp.BackOffice.Sale.Controllers.TotalDiscountMoneyNTController.SyncTotalDisCountMoneyNT(productInvoice, model.CreatedUserId.GetValueOrDefault());
                //cap nhat hoa hong nhan vien
                Erp.BackOffice.Staff.Controllers.HistoryCommissionStaffController.SyncCommissionStaff(productInvoice, model.CreatedUserId.GetValueOrDefault());
            }
            return model;
        }

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



        [System.Web.Http.HttpPost, System.Web.Http.Route(UrlCommon.Getdetail_Invoice)]
        public IHttpActionResult Getdetail_Invoice()
        {

            var userRequestModel = GetRequestData<User>();
            int pID_Invoice = 0;
            pID_Invoice = userRequestModel.Id;
            var productInvoice = new vwProductInvoice();
            if (pID_Invoice > 0)
            {
                productInvoice = _iproductInvoiceRepository.GetvwProductInvoiceFullById(pID_Invoice);
            }


            if (productInvoice == null)
            {
                return null;
            }

            var model = new ProductInvoiceViewModel();
            AutoMapper.Mapper.CreateMap<vwProductInvoice, ProductInvoiceViewModel>();
            //AutoMapper.Mapper.Initialize(cfg =>
            //{
            //    cfg.CreateMap<vwProductInvoice, ProductInvoiceViewModel>();
            //    /* etc */
            //});
            AutoMapper.Mapper.Map(productInvoice, model);

            model.ReceiptViewModel = new ReceiptViewModel();
            model.NextPaymentDate_Temp = DateTime.Now.AddDays(30);
            model.ReceiptViewModel.Name = "Bán hàng - Thu tiền mặt";
            model.ReceiptViewModel.Amount = Convert.ToDecimal(productInvoice.TotalAmount);

            //Lấy thông tin kiểm tra cho phép sửa chứng từ này hay không

            //Lấy lịch sử giao dịch thanh toán
            var listTransaction = _itransactionLiabilitiesRepository.GetAllvwTransaction()
                        .Where(item => item.MaChungTuGoc == productInvoice.Code)
                        .OrderByDescending(item => item.CreatedDate)
                        .ToList();

            model.ListTransactionLiabilities = new List<TransactionLiabilitiesViewModel>();
            AutoMapper.Mapper.CreateMap<vwTransactionLiabilities, TransactionLiabilitiesViewModel>();
            //AutoMapper.Mapper.Initialize(cfg =>
            //{
            //    cfg.CreateMap<vwTransactionLiabilities, TransactionLiabilitiesViewModel>();
            //    /* etc */
            //});
            AutoMapper.Mapper.Map(listTransaction, model.ListTransactionLiabilities);

            model.Code = productInvoice.Code;
            //model.SalerId = productInvoice.SalerId;
            //model.SalerName = productInvoice.SalerFullName;
            var saleDepartmentCode = Erp.BackOffice.Helpers.Common.GetSetting("SaleDepartmentCode");

            //Lấy danh sách chi tiết đơn hàng
            model.DetailList = _iproductInvoiceRepository.GetAllvwInvoiceDetailsByInvoiceId(productInvoice.Id).Select(x =>
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
                }).ToList();


            model.DetailList = model.DetailList.Select(x =>
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
                     ExpiryDate = x.ExpiryDate,
                     strExpiryDate = x.ExpiryDate.ToStringFormat("dd/MM/yyyy")
                 }).ToList();


            model.GroupProduct = model.DetailList.GroupBy(x => new { x.ProductGroup }, (key, group) => new ProductInvoiceDetailViewModel
            {
                ProductGroup = key.ProductGroup,
                ProductId = group.FirstOrDefault().ProductId,
                Id = group.FirstOrDefault().Id
            }).ToList();

            //foreach (var item in model.GroupProduct)
            //{
            //    if (!string.IsNullOrEmpty(item.ProductGroup))
            //    {
            //        var ProductGroupName = categoryRepository.GetCategoryByCode("Categories_product").Where(x => x.Value == item.ProductGroup).FirstOrDefault();
            //        item.ProductGroupName = ProductGroupName.Name;
            //    }
            //}

            ////Lấy thông tin phiếu xuất kho
            //if (productInvoice.ProductOutboundId != null && productInvoice.ProductOutboundId > 0)
            //{
            //    var productOutbound = productOutboundRepository.GetvwProductOutboundById(productInvoice.ProductOutboundId.Value);
            //    model.ProductOutboundViewModel = new ProductOutboundViewModel();
            //    AutoMapper.Mapper.Map(productOutbound, model.ProductOutboundViewModel);
            //}


            return DSofTResult(model);
        }

    }
}