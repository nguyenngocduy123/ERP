using System.Globalization;
using Erp.BackOffice.Sale.Models;
using Erp.BackOffice.Staff.Models;
using Erp.BackOffice.Filters;
using Erp.Domain.Entities;
using Erp.Domain.Interfaces;
using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
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

namespace Erp.BackOffice.Sale.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class CommissionCusController : Controller
    {
        private readonly ICommissionCusRepository CommissionCusRepository;
        private readonly IUserRepository userRepository;
        private readonly IProductOrServiceRepository productRepository;
        private readonly ICommisionCustomerRepository CommissionDetailRepository;
        private readonly IBranchRepository branchRepository;
        public CommissionCusController(
            ICommissionCusRepository _CommissionCus
            , IUserRepository _user
             , IProductOrServiceRepository _Product
            , ICommisionCustomerRepository CommissionDetail
            ,IBranchRepository branch
            )
        {
            CommissionCusRepository = _CommissionCus;
            userRepository = _user;
            productRepository = _Product;
            CommissionDetailRepository = CommissionDetail;
            branchRepository = branch;
        }
        #region List Category
        //IEnumerable<CommissionCusViewModel> getCommission(string ApplyFor)
        //{
        //    IEnumerable<CommissionCusViewModel> listCategory = new List<CommissionCusViewModel>();
        //    var model = CommissionCusRepository.GetAllCommissionCus()
        //        .Where(item => item.ApplyFor == ApplyFor)
        //        .OrderByDescending(m => m.CreatedDate).ToList();

        //    listCategory = AutoMapper.Mapper.Map(model, listCategory);

        //    return listCategory;
        //}

        //public ViewResult CommissionDrugStore()
        //{
        //    string ApplyFor = "DrugStore";
        //    ViewBag.SuccessMessage = TempData[Globals.SuccessMessageKey];
        //    ViewBag.FailedMessage = TempData[Globals.FailedMessageKey];
        //    ViewBag.ApplyFor = ApplyFor;
        //    ViewBag.ActionName = "CommissionDrugStore";

        //    return View("Index", getCommission(ApplyFor));
        //}

        //public ViewResult CommissionCustomer()
        //{
        //    string ApplyFor = "Customer";
        //    ViewBag.SuccessMessage = TempData[Globals.SuccessMessageKey];
        //    ViewBag.FailedMessage = TempData[Globals.FailedMessageKey];
        //    ViewBag.ApplyFor = ApplyFor;
        //    ViewBag.ActionName = "CommissionCustomer";

        //    return View("Index", getCommission(ApplyFor));
        //}

        public ViewResult Index(string txtSearch, string Type)
        {
            var user = userRepository.GetByvwUserName(Helpers.Common.CurrentUser.UserName);
            IEnumerable<CommissionCusViewModel> q = CommissionCusRepository.GetAllCommissionCus()
                .Select(item => new CommissionCusViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    ApplyFor=item.ApplyFor,
                    EndDate=item.EndDate,
                    Note=item.Note,
                    StartDate=item.StartDate,
                    Type=item.Type

                }).OrderByDescending(m => m.ModifiedDate);

            if (string.IsNullOrEmpty(txtSearch) == false || string.IsNullOrEmpty(Type) == false)
            {
                txtSearch = txtSearch == "" ? "~" : Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(txtSearch);
                Type = Type == "" ? "~" : Type.ToLower();
                //txtPhone = txtPhone == "" ? "~" : txtPhone.ToLower();
                //txtEmail = txtEmail == "" ? "~" : txtEmail.ToLower();
                q = q.Where(x => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(x.Name).Contains(txtSearch) || x.Type.ToLowerOrEmpty().Contains(Type));
            }

            if (!Filters.SecurityFilter.IsAdmin() && !Filters.SecurityFilter.IsKeToan())
            {
                q = q.Where(item => ("," + user.DrugStore + ",").Contains("," + item.ApplyFor + ",") == true);
            }
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        #endregion

        #region LoadProductItem

        public PartialViewResult LoadProduct(int? CommissionCusId)
        {
            var model = new CommissionCusViewModel();
            model.DetailList = new List<CommisionCustomerViewModel>();
            model.ProductList = new List<ProductViewModel>();
            //lấy danh sách chi tiết chiết khấu sản phẩm
            if (CommissionCusId != null && CommissionCusId.Value > 0)
            {
                var detail = CommissionDetailRepository.GetAllCommisionCustomer().Where(x => x.CommissionCusId == CommissionCusId).ToList();
                model.DetailList = detail.Select(x => new CommisionCustomerViewModel
                {
                    ProductId = x.ProductId.Value,
                    Id = x.Id,
                    IsMoney = x.IsMoney,
                    Type = x.Type,
                    CommissionValue = x.CommissionValue,
                    CommissionCusId = x.CommissionCusId.Value
                }).ToList();
            }
            //lấy danh sách sản phẩm thuộc nhóm đã chọn
            var product = productRepository.GetAllProduct();
            model.ProductList = product.Select(x => new ProductViewModel
                {
                    Id = x.Id,
                    Code = x.Code,
                    Name = x.Name,
                    PriceOutbound = x.PriceOutbound
                }).ToList();
            return PartialView(model);
        }
        #endregion
        #region Create

        public ViewResult Create(int? Id)
        {

            var model = new CommissionCusViewModel();
            //model.ApplyFor = "DrugStore";
            model.DetailList = new List<CommisionCustomerViewModel>();
            var departmentList = branchRepository.GetAllBranch().Where(x=>x.ParentId!=null||x.ParentId>0)
               .Select(item => new BranchViewModel
               {
                   Name = item.Name,
                   Id = item.Id,
                   ParentId = item.ParentId
               }).ToList();
            ViewBag.departmentList = departmentList;
            if (Id != null && Id > 0)
            {
                var CommissionCus = CommissionCusRepository.GetCommissionCusById(Id.Value);
                AutoMapper.Mapper.Map(CommissionCus, model);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CommissionCusViewModel model)
        {
            var urlRefer = Request["UrlReferrer"];
            var check = Request["group_choice"];
            var ApplyFor = Request["ApplyFor"];
            if (ModelState.IsValid)
            {
                CommissionCus commissioncus = null;
                if (model.Id > 0)
                {
                    commissioncus = CommissionCusRepository.GetCommissionCusById(model.Id);
                }
                if (commissioncus != null)
                {
                    AutoMapper.Mapper.Map(model, commissioncus);
                    commissioncus.Type = check;
                    commissioncus.ApplyFor = ApplyFor;
                    commissioncus.StartDate = check == "FixedDiscount" ? null : model.StartDate;
                    if (model.EndDate != null)
                    {
                        if (check != "FixedDiscount")
                        {
                            commissioncus.EndDate = model.EndDate.Value.AddHours(23).AddMinutes(59);
                        }
                    }
                    else
                        commissioncus.EndDate = null;
                    commissioncus.ModifiedUserId = WebSecurity.CurrentUserId;
                    commissioncus.ModifiedDate = DateTime.Now;
                }
                else
                {
                    commissioncus = new CommissionCus();
                    AutoMapper.Mapper.Map(model, commissioncus);
                    commissioncus.IsDeleted = false;
                    commissioncus.CreatedUserId = WebSecurity.CurrentUserId;
                    commissioncus.ModifiedUserId = WebSecurity.CurrentUserId;
                    commissioncus.AssignedUserId = WebSecurity.CurrentUserId;
                    commissioncus.CreatedDate = DateTime.Now;
                    commissioncus.ModifiedDate = DateTime.Now;
                    commissioncus.Type = check;
                    commissioncus.ApplyFor = ApplyFor;
                    commissioncus.StartDate = check == "FixedDiscount" ? null : model.StartDate;
                    if (model.EndDate != null)
                    {
                        if (check != "FixedDiscount")
                        {
                            commissioncus.EndDate = model.EndDate.Value.AddHours(23).AddMinutes(59);
                        }
                    }
                    else
                        commissioncus.EndDate = null;
                }
                model.DetailList.RemoveAll(x => x.CommissionValue <= 0);
                //hàm edit 
                if (model.Id > 0)
                {
                    CommissionCusRepository.UpdateCommissionCus(commissioncus);
                    var detail = CommissionDetailRepository.GetAllCommisionCustomer().Where(x => x.CommissionCusId == model.Id).ToList();
                    for (int i = 0; i < detail.Count(); i++)
                    {
                        CommissionDetailRepository.DeleteCommisionCustomer(detail[i].Id);
                    }
                }
                else
                {
                    CommissionCusRepository.InsertCommissionCus(commissioncus);
                }
                foreach (var item in model.DetailList)
                {
                    var commision = new CommisionCustomer();
                    commision.IsDeleted = false;
                    commision.CreatedUserId = WebSecurity.CurrentUserId;
                    commision.ModifiedUserId = WebSecurity.CurrentUserId;
                    commision.CreatedDate = DateTime.Now;
                    commision.ModifiedDate = DateTime.Now;
                    commision.CommissionCusId = commissioncus.Id;
                    commision.ProductId = item.ProductId;
                    commision.CommissionValue = item.CommissionValue;
                    commision.IsMoney = item.IsMoney;
                    CommissionDetailRepository.InsertCommisionCustomer(commision);
                }
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                {
                    return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
                }
                //return Redirect(urlRefer);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var CommissionCus = CommissionCusRepository.GetCommissionCusById(Id.Value);
            if (CommissionCus != null && CommissionCus.IsDeleted != true)
            {
                var model = new CommissionCusViewModel();
                AutoMapper.Mapper.Map(CommissionCus, model);

                if (model.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                {
                    TempData["FailedMessage"] = "NotOwner";
                    return RedirectToAction("Index");
                }

                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(CommissionCusViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var CommissionCus = CommissionCusRepository.GetCommissionCusById(model.Id);
                    AutoMapper.Mapper.Map(model, CommissionCus);
                    CommissionCus.ModifiedUserId = WebSecurity.CurrentUserId;
                    CommissionCus.ModifiedDate = DateTime.Now;
                    CommissionCusRepository.UpdateCommissionCus(CommissionCus);

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
            var CommissionCus = CommissionCusRepository.GetCommissionCusById(Id.Value);
            if (CommissionCus != null && CommissionCus.IsDeleted != true)
            {
                var model = new CommissionCusViewModel();
                AutoMapper.Mapper.Map(CommissionCus, model);
                model.DetailList = new List<CommisionCustomerViewModel>();
                model.ProductList = new List<ProductViewModel>();
                //lấy danh sách chi tiết chiết khấu sản phẩm
                    var detail = CommissionDetailRepository.GetAllCommisionCustomer().Where(x => x.CommissionCusId == Id).ToList();
                    model.DetailList = detail.Select(x => new CommisionCustomerViewModel
                    {
                        ProductId = x.ProductId.Value,
                        Id = x.Id,
                        IsMoney = x.IsMoney,
                        Type = x.Type,
                        CommissionValue = x.CommissionValue,
                        CommissionCusId = x.CommissionCusId.Value
                    }).ToList();
                //lấy danh sách sản phẩm thuộc nhóm đã chọn
                var product = productRepository.GetAllProduct();
                model.ProductList = product.Select(x => new ProductViewModel
                {
                    Id = x.Id,
                    Code = x.Code,
                    Name = x.Name,
                    PriceOutbound = x.PriceOutbound
                }).ToList();
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
                    var item = CommissionCusRepository.GetCommissionCusById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        {
                            TempData["FailedMessage"] = "NotOwner";
                            return RedirectToAction("Index");
                        }

                        item.IsDeleted = true;
                        CommissionCusRepository.UpdateCommissionCus(item);
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
