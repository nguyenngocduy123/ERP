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
    public class KPILogDetailController : Controller
    {
        private readonly IKPILogDetailRepository KPILogDetailRepository;
        private readonly IUserRepository userRepository;
        private readonly IKPILogDetail_ItemRepository KPILogDetail_ItemRepository;
        private readonly IKPIItemRepository KPIItemRepository;

        public KPILogDetailController(
            IKPILogDetailRepository _KPILogDetail
            , IUserRepository _user
            , IKPILogDetail_ItemRepository _KPILogDetail_Item
            , IKPIItemRepository _KPIItem
            )
        {
            KPILogDetailRepository = _KPILogDetail;
            userRepository = _user;
            KPILogDetail_ItemRepository = _KPILogDetail_Item;
            KPIItemRepository = _KPIItem;
        }

        #region Index

        public ViewResult Index(int KPILogId, int KPICatalogId)
        {
            IQueryable<KPILogDetailViewModel> q = KPILogDetailRepository.GetAllvwKPILogDetail()
                .Where(item => item.KPILogId == KPILogId)
                .Select(item => new KPILogDetailViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    ModifiedDate = item.ModifiedDate,
                    AchieveKPIWeight = item.AchieveKPIWeight,
                    StaffName = item.StaffName,
                    Completed = item.Completed
                }).OrderBy(m => m.StaffName);

            ViewBag.KPILogId = KPILogId;
            ViewBag.KPICatalogId = KPICatalogId;
            return View(q);
        }
        #endregion

        #region Create
        [HttpPost]
        public ActionResult Create(int KPILogId, int StaffId, int KPICatalogId)
        {
            KPILogDetail KPILogDetail = KPILogDetailRepository.GetAllKPILogDetail()
                .Where(item => item.KPILogId == KPILogId && item.StaffId == StaffId)
                .FirstOrDefault();

            if (KPILogDetail == null)
            {
                KPILogDetail = new KPILogDetail();
                KPILogDetail.IsDeleted = false;
                KPILogDetail.CreatedUserId = WebSecurity.CurrentUserId;
                KPILogDetail.ModifiedUserId = WebSecurity.CurrentUserId;
                KPILogDetail.CreatedDate = DateTime.Now;
                KPILogDetail.ModifiedDate = DateTime.Now;

                KPILogDetail.KPILogId = KPILogId;
                KPILogDetail.StaffId = StaffId;

                KPILogDetailRepository.InsertKPILogDetail(KPILogDetail);

                var listKPIItem = KPIItemRepository.GetAllKPIItem()
                    .Where(item => item.KPICatalogId == KPICatalogId)
                    .ToList();

                //Thêm tiêu chí
                foreach (var item in listKPIItem)
                {
                    var KPILogDetail_Item = new KPILogDetail_Item();
                    AutoMapper.Mapper.Map(item, KPILogDetail_Item);
                    KPILogDetail_Item.IsDeleted = false;
                    KPILogDetail_Item.CreatedUserId = WebSecurity.CurrentUserId;
                    KPILogDetail_Item.ModifiedUserId = WebSecurity.CurrentUserId;
                    KPILogDetail_Item.CreatedDate = DateTime.Now;
                    KPILogDetail_Item.ModifiedDate = DateTime.Now;
                    KPILogDetail_Item.KPILogDetailId = KPILogDetail.Id;

                    KPILogDetail_ItemRepository.InsertKPILogDetail_Item(KPILogDetail_Item);
                }
            }

            return Content("success");
        }
        #endregion

        #region Delete
        [HttpPost]
        public ActionResult Delete(int KPILogId)
        {
            try
            {
                string idDeleteAll = Request["DeleteId-checkbox"];
                string[] arrDeleteId = idDeleteAll.Split(',');
                for (int i = 0; i < arrDeleteId.Count(); i++)
                {
                    var item = KPILogDetailRepository.GetKPILogDetailById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        {
                            TempData["FailedMessage"] = "NotOwner";
                            return RedirectToAction("Index");
                        }

                        item.IsDeleted = true;
                        KPILogDetailRepository.UpdateKPILogDetail(item);
                    }
                }
            }
            catch (DbUpdateException)
            {
                
            }

            return RedirectToAction("Detail", "KPILog", new { Id = KPILogId });
        }
        #endregion
    }
}
