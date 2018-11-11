using System.Globalization;
using <APP_NAME>.BackOffice.<AREA_NAME>.Models;
using <APP_NAME>.BackOffice.Filters;
using <APP_NAME>.Domain.Entities;
using <APP_NAME>.Domain.Interfaces;
using <APP_NAME>.Domain.<AREA_NAME>.Entities;
using <APP_NAME>.Domain.<AREA_NAME>.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using <APP_NAME>.Utilities;
using WebMatrix.WebData;
using <APP_NAME>.BackOffice.Helpers;

namespace <APP_NAME>.BackOffice.<AREA_NAME>.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [<APP_NAME>.BackOffice.Helpers.NoCacheHelper]
    public class <MODULE_NAME>Controller : Controller
    {
        private readonly I<MODULE_NAME>Repository <MODULE_NAME>Repository;
        private readonly IUserRepository userRepository;

        public <MODULE_NAME>Controller(
            I<MODULE_NAME>Repository _<MODULE_NAME>
            , IUserRepository _user
            )
        {
            <MODULE_NAME>Repository = _<MODULE_NAME>;
            userRepository = _user;
        }

        #region Index

        public ViewResult Index(string txtSearch)
        {

            IQueryable<<MODULE_NAME>ViewModel> q = <MODULE_NAME>Repository.GetAll<MODULE_NAME>()
                .Select(item => new <MODULE_NAME>ViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
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
            var model = new <MODULE_NAME>ViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(<MODULE_NAME>ViewModel model)
        {
            if (ModelState.IsValid)
            {
                var <MODULE_NAME> = new <MODULE_NAME>();
                AutoMapper.Mapper.Map(model, <MODULE_NAME>);
                <MODULE_NAME>.IsDeleted = false;
                <MODULE_NAME>.CreatedUserId = WebSecurity.CurrentUserId;
                <MODULE_NAME>.ModifiedUserId = WebSecurity.CurrentUserId;
                <MODULE_NAME>.AssignedUserId = WebSecurity.CurrentUserId;
                <MODULE_NAME>.CreatedDate = DateTime.Now;
                <MODULE_NAME>.ModifiedDate = DateTime.Now;
                <MODULE_NAME>Repository.Insert<MODULE_NAME>(<MODULE_NAME>);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var <MODULE_NAME> = <MODULE_NAME>Repository.Get<MODULE_NAME>ById(Id.Value);
            if (<MODULE_NAME> != null && <MODULE_NAME>.IsDeleted != true)
            {
                var model = new <MODULE_NAME>ViewModel();
                AutoMapper.Mapper.Map(<MODULE_NAME>, model);
                
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
        public ActionResult Edit(<MODULE_NAME>ViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var <MODULE_NAME> = <MODULE_NAME>Repository.Get<MODULE_NAME>ById(model.Id);
                    AutoMapper.Mapper.Map(model, <MODULE_NAME>);
                    <MODULE_NAME>.ModifiedUserId = WebSecurity.CurrentUserId;
                    <MODULE_NAME>.ModifiedDate = DateTime.Now;
                    <MODULE_NAME>Repository.Update<MODULE_NAME>(<MODULE_NAME>);

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
            var <MODULE_NAME> = <MODULE_NAME>Repository.Get<MODULE_NAME>ById(Id.Value);
            if (<MODULE_NAME> != null && <MODULE_NAME>.IsDeleted != true)
            {
                var model = new <MODULE_NAME>ViewModel();
                AutoMapper.Mapper.Map(<MODULE_NAME>, model);
                
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
                    var item = <MODULE_NAME>Repository.Get<MODULE_NAME>ById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if(item != null)
                    {
                        if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        {
                            TempData["FailedMessage"] = "NotOwner";
                            return RedirectToAction("Index");
                        }

                        item.IsDeleted = true;
                        <MODULE_NAME>Repository.Update<MODULE_NAME>(item);
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
