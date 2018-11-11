using System.Globalization;
using Erp.BackOffice.Sale.Models;
using Erp.BackOffice.Filters;
using Erp.Domain.Entities;
using Erp.Domain.Interfaces;
using Erp.Domain.Sale.Interfaces;
using Erp.Domain.Sale.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Erp.Utilities;
using WebMatrix.WebData;
using Erp.BackOffice.Helpers;
using Erp.Domain.Sale.Entities;
using System.Text.RegularExpressions;

namespace Erp.BackOffice.Sale.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class SupplierController : Controller
    {
        private readonly ISupplierRepository SupplierRepository;
        private readonly IProductOrServiceRepository ProductRepository;
        private readonly IUserRepository userRepository;

        public SupplierController(
            ISupplierRepository _Supplier
            , IProductOrServiceRepository _Product
            , IUserRepository _user
            )
        {
            SupplierRepository = _Supplier;
            ProductRepository = _Product;
            userRepository = _user;

        }

        #region Index

        public ViewResult Index(string txtSearch, string txtCode, string txtPhone, string txtEmail)
        {
            IEnumerable<SupplierViewModel> q = SupplierRepository.GetAllSupplier()
                .Select(item => new SupplierViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    Code = item.Code,
                    CompanyName = item.CompanyName,
                    Address = item.Address,
                    Email = item.Email,
                    Phone = item.Phone

                }).OrderByDescending(m => m.ModifiedDate);

            if (string.IsNullOrEmpty(txtSearch) == false || string.IsNullOrEmpty(txtCode) == false || string.IsNullOrEmpty(txtPhone) == false || string.IsNullOrEmpty(txtEmail) == false)
            {
                txtSearch = txtSearch == "" ? "~" : Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(txtSearch);
                txtCode = txtCode == "" ? "~" : txtCode.ToLower();
                txtPhone = txtPhone == "" ? "~" : txtPhone.ToLower();
                txtEmail = txtEmail == "" ? "~" : txtEmail.ToLower();

                q = q.Where(x => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(x.Name).Contains(txtSearch) || x.Code.ToLowerOrEmpty().Contains(txtCode) || x.Phone.ToLowerOrEmpty().Contains(txtPhone) || x.Email.ToLowerOrEmpty().Contains(txtEmail));
            }

            //if (string.IsNullOrEmpty(txtCode) == false)
            //{
            //    txtCode = txtCode.ToLower();
            //    q = q.Where(x => x.Code.ToLower().Contains(txtCode));
            //}

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        #endregion

        #region Create
        public ViewResult Create()
        {
            var model = new SupplierViewModel();
            var productList = ProductRepository.GetAllProductByType("product")
                .Select(item => new ProductViewModel
                {
                    Name = item.Name,
                    Id = item.Id,
                    CategoryCode = item.CategoryCode
                }).ToList();

            ViewBag.productList = productList;           

            model.Code = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_Supplier");

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(SupplierViewModel model)
        {
            if (ModelState.IsValid)
            {
                var Supplier = new Domain.Sale.Entities.Supplier();
                AutoMapper.Mapper.Map(model, Supplier);
                Supplier.IsDeleted = false;
                Supplier.CreatedUserId = WebSecurity.CurrentUserId;
                Supplier.ModifiedUserId = WebSecurity.CurrentUserId;
                Supplier.CreatedDate = DateTime.Now;
                Supplier.ModifiedDate = DateTime.Now;
                Supplier.ProductIdOfSupplier = string.IsNullOrEmpty(Request["ProductIdOfSupplier"]) == true ? "" : Request["ProductIdOfSupplier"] ;

                SupplierRepository.InsertSupplier(Supplier);


                string prefix = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_Supplier");
                string code = Regex.Replace(model.Code, prefix, "");

                if (string.IsNullOrEmpty(code) == true)
                {
                    Supplier.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix, Supplier.Id, 3);
                    SupplierRepository.UpdateSupplier(Supplier);
                }


                //tạo đặc tính động cho sản phẩm nếu có danh sách đặc tính động truyền vào và đặc tính đó phải có giá trị
                ObjectAttributeController.CreateOrUpdateForObject(Supplier.Id, model.AttributeValueList);

                if (Request["IsPopup"] == "true")
                {
                    ViewBag.closePopup = "close and append to page parent";
                    model.Id = Supplier.Id;
                    return View(model);
                }

                
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var Supplier = SupplierRepository.GetSupplierById(Id.Value);
            if (Supplier != null && Supplier.IsDeleted != true)
            {
                var model = new SupplierViewModel();
                AutoMapper.Mapper.Map(Supplier, model);

                //if (model.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                //{
                //    TempData["FailedMessage"] = "NotOwner";
                //    return RedirectToAction("Index");
                //}

                var productList = ProductRepository.GetAllProductByType("product")
                .Select(item => new ProductViewModel
                {
                    Name = item.Name,
                    Id = item.Id,
                    CategoryCode = item.CategoryCode
                }).ToList();
                ViewBag.productList = productList;


                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(SupplierViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var Supplier = SupplierRepository.GetSupplierById(model.Id);
                    AutoMapper.Mapper.Map(model, Supplier);
                    Supplier.ModifiedUserId = WebSecurity.CurrentUserId;
                    Supplier.ModifiedDate = DateTime.Now;
                    Supplier.ProductIdOfSupplier = Request["ProductIdOfSupplier"];

                    //tạo đặc tính động cho sản phẩm nếu có danh sách đặc tính động truyền vào và đặc tính đó phải có giá trị
                    ObjectAttributeController.CreateOrUpdateForObject(Supplier.Id, model.AttributeValueList);

                    SupplierRepository.UpdateSupplier(Supplier);

                    if (string.IsNullOrEmpty(Request["IsPopup"]) == false)
                    {
                        return RedirectToAction("Edit", new { Id = model.Id, IsPopup = "True" });
                    }

                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                    return RedirectToAction("Index");
                }

                return View(model);
            }

            return View(model);
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
                    var item = SupplierRepository.GetSupplierById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        //if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                        //{
                        //    TempData["FailedMessage"] = "NotOwner";
                        //    return RedirectToAction("Index");
                        //}

                        item.IsDeleted = true;
                        SupplierRepository.UpdateSupplier(item);
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


        #region  - Json -
        public JsonResult GetListProductJsonBySupplierId(int? supplierId)
        {
            if (supplierId == null)
                return Json(new List<int>(), JsonRequestBehavior.AllowGet);


            var supplier =  SupplierRepository.GetSupplierById(supplierId.Value);
            if (supplier != null)
            {
                supplier.ProductIdOfSupplier = "," + supplier.ProductIdOfSupplier + ",";
                var list = ProductRepository.GetAllProduct().AsEnumerable().Where(x => supplier.ProductIdOfSupplier.Contains( "," + x.Id + ",") == true);

                return Json(list.Select(x => new { Id = x.Id, Name = x.Name, Price = x.PriceInbound, Unit = x.Unit }), JsonRequestBehavior.AllowGet);
            }
            return Json(new List<int>(), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
