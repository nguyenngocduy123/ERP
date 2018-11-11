using System.Globalization;
using Erp.BackOffice.Sale.Models;
using Erp.BackOffice.Filters;
using Erp.Domain.Entities;
using Erp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Erp.Utilities;
using WebMatrix.WebData;
using Erp.Domain.Sale.Interfaces;
using Erp.BackOffice.Helpers;
using Erp.Domain.Staff.Interfaces;
using Erp.Domain.Sale.Entities;
using Erp.BackOffice.App_GlobalResources;
using Erp.BackOffice.Areas.Staff.Models;
using Erp.BackOffice.Staff.Models;

namespace Erp.BackOffice.Sale.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class CommisionController : Controller
    {
        private readonly ICommisionRepository commisionRepository;
        private readonly IBranchRepository branchRepository;
        private readonly IStaffsRepository staffsRepository;
        private readonly IUserRepository userRepository;
        private readonly IProductOrServiceRepository productRepository;

        public CommisionController(
            ICommisionRepository _Commision
            , IBranchRepository _Branch
            , IUserRepository _user
            , IStaffsRepository _staffs
            , IProductOrServiceRepository _Product
            )
        {
            commisionRepository = _Commision;
            branchRepository = _Branch;
            userRepository = _user;
            staffsRepository = _staffs;
            productRepository = _Product;
        }

        #region Index

        public ViewResult Index(string txtSearch)
        {
            IQueryable<BranchViewModel> q = branchRepository.GetAllvwBranch()
                .Select(item => new BranchViewModel
                {
                    Id = item.Id,
                    Name = item.Name
                }).OrderBy(m => m.Name);

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var branch = branchRepository.GetBranchById(Id.Value);
            if (branch != null && branch.IsDeleted != true)
            {
                var model = new CommisionEditViewModel();
                model.BranchId = branch.Id;
                model.BranchName = branch.Name;

                model.ListStaff = staffsRepository.GetvwAllStaffs()
                  //  .Where(item => item.Sale_BranchId == branch.Id)
                    .Select(item => new StaffGeneralInfoViewModel
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Code = item.Code,
                        BranchId = item.Sale_BranchId
                    })
                    .OrderBy(item => item.Name)
                    .ToList();

                return View(model);
            }

            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }
        #endregion

        #region Detail
        public ViewResult Detail(int BranchId, int StaffId, string txtProductCode,string txtCode, string txtSearch)
        {
            CommisionDetailViewModel model = new CommisionDetailViewModel();
            var listCommision = commisionRepository.GetAllCommision().Where(item => item.BranchId == BranchId && item.StaffId == StaffId).ToList();
            model.StaffName = staffsRepository.GetAllStaffs().Where(item => item.Id == StaffId).Select(item => item.Name).FirstOrDefault();
            model.StaffId = StaffId;
            model.DetailList = new List<CommisionViewModel>();

            var productList = productRepository.GetAllvwProductAndService().Select(item => new
            {
                item.Id,
                item.Code,
                item.Name,
                item.PriceOutbound
            }).ToList();
            if (!string.IsNullOrEmpty(txtCode))
            {
                productList = productList.Where(x => x.Code.Contains(txtCode)).ToList();
            }
            if (!string.IsNullOrEmpty(txtSearch))
            {
                productList = productList.Where(x => x.Name.Contains(txtSearch)).ToList();
            }
            foreach (var item in productList)
            {
                var commisionViewModel = new CommisionViewModel();
                commisionViewModel.ProductId = item.Id;
                commisionViewModel.Name = item.Code + " - " + item.Name;
                commisionViewModel.Price = item.PriceOutbound.Value;
                commisionViewModel.IsMoney = false;
                var commision = listCommision.Where(i => i.ProductId == item.Id).FirstOrDefault();
                if (commision != null)
                {
                    commisionViewModel.Id = commision.Id;
                    commisionViewModel.CommissionValue = commision.CommissionValue;
                    commisionViewModel.IsMoney = commision.IsMoney == null ? false : commision.IsMoney;
                }

                model.DetailList.Add(commisionViewModel);
            }

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];

            return View(model);
        }

        [HttpPost]
        public ActionResult Detail(CommisionDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    foreach (var item in model.DetailList)
                    {
                        if (item.CommissionValue < 0)
                            item.CommissionValue = 0;
                        else if ((item.IsMoney == null || item.IsMoney == false) && item.CommissionValue > 100)
                            item.CommissionValue = 100;
                        else if (item.CommissionValue > item.Price)
                            item.CommissionValue = item.Price;

                        if (item.Id > 0)
                        {
                            var commision = commisionRepository.GetCommisionById(item.Id);
                            commision.ModifiedUserId = WebSecurity.CurrentUserId;
                            commision.ModifiedDate = DateTime.Now;
                            commision.CommissionValue = item.CommissionValue;
                            commision.IsMoney = item.IsMoney;
                            commisionRepository.UpdateCommision(commision);
                        }
                        else
                        {
                            if (item.CommissionValue > 0)
                            {
                                var commision = new Commision();
                                commision.IsDeleted = false;
                                commision.CreatedUserId = WebSecurity.CurrentUserId;
                                commision.ModifiedUserId = WebSecurity.CurrentUserId;
                                commision.CreatedDate = DateTime.Now;
                                commision.ModifiedDate = DateTime.Now;
                                commision.BranchId = model.BranchId;
                                commision.StaffId = model.StaffId;
                                commision.ProductId = item.ProductId;
                                commision.CommissionValue = item.CommissionValue;
                                commision.IsMoney = item.IsMoney;
                                commisionRepository.InsertCommision(commision);
                            }
                        }
                    }

                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                    return RedirectToAction("Detail", new { BranchId = model.BranchId, StaffId = model.StaffId });
                }

                return View(model);
            }

            return View(model);
        }
        #endregion
    }
}
