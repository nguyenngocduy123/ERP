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
    public class KPILogDetail_ItemController : Controller
    {
        private readonly IKPILogDetail_ItemRepository KPILogDetail_ItemRepository;
        private readonly IUserRepository userRepository;
        private readonly IKPILogDetailRepository KPILogDetailRepository;
        private readonly IKPICatalogRepository KPICatalogRepository;

        public KPILogDetail_ItemController(
            IKPILogDetail_ItemRepository _KPILogDetail_Item
            , IUserRepository _user
            , IKPILogDetailRepository _KPILogDetail
            , IKPICatalogRepository _KPICatalog
            )
        {
            KPILogDetail_ItemRepository = _KPILogDetail_Item;
            userRepository = _user;
            KPILogDetailRepository = _KPILogDetail;
            KPICatalogRepository = _KPICatalog;
        }

        #region Index
        public ViewResult Index(int KPILogDetailId, int KPICatalogId)
        {
            IQueryable<KPILogDetail_ItemViewModel> q = KPILogDetail_ItemRepository.GetAllKPILogDetail_Item()
                .Where(item=>item.KPILogDetailId == KPILogDetailId)
                .Select(item => new KPILogDetail_ItemViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    TargetScore_From = item.TargetScore_From,
                    TargetScore_To = item.TargetScore_To,
                    AchieveScore = item.AchieveScore,
                    AchieveKPIWeight = item.AchieveKPIWeight,
                    KPIWeight = item.KPIWeight,
                    Measure = item.Measure
                }).OrderBy(m => m.Name);

            ViewBag.KPILogDetailId = KPILogDetailId;
            ViewBag.KPICatalogId = KPICatalogId;

            return View(q);
        }

        [HttpPost]
        public ActionResult Index(int KPILogDetailId, List<KPILogDetail_ItemViewModel> model, int KPICatalogId)
        {
            double totalScores = 0;
            foreach(var item in model)
            {
                KPILogDetail_Item KPILogDetail_Item = KPILogDetail_ItemRepository.GetKPILogDetail_ItemById(item.Id);
                KPILogDetail_Item.ModifiedDate = DateTime.Now;
                KPILogDetail_Item.ModifiedUserId = WebSecurity.CurrentUserId;
                KPILogDetail_Item.AchieveScore = item.AchieveScore;
                if (KPILogDetail_Item.TargetScore_To > KPILogDetail_Item.TargetScore_From)
                {
                    KPILogDetail_Item.AchieveKPIWeight = KPILogDetail_Item.AchieveScore / KPILogDetail_Item.TargetScore_To * KPILogDetail_Item.KPIWeight;
                }
                else
                {
                    KPILogDetail_Item.AchieveKPIWeight = (1 - KPILogDetail_Item.AchieveScore / KPILogDetail_Item.TargetScore_From) * KPILogDetail_Item.KPIWeight;
                }

                KPILogDetail_ItemRepository.UpdateKPILogDetail_Item(KPILogDetail_Item);

                totalScores += KPILogDetail_Item.AchieveKPIWeight;
            }

            KPILogDetail KPILogDetail = KPILogDetailRepository.GetAllKPILogDetail()
                .Where(item => item.Id == KPILogDetailId)
                .FirstOrDefault();

            if (KPILogDetail != null)
            {
                var KPICatalog = KPICatalogRepository.GetKPICatalogById(KPICatalogId);
                if (totalScores >= KPICatalog.ExpectScore)
                {
                    KPILogDetail.Completed = true;
                }
                else
                {
                    KPILogDetail.Completed = false;
                }

                KPILogDetail.AchieveKPIWeight = totalScores;
                KPILogDetailRepository.UpdateKPILogDetail(KPILogDetail);
            }
            return RedirectToAction("Index", new { KPILogDetailId = KPILogDetailId, KPICatalogId = KPICatalogId });
        }
        #endregion
    }
}
