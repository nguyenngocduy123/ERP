using System.Globalization;
using Erp.BackOffice.Account.Models;
using Erp.BackOffice.Filters;
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
using Erp.BackOffice.Helpers;

namespace Erp.BackOffice.Account.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class MemberCardDetailController : Controller
    {
        private readonly IMemberCardDetailRepository MemberCardDetailRepository;
        private readonly IUserRepository userRepository;

        public MemberCardDetailController(
            IMemberCardDetailRepository _MemberCardDetail
            , IUserRepository _user
            )
        {
            MemberCardDetailRepository = _MemberCardDetail;
            userRepository = _user;
        }

        #region Index

        public ViewResult Index(string txtSearch)
        {

            IQueryable<MemberCardDetailViewModel> q = MemberCardDetailRepository.GetAllMemberCardDetail()
                .Select(item => new MemberCardDetailViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    //Name = item.Name,
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
            var model = new MemberCardDetailViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(MemberCardDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var MemberCardDetail = new MemberCardDetail();
                AutoMapper.Mapper.Map(model, MemberCardDetail);
                MemberCardDetail.IsDeleted = false;
                MemberCardDetail.CreatedUserId = WebSecurity.CurrentUserId;
                MemberCardDetail.ModifiedUserId = WebSecurity.CurrentUserId;
                MemberCardDetail.AssignedUserId = WebSecurity.CurrentUserId;
                MemberCardDetail.CreatedDate = DateTime.Now;
                MemberCardDetail.ModifiedDate = DateTime.Now;
                MemberCardDetailRepository.InsertMemberCardDetail(MemberCardDetail);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var MemberCardDetail = MemberCardDetailRepository.GetMemberCardDetailById(Id.Value);
            if (MemberCardDetail != null && MemberCardDetail.IsDeleted != true)
            {
                var model = new MemberCardDetailViewModel();
                AutoMapper.Mapper.Map(MemberCardDetail, model);
                
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
        public ActionResult Edit(MemberCardDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var MemberCardDetail = MemberCardDetailRepository.GetMemberCardDetailById(model.Id);
                    AutoMapper.Mapper.Map(model, MemberCardDetail);
                    MemberCardDetail.ModifiedUserId = WebSecurity.CurrentUserId;
                    MemberCardDetail.ModifiedDate = DateTime.Now;
                    MemberCardDetailRepository.UpdateMemberCardDetail(MemberCardDetail);

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
            var MemberCardDetail = MemberCardDetailRepository.GetMemberCardDetailById(Id.Value);
            if (MemberCardDetail != null && MemberCardDetail.IsDeleted != true)
            {
                var model = new MemberCardDetailViewModel();
                AutoMapper.Mapper.Map(MemberCardDetail, model);
                
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
                    var item = MemberCardDetailRepository.GetMemberCardDetailById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if(item != null)
                    {
                        if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        {
                            TempData["FailedMessage"] = "NotOwner";
                            return RedirectToAction("Index");
                        }

                        item.IsDeleted = true;
                        MemberCardDetailRepository.UpdateMemberCardDetail(item);
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
