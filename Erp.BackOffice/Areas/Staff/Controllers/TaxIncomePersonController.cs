using System.Globalization;
using Erp.BackOffice.Staff.Models;
using Erp.BackOffice.Filters;
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
using Erp.BackOffice.Helpers;
using System.Web.Script.Serialization;

namespace Erp.BackOffice.Staff.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class TaxIncomePersonController : Controller
    {
        private readonly ITaxIncomePersonRepository TaxIncomePersonRepository;
        private readonly ITaxIncomePersonDetailRepository taxIncomePersonDetailRepository;
        private readonly IUserRepository userRepository;
        private readonly IStaffsRepository staffsRepository;

        public TaxIncomePersonController(
            ITaxIncomePersonRepository _TaxIncomePerson
            , IUserRepository _user
            , IStaffsRepository _staff
            , ITaxIncomePersonDetailRepository _taxDetail
            )
        {
            TaxIncomePersonRepository = _TaxIncomePerson;
            userRepository = _user;
            staffsRepository = _staff;
            taxIncomePersonDetailRepository = _taxDetail;
        }

        #region Index

        public ViewResult Index(string Code)
        {

            IQueryable<TaxIncomePersonViewModel> q = TaxIncomePersonRepository.GetAllTaxIncomePerson()
                .Select(item => new TaxIncomePersonViewModel
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
                    GeneralTaxationId = item.GeneralTaxationId,
                    GeneralManageId = item.GeneralManageId

                }).OrderByDescending(m => m.ModifiedDate);

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];

            if (!string.IsNullOrEmpty(Code))
            {
                q = q.Where(n => n.Code.Contains(Code));
            }
            return View(q);
        }
        #endregion

        #region Create
        public ViewResult Create()
        {
            var model = new TaxIncomePersonViewModel();

            model.Name = Helpers.Common.GetSetting("companyName");

            return View(model);
        }

        //Add Staff to TAX


        //Load Staff

        public JsonResult GetStaffs(int? Id, int? BranchId, string StartDate, string EndDate)
        {

            List<StaffsTaxModel> modelStaff = staffsRepository.GetvwAllStaffs()
              .Select(item => new StaffsTaxModel
              {
                  Id = item.Id,
                  Name = item.Name,
                  //Birthday = item.Birthday.Value!=null? item.Birthday.Value.ToString("dd.MM.yy"):"",
                  Code = item.Code,
                  DistrictName = item.DistrictName,
                  BranchId = item.Sale_BranchId,
                  Email = item.Email,
                  EndDate = item.EndDate,
                  StartDate = item.StartDate,
                  Gender = item.Gender,
                  IdCardNumber = item.IdCardNumber,
                  BranchName = item.BranchName,
                  ContryName = item.CountryId,
                  ProvinceName = item.ProvinceName,
                  WardName = item.WardName
              }).OrderByDescending(m => m.Id).ToList();

            if (modelStaff == null)
                return Json(null);

            if (Id != null)
            {
                var modelbyId = modelStaff.Where(n => n.Id == Id);
                var _json = new JavaScriptSerializer().Serialize(modelbyId);
                return Json(_json, JsonRequestBehavior.AllowGet);
            }

            if (BranchId != null)
            {
                modelStaff = modelStaff.Where(n => n.BranchId == BranchId).ToList();
            }
            if (!string.IsNullOrEmpty(StartDate))
            {
                DateTime dt = DateTime.ParseExact(StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                modelStaff = modelStaff.Where(n => n.StartDate <= dt).ToList();
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                DateTime dt = DateTime.ParseExact(EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                modelStaff = modelStaff.Where(n => n.EndDate >= dt).ToList();
            }
            var json = new JavaScriptSerializer().Serialize(modelStaff);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(TaxIncomePersonViewModel model)
        {
            var _model = TaxIncomePersonRepository.GetAllTaxIncomePerson().Where(n => n.Code == model.Code);
            if (_model != null && _model.Count() > 0)
            {
                //StaffsTaxModel
                string staffids = Request["StaffIds"];
                if (!string.IsNullOrEmpty(staffids))
                {
                    List<string> arrStaffIds = staffids.Split(',').ToList();
                    List<StaffsTaxModel> modelStaff = staffsRepository.GetvwAllStaffs()
                     .Select(item => new StaffsTaxModel
                     {
                         Id = item.Id,
                         Name = item.Name,
                         Code = item.Code,
                         DistrictName = item.DistrictName,
                         BranchId = item.Sale_BranchId,
                         Email = item.Email,
                         EndDate = item.EndDate,
                         StartDate = item.StartDate,
                         Gender = item.Gender,
                         IdCardNumber = item.IdCardNumber,
                         BranchName = item.BranchName,
                         ContryName = item.CountryId,
                         ProvinceName = item.ProvinceName,
                         WardName = item.WardName
                     }).OrderByDescending(m => m.Id).ToList();

                    model.ListStaffsTax = modelStaff.Where(n => arrStaffIds.Contains(n.Id.ToString()));
                    ViewBag.FailedMessage = "Mã số thuế đã tồn tại";
                }

                return View(model);
            }
            if (ModelState.IsValid)
            {
                //Add Taxperson
                var TaxIncomePerson = new TaxIncomePerson();
                AutoMapper.Mapper.Map(model, TaxIncomePerson);
                TaxIncomePerson.IsDeleted = false;
                TaxIncomePerson.CreatedUserId = WebSecurity.CurrentUserId;
                TaxIncomePerson.ModifiedUserId = WebSecurity.CurrentUserId;
                TaxIncomePerson.AssignedUserId = WebSecurity.CurrentUserId;
                TaxIncomePerson.CreatedDate = DateTime.Now;
                TaxIncomePerson.ModifiedDate = DateTime.Now;


                TaxIncomePersonRepository.InsertTaxIncomePerson(TaxIncomePerson);

                //Add Staffs to Detail
                string staffids = Request["StaffIds"];
                if (!string.IsNullOrEmpty(staffids))
                {
                    string[] arrStaffIds = staffids.Split(',');
                    for (int i = 0; i < arrStaffIds.Count(); i++)
                    {
                        var TaxIncomePersonDetail = new TaxIncomePersonDetail();
                        TaxIncomePersonDetail.IsDeleted = false;
                        TaxIncomePersonDetail.CreatedUserId = WebSecurity.CurrentUserId;
                        TaxIncomePersonDetail.ModifiedUserId = WebSecurity.CurrentUserId;
                        TaxIncomePersonDetail.AssignedUserId = WebSecurity.CurrentUserId;
                        TaxIncomePersonDetail.CreatedDate = DateTime.Now;
                        TaxIncomePersonDetail.ModifiedDate = DateTime.Now;
                        TaxIncomePersonDetail.StaffId = int.Parse(arrStaffIds[i]);
                        TaxIncomePersonDetail.TaxIncomePersonId = TaxIncomePerson.Id;

                        taxIncomePersonDetailRepository.InsertTaxIncomePersonDetail(TaxIncomePersonDetail);
                    }
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
            var TaxIncomePerson = TaxIncomePersonRepository.GetTaxIncomePersonById(Id.Value);
            if (TaxIncomePerson != null && TaxIncomePerson.IsDeleted != true)
            {

                var model = new TaxIncomePersonViewModel();
                AutoMapper.Mapper.Map(TaxIncomePerson, model);

                List<int?> arrStaffIds = taxIncomePersonDetailRepository.GetAllTaxIncomePersonDetail().Where(n => n.TaxIncomePersonId == Id.Value).Select(n => n.StaffId).ToList();
                List<StaffsTaxModel> modelStaff = staffsRepository.GetvwAllStaffs()
                 .Select(item => new StaffsTaxModel
                 {
                     Id = item.Id,
                     Name = item.Name,
                     Code = item.Code,
                     DistrictName = item.DistrictName,
                     BranchId = item.Sale_BranchId,
                     Email = item.Email,
                     EndDate = item.EndDate,
                     StartDate = item.StartDate,
                     Gender = item.Gender,
                     IdCardNumber = item.IdCardNumber,
                     BranchName = item.BranchName,
                     ContryName = item.CountryId,
                     ProvinceName = item.ProvinceName,
                     WardName = item.WardName
                 }).OrderByDescending(m => m.Id).ToList();

                model.ListStaffsTax = modelStaff.Where(n => arrStaffIds.Contains(n.Id));


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
        public ActionResult Edit(TaxIncomePersonViewModel model)
        {

            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var TaxIncomePerson = TaxIncomePersonRepository.GetTaxIncomePersonById(model.Id);
                    var _model = TaxIncomePersonRepository.GetAllTaxIncomePerson().Where(n => n.Code != TaxIncomePerson.Code && n.Code == model.Code);
                    if (_model != null && _model.Count() > 0)
                    {
                        //StaffsTaxModel
                        string staffids = Request["StaffIds"];
                        if (!string.IsNullOrEmpty(staffids))
                        {
                            List<string> arrStaffIds = staffids.Split(',').ToList();
                            List<StaffsTaxModel> modelStaff = staffsRepository.GetvwAllStaffs()
                             .Select(item => new StaffsTaxModel
                             {
                                 Id = item.Id,
                                 Name = item.Name,
                                 Code = item.Code,
                                 DistrictName = item.DistrictName,
                                 BranchId = item.Sale_BranchId,
                                 Email = item.Email,
                                 EndDate = item.EndDate,
                                 StartDate = item.StartDate,
                                 Gender = item.Gender,
                                 IdCardNumber = item.IdCardNumber,
                                 BranchName = item.BranchName,
                                 ContryName = item.CountryId,
                                 ProvinceName = item.ProvinceName,
                                 WardName = item.WardName
                             }).OrderByDescending(m => m.Id).ToList();

                            model.ListStaffsTax = modelStaff.Where(n => arrStaffIds.Contains(n.Id.ToString()));
                            ViewBag.FailedMessage = "Mã số thuế đã tồn tại";
                        }

                        return View(model);
                    }



                    AutoMapper.Mapper.Map(model, TaxIncomePerson);
                    TaxIncomePerson.ModifiedUserId = WebSecurity.CurrentUserId;
                    TaxIncomePerson.ModifiedDate = DateTime.Now;
                    TaxIncomePersonRepository.UpdateTaxIncomePerson(TaxIncomePerson);

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
            var TaxIncomePerson = TaxIncomePersonRepository.GetTaxIncomePersonById(Id.Value);
            if (TaxIncomePerson != null && TaxIncomePerson.IsDeleted != true)
            {
                var model = new TaxIncomePersonViewModel();
                AutoMapper.Mapper.Map(TaxIncomePerson, model);

                List<int?> arrStaffIds = taxIncomePersonDetailRepository.GetAllTaxIncomePersonDetail().Where(n => n.TaxIncomePersonId == Id.Value).Select(n => n.StaffId).ToList();
                List<StaffsTaxModel> modelStaff = staffsRepository.GetvwAllStaffs()
                 .Select(item => new StaffsTaxModel
                 {
                     Id = item.Id,
                     Name = item.Name,
                     Code = item.Code,
                     DistrictName = item.DistrictName,
                     BranchId = item.Sale_BranchId,
                     Email = item.Email,
                     EndDate = item.EndDate,
                     StartDate = item.StartDate,
                     Gender = item.Gender,
                     IdCardNumber = item.IdCardNumber,
                     BranchName = item.BranchName,
                     ContryName = item.CountryId,
                     ProvinceName = item.ProvinceName,
                     WardName = item.WardName
                 }).OrderByDescending(m => m.Id).ToList();

                model.ListStaffsTax = modelStaff.Where(n => arrStaffIds.Contains(n.Id));


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
                    var item = TaxIncomePersonRepository.GetTaxIncomePersonById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        {
                            TempData["FailedMessage"] = "NotOwner";
                            return RedirectToAction("Index");
                        }

                        item.IsDeleted = true;
                        TaxIncomePersonRepository.UpdateTaxIncomePerson(item);
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

        #region Báo cáo - tính toán thuế thu nhập
        //Lấy bảng lương cần tính thuế load vào
        //Lấy danh sách nhân viên đã đăng ký mã số thuế
        public ActionResult Tax(string Code)
        {
            IQueryable<TaxIncomePersonViewModel> q = TaxIncomePersonRepository.GetAllTaxIncomePerson()
                .Select(item => new TaxIncomePersonViewModel
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
                    GeneralTaxationId = item.GeneralTaxationId,
                    GeneralManageId = item.GeneralManageId

                }).OrderByDescending(m => m.ModifiedDate);

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];

            if (!string.IsNullOrEmpty(Code))
            {
                q = q.Where(n => n.Code.Contains(Code));
            }
            return View(q);
        }
        #endregion
    }
}
