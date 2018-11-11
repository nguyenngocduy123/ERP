using Erp.BackOffice.Filters;
using Erp.BackOffice.Staff.Models;
using Erp.Domain.Interfaces;
using Erp.Domain.Staff.Interfaces;
using System.Linq;
using System.Web.Mvc;

namespace Erp.BackOffice.Staff.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class LogDocumentAttributeController : Controller
    {
        private readonly ILogDocumentAttributeRepository LogDocumentAttributeRepository;
        private readonly IUserRepository userRepository;

        public LogDocumentAttributeController(
            ILogDocumentAttributeRepository _LogDocumentAttribute
            , IUserRepository _user
            )
        {
            LogDocumentAttributeRepository = _LogDocumentAttribute;
            userRepository = _user;
        }

        #region Index

        public ViewResult Index(int? Id)
        {

            IQueryable<LogDocumentAttributeViewModel> q = LogDocumentAttributeRepository.GetAllvwLogDocumentAttribute().Where(x=>x.DocumentAttributeId==Id)
                .Select(item => new LogDocumentAttributeViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    FullName = item.FullName,
                    DocumentAttributeId=item.DocumentAttributeId,
                    UserId=item.UserId,
                    UserName=item.UserName
                }).OrderByDescending(m => m.ModifiedDate);
            return View(q);
        }
        #endregion

        //#region Create
        //public ViewResult Create()
        //{
        //    var model = new LogDocumentAttributeViewModel();

        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult Create(LogDocumentAttributeViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var LogDocumentAttribute = new LogDocumentAttribute();
        //        AutoMapper.Mapper.Map(model, LogDocumentAttribute);
        //        LogDocumentAttribute.IsDeleted = false;
        //        LogDocumentAttribute.CreatedUserId = WebSecurity.CurrentUserId;
        //        LogDocumentAttribute.ModifiedUserId = WebSecurity.CurrentUserId;
        //        LogDocumentAttribute.AssignedUserId = WebSecurity.CurrentUserId;
        //        LogDocumentAttribute.CreatedDate = DateTime.Now;
        //        LogDocumentAttribute.ModifiedDate = DateTime.Now;
        //        LogDocumentAttributeRepository.InsertLogDocumentAttribute(LogDocumentAttribute);

        //        TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
        //        return RedirectToAction("Index");
        //    }
        //    return View(model);
        //}
        //#endregion

        //#region Edit
        //public ActionResult Edit(int? Id)
        //{
        //    var LogDocumentAttribute = LogDocumentAttributeRepository.GetLogDocumentAttributeById(Id.Value);
        //    if (LogDocumentAttribute != null && LogDocumentAttribute.IsDeleted != true)
        //    {
        //        var model = new LogDocumentAttributeViewModel();
        //        AutoMapper.Mapper.Map(LogDocumentAttribute, model);
                
        //        if (model.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
        //        {
        //            TempData["FailedMessage"] = "NotOwner";
        //            return RedirectToAction("Index");
        //        }                

        //        return View(model);
        //    }
        //    if (Request.UrlReferrer != null)
        //        return Redirect(Request.UrlReferrer.AbsoluteUri);
        //    return RedirectToAction("Index");
        //}

        //[HttpPost]
        //public ActionResult Edit(LogDocumentAttributeViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (Request["Submit"] == "Save")
        //        {
        //            var LogDocumentAttribute = LogDocumentAttributeRepository.GetLogDocumentAttributeById(model.Id);
        //            AutoMapper.Mapper.Map(model, LogDocumentAttribute);
        //            LogDocumentAttribute.ModifiedUserId = WebSecurity.CurrentUserId;
        //            LogDocumentAttribute.ModifiedDate = DateTime.Now;
        //            LogDocumentAttributeRepository.UpdateLogDocumentAttribute(LogDocumentAttribute);

        //            TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
        //            return RedirectToAction("Index");
        //        }

        //        return View(model);
        //    }

        //    return View(model);

        //    //if (Request.UrlReferrer != null)
        //    //    return Redirect(Request.UrlReferrer.AbsoluteUri);
        //    //return RedirectToAction("Index");
        //}

        //#endregion

        //#region Detail
        //public ActionResult Detail(int? Id)
        //{
        //    var LogDocumentAttribute = LogDocumentAttributeRepository.GetLogDocumentAttributeById(Id.Value);
        //    if (LogDocumentAttribute != null && LogDocumentAttribute.IsDeleted != true)
        //    {
        //        var model = new LogDocumentAttributeViewModel();
        //        AutoMapper.Mapper.Map(LogDocumentAttribute, model);
                
        //        if (model.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
        //        {
        //            TempData["FailedMessage"] = "NotOwner";
        //            return RedirectToAction("Index");
        //        }                

        //        return View(model);
        //    }
        //    if (Request.UrlReferrer != null)
        //        return Redirect(Request.UrlReferrer.AbsoluteUri);
        //    return RedirectToAction("Index");
        //}
        //#endregion

        //#region Delete
        //[HttpPost]
        //public ActionResult Delete()
        //{
        //    try
        //    {
        //        string idDeleteAll = Request["DeleteId-checkbox"];
        //        string[] arrDeleteId = idDeleteAll.Split(',');
        //        for (int i = 0; i < arrDeleteId.Count(); i++)
        //        {
        //            var item = LogDocumentAttributeRepository.GetLogDocumentAttributeById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
        //            if(item != null)
        //            {
        //                if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
        //                {
        //                    TempData["FailedMessage"] = "NotOwner";
        //                    return RedirectToAction("Index");
        //                }

        //                item.IsDeleted = true;
        //                LogDocumentAttributeRepository.UpdateLogDocumentAttribute(item);
        //            }
        //        }
        //        TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.DeleteSuccess;
        //        return RedirectToAction("Index");
        //    }
        //    catch (DbUpdateException)
        //    {
        //        TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.RelationError;
        //        return RedirectToAction("Index");
        //    }
        //}
        //#endregion
    }
}
