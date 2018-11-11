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
    public class ShiftsController : Controller
    {
        private readonly IShiftsRepository ShiftsRepository;
        private readonly IUserRepository userRepository;

        public ShiftsController(
            IShiftsRepository _Shifts
            , IUserRepository _user
            )
        {
            ShiftsRepository = _Shifts;
            userRepository = _user;
        }

        #region Index

        public ViewResult Index(string txtSearch)
        {

            IQueryable<ShiftsViewModel> q = ShiftsRepository.GetAllShifts()
                .Select(item => new ShiftsViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    StartTime=item.StartTime,
                    EndTime=item.EndTime,
                    Code=item.Code,
                    NightShifts = item.NightShifts,
                    EndTimeIn=item.EndTimeIn,
                    StartTimeOut=item.StartTimeOut,
                    CategoryShifts=item.CategoryShifts,
                    EndTimeOut=item.EndTimeOut,
                    StartTimeIn=item.StartTimeIn,
                    MinuteEarly=item.MinuteEarly,
                    MinuteLate=item.MinuteLate
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
            var model = new ShiftsViewModel();
            model.MinuteEarly = 0;
            model.MinuteLate = 0;

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ShiftsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var Shifts = new Domain.Staff.Entities.Shifts();
                AutoMapper.Mapper.Map(model, Shifts);
                Shifts.IsDeleted = false;
                Shifts.CreatedUserId = WebSecurity.CurrentUserId;
                Shifts.ModifiedUserId = WebSecurity.CurrentUserId;
                Shifts.CreatedDate = DateTime.Now;
                Shifts.ModifiedDate = DateTime.Now;
                Shifts.CategoryShifts = "Part-time";
                string[] list_StartTime = model.StartTime.Split(':');
                if (Convert.ToInt32(list_StartTime[0]) < 10)
                {
                    Shifts.StartTime = 0 + model.StartTime;
                }
                string[] list_StartTimeIn = model.StartTimeIn.Split(':');
                if (Convert.ToInt32(list_StartTimeIn[0]) < 10)
                {
                    Shifts.StartTimeIn = 0 + model.StartTimeIn;
                }
                string[] list_StartTimeOut = model.StartTimeOut.Split(':');
                if (Convert.ToInt32(list_StartTimeOut[0]) < 10)
                {
                    Shifts.StartTimeOut = 0 + model.StartTimeOut;
                }
                string[] list_EndTime = model.EndTime.Split(':');
                if (Convert.ToInt32(list_EndTime[0]) < 10)
                {
                    Shifts.EndTime = 0 + model.EndTime;
                }
                string[] list_EndTimeIn = model.EndTimeIn.Split(':');
                if (Convert.ToInt32(list_EndTimeIn[0]) < 10)
                {
                    Shifts.EndTimeIn = 0 + model.EndTimeIn;
                }
                string[] list_EndTimeOut = model.EndTimeOut.Split(':');
                if (Convert.ToInt32(list_EndTimeOut[0]) < 10)
                {
                    Shifts.EndTimeOut = 0 + model.EndTimeOut;
                }
                ShiftsRepository.InsertShifts(Shifts);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var Shifts = ShiftsRepository.GetShiftsById(Id.Value);
            if (Shifts != null && Shifts.IsDeleted != true)
            {
                var model = new ShiftsViewModel();
                AutoMapper.Mapper.Map(Shifts, model);
                
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
        public ActionResult Edit(ShiftsViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var Shifts = ShiftsRepository.GetShiftsById(model.Id);
                    AutoMapper.Mapper.Map(model, Shifts);
                    Shifts.ModifiedUserId = WebSecurity.CurrentUserId;
                    Shifts.ModifiedDate = DateTime.Now;
                    string[] list_StartTime = model.StartTime.Split(':');
                    if (Convert.ToInt32(list_StartTime[0]) < 10)
                    {
                        Shifts.StartTime = 0 + model.StartTime;
                    }
                    string[] list_StartTimeIn = model.StartTimeIn.Split(':');
                    if (Convert.ToInt32(list_StartTimeIn[0]) < 10)
                    {
                        Shifts.StartTimeIn = 0 + model.StartTimeIn;
                    }
                    string[] list_StartTimeOut = model.StartTimeOut.Split(':');
                    if (Convert.ToInt32(list_StartTimeOut[0]) < 10)
                    {
                        Shifts.StartTimeOut = 0 + model.StartTimeOut;
                    }
                    string[] list_EndTime = model.EndTime.Split(':');
                    if (Convert.ToInt32(list_EndTime[0]) < 10)
                    {
                        Shifts.EndTime = 0 + model.EndTime;
                    }
                    string[] list_EndTimeIn = model.EndTimeIn.Split(':');
                    if (Convert.ToInt32(list_EndTimeIn[0]) < 10)
                    {
                        Shifts.EndTimeIn = 0 + model.EndTimeIn;
                    }
                    string[] list_EndTimeOut = model.EndTimeOut.Split(':');
                    if (Convert.ToInt32(list_EndTimeOut[0]) < 10)
                    {
                        Shifts.EndTimeOut = 0 + model.EndTimeOut;
                    }
                    ShiftsRepository.UpdateShifts(Shifts);

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
                    var item = ShiftsRepository.GetShiftsById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if(item != null)
                    {
                        //if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                        //{
                        //    TempData["FailedMessage"] = "NotOwner";
                        //    return RedirectToAction("Index");
                        //}

                        item.IsDeleted = true;
                        ShiftsRepository.UpdateShifts(item);
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
