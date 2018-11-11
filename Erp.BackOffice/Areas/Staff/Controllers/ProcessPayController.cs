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
using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;

namespace Erp.BackOffice.Staff.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class ProcessPayController : Controller
    {
        private readonly IProcessPayRepository ProcessPayRepository;
        private readonly IUserRepository userRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly ITemplatePrintRepository templatePrintRepository;
        private readonly IStaffsRepository staffsRepository;
        public ProcessPayController(
            IProcessPayRepository _ProcessPay
            , IUserRepository _user
            ,ICategoryRepository category
            ,ITemplatePrintRepository templatePrint
            ,IStaffsRepository staff
            )
        {
            ProcessPayRepository = _ProcessPay;
            userRepository = _user;
            categoryRepository = category;
            templatePrintRepository = templatePrint;
            staffsRepository = staff;
        }

        #region Index

        public ViewResult Index(int? StaffId)
        {
            ViewBag.StaffId = StaffId;
            IQueryable<ProcessPayViewModel> q = ProcessPayRepository.GetAllProcessPay().Where(x=>x.StaffId==StaffId)
                .Select(item => new ProcessPayViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    DayDecision=item.DayDecision,
                    DayEffective=item.DayEffective,
                    StaffId=item.StaffId,
                    CodePay = item.CodePay,
                    BasicPay=item.BasicPay
                }).OrderByDescending(m => m.ModifiedDate);
            ViewBag.AccessRightCreate = SecurityFilter.AccessRight("Create", "ProcessPay", "Staff");
            return View(q);
        }
        #endregion

        #region Create
        public ViewResult Create(int? StaffId)
        {
            var model = new ProcessPayViewModel();
            model.StaffId = StaffId;
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ProcessPayViewModel model)
        {
            if (ModelState.IsValid)
            {
                var ProcessPay = new Domain.Staff.Entities.ProcessPay();
                AutoMapper.Mapper.Map(model, ProcessPay);
                ProcessPay.IsDeleted = false;
                ProcessPay.CreatedUserId = WebSecurity.CurrentUserId;
                ProcessPay.ModifiedUserId = WebSecurity.CurrentUserId;
                ProcessPay.CreatedDate = DateTime.Now;
                ProcessPay.ModifiedDate = DateTime.Now;
              
                ProcessPayRepository.InsertProcessPay(ProcessPay);
                var prefix1 = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_ProcessPay");
                ProcessPay.CodePay = Erp.BackOffice.Helpers.Common.GetCode(prefix1, ProcessPay.Id);
                ProcessPayRepository.UpdateProcessPay(ProcessPay);
                if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                {
                    ViewBag.closePopup = "true";
                    model.Id = ProcessPay.Id;
                   
                    return View(model);
                }
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var ProcessPay = ProcessPayRepository.GetProcessPayById(Id.Value);
            if (ProcessPay != null && ProcessPay.IsDeleted != true)
            {
                var model = new ProcessPayViewModel();
                AutoMapper.Mapper.Map(ProcessPay, model);
              
                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ProcessPayViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var ProcessPay = ProcessPayRepository.GetProcessPayById(model.Id);
                    AutoMapper.Mapper.Map(model, ProcessPay);
                    ProcessPay.ModifiedUserId = WebSecurity.CurrentUserId;
                    ProcessPay.ModifiedDate = DateTime.Now;
                 
                    ProcessPayRepository.UpdateProcessPay(ProcessPay);
                    if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                    {
                        ViewBag.closePopup = "true";

                        return View(model);
                    }
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
        public ActionResult Delete(int? id)
        {
            try
            {

                var item = ProcessPayRepository.GetProcessPayById(id.Value);
                if (item != null)
                {
                    //if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                    //{
                    //    TempData["FailedMessage"] = "NotOwner";
                    //    return RedirectToAction("Index");
                    //}
                    //item.IsDeleted = true;
                    //TechniqueRepository.UpdateTechnique(item);
                    ProcessPayRepository.DeleteProcessPay(id.Value);
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

        //custom them mới khi thêm hợp đồng lao động
        #region Create custom
        public static void CreateProcessPay(int? StaffId, int? WageAgreement, DateTime? DayEffective)
        {
            Erp.Domain.Staff.Repositories.ProcessPayRepository processPayRepository = new Erp.Domain.Staff.Repositories.ProcessPayRepository(new Domain.Staff.ErpStaffDbContext());
            Erp.Domain.Repositories.CategoryRepository categoryRepository = new Erp.Domain.Repositories.CategoryRepository(new Domain.ErpDbContext());
            var processpay = new ProcessPay();
            processpay.IsDeleted = false;
            processpay.CreatedUserId = WebSecurity.CurrentUserId;
            processpay.ModifiedUserId = WebSecurity.CurrentUserId;
            processpay.CreatedDate = DateTime.Now;
            processpay.ModifiedDate = DateTime.Now;
            processpay.StaffId = StaffId;
            processpay.DayEffective = DayEffective;
            processPayRepository.InsertProcessPay(processpay);
            var prefix1 = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_ProcessPay");
            processpay.CodePay = Erp.BackOffice.Helpers.Common.GetCode(prefix1, processpay.Id);
            processPayRepository.UpdateProcessPay(processpay);
        }
        #endregion

        public ContentResult ContentProcessPay(string Code, int? StaffId, string DayDecision, string DayEffective, int BasicPay)
        {
            var model = new ProcessPayViewModel();
            model.CodePay = Code;
            model.StaffId = StaffId;
            model.DayDecision = Convert.ToDateTime(DayDecision);
            model.DayEffective = Convert.ToDateTime(DayEffective);
            model.BasicPay = BasicPay;
            model.Content = bill(model);
            return Content(model.Content);
        }
        string bill(ProcessPayViewModel model)
        {

            //lấy thông tin
            var company = Erp.BackOffice.Helpers.Common.GetSetting("companyName");
            //var BranchName = Erp.BackOffice.Helpers.Common.CurrentUser.BranchName;
            var staff=staffsRepository.GetStaffsById(model.StaffId.Value);
            var address = Erp.BackOffice.Helpers.Common.GetSetting("addresscompany");
            var phone = Erp.BackOffice.Helpers.Common.GetSetting("phonecompany");
            var fax = Erp.BackOffice.Helpers.Common.GetSetting("faxcompany");
            //lấy template phiếu xuất.
            var template = templatePrintRepository.GetAllTemplatePrint().Where(x => x.Code.Contains("ProcessPay")).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            //truyền dữ liệu vào template.
            model.Content = template.Content;
            model.Content = model.Content.Replace("{StaffName}", staff.Name);
            //model.Content = model.Content.Replace("{Code}", staff.Name);
            model.Content = model.Content.Replace("{Day}", model.DayDecision.Value.Day.ToString());
            model.Content = model.Content.Replace("{Month}", model.DayDecision.Value.Month.ToString());
            model.Content = model.Content.Replace("{Year}", model.DayDecision.Value.Year.ToString());

            model.Content = model.Content.Replace("{EffectiveDay}", model.DayEffective.Value.Day.ToString());
            model.Content = model.Content.Replace("{EffectiveMonth}", model.DayEffective.Value.Month.ToString());
            model.Content = model.Content.Replace("{EffectiveYear}", model.DayEffective.Value.Year.ToString());
            model.Content = model.Content.Replace("{BasicPay}", Erp.BackOffice.Helpers.Common.PhanCachHangNgan(model.BasicPay));
            model.Content = model.Content.Replace("{BasicPayText}", Erp.BackOffice.Helpers.Common.ChuyenSoThanhChu(model.BasicPay.Value.ToString()));

            model.Content = model.Content.Replace("{System.AddressCompany}", address);
            model.Content = model.Content.Replace("{System.PhoneCompany}", phone);
            model.Content = model.Content.Replace("{System.Fax}", fax);
            model.Content = model.Content.Replace("{System.CompanyName}", company);
            //model.Content = model.Content.Replace("{System.AddressCompany}", BranchName);
            return model.Content;
        }

        #region Detail
        public ActionResult Detail(int? Id)
        {
            var ProcessPay = ProcessPayRepository.GetProcessPayById(Id.Value);
            if (ProcessPay != null && ProcessPay.IsDeleted != true)
            {
                var model = new ProcessPayViewModel();
                AutoMapper.Mapper.Map(ProcessPay, model);
                var staff= staffsRepository.GetvwStaffsById(model.StaffId.Value);
                AutoMapper.Mapper.Map(staff, model.model_Staff);
                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }
        #endregion
        public ActionResult Print(int? Id)
        {
            var ProcessPay = ProcessPayRepository.GetProcessPayById(Id.Value);
            if (ProcessPay != null && ProcessPay.IsDeleted != true)
            {
                var model = new ProcessPayViewModel();
                AutoMapper.Mapper.Map(ProcessPay, model);
                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }
    }
}
