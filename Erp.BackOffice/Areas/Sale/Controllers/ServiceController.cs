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
using System.Web.Script.Serialization;
using System.IO;
using System.Text.RegularExpressions;
using Erp.Domain.Account.Interfaces;
using Erp.BackOffice.Account.Models;

namespace Erp.BackOffice.Sale.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class ServiceController : Controller
    {
        private readonly IProductOrServiceRepository ServiceRepository;
        private readonly IUserRepository userRepository;
        private readonly IObjectAttributeRepository ObjectAttributeRepository;
        private readonly IServiceComboRepository serviceComboRepository;
        private readonly IUsingServiceLogRepository usingServiceLogRepository;
        private readonly IProductInvoiceRepository productInvoiceRepository;
        private readonly ICustomerRepository CustomerRepository;
        private readonly IUsingServiceLogDetailRepository usingServiceLogDetailRepository;
        private readonly IServiceReminderGroupRepository serviceReminderGroupRepository;
        private readonly IServiceReminderRepository serviceReminderRepository;
        public ServiceController(
            IProductOrServiceRepository _Service
            , IUserRepository _user
            , IObjectAttributeRepository _ObjectAttribute
            , IServiceComboRepository serviceCombo
           , IUsingServiceLogRepository usingServiceLog
             , IProductInvoiceRepository productInvoice
            , ICustomerRepository _Customer
            ,IUsingServiceLogDetailRepository usingServiceLogDetail
            , IServiceReminderRepository serviceReminder
            , IServiceReminderGroupRepository serviceReminderGroup
            )
        {
            ServiceRepository = _Service;
            userRepository = _user;
            ObjectAttributeRepository = _ObjectAttribute;
            serviceComboRepository = serviceCombo;
            usingServiceLogRepository = usingServiceLog;
            productInvoiceRepository = productInvoice;
            CustomerRepository = _Customer;
            usingServiceLogDetailRepository = usingServiceLogDetail;
            serviceReminderGroupRepository = serviceReminderGroup;
            serviceReminderRepository = serviceReminder;
        }

        #region Index

        public ViewResult Index(string txtSearch, string txtCode,string CategoryCode, SearchObjectAttributeViewModel SearchOjectAttr)
        {

            IEnumerable<ServiceViewModel> q = ServiceRepository.GetAllvwService().AsEnumerable()
                .Select(item => new ServiceViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    CategoryCode = item.CategoryCode,
                    Code = item.Code,
                    Image_Name = item.Image_Name,
                    Name = item.Name,
                    PriceOutbound = item.PriceOutbound,
                    Unit = item.Unit,
                    Barcode = item.Barcode,
                    IsCombo = item.IsCombo,
                    //DiscountStaff = item.DiscountStaff,
                    //IsMoneyDiscount = item.IsMoneyDiscount
                }).OrderByDescending(m => m.ModifiedDate);
            if (SearchOjectAttr.ListField != null)
            {
                if (SearchOjectAttr.ListField.Count > 0)
                {
                    //lấy các đối tượng ObjectAttributeValue nào thỏa đk có AttributeId trong ListField và có giá trị tương ứng trong ListField
                    var listObjectAttrValue = ObjectAttributeRepository.GetAllObjectAttributeValue().AsEnumerable().Where(attr => SearchOjectAttr.ListField.Any(item => item.Id == attr.AttributeId && Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(attr.Value).Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.Value)))).ToList();

                    //tiếp theo tìm các sản phẩm có id bằng với ObjectId trong listObjectAttrValue vừa tìm được
                    q = q.Where(product => listObjectAttrValue.Any(item => item.ObjectId == product.Id));

                    ViewBag.ListOjectAttrSearch = new JavaScriptSerializer().Serialize(SearchOjectAttr.ListField.Select(x => new { Id = x.Id, Value = x.Value }));
                }
            }

            if (string.IsNullOrEmpty(txtSearch) == false)
            {
                txtSearch = txtSearch == "" ? "~" : Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(txtSearch);
                q = q.Where(x => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(x.Name).Contains(txtSearch));
            }
            if(string.IsNullOrEmpty(txtCode) == false)
            {
                txtCode = txtCode == "" ? "~" : txtCode.ToLower();
                q = q.Where(x => x.Code.ToLower().Contains(txtCode));

            }
            if (!string.IsNullOrEmpty(CategoryCode))
            {
                q = q.Where(x => x.CategoryCode == CategoryCode);
            }
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        #endregion

        #region LoadServiceItem
        public PartialViewResult LoadServiceItem(int OrderNo, int serviceId, string serviceName, int Quantity, string serviceCode)
        {
            var model = new ServiceComboViewModel();
            model.OrderNo = OrderNo;
            model.ServiceId = serviceId;
            model.Name = serviceName;
            model.Quantity = Quantity;
            model.Code = serviceCode;
            model.Id = 0;
            return PartialView(model);
        }
        #endregion

        #region LoadServiceItem
        public PartialViewResult LoadReminderItem(int OrderNo, int reminderId, string reminderName)
        {
            var model = new ServiceReminderGroupViewModel();
            model.OrderNo = OrderNo;
            model.ServiceReminderId = reminderId;
            model.Name = reminderName;
            model.Id = 0;
            return PartialView(model);
        }
        #endregion

        #region Create
        public ViewResult Create()
        {
            var model = new ServiceViewModel();
            model.PriceOutbound = 0;
            //model.Quantity = 0;
            string image_folder = Helpers.Common.GetSetting("service-image-folder");

            var serviceList = ServiceRepository.GetAllvwService()
               .Select(item => new ServiceViewModel
               {
                   Code = item.Code,
                   Barcode = item.Barcode,
                   Name = item.Name,
                   Id = item.Id,
                   CategoryCode = item.CategoryCode,
                   PriceOutbound = item.PriceOutbound,
                   Unit = item.Unit,
                   Image_Name = image_folder + item.Image_Name
               }).ToList();
            ViewBag.serviceList = serviceList;
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ServiceViewModel model)
        {
            if (ModelState.IsValid)
            {
                var Service = new Product();
                Service.IsDeleted = false;
                Service.CreatedUserId = WebSecurity.CurrentUserId;
                Service.ModifiedUserId = WebSecurity.CurrentUserId;
                Service.CreatedDate = DateTime.Now;
                Service.ModifiedDate = DateTime.Now;
                Service.Barcode = model.Barcode;
                Service.CategoryCode = model.CategoryCode;
                Service.Description = model.Description;
                Service.IsCombo = model.IsCombo;
                Service.Name = model.Name;
                Service.PriceOutbound = model.PriceOutbound;
                Service.Type = "service";
                Service.Unit = model.Unit;
                Service.Code = model.Code.Trim();
                var path = System.Web.HttpContext.Current.Server.MapPath("~" + Helpers.Common.GetSetting("service-image-folder"));
                if (Request.Files["file-image"] != null)
                {
                    var file = Request.Files["file-image"];
                    if (file.ContentLength > 0)
                    {
                        string image_name = "service_" + Helpers.Common.ChuyenThanhKhongDau(Regex.Replace(Service.Name, @"\s+", "_")) + "." + file.FileName.Split('.').Last();
                        bool isExists = System.IO.Directory.Exists(path);
                        if (!isExists)
                            System.IO.Directory.CreateDirectory(path);
                        file.SaveAs(path + image_name);
                        Service.Image_Name = image_name;
                    }
                }
                ServiceRepository.InsertService(Service);
                //tạo đặc tính động cho sản phẩm nếu có danh sách đặc tính động truyền vào và đặc tính đó phải có giá trị
                ObjectAttributeController.CreateOrUpdateForObject(Service.Id, model.AttributeValueList);
                if (model.IsCombo == true)
                {
                    foreach (var item in model.DetailList)
                    {
                        var serviceCombo = new ServiceCombo();
                        serviceCombo.IsDeleted = false;
                        serviceCombo.CreatedUserId = WebSecurity.CurrentUserId;
                        serviceCombo.ModifiedUserId = WebSecurity.CurrentUserId;
                        serviceCombo.AssignedUserId = WebSecurity.CurrentUserId;
                        serviceCombo.CreatedDate = DateTime.Now;
                        serviceCombo.ModifiedDate = DateTime.Now;
                        serviceCombo.ComboId = Service.Id;
                        serviceCombo.Quantity = item.Quantity;
                        serviceCombo.ServiceId = item.ServiceId;
                        serviceComboRepository.InsertServiceCombo(serviceCombo);
                    }
                }
                else
                {
                    foreach (var item in model.ReminderList)
                    {
                        var reminder = new ServiceReminderGroup();
                        reminder.IsDeleted = false;
                        reminder.CreatedUserId = WebSecurity.CurrentUserId;
                        reminder.ModifiedUserId = WebSecurity.CurrentUserId;
                        reminder.AssignedUserId = WebSecurity.CurrentUserId;
                        reminder.CreatedDate = DateTime.Now;
                        reminder.ModifiedDate = DateTime.Now;
                        reminder.ServiceId = Service.Id;
                        reminder.ServiceReminderId = item.ServiceReminderId;
                        serviceReminderGroupRepository.InsertServiceReminderGroup(reminder);
                    }
                }
                if (string.IsNullOrEmpty(Request["IsPopup"]) == false)
                {
                    return RedirectToAction("Edit", new { Id = model.Id, IsPopup = Request["IsPopup"] });
                }
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region CheckCodeExsist
        public ActionResult CheckCodeExsist(int? id, string code)
        {
            code = code.Trim();
            var product = ServiceRepository.GetAllvwService()
                .Where(item => item.Code == code).FirstOrDefault();
            if (product != null)
            {
                if (id == null || (id != null && product.Id != id))
                    return Content("Trùng mã dịch vụ!");
                else
                {
                    return Content("");
                }
            }
            else
            {
                return Content("");
            }
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            string image_folder = Helpers.Common.GetSetting("service-image-folder");
            var Service = ServiceRepository.GetvwServiceById(Id.Value);
            if (Service != null && Service.IsDeleted != true)
            {
                var model = new ServiceViewModel();
                AutoMapper.Mapper.Map(Service, model);
                if (string.IsNullOrEmpty(model.Image_Name))
                {
                    model.Image_Name = "/assets/css/images/noimage.gif";
                }
                else
                {
                    model.Image_Name = image_folder + Service.Image_Name;
                }
                if (model.IsCombo == true)
                {
                    model.DetailList = serviceComboRepository.GetAllvwServiceCombo().Where(x => x.ComboId.Value == model.Id).Select(x => new ServiceComboViewModel
                    {
                        Id = x.Id,
                        ComboId = x.ComboId,
                        Quantity = x.Quantity,
                        ServiceId = x.ServiceId,
                        Name = x.Name,
                        Code = x.Code
                    }).ToList();
                    //model.DetailList.RemoveAll(x => x.Id == 0);
                    int n = 0;
                    foreach (var item in model.DetailList)
                    {
                        item.OrderNo = n;
                        n++;
                    }
                }
                else
                {
                    model.ReminderList = serviceReminderGroupRepository.GetAllvwServiceReminderGroup().Where(x => x.ServiceId.Value == model.Id).Select(x => new ServiceReminderGroupViewModel
                    {
                        Id = x.Id,
                        Name=x.Name,
                        QuantityDate=x.QuantityDate,
                        ServiceId=x.ServiceId,
                        ServiceReminderId=x.ServiceReminderId,
                        Reminder=x.Reminder
                    }).ToList();
                    //model.DetailList.RemoveAll(x => x.Id == 0);
                    int n = 0;
                    foreach (var item in model.ReminderList)
                    {
                        item.OrderNo = n;
                        n++;
                    }
                }
                //if (model.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                //{
                //    TempData["FailedMessage"] = "NotOwner";
                //    return RedirectToAction("Index");
                //}                


                var serviceList = ServiceRepository.GetAllvwService()
                   .Select(item => new ServiceViewModel
                   {
                       Code = item.Code,
                       Barcode = item.Barcode,
                       Name = item.Name,
                       Id = item.Id,
                       CategoryCode = item.CategoryCode,
                       PriceOutbound = item.PriceOutbound,
                       Unit = item.Unit,
                       Image_Name = image_folder + item.Image_Name
                   }).ToList();
                ViewBag.serviceList = serviceList;
                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ServiceViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var Service = ServiceRepository.GetProductById(model.Id);
                    AutoMapper.Mapper.Map(model, Service);
                    Service.ModifiedUserId = WebSecurity.CurrentUserId;
                    Service.ModifiedDate = DateTime.Now;
                    Service.Type = "service";
                    var path = Helpers.Common.GetSetting("service-image-folder");
                    var filepath = System.Web.HttpContext.Current.Server.MapPath("~" + path);
                    if (Request.Files["file-image"] != null)
                    {
                        var file = Request.Files["file-image"];
                        if (file.ContentLength > 0)
                        {
                            FileInfo fi = new FileInfo(Server.MapPath("~" + path) + Service.Image_Name);
                            if (fi.Exists)
                            {
                                fi.Delete();
                            }

                            string image_name = "service_" + Helpers.Common.ChuyenThanhKhongDau(Regex.Replace(Service.Name, @"\s+", "_")) + "." + file.FileName.Split('.').Last();
                            bool isExists = System.IO.Directory.Exists(filepath);
                            if (!isExists)
                                System.IO.Directory.CreateDirectory(filepath);
                            file.SaveAs(filepath + image_name);
                            Service.Image_Name = image_name;
                        }
                    }
                    if (model.IsCombo == true)
                    {
                        var q = serviceComboRepository.GetAllServiceCombo().Where(x => x.ComboId == Service.Id).ToList();
                        ServiceRepository.DeleteServiceCombo(q);
                        if (model.DetailList != null)
                        {
                            foreach (var item in model.DetailList)
                            {
                                var serviceCombo = new ServiceCombo();
                                serviceCombo.IsDeleted = false;
                                serviceCombo.CreatedUserId = WebSecurity.CurrentUserId;
                                serviceCombo.ModifiedUserId = WebSecurity.CurrentUserId;
                                serviceCombo.AssignedUserId = WebSecurity.CurrentUserId;
                                serviceCombo.CreatedDate = DateTime.Now;
                                serviceCombo.ModifiedDate = DateTime.Now;
                                serviceCombo.ComboId = Service.Id;
                                serviceCombo.Quantity = item.Quantity;
                                serviceCombo.ServiceId = item.ServiceId;
                                serviceComboRepository.InsertServiceCombo(serviceCombo);
                            }
                        }
                    }
                    else
                    {
                        var q = serviceReminderGroupRepository.GetAllServiceReminderGroup().Where(x => x.ServiceId == Service.Id).ToList();
                        serviceReminderGroupRepository.DeleteServiceReminderGroupList(q);

                        if (model.ReminderList != null)
                        {
                            foreach (var item in model.ReminderList)
                            {
                                var reminder = new ServiceReminderGroup();
                                reminder.IsDeleted = false;
                                reminder.CreatedUserId = WebSecurity.CurrentUserId;
                                reminder.ModifiedUserId = WebSecurity.CurrentUserId;
                                reminder.AssignedUserId = WebSecurity.CurrentUserId;
                                reminder.CreatedDate = DateTime.Now;
                                reminder.ModifiedDate = DateTime.Now;
                                reminder.ServiceId = Service.Id;
                                reminder.ServiceReminderId = item.ServiceReminderId;
                                serviceReminderGroupRepository.InsertServiceReminderGroup(reminder);
                            }
                        }
                    }
                    //tạo hoặc cập nhật đặc tính động cho sản phẩm nếu có danh sách đặc tính động truyền vào và đặc tính đó phải có giá trị
                    ObjectAttributeController.CreateOrUpdateForObject(Service.Id, model.AttributeValueList);

                    ServiceRepository.UpdateService(Service);

                    if (string.IsNullOrEmpty(Request["IsPopup"]) == false)
                    {
                        return RedirectToAction("Edit", new { Id = model.Id, IsPopup = Request["IsPopup"] });
                    }
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
                    var item = ServiceRepository.GetProductById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        //if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        //{
                        //    TempData["FailedMessage"] = "NotOwner";
                        //    return RedirectToAction("Index");
                        //}

                        item.IsDeleted = true;
                        ServiceRepository.UpdateService(item);
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

        #region UsingServiceLog
        public ViewResult UsingServiceLog(int? UsingServiceId)
        {
            var usingServiceLogDetail = usingServiceLogDetailRepository.GetAllvwUsingServiceLogDetail().Where(x => x.UsingServiceId.Value == UsingServiceId).ToList();
            List<UsingServiceLogDetailViewModel> model = usingServiceLogDetail.Select(i => new UsingServiceLogDetailViewModel
            {
                    Id=i.Id,
                    Code=i.Code,
                    Name=i.Name,
                    StaffId=i.StaffId,
                    UsingServiceId=i.UsingServiceId,
                    CreatedDate=i.CreatedDate,
                    CreatedUserId=i.CreatedUserId,
                    Status=i.Status,
                    Type=i.Type,
                    CustomerId=i.CustomerId,
                    ProductInvoiceId=i.ProductInvoiceId,
                    ServiceName=i.ServiceName,
                    IsVote=i.IsVote
            }).OrderByDescending(x=>x.CreatedDate).ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult Update(int StaffId, string check, string Status, string type)
        {
            string[] arrUsingServiceLogId = check.Split(',');
            for (int i = 0; i < arrUsingServiceLogId.Count(); i++)
            {
                if (arrUsingServiceLogId[i] != "")
                {
                    var log = new UsingServiceLogDetail();
                    log.StaffId = StaffId;
                    log.CreatedDate = DateTime.Now;
                    log.CreatedUserId = WebSecurity.CurrentUserId;
                    log.ModifiedDate = DateTime.Now;
                    log.ModifiedUserId = WebSecurity.CurrentUserId;
                    log.AssignedUserId = WebSecurity.CurrentUserId;
                    log.IsDeleted = false;
                    log.Status = Status;
                    log.Type = type;
                    log.UsingServiceId = int.Parse(arrUsingServiceLogId[i], CultureInfo.InvariantCulture);
                   
                    var usingLog = usingServiceLogRepository.GetUsingServiceLogById(int.Parse(arrUsingServiceLogId[i], CultureInfo.InvariantCulture));
                    if (type == "usedservice")
                    {
                        if (usingLog.QuantityUsed < usingLog.Quantity)
                        {
                            usingLog.QuantityUsed = usingLog.QuantityUsed + 1;
                            usingServiceLogRepository.UpdateUsingServiceLog(usingLog);
                            usingServiceLogDetailRepository.InsertUsingServiceLogDetail(log);
                        }
                        else
                        {
                            return Content("error");
                        }

                    }
                    else
                    {
                        usingServiceLogDetailRepository.InsertUsingServiceLogDetail(log);
                    }
                    
                }
            }
            return Content("success");
        }
        #endregion

        #region UsingService
        public ActionResult UsingService(int? InvoiceId, bool? IsNullLayOut)
        {
            IEnumerable<UsingServiceLogViewModel> usingservice = usingServiceLogRepository.GetAllvwUsingServiceLog().Where(x => x.ProductInvoiceId == InvoiceId)
                .Select(x => new UsingServiceLogViewModel
                {
                    CategoryCode = x.CategoryCode,
                    CustomerId = x.CustomerId,
                    Id = x.Id,
                    IsCombo = x.IsCombo,
                    ItemCategory = x.ItemCategory,
                    ItemCode = x.ItemCode,
                    ItemName = x.ItemName,
                    ProductCode = x.ProductCode,
                    ProductInvoiceCode = x.ProductInvoiceCode,
                    ProductInvoiceDate = x.ProductInvoiceDate,
                    ProductInvoiceId = x.ProductInvoiceId,
                    Quantity = x.Quantity,
                    QuantityUsed = x.QuantityUsed,
                    ProductName = x.ProductName,
                    ServiceComboId = x.ServiceComboId,
                    ServiceId = x.ServiceId,
                    ServiceInvoiceDetailId = x.ServiceInvoiceDetailId
                }).ToList();
            ViewBag.IsNullLayOut = IsNullLayOut;
            return View(usingservice);
        }
        #endregion

        #region UpdateNote
        //[HttpPost]
        public ActionResult UpdateUsingServiceLogDetail(int? Id, string status)
        {
            var usingservice = usingServiceLogDetailRepository.GetUsingServiceLogDetailById(Id.Value);
            if (usingservice != null)
            {
                usingservice.Status = status;
                usingservice.ModifiedDate = DateTime.Now;
                usingservice.ModifiedUserId = WebSecurity.CurrentUserId;
                usingServiceLogDetailRepository.UpdateUsingServiceLogDetail(usingservice);
                return Content("success");
            }
            else
            {
                return Content("error");
            }
        }
        #endregion
    }
}
