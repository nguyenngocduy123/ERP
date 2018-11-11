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
    public class FPMachineController : Controller
    {
        private readonly IFPMachineRepository FPMachineRepository;
        private readonly IUserRepository userRepository;

        public FPMachineController(
            IFPMachineRepository _FPMachine
            , IUserRepository _user
            )
        {
            FPMachineRepository = _FPMachine;
            userRepository = _user;
        }

        #region Index

        public ViewResult Index(string txtSearch)
        {
            var q = FPMachineRepository.GetAllFPMachine().OrderByDescending(m => m.ModifiedDate);
            List<FPMachineViewModel> model = new List<FPMachineViewModel>();
            AutoMapper.Mapper.Map(q, model);
            var van_tay = FPMachineRepository.GetAllFingerPrint();
            foreach (var item in model)
            {
                var list = van_tay.Where(x => x.FPMachineId == item.Id);
                var count_van_tay = list.Where(x =>!string.IsNullOrEmpty(x.TmpData)).GroupBy(x => x.UserId).Count();
                var total_van_tay= list.GroupBy(x => x.UserId).Count();
                item.count_van_tay = count_van_tay;
                item.total_van_tay = total_van_tay;
            }
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(model);
        }
        #endregion

        #region Create
        public ViewResult Create()
        {
            var model = new FPMachineViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(FPMachineViewModel model)
        {
            if (ModelState.IsValid)
            {
                var FPMachine = new FPMachine();
                AutoMapper.Mapper.Map(model, FPMachine);
                FPMachine.IsDeleted = false;
                FPMachine.CreatedUserId = WebSecurity.CurrentUserId;
                FPMachine.ModifiedUserId = WebSecurity.CurrentUserId;
                FPMachine.AssignedUserId = WebSecurity.CurrentUserId;
                FPMachine.CreatedDate = DateTime.Now;
                FPMachine.ModifiedDate = DateTime.Now;
                FPMachineRepository.InsertFPMachine(FPMachine);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var FPMachine = FPMachineRepository.GetFPMachineById(Id.Value);
            if (FPMachine != null && FPMachine.IsDeleted != true)
            {
                var model = new FPMachineViewModel();
                AutoMapper.Mapper.Map(FPMachine, model);
                
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
        public ActionResult Edit(FPMachineViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var FPMachine = FPMachineRepository.GetFPMachineById(model.Id);
                    AutoMapper.Mapper.Map(model, FPMachine);
                    FPMachine.ModifiedUserId = WebSecurity.CurrentUserId;
                    FPMachine.ModifiedDate = DateTime.Now;
                    FPMachineRepository.UpdateFPMachine(FPMachine);

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
            var FPMachine = FPMachineRepository.GetFPMachineById(Id.Value);
            if (FPMachine != null && FPMachine.IsDeleted != true)
            {
                var model = new FPMachineViewModel();
                AutoMapper.Mapper.Map(FPMachine, model);
                
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
                    var item = FPMachineRepository.GetFPMachineById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if(item != null)
                    {
                        //if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        //{
                        //    TempData["FailedMessage"] = "NotOwner";
                        //    return RedirectToAction("Index");
                        //}

                        item.IsDeleted = true;
                        FPMachineRepository.UpdateFPMachine(item);
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
