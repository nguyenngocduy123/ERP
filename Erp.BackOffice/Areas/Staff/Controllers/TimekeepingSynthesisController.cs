using System.Globalization;
using Erp.BackOffice.Staff.Models;
using Erp.BackOffice.Filters;
using Erp.BackOffice.Helpers;
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
    public class TimekeepingSynthesisController : Controller
    {
        private readonly ITimekeepingSynthesisRepository TimekeepingSynthesisRepository;
        private readonly IUserRepository userRepository;

        public TimekeepingSynthesisController(
            ITimekeepingSynthesisRepository _TimekeepingSynthesis
            , IUserRepository _user
            )
        {
            TimekeepingSynthesisRepository = _TimekeepingSynthesis;
            userRepository = _user;
        }

        #region Create
        public ViewResult Create()
        {
            var model = new TimekeepingSynthesisViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(TimekeepingSynthesisViewModel model)
        {
            if (ModelState.IsValid)
            {
                var TimekeepingSynthesis = new TimekeepingSynthesis();
                AutoMapper.Mapper.Map(model, TimekeepingSynthesis);
                TimekeepingSynthesis.IsDeleted = false;
                TimekeepingSynthesis.CreatedUserId = WebSecurity.CurrentUserId;
                TimekeepingSynthesis.ModifiedUserId = WebSecurity.CurrentUserId;
                TimekeepingSynthesis.AssignedUserId = WebSecurity.CurrentUserId;
                TimekeepingSynthesis.CreatedDate = DateTime.Now;
                TimekeepingSynthesis.ModifiedDate = DateTime.Now;
                TimekeepingSynthesisRepository.InsertTimekeepingSynthesis(TimekeepingSynthesis);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var TimekeepingSynthesis = TimekeepingSynthesisRepository.GetTimekeepingSynthesisById(Id.Value);
            if (TimekeepingSynthesis != null && TimekeepingSynthesis.IsDeleted != true)
            {
                var model = new TimekeepingSynthesisViewModel();
                AutoMapper.Mapper.Map(TimekeepingSynthesis, model);
                
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
        public ActionResult Edit(TimekeepingSynthesisViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var TimekeepingSynthesis = TimekeepingSynthesisRepository.GetTimekeepingSynthesisById(model.Id);
                    AutoMapper.Mapper.Map(model, TimekeepingSynthesis);
                    TimekeepingSynthesis.ModifiedUserId = WebSecurity.CurrentUserId;
                    TimekeepingSynthesis.ModifiedDate = DateTime.Now;
                    TimekeepingSynthesisRepository.UpdateTimekeepingSynthesis(TimekeepingSynthesis);

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
            var TimekeepingSynthesis = TimekeepingSynthesisRepository.GetTimekeepingSynthesisById(Id.Value);
            if (TimekeepingSynthesis != null && TimekeepingSynthesis.IsDeleted != true)
            {
                var model = new TimekeepingSynthesisViewModel();
                AutoMapper.Mapper.Map(TimekeepingSynthesis, model);
                
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
                    var item = TimekeepingSynthesisRepository.GetTimekeepingSynthesisById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if(item != null)
                    {
                        //if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        //{
                        //    TempData["FailedMessage"] = "NotOwner";
                        //    return RedirectToAction("Index");
                        //}

                        item.IsDeleted = true;
                        TimekeepingSynthesisRepository.UpdateTimekeepingSynthesis(item);
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
