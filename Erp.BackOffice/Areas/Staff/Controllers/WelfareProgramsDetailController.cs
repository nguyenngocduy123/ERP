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
    public class WelfareProgramsDetailController : Controller
    {
        private readonly IWelfareProgramsDetailRepository WelfareProgramsDetailRepository;
        private readonly IUserRepository userRepository;

        public WelfareProgramsDetailController(
            IWelfareProgramsDetailRepository _WelfareProgramsDetail
            , IUserRepository _user
            )
        {
            WelfareProgramsDetailRepository = _WelfareProgramsDetail;
            userRepository = _user;
        }

        #region Index

        public ViewResult Index(string txtSearch)
        {

            IQueryable<WelfareProgramsDetailViewModel> q = WelfareProgramsDetailRepository.GetAllWelfareProgramsDetail()
                .Select(item => new WelfareProgramsDetailViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate
                }).OrderByDescending(m => m.ModifiedDate);

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        #endregion

        #region Create
        public ViewResult Create(int WelfareProgramsId)
        {
            var model = new WelfareProgramsDetailViewModel();
            model.WelfareProgramsId = WelfareProgramsId;
            model.RegistrationDate = DateTime.Now;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(WelfareProgramsDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var WelfareProgramsDetail = new WelfareProgramsDetail();
                AutoMapper.Mapper.Map(model, WelfareProgramsDetail);
                WelfareProgramsDetail.IsDeleted = false;
                WelfareProgramsDetail.CreatedUserId = WebSecurity.CurrentUserId;
                WelfareProgramsDetail.ModifiedUserId = WebSecurity.CurrentUserId;
                WelfareProgramsDetail.AssignedUserId = WebSecurity.CurrentUserId;
                WelfareProgramsDetail.CreatedDate = DateTime.Now;
                WelfareProgramsDetail.ModifiedDate = DateTime.Now;
                WelfareProgramsDetailRepository.InsertWelfareProgramsDetail(WelfareProgramsDetail);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Detail","WelfarePrograms",new { area="Staff",Id= model.WelfareProgramsId});
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var WelfareProgramsDetail = WelfareProgramsDetailRepository.GetWelfareProgramsDetailById(Id.Value);
            if (WelfareProgramsDetail != null && WelfareProgramsDetail.IsDeleted != true)
            {
                var model = new WelfareProgramsDetailViewModel();
                AutoMapper.Mapper.Map(WelfareProgramsDetail, model);
                
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
        public ActionResult Edit(WelfareProgramsDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var WelfareProgramsDetail = WelfareProgramsDetailRepository.GetWelfareProgramsDetailById(model.Id);
                    AutoMapper.Mapper.Map(model, WelfareProgramsDetail);
                    WelfareProgramsDetail.ModifiedUserId = WebSecurity.CurrentUserId;
                    WelfareProgramsDetail.ModifiedDate = DateTime.Now;
                    WelfareProgramsDetailRepository.UpdateWelfareProgramsDetail(WelfareProgramsDetail);

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
            var WelfareProgramsDetail = WelfareProgramsDetailRepository.GetWelfareProgramsDetailById(Id.Value);
            if (WelfareProgramsDetail != null && WelfareProgramsDetail.IsDeleted != true)
            {
                var model = new WelfareProgramsDetailViewModel();
                AutoMapper.Mapper.Map(WelfareProgramsDetail, model);
                
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
            var urlRefer = Request["UrlReferrer"];
            try
            {
              
                string idDeleteAll = Request["DeleteId-checkbox"];
                string[] arrDeleteId = idDeleteAll.Split(',');
                for (int i = 0; i < arrDeleteId.Count(); i++)
                {
                    var item = WelfareProgramsDetailRepository.GetWelfareProgramsDetailById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if(item != null)
                    {
                        if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        {
                            TempData["FailedMessage"] = "NotOwner";
                            return RedirectToAction("Index");
                        }

                        item.IsDeleted = true;
                        WelfareProgramsDetailRepository.UpdateWelfareProgramsDetail(item);
                    }
                }
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.DeleteSuccess;
                return Redirect(urlRefer);
            }
            catch (DbUpdateException)
            {
                TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.RelationError;
                return Redirect(urlRefer);
            }
        }
        #endregion
    }
}
