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
using System.Text.RegularExpressions;

namespace Erp.BackOffice.Staff.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class BonusDisciplineController : Controller
    {
        private readonly IBonusDisciplineRepository BonusDisciplineRepository;
        private readonly IUserRepository userRepository;
        private readonly IStaffsRepository staffsRepository;
        public BonusDisciplineController(
            IBonusDisciplineRepository _BonusDiscipline
            , IUserRepository _user
            , IStaffsRepository staffs
            )
        {
            BonusDisciplineRepository = _BonusDiscipline;
            userRepository = _user;
            staffsRepository = staffs;
        }
        #region Index
        public ViewResult Index(string Code, string Name, string Position, int? branchId, int? DepartmentId, string Category)
        {
            var start_date = Request["start_date"];
            var end_date = Request["end_date"];
            var start_DayDecision = Request["start_DayDecision"];
            var end_DayDecision = Request["end_DayDecision"];
            ViewData["Code"] = Code;
            ViewData["Name"] = Name;
            //ViewData["BranchList"] = Helpers.SelectListHelper.GetSelectList_Branch(null);
            //ViewData["DepartmentList"] = Helpers.SelectListHelper.GetSelectList_BranchDepartment(null,0);
            //ViewData["PositionList"] = Helpers.SelectListHelper.GetSelectList_Category("position", null, "Value", false);
            //ViewData["CategoryList"] = Helpers.SelectListHelper.GetSelectList_Category("BonusDiscipline_Category", null, "Value", false);
            var staff = Erp.BackOffice.Helpers.Common.GetStaffByCurrentUser();
            var user = userRepository.GetUserById(WebSecurity.CurrentUserId);
            // var userType = userTypeRepository.GetUserTypeById((int)user.UserTypeId);
            IEnumerable<BonusDisciplineViewModel> q = BonusDisciplineRepository.GetAllvwBonusDiscipline()
                .Select(item => new BonusDisciplineViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Category = item.Category,
                    DayDecision = item.DayDecision,
                    DayEffective = item.DayEffective,
                    Formality = item.Formality,
                    Note = item.Note,
                    PlaceDecisions = item.PlaceDecisions,
                    Reason = item.Reason,
                    StaffId = item.StaffId,
                    Code = item.Code,
                    BranchDepartmentId=item.BranchDepartmentId,
                    BranchName=item.BranchName,
                    CodeName=item.CodeName,
                    Name=item.Name,
                    Position=item.Position,
                    Sale_BranchId=item.Sale_BranchId,
                    Staff_DepartmentId=item.Staff_DepartmentId,
                   Gender=item.Gender,
                   ProfileImage=item.ProfileImage,
                   StaffCode=item.StaffCode,
                   Birthday=item.Birthday,
                   PlaceDecisionsName = item.PlaceDecisionsName
                }).ToList();

            if (!string.IsNullOrEmpty(Code))
            {
                q = q.Where(item => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.Code).ToLower().Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(Code).ToLower()));
            }
            if (!string.IsNullOrEmpty(Name))
            {
                q = q.Where(item => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.CodeName).ToLower().Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(Name).ToLower()));
            }
            if (!string.IsNullOrEmpty(Position))
            {
                q = q.Where(item => item.Position == Position);
            }
            if (branchId != null && branchId.Value > 0)
            {
                q = q.Where(item => item.Sale_BranchId == branchId);
            }
            if (DepartmentId != null && DepartmentId.Value > 0)
            {
                q = q.Where(item => item.BranchDepartmentId == DepartmentId);
            }
            if (!string.IsNullOrEmpty(Category))
            {
                q = q.Where(item => item.Category == Category);
            }
            if (!string.IsNullOrEmpty(start_date) && !string.IsNullOrEmpty(end_date))
            {
                DateTime start_d;
                if (DateTime.TryParseExact(start_date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out start_d))
                {
                    DateTime end_d;
                    if (DateTime.TryParseExact(end_date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out end_d))
                    {
                        end_d = end_d.AddHours(23);
                        q = q.Where(x => start_d <= x.DayEffective && x.DayEffective <= end_d);
                    }
                }
            }
            if (!string.IsNullOrEmpty(start_DayDecision) && !string.IsNullOrEmpty(end_DayDecision))
            {
                DateTime start_d;
                if (DateTime.TryParseExact(start_DayDecision, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out start_d))
                {
                    DateTime end_d;
                    if (DateTime.TryParseExact(end_DayDecision, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out end_d))
                    {
                        end_d = end_d.AddHours(23);
                        q = q.Where(x => start_d <= x.DayDecision && x.DayDecision <= end_d);
                    }
                }
            }

            ViewBag.SuccessMessage = TempData[Globals.SuccessMessageKey];
            ViewBag.FailedMessage = TempData[Globals.FailedMessageKey];
            return View(q);
        }

        #endregion

        #region List

        public ViewResult List(int? StaffId)
        {
            ViewBag.StaffId = StaffId;
            IQueryable<BonusDisciplineViewModel> q = BonusDisciplineRepository.GetAllvwBonusDiscipline().Where(x=>x.StaffId==StaffId)
                .Select(item => new BonusDisciplineViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Category = item.Category,
                    DayDecision = item.DayDecision,
                    DayEffective = item.DayEffective,
                    Formality = item.Formality,
                    Note = item.Note,
                    PlaceDecisions = item.PlaceDecisions,
                    Reason = item.Reason,
                    StaffId = item.StaffId,
                    Code = item.Code,
                    BranchDepartmentId = item.BranchDepartmentId,
                    BranchName = item.BranchName,
                    CodeName = item.CodeName,
                    Name = item.Name,
                    Position = item.Position,
                    Sale_BranchId = item.Sale_BranchId,
                    Staff_DepartmentId = item.Staff_DepartmentId,
                    Gender = item.Gender,
                    ProfileImage = item.ProfileImage,
                    StaffCode = item.StaffCode,
                    Birthday = item.Birthday,
                    PlaceDecisionsName = item.PlaceDecisionsName
                }).OrderByDescending(m => m.ModifiedDate);
            ViewBag.AccessRightCreate = SecurityFilter.AccessRight("Create", "BonusDiscipline", "Staff");
            return View(q);
        }
        #endregion

        #region Create
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id">StaffId</param>
        /// <returns></returns>
        public ViewResult Create(int? Id)
        {
            var model = new BonusDisciplineViewModel();
            if (Id != null && Id.Value > 0)
            {
                model.StaffId = Id;
                var staffs = staffsRepository.GetStaffsById(Id.Value);
                model.Name = staffs.Name;
            }
            //model.StaffList = Helpers.SelectListHelper.GetSelectList_Staff(null);
            //model.CategoryList = Helpers.SelectListHelper.GetSelectList_Category("BonusDiscipline_Category", null, "Value", App_GlobalResources.Wording.Empty);
            var user = userRepository.GetUserById(WebSecurity.CurrentUserId);
            var staff = Erp.BackOffice.Helpers.Common.GetStaffByCurrentUser();
           // model.DepartmentList = Helpers.SelectListHelper.GetSelectList_BranchDepartment(null, staff.Sale_BranchId.HasValue?staff.Sale_BranchId.Value:0,App_GlobalResources.Wording.Empty);
            //model.BranchList = Helpers.SelectListHelper.GetSelectList_Branch(null);
            ViewBag.user = user.UserTypeId;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(BonusDisciplineViewModel model)
        {
            var urlRefer = Request["UrlReferrer"];
            if (ModelState.IsValid)
            {
                var user = userRepository.GetUserById(WebSecurity.CurrentUserId);
                var BonusDiscipline = new Domain.Staff.Entities.BonusDiscipline();
                AutoMapper.Mapper.Map(model, BonusDiscipline);
                BonusDiscipline.IsDeleted = false;
                BonusDiscipline.CreatedUserId = WebSecurity.CurrentUserId;
                BonusDiscipline.ModifiedUserId = WebSecurity.CurrentUserId;
                BonusDiscipline.CreatedDate = DateTime.Now;
                BonusDiscipline.ModifiedDate = DateTime.Now;
                BonusDiscipline.DayDecision = DateTime.Now;
                BonusDisciplineRepository.InsertBonusDiscipline(BonusDiscipline);
                var prefixBonus = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_Bonus");
                var prefixDiscipline = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_Discipline");
                if (BonusDiscipline.Category == "Bonus")
                {
                    BonusDiscipline.Code = Erp.BackOffice.Helpers.Common.GetCode(prefixBonus, BonusDiscipline.Id);
                    BonusDisciplineRepository.UpdateBonusDiscipline(BonusDiscipline);
                }
                else
                {
                    BonusDiscipline.Code = Erp.BackOffice.Helpers.Common.GetCode(prefixDiscipline, BonusDiscipline.Id);
                    BonusDisciplineRepository.UpdateBonusDiscipline(BonusDiscipline);
                }
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess + " " + BonusDiscipline.Code;
                if (Request["IsPopup"] == "true")
                {
                    return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
                }
                return Redirect(urlRefer);
            }
            TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.InsertUnsucess;
            return RedirectToAction("Index");
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var BonusDiscipline = BonusDisciplineRepository.GetvwBonusDisciplineById(Id.Value);
            if (BonusDiscipline != null && BonusDiscipline.IsDeleted != true)
            {
                var model = new BonusDisciplineViewModel();
                AutoMapper.Mapper.Map(BonusDiscipline, model);
               // model.StaffList = Helpers.SelectListHelper.GetSelectList_Staff();
                model.CategoryList = Helpers.SelectListHelper.GetSelectList_Category("BonusDiscipline_Category", null, "Value",App_GlobalResources.Wording.Empty);
                var user = userRepository.GetUserById(WebSecurity.CurrentUserId);
                var staff = Erp.BackOffice.Helpers.Common.GetStaffByCurrentUser();
                model.DepartmentList = Helpers.SelectListHelper.GetSelectList_BranchDepartment(model.PlaceDecisions,model.PlaceDecisions_Branch.Value, App_GlobalResources.Wording.Empty);
                model.BranchList = Helpers.SelectListHelper.GetSelectList_Branch(model.PlaceDecisions_Branch, App_GlobalResources.Wording.Empty);
                if (model.Category == "Bonus")
                {
                    model.ReasonList = Helpers.SelectListHelper.GetSelectList_Category("Reason_Bonus", model.Reason, "Value", App_GlobalResources.Wording.Empty);
                    model.FormalityList = Helpers.SelectListHelper.GetSelectList_Category("Formality_Bonus", model.Formality, "Value", App_GlobalResources.Wording.Empty);
                }
                else
                {
                    model.ReasonList = Helpers.SelectListHelper.GetSelectList_Category("Reason_Discipline", model.Reason, "Value", App_GlobalResources.Wording.Empty);
                    model.FormalityList = Helpers.SelectListHelper.GetSelectList_Category("Formality_Discipline", model.Formality, "Value", App_GlobalResources.Wording.Empty);
                }
                ViewBag.user = user.UserTypeId;
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
        public ActionResult Edit(BonusDisciplineViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var BonusDiscipline = BonusDisciplineRepository.GetBonusDisciplineById(model.Id);
                    AutoMapper.Mapper.Map(model, BonusDiscipline);
                    BonusDiscipline.ModifiedUserId = WebSecurity.CurrentUserId;
                    BonusDiscipline.ModifiedDate = DateTime.Now;
                    var prefixBonus = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_Bonus");
                    var prefixDiscipline = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_Discipline");
                    if (BonusDiscipline.Category == "Bonus")
                    {
                        BonusDiscipline.Code = Erp.BackOffice.Helpers.Common.GetCode(prefixBonus, model.Id);
                    }
                    else
                    {
                        BonusDiscipline.Code = Erp.BackOffice.Helpers.Common.GetCode(prefixDiscipline, model.Id);
                    }
                    BonusDisciplineRepository.UpdateBonusDiscipline(BonusDiscipline);
                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess + " " + BonusDiscipline.Code;
                    if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                    {
                        ViewBag.closePopup = "true";
                        model.CategoryList = Helpers.SelectListHelper.GetSelectList_Category("BonusDiscipline_Category", null, "Value", App_GlobalResources.Wording.Empty);
                        var user = userRepository.GetUserById(WebSecurity.CurrentUserId);
                        var staff = Erp.BackOffice.Helpers.Common.GetStaffByCurrentUser();
                        model.DepartmentList = Helpers.SelectListHelper.GetSelectList_BranchDepartment(model.PlaceDecisions, model.PlaceDecisions_Branch.Value, App_GlobalResources.Wording.Empty);
                        model.BranchList = Helpers.SelectListHelper.GetSelectList_Branch(model.PlaceDecisions_Branch, App_GlobalResources.Wording.Empty);
                        if (model.Category == "Bonus")
                        {
                            model.ReasonList = Helpers.SelectListHelper.GetSelectList_Category("Reason_Bonus", model.Reason, "Value", App_GlobalResources.Wording.Empty);
                            model.FormalityList = Helpers.SelectListHelper.GetSelectList_Category("Formality_Bonus", model.Formality, "Value", App_GlobalResources.Wording.Empty);
                        }
                        else
                        {
                            model.ReasonList = Helpers.SelectListHelper.GetSelectList_Category("Reason_Discipline", model.Reason, "Value", App_GlobalResources.Wording.Empty);
                            model.FormalityList = Helpers.SelectListHelper.GetSelectList_Category("Formality_Discipline", model.Formality, "Value", App_GlobalResources.Wording.Empty);
                        }
                        ViewBag.user = user.UserTypeId;
                        
                        return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
                       // return View(model);
                    }
                    return RedirectToAction("Index");
                }
            }
            TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.UpdateUnsuccess + " " + model.Code;
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
                string code = "";
                for (int i = 0; i < arrDeleteId.Count(); i++)
                {
                    var item = BonusDisciplineRepository.GetBonusDisciplineById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        //if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                        //{
                        //    TempData["FailedMessage"] = "NotOwner";
                        //    return RedirectToAction("Index");
                        //}

                        item.IsDeleted = true;
                        BonusDisciplineRepository.UpdateBonusDiscipline(item);
                        code += item.Code+" ; ";
                    }
                }
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.DeleteSuccess+" "+code;
                return RedirectToAction("Index");
            }
            catch (DbUpdateException)
            {
                TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.RelationError;
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region Detail
        public ActionResult Detail(int? Id)
        {
            var bonusDiscipline = BonusDisciplineRepository.GetvwBonusDisciplineById(Id.Value);
            if (bonusDiscipline != null && bonusDiscipline.IsDeleted != true)
            {
                var model = new BonusDisciplineViewModel();
                AutoMapper.Mapper.Map(bonusDiscipline, model);
                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
