using System.Globalization;
using Erp.BackOffice.Sale.Models;
using Erp.BackOffice.Filters;
using Erp.Domain.Entities;
using Erp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Erp.Utilities;
using WebMatrix.WebData;
using Erp.Domain.Sale.Interfaces;
using Erp.BackOffice.Helpers;
using Erp.Domain.Staff.Interfaces;
using Erp.Domain.Account.Interfaces;
using Erp.Domain.Sale.Entities;
using Erp.BackOffice.App_GlobalResources;
using Erp.BackOffice.Areas.Staff.Models;
using Erp.BackOffice.Staff.Models;
namespace Erp.BackOffice.Sale.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class CommisionCustomerController : Controller
    {
        private readonly ICommisionCustomerRepository commisionCustomerRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly IUserRepository userRepository;
        private readonly IProductOrServiceRepository productRepository;

        public CommisionCustomerController(
            ICommisionCustomerRepository _Commision_Customer
            , IUserRepository _user
            , ICustomerRepository _customer
            , IProductOrServiceRepository _Product
            )
        {
            commisionCustomerRepository = _Commision_Customer;
            userRepository = _user;
            customerRepository = _customer;
            productRepository = _Product;
        }

        //#region Create
        //public ViewResult Create(int CustomerId)
        //{
        //    CommisionCustomerDetailViewModel model = new CommisionCustomerDetailViewModel();
        //    var listCommision = commisionCustomerRepository.GetAllCommisionCustomer().Where(item => item.CustomerId == CustomerId).ToList();
        //    model.CustomerName = customerRepository.GetAllCustomer().Where(item => item.Id == CustomerId).Select(item => item.CompanyName).FirstOrDefault();
        //    model.CustomerId = CustomerId;
        //    model.DetailList = new List<CommisionCustomerViewModel>();

        //    var productList = productRepository.GetAllvwProductByType("product").Select(item => new
        //    {
        //        item.Id,
        //        item.Code,
        //        item.Name,
        //        item.PriceOutbound
        //    }).ToList();

        //    foreach (var item in productList)
        //    {
        //        var commisionCustomerViewModel = new CommisionCustomerViewModel();
        //        commisionCustomerViewModel.ProductId = item.Id;
        //        commisionCustomerViewModel.Name = item.Code + " - " + item.Name;
        //        commisionCustomerViewModel.Price = item.PriceOutbound.Value;
        //        commisionCustomerViewModel.IsMoney = false;
        //        var commision = listCommision.Where(i => i.ProductId == item.Id).FirstOrDefault();
        //        if (commision != null)
        //        {
        //            commisionCustomerViewModel.Id = commision.Id;
        //            commisionCustomerViewModel.CommissionValue = commision.CommissionValue;
        //            commisionCustomerViewModel.IsMoney = commision.IsMoney == null ? false : commision.IsMoney;
        //        }

        //        model.DetailList.Add(commisionCustomerViewModel);
        //    }

        //    ViewBag.SuccessMessage = TempData["SuccessMessage"];
        //    ViewBag.FailedMessage = TempData["FailedMessage"];
        //    ViewBag.AlertMessage = TempData["AlertMessage"];

        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult Create(CommisionCustomerDetailViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (Request["Submit"] == "Save")
        //        {
        //            foreach (var item in model.DetailList)
        //            {
        //                if (item.CommissionValue < 0)
        //                    item.CommissionValue = 0;
        //                else if ((item.IsMoney == null || item.IsMoney == false) && item.CommissionValue > 100)
        //                    item.CommissionValue = 100;
        //                else if (item.CommissionValue > item.Price)
        //                    item.CommissionValue = item.Price;

        //                if (item.Id > 0)
        //                {
        //                    var commision = commisionCustomerRepository.GetCommisionCustomerById(item.Id);
        //                    commision.ModifiedUserId = WebSecurity.CurrentUserId;
        //                    commision.ModifiedDate = DateTime.Now;
        //                    commision.CommissionValue = item.CommissionValue;
        //                    commision.IsMoney = item.IsMoney;
        //                    commisionCustomerRepository.UpdateCommisionCustomer(commision);
        //                }
        //                else
        //                {
        //                    if (item.CommissionValue > 0)
        //                    {
        //                        var commision = new CommisionCustomer();
        //                        commision.IsDeleted = false;
        //                        commision.CreatedUserId = WebSecurity.CurrentUserId;
        //                        commision.ModifiedUserId = WebSecurity.CurrentUserId;
        //                        commision.CreatedDate = DateTime.Now;
        //                        commision.ModifiedDate = DateTime.Now;
        //                        commision.CustomerId = model.CustomerId;
        //                        commision.ProductId = item.ProductId;
        //                        commision.CommissionValue = item.CommissionValue;
        //                        commision.IsMoney = item.IsMoney;
        //                        commisionCustomerRepository.InsertCommisionCustomer(commision);
        //                    }
        //                }
        //            }

        //            TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
        //            return RedirectToAction("Create", "CommisionCustomer", new { CustomerId = model.CustomerId });
        //        }

        //        return View(model);
        //    }

        //    return View(model);
        //}
        //#endregion

    }

}
