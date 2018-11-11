using System.Globalization;
using Erp.BackOffice.Account.Models;
using Erp.BackOffice.Filters;
using Erp.Domain.Entities;
using Erp.Domain.Interfaces;
using Erp.Domain.Account.Entities;
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
using Erp.Domain.Account.Repositories;
using System.ComponentModel.DataAnnotations;
using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Sale.Interfaces;
using Erp.BackOffice.Sale.Models;
using Erp.Domain.Staff.Interfaces;

namespace Erp.BackOffice.Account.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class TransactionLiabilitiesController : Controller
    {
        private readonly ITransactionLiabilitiesRepository transactionLiabilitiesRepository;
        private readonly IUserRepository userRepository;
        private readonly IProcessPaymentRepository processPaymentRepository;
        private readonly IReceiptRepository receiptRepository;
        private readonly IPaymentRepository paymentRepository;
        private readonly IProductInvoiceRepository productInvoiceRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly IReceiptDetailRepository receiptDetailRepository;
        private readonly IPurchaseOrderRepository purchaseOrderRepository;
        private readonly ISupplierRepository supplierRepository;
        private readonly IProductOutboundRepository productOutboundRepository;
        private readonly IBranchRepository branchRepository;
        private readonly IWarehouseRepository warehouseRepository;
        public TransactionLiabilitiesController(
            ITransactionLiabilitiesRepository _Transaction
            , IUserRepository _user
            , IProcessPaymentRepository _ProcessPayment
            , IReceiptRepository _Receipt
            , IPaymentRepository _Payment
            , IProductInvoiceRepository _ProductInvoice
            , ICustomerRepository _Customer
            , IReceiptDetailRepository _ReceiptDetail
            ,IPurchaseOrderRepository purchase
            ,ISupplierRepository supplier
            ,IProductOutboundRepository productOutbound
            , IBranchRepository branch
            ,IWarehouseRepository warehouse
            )
        {
            transactionLiabilitiesRepository = _Transaction;
            userRepository = _user;
            processPaymentRepository = _ProcessPayment;
            receiptRepository = _Receipt;
            paymentRepository = _Payment;
            productInvoiceRepository = _ProductInvoice;
            customerRepository = _Customer;
            receiptDetailRepository = _ReceiptDetail;
            purchaseOrderRepository = purchase;
            supplierRepository = supplier;
            productOutboundRepository = productOutbound;
            branchRepository = branch;
            warehouseRepository = warehouse;
        }

        #region Index

        public ViewResult Index()
        {
            List<TransactionLiabilitiesViewModel> q = transactionLiabilitiesRepository.GetAllTransaction()
                .Select(item => new TransactionLiabilitiesViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    ModifiedDate = item.ModifiedDate,
                    TransactionCode = item.TransactionCode,
                    TransactionModule = item.TransactionModule,
                    TransactionName = item.TransactionName,
                    Debit = item.Debit,
                    Credit = item.Credit
                }).OrderByDescending(m => m.CreatedDate).ToList();

            //foreach (var item in q)
            //{
            //    item.SubListTransaction = transactionRepository.GetAllTransaction()
            //    .Select(i => new TransactionLiabilitiesViewModel
            //    {
            //        Id = i.Id,
            //        CreatedDate = i.CreatedDate,
            //        TransactionCode = i.TransactionCode,
            //        TransactionModule = item.TransactionModule,
            //        TransactionName = item.TransactionName,
            //        Debit = item.Debit,
            //        Credit = item.Credit
            //    }).OrderByDescending(m => m.CreatedDate).ToList();
            //}

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        #endregion

        #region Liabilities
        public ActionResult LiabilitiesCustomer(string TargetCode, string TargetName, string AllData)
        {
            var model = transactionLiabilitiesRepository.GetvwAccount_Liabilities().Where(item => item.TargetModule == "Customer");

            if (!string.IsNullOrEmpty(TargetCode))
            {
                model = model.Where(item => item.TargetCode == TargetCode);
            }

            if (!string.IsNullOrEmpty(TargetName))
            {
                model = model.Where(item => item.TargetName.Contains(TargetName));
            }

            if (AllData != "on")
            {
                model = model.Where(item => item.Remain > 0);
            }

            model = model.OrderBy(item => item.TargetName);
            return View(model);
        }

        public ActionResult LiabilitiesSupplier(string TargetCode, string TargetName, string AllData)
        {
            var model = transactionLiabilitiesRepository.GetvwAccount_Liabilities().Where(item => item.TargetModule == "Supplier");

            if (!string.IsNullOrEmpty(TargetCode))
            {
                model = model.Where(item => item.TargetCode == TargetCode);
            }

            if (!string.IsNullOrEmpty(TargetName))
            {
                model = model.Where(item => item.TargetName.Contains(TargetName));
            }

            if (AllData != "on")
            {
                model = model.Where(item => item.Remain > 0);
            }

            model = model.OrderBy(item => item.TargetName);
            return View(model);
        }

        public ActionResult LiabilitiesDrugStore(string TargetCode, string TargetName, string AllData)
        {
            var model = transactionLiabilitiesRepository.GetvwAccount_Liabilities().Where(item => item.TargetModule == "DrugStore");

            if (!string.IsNullOrEmpty(TargetCode))
            {
                model = model.Where(item => item.TargetCode == TargetCode);
            }

            if (!string.IsNullOrEmpty(TargetName))
            {
                model = model.Where(item => item.TargetName.Contains(TargetName));
            }

            if (AllData != "on")
            {
                model = model.Where(item => item.Remain > 0);
            }

            model = model.OrderBy(item => item.TargetName);
            return View(model);
        }

        public ViewResult LiabilitiesDetail(string TargetModule, string TargetCode, string TargetName, string TransactionCode)
        {
            var model = new ResolveLiabilitiesViewModel();
            if (TargetModule == "Customer")
            {
                model.TransactionName = "Bán hàng";
                model.NextPaymentDate = DateTime.Now.AddMonths(1);
                model.TargetModule = TargetModule;
                model.TargetName = TargetName;
                model.Amount = 0;

                //Lấy danh sách chứng từ bán hàng của KH này
                var listProductInvoice = productInvoiceRepository.GetAllvwProductInvoice()
                    .Where(item => item.IsArchive && item.CustomerCode == TargetCode)
                    .Select(item => new ProductInvoiceViewModel
                    {
                        Id = item.Id,
                        Code = item.Code,
                        CreatedDate = item.CreatedDate,
                        TotalAmount = item.TotalAmount,
                        PaidAmount = item.PaidAmount,
                        RemainingAmount = item.RemainingAmount,
                        Note = item.Note,
                        NextPaymentDate = item.NextPaymentDate
                    }).OrderBy(x => x.NextPaymentDate)
                    .ToList();
                 
                foreach (var item in listProductInvoice)
                {
                    item.ListTransactionLiabilities = new List<TransactionLiabilitiesViewModel>();

                    var q = transactionLiabilitiesRepository.GetAllvwTransaction()
                    .Where(x => x.MaChungTuGoc == item.Code)
                    .OrderByDescending(x => x.CreatedDate)
                    .ToList();

                    item.PaidAmount = q.Sum(i => i.Credit);
                    item.RemainingAmount = item.TotalAmount - item.PaidAmount;

                    AutoMapper.Mapper.Map(q, item.ListTransactionLiabilities);
                }

                //int n = 0;
                //foreach (var item in listProductInvoice)
                //{
                //    item.OrderNo = n;
                //    n++;
                //}
                model.ListProductInvoice = new List<ProductInvoiceViewModel>();
                AutoMapper.Mapper.Map(listProductInvoice, model.ListProductInvoice);
            }
            else
            if (TargetModule == "DrugStore")
            {
                model.TransactionName = "Xuất hàng cho nhà thuốc";
                model.NextPaymentDate = DateTime.Now.AddMonths(1);
                model.TargetModule = TargetModule;
                model.TargetName = TargetName;
                model.Amount = 0;

                //Lấy danh sách chứng từ bán hàng của KH này
                var listProductInvoice = transactionLiabilitiesRepository.GetAllvwTransaction()
                    .Where(item => item.TargetModule=="DrugStore"&&item.TargetCode == TargetCode&&item.TransactionModule=="ProductOutbound")
                    .Select(item => new ProductInvoiceViewModel
                    {
                        Id = item.Id,
                        Code = item.TransactionCode,
                        CreatedDate = item.CreatedDate,
                        TotalAmount = item.Debit,
                      //  PaidAmount = item.PaidAmount,
                      //  RemainingAmount = item.Debit,
                        Note = item.Note,
                        NextPaymentDate = item.NextPaymentDate
                    }).OrderBy(x => x.NextPaymentDate)
                    .ToList();

                foreach (var item in listProductInvoice)
                {
                    item.ListTransactionLiabilities = new List<TransactionLiabilitiesViewModel>();

                    var q = transactionLiabilitiesRepository.GetAllvwTransaction()
                    .Where(x => x.MaChungTuGoc == item.Code)
                    .OrderByDescending(x => x.CreatedDate)
                    .ToList();

                    item.PaidAmount = q.Sum(i => i.Credit);
                    item.RemainingAmount = item.TotalAmount - item.PaidAmount;

                    AutoMapper.Mapper.Map(q, item.ListTransactionLiabilities);
                }

                //int n = 0;
                //foreach (var item in listProductInvoice)
                //{
                //    item.OrderNo = n;
                //    n++;
                //}
                model.ListProductInvoice = new List<ProductInvoiceViewModel>();
                AutoMapper.Mapper.Map(listProductInvoice, model.ListProductInvoice);
            }
            else
            {
                model.TransactionName = "Mua hàng";
                model.NextPaymentDate = DateTime.Now.AddMonths(1);
                model.TargetModule = TargetModule;
                model.TargetName = TargetName;
                model.Amount = 0;

                //Lấy danh sách chứng từ mua hàng từ nhà cung cấp này
                var listPurchaseOrder = purchaseOrderRepository.GetAllvwPurchaseOrder()
                    .Where(item => item.IsArchive.Value && item.SupplierCode == TargetCode)
                    .Select(item => new PurchaseOrderViewModel
                    {
                        Id = item.Id,
                        Code = item.Code,
                        CreatedDate = item.CreatedDate,
                        TotalAmount = item.TotalAmount,
                        PaidAmount = item.PaidAmount,
                        RemainingAmount = item.RemainingAmount,
                        Note = item.Note,
                        NextPaymentDate = item.NextPaymentDate
                    }).OrderBy(x => x.NextPaymentDate)
                    .ToList();

                foreach (var item in listPurchaseOrder)
                {
                    item.ListTransactionLiabilities = new List<TransactionLiabilitiesViewModel>();

                    var q = transactionLiabilitiesRepository.GetAllvwTransaction()
                    .Where(x => x.MaChungTuGoc == item.Code)
                    .OrderByDescending(x => x.CreatedDate)
                    .ToList();

                    item.PaidAmount = q.Sum(i => i.Credit);
                    item.RemainingAmount = item.TotalAmount - item.PaidAmount;

                    AutoMapper.Mapper.Map(q, item.ListTransactionLiabilities);
                }

                //int n = 0;
                //foreach (var item in listProductInvoice)
                //{
                //    item.OrderNo = n;
                //    n++;
                //}
                model.ListPurchaseOrder = new List<PurchaseOrderViewModel>();
                AutoMapper.Mapper.Map(listPurchaseOrder, model.ListPurchaseOrder);
            }

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];

            return View(model);
        }

        [HttpPost]
        public ActionResult ResolveLiabilities(List<ResolveLiabilitiesViewModel> model, string PaymentMethod, decimal? Amount, string TargetCode, string TargetModule)
        {
            //if (ModelState.IsValid)
            //{
            if (TargetModule == "Customer")
            {
                var customer = customerRepository.GetvwCustomerByCode(TargetCode);
                var receipt = new Receipt();
                receipt.IsDeleted = false;
                receipt.CreatedUserId = WebSecurity.CurrentUserId;
                receipt.ModifiedUserId = WebSecurity.CurrentUserId;
                receipt.AssignedUserId = WebSecurity.CurrentUserId;
                receipt.CreatedDate = DateTime.Now;
                receipt.ModifiedDate = DateTime.Now;
                receipt.VoucherDate = DateTime.Now;

                receipt.Name = "Thu tiền khách hàng";

                receipt.CustomerId = customer.Id;
                receipt.Address = customer.Address;

                receipt.Amount = Amount;
                receipt.PaymentMethod = PaymentMethod;
                receiptRepository.InsertReceipt(receipt);

                var prefix = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_ReceiptCustomer");
                receipt.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix, receipt.Id);
                receiptRepository.UpdateReceipt(receipt);
                foreach (var item in model)
                {
                    if (item.Amount != null)
                    {
                        ResolveLiabilities(item, PaymentMethod, receipt.Code, receipt.Id);
                    }
                }
            }
                else
                if (TargetModule == "DrugStore")
                {
                    var branch = branchRepository.GetvwBranchByCode(TargetCode);
                    var receipt = new Receipt();
                    receipt.IsDeleted = false;
                    receipt.CreatedUserId = WebSecurity.CurrentUserId;
                    receipt.ModifiedUserId = WebSecurity.CurrentUserId;
                    receipt.AssignedUserId = WebSecurity.CurrentUserId;
                    receipt.CreatedDate = DateTime.Now;
                    receipt.ModifiedDate = DateTime.Now;
                    receipt.VoucherDate = DateTime.Now;

                    receipt.Name = "Thu tiền nhà thuốc";
                    receipt.BranchId = branch.Id;
                    receipt.CustomerId = branch.Id;
                    receipt.Address = branch.Address;

                    receipt.Amount = Amount;
                    receipt.PaymentMethod = PaymentMethod;
                    receiptRepository.InsertReceipt(receipt);

                    var prefix = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_ReceiptCustomer");
                    receipt.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix, receipt.Id);
                    receiptRepository.UpdateReceipt(receipt);
                    foreach (var item in model)
                    {
                        if (item.Amount != null)
                        {
                            ResolveLiabilities(item, PaymentMethod, receipt.Code, receipt.Id);
                        }
                    }
                }
            else
            {
                var supplier = supplierRepository.GetAllSupplier().Where(x=>x.Code==TargetCode).FirstOrDefault();
                var payment = new Payment();
                payment.IsDeleted = false;
                payment.CreatedUserId = WebSecurity.CurrentUserId;
                payment.ModifiedUserId = WebSecurity.CurrentUserId;
                payment.AssignedUserId = WebSecurity.CurrentUserId;
                payment.CreatedDate = DateTime.Now;
                payment.ModifiedDate = DateTime.Now;
                payment.VoucherDate = DateTime.Now;

                payment.Name = "Trả tiền cho nhà cung cấp";

                payment.TargetId = supplier.Id;
                payment.TargetName = "Supplier";
                payment.Address = supplier.Address;
                payment.Amount = Amount;
                payment.PaymentMethod = PaymentMethod;
                paymentRepository.InsertPayment(payment);

                var prefix = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_PaymentSupplier");
                payment.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix, payment.Id);
                paymentRepository.UpdatePayment(payment);
                foreach (var item in model)
                {
                    if (item.Amount != null)
                    {
                        ResolveLiabilities(item, PaymentMethod, payment.Code, payment.Id);
                    }
                }
            }

                return Content("success");
            //}

            //return Content("error");
        }

        public void ResolveLiabilities(ResolveLiabilitiesViewModel model, string PaymentMethod, string receiptCode, int receiptId)
        {
            if (model.TargetModule == "Customer")
            {
                //Lấy đơn hàng và cập nhật số tiền đã trả
                var productInvoice = productInvoiceRepository.GetAllProductInvoice()
                    .Where(item => item.IsArchive && item.IsDeleted == false && item.Code == model.MaChungTuGoc).FirstOrDefault();
                productInvoice.PaidAmount += Convert.ToDecimal(model.Amount);
                productInvoice.RemainingAmount = productInvoice.TotalAmount - productInvoice.PaidAmount;
                productInvoiceRepository.UpdateProductInvoice(productInvoice);

                //Lấy thông tin KH
                var customer = customerRepository.GetCustomerById(productInvoice.CustomerId.Value);

                //Lập chi tiết phiếu thu

                var receiptDetail = new ReceiptDetail();
                receiptDetail.IsDeleted = false;
                receiptDetail.CreatedUserId = WebSecurity.CurrentUserId;
                receiptDetail.ModifiedUserId = WebSecurity.CurrentUserId;
                receiptDetail.AssignedUserId = WebSecurity.CurrentUserId;
                receiptDetail.CreatedDate = DateTime.Now;
                receiptDetail.ModifiedDate = DateTime.Now;

                receiptDetail.Name = "Thu tiền khách hàng";
                receiptDetail.Amount = model.Amount;
                receiptDetail.ReceiptId = receiptId;
                receiptDetail.MaChungTuGoc = productInvoice.Code;
                receiptDetail.LoaiChungTuGoc = "ProductInvoice";

                receiptDetailRepository.InsertReceiptDetail(receiptDetail);
                //Thêm vào quản lý chứng từ
                TransactionController.Create(new TransactionViewModel
                {
                    TransactionModule = "Receipt",
                    TransactionCode = receiptCode,
                    TransactionName = "Thu tiền khách hàng"
                });

                //Thêm chứng từ liên quan
                TransactionController.CreateRelationship(new TransactionRelationshipViewModel
                {
                    TransactionA = receiptCode,
                    TransactionB = productInvoice.Code
                });

                //Lấy lịch sử giao dịch thanh toán
                var q = transactionLiabilitiesRepository.GetAllvwTransaction()
                            .Where(item => item.MaChungTuGoc == productInvoice.Code).ToList();
                decimal du_no=0;
                if(q.Count>0)
                {
                      du_no =  q.Sum(item => item.Debit - item.Credit);
                }
                

                bool da_thanh_toan_du = false;
                if (Convert.ToDecimal(model.Amount) == du_no)
                {
                    da_thanh_toan_du = true;
                }

                //Ghi Có TK 131 - Phải thu của khách hàng.
                Erp.BackOffice.Account.Controllers.TransactionLiabilitiesController.Create(
                    receiptCode,
                    "Receipt",
                    "Thu tiền khách hàng",
                    model.TargetCode,
                    model.TargetModule,
                    0,
                    Convert.ToDecimal(model.Amount),
                    model.MaChungTuGoc,
                    model.LoaiChungTuGoc,
                    model.PaymentMethod = PaymentMethod,
                    da_thanh_toan_du ? null : model.NextPaymentDate,
                    model.Note);

                //Cập nhật ngày hẹn trả cho đơn hàng này
                productInvoice.NextPaymentDate = da_thanh_toan_du ? null : model.NextPaymentDate;
                productInvoiceRepository.UpdateProductInvoice(productInvoice);
            }
                else
                if (model.TargetModule == "DrugStore")
                {
                    ////Lấy đơn hàng và cập nhật số tiền đã trả
                    var productOutbound = productOutboundRepository.GetAllvwProductOutbound()
                        .Where(item => item.IsArchive && item.IsDeleted == false && item.Code == model.MaChungTuGoc).FirstOrDefault();
                    //productInvoice.PaidAmount += Convert.ToDecimal(model.Amount);
                    //productInvoice.RemainingAmount = productInvoice.TotalAmount - productInvoice.PaidAmount;
                    //productInvoiceRepository.UpdateProductInvoice(productInvoice);

                    //Lấy thông tin KH
                    //var wh = warehouseRepository.GetWarehouseById(productOutbound.WarehouseDestinationId.Value);
                    //var branch = branchRepository.GetBranchById(wh.BranchId.Value);

                    //Lập chi tiết phiếu thu

                    var receiptDetail = new ReceiptDetail();
                    receiptDetail.IsDeleted = false;
                    receiptDetail.CreatedUserId = WebSecurity.CurrentUserId;
                    receiptDetail.ModifiedUserId = WebSecurity.CurrentUserId;
                    receiptDetail.AssignedUserId = WebSecurity.CurrentUserId;
                    receiptDetail.CreatedDate = DateTime.Now;
                    receiptDetail.ModifiedDate = DateTime.Now;

                    receiptDetail.Name = "Thu tiền khách hàng";
                    receiptDetail.Amount = model.Amount;
                    receiptDetail.ReceiptId = receiptId;
                    receiptDetail.MaChungTuGoc = productOutbound.Code;
                    receiptDetail.LoaiChungTuGoc = "ProductOutbound";

                    receiptDetailRepository.InsertReceiptDetail(receiptDetail);
                    //Thêm vào quản lý chứng từ
                    TransactionController.Create(new TransactionViewModel
                    {
                        TransactionModule = "Receipt",
                        TransactionCode = receiptCode,
                        TransactionName = "Thu tiền nhà thuốc"
                    });

                    //Thêm chứng từ liên quan
                    TransactionController.CreateRelationship(new TransactionRelationshipViewModel
                    {
                        TransactionA = receiptCode,
                        TransactionB = productOutbound.Code
                    });

                    //Lấy lịch sử giao dịch thanh toán
                    var q = transactionLiabilitiesRepository.GetAllvwTransaction()
                                .Where(item => item.MaChungTuGoc == productOutbound.Code).ToList();
                    decimal du_no = 0;
                    if (q.Count > 0)
                    {
                        du_no = q.Sum(item => item.Debit - item.Credit);
                    }


                    bool da_thanh_toan_du = false;
                    if (Convert.ToDecimal(model.Amount) == du_no)
                    {
                        da_thanh_toan_du = true;
                    }

                    //Ghi Có TK 131 - Phải thu của khách hàng.
                    Erp.BackOffice.Account.Controllers.TransactionLiabilitiesController.Create(
                        receiptCode,
                        "Receipt",
                        "Thu tiền nhà thuốc",
                        model.TargetCode,
                        model.TargetModule,
                        0,
                        Convert.ToDecimal(model.Amount),
                        model.MaChungTuGoc,
                        model.LoaiChungTuGoc,
                        model.PaymentMethod = PaymentMethod,
                        da_thanh_toan_du ? null : model.NextPaymentDate,
                        model.Note);

                    ////Cập nhật ngày hẹn trả cho đơn hàng này
                    //productInvoice.NextPaymentDate = da_thanh_toan_du ? null : model.NextPaymentDate;
                    //productInvoiceRepository.UpdateProductInvoice(productInvoice);
                }
            else
            {
                //Lấy đơn hàng và cập nhật số tiền đã trả
                var purchaseOrder = purchaseOrderRepository.GetAllPurchaseOrder()
                    .Where(item => item.IsArchive.Value && item.IsDeleted == false && item.Code == model.MaChungTuGoc).FirstOrDefault();
                purchaseOrder.PaidAmount += Convert.ToDecimal(model.Amount);
                purchaseOrder.RemainingAmount = purchaseOrder.TotalAmount - purchaseOrder.PaidAmount;
                purchaseOrderRepository.UpdatePurchaseOrder(purchaseOrder);

                //Lấy thông tin KH
                var supplier = supplierRepository.GetSupplierById(purchaseOrder.SupplierId.Value);
                var payment = new Payment();
                payment.IsDeleted = false;
                payment.CreatedUserId = WebSecurity.CurrentUserId;
                payment.ModifiedUserId = WebSecurity.CurrentUserId;
                payment.AssignedUserId = WebSecurity.CurrentUserId;
                payment.CreatedDate = DateTime.Now;
                payment.ModifiedDate = DateTime.Now;
                payment.MaChungTuGoc = model.MaChungTuGoc;
                payment.LoaiChungTuGoc = model.LoaiChungTuGoc;

                paymentRepository.InsertPayment(payment);

                var prefix = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_PaymentSupplier");
                payment.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix, payment.Id);
                paymentRepository.UpdatePayment(payment);

                //Thêm vào quản lý chứng từ
                TransactionController.Create(new TransactionViewModel
                {
                    TransactionModule = "Payment",
                    TransactionCode = payment.Code,
                    TransactionName = "Chi tiền nhà cung cấp"
                });

                //Thêm chứng từ liên quan
                TransactionController.CreateRelationship(new TransactionRelationshipViewModel
                {
                    TransactionA = payment.Code,
                    TransactionB = purchaseOrder.Code
                });
                //Lấy lịch sử giao dịch thanh toán
                var q = transactionLiabilitiesRepository.GetAllvwTransaction()
                            .Where(item => item.MaChungTuGoc == purchaseOrder.Code).ToList();
                decimal du_no = 0;
                if (q.Count > 0)
                {
                    du_no = q.Sum(item => item.Debit - item.Credit);
                }


                bool da_thanh_toan_du = false;
                if (Convert.ToDecimal(model.Amount) == du_no)
                {
                    da_thanh_toan_du = true;
                }
                //Ghi Có TK ??? - Phải trả nhà cung cấp
                Erp.BackOffice.Account.Controllers.TransactionLiabilitiesController.Create(
                    payment.Code,
                    "Payment",
                    "Chi tiền nhà cung cấp",
                    model.TargetCode,
                    model.TargetModule,
                    0,
                    Convert.ToDecimal(model.Amount),
                    model.MaChungTuGoc,
                    model.LoaiChungTuGoc,
                    model.PaymentMethod = PaymentMethod,
                    model.NextPaymentDate,
                    model.Note);
                //Cập nhật ngày hẹn trả cho đơn hàng này
                purchaseOrder.NextPaymentDate = da_thanh_toan_du ? null : model.NextPaymentDate;
                purchaseOrderRepository.UpdatePurchaseOrder(purchaseOrder);
            }

            //Update Process Payment
            if (model.ProcessPaymentId > 0)
            {
                var processPayment = processPaymentRepository.GetProcessPaymentById(model.ProcessPaymentId.Value);
                processPayment.ModifiedUserId = WebSecurity.CurrentUserId;
                processPayment.ModifiedDate = DateTime.Now;
                processPayment.Status = "Đã thanh toán";

                processPaymentRepository.UpdateProcessPayment(processPayment);
            }
        }
   
        public ViewResult TrackLiabilities(int? year, int? month,string type)
        {
            if (year == null)
            {
                year = DateTime.Now.Year;
                month = DateTime.Now.Month;
            }
            if (type.Contains("Customer") == true)
            {
                #region listProductInvoice
                //Lấy danh sách chứng từ công nợ
                var listProductInvoice = productInvoiceRepository.GetAllvwProductInvoice()
                    .Where(item => item.NextPaymentDate != null
                            && item.NextPaymentDate.Value.Year == year
                            && item.NextPaymentDate.Value.Month == month)
                    .Select(item => new ProductInvoiceViewModel
                    {
                        Id = item.Id,
                        Code = item.Code,
                        CreatedDate = item.CreatedDate,
                        TotalAmount = item.TotalAmount,
                        PaidAmount = item.PaidAmount,
                        RemainingAmount = item.RemainingAmount,
                        Note = item.Note,
                        CustomerName = item.CustomerName,
                        CustomerPhone = item.CustomerPhone,
                        CustomerCode = item.CustomerCode,
                        NextPaymentDate = item.NextPaymentDate
                    }).OrderBy(x => x.NextPaymentDate).ToList();

                foreach (var item in listProductInvoice)
                {
                    //item.ListTransactionLiabilities = new List<TransactionLiabilitiesViewModel>();

                    var q = transactionLiabilitiesRepository.GetAllvwTransaction()
                    .Where(x => x.MaChungTuGoc == item.Code)
                    .OrderByDescending(x => x.CreatedDate)
                    .ToList();

                    item.PaidAmount = q.Sum(i => i.Credit);
                    item.RemainingAmount = item.TotalAmount - item.PaidAmount;

                    //AutoMapper.Mapper.Map(q, item.ListTransactionLiabilities);
                }
                ViewBag.Year = year;
                ViewBag.Month = month;
                ViewBag.Type = type;
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
                ViewBag.FailedMessage = TempData["FailedMessage"];
                ViewBag.AlertMessage = TempData["AlertMessage"];
                return View("TrackLiabilities", listProductInvoice);
                #endregion
            }
            else
            {
                #region listPurchaseOrder
                //Lấy danh sách chứng từ công nợ
                var listPurchaseOrder = purchaseOrderRepository.GetAllvwPurchaseOrder()
                    .Where(item => item.NextPaymentDate != null
                            && item.NextPaymentDate.Value.Year == year
                            && item.NextPaymentDate.Value.Month == month)
                    .Select(item => new PurchaseOrderViewModel
                    {
                        Id = item.Id,
                        Code = item.Code,
                        CreatedDate = item.CreatedDate,
                        TotalAmount = item.TotalAmount,
                        PaidAmount = item.PaidAmount,
                        RemainingAmount = item.RemainingAmount,
                        Note = item.Note,
                        SupplierName = item.SupplierName,
                        //Phone = item.Phone,
                        SupplierCode = item.SupplierCode,
                        NextPaymentDate = item.NextPaymentDate
                    }).OrderBy(x => x.NextPaymentDate).ToList();

                foreach (var item in listPurchaseOrder)
                {
                    //item.ListTransactionLiabilities = new List<TransactionLiabilitiesViewModel>();

                    var q = transactionLiabilitiesRepository.GetAllvwTransaction()
                    .Where(x => x.MaChungTuGoc == item.Code)
                    .OrderByDescending(x => x.CreatedDate)
                    .ToList();

                    item.PaidAmount = q.Sum(i => i.Credit);
                    item.RemainingAmount = item.TotalAmount - item.PaidAmount;

                    //AutoMapper.Mapper.Map(q, item.ListTransactionLiabilities);
                }
                ViewBag.Year = year;
                ViewBag.Month = month;
                ViewBag.Type = type;
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
                ViewBag.FailedMessage = TempData["FailedMessage"];
                ViewBag.AlertMessage = TempData["AlertMessage"];
                return View("TrackLiabilitiesSupplier", listPurchaseOrder);
                #endregion
            }
        }

        public ViewResult TrackLiabilitiesExpire(int? DateRange, string type)
        {
            if (DateRange == null)
            {
                if (type.Contains("Customer") == true)
                {
                    #region listProductInvoice
                    //Lấy danh sách chứng từ công nợ
                    var listProductInvoice = productInvoiceRepository.GetAllvwProductInvoice()
                        .Where(item => item.NextPaymentDate != null
                            && item.NextPaymentDate < DateTime.Now)
                        .Select(item => new ProductInvoiceViewModel
                        {
                            Id = item.Id,
                            Code = item.Code,
                            CreatedDate = item.CreatedDate,
                            TotalAmount = item.TotalAmount,
                            PaidAmount = item.PaidAmount,
                            RemainingAmount = item.RemainingAmount,
                            Note = item.Note,
                            CustomerName = item.CustomerName,
                            CustomerPhone = item.CustomerPhone,
                            CustomerCode = item.CustomerCode,
                            NextPaymentDate = item.NextPaymentDate
                        }).OrderBy(x => x.NextPaymentDate).ToList();

                    foreach (var item in listProductInvoice)
                    {
                        //item.ListTransactionLiabilities = new List<TransactionLiabilitiesViewModel>();

                        var q = transactionLiabilitiesRepository.GetAllvwTransaction()
                        .Where(x => x.MaChungTuGoc == item.Code)
                        .OrderByDescending(x => x.CreatedDate)
                        .ToList();

                        item.PaidAmount = q.Sum(i => i.Credit);
                        item.RemainingAmount = item.TotalAmount - item.PaidAmount;

                        //AutoMapper.Mapper.Map(q, item.ListTransactionLiabilities);
                    }

                    return View("TrackLiabilitiesExpire", listProductInvoice);
                    #endregion
                }
                else
                {
                    #region listPurchaseOrder
                    //Lấy danh sách chứng từ công nợ
                    var listPurchaseOrder = purchaseOrderRepository.GetAllvwPurchaseOrder()
                        .Where(item => item.NextPaymentDate != null
                            && item.NextPaymentDate < DateTime.Now)
                        .Select(item => new PurchaseOrderViewModel
                        {
                            Id = item.Id,
                            Code = item.Code,
                            CreatedDate = item.CreatedDate,
                            TotalAmount = item.TotalAmount,
                            PaidAmount = item.PaidAmount,
                            RemainingAmount = item.RemainingAmount,
                            Note = item.Note,
                            SupplierName = item.SupplierName,
                            //Phone = item.Phone,
                            SupplierCode = item.SupplierCode,
                            NextPaymentDate = item.NextPaymentDate
                        }).OrderBy(x => x.NextPaymentDate).ToList();

                    foreach (var item in listPurchaseOrder)
                    {
                        //item.ListTransactionLiabilities = new List<TransactionLiabilitiesViewModel>();

                        var q = transactionLiabilitiesRepository.GetAllvwTransaction()
                        .Where(x => x.MaChungTuGoc == item.Code)
                        .OrderByDescending(x => x.CreatedDate)
                        .ToList();

                        item.PaidAmount = q.Sum(i => i.Credit);
                        item.RemainingAmount = item.TotalAmount - item.PaidAmount;

                        //AutoMapper.Mapper.Map(q, item.ListTransactionLiabilities);
                    }

                    return View("TrackLiabilitiesExpireSupplier", listPurchaseOrder);
                    #endregion
                }
            }
            else
            {
                var date = DateTime.Now.AddDays(DateRange.Value);

                if (type.Contains("Customer") == true)
                {
                    #region listProductInvoice
                    //Lấy danh sách chứng từ công nợ
                    var listProductInvoice = productInvoiceRepository.GetAllvwProductInvoice()
                        .Where(item => item.NextPaymentDate != null
                            && item.NextPaymentDate > DateTime.Now
                            && item.NextPaymentDate < date)
                        .Select(item => new ProductInvoiceViewModel
                        {
                            Id = item.Id,
                            Code = item.Code,
                            CreatedDate = item.CreatedDate,
                            TotalAmount = item.TotalAmount,
                            PaidAmount = item.PaidAmount,
                            RemainingAmount = item.RemainingAmount,
                            Note = item.Note,
                            CustomerName = item.CustomerName,
                            CustomerPhone = item.CustomerPhone,
                            CustomerCode = item.CustomerCode,
                            NextPaymentDate = item.NextPaymentDate
                        }).OrderBy(x => x.NextPaymentDate).ToList();

                    foreach (var item in listProductInvoice)
                    {
                        //item.ListTransactionLiabilities = new List<TransactionLiabilitiesViewModel>();

                        var q = transactionLiabilitiesRepository.GetAllvwTransaction()
                        .Where(x => x.MaChungTuGoc == item.Code)
                        .OrderByDescending(x => x.CreatedDate)
                        .ToList();

                        item.PaidAmount = q.Sum(i => i.Credit);
                        item.RemainingAmount = item.TotalAmount - item.PaidAmount;

                        //AutoMapper.Mapper.Map(q, item.ListTransactionLiabilities);
                    }

                    return View("TrackLiabilitiesExpire", listProductInvoice);
                     #endregion
                }
                else
                {
                    #region listProductInvoice
                    //Lấy danh sách chứng từ công nợ
                    var listPurchaseOrder = purchaseOrderRepository.GetAllvwPurchaseOrder()
                        .Where(item => item.NextPaymentDate != null
                            && item.NextPaymentDate > DateTime.Now
                            && item.NextPaymentDate < date)
                        .Select(item => new PurchaseOrderViewModel
                        {
                            Id = item.Id,
                            Code = item.Code,
                            CreatedDate = item.CreatedDate,
                            TotalAmount = item.TotalAmount,
                            PaidAmount = item.PaidAmount,
                            RemainingAmount = item.RemainingAmount,
                            Note = item.Note,
                            SupplierName = item.SupplierName,
                            //Phone = item.Phone,
                            SupplierCode = item.SupplierCode,
                            NextPaymentDate = item.NextPaymentDate
                        }).OrderBy(x => x.NextPaymentDate).ToList();

                    foreach (var item in listPurchaseOrder)
                    {
                        //item.ListTransactionLiabilities = new List<TransactionLiabilitiesViewModel>();

                        var q = transactionLiabilitiesRepository.GetAllvwTransaction()
                        .Where(x => x.MaChungTuGoc == item.Code)
                        .OrderByDescending(x => x.CreatedDate)
                        .ToList();

                        item.PaidAmount = q.Sum(i => i.Credit);
                        item.RemainingAmount = item.TotalAmount - item.PaidAmount;

                        //AutoMapper.Mapper.Map(q, item.ListTransactionLiabilities);
                    }

                    return View("TrackLiabilitiesExpireSupplier", listPurchaseOrder);
                    #endregion
                }
            }
        }

        public ViewResult TrackLiabilitiesSupplier(int? year, int? month)
        {
            if (year == null)
            {
                year = DateTime.Now.Year;
                month = DateTime.Now.Month;
            }

            //Lấy danh sách chứng từ công nợ
            var listPurchaseOrder = purchaseOrderRepository.GetAllvwPurchaseOrder()
                .Where(item => item.NextPaymentDate != null
                        && item.NextPaymentDate.Value.Year == year
                        && item.NextPaymentDate.Value.Month == month)
                .Select(item => new PurchaseOrderViewModel
                {
                    Id = item.Id,
                    Code = item.Code,
                    CreatedDate = item.CreatedDate,
                    TotalAmount = item.TotalAmount,
                    PaidAmount = item.PaidAmount,
                    RemainingAmount = item.RemainingAmount,
                    Note = item.Note,
                    SupplierName = item.SupplierName,
                    //Phone = item.Phone,
                    SupplierCode = item.SupplierCode,
                    NextPaymentDate = item.NextPaymentDate
                }).OrderBy(x => x.NextPaymentDate).ToList();

            foreach (var item in listPurchaseOrder)
            {
                //item.ListTransactionLiabilities = new List<TransactionLiabilitiesViewModel>();

                var q = transactionLiabilitiesRepository.GetAllvwTransaction()
                .Where(x => x.MaChungTuGoc == item.Code)
                .OrderByDescending(x => x.CreatedDate)
                .ToList();

                item.PaidAmount = q.Sum(i => i.Credit);
                item.RemainingAmount = item.TotalAmount - item.PaidAmount;

                //AutoMapper.Mapper.Map(q, item.ListTransactionLiabilities);
            }

            ViewBag.Year = year;
            ViewBag.Month = month;
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];

            return View(listPurchaseOrder);
        }

        public ViewResult TrackLiabilitiesExpireSupplier(int? DateRange)
        {
            if (DateRange == null)
            {
                //Lấy danh sách chứng từ công nợ
                var listPurchaseOrder = purchaseOrderRepository.GetAllvwPurchaseOrder()
                    .Where(item => item.NextPaymentDate != null
                        && item.NextPaymentDate < DateTime.Now)
                    .Select(item => new PurchaseOrderViewModel
                    {
                        Id = item.Id,
                        Code = item.Code,
                        CreatedDate = item.CreatedDate,
                        TotalAmount = item.TotalAmount,
                        PaidAmount = item.PaidAmount,
                        RemainingAmount = item.RemainingAmount,
                        Note = item.Note,
                        SupplierName = item.SupplierName,
                        //Phone = item.Phone,
                        SupplierCode = item.SupplierCode,
                        NextPaymentDate = item.NextPaymentDate
                    }).OrderBy(x => x.NextPaymentDate).ToList();

                foreach (var item in listPurchaseOrder)
                {
                    //item.ListTransactionLiabilities = new List<TransactionLiabilitiesViewModel>();

                    var q = transactionLiabilitiesRepository.GetAllvwTransaction()
                    .Where(x => x.MaChungTuGoc == item.Code)
                    .OrderByDescending(x => x.CreatedDate)
                    .ToList();

                    item.PaidAmount = q.Sum(i => i.Credit);
                    item.RemainingAmount = item.TotalAmount - item.PaidAmount;

                    //AutoMapper.Mapper.Map(q, item.ListTransactionLiabilities);
                }

                return View(listPurchaseOrder);
            }
            else
            {
                var date = DateTime.Now.AddDays(DateRange.Value);

                //Lấy danh sách chứng từ công nợ
                var listPurchaseOrder = purchaseOrderRepository.GetAllvwPurchaseOrder()
                    .Where(item => item.NextPaymentDate != null
                        && item.NextPaymentDate > DateTime.Now
                        && item.NextPaymentDate < date)
                    .Select(item => new PurchaseOrderViewModel
                    {
                        Id = item.Id,
                        Code = item.Code,
                        CreatedDate = item.CreatedDate,
                        TotalAmount = item.TotalAmount,
                        PaidAmount = item.PaidAmount,
                        RemainingAmount = item.RemainingAmount,
                        Note = item.Note,
                        SupplierName = item.SupplierName,
                        //Phone = item.Phone,
                        SupplierCode = item.SupplierCode,
                        NextPaymentDate = item.NextPaymentDate
                    }).OrderBy(x => x.NextPaymentDate).ToList();

                foreach (var item in listPurchaseOrder)
                {
                    //item.ListTransactionLiabilities = new List<TransactionLiabilitiesViewModel>();

                    var q = transactionLiabilitiesRepository.GetAllvwTransaction()
                    .Where(x => x.MaChungTuGoc == item.Code)
                    .OrderByDescending(x => x.CreatedDate)
                    .ToList();

                    item.PaidAmount = q.Sum(i => i.Credit);
                    item.RemainingAmount = item.TotalAmount - item.PaidAmount;

                    //AutoMapper.Mapper.Map(q, item.ListTransactionLiabilities);
                }

                return View(listPurchaseOrder);
            }
        }

        public ViewResult TrackLiabilitiesCustomer(int? year, int? month)
        {
            if (year == null)
            {
                year = DateTime.Now.Year;
                month = DateTime.Now.Month;
            }

            //Lấy danh sách chứng từ công nợ
            var listProductInvoice = productInvoiceRepository.GetAllvwProductInvoice()
                .Where(item => item.NextPaymentDate != null
                        && item.NextPaymentDate.Value.Year == year
                        && item.NextPaymentDate.Value.Month == month)
                .Select(item => new ProductInvoiceViewModel
                {
                    Id = item.Id,
                    Code = item.Code,
                    CreatedDate = item.CreatedDate,
                    TotalAmount = item.TotalAmount,
                    PaidAmount = item.PaidAmount,
                    RemainingAmount = item.RemainingAmount,
                    Note = item.Note,
                    CustomerName = item.CustomerName,
                    CustomerPhone = item.CustomerPhone,
                    CustomerCode = item.CustomerCode,
                    NextPaymentDate = item.NextPaymentDate
                }).OrderBy(x => x.NextPaymentDate).ToList();

            foreach (var item in listProductInvoice)
            {
                //item.ListTransactionLiabilities = new List<TransactionLiabilitiesViewModel>();

                var q = transactionLiabilitiesRepository.GetAllvwTransaction()
                .Where(x => x.MaChungTuGoc == item.Code)
                .OrderByDescending(x => x.CreatedDate)
                .ToList();

                item.PaidAmount = q.Sum(i => i.Credit);
                item.RemainingAmount = item.TotalAmount - item.PaidAmount;

                //AutoMapper.Mapper.Map(q, item.ListTransactionLiabilities);
            }

            ViewBag.Year = year;
            ViewBag.Month = month;
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];

            return View(listProductInvoice);
        }

        public ViewResult TrackLiabilitiesExpireCustomer(int? DateRange)
        {
            if (DateRange == null)
            {
                //Lấy danh sách chứng từ công nợ
                var listProductInvoice = productInvoiceRepository.GetAllvwProductInvoice()
                    .Where(item => item.NextPaymentDate != null
                        && item.NextPaymentDate < DateTime.Now)
                    .Select(item => new ProductInvoiceViewModel
                    {
                        Id = item.Id,
                        Code = item.Code,
                        CreatedDate = item.CreatedDate,
                        TotalAmount = item.TotalAmount,
                        PaidAmount = item.PaidAmount,
                        RemainingAmount = item.RemainingAmount,
                        Note = item.Note,
                        CustomerName = item.CustomerName,
                        CustomerPhone = item.CustomerPhone,
                        CustomerCode = item.CustomerCode,
                        NextPaymentDate = item.NextPaymentDate
                    }).OrderBy(x => x.NextPaymentDate).ToList();

                foreach (var item in listProductInvoice)
                {
                    //item.ListTransactionLiabilities = new List<TransactionLiabilitiesViewModel>();

                    var q = transactionLiabilitiesRepository.GetAllvwTransaction()
                    .Where(x => x.MaChungTuGoc == item.Code)
                    .OrderByDescending(x => x.CreatedDate)
                    .ToList();

                    item.PaidAmount = q.Sum(i => i.Credit);
                    item.RemainingAmount = item.TotalAmount - item.PaidAmount;

                    //AutoMapper.Mapper.Map(q, item.ListTransactionLiabilities);
                }

                return View(listProductInvoice);
            }
            else
            {
                var date = DateTime.Now.AddDays(DateRange.Value);

                //Lấy danh sách chứng từ công nợ
                var listProductInvoice = productInvoiceRepository.GetAllvwProductInvoice()
                    .Where(item => item.NextPaymentDate != null
                        && item.NextPaymentDate > DateTime.Now
                        && item.NextPaymentDate < date)
                    .Select(item => new ProductInvoiceViewModel
                    {
                        Id = item.Id,
                        Code = item.Code,
                        CreatedDate = item.CreatedDate,
                        TotalAmount = item.TotalAmount,
                        PaidAmount = item.PaidAmount,
                        RemainingAmount = item.RemainingAmount,
                        Note = item.Note,
                        CustomerName = item.CustomerName,
                        CustomerPhone = item.CustomerPhone,
                        CustomerCode = item.CustomerCode,
                        NextPaymentDate = item.NextPaymentDate
                    }).OrderBy(x => x.NextPaymentDate).ToList();

                foreach (var item in listProductInvoice)
                {
                    //item.ListTransactionLiabilities = new List<TransactionLiabilitiesViewModel>();

                    var q = transactionLiabilitiesRepository.GetAllvwTransaction()
                    .Where(x => x.MaChungTuGoc == item.Code)
                    .OrderByDescending(x => x.CreatedDate)
                    .ToList();

                    item.PaidAmount = q.Sum(i => i.Credit);
                    item.RemainingAmount = item.TotalAmount - item.PaidAmount;

                    //AutoMapper.Mapper.Map(q, item.ListTransactionLiabilities);
                }

                return View(listProductInvoice);
            }
        }
        #endregion

        #region Create
        public static TransactionLiabilities Create(string TransactionCode, string TransactionModule, string TransactionName, string TargetCode, string TargetModule, decimal Debit, decimal Credit, string MaChungTuGoc, string LoaiChungTuGoc, string PaymentMethod, DateTime? NextPaymentDate, string Note)
        {
            TransactionLiabilitiesRepository transactionRepository = new TransactionLiabilitiesRepository(new Domain.Account.ErpAccountDbContext());

            var transaction = new TransactionLiabilities();
            transaction.IsDeleted = false;
            transaction.CreatedUserId = WebSecurity.CurrentUserId;
            transaction.ModifiedUserId = WebSecurity.CurrentUserId;
            transaction.AssignedUserId = WebSecurity.CurrentUserId;
            transaction.CreatedDate = DateTime.Now;
            transaction.ModifiedDate = DateTime.Now;

            transaction.TransactionCode = TransactionCode;
            transaction.TransactionModule = TransactionModule;
            transaction.TransactionName = TransactionName;
            transaction.TargetCode = TargetCode;
            transaction.TargetModule = TargetModule;
            transaction.Debit = Debit;
            transaction.Credit = Credit;
            transaction.PaymentMethod = PaymentMethod;
            transaction.NextPaymentDate = NextPaymentDate;
            transaction.MaChungTuGoc = MaChungTuGoc;
            transaction.LoaiChungTuGoc = LoaiChungTuGoc;
            transaction.Note = Note;

            transactionRepository.InsertTransaction(transaction);
            return transaction;
        }



        public static TransactionLiabilities Create_mobile(string TransactionCode, string TransactionModule, string TransactionName, string TargetCode, string TargetModule, decimal Debit, decimal Credit, string MaChungTuGoc, string LoaiChungTuGoc, string PaymentMethod, DateTime? NextPaymentDate, string Note,int CurrentUserId)
        {
            TransactionLiabilitiesRepository transactionRepository = new TransactionLiabilitiesRepository(new Domain.Account.ErpAccountDbContext());

            var transaction = new TransactionLiabilities();
            transaction.IsDeleted = false;
            transaction.CreatedUserId = CurrentUserId;
            transaction.ModifiedUserId = CurrentUserId;
            transaction.AssignedUserId = CurrentUserId;
            transaction.CreatedDate = DateTime.Now;
            transaction.ModifiedDate = DateTime.Now;

            transaction.TransactionCode = TransactionCode;
            transaction.TransactionModule = TransactionModule;
            transaction.TransactionName = TransactionName;
            transaction.TargetCode = TargetCode;
            transaction.TargetModule = TargetModule;
            transaction.Debit = Debit;
            transaction.Credit = Credit;
            transaction.PaymentMethod = PaymentMethod;
            transaction.NextPaymentDate = NextPaymentDate;
            transaction.MaChungTuGoc = MaChungTuGoc;
            transaction.LoaiChungTuGoc = LoaiChungTuGoc;
            transaction.Note = Note;

            transactionRepository.InsertTransaction(transaction);
            return transaction;
        }
        #endregion

        //#region Edit
        //public static TransactionLiabilities Edit(string TransactionCode, string TransactionModule, string TransactionName, string TargetCode, string TargetModule, decimal Debit, decimal Credit, string MaChungTuGoc, string LoaiChungTuGoc, string PaymentMethod, DateTime? NextPaymentDate, string Note)
        //{
        //    TransactionLiabilitiesRepository transactionRepository = new TransactionLiabilitiesRepository(new Domain.Account.ErpAccountDbContext());
        //    var
        //    var transaction = new TransactionLiabilities();
        //    transaction.IsDeleted = false;
        //    transaction.CreatedUserId = WebSecurity.CurrentUserId;
        //    transaction.ModifiedUserId = WebSecurity.CurrentUserId;
        //    transaction.AssignedUserId = WebSecurity.CurrentUserId;
        //    transaction.CreatedDate = DateTime.Now;
        //    transaction.ModifiedDate = DateTime.Now;

        //    transaction.TransactionCode = TransactionCode;
        //    transaction.TransactionModule = TransactionModule;
        //    transaction.TransactionName = TransactionName;
        //    transaction.TargetCode = TargetCode;
        //    transaction.TargetModule = TargetModule;
        //    transaction.Debit = Debit;
        //    transaction.Credit = Credit;
        //    transaction.PaymentMethod = PaymentMethod;
        //    transaction.NextPaymentDate = NextPaymentDate;
        //    transaction.MaChungTuGoc = MaChungTuGoc;
        //    transaction.LoaiChungTuGoc = LoaiChungTuGoc;
        //    transaction.Note = Note;

        //    transactionRepository.InsertTransaction(transaction);
        //    return transaction;
        //}
        //#endregion
    }
}
