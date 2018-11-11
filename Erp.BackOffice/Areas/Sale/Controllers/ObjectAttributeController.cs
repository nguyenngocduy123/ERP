using System.Globalization;
using Erp.BackOffice.Sale.Models;
using Erp.BackOffice.Filters;
using Erp.Domain.Sale.Entities;
using Erp.Domain.Interfaces;
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
using System.Web.Script.Serialization;
using Erp.Domain.Sale.Repositories;

namespace Erp.BackOffice.Sale.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class ObjectAttributeController : Controller
    {
        private readonly IObjectAttributeRepository ObjectAttributeRepository;
        private readonly IUserRepository userRepository;

        public ObjectAttributeController(
            IObjectAttributeRepository _ObjectAttribute
            , IUserRepository _user
            )
        {
            ObjectAttributeRepository = _ObjectAttribute;
            userRepository = _user;
        }

        #region Index

        public ViewResult Index(string txtSearch)
        {

            IQueryable<ObjectAttributeViewModel> q = ObjectAttributeRepository.GetAllObjectAttribute()
                .Select(item => new ObjectAttributeViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    DataType = item.DataType,
                    IsSelected = item.IsSelected,
                    ModuleCategoryType = item.ModuleCategoryType,
                    ModuleType = item.ModuleType,
                    OrderNo = item.OrderNo
                }).OrderBy(m => m.ModuleType).ThenBy(m => m.OrderNo);

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        #endregion

        #region Create
        public ViewResult Create()
        {
            var model = new ObjectAttributeViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ObjectAttributeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var ObjectAttribute = new Domain.Sale.Entities.ObjectAttribute();
                AutoMapper.Mapper.Map(model, ObjectAttribute);
                ObjectAttribute.IsDeleted = false;
                ObjectAttribute.CreatedUserId = WebSecurity.CurrentUserId;
                ObjectAttribute.ModifiedUserId = WebSecurity.CurrentUserId;
                ObjectAttribute.CreatedDate = DateTime.Now;
                ObjectAttribute.ModifiedDate = DateTime.Now;
                ObjectAttributeRepository.InsertObjectAttribute(ObjectAttribute);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var ObjectAttribute = ObjectAttributeRepository.GetObjectAttributeById(Id.Value);
            if (ObjectAttribute != null && ObjectAttribute.IsDeleted != true)
            {
                var model = new ObjectAttributeViewModel();
                AutoMapper.Mapper.Map(ObjectAttribute, model);
                
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
        public ActionResult Edit(ObjectAttributeViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var ObjectAttribute = ObjectAttributeRepository.GetObjectAttributeById(model.Id);
                    AutoMapper.Mapper.Map(model, ObjectAttribute);
                    ObjectAttribute.ModifiedUserId = WebSecurity.CurrentUserId;
                    ObjectAttribute.ModifiedDate = DateTime.Now;
                    ObjectAttributeRepository.UpdateObjectAttribute(ObjectAttribute);

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
                    var item = ObjectAttributeRepository.GetObjectAttributeById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if(item != null)
                    {
                        //if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                        //{
                        //    TempData["FailedMessage"] = "NotOwner";
                        //    return RedirectToAction("Index");
                        //}

                        var listObjAattrValue = ObjectAttributeRepository.GetAllObjectAttributeValueByAttributeId(item.Id).ToList();
                        foreach (var oav in listObjAattrValue)
                        {
                            oav.IsDeleted = true;
                            ObjectAttributeRepository.UpdateObjectAttributeValue(oav);
                        }

                        item.IsDeleted = true;
                        ObjectAttributeRepository.UpdateObjectAttribute(item);
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

        #region - Attibite Value -
        public ViewResult GetListByModuleCategory(string category, int? objectId, string colum = "col-sm-6")
        {
            var list = ObjectAttributeRepository.GetAllObjectAttributeByModuleCategoryType(category).OrderBy(x => x.OrderNo).AsEnumerable()
                .Select(item => new ObjectAttributeValueViewModel { 
                    AttributeId = item.Id,
                    DataType = SwitchDataType(item.DataType),
                    AttributeName = item.Name,
                    ObjectId = objectId
                }).ToList();

            if (objectId != null)
            {
                var listValue = ObjectAttributeRepository.GetAllObjectAttributeValueByObjectId(objectId.Value).ToList();
                foreach(var attr in list)
                {
                    var valueOf = listValue.Where(x => x.AttributeId == attr.AttributeId).FirstOrDefault();
                    if (valueOf != null)
                    {
                        attr.Value = valueOf.Value;
                    }
                }
            }
            ViewBag.colum = colum;
            return View("AttributeList", list);
        }
        public ViewResult GetListByModule(string module, int? objectId, string colum, string disabled)
        {
            var list = ObjectAttributeRepository.GetAllObjectAttributeByModuleType(module).OrderBy(x => x.OrderNo).AsEnumerable()
                .Select(item => new ObjectAttributeValueViewModel
                {
                    AttributeId = item.Id, ObjectId = objectId,
                    DataType = SwitchDataType(item.DataType),
                    AttributeName = item.Name
                }).ToList();

            if (objectId != null)
            {
                var listValue = ObjectAttributeRepository.GetAllObjectAttributeValueByObjectId(objectId.Value).ToList();
                foreach (var attr in list)
                {
                    var valueOf = listValue.Where(x => x.AttributeId == attr.AttributeId).FirstOrDefault();
                    if (valueOf != null)
                    {
                        attr.Value = valueOf.Value;
                    }
                }
            }
            if (colum == null)
            {
                ViewBag.colum = "col-sm-6";
            }
            else
            {
                ViewBag.colum = colum;
            }
            if (disabled == null)
            {
                ViewBag.disabled = false;
            }
            else
            {
                ViewBag.disabled = true;
            }
            return View("AttributeList", list);
        }

        public static void CreateOrUpdateForObject(int objectId, List<ObjectAttributeValueViewModel> attributeValueList)
        {
            ObjectAttributeRepository ObjectAttributeRepository = new ObjectAttributeRepository(new Domain.Sale.ErpSaleDbContext());

            if (attributeValueList != null)
            {
                // nếu đối tượng là tạo mới thì danh sách listValue sẽ rỗng
                var listValue = ObjectAttributeRepository.GetAllObjectAttributeValueByObjectId(objectId).ToList();

                var listEmpty = attributeValueList.Where(x => x.Value == null || x.Value == "").ToList();

                foreach (var attrValue in attributeValueList.Where(x => x.Value != null && x.Value != ""))
                {
                    var valueOf = listValue.Where(x => x.AttributeId == attrValue.AttributeId).FirstOrDefault();
                    if (valueOf != null)
                    {
                        valueOf.Value = attrValue.Value;
                        valueOf.ModifiedDate = DateTime.Now;
                        valueOf.ModifiedUserId = WebSecurity.CurrentUserId;
                        ObjectAttributeRepository.UpdateObjectAttributeValue(valueOf);
                    }
                    else
                    {
                        valueOf = new ObjectAttributeValue
                        {
                            AttributeId = attrValue.AttributeId,
                            ObjectId = objectId,
                            IsDeleted = false,
                            Value = attrValue.Value,
                            CreatedDate = DateTime.Now,
                            CreatedUserId = WebSecurity.CurrentUserId,
                            ModifiedDate = DateTime.Now,
                            ModifiedUserId = WebSecurity.CurrentUserId,
                        };
                        ObjectAttributeRepository.InsertObjectAttributeValue(valueOf);
                    }
                }

                //xóa những đặc tính cũ đã không còn giá trị nữa
                foreach (var item in listEmpty)
                {
                    var valueOf = listValue.Where(x => x.AttributeId == item.AttributeId).FirstOrDefault();
                    if (valueOf != null)
                    {
                        ObjectAttributeRepository.DeleteObjectAttributeValue(valueOf.Id);
                    }
                }
            }
        }

        public ViewResult SidebarSearch(string module, string listField)
        {
            IEnumerable<ObjectAttributeViewModel> list = ObjectAttributeRepository.GetAllObjectAttributeByModuleType(module).OrderBy(x => x.OrderNo)
               .Select(item => new ObjectAttributeViewModel
               {
                   Id = item.Id,
                   DataType = item.DataType,
                   Name = item.Name,
                   OrderNo = item.OrderNo
               }).ToList();

            var listFieldInput = new List<ObjectAttributeViewModel>();
            if (string.IsNullOrEmpty(listField) == false)
                listFieldInput = new JavaScriptSerializer().Deserialize<List<ObjectAttributeViewModel>>(listField);

            ViewBag.listFieldInput = listFieldInput;

            return View(list);
        }

        string SwitchDataType(string dataType)
        {
            string type = string.Empty;
            switch (dataType)
            {
                case "string":
                    type = "text";
                    break;
                case "int":
                case "double":
                    type = "number";
                    break;
                case "datetime":
                    type = "date";
                    break;
                default:
                    type = "text";
                    break;
            }
            return type;
        }
        #endregion
    }
}
