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
using System.Web.Script.Serialization;
using Erp.BackOffice.Crm.Controllers;

namespace Erp.BackOffice.Staff.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class CalendarVisitDrugStoreController : Controller
    {
        private readonly ICalendarVisitDrugStoreRepository CalendarVisitDrugStoreRepository;
        private readonly IUserRepository userRepository;

        public CalendarVisitDrugStoreController(
            ICalendarVisitDrugStoreRepository _CalendarVisitDrugStore
            , IUserRepository _user
            )
        {
            CalendarVisitDrugStoreRepository = _CalendarVisitDrugStore;
            userRepository = _user;
        }


        #region Edit
        public ActionResult Edit(int? Id)
        {
            var CalendarVisitDrugStore = CalendarVisitDrugStoreRepository.GetCalendarVisitDrugStoreById(Id.Value);
            if (CalendarVisitDrugStore != null && CalendarVisitDrugStore.IsDeleted != true)
            {
                var model = new CalendarVisitDrugStoreViewModel();
                AutoMapper.Mapper.Map(CalendarVisitDrugStore, model);

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
        public ActionResult Edit(CalendarVisitDrugStoreViewModel model)
        {
            var urlRefer = Request["UrlReferrer"];
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var CalendarVisitDrugStore = CalendarVisitDrugStoreRepository.GetCalendarVisitDrugStoreById(model.Id);
                    AutoMapper.Mapper.Map(model, CalendarVisitDrugStore);
                    CalendarVisitDrugStore.ModifiedUserId = WebSecurity.CurrentUserId;
                    CalendarVisitDrugStore.ModifiedDate = DateTime.Now;
                    CalendarVisitDrugStoreRepository.UpdateCalendarVisitDrugStore(CalendarVisitDrugStore);

                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;

                    var task = CalendarVisitDrugStoreRepository.GetvwCalendarVisitDrugStoreById(CalendarVisitDrugStore.Id);
                    ProcessController.Run("CalendarVisitDrugStore"
                        , "Edit"
                        , task.Id
                        , null
                        , null
                        , task
                        , task.DrugStoreId.Value.ToString());

                    if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                    {
                        return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
                    }
                    return Redirect(urlRefer);
                }
                return View(model);
            }
            return View(model);
        }

        #endregion

        #region Detail
        public ActionResult Detail(int? Id)
        {
            var CalendarVisitDrugStore = CalendarVisitDrugStoreRepository.GetvwCalendarVisitDrugStoreById(Id.Value);
            if (CalendarVisitDrugStore != null && CalendarVisitDrugStore.IsDeleted != true)
            {
                var model = new CalendarVisitDrugStoreViewModel();
                AutoMapper.Mapper.Map(CalendarVisitDrugStore, model);

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
        public JsonResult Delete(int? id)
        {
            try
            {
                var item = CalendarVisitDrugStoreRepository.GetCalendarVisitDrugStoreById(id.Value);
                if (item != null)
                {
                    CalendarVisitDrugStoreRepository.DeleteCalendarVisitDrugStore(id.Value);
                }
                return Json(new { Result = "success", Message = App_GlobalResources.Wording.DeleteSuccess },
                             JsonRequestBehavior.AllowGet);

            }
            catch (DbUpdateException)
            {
                return Json(new { Result = "success", Message = App_GlobalResources.Error.RelationError },
                              JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Index
        public ViewResult Index(int? Month, int? Year, string CityId, string DistrictId, int? branchId, int? tdvId)
        {
            var user = userRepository.GetByvwUserName(Helpers.Common.CurrentUser.UserName);
            var model = new CalendarVisitDrugStoreViewModel();
            List<CalendarVisitDrugStoreViewModel> q = CalendarVisitDrugStoreRepository.GetvwAllCalendarVisitDrugStore()
                .Select(i => new CalendarVisitDrugStoreViewModel
                {
                    Id = i.Id,
                    DrugStoreCode = i.DrugStoreCode,
                    DrugStoreName = i.DrugStoreName,
                    DrugStoreId = i.DrugStoreId,
                    StaffId = i.StaffId,
                    StaffCode = i.StaffCode,
                    StaffName = i.StaffName,
                    StartDate = i.StartDate,
                    EndDate = i.EndDate,
                    Note = i.Note,
                    Status = i.Status,
                    Month = i.Month,
                    Year = i.Year,
                    DistrictId=i.DistrictId,
                    CityId=i.CityId,
                    CreatedUserId=i.CreatedUserId,
                    CreatedDate=i.CreatedDate
                }).ToList();
            var month = Month == null ? DateTime.Now.Month : Month.Value;
            var year = Year == null ? DateTime.Now.Year : Year.Value;

            DateTime aDateTime = new DateTime(year, month, 1);
            DateTime retDateTime = aDateTime.AddMonths(1).AddDays(-1);

            q = q.Where(x => x.Month == month && x.Year == year).ToList();
            if (!Erp.BackOffice.Filters.SecurityFilter.IsAdmin()&&!Erp.BackOffice.Filters.SecurityFilter.IsKeToan())
            {
                q = q.Where(x => ("," + user.DrugStore + ",").Contains("," + x.DrugStoreId + ",") == true).ToList();
            }

            if (!string.IsNullOrEmpty(CityId))
            {
                q = q.Where(x => x.CityId == CityId).ToList();
            }
            if (!string.IsNullOrEmpty(DistrictId))
            {
                q = q.Where(x => x.DistrictId == DistrictId).ToList();
            }
            if(branchId!=null&&branchId.Value>0)
            {
              q = q.Where(x => x.DrugStoreId == branchId).ToList();
            }
           
            if(tdvId!=null&&tdvId.Value>0)
            {
               // var user_tdv = userRepository.GetvwUserById(tdvId.Value);
                //q = q.Where(x => ("," + user_tdv.DrugStore + ",").Contains("," + x.DrugStoreId + ",") == true).ToList();
                q = q.Where(x => x.CreatedUserId == tdvId).ToList();
            }
            var dataEvent = q.Select(e => new
            {
                title = e.StaffName + " - " + e.DrugStoreName,
                start = e.StartDate.Value.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                end = e.EndDate.Value.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                allDay = false,
                className = (e.Status == "pending" ? "primary" : (e.Status == "success" ? "success" : "danger")),
                url = e.Id,
                backgroundColor = (e.Status == "pending" ? "#5090C1" : (e.Status == "success" ? "#82AF6F" : "#D15B47")),
                tooltip = e.Note
            }).ToList();
            ViewBag.dataEvent = new JavaScriptSerializer().Serialize(dataEvent);
            ViewBag.aDateTime = aDateTime.ToString("yyyy-MM-dd");
            ViewBag.date = aDateTime;
            return View(model);
        }
        #endregion

        #region Create
        public ViewResult Create(DateTime date)
        {
            var model = new CalendarVisitDrugStoreViewModel();
            model.StartDate = date;
            model.EndDate = date;
            model.StaffId = Erp.BackOffice.Helpers.Common.GetStaffByCurrentUser().Id;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CalendarVisitDrugStoreViewModel model)
        {
            var urlRefer = Request["UrlReferrer"];
            var DrugStore = Request["DrugStore"];
            if (ModelState.IsValid)
            {
                List<string> ListArrayID = new List<string>();
                if (!string.IsNullOrEmpty(DrugStore))
                    ListArrayID = DrugStore.Split(',').ToList();
               
                for (int i = 0; i < ListArrayID.Count(); i++)
                {
                    //
                    var insert = new CalendarVisitDrugStore();
                    AutoMapper.Mapper.Map(model, insert);
                    insert.IsDeleted = false;
                    insert.CreatedUserId = WebSecurity.CurrentUserId;
                    insert.ModifiedUserId = WebSecurity.CurrentUserId;
                    insert.AssignedUserId = WebSecurity.CurrentUserId;
                    insert.CreatedDate = DateTime.Now;
                    insert.ModifiedDate = DateTime.Now;
                    insert.Status = "pending";
                    insert.DrugStoreId = Convert.ToInt32(ListArrayID[i]);
                    CalendarVisitDrugStoreRepository.InsertCalendarVisitDrugStore(insert);
                    var task = CalendarVisitDrugStoreRepository.GetvwCalendarVisitDrugStoreById(insert.Id);
                    ProcessController.Run("CalendarVisitDrugStore"
                        , "Create"
                        , task.Id
                        , null
                        , null
                        , task
                        , task.DrugStoreId.Value.ToString());
                }

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
               
                if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                {
                    return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
                }
                return Redirect(urlRefer);
            }
            return View(model);
        }
        #endregion

        //#region UpdateDate
        //[HttpPost]
        //public ActionResult UpdateDate(int? Id, string date)
        //{

        //    var item = CalendarVisitDrugStoreRepository.GetCalendarVisitDrugStoreById(Id.Value);
        //    if (item != null)
        //    {
        //        var time = Convert.ToDateTime(date);
        //        DateTime aDateTime = new DateTime(time.Year, time.Month, time.Day);
        //        item.Day = aDateTime;
        //        CalendarVisitDrugStoreRepository.UpdateCalendarVisitDrugStore(item);
        //        return Content("success");
        //    }
        //    return Content("error");
        //}
        //#endregion
    }
}
