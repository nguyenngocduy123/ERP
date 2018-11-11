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
    public class ProcessPaymentController : Controller
    {
        private readonly IProcessPaymentRepository ProcessPaymentRepository;
        private readonly IUserRepository userRepository;
        private readonly IContactRepository contractRepository;
        public ProcessPaymentController(
            IProcessPaymentRepository _ProcessPayment
            , IUserRepository _user
            ,IContactRepository contract
            )
        {
            ProcessPaymentRepository = _ProcessPayment;
            userRepository = _user;
            contractRepository = contract;
        }

        #region Index

        public ViewResult Index(int? Id)
        {

            IQueryable<ProcessPaymentViewModel> q = ProcessPaymentRepository.GetAllProcessPayment().Where(x=>x.ContractId==Id)
                .Select(item => new ProcessPaymentViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    Bank=item.Bank,
                    CodeTrading=item.CodeTrading,
                    ContractId=item.ContractId,
                    DayPayment=item.DayPayment,
                    FormPayment=item.FormPayment,
                    MoneyPayment=item.MoneyPayment,
                    OrderNo=item.OrderNo,
                    Payer=item.Payer,
                    Status=item.Status
                }).OrderBy(m => m.OrderNo);

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        #endregion

        #region Create
        public ViewResult Create(int? Id)
        {
            var model = new ProcessPaymentViewModel();
            model.ContractId = Id;
            var q = ProcessPaymentRepository.GetAllProcessPayment().Where(x => x.ContractId == Id).OrderByDescending(x => x.OrderNo);
            var priceCondos = contractRepository.GetvwLogContractbyId(Id.Value);
            int dem = 0;
            if (q != null && q.Count() > 0)
            {
                dem = q.FirstOrDefault().OrderNo.Value;
                ViewBag.Payment = q.Sum(x => x.MoneyPayment.Value);
            }
            ViewBag.Price = priceCondos.Price;
            model.DayPayment = DateTime.Now;
            model.OrderNo = dem + 1;
            model.Name = "Đợt " + model.OrderNo;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ProcessPaymentViewModel model)
        {
            var urlRefer = Request["UrlReferrer"];
            if (ModelState.IsValid)
            {
                var ProcessPayment = new ProcessPayment();
                AutoMapper.Mapper.Map(model, ProcessPayment);
                ProcessPayment.IsDeleted = false;
                ProcessPayment.CreatedUserId = WebSecurity.CurrentUserId;
                ProcessPayment.ModifiedUserId = WebSecurity.CurrentUserId;
                ProcessPayment.AssignedUserId = WebSecurity.CurrentUserId;
                ProcessPayment.CreatedDate = DateTime.Now;
                ProcessPayment.ModifiedDate = DateTime.Now;
                ProcessPayment.Status = "Chưa thanh toán";
                ProcessPaymentRepository.InsertProcessPayment(ProcessPayment);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                if (Request["IsPopup"] == "true")
                {
                    ViewBag.closePopup = "close and append to page parent";
                    ViewBag.urlRefer = urlRefer;
                    //  model.Id = InfoPartyA.Id;
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
            var ProcessPayment = ProcessPaymentRepository.GetProcessPaymentById(Id.Value);
            if (ProcessPayment != null && ProcessPayment.IsDeleted != true)
            {
                var model = new ProcessPaymentViewModel();
                AutoMapper.Mapper.Map(ProcessPayment, model);
                
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
        public ActionResult Edit(ProcessPaymentViewModel model)
        {
            var urlRefer = Request["UrlReferrer"];
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var ProcessPayment = ProcessPaymentRepository.GetProcessPaymentById(model.Id);
                    AutoMapper.Mapper.Map(model, ProcessPayment);
                    ProcessPayment.ModifiedUserId = WebSecurity.CurrentUserId;
                    ProcessPayment.ModifiedDate = DateTime.Now;
                    ProcessPaymentRepository.UpdateProcessPayment(ProcessPayment);

                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                    if (Request["IsPopup"] == "true")
                    {
                        ViewBag.closePopup = "close and append to page parent";
                        ViewBag.urlRefer = urlRefer;
                        //  model.Id = InfoPartyA.Id;
                        return View(model);
                    }
                    return Redirect(urlRefer);
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
            var ProcessPayment = ProcessPaymentRepository.GetProcessPaymentById(Id.Value);
            if (ProcessPayment != null && ProcessPayment.IsDeleted != true)
            {
                var model = new ProcessPaymentViewModel();
                AutoMapper.Mapper.Map(ProcessPayment, model);
                
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
                    var item = ProcessPaymentRepository.GetProcessPaymentById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if(item != null)
                    {
                        if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                        {
                            TempData["FailedMessage"] = "NotOwner";
                            return RedirectToAction("Index");
                        }

                        item.IsDeleted = true;
                        ProcessPaymentRepository.UpdateProcessPayment(item);
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
