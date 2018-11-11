using Erp.BackOffice.Account.Models;
using Erp.BackOffice.Filters;
using Erp.Domain.Account.Entities;
using Erp.Domain.Account.Interfaces;
using Erp.Domain.Interfaces;
using Erp.Utilities;
using System;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Erp.BackOffice.Account.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class ContractSellController : Controller
    {
        private readonly IContractSellRepository ContractSellRepository;
        private readonly IUserRepository userRepository;

        public ContractSellController(
            IContractSellRepository _ContractSell
            , IUserRepository _user
            )
        {
            ContractSellRepository = _ContractSell;
            userRepository = _user;
        }

        #region Index

        public ViewResult Index(string txtSearch)
        {

            IQueryable<ContractSellViewModel> q = ContractSellRepository.GetAllvwContractSell()
                .Select(item => new ContractSellViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                 NameCondos=item.NameCondos,
                 CodeCondos=item.CodeCondos,
                 CondosId=item.CondosId,
                 DayHandOver=item.DayHandOver,
                 DayPay=item.DayPay,
                 Price=item.Price,
                 Quantity=item.Quantity
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
            var model = new ContractSellViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ContractSellViewModel model)
        {
            if (ModelState.IsValid)
            {
                var ContractSell = new ContractSell();
                AutoMapper.Mapper.Map(model, ContractSell);
                ContractSell.IsDeleted = false;
                ContractSell.CreatedUserId = WebSecurity.CurrentUserId;
                ContractSell.ModifiedUserId = WebSecurity.CurrentUserId;
                ContractSell.AssignedUserId = WebSecurity.CurrentUserId;
                ContractSell.CreatedDate = DateTime.Now;
                ContractSell.ModifiedDate = DateTime.Now;
                ContractSellRepository.InsertContractSell(ContractSell);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var ContractSell = ContractSellRepository.GetContractSellById(Id.Value);
            if (ContractSell != null && ContractSell.IsDeleted != true)
            {
                var model = new ContractSellViewModel();
                AutoMapper.Mapper.Map(ContractSell, model);
                
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
        public ActionResult Edit(ContractSellViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var ContractSell = ContractSellRepository.GetContractSellById(model.Id);
                    AutoMapper.Mapper.Map(model, ContractSell);
                    ContractSell.ModifiedUserId = WebSecurity.CurrentUserId;
                    ContractSell.ModifiedDate = DateTime.Now;
                    ContractSellRepository.UpdateContractSell(ContractSell);

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
                    var item = ContractSellRepository.GetContractSellById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if(item != null)
                    {
                        if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                        {
                            TempData["FailedMessage"] = "NotOwner";
                            return RedirectToAction("Index");
                        }

                        item.IsDeleted = true;
                        ContractSellRepository.UpdateContractSell(item);
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
