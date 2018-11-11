using System.Globalization;
using Erp.BackOffice.Staff.Models;
using Erp.BackOffice.Filters;
using Erp.Domain.Entities;
using Erp.Domain.Interfaces;
using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Erp.Utilities;
using WebMatrix.WebData;
using Erp.BackOffice.Helpers;

namespace Erp.BackOffice.Staff.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class TaxRateController : Controller
    {
        private readonly ITaxRateRepository TaxRateRepository;
        private readonly IUserRepository userRepository;

        public TaxRateController(
            ITaxRateRepository _TaxRate
            , IUserRepository _user
            )
        {
            TaxRateRepository = _TaxRate;
            userRepository = _user;
        }

        #region Index

        public ViewResult Index(string txtSearch)
        {
            IQueryable<TaxRateViewModel> q = TaxRateRepository.GetAllTaxRate()
                .Select(item => new TaxRateViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Level = item.Level,
                    FromValue = item.FromValue.ToString(),
                    ToValue = item.ToValue.ToString(),
                    TaxRateValue = item.TaxRateValue,
                    Name = item.Name,
                }).OrderBy(m => m.Level);

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        #endregion

        #region Create
        public ViewResult Create()
        {
           
            var model = new TaxRateViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(TaxRateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var TaxRate = new TaxRate();
                AutoMapper.Mapper.Map(model, TaxRate);
                TaxRate.IsDeleted = false;
                TaxRate.CreatedUserId = WebSecurity.CurrentUserId;
                TaxRate.ModifiedUserId = WebSecurity.CurrentUserId;
                TaxRate.AssignedUserId = WebSecurity.CurrentUserId;
                TaxRate.CreatedDate = DateTime.Now;
                TaxRate.ModifiedDate = DateTime.Now;

                //Name
                TaxRate.Name = string.Format("Từ {0} đến {1}", Helpers.Common.PhanCachHangNgan(model.FromValue), Helpers.Common.PhanCachHangNgan(model.ToValue));
                
                TaxRateRepository.InsertTaxRate(TaxRate);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var TaxRate = TaxRateRepository.GetTaxRateById(Id.Value);
            if (TaxRate != null && TaxRate.IsDeleted != true)
            {
                var model = new TaxRateViewModel();
                AutoMapper.Mapper.Map(TaxRate, model);
                
                if (model.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
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
        public ActionResult Edit(TaxRateViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var TaxRate = TaxRateRepository.GetTaxRateById(model.Id);
                    AutoMapper.Mapper.Map(model, TaxRate);
                    TaxRate.ModifiedUserId = WebSecurity.CurrentUserId;
                    TaxRate.ModifiedDate = DateTime.Now;
                    TaxRateRepository.UpdateTaxRate(TaxRate);

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
            var TaxRate = TaxRateRepository.GetTaxRateById(Id.Value);
            if (TaxRate != null && TaxRate.IsDeleted != true)
            {
                var model = new TaxRateViewModel();
                AutoMapper.Mapper.Map(TaxRate, model);
                
                if (model.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
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
                    var item = TaxRateRepository.GetTaxRateById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if(item != null)
                    {
                        if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        {
                            TempData["FailedMessage"] = "NotOwner";
                            return RedirectToAction("Index");
                        }

                        item.IsDeleted = true;
                        TaxRateRepository.UpdateTaxRate(item);
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
