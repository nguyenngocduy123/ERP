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
    public class ThuNhapChiuThueController : Controller
    {
        private readonly IThuNhapChiuThueRepository ThuNhapChiuThueRepository;
        private readonly IUserRepository userRepository;
        private readonly ITaxIncomePersonDetailRepository taxIncomePersonDetailRepository;
        private readonly IStaffsRepository staffsRepository;

        public ThuNhapChiuThueController(
            IThuNhapChiuThueRepository _ThuNhapChiuThue
            , IUserRepository _user
            , ITaxIncomePersonDetailRepository _taxpd
            , IStaffsRepository _staffs
            )
        {
            ThuNhapChiuThueRepository = _ThuNhapChiuThue;
            userRepository = _user;
            taxIncomePersonDetailRepository = _taxpd;
            staffsRepository = _staffs;
        }

        #region Index

        public ViewResult Index(int? TaxIncomePersonDetailId, int? staffId , int? TaxId)
        {
            IEnumerable<ThuNhapChiuThueViewModel> q = ThuNhapChiuThueRepository.GetAllThuNhapChiuThue().Where(n => n.TaxIncomePersonDetailId == TaxIncomePersonDetailId && n.StaffId == staffId && n.TaxId == TaxId)
                .Select(item => new ThuNhapChiuThueViewModel
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
            var model = new ThuNhapChiuThueViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ThuNhapChiuThueViewModel model)
        {
            if (ModelState.IsValid)
            {
                var ThuNhapChiuThue = new ThuNhapChiuThue();
                AutoMapper.Mapper.Map(model, ThuNhapChiuThue);
                ThuNhapChiuThue.IsDeleted = false;
                ThuNhapChiuThue.CreatedUserId = WebSecurity.CurrentUserId;
                ThuNhapChiuThue.ModifiedUserId = WebSecurity.CurrentUserId;
                ThuNhapChiuThue.AssignedUserId = WebSecurity.CurrentUserId;
                ThuNhapChiuThue.CreatedDate = DateTime.Now;
                ThuNhapChiuThue.ModifiedDate = DateTime.Now;
                ThuNhapChiuThueRepository.InsertThuNhapChiuThue(ThuNhapChiuThue);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var ThuNhapChiuThue = ThuNhapChiuThueRepository.GetThuNhapChiuThueById(Id.Value);
            if (ThuNhapChiuThue != null && ThuNhapChiuThue.IsDeleted != true)
            {
                var model = new ThuNhapChiuThueViewModel();
                AutoMapper.Mapper.Map(ThuNhapChiuThue, model);
                
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
        public ActionResult Edit(ThuNhapChiuThueViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var ThuNhapChiuThue = ThuNhapChiuThueRepository.GetThuNhapChiuThueById(model.Id);
                    AutoMapper.Mapper.Map(model, ThuNhapChiuThue);
                    ThuNhapChiuThue.ModifiedUserId = WebSecurity.CurrentUserId;
                    ThuNhapChiuThue.ModifiedDate = DateTime.Now;
                    ThuNhapChiuThueRepository.UpdateThuNhapChiuThue(ThuNhapChiuThue);

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
            var ThuNhapChiuThue = ThuNhapChiuThueRepository.GetThuNhapChiuThueById(Id.Value);
            if (ThuNhapChiuThue != null && ThuNhapChiuThue.IsDeleted != true)
            {
                var model = new ThuNhapChiuThueViewModel();
                AutoMapper.Mapper.Map(ThuNhapChiuThue, model);
                
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
                    var item = ThuNhapChiuThueRepository.GetThuNhapChiuThueById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if(item != null)
                    {
                        if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        {
                            TempData["FailedMessage"] = "NotOwner";
                            return RedirectToAction("Index");
                        }

                        item.IsDeleted = true;
                        ThuNhapChiuThueRepository.UpdateThuNhapChiuThue(item);
                    }
                }
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.DeleteSuccess;
                return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
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
