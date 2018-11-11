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
using System.Data;
using System.Text.RegularExpressions;
using org.mariuszgromada.math.mxparser;
using Erp.Domain.Staff.Repositories;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Erp.BackOffice.Staff.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class SalarySettingController : Controller
    {
        private readonly ISalarySettingRepository SalarySettingRepository;
        private readonly IUserRepository userRepository;
        private readonly IStaffsRepository staffsRepository;
        private readonly ISalarySettingDetailRepository salarySettingDetailRepository;
        private readonly IBranchDepartmentRepository branchDepartmentRepository;
        private readonly ISalarySettingDetail_StaffRepository salarySettingDetail_StaffRepository;
        private readonly ISettingRepository settingRepository;
        private readonly ISalaryTableDetailReportRepository salaryTableDetailReportRepository;

        public SalarySettingController(
            ISalarySettingRepository _SalarySetting
            , IUserRepository _user
            , IStaffsRepository _Staffs
            , ISalarySettingDetailRepository _SalarySettingDetail
            , IBranchDepartmentRepository branchDepartment
            , ISalarySettingDetail_StaffRepository _SalarySettingDetail_Staff
            , ISettingRepository _Setting
            , ISalaryTableDetailReportRepository _srp
            )
        {
            SalarySettingRepository = _SalarySetting;
            userRepository = _user;
            staffsRepository = _Staffs;
            salarySettingDetailRepository = _SalarySettingDetail;
            branchDepartmentRepository = branchDepartment;
            salarySettingDetail_StaffRepository = _SalarySettingDetail_Staff;
            settingRepository = _Setting;
            salaryTableDetailReportRepository = _srp;
        }

        #region Index
        public ViewResult Index(string txtSearch)
        {
            IQueryable<SalarySettingViewModel> q = SalarySettingRepository.GetAllSalarySetting()
                .Select(item => new SalarySettingViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    IsTemplate = item.IsTemplate
                }).OrderBy(m => m.Name);

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        #endregion

        #region Create
        public ViewResult Create()
        {
            var model = new SalarySettingViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(SalarySettingViewModel model)
        {
            if (ModelState.IsValid)
            {
                var SalarySetting = new SalarySetting();
                AutoMapper.Mapper.Map(model, SalarySetting);
                SalarySetting.IsDeleted = false;
                SalarySetting.CreatedUserId = WebSecurity.CurrentUserId;
                SalarySetting.ModifiedUserId = WebSecurity.CurrentUserId;
                SalarySetting.AssignedUserId = WebSecurity.CurrentUserId;
                SalarySetting.CreatedDate = DateTime.Now;
                SalarySetting.ModifiedDate = DateTime.Now;
                SalarySetting.Name = model.Name;
                SalarySettingRepository.InsertSalarySetting(SalarySetting);

                var salarySettingTemplate = SalarySettingRepository.GetAllSalarySetting()
                    .Where(item => item.IsTemplate).FirstOrDefault();

                if (salarySettingTemplate != null)
                {
                    var listAll = salarySettingDetailRepository.GetAllSalarySettingDetail()
                        .Where(item => item.SalarySettingId == salarySettingTemplate.Id).ToList();
                    var listGroup = listAll.Where(item => item.ParentId == null).ToList();
                    foreach (var group in listGroup)
                    {
                        int groupId = group.Id;
                        group.SalarySettingId = SalarySetting.Id;
                        group.CreatedUserId = WebSecurity.CurrentUserId;
                        group.ModifiedUserId = WebSecurity.CurrentUserId;
                        group.AssignedUserId = WebSecurity.CurrentUserId;
                        group.CreatedDate = DateTime.Now;
                        group.ModifiedDate = DateTime.Now;
                        salarySettingDetailRepository.InsertSalarySettingDetail(group);

                        var subList = listAll.Where(i => i.ParentId == groupId).ToList();
                        foreach (var item in subList)
                        {
                            item.SalarySettingId = SalarySetting.Id;
                            item.ParentId = group.Id;
                            item.CreatedUserId = WebSecurity.CurrentUserId;
                            item.ModifiedUserId = WebSecurity.CurrentUserId;
                            item.AssignedUserId = WebSecurity.CurrentUserId;
                            item.CreatedDate = DateTime.Now;
                            item.ModifiedDate = DateTime.Now;
                            salarySettingDetailRepository.InsertSalarySettingDetail(item);
                        }
                    }
                }

                //TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Edit", new { Id = SalarySetting.Id });
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var SalarySetting = SalarySettingRepository.GetSalarySettingById(Id.Value);
            if (SalarySetting != null && SalarySetting.IsDeleted != true)
            {
                var model = new SalarySettingViewModel();
                AutoMapper.Mapper.Map(SalarySetting, model);

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
        public ActionResult Edit(SalarySettingViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var SalarySetting = SalarySettingRepository.GetSalarySettingById(model.Id);
                    AutoMapper.Mapper.Map(model, SalarySetting);
                    SalarySetting.ModifiedUserId = WebSecurity.CurrentUserId;
                    SalarySetting.ModifiedDate = DateTime.Now;
                    SalarySettingRepository.UpdateSalarySetting(SalarySetting);

                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                    return RedirectToAction("Index");
                }

                return View(model);
            }

            return View(model);
        }

        public ActionResult EditValue(int? Id)
        {
            var SalarySetting = SalarySettingRepository.GetSalarySettingById(Id.Value);
            if (SalarySetting != null && SalarySetting.IsDeleted != true)
            {
                var model = new SalarySettingViewModel();
                AutoMapper.Mapper.Map(SalarySetting, model);
                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditValue(SalarySettingViewModel model)
        {
            var urlRefer = Request["UrlReferrer"];
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var SalarySetting = SalarySettingRepository.GetSalarySettingById(model.Id);
                    AutoMapper.Mapper.Map(model, SalarySetting);
                    SalarySetting.ModifiedUserId = WebSecurity.CurrentUserId;
                    SalarySetting.ModifiedDate = DateTime.Now;
                    SalarySettingRepository.UpdateSalarySetting(SalarySetting);

                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;

                    if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                    {
                        TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                        ViewBag.closePopup = "true";
                        ViewBag.urlRefer = urlRefer;
                        return View(model);
                    }

                    return View("Index");
                }

                return View(model);
            }

            return View(model);
        }


        [HttpPost]
        public ActionResult Update(int Id)
        {
            Domain.Helper.SqlHelper.ExecuteSQL("update Staff_SalarySetting set IsTemplate = 0");
            var salarySetting = SalarySettingRepository.GetSalarySettingById(Id);
            salarySetting.IsTemplate = true;
            SalarySettingRepository.UpdateSalarySetting(salarySetting);

            return Content("success");
        }

        #endregion

        #region Detail
        public ActionResult Calculate(int id, string input)
        {
            var q = salarySettingDetailRepository.GetAllSalarySettingDetail()
                .Where(item => item.SalarySettingId == id && item.IsGroup == false && item.IsDisplay)
                .ToList();

            List<SalarySettingDetailViewModel> ListAllColumns = new List<SalarySettingDetailViewModel>();
            AutoMapper.Mapper.Map(q, ListAllColumns);

            //Dữ liệu hệ thống 
            var du_lieu_setting = settingRepository.GetAlls()
               .Where(item => item.Code == "staff_salary")
               .OrderBy(item => item.Note).ToList();
            string folink = "";
            Expression e = new Expression(Erp.BackOffice.Staff.Controllers.SalaryTableController.BuildFormula(ListAllColumns, du_lieu_setting, input,ref folink ,true));
            if (e.checkSyntax())
                return Content("success");
            else
                return Content(e.getErrorMessage());
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
                    var item = SalarySettingRepository.GetSalarySettingById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        //if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        //{
                        //    TempData["FailedMessage"] = "NotOwner";
                        //    return RedirectToAction("Index");
                        //}

                        item.IsDeleted = true;
                        SalarySettingRepository.UpdateSalarySetting(item);
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

        #region Index
        public ViewResult Config()
        {
            var q = settingRepository.GetAlls()
                .Where(item => item.Code == "staff_salary")
                .OrderBy(item => item.Note).ToList();

            List<Erp.BackOffice.Areas.Administration.Models.SettingViewModel> model = new List<Areas.Administration.Models.SettingViewModel>();
            AutoMapper.Mapper.Map(q, model);

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(model);
        }
        #endregion

        #region Duplicate
        public ActionResult Duplicate(int? Id)
        {
            //Comment: Id = SalarySetting
            var SalarySetting = SalarySettingRepository.GetSalarySettingById(Id.Value);
            if (SalarySetting != null && SalarySetting.IsDeleted != true)
            {
                var model = new SalarySettingViewModel();
                AutoMapper.Mapper.Map(SalarySetting, model);

                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Duplicate(SalarySettingViewModel model)
        {


            if (ModelState.IsValid)
            {
                var SalarySetting = new SalarySetting();
                AutoMapper.Mapper.Map(model, SalarySetting);
                SalarySetting.IsDeleted = false;
                SalarySetting.CreatedUserId = WebSecurity.CurrentUserId;
                SalarySetting.ModifiedUserId = WebSecurity.CurrentUserId;
                SalarySetting.AssignedUserId = WebSecurity.CurrentUserId;
                SalarySetting.CreatedDate = DateTime.Now;
                SalarySetting.ModifiedDate = DateTime.Now;
                SalarySetting.Name = model.Name;
                SalarySettingRepository.InsertSalarySetting(SalarySetting);

                var salarySettingTemplate = SalarySettingRepository.GetAllSalarySetting()
                    .Where(item => item.Id == model.Id).FirstOrDefault();

                if (salarySettingTemplate != null)
                {
                    var listAll = salarySettingDetailRepository.GetAllSalarySettingDetail()
                        .Where(item => item.SalarySettingId == salarySettingTemplate.Id).ToList();
                    var listGroup = listAll.Where(item => item.ParentId == null).ToList();
                    foreach (var group in listGroup)
                    {
                        int groupId = group.Id;
                        group.SalarySettingId = SalarySetting.Id;
                        group.CreatedUserId = WebSecurity.CurrentUserId;
                        group.ModifiedUserId = WebSecurity.CurrentUserId;
                        group.AssignedUserId = WebSecurity.CurrentUserId;
                        group.CreatedDate = DateTime.Now;
                        group.ModifiedDate = DateTime.Now;
                        salarySettingDetailRepository.InsertSalarySettingDetail(group);

                        var subList = listAll.Where(i => i.ParentId == groupId).ToList();
                        foreach (var item in subList)
                        {
                            item.SalarySettingId = SalarySetting.Id;
                            item.ParentId = group.Id;
                            item.CreatedUserId = WebSecurity.CurrentUserId;
                            item.ModifiedUserId = WebSecurity.CurrentUserId;
                            item.AssignedUserId = WebSecurity.CurrentUserId;
                            item.CreatedDate = DateTime.Now;
                            item.ModifiedDate = DateTime.Now;
                            salarySettingDetailRepository.InsertSalarySettingDetail(item);
                        }
                    }
                }

                var newModel = SalarySettingRepository.GetAllSalarySetting().OrderByDescending(n => n.Id).SingleOrDefault(n => n.Name.Contains(model.Name) && n.CreatedUserId == WebSecurity.CurrentUserId && n.AssignedUserId == WebSecurity.CurrentUserId);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                var _model = new SalarySettingViewModel();
                AutoMapper.Mapper.Map(newModel, _model);
                var urlRefer = @Url.Action("Edit", "SalarySetting", new { Id = _model.Id });
                if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                {
                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                    ViewBag.closePopup = "true";
                    ViewBag.urlRefer = urlRefer;
                    return View(_model);
                }

                return RedirectToAction("Edit", new { Id = SalarySetting.Id });
            }
            return View(model);
        }
        #endregion
    }
}
