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

namespace Erp.BackOffice.Account.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class CustomerDiscountController : Controller
    {
        private readonly ICustomerDiscountRepository CustomerDiscountRepository;
        private readonly IUserRepository userRepository;
        private readonly IQueryHelper QueryHelper;

        public CustomerDiscountController(
            ICustomerDiscountRepository _CustomerDiscount
            , IUserRepository _user
            , IQueryHelper _QueryHelper
            )
        {
            CustomerDiscountRepository = _CustomerDiscount;
            userRepository = _user;
            QueryHelper = _QueryHelper;
        }

        #region Index

        public ViewResult Index(string txtSearch)
        {

            IQueryable<CustomerDiscountViewModel> q = CustomerDiscountRepository.GetAllCustomerDiscount()
                .Select(item => new CustomerDiscountViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                }).OrderByDescending(m => m.ModifiedDate);

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }

        public ActionResult ListByCustomer(int? CustomerId) 
        {
            IQueryable<CustomerDiscountViewModel> q = CustomerDiscountRepository.GetAllCustomerDiscount().Where(x => x.CustomerId == CustomerId)
                .Select(item => new CustomerDiscountViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    Code = item.Code,
                    CustomerId = item.CustomerId,
                    EndDate = item.EndDate,
                    IsActive = item.IsActive,
                    Note = item.Note,
                    StartDate = item.StartDate,
                    ValuePercent = item.ValuePercent

                }).OrderByDescending(m => m.ModifiedDate);

            ViewBag.CustomerId = CustomerId;
            return View(q);
        }
        #endregion

        #region Create
        public ViewResult Create()
        {
            var model = new CustomerDiscountViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CustomerDiscountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var CustomerDiscount = new CustomerDiscount();
                AutoMapper.Mapper.Map(model, CustomerDiscount);
                CustomerDiscount.IsDeleted = false;
                CustomerDiscount.IsActive = true;
                CustomerDiscount.CreatedUserId = WebSecurity.CurrentUserId;
                CustomerDiscount.ModifiedUserId = WebSecurity.CurrentUserId;
                CustomerDiscount.AssignedUserId = WebSecurity.CurrentUserId;
                CustomerDiscount.CreatedDate = DateTime.Now;
                CustomerDiscount.ModifiedDate = DateTime.Now;
                CustomerDiscountRepository.InsertCustomerDiscount(CustomerDiscount);

                if (Request["IsPopup"] == "true")
                {
                    ViewBag.closePopup = "close and append to page parent";
                    model.Name = model.Name;
                    model.Id = CustomerDiscount.Id;
                    return View(model);
                }

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var CustomerDiscount = CustomerDiscountRepository.GetCustomerDiscountById(Id.Value);
            if (CustomerDiscount != null && CustomerDiscount.IsDeleted != true)
            {
                var model = new CustomerDiscountViewModel();
                AutoMapper.Mapper.Map(CustomerDiscount, model);
                
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

        [HttpPost]
        public ActionResult Edit(CustomerDiscountViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var CustomerDiscount = CustomerDiscountRepository.GetCustomerDiscountById(model.Id);
                    AutoMapper.Mapper.Map(model, CustomerDiscount);
                    CustomerDiscount.ModifiedUserId = WebSecurity.CurrentUserId;
                    CustomerDiscount.ModifiedDate = DateTime.Now;
                    CustomerDiscountRepository.UpdateCustomerDiscount(CustomerDiscount);

                    if (string.IsNullOrEmpty(Request["IsPopup"]) == false)
                    {
                        return RedirectToAction("Edit", new { Id = model.Id, IsPopup = "true" });
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

        #region Detail
        public ActionResult Detail(int? Id)
        {
            var CustomerDiscount = CustomerDiscountRepository.GetCustomerDiscountById(Id.Value);
            if (CustomerDiscount != null && CustomerDiscount.IsDeleted != true)
            {
                var model = new CustomerDiscountViewModel();
                AutoMapper.Mapper.Map(CustomerDiscount, model);
                
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
                    var item = CustomerDiscountRepository.GetCustomerDiscountById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if(item != null)
                    {
                        //if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        //{
                        //    TempData["FailedMessage"] = "NotOwner";
                        //    return RedirectToAction("Index");
                        //}

                        item.IsDeleted = true;
                        CustomerDiscountRepository.UpdateCustomerDiscount(item);
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


        public ActionResult Delete(int id, string IsAjax)
        {
            try
            {
                int customerId = 0;

                var item = CustomerDiscountRepository.GetCustomerDiscountById(id);

                if (item != null)
                {
                    customerId = item.CustomerId.Value;

                    //if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                    //{
                    //    TempData["FailedMessage"] = "NotOwner";
                    //    return Content("NotOwner");
                    //}

                    item.IsDeleted = true;
                    CustomerDiscountRepository.UpdateCustomerDiscount(item);
                }


                if (string.IsNullOrEmpty(IsAjax) == false)
                {
                    return Content("success");
                }

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.DeleteSuccess;
                return RedirectToAction("Edit", new { Id = customerId });
            }
            catch (DbUpdateException)
            {
                TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.RelationError;
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region JSON
        public JsonResult EditInline(int? Id, string fieldName, string value)
        {
            Dictionary<string, object> field_value = new Dictionary<string, object>();
            field_value.Add(fieldName, value);
            field_value.Add("ModifiedDate", DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"));
            field_value.Add("ModifiedUserId", WebSecurity.CurrentUserId);

            var flag = QueryHelper.UpdateFields("Sale_CustomerDiscount", field_value, Id.Value);
            if (flag == true)
                return Json(new { status = "success", id = Id, fieldName = fieldName, value = value }, JsonRequestBehavior.AllowGet);

            return Json(new { status = "error", id = Id, fieldName = fieldName, value = value }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDiscountLast(int? customerId)
        {
            if (customerId == null)
                return Json(new CustomerDiscountViewModel(), JsonRequestBehavior.AllowGet);

            var item = CustomerDiscountRepository.GetAllCustomerDiscount().Where(x => x.CustomerId == customerId && x.IsActive == true).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            if (item == null)
                return Json(new CustomerDiscountViewModel(), JsonRequestBehavior.AllowGet);

            return Json(item, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
