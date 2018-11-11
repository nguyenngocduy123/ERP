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
using Erp.BackOffice.Areas.Cms.Models;
using System.Web.Script.Serialization;

namespace Erp.BackOffice.Sale.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class ServiceScheduleController : Controller
    {
        private readonly IServiceScheduleRepository ServiceScheduleRepository;
        private readonly IUserRepository userRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ServiceScheduleController(
            IServiceScheduleRepository _ServiceSchedule
            , IUserRepository _user
            ,ICategoryRepository category
            )
        {
            ServiceScheduleRepository = _ServiceSchedule;
            userRepository = _user;
            _categoryRepository = category;
        }
        #region Index
        public ActionResult Index()
        {
            var category = _categoryRepository.GetCategoryByCode("serviceSchedule_status").Select(x => new CategoryViewModel
            {
                Code = x.Code,
                Description = x.Description,
                Id = x.Id,
                Name = x.Name,
                Value = x.Value,
                OrderNo = x.OrderNo
            }).OrderBy(x => x.OrderNo).ToList();
            ViewBag.Category = category;
            return View();
        }
        #endregion

        #region Create
        public ViewResult Create(int? CustomerId, DateTime date)
        {
            var model = new ServiceScheduleViewModel();
            model.StartDate = date;
            model.DueDate = date.AddHours(1);
            //var branchId = Helpers.Common.CurrentUser.BranchId.Value;
            model.BranchId = null;
            model.CustomerId = CustomerId;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ServiceScheduleViewModel model)
        {
            var urlRefer = Request["UrlReferrer"];
            if (ModelState.IsValid)
            {
                var serviceSchedule = new ServiceSchedule();
                AutoMapper.Mapper.Map(model, serviceSchedule);
                serviceSchedule.IsDeleted = false;
                serviceSchedule.CreatedUserId = WebSecurity.CurrentUserId;
                serviceSchedule.ModifiedUserId = WebSecurity.CurrentUserId;
                serviceSchedule.AssignedUserId = WebSecurity.CurrentUserId;
                serviceSchedule.CreatedDate = DateTime.Now;
                serviceSchedule.ModifiedDate = DateTime.Now;
                serviceSchedule.Status = "pending";
                ServiceScheduleRepository.InsertServiceSchedule(serviceSchedule);
                string prefix = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_ServiceSchedule");
                serviceSchedule.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix, serviceSchedule.Id);
                ServiceScheduleRepository.UpdateServiceSchedule(serviceSchedule);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                {
                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                    ViewBag.closePopup = "true";
                    model.Id = serviceSchedule.Id;
                    ViewBag.urlRefer = urlRefer;
                    return View(model);
                }
                return Redirect(urlRefer);
            }
            return View(model);
        }
        #endregion

        #region Edit

        public ActionResult Edit(int? Id)
        {
            var serviceSchedule = ServiceScheduleRepository.GetvwServiceScheduleById(Id.Value);
            var customerImagePath = Helpers.Common.GetSetting("uploads_image_path_customer");
            var filepath = System.Web.HttpContext.Current.Server.MapPath("~" +customerImagePath);
            if (serviceSchedule != null && serviceSchedule.IsDeleted != true)
            {
                var model = new ServiceScheduleViewModel();
                AutoMapper.Mapper.Map(serviceSchedule, model);
                //kiem tra hinh co ton tai hay khong
                if (!string.IsNullOrEmpty(model.CustomerImage))
                {
                    model.CustomerImagePath = customerImagePath + model.CustomerImage;
                    if (!System.IO.File.Exists(filepath + model.CustomerImage))
                    {
                        model.CustomerImagePath = "/assets/img/no-avatar.png";
                    }
                    else
                    {
                        model.CustomerImagePath = customerImagePath + model.CustomerImage;
                    }
                }
                else
                    if (string.IsNullOrEmpty(model.CustomerImage))//Đã có hình
                    {
                        model.CustomerImagePath = "/assets/img/no-avatar.png";
                    }

                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(ServiceScheduleViewModel model)
        {
            var urlRefer = Request["UrlReferrer"];
            var serviceSchedule = ServiceScheduleRepository.GetServiceScheduleById(model.Id);
            AutoMapper.Mapper.Map(model, serviceSchedule);
            serviceSchedule.ModifiedUserId = WebSecurity.CurrentUserId;
            serviceSchedule.ModifiedDate = DateTime.Now;
            ServiceScheduleRepository.UpdateServiceSchedule(serviceSchedule);

            TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
            if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
            {
                ViewBag.closePopup = "true";
                model.Id = serviceSchedule.Id;
                ViewBag.urlRefer = urlRefer;
                return View(model);
            }
            return Redirect(urlRefer);
        }
        #endregion

        #region Detail
        public ActionResult Detail(int? Id)
        {
            var ServiceSchedule = ServiceScheduleRepository.GetvwServiceScheduleById(Id.Value);
            var customerImagePath = Helpers.Common.GetSetting("uploads_image_path_customer");
            var filepath = System.Web.HttpContext.Current.Server.MapPath("~" + customerImagePath);
            if (ServiceSchedule != null && ServiceSchedule.IsDeleted != true)
            {
                var model = new ServiceScheduleViewModel();
                AutoMapper.Mapper.Map(ServiceSchedule, model);
                //kiem tra hinh co ton tai hay khong
                if (!string.IsNullOrEmpty(model.CustomerImage))
                {
                    model.CustomerImagePath = customerImagePath + model.CustomerImage;
                    if (!System.IO.File.Exists(filepath + model.CustomerImage))
                    {
                        model.CustomerImagePath = "/assets/img/no-avatar.png";
                    }
                    else
                    {
                        model.CustomerImagePath = customerImagePath + model.CustomerImage;
                    }
                }
                else
                    if (string.IsNullOrEmpty(model.CustomerImage))//Đã có hình
                    {
                        model.CustomerImagePath = "/assets/img/no-avatar.png";
                    }
                //if (model.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                //{
                //    TempData["FailedMessage"] = "NotOwner";
                //    return RedirectToAction("Index");
                //}                

                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }
        #endregion

        //#region Delete
        //[HttpPost]
        //public ActionResult Delete()
        //{
        //    try
        //    {
        //        string idDeleteAll = Request["DeleteId-checkbox"];
        //        string[] arrDeleteId = idDeleteAll.Split(',');
        //        for (int i = 0; i < arrDeleteId.Count(); i++)
        //        {
        //            var item = ServiceScheduleRepository.GetServiceScheduleById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
        //            if(item != null)
        //            {
        //                if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
        //                {
        //                    TempData["FailedMessage"] = "NotOwner";
        //                    return RedirectToAction("Index");
        //                }

        //                item.IsDeleted = true;
        //                ServiceScheduleRepository.UpdateServiceSchedule(item);
        //            }
        //        }
        //        TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.DeleteSuccess;
        //        return RedirectToAction("Index");
        //    }
        //    catch (DbUpdateException)
        //    {
        //        TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.RelationError;
        //        return RedirectToAction("Index");
        //    }
        //}
        //#endregion

        #region Delete
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            try
            {
                var item = ServiceScheduleRepository.GetServiceScheduleById(id.Value);
                if (item != null)
                {
                    ServiceScheduleRepository.DeleteServiceSchedule(item.Id);
                }
                return Content("success");
            }
            catch (DbUpdateException)
            {
                return Content("error");
            }
        }
        #endregion

        #region Calendar
        public ViewResult Calendar(string status_check, int? branchId, int? month,int? year)
        {
            var user = userRepository.GetUserById(WebSecurity.CurrentUserId);
            List<string> ListCheck = new List<string>();
            if (!string.IsNullOrEmpty(status_check))
            {
                ListCheck = status_check.Split(',').ToList();
            }
            else
            {
                ListCheck = "pending,inprogress".Split(',').ToList();
            }
            List<ServiceScheduleViewModel> q = new List<ServiceScheduleViewModel>();
            IEnumerable<ServiceScheduleViewModel> model = ServiceScheduleRepository.GetAllvwServiceSchedule()
                .Select(i => new ServiceScheduleViewModel
                {
                        BranchId=i.BranchId,
                        BranchName=i.BranchName,
                        Code=i.Code,
                        Status=i.Status,
                        StartDate=i.StartDate,
                        DueDate=i.DueDate,
                        CustomerId=i.CustomerId,
                        ServiceId=i.ServiceId,
                        Id=i.Id,
                        CustomerCode=i.CustomerCode,
                        CustomerName=i.CustomerName,
                        Note=i.Note,
                        ServiceCode=i.ServiceCode,
                        ServiceName=i.ServiceName,
                        CustomerImage=i.CustomerImage
                }).OrderByDescending(x => x.StartDate).ToList();
            for (int i = 0; i < ListCheck.Count(); i++)
            {
                var a = model.Where(x => x.Status == ListCheck[i].ToString());
                q = q.Union(a).ToList();
            }
            //if (!string.IsNullOrEmpty(Code))
            //{
            //    q = q.Where(item => item.Code == Code).ToList();
            //}
            if (branchId != null && branchId.Value > 0)
            {
                q = q.Where(item => item.BranchId == branchId).ToList();
            }
            //if (CustomerId != null && CustomerId.Value > 0)
            //{
            //    q = q.Where(item => item.CustomerId == CustomerId).ToList();
            //}
            //if (ServiceId != null && ServiceId.Value > 0)
            //{
            //    q = q.Where(item => item.ServiceId == ServiceId).ToList();
            //}
            DateTime aDateTime = new DateTime(year.Value, month.Value, 1);
            // Cộng thêm 1 tháng và trừ đi một ngày.
            DateTime retDateTime = aDateTime.AddMonths(1).AddDays(-1);
            q = q.Where(x => aDateTime <= x.StartDate.Value && x.StartDate.Value <= retDateTime).OrderBy(x=>x.StartDate).ToList();
            var category = _categoryRepository.GetCategoryByCode("serviceSchedule_status").Select(x => new CategoryViewModel
            {
                Code = x.Code,
                Description = x.Description,
                Id = x.Id,
                Name = x.Name,
                Value = x.Value,
                OrderNo = x.OrderNo
            }).ToList();
            var aa = category.Where(id1 => ListCheck.Any(id2 => id2 == id1.Value)).ToList();
            ViewBag.Category = aa.OrderBy(x => x.OrderNo).ToList();
            //var customerImagePath = Helpers.Common.GetSetting("uploads_image_path_customer");
            //var filepath = System.Web.HttpContext.Current.Server.MapPath("~" + customerImagePath);
            foreach (var item in q)
            {
                item.CustomerImagePath = Erp.BackOffice.Helpers.Common.KiemTraTonTaiHinhAnh(item.CustomerImage, "uploads_image_path_customer","user");
            }
            var dataEvent = q.Select(e => new { 
                title = e.CustomerCode,
                start = e.StartDate.Value.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                end = e.DueDate.Value.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                allDay = false,
                className = (e.Status == "pending" ? "label-info" : (e.Status == "inprogress" ? "label-warning" : (e.Status == "completed" ? "label-success" : "label-danger"))),
                url = string.Format("/ServiceSchedule/{0}/?Id={1}", (e.Status == "completed" ? "Detail" : "Edit"), e.Id)
            }).ToList();

            ViewBag.dataEvent = new JavaScriptSerializer().Serialize(dataEvent);
            ViewBag.aDateTime = aDateTime.ToString("yyyy-MM-dd");
            return View(q);
        }
        #endregion

       
    }
}
