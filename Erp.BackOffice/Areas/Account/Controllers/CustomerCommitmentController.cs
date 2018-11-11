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
    public class CustomerCommitmentController : Controller
    {
        private readonly ICustomerCommitmentRepository CustomerCommitmentRepository;
        private readonly IUserRepository userRepository;

        public CustomerCommitmentController(
            ICustomerCommitmentRepository _CustomerCommitment
            , IUserRepository _user
            )
        {
            CustomerCommitmentRepository = _CustomerCommitment;
            userRepository = _user;
        }

        #region Index

        public ViewResult Index(int? customerId)
        {
            IQueryable<CustomerCommitmentViewModel> q = CustomerCommitmentRepository.GetAllCustomerCommitment()
                .Select(item => new CustomerCommitmentViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    Description = item.Description,
                    Date = item.Date
                }).OrderByDescending(m => m.ModifiedDate);

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];

            ViewBag.CustomerId = customerId;

            if (customerId != null)
                return View("ListByCustomer", q);

            return View(q);
        }
        #endregion

        #region Create
        public ViewResult Create(int? customerId)
        {
            var model = new CustomerCommitmentViewModel();
            model.CustomerId = customerId;
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(CustomerCommitmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var CustomerCommitment = new CustomerCommitment();
                AutoMapper.Mapper.Map(model, CustomerCommitment);
                CustomerCommitment.IsDeleted = false;
                CustomerCommitment.CreatedUserId = WebSecurity.CurrentUserId;
                CustomerCommitment.ModifiedUserId = WebSecurity.CurrentUserId;
                CustomerCommitment.AssignedUserId = WebSecurity.CurrentUserId;
                CustomerCommitment.CreatedDate = DateTime.Now;
                CustomerCommitment.ModifiedDate = DateTime.Now;
                CustomerCommitmentRepository.InsertCustomerCommitment(CustomerCommitment);

                if (Request["IsPopup"] == "true")
                {
                    ViewBag.closePopup = "close and append to page parent";
                    model.Name = model.Name;
                    model.Id = CustomerCommitment.Id;
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
            var CustomerCommitment = CustomerCommitmentRepository.GetCustomerCommitmentById(Id.Value);
            if (CustomerCommitment != null && CustomerCommitment.IsDeleted != true)
            {
                var model = new CustomerCommitmentViewModel();
                AutoMapper.Mapper.Map(CustomerCommitment, model);
                
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
        [ValidateInput(false)]
        public ActionResult Edit(CustomerCommitmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var CustomerCommitment = CustomerCommitmentRepository.GetCustomerCommitmentById(model.Id);
                    AutoMapper.Mapper.Map(model, CustomerCommitment);
                    CustomerCommitment.ModifiedUserId = WebSecurity.CurrentUserId;
                    CustomerCommitment.ModifiedDate = DateTime.Now;
                    CustomerCommitmentRepository.UpdateCustomerCommitment(CustomerCommitment);

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
            var CustomerCommitment = CustomerCommitmentRepository.GetCustomerCommitmentById(Id.Value);
            if (CustomerCommitment != null && CustomerCommitment.IsDeleted != true)
            {
                var model = new CustomerCommitmentViewModel();
                AutoMapper.Mapper.Map(CustomerCommitment, model);
                
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
                    var item = CustomerCommitmentRepository.GetCustomerCommitmentById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if(item != null)
                    {
                        //if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        //{
                        //    TempData["FailedMessage"] = "NotOwner";
                        //    return RedirectToAction("Index");
                        //}

                        item.IsDeleted = true;
                        CustomerCommitmentRepository.UpdateCustomerCommitment(item);
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
    }
}
