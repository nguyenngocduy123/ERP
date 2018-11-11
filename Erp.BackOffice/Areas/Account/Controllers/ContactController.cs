using System.Globalization;
using Erp.BackOffice.Account.Models;
using Erp.BackOffice.Filters;
using Erp.Domain.Entities;
using Erp.Domain.Interfaces;
using Erp.Domain.Account.Entities;
using Erp.Domain.Account.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Erp.Utilities;
using WebMatrix.WebData;
using Erp.BackOffice.Helpers;
using Erp.Domain.Sale.Interfaces;

namespace Erp.BackOffice.Account.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class ContactController : Controller
    {
        private readonly IContactRepository ContactRepository;
        private readonly IUserRepository userRepository;

        public ContactController(
            IContactRepository _Contact
            , IUserRepository _user
            )
        {
            ContactRepository = _Contact;
            userRepository = _user;
        }

        #region Index

        public ViewResult Index(string txtName, string txtPhone, int? customerId, int? supplierId)
        {
            IEnumerable<ContactViewModel> q = ContactRepository.GetAllContact()
                .Select(item => new ContactViewModel
                {
                    Id = item.Id,
                    Address = item.Address,
                    Birthday = item.Birthday,
                    CityId = item.CityId,
                    CustomerId = item.CustomerId,
                    DistrictId = item.DistrictId,
                    Email = item.Email,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Gender = item.Gender,
                    Mobile = item.Mobile,
                    Note = item.Note,
                    Phone = item.Phone,
                    WardId = item.WardId,
                    CreatedDate = item.CreatedDate,
                    ModifiedDate = item.ModifiedDate
                }).AsEnumerable().Where(item => (item.CustomerId == customerId || customerId == null) && (item.SupplierId == supplierId || supplierId == null))
                .OrderByDescending(m => m.ModifiedDate);

            if (string.IsNullOrEmpty(txtName) == false || string.IsNullOrEmpty(txtPhone) == false)
            {
                txtName = txtName == "" ? "~" : Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(txtName);
                txtPhone = txtPhone == "" ? "~" : txtPhone.ToLower();
                q = q.Where(x => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(x.LastName + " " + x.FirstName).Contains(txtName) || x.Phone.ToLower().Contains(txtPhone));
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
            var model = new ContactViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                var Contact = new Domain.Account.Entities.Contact();
                AutoMapper.Mapper.Map(model, Contact);
                Contact.IsDeleted = false;
                Contact.CreatedUserId = WebSecurity.CurrentUserId;
                Contact.ModifiedUserId = WebSecurity.CurrentUserId;
                Contact.CreatedDate = DateTime.Now;
                Contact.ModifiedDate = DateTime.Now;
                ContactRepository.InsertContact(Contact);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var Contact = ContactRepository.GetContactById(Id.Value);
            if (Contact != null && Contact.IsDeleted != true)
            {
                var model = new ContactViewModel();
                AutoMapper.Mapper.Map(Contact, model);

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
        public ActionResult Edit(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var Contact = ContactRepository.GetContactById(model.Id);
                    AutoMapper.Mapper.Map(model, Contact);
                    Contact.ModifiedUserId = WebSecurity.CurrentUserId;
                    Contact.ModifiedDate = DateTime.Now;
                    ContactRepository.UpdateContact(Contact);

                    if(string.IsNullOrEmpty(Request["IsPopup"]) == false)
                    {
                        return RedirectToAction("Edit", new { Id = model.Id, IsPopup = "true"  });
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
            var Contact = ContactRepository.GetContactById(Id.Value);
            if (Contact != null && Contact.IsDeleted != true)
            {
                var model = new ContactViewModel();
                AutoMapper.Mapper.Map(Contact, model);

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
                    var item = ContactRepository.GetContactById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        //if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                        //{
                        //    TempData["FailedMessage"] = "NotOwner";
                        //    return RedirectToAction("Index");
                        //}

                        item.IsDeleted = true;
                        ContactRepository.UpdateContact(item);
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

        #region Contact

        public ActionResult ContactList(int? CustomerId, int? SupplierId)
        {
            IQueryable<ContactViewModel> contactList = ContactRepository.GetAllContact().Where(x => (x.SupplierId == SupplierId && x.SupplierId != null ) || (x.CustomerId == CustomerId && x.CustomerId != null))
                .Select(item => new ContactViewModel
            {
                Id = item.Id,
                Address = item.Address,
                Birthday = item.Birthday,
                CityId = item.CityId,
                CustomerId = item.CustomerId,
                DistrictId = item.DistrictId,
                Email = item.Email,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Gender = item.Gender,
                Mobile = item.Mobile,
                Note = item.Note,
                Phone = item.Phone,
                WardId = item.WardId,
                CreatedDate = item.CreatedDate,
                ModifiedDate = item.ModifiedDate,
                Position = item.Position,
                DepartmentName = item.DepartmentName
            }).OrderBy(x => x.CreatedDate);
           
            ViewBag.CustomerId = CustomerId;
            ViewBag.SupplierId = SupplierId;
            return View(contactList);
        }

        public ActionResult ContactCreate(int? CustomerId, int? SupplierId)
        {
            var model = new ContactViewModel();
            model.CustomerId = CustomerId;
            model.SupplierId = SupplierId;
            return View(model);
        }
        [HttpPost]
        public ActionResult ContactCreate(ContactViewModel model)
        {
            var contact = new Domain.Account.Entities.Contact();
            AutoMapper.Mapper.Map(model, contact);

            contact.IsDeleted = false;
            contact.CreatedDate = DateTime.Now;
            contact.CreatedUserId = WebSecurity.CurrentUserId;
            ContactRepository.InsertContact(contact);

            if (Request["IsPopup"] == "true")
            {
                ViewBag.closePopup = "close and append to page parent";
                model.FullName = model.LastName + " " + model.FirstName;
                model.Id = contact.Id;
                return View(model);
            }

            return RedirectToAction("ContactList", new { customerId = model.CustomerId, SupplierId = model.SupplierId });
        }

        public ActionResult ContactEdit(int? Id)
        {
            var contact = ContactRepository.GetContactById(Id.Value);
            var model = new ContactViewModel();
            AutoMapper.Mapper.Map(contact, model);

            return View("ContactCreate", model);
        }
        [HttpPost]
        public ActionResult ContactEdit(ContactViewModel model)
        {
            var contact = ContactRepository.GetContactById(model.Id);
            AutoMapper.Mapper.Map(model, contact);

            contact.ModifiedUserId = WebSecurity.CurrentUserId;
            contact.ModifiedDate = DateTime.Now;
            ContactRepository.UpdateContact(contact);

            if (Request["IsPopup"] == "true")
            {
                ViewBag.closePopup = "close and append to page parent";
                model.FullName = model.LastName + " " + model.FirstName;
                return View("ContactCreate", model);
            }

            return RedirectToAction("ContactList", new { customerId = model.CustomerId, SupplierId = model.SupplierId });
        }

        [HttpPost]
        public ActionResult ContactDelete(int? id, string IsAjax)
        {
            try
            {
                int customerId = 0;

                var item = ContactRepository.GetContactById(id.Value);

                if (item != null)
                {
                    customerId = item.CustomerId.Value;

                    //if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                    //{
                    //    TempData["FailedMessage"] = "NotOwner";
                    //    return RedirectToAction("Index");
                    //}

                    item.IsDeleted = true;
                    ContactRepository.UpdateContact(item);
                }


                if (string.IsNullOrEmpty(IsAjax) == false)
                {
                    return Content("success");
                }

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.DeleteSuccess;
                return RedirectToAction("Edit", new { Id = customerId });
            }
            catch (DbUpdateException)
            {
                TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.RelationError;
                return RedirectToAction("Index");
            }
        }

        public JsonResult GetContactListByCustomerId(int? customerId)
        {
            if (customerId == null)
                return Json(new List<int>(), JsonRequestBehavior.AllowGet);

            var list = ContactRepository.GetAllContactByCustomerId(customerId.Value);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}
