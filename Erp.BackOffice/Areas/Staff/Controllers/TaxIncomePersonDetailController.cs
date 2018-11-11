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
    public class TaxIncomePersonDetailController : Controller
    {
        private readonly ITaxIncomePersonDetailRepository TaxIncomePersonDetailRepository;
        private readonly IUserRepository userRepository;
        private readonly IStaffsRepository staffsRepository;
        private readonly ITaxIncomePersonRepository taxIncomePersonRepository;

        public TaxIncomePersonDetailController(
            ITaxIncomePersonDetailRepository _TaxIncomePersonDetail
            , IUserRepository _user
            , IStaffsRepository _staff
            , ITaxIncomePersonRepository _tax
            )
        {
            TaxIncomePersonDetailRepository = _TaxIncomePersonDetail;
            userRepository = _user;
            staffsRepository = _staff;
            taxIncomePersonRepository = _tax;
        }

        #region Index

        public ViewResult Index(string txtSearch)
        {

            IQueryable<TaxIncomePersonDetailViewModel> q = TaxIncomePersonDetailRepository.GetAllTaxIncomePersonDetail()
                .Select(item => new TaxIncomePersonDetailViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    StaffId = item.StaffId,
                }).OrderByDescending(m => m.ModifiedDate);

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        #endregion

        #region Create
        //public ViewResult Create()
        //{
        //    var model = new TaxIncomePersonDetailViewModel();

        //    return View(model);
        //}


        public ActionResult Create(int? staffId, int? taxPersonId)
        {
            bool anyStaff = staffsRepository.GetAllStaffs().Any(n => n.Id == staffId);
            bool anyPerson = staffsRepository.GetAllStaffs().Any(n => n.Id == taxPersonId);
            if (!anyStaff || !anyPerson)
            {
                return Content("error");
            }

            var TaxIncomePersonDetail = new TaxIncomePersonDetail();
            TaxIncomePersonDetail.IsDeleted = false;
            TaxIncomePersonDetail.CreatedUserId = WebSecurity.CurrentUserId;
            TaxIncomePersonDetail.ModifiedUserId = WebSecurity.CurrentUserId;
            TaxIncomePersonDetail.AssignedUserId = WebSecurity.CurrentUserId;
            TaxIncomePersonDetail.CreatedDate = DateTime.Now;
            TaxIncomePersonDetail.ModifiedDate = DateTime.Now;
            TaxIncomePersonDetail.StaffId = staffId;
            TaxIncomePersonDetail.TaxIncomePersonId = taxPersonId;
            TaxIncomePersonDetailRepository.InsertTaxIncomePersonDetail(TaxIncomePersonDetail);

            return Content("ok");
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var TaxIncomePersonDetail = TaxIncomePersonDetailRepository.GetTaxIncomePersonDetailById(Id.Value);
            if (TaxIncomePersonDetail != null && TaxIncomePersonDetail.IsDeleted != true)
            {
                var model = new TaxIncomePersonDetailViewModel();
                AutoMapper.Mapper.Map(TaxIncomePersonDetail, model);

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
        public ActionResult Edit(TaxIncomePersonDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var TaxIncomePersonDetail = TaxIncomePersonDetailRepository.GetTaxIncomePersonDetailById(model.Id);
                    AutoMapper.Mapper.Map(model, TaxIncomePersonDetail);
                    TaxIncomePersonDetail.ModifiedUserId = WebSecurity.CurrentUserId;
                    TaxIncomePersonDetail.ModifiedDate = DateTime.Now;
                    TaxIncomePersonDetailRepository.UpdateTaxIncomePersonDetail(TaxIncomePersonDetail);

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
            var TaxIncomePersonDetail = TaxIncomePersonDetailRepository.GetTaxIncomePersonDetailById(Id.Value);
            if (TaxIncomePersonDetail != null && TaxIncomePersonDetail.IsDeleted != true)
            {
                var model = new TaxIncomePersonDetailViewModel();
                AutoMapper.Mapper.Map(TaxIncomePersonDetail, model);

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

        public ActionResult Delete(int? staffId, int? taxPersonId)
        {
            try
            {
                var item = TaxIncomePersonDetailRepository.GetAllTaxIncomePersonDetail().Where(n => n.StaffId == staffId && n.TaxIncomePersonId == taxPersonId).ToList();
                
                if (item != null)
                {
                    for (int i = 0; i < item.Count(); i++)
                    {
                        if (item[i].CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        {
                            //TempData["FailedMessage"] = "NotOwner";
                            return Content("no");
                        }
                        item[i].IsDeleted = true;
                        TaxIncomePersonDetailRepository.UpdateTaxIncomePersonDetail(item[i]);
                    }
                  
                }

                return Content("ok");
            }
            catch (DbUpdateException)
            {
                TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.RelationError;
                return Content("no");
            }
        }

        [HttpPost]
        public ActionResult Delete()
        {
            try
            {
                string idDeleteAll = Request["DeleteId-checkbox"];
                string[] arrDeleteId = idDeleteAll.Split(',');
                for (int i = 0; i < arrDeleteId.Count(); i++)
                {
                    var item = TaxIncomePersonDetailRepository.GetTaxIncomePersonDetailById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        {
                            TempData["FailedMessage"] = "NotOwner";
                            return RedirectToAction("Index");
                        }

                        item.IsDeleted = true;
                        TaxIncomePersonDetailRepository.UpdateTaxIncomePersonDetail(item);
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
