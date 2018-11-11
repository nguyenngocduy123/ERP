using System.Data.Entity.Infrastructure;
using System.Globalization;
using Erp.BackOffice.Administration.Models;
using Erp.BackOffice.App_GlobalResources;
using Erp.BackOffice.Filters;
using Erp.Domain.Entities;
using Erp.Domain.Interfaces;
using System.Linq;
using System.Web.Mvc;
using Erp.Utilities;
using System.Collections.Generic;

namespace Erp.BackOffice.Administration.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class UserTypeController : Controller
    {
        private readonly IUserTypeRepository _userTypeRepository;
        public UserTypeController(IUserTypeRepository userType)
        {
            _userTypeRepository = userType;
        }

        #region Index Action
        public ViewResult Index(string txtSearch)
        {
            var userTypes = _userTypeRepository.GetUserTypes();
            var model = new ListUsersTypeModel { UserTypes = userTypes };
            if (!string.IsNullOrEmpty(txtSearch))
            {
                model.UserTypes = userTypes.Where(m => m.Name.ToUpper().Contains(txtSearch.ToUpper()));
            }
            ViewBag.SuccessMessage = TempData[Globals.SuccessMessageKey];
            ViewBag.FailedMessage = TempData[Globals.FailedMessageKey];
            return View(model);
        }
        #endregion

        #region Edit Action
        public ActionResult EditUserType(int userTypeId)
        {
            List<SelectListItem> lstScope = new List<SelectListItem>
            {
                        new SelectListItem(),
                        new SelectListItem() {Text = "Internal", Value="false"},
                        new SelectListItem() {Text = "External", Value="true"}
            };
            
            var editUserType = new EditUserTypeModel();
            var userType = _userTypeRepository.GetUserTypeById(userTypeId);
            if (userType != null)
            {
                AutoMapper.Mapper.Map(userType, editUserType);
                
                ViewBag.Scope = new SelectList(lstScope, "Value", "Text", (editUserType.Scope.HasValue == true) ? ((editUserType.Scope == true) ? "true" : "false") : null);

                return View("EditUserType", editUserType);
            }

            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditUserType(EditUserTypeModel model)
        {
            List<SelectListItem> lstScope = new List<SelectListItem>
            {
                        new SelectListItem(),
                        new SelectListItem() {Text = "Internal", Value=false.ToString()},
                        new SelectListItem() {Text = "External", Value=true.ToString()}
            };
            ViewBag.Scope = new SelectList(lstScope, "Value", "Text");
            if (ModelState.IsValid)
            {
                if (Request["Submit"]=="Save")
                {
                    var userType = _userTypeRepository.GetUserTypeById(model.Id);
                    AutoMapper.Mapper.Map(model, userType);
                    _userTypeRepository.Save();
                    TempData[Globals.SuccessMessageKey] = Wording.UpdateSuccess;
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            TempData[Globals.FailedMessageKey] = Error.UpdateUnsuccess;
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }
        #endregion

        #region Delete Action
        [HttpPost]
        public ActionResult DeleteUserType()
        {
            if (ModelState.IsValid)
            {
                int deleteUserTypeId = int.Parse(Request["DeleteUserTypeId"], CultureInfo.InvariantCulture);
                try
                {
                    _userTypeRepository.DeleteUserType(deleteUserTypeId);
                    TempData[Globals.SuccessMessageKey] = Wording.DeleteSuccess;
                }
                catch(DbUpdateException)
                {
                    TempData[Globals.FailedMessageKey] = Error.DeletingError;
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
            
        }

        [HttpPost]
        public ActionResult DeleteAll()
        {
            try
            {
                string idDeleteAll = Request["DeleteAll-checkbox"];
                string[] arrDeleteId = idDeleteAll.Split(',');
                for (int i = 0; i < arrDeleteId.Count(); i++)
                {
                    _userTypeRepository.DeleteUserType(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                }
                TempData[Globals.SuccessMessageKey] = Wording.DeleteSuccess;
                return RedirectToAction("Index");
            }
            catch(DbUpdateException)
            {
                TempData[Globals.FailedMessageKey] = Error.RelationError;
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region Create Action
        public ActionResult AddUserType()
        {
            List<SelectListItem> lstScope = new List<SelectListItem>
            {
                        new SelectListItem(),
                        new SelectListItem() {Text = "Internal", Value="false"},
                        new SelectListItem() {Text = "External", Value="true"}
            };
            ViewBag.Scope = new SelectList(lstScope, "Value", "Text");
            return View();
        }

        [HttpPost]
        public ActionResult AddUserType(EditUserTypeModel model)
        {
            List<SelectListItem> lstScope = new List<SelectListItem>
            {
                        new SelectListItem(),
                          new SelectListItem() {Text = "Internal", Value="false"},
                        new SelectListItem() {Text = "External", Value="true"}
            };
            ViewBag.Scope = new SelectList(lstScope, "Value", "Text");
            if (ModelState.IsValid)
            {
                if (Request["Submit"]=="Save")
                {
                    var userType = new UserType();
                    AutoMapper.Mapper.Map(model, userType);
                    _userTypeRepository.InsertUserType(userType);
                    TempData[Globals.SuccessMessageKey] = Wording.InsertSuccess;
                    return RedirectToAction("Index");
                }
                if (Request["Cancel"]=="Cancel")
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("AddUserType");
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
