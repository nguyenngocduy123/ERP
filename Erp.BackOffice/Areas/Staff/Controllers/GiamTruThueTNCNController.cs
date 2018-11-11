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
    public class GiamTruThueTNCNController : Controller
    {
        private readonly IGiamTruThueTNCNRepository GiamTruThueTNCNRepository;
        private readonly IUserRepository userRepository;

        public GiamTruThueTNCNController(
            IGiamTruThueTNCNRepository _GiamTruThueTNCN
            , IUserRepository _user
            )
        {
            GiamTruThueTNCNRepository = _GiamTruThueTNCN;
            userRepository = _user;
        }

        #region Index

        public ViewResult Index(int? TaxIncomePersonDetailId, int? staffId, int? TaxId)
        {
            IEnumerable<GiamTruThueTNCNViewModel> q = GiamTruThueTNCNRepository.GetAllGiamTruThueTNCN().Where(n => n.TaxIncomePersonDetailId == TaxIncomePersonDetailId && n.StaffId == staffId && n.TaxId == TaxId)
                .Select(item => new GiamTruThueTNCNViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    Value = item.Value
                }).OrderByDescending(m => m.ModifiedDate).ToList();

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        #endregion

        #region Create
        public ViewResult Create()
        {
            var model = new GiamTruThueTNCNViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(GiamTruThueTNCNViewModel model)
        {
            if (ModelState.IsValid)
            {
                var GiamTruThueTNCN = new GiamTruThueTNCN();
                AutoMapper.Mapper.Map(model, GiamTruThueTNCN);
                GiamTruThueTNCN.IsDeleted = false;
                GiamTruThueTNCN.CreatedUserId = WebSecurity.CurrentUserId;
                GiamTruThueTNCN.ModifiedUserId = WebSecurity.CurrentUserId;
                GiamTruThueTNCN.AssignedUserId = WebSecurity.CurrentUserId;
                GiamTruThueTNCN.CreatedDate = DateTime.Now;
                GiamTruThueTNCN.ModifiedDate = DateTime.Now;
                GiamTruThueTNCNRepository.InsertGiamTruThueTNCN(GiamTruThueTNCN);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var GiamTruThueTNCN = GiamTruThueTNCNRepository.GetGiamTruThueTNCNById(Id.Value);
            if (GiamTruThueTNCN != null && GiamTruThueTNCN.IsDeleted != true)
            {
                var model = new GiamTruThueTNCNViewModel();
                AutoMapper.Mapper.Map(GiamTruThueTNCN, model);
                
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
        public ActionResult Edit(GiamTruThueTNCNViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var GiamTruThueTNCN = GiamTruThueTNCNRepository.GetGiamTruThueTNCNById(model.Id);
                    AutoMapper.Mapper.Map(model, GiamTruThueTNCN);
                    GiamTruThueTNCN.ModifiedUserId = WebSecurity.CurrentUserId;
                    GiamTruThueTNCN.ModifiedDate = DateTime.Now;
                    GiamTruThueTNCNRepository.UpdateGiamTruThueTNCN(GiamTruThueTNCN);

                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                    return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
                }

                return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
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
            var GiamTruThueTNCN = GiamTruThueTNCNRepository.GetGiamTruThueTNCNById(Id.Value);
            if (GiamTruThueTNCN != null && GiamTruThueTNCN.IsDeleted != true)
            {
                var model = new GiamTruThueTNCNViewModel();
                AutoMapper.Mapper.Map(GiamTruThueTNCN, model);
                
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
                    var item = GiamTruThueTNCNRepository.GetGiamTruThueTNCNById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if(item != null)
                    {
                        if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        {
                            TempData["FailedMessage"] = "NotOwner";
                            return RedirectToAction("Index");
                        }

                        item.IsDeleted = true;
                        GiamTruThueTNCNRepository.UpdateGiamTruThueTNCN(item);
                    }
                }
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.DeleteSuccess;
                return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
            }
            catch (DbUpdateException)
            {
                TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.RelationError;
                return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
            }
        }
        #endregion
    }
}
