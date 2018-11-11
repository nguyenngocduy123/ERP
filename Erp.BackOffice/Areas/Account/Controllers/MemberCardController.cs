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

namespace Erp.BackOffice.Account.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class MemberCardController : Controller
    {
        private readonly IMemberCardRepository MemberCardRepository;
        private readonly IUserRepository userRepository;
        private readonly ICustomerRepository customerRepository;
        public MemberCardController(
            IMemberCardRepository _MemberCard
            , IUserRepository _user
            ,ICustomerRepository customer
            )
        {
            MemberCardRepository = _MemberCard;
            userRepository = _user;
            customerRepository = customer;
        }

        #region Index

        public ViewResult Index(string Code, string Status, int? BranchId)
        {
            List<MemberCardViewModel> q = MemberCardRepository.GetAllvwMemberCard()
                .Select(item => new MemberCardViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    BranchName = item.BranchName,
                    BranchId=item.BranchId,
                    Code=item.Code,
                    Status=item.Status,
                    DateOfIssue=item.DateOfIssue,
                    CustomerId=item.CustomerId,
                    CustomerName=item.CustomerName,
                    Image=item.Image,
                    CustomerCode=item.CustomerCode,
                    Type=item.Type,
                    CreateUserName=item.CreateUserName
                }).OrderByDescending(m => m.ModifiedDate).ToList();
            if (!string.IsNullOrEmpty(Code))
            {
                q = q.Where(item => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.Code).ToLower().Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(Code).ToLower())).ToList();
            }
            if (BranchId != null && BranchId.Value > 0)
            {
                q = q.Where(item => item.BranchId == BranchId).ToList();
            }
            if (!string.IsNullOrEmpty(Status))
            {
                q = q.Where(item => item.Status == Status).ToList();
            }
            foreach (var item in q)
            {
                item.ImagePath = Erp.BackOffice.Helpers.Common.KiemTraTonTaiHinhAnh(item.Image, "uploads_image_path_customer", "user");
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
            var model = new MemberCardViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(MemberCardViewModel model)
        {
            if (ModelState.IsValid)
            {
                var MemberCard = new MemberCard();
                AutoMapper.Mapper.Map(model, MemberCard);
                MemberCard.IsDeleted = false;
                MemberCard.CreatedUserId = WebSecurity.CurrentUserId;
                MemberCard.ModifiedUserId = WebSecurity.CurrentUserId;
                MemberCard.AssignedUserId = WebSecurity.CurrentUserId;
                MemberCard.CreatedDate = DateTime.Now;
                MemberCard.ModifiedDate = DateTime.Now;
                MemberCard.DateOfIssue = DateTime.Now;
                MemberCard.Status = "inprogress";
                MemberCardRepository.InsertMemberCard(MemberCard);
                //cập nhật mã thẻ cho khách hang
                var customer = customerRepository.GetCustomerById(model.CustomerId.Value);
                customer.CardCode = customer.CardCode;
                customerRepository.UpdateCustomer(customer);
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.CreateMemberCardSuccess;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var MemberCard = MemberCardRepository.GetvwMemberCardById(Id.Value);
            if (MemberCard != null && MemberCard.IsDeleted != true)
            {
                var model = new MemberCardViewModel();
                AutoMapper.Mapper.Map(MemberCard, model);
                
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

        [HttpPost]
        public ActionResult Edit(MemberCardViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var MemberCard = MemberCardRepository.GetMemberCardById(model.Id);
                    AutoMapper.Mapper.Map(model, MemberCard);
                    MemberCard.ModifiedUserId = WebSecurity.CurrentUserId;
                    MemberCard.ModifiedDate = DateTime.Now;
                    MemberCardRepository.UpdateMemberCard(MemberCard);

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
            var MemberCard = MemberCardRepository.GetMemberCardById(Id.Value);
            if (MemberCard != null && MemberCard.IsDeleted != true)
            {
                var model = new MemberCardViewModel();
                AutoMapper.Mapper.Map(MemberCard, model);
                
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
                    var item = MemberCardRepository.GetMemberCardById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if(item != null)
                    {
                        //if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        //{
                        //    TempData["FailedMessage"] = "NotOwner";
                        //    return RedirectToAction("Index");
                        //}

                        item.IsDeleted = true;
                        MemberCardRepository.UpdateMemberCard(item);
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
