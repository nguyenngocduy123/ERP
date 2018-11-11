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
    public class BankController : Controller
    {
        private readonly IBankRepository BankRepository;
        private readonly IUserRepository userRepository;

        public BankController(
            IBankRepository _Bank
            , IUserRepository _user
            )
        {
            BankRepository = _Bank;
            userRepository = _user;
        }

        #region Index

        public ViewResult Index(int? StaffId)
        {
            ViewBag.StaffId = StaffId;
            IQueryable<BankViewModel> q = BankRepository.GetAllBank().Where(x => x.StaffId == StaffId)
                .Select(item => new BankViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    NameBank = item.NameBank,
                    BranchName = item.BranchName,
                    CodeBank = item.CodeBank,
                    StaffId = item.StaffId
                }).OrderByDescending(m => m.ModifiedDate);
            ViewBag.AccessRightCreate = SecurityFilter.AccessRight("Create", "Bank", "Staff");
            return View(q);
        }
        #endregion

        #region Create
        public ViewResult Create(int? Id)
        {
            var model = new BankViewModel();
            model.BankList = Helpers.SelectListHelper.GetSelectList_Category("bank", null, "Name",App_GlobalResources.Wording.Empty);
            model.StaffId = Id;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(BankViewModel model)
        {
            if (ModelState.IsValid)
            {
                var Bank = new Domain.Staff.Entities.Bank();
                AutoMapper.Mapper.Map(model, Bank);
                Bank.IsDeleted = false;
                Bank.CreatedUserId = WebSecurity.CurrentUserId;
                Bank.ModifiedUserId = WebSecurity.CurrentUserId;
                Bank.CreatedDate = DateTime.Now;
                Bank.ModifiedDate = DateTime.Now;
                BankRepository.InsertBank(Bank);

                if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                {
                    return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
                }
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var Bank = BankRepository.GetBankById(Id.Value);
            if (Bank != null && Bank.IsDeleted != true)
            {
                var model = new BankViewModel();
                AutoMapper.Mapper.Map(Bank, model);
                model.BankList = Helpers.SelectListHelper.GetSelectList_Category("bank", model.NameBank, "Name", App_GlobalResources.Wording.Empty);
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
        public ActionResult Edit(BankViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var Bank = BankRepository.GetBankById(model.Id);
                    AutoMapper.Mapper.Map(model, Bank);
                    Bank.ModifiedUserId = WebSecurity.CurrentUserId;
                    Bank.ModifiedDate = DateTime.Now;
                    BankRepository.UpdateBank(Bank);

                    if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                    {
                        return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
                    }
                }

                return View(model);
            }

            return View(model);
        }

        #endregion


        #region Delete
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            try
            {

                var item = BankRepository.GetBankById(id.Value);
                if (item != null)
                {
                    //if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                    //{
                    //    TempData["FailedMessage"] = "NotOwner";
                    //    return RedirectToAction("Index");
                    //}

                    item.IsDeleted = true;
                    BankRepository.UpdateBank(item);
                }
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.DeleteSuccess;
                return RedirectToAction("Detail", "Staffs", new { area = "Staff", Id = item.StaffId });
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
