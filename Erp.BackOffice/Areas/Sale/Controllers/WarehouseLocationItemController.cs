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
    public class WarehouseLocationItemController : Controller
    {
        private readonly IWarehouseLocationItemRepository WarehouseLocationItemRepository;
        private readonly IUserRepository userRepository;

        public WarehouseLocationItemController(
            IWarehouseLocationItemRepository _WarehouseLocationItem
            , IUserRepository _user
            )
        {
            WarehouseLocationItemRepository = _WarehouseLocationItem;
            userRepository = _user;
        }

        #region Index

        public ViewResult Index(string LoCode, int? productId,string ProductInboundCode, int? warehouseId)
        {
            var start_date = Request["start_date"];
            var end_date = Request["end_date"];
            var startDate = Request["startDate"];
            var endDate = Request["endDate"];
            IEnumerable<WarehouseLocationItemViewModel> q = WarehouseLocationItemRepository.GetAllvwWarehouseLocationItem()
                //.Where(x => x.BranchId == Helpers.Common.CurrentUser.BranchId)
                .Select(item => new WarehouseLocationItemViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    ModifiedDate = item.ModifiedDate,
                    Floor = item.Floor,
                    Position = item.Position,
                    ProductId = item.ProductId,
                    ProductInboundDetailId = item.ProductInboundDetailId,
                    ProductInboundId = item.ProductInboundId,
                    Shelf = item.Shelf,
                    SN = item.SN,
                    WarehouseId = item.WarehouseId,
                    WarehouseName = item.WarehouseName,
                    ProductInboundCode = item.ProductInboundCode,
                    ProductName = item.ProductName,
                    ExpiryDate = item.ExpiryDate,
                    LoCode=item.LoCode,
                    IsOut=item.IsOut

                }).OrderByDescending(m => m.CreatedDate).ThenByDescending(m => m.ModifiedDate);
            if (!string.IsNullOrEmpty(LoCode))
            {
                LoCode = LoCode.ToLower();
                q = q.Where(x => x.LoCode!=null&&x.LoCode.ToLower().Contains(LoCode));
            }
            if (!string.IsNullOrEmpty(ProductInboundCode))
            {
                ProductInboundCode = ProductInboundCode.ToLower();
                q = q.Where(x => x.ProductInboundCode.ToLower().Contains(ProductInboundCode));
            }
            if (productId!=null&&productId.Value>0)
            {
                q = q.Where(x => x.ProductId==productId);
            }
            if (warehouseId != null && warehouseId.Value > 0)
            {
                q = q.Where(x => x.WarehouseId == warehouseId);
            }
            q = q.Where(x => x.IsOut != true);
            if (!string.IsNullOrEmpty(start_date) && !string.IsNullOrEmpty(end_date))
            {
                DateTime start_d;
                if (DateTime.TryParseExact(start_date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out start_d))
                {
                    DateTime end_d;
                    if (DateTime.TryParseExact(end_date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out end_d))
                    {
                        end_d = end_d.AddHours(23);
                        q = q.Where(x => start_d <= x.ExpiryDate && x.ExpiryDate <= end_d);
                    }
                }
            }
            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                DateTime start_d;
                if (DateTime.TryParseExact(startDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out start_d))
                {
                    DateTime end_d;
                    if (DateTime.TryParseExact(endDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out end_d))
                    {
                        end_d = end_d.AddHours(23);
                        q = q.Where(x => start_d <= x.CreatedDate && x.CreatedDate <= end_d);
                    }
                }
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
            var model = new WarehouseLocationItemViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(WarehouseLocationItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                var WarehouseLocationItem = new WarehouseLocationItem();
                AutoMapper.Mapper.Map(model, WarehouseLocationItem);
                WarehouseLocationItem.IsDeleted = false;
                WarehouseLocationItem.CreatedUserId = WebSecurity.CurrentUserId;
                WarehouseLocationItem.ModifiedUserId = WebSecurity.CurrentUserId;
                WarehouseLocationItem.AssignedUserId = WebSecurity.CurrentUserId;
                WarehouseLocationItem.CreatedDate = DateTime.Now;
                WarehouseLocationItem.ModifiedDate = DateTime.Now;
                WarehouseLocationItemRepository.InsertWarehouseLocationItem(WarehouseLocationItem);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var WarehouseLocationItem = WarehouseLocationItemRepository.GetWarehouseLocationItemById(Id.Value);
            if (WarehouseLocationItem != null && WarehouseLocationItem.IsDeleted != true)
            {
                var model = new WarehouseLocationItemViewModel();
                AutoMapper.Mapper.Map(WarehouseLocationItem, model);
                
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
        public ActionResult Edit(WarehouseLocationItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var WarehouseLocationItem = WarehouseLocationItemRepository.GetWarehouseLocationItemById(model.Id.Value);
                    AutoMapper.Mapper.Map(model, WarehouseLocationItem);
                    WarehouseLocationItem.ModifiedUserId = WebSecurity.CurrentUserId;
                    WarehouseLocationItem.ModifiedDate = DateTime.Now;
                    WarehouseLocationItemRepository.UpdateWarehouseLocationItem(WarehouseLocationItem);

                    if (string.IsNullOrEmpty(Request["IsPopup"]) == false)
                    {
                        ViewBag.IsPopup = Request["IsPopup"];
                        return View(model);
                    }

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
            var WarehouseLocationItem = WarehouseLocationItemRepository.GetWarehouseLocationItemById(Id.Value);
            if (WarehouseLocationItem != null && WarehouseLocationItem.IsDeleted != true)
            {
                var model = new WarehouseLocationItemViewModel();
                AutoMapper.Mapper.Map(WarehouseLocationItem, model);
                
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
                    var item = WarehouseLocationItemRepository.GetWarehouseLocationItemById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if(item != null)
                    {
                        //if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                        //{
                        //    TempData["FailedMessage"] = "NotOwner";
                        //    return RedirectToAction("Index");
                        //}

                        item.IsDeleted = true;
                        WarehouseLocationItemRepository.UpdateWarehouseLocationItem(item);
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
