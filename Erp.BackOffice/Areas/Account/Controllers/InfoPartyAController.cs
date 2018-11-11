using Erp.BackOffice.Account.Models;
using Erp.BackOffice.Filters;
using Erp.Domain.Account.Entities;
using Erp.Domain.Account.Interfaces;
using Erp.Domain.Interfaces;
using Erp.Utilities;
using System;
using System.Collections.Generic;
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
    public class InfoPartyAController : Controller
    {
        private readonly IInfoPartyARepository InfoPartyARepository;
        private readonly IUserRepository userRepository;

        public InfoPartyAController(
            IInfoPartyARepository _InfoPartyA
            , IUserRepository _user
            )
        {
            InfoPartyARepository = _InfoPartyA;
            userRepository = _user;
        }

        #region Index

        public ViewResult Index(string Name, string CompanyName, string TaxCode)
        {

            IEnumerable<InfoPartyAViewModel> q = InfoPartyARepository.GetAllInfoPartyA()
                .Select(item => new InfoPartyAViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    AccountNumber = item.AccountNumber,
                    Address = item.Address,
                    Bank = item.Bank,
                    Birthday = item.Birthday,
                    Fax = item.Fax,
                    NamePrefix = item.NamePrefix,
                    IdCardDate = item.IdCardDate,
                    IdCardIssued = item.IdCardIssued,
                    IdCardNumber = item.IdCardNumber,
                    Phone = item.Phone,
                    Position = item.Position,
                    TaxCode = item.TaxCode,
                    CompanyName = item.CompanyName
                }).OrderByDescending(m => m.ModifiedDate).ToList();
            if (!string.IsNullOrEmpty(Name))
            {
                q = q.Where(item => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.Name).ToLower().Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(Name).ToLower()));

            }
            if (!string.IsNullOrEmpty(CompanyName))
            {
                q = q.Where(item => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.CompanyName).ToLower().Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(CompanyName).ToLower()));

            }
            if (!string.IsNullOrEmpty(TaxCode))
            {
                q = q.Where(item => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.TaxCode).ToLower().Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(TaxCode).ToLower()));

            }
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        #endregion

        #region Create
        public ViewResult Create()
        {
            var model = new InfoPartyAViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(InfoPartyAViewModel model)
        {
            if (ModelState.IsValid)
            {
                var InfoPartyA = new InfoPartyA();
                AutoMapper.Mapper.Map(model, InfoPartyA);
                InfoPartyA.IsDeleted = false;
                InfoPartyA.CreatedUserId = WebSecurity.CurrentUserId;
                InfoPartyA.ModifiedUserId = WebSecurity.CurrentUserId;
                InfoPartyA.AssignedUserId = WebSecurity.CurrentUserId;
                InfoPartyA.CreatedDate = DateTime.Now;
                InfoPartyA.ModifiedDate = DateTime.Now;
                InfoPartyARepository.InsertInfoPartyA(InfoPartyA);

                if (Request["IsPopup"] == "true")
                {
                    ViewBag.closePopup = "close and append to page parent";
                    model.Id = InfoPartyA.Id;
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
            var InfoPartyA = InfoPartyARepository.GetInfoPartyAById(Id.Value);
            if (InfoPartyA != null && InfoPartyA.IsDeleted != true)
            {
                var model = new InfoPartyAViewModel();
                AutoMapper.Mapper.Map(InfoPartyA, model);

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
        public ActionResult Edit(InfoPartyAViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var InfoPartyA = InfoPartyARepository.GetInfoPartyAById(model.Id);
                    AutoMapper.Mapper.Map(model, InfoPartyA);
                    InfoPartyA.ModifiedUserId = WebSecurity.CurrentUserId;
                    InfoPartyA.ModifiedDate = DateTime.Now;
                    InfoPartyARepository.UpdateInfoPartyA(InfoPartyA);

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
                    var item = InfoPartyARepository.GetInfoPartyAById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                        {
                            TempData["FailedMessage"] = "NotOwner";
                            return RedirectToAction("Index");
                        }

                        item.IsDeleted = true;
                        InfoPartyARepository.UpdateInfoPartyA(item);
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

        #region Detail
        public ActionResult Detail(int? Id)
        {
            var infoPartyA = InfoPartyARepository.GetvwInfoPartyAById(Id.Value);

            if (infoPartyA != null && infoPartyA.IsDeleted != true)
            {
                var model = new InfoPartyAViewModel();
                AutoMapper.Mapper.Map(infoPartyA, model);
                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
