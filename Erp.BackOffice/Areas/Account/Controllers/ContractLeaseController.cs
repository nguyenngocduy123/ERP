using System.Globalization;
using Erp.BackOffice.Account.Models;
using Erp.BackOffice.Filters;
using Erp.BackOffice.Helpers;
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

namespace Erp.BackOffice.Account.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class ContractLeaseController : Controller
    {
        private readonly IContractLeaseRepository ContractLeaseRepository;
        private readonly IUserRepository userRepository;

        public ContractLeaseController(
            IContractLeaseRepository _ContractLease
            , IUserRepository _user
            )
        {
            ContractLeaseRepository = _ContractLease;
            userRepository = _user;
        }

        #region Index

        public ViewResult Index(string txtSearch)
        {

            IQueryable<ContractLeaseViewModel> q = ContractLeaseRepository.GetAllvwContractLease()
                .Select(item => new ContractLeaseViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    CondosId=item.CondosId,
                    DayHandOver=item.DayHandOver,
                    DayPay=item.DayPay,
                    EffectiveDate=item.EffectiveDate,
                    ExpiryDate=item.ExpiryDate,
                    Price=item.Price,
                    Quantity=item.Quantity,
                    Unit=item.Unit,
                    UnitMoney=item.UnitMoney,
                    NameCondos=item.NameCondos,
                    CodeCondos=item.CodeCondos
                }).OrderByDescending(m => m.ModifiedDate);

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        #endregion

        #region Create
        public ViewResult Create()
        {
            var model = new ContractLeaseViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ContractLeaseViewModel model)
        {
            if (ModelState.IsValid)
            {
                var ContractLease = new ContractLease();
                AutoMapper.Mapper.Map(model, ContractLease);
                ContractLease.IsDeleted = false;
                ContractLease.CreatedUserId = WebSecurity.CurrentUserId;
                ContractLease.ModifiedUserId = WebSecurity.CurrentUserId;
                ContractLease.AssignedUserId = WebSecurity.CurrentUserId;
                ContractLease.CreatedDate = DateTime.Now;
                ContractLease.ModifiedDate = DateTime.Now;
                ContractLeaseRepository.InsertContractLease(ContractLease);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var ContractLease = ContractLeaseRepository.GetContractLeaseById(Id.Value);
            if (ContractLease != null && ContractLease.IsDeleted != true)
            {
                var model = new ContractLeaseViewModel();
                AutoMapper.Mapper.Map(ContractLease, model);
                
                if (model.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                {
                    TempData["FailedMessage"] = "NotOwner";
                    return RedirectToAction("Index");
                }                

                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(ContractLeaseViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var ContractLease = ContractLeaseRepository.GetContractLeaseById(model.Id);
                    AutoMapper.Mapper.Map(model, ContractLease);
                    ContractLease.ModifiedUserId = WebSecurity.CurrentUserId;
                    ContractLease.ModifiedDate = DateTime.Now;
                    ContractLeaseRepository.UpdateContractLease(ContractLease);

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
            var ContractLease = ContractLeaseRepository.GetContractLeaseById(Id.Value);
            if (ContractLease != null && ContractLease.IsDeleted != true)
            {
                var model = new ContractLeaseViewModel();
                AutoMapper.Mapper.Map(ContractLease, model);
                
                if (model.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                {
                    TempData["FailedMessage"] = "NotOwner";
                    return RedirectToAction("Index");
                }                

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
                    var item = ContractLeaseRepository.GetContractLeaseById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if(item != null)
                    {
                        if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                        {
                            TempData["FailedMessage"] = "NotOwner";
                            return RedirectToAction("Index");
                        }

                        item.IsDeleted = true;
                        ContractLeaseRepository.UpdateContractLease(item);
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
