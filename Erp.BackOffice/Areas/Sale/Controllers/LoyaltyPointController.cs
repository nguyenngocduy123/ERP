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

namespace Erp.BackOffice.Sale.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class LoyaltyPointController : Controller
    {
        private readonly ILoyaltyPointRepository LoyaltyPointRepository;
        private readonly IUserRepository userRepository;

        public LoyaltyPointController(
            ILoyaltyPointRepository _LoyaltyPoint
            , IUserRepository _user
            )
        {
            LoyaltyPointRepository = _LoyaltyPoint;
            userRepository = _user;
        }

        #region Index

        public ViewResult Index(string txtSearch)
        {

            IQueryable<LoyaltyPointViewModel> q = LoyaltyPointRepository.GetAllLoyaltyPoint()
                .Select(item => new LoyaltyPointViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    MaxMoney=item.MaxMoney,
                    MinMoney=item.MinMoney,
                    PlusPoint=item.PlusPoint
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
            var model = new LoyaltyPointViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(LoyaltyPointViewModel model)
        {
            if (ModelState.IsValid)
            {
                var LoyaltyPoint = new LoyaltyPoint();
                AutoMapper.Mapper.Map(model, LoyaltyPoint);
                LoyaltyPoint.IsDeleted = false;
                LoyaltyPoint.CreatedUserId = WebSecurity.CurrentUserId;
                LoyaltyPoint.ModifiedUserId = WebSecurity.CurrentUserId;
                LoyaltyPoint.AssignedUserId = WebSecurity.CurrentUserId;
                LoyaltyPoint.CreatedDate = DateTime.Now;
                LoyaltyPoint.ModifiedDate = DateTime.Now;
                LoyaltyPointRepository.InsertLoyaltyPoint(LoyaltyPoint);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var LoyaltyPoint = LoyaltyPointRepository.GetLoyaltyPointById(Id.Value);
            if (LoyaltyPoint != null && LoyaltyPoint.IsDeleted != true)
            {
                var model = new LoyaltyPointViewModel();
                AutoMapper.Mapper.Map(LoyaltyPoint, model);
                
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
        public ActionResult Edit(LoyaltyPointViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var LoyaltyPoint = LoyaltyPointRepository.GetLoyaltyPointById(model.Id);
                    AutoMapper.Mapper.Map(model, LoyaltyPoint);
                    LoyaltyPoint.ModifiedUserId = WebSecurity.CurrentUserId;
                    LoyaltyPoint.ModifiedDate = DateTime.Now;
                    LoyaltyPointRepository.UpdateLoyaltyPoint(LoyaltyPoint);

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

        //#region Detail
        //public ActionResult Detail(int? Id)
        //{
        //    var LoyaltyPoint = LoyaltyPointRepository.GetLoyaltyPointById(Id.Value);
        //    if (LoyaltyPoint != null && LoyaltyPoint.IsDeleted != true)
        //    {
        //        var model = new LoyaltyPointViewModel();
        //        AutoMapper.Mapper.Map(LoyaltyPoint, model);
                
        //        if (model.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
        //        {
        //            TempData["FailedMessage"] = "NotOwner";
        //            return RedirectToAction("Index");
        //        }                

        //        return View(model);
        //    }
        //    if (Request.UrlReferrer != null)
        //        return Redirect(Request.UrlReferrer.AbsoluteUri);
        //    return RedirectToAction("Index");
        //}
        //#endregion

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
                    var item = LoyaltyPointRepository.GetLoyaltyPointById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if(item != null)
                    {
                        if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        {
                            TempData["FailedMessage"] = "NotOwner";
                            return RedirectToAction("Index");
                        }

                        item.IsDeleted = true;
                        LoyaltyPointRepository.UpdateLoyaltyPoint(item);
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
