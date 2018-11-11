using System.Globalization;
using Erp.BackOffice.Crm.Models;
using Erp.BackOffice.Filters;
using Erp.Domain.Entities;
using Erp.Domain.Interfaces;
using Erp.Domain.Crm.Entities;
using Erp.Domain.Crm.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Erp.Utilities;
using WebMatrix.WebData;
using Erp.BackOffice.Helpers;

namespace Erp.BackOffice.Crm.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class CampaignController : Controller
    {
        private readonly ICampaignRepository CampaignRepository;
        private readonly IUserRepository userRepository;

        public CampaignController(ICampaignRepository _Campaign, IUserRepository _user)
        {
            CampaignRepository = _Campaign;
            userRepository = _user;
        }

        #region Index
        public ViewResult Index(string txtCampaign)
        {
            IEnumerable<CampaignViewModel> q = CampaignRepository.GetAllCampaign().AsEnumerable()
                .Select(item => new CampaignViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                }).OrderByDescending(m => m.ModifiedDate);
            if (!string.IsNullOrEmpty(txtCampaign))
            {
                q = q.Where(item => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.Name).ToLower().Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(txtCampaign).ToLower()));
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
            var model = new CampaignViewModel();
            model.TypeSelectList = Helpers.SelectListHelper.GetSelectList_Category("campaign_type", null, "Value", App_GlobalResources.Wording.Empty);
            model.StatusSelectList = Helpers.SelectListHelper.GetSelectList_Category("campaign_status", null, "Value", App_GlobalResources.Wording.Empty);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CampaignViewModel model)
        {
            if (ModelState.IsValid)
            {
                var Campaign = new Domain.Crm.Entities.Campaign();
                AutoMapper.Mapper.Map(model, Campaign);
                Campaign.IsDeleted = false;
                Campaign.CreatedUserId = WebSecurity.CurrentUserId;
                Campaign.ModifiedUserId = WebSecurity.CurrentUserId;
                Campaign.CreatedDate = DateTime.Now;
                Campaign.ModifiedDate = DateTime.Now;
                CampaignRepository.InsertCampaign(Campaign);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Edit");
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var Campaign = CampaignRepository.GetCampaignById(Id.Value);
            if (Campaign != null && Campaign.IsDeleted != true)
            {
                var model = new CampaignViewModel();
                AutoMapper.Mapper.Map(Campaign, model);

                if (model.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                {
                    TempData["FailedMessage"] = "NotOwner";
                    return RedirectToAction("Index");
                }
                model.TypeSelectList = Helpers.SelectListHelper.GetSelectList_Category("campaign_type", null, "Value", App_GlobalResources.Wording.Empty);
                model.StatusSelectList = Helpers.SelectListHelper.GetSelectList_Category("campaign_status", null, "Value", App_GlobalResources.Wording.Empty);
                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(CampaignViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var Campaign = CampaignRepository.GetCampaignById(model.Id);
                    AutoMapper.Mapper.Map(model, Campaign);
                    Campaign.ModifiedUserId = WebSecurity.CurrentUserId;
                    Campaign.ModifiedDate = DateTime.Now;
                    CampaignRepository.UpdateCampaign(Campaign);
                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Edit", new { Id = model.Id });
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }
        #endregion

        #region Detail
        public ActionResult Detail(int? Id)
        {
            var Campaign = CampaignRepository.GetCampaignById(Id.Value);
            if (Campaign != null && Campaign.IsDeleted != true)
            {
                var model = new CampaignViewModel();
                AutoMapper.Mapper.Map(Campaign, model);
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
                    var item = CampaignRepository.GetCampaignById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                        {
                            TempData["FailedMessage"] = "NotOwner";
                            return RedirectToAction("Index");
                        }
                        item.IsDeleted = true;
                        CampaignRepository.UpdateCampaign(item);
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
