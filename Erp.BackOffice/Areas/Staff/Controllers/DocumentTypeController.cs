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
    public class DocumentTypeController : Controller
    {
        private readonly IDocumentTypeRepository DocumentTypeRepository;
        private readonly IUserRepository userRepository;

        public DocumentTypeController(
            IDocumentTypeRepository _DocumentType
            , IUserRepository _user
            )
        {
            DocumentTypeRepository = _DocumentType;
            userRepository = _user;
        }

        #region Index

        public ViewResult Index(string txtSearch)
        {

            IQueryable<DocumentTypeViewModel> q = DocumentTypeRepository.GetAllDocumentType()
                .Select(item => new DocumentTypeViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name
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
            var model = new DocumentTypeViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(DocumentTypeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var DocumentType = new DocumentType();
                AutoMapper.Mapper.Map(model, DocumentType);
                DocumentType.IsDeleted = false;
                DocumentType.CreatedUserId = WebSecurity.CurrentUserId;
                DocumentType.ModifiedUserId = WebSecurity.CurrentUserId;
                DocumentType.AssignedUserId = WebSecurity.CurrentUserId;
                DocumentType.CreatedDate = DateTime.Now;
                DocumentType.ModifiedDate = DateTime.Now;
                DocumentTypeRepository.InsertDocumentType(DocumentType);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var DocumentType = DocumentTypeRepository.GetDocumentTypeById(Id.Value);
            if (DocumentType != null && DocumentType.IsDeleted != true)
            {
                var model = new DocumentTypeViewModel();
                AutoMapper.Mapper.Map(DocumentType, model);
                
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
        public ActionResult Edit(DocumentTypeViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var DocumentType = DocumentTypeRepository.GetDocumentTypeById(model.Id);
                    AutoMapper.Mapper.Map(model, DocumentType);
                    DocumentType.ModifiedUserId = WebSecurity.CurrentUserId;
                    DocumentType.ModifiedDate = DateTime.Now;
                    DocumentTypeRepository.UpdateDocumentType(DocumentType);

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
                    var item = DocumentTypeRepository.GetDocumentTypeById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if(item != null)
                    {
                        //if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                        //{
                        //    TempData["FailedMessage"] = "NotOwner";
                        //    return RedirectToAction("Index");
                        //}

                        item.IsDeleted = true;
                        DocumentTypeRepository.UpdateDocumentType(item);
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
