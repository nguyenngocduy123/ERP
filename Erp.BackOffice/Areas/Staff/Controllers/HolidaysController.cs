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

namespace Erp.BackOffice.Staff.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class HolidaysController : Controller
    {
        private readonly IHolidaysRepository HolidaysRepository;
        private readonly IUserRepository userRepository;

        public HolidaysController(
            IHolidaysRepository _Holidays
            , IUserRepository _user
            )
        {
            HolidaysRepository = _Holidays;
            userRepository = _user;
        }

        #region Index

        public ViewResult Index(string txtSearch)
        {

            IQueryable<HolidaysViewModel> q = HolidaysRepository.GetAllHolidays()
                .Select(item => new HolidaysViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    Code=item.Code,
                    DayStart=item.DayStart,
                    DayEnd=item.DayEnd,
                    Note=item.Note,
                    DayOffset=item.DayOffset
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
            var model = new HolidaysViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(HolidaysViewModel model)
        {
            if (ModelState.IsValid)
            {
                var Holidays = new Domain.Staff.Entities.Holidays();
                AutoMapper.Mapper.Map(model, Holidays);
                Holidays.IsDeleted = false;
                Holidays.CreatedUserId = WebSecurity.CurrentUserId;
                Holidays.ModifiedUserId = WebSecurity.CurrentUserId;
                Holidays.CreatedDate = DateTime.Now;
                Holidays.ModifiedDate = DateTime.Now;
                HolidaysRepository.InsertHolidays(Holidays);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var Holidays = HolidaysRepository.GetHolidaysById(Id.Value);
            if (Holidays != null && Holidays.IsDeleted != true)
            {
                var model = new HolidaysViewModel();
                AutoMapper.Mapper.Map(Holidays, model);
                
                //if (model.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
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
        public ActionResult Edit(HolidaysViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var Holidays = HolidaysRepository.GetHolidaysById(model.Id);
                    AutoMapper.Mapper.Map(model, Holidays);
                    Holidays.ModifiedUserId = WebSecurity.CurrentUserId;
                    Holidays.ModifiedDate = DateTime.Now;
                    HolidaysRepository.UpdateHolidays(Holidays);

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
                    var item = HolidaysRepository.GetHolidaysById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if(item != null)
                    {
                        //if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                        //{
                        //    TempData["FailedMessage"] = "NotOwner";
                        //    return RedirectToAction("Index");
                        //}

                        item.IsDeleted = true;
                        HolidaysRepository.UpdateHolidays(item);
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
