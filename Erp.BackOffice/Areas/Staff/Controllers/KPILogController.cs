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
    public class KPILogController : Controller
    {
        private readonly IKPILogRepository KPILogRepository;
        private readonly IUserRepository userRepository;
        private readonly IKPICatalogRepository KPICatalogRepository;

        public KPILogController(
            IKPILogRepository _KPILog
            , IUserRepository _user
            , IKPICatalogRepository _KPICatalog
            )
        {
            KPILogRepository = _KPILog;
            userRepository = _user;
            KPICatalogRepository = _KPICatalog;
        }

        #region Index

        public ViewResult Index(string txtSearch)
        {

            IQueryable<KPILogViewModel> q = KPILogRepository.GetAllKPILog()
                .Select(item => new KPILogViewModel
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
            var model = new KPILogViewModel();
            model.Name = "Đợt đánh giá nhân sự tháng " + DateTime.Now.ToString("MM/yyyy");
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(KPILogViewModel model)
        {
            if (ModelState.IsValid)
            {
                var KPILog = new KPILog();
                AutoMapper.Mapper.Map(model, KPILog);
                KPILog.IsDeleted = false;
                KPILog.CreatedUserId = WebSecurity.CurrentUserId;
                KPILog.ModifiedUserId = WebSecurity.CurrentUserId;
                KPILog.AssignedUserId = WebSecurity.CurrentUserId;
                KPILog.CreatedDate = DateTime.Now;
                KPILog.ModifiedDate = DateTime.Now;

                //Lấy bảng thông số KPI
                var KPICatalog = KPICatalogRepository.GetKPICatalogById(model.KPICatalogId);
                KPILog.Description = KPICatalog.Description;
                KPILog.ExpectScore = KPICatalog.ExpectScore;
                KPILogRepository.InsertKPILog(KPILog);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Detail", new { Id = KPILog.Id });
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var KPILog = KPILogRepository.GetKPILogById(Id.Value);
            if (KPILog != null && KPILog.IsDeleted != true)
            {
                var model = new KPILogViewModel();
                AutoMapper.Mapper.Map(KPILog, model);
                
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
        public ActionResult Edit(KPILogViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var KPILog = KPILogRepository.GetKPILogById(model.Id);
                    AutoMapper.Mapper.Map(model, KPILog);
                    KPILog.ModifiedUserId = WebSecurity.CurrentUserId;
                    KPILog.ModifiedDate = DateTime.Now;
                    KPILogRepository.UpdateKPILog(KPILog);

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
            var KPILog = KPILogRepository.GetKPILogById(Id.Value);
            if (KPILog != null && KPILog.IsDeleted != true)
            {
                var model = new KPILogViewModel();
                AutoMapper.Mapper.Map(KPILog, model);
                
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
                    var item = KPILogRepository.GetKPILogById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if(item != null)
                    {
                        if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        {
                            TempData["FailedMessage"] = "NotOwner";
                            return RedirectToAction("Index");
                        }

                        item.IsDeleted = true;
                        KPILogRepository.UpdateKPILog(item);
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
