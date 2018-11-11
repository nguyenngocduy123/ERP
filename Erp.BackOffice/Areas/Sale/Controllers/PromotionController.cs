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
    public class PromotionController : Controller
    {
        private readonly IPromotionRepository PromotionRepository;
        private readonly IProductOrServiceRepository ProductRepository;
        private readonly IUserRepository userRepository;

        public PromotionController(
            IPromotionRepository _Promotion
            , IProductOrServiceRepository _Product
            , IUserRepository _user
            )
        {
            PromotionRepository = _Promotion;
            ProductRepository = _Product;
            userRepository = _user;
        }

        #region Index

        public ViewResult Index(string txtSearch)
        {

            IQueryable<PromotionViewModel> q = PromotionRepository.GetAllPromotion()
                .Select(item => new PromotionViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate,
                    IsAllProduct = item.IsAllProduct,
                    Code = item.Code,
                    PercentValue = item.PercentValue

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
            var model = new PromotionViewModel();
            var productList = ProductRepository.GetAllProduct()
               .Select(item => new ProductViewModel
               {
                   Code = item.Code,
                   Barcode = item.Barcode,
                   Name = item.Name,
                   Id = item.Id,
                   CategoryCode = item.CategoryCode,
                   PriceOutbound = item.PriceOutbound,
                   Unit = item.Unit,
                   Type = item.Type
               });
            ViewBag.productList = productList;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(PromotionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var Promotion = new Promotion();
                AutoMapper.Mapper.Map(model, Promotion);
                Promotion.IsDeleted = false;
                Promotion.CreatedUserId = WebSecurity.CurrentUserId;
                Promotion.ModifiedUserId = WebSecurity.CurrentUserId;
                Promotion.AssignedUserId = WebSecurity.CurrentUserId;
                Promotion.CreatedDate = DateTime.Now;
                Promotion.ModifiedDate = DateTime.Now;


                List<PromotionDetail> promotionDetails = new List<PromotionDetail>();

                if (model.DetailList == null)
                    model.DetailList = new List<PromotionDetailViewModel>();

                //lọc tất cả các dòng chi tiết phải có giá trị giảm giá
                model.DetailList = model.DetailList.Where(x => x.PercentValue > 0).ToList();

                //nếu có các dòng danh mục đã được chọn thì lọc hết tất cá các dòng danh mục và sản phẩm không được chọn
                if (model.DetailList.Where(x => string.IsNullOrEmpty(x.CategoryCode) == false && x.Type == "product").Count() > 0)
                    model.DetailList.RemoveAll(x => (x.ProductId == null && string.IsNullOrEmpty(x.CategoryCode)) == true && x.Type == "product");
                else
                {
                    //nếu ko có các dòng dnah mục được chọn thì chỉ cho phép tối đa một dòng danh mục và sản phẩm không chọn
                    var categoryEmpty_productAll = model.DetailList.Where(x => (x.ProductId == null && string.IsNullOrEmpty(x.CategoryCode)) == true && x.Type == "product").ToList();
                    if (categoryEmpty_productAll.Count > 0)
                    {
                        model.DetailList.RemoveAll(x => (x.ProductId == null && string.IsNullOrEmpty(x.CategoryCode)) == true && x.Type == "product");
                        model.DetailList.Add(categoryEmpty_productAll.FirstOrDefault());
                    }
                }

                //nếu có các dòng danh mục đã được chọn thì lọc hết tất cá các dòng danh mục và dịch vụ không được chọn
                if (model.DetailList.Where(x => string.IsNullOrEmpty(x.CategoryCode) == false && x.Type == "service").Count() > 0)
                    model.DetailList.RemoveAll(x => (x.ProductId == null && string.IsNullOrEmpty(x.CategoryCode)) == true && x.Type == "service");
                else
                {
                    //nếu ko có các dòng dnah mục được chọn thì chỉ cho phép tối đa một dòng danh mục và dịch vụ không chọn
                    var categoryEmpty_productAll = model.DetailList.Where(x => (x.ProductId == null && string.IsNullOrEmpty(x.CategoryCode)) == true && x.Type == "service").ToList();
                    if (categoryEmpty_productAll.Count > 0)
                    {
                        model.DetailList.RemoveAll(x => (x.ProductId == null && string.IsNullOrEmpty(x.CategoryCode)) == true && x.Type == "service");
                        model.DetailList.Add(categoryEmpty_productAll.FirstOrDefault());
                    }
                }

                if(model.DetailList.Count != 0)
                {
                    foreach(var item in model.DetailList.Where(x => x.ProductId == null))
                        item.IsAll = true;


                    promotionDetails = model.DetailList.Select(x => new PromotionDetail { 
                        IsAll = x.IsAll,
                        IsDeleted = false,
                        PercentValue = x.PercentValue,
                        ProductId = x.ProductId,
                        CategoryCode = x.CategoryCode,
                        QuantityFor = x.QuantityFor,
                        Type = x.Type
                    }).ToList();
                }

                PromotionRepository.InsertPromotion(Promotion, promotionDetails);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var Promotion = PromotionRepository.GetPromotionById(Id.Value);
            if (Promotion != null && Promotion.IsDeleted != true)
            {
                var model = new PromotionViewModel();
                AutoMapper.Mapper.Map(Promotion, model);
                
                //if (model.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                //{
                //    TempData["FailedMessage"] = "NotOwner";
                //    return RedirectToAction("Index");
                //}                

                var detailList = PromotionRepository.GetAllPromotionDetailBy(model.Id).Select(x => new PromotionDetailViewModel
                {
                    Id = x.Id,
                    IsAll = x.IsAll,
                    IsDeleted = false,
                    PercentValue = x.PercentValue,
                    ProductId = x.ProductId,
                    CategoryCode = x.CategoryCode,
                    QuantityFor = x.QuantityFor,
                    Type = x.Type
                }).ToList();

                if (detailList.Count == 0)
                {
                    detailList.Add(new PromotionDetailViewModel() { QuantityFor = 1, PercentValue = 0});
                }

                model.DetailList = detailList;


                var productList = ProductRepository.GetAllProduct()
               .Select(item => new ProductViewModel
               {
                   Code = item.Code,
                   Barcode = item.Barcode,
                   Name = item.Name,
                   Id = item.Id,
                   CategoryCode = item.CategoryCode,
                   PriceOutbound = item.PriceOutbound,
                   Unit = item.Unit,
               });
                ViewBag.productList = productList;

                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(PromotionViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var Promotion = PromotionRepository.GetPromotionById(model.Id);
                    AutoMapper.Mapper.Map(model, Promotion);
                    Promotion.ModifiedUserId = WebSecurity.CurrentUserId;
                    Promotion.ModifiedDate = DateTime.Now;
                    PromotionRepository.UpdatePromotion(Promotion);


                    List<PromotionDetail> promotionDetails = new List<PromotionDetail>();

                    if (model.DetailList == null)
                        model.DetailList = new List<PromotionDetailViewModel>();

                    //lọc tất cả các dòng chi tiết phải có giá trị giảm giá
                    model.DetailList = model.DetailList.Where(x => x.PercentValue > 0).ToList();

                    //nếu có các dòng danh mục đã được chọn thì lọc hết tất cá các dòng danh mục và sản phẩm không được chọn
                    if (model.DetailList.Where(x => string.IsNullOrEmpty(x.CategoryCode) == false && x.Type == "product").Count() > 0)
                        model.DetailList.RemoveAll(x => (x.ProductId == null && string.IsNullOrEmpty(x.CategoryCode)) == true && x.Type == "product");
                    else
                    {
                        //nếu ko có các dòng dnah mục được chọn thì chỉ cho phép tối đa một dòng danh mục và sản phẩm không chọn
                        var categoryEmpty_productAll = model.DetailList.Where(x => (x.ProductId == null && string.IsNullOrEmpty(x.CategoryCode)) == true && x.Type == "product").ToList();
                        if (categoryEmpty_productAll.Count > 0)
                        {
                            model.DetailList.RemoveAll(x => (x.ProductId == null && string.IsNullOrEmpty(x.CategoryCode)) == true && x.Type == "product");
                            model.DetailList.Add(categoryEmpty_productAll.FirstOrDefault());
                        }
                    }

                    //nếu có các dòng danh mục đã được chọn thì lọc hết tất cá các dòng danh mục và dịch vụ không được chọn
                    if (model.DetailList.Where(x => string.IsNullOrEmpty(x.CategoryCode) == false && x.Type == "service").Count() > 0)
                        model.DetailList.RemoveAll(x => (x.ProductId == null && string.IsNullOrEmpty(x.CategoryCode)) == true && x.Type == "service");
                    else
                    {
                        //nếu ko có các dòng dnah mục được chọn thì chỉ cho phép tối đa một dòng danh mục và dịch vụ không chọn
                        var categoryEmpty_productAll = model.DetailList.Where(x => (x.ProductId == null && string.IsNullOrEmpty(x.CategoryCode)) == true && x.Type == "service").ToList();
                        if (categoryEmpty_productAll.Count > 0)
                        {
                            model.DetailList.RemoveAll(x => (x.ProductId == null && string.IsNullOrEmpty(x.CategoryCode)) == true && x.Type == "service");
                            model.DetailList.Add(categoryEmpty_productAll.FirstOrDefault());
                        }
                    }


                    if (model.DetailList.Count != 0)
                    {
                        foreach (var item in model.DetailList.Where(x => x.ProductId == null))
                            item.IsAll = true;

                        promotionDetails = model.DetailList.Select(x => new PromotionDetail
                        {
                            Id = x.Id,
                            IsAll = x.IsAll,
                            IsDeleted = false,
                            PercentValue = x.PercentValue,
                            ProductId = x.ProductId,
                            CategoryCode = x.CategoryCode,
                            QuantityFor = x.QuantityFor,
                            PromotionId = Promotion.Id
                        }).ToList();
                    }

                    var detailListOld = PromotionRepository.GetAllPromotionDetailBy(model.Id).ToList();

                    var deleteList = detailListOld.Where(x => promotionDetails.Any(y => y.Id == x.Id) == false).ToList();

                    var updateList = detailListOld.Where(x => promotionDetails.Any(y => y.Id == x.Id)).ToList();

                    var addNewList = promotionDetails.Where(x => x.Id == 0).ToList();

                    //xóa các chi tiết không chọn nữa
                    for (int i = 0; i < deleteList.Count; i++ )
                    {
                        PromotionRepository.DeletePromotionDetailRs(deleteList[i].Id);
                    }

                    //cập nhật các chi tiết thay đổi
                    for (int i = 0; i < updateList.Count; i++)
                    {
                        var detailUpateNewData = promotionDetails.Where(x => x.Id == updateList[i].Id).FirstOrDefault();
                        if(detailUpateNewData != null)
                        {
                            updateList[i].IsAll = detailUpateNewData.IsAll;
                            updateList[i].PercentValue = detailUpateNewData.PercentValue;
                            updateList[i].CategoryCode = detailUpateNewData.CategoryCode;
                            updateList[i].ProductId = detailUpateNewData.ProductId;
                            updateList[i].QuantityFor = detailUpateNewData.QuantityFor;
                        }

                        PromotionRepository.UpdatePromotionDetail(updateList[i]);
                    }

                    //thêm mới các chi tiết mới chọn
                    for (int i = 0; i < addNewList.Count; i++)
                    {
                        PromotionRepository.InsertPromotionDetail(addNewList[i]);
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
            var Promotion = PromotionRepository.GetPromotionById(Id.Value);
            if (Promotion != null && Promotion.IsDeleted != true)
            {
                var model = new PromotionViewModel();
                AutoMapper.Mapper.Map(Promotion, model);
                
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
                    var item = PromotionRepository.GetPromotionById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if(item != null)
                    {
                        //if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        //{
                        //    TempData["FailedMessage"] = "NotOwner";
                        //    return RedirectToAction("Index");
                        //}

                        item.IsDeleted = true;
                        PromotionRepository.UpdatePromotion(item);
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

        #region JSON

        public JsonResult GetCurrentPromotion()
        {
            var currentEndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            var list = PromotionRepository.GetAllPromotion().Where(x => x.StartDate <= DateTime.Now && currentEndDate <= x.EndDate).ToList();

            var listDetailView = new List<PromotionDetail>();
            foreach(var item in list)
            {
                listDetailView.AddRange(PromotionRepository.GetAllPromotionDetailBy(item.Id).ToList());
            }

            var result = new
            {
                promotionList = list.Select(x => new
                {
                    Id = x.Id,
                    Code = x.Code,
                    IsAllProduct = x.IsAllProduct,
                    PercentValue = x.PercentValue,
                    EndDate = x.EndDate,
                    StartDate = x.StartDate,
                    Name = x.Name
                }),
                productCategoryList = listDetailView.Where(x => x.IsAll == true).OrderByDescending(x => x.PercentValue).ThenByDescending(x => x.QuantityFor).Select(x => new
                {
                    Id = x.Id,
                    IsAll = x.IsAll,
                    PercentValue = x.PercentValue,
                    ProductId = x.ProductId,
                    CategoryCode = x.CategoryCode,
                    QuantityFor = x.QuantityFor,
                    PromotionId = x.PromotionId,
                    Type = x.Type
                }).ToList(),
                productList = listDetailView.Where(x => x.ProductId != null).OrderByDescending(x => x.PercentValue).ThenByDescending(x => x.QuantityFor).Select(x => new
                {
                    Id = x.Id,
                    IsAll = x.IsAll,
                    PercentValue = x.PercentValue,
                    ProductId = x.ProductId,
                    CategoryCode = x.CategoryCode,
                    QuantityFor = x.QuantityFor,
                    PromotionId = x.PromotionId,
                    Type = x.Type
                }).ToList(),
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
