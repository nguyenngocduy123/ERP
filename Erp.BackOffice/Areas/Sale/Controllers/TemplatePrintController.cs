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
    public class TemplatePrintController : Controller
    {
        private readonly ITemplatePrintRepository TemplatePrintRepository;
        private readonly IUserRepository userRepository;

        public TemplatePrintController(
            ITemplatePrintRepository _TemplatePrint
            , IUserRepository _user
            )
        {
            TemplatePrintRepository = _TemplatePrint;
            userRepository = _user;
        }

        #region Index
        public ViewResult Index(string txtSearch)
        {
            IQueryable<TemplatePrintViewModel> q = TemplatePrintRepository.GetAllTemplatePrint()
                .Select(item => new TemplatePrintViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    ModifiedDate = item.ModifiedDate,
                    Code = item.Code,
                    Content = item.Content,
                    Title = item.Title
                }).OrderBy(m => m.Title);

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        #endregion

        #region Create
        public ViewResult Create()
        {
            var model = new TemplatePrintViewModel();

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(TemplatePrintViewModel model)
        {
            if (ModelState.IsValid)
            {
                var TemplatePrint = new TemplatePrint();
                AutoMapper.Mapper.Map(model, TemplatePrint);
                TemplatePrint.IsDeleted = false;
                TemplatePrint.CreatedUserId = WebSecurity.CurrentUserId;
                TemplatePrint.ModifiedUserId = WebSecurity.CurrentUserId;
                TemplatePrint.AssignedUserId = WebSecurity.CurrentUserId;
                TemplatePrint.CreatedDate = DateTime.Now;
                TemplatePrint.ModifiedDate = DateTime.Now;
                TemplatePrint.ContentDefault = model.Content;
                TemplatePrintRepository.InsertTemplatePrint(TemplatePrint);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var TemplatePrint = TemplatePrintRepository.GetTemplatePrintById(Id.Value);
            if (TemplatePrint != null && TemplatePrint.IsDeleted != true)
            {
                var model = new TemplatePrintViewModel();
                AutoMapper.Mapper.Map(TemplatePrint, model);

                //if (model.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                //{
                //    TempData["FailedMessage"] = "NotOwner";
                //    return RedirectToAction("Index");
                //}

                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(TemplatePrintViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var TemplatePrint = TemplatePrintRepository.GetTemplatePrintById(model.Id);
                    AutoMapper.Mapper.Map(model, TemplatePrint);
                    TemplatePrint.ModifiedUserId = WebSecurity.CurrentUserId;
                    TemplatePrint.ModifiedDate = DateTime.Now;
                    TemplatePrintRepository.UpdateTemplatePrint(TemplatePrint);

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
                    var item = TemplatePrintRepository.GetTemplatePrintById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        //if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        //{
                        //    TempData["FailedMessage"] = "NotOwner";
                        //    return RedirectToAction("Index");
                        //}

                        item.IsDeleted = true;
                        TemplatePrintRepository.UpdateTemplatePrint(item);
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
