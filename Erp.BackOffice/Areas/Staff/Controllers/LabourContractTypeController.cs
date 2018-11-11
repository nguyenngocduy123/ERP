using Erp.BackOffice.Filters;
using Erp.BackOffice.Staff.Models;
using Erp.Domain.Interfaces;
using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using Erp.Utilities;
using System;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Erp.BackOffice.Staff.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class LabourContractTypeController : Controller
    {
        private readonly ILabourContractTypeRepository LabourContractTypeRepository;
        private readonly IUserRepository userRepository;

        public LabourContractTypeController(
            ILabourContractTypeRepository _LabourContractType
            , IUserRepository _user
            )
        {
            LabourContractTypeRepository = _LabourContractType;
            userRepository = _user;
        }

        #region Index

        public ViewResult Index(string txtSearch)
        {

            IQueryable<LabourContractTypeViewModel> q = LabourContractTypeRepository.GetAllLabourContractType()
                .Select(item => new LabourContractTypeViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    Notice=item.Notice,
                    QuantityMonth=item.QuantityMonth
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
            var model = new LabourContractTypeViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(LabourContractTypeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var LabourContractType = new LabourContractType();
                AutoMapper.Mapper.Map(model, LabourContractType);
                LabourContractType.IsDeleted = false;
                LabourContractType.CreatedUserId = WebSecurity.CurrentUserId;
                LabourContractType.ModifiedUserId = WebSecurity.CurrentUserId;
                LabourContractType.AssignedUserId = WebSecurity.CurrentUserId;
                LabourContractType.CreatedDate = DateTime.Now;
                LabourContractType.ModifiedDate = DateTime.Now;
                LabourContractTypeRepository.InsertLabourContractType(LabourContractType);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var LabourContractType = LabourContractTypeRepository.GetLabourContractTypeById(Id.Value);
            if (LabourContractType != null && LabourContractType.IsDeleted != true)
            {
                var model = new LabourContractTypeViewModel();
                AutoMapper.Mapper.Map(LabourContractType, model);
                
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
        public ActionResult Edit(LabourContractTypeViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var LabourContractType = LabourContractTypeRepository.GetLabourContractTypeById(model.Id);
                    AutoMapper.Mapper.Map(model, LabourContractType);
                    LabourContractType.ModifiedUserId = WebSecurity.CurrentUserId;
                    LabourContractType.ModifiedDate = DateTime.Now;
                    LabourContractTypeRepository.UpdateLabourContractType(LabourContractType);

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
            var LabourContractType = LabourContractTypeRepository.GetLabourContractTypeById(Id.Value);
            if (LabourContractType != null && LabourContractType.IsDeleted != true)
            {
                var model = new LabourContractTypeViewModel();
                AutoMapper.Mapper.Map(LabourContractType, model);
                
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
                    var item = LabourContractTypeRepository.GetLabourContractTypeById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if(item != null)
                    {
                        //if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                        //{
                        //    TempData["FailedMessage"] = "NotOwner";
                        //    return RedirectToAction("Index");
                        //}

                        item.IsDeleted = true;
                        LabourContractTypeRepository.UpdateLabourContractType(item);
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
