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
using Erp.BackOffice.Sale.Models;
using Erp.Domain.Sale.Interfaces;
using System.Drawing;
using System.Web;
namespace Erp.BackOffice.Staff.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class WelfareProgramsController : Controller
    {
        private readonly IWelfareProgramsRepository WelfareProgramsRepository;
        private readonly IUserRepository userRepository;
        private readonly IWelfareProgramsDetailRepository WelfareProgramsDetailRepository;
        private readonly ITemplatePrintRepository templatePrintRepository;
        public WelfareProgramsController(
            IWelfareProgramsRepository _WelfarePrograms
            , IUserRepository _user
            ,IWelfareProgramsDetailRepository welfareProgramsDetail
            , ITemplatePrintRepository templetePrint
            )
        {
            WelfareProgramsRepository = _WelfarePrograms;
            userRepository = _user;
            WelfareProgramsDetailRepository = welfareProgramsDetail;
            templatePrintRepository = templetePrint;
        }

        #region Index

        public ViewResult Index(string Code, string Name, string Category, string Status)
        {

            IQueryable<WelfareProgramsViewModel> q = WelfareProgramsRepository.GetAllWelfarePrograms()
                .Select(item => new WelfareProgramsViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    Address=item.Address,
                    ApplicationObject=item.ApplicationObject,
                    Category=item.Category,
                    Formality=item.Formality,
                    ImplementationEndDate=item.ImplementationEndDate,
                    ImplementationStartDate=item.ImplementationStartDate,
                    MoneyCompany=item.MoneyCompany,
                    MoneyStaff=item.MoneyStaff,
                    Note=item.Note,
                    ProvideEndDate=item.ProvideEndDate,
                    ProvideStartDate=item.ProvideStartDate,
                    Purpose=item.Purpose,
                    Quantity=item.Quantity,
                    RegistrationEndDate=item.RegistrationEndDate,
                    RegistrationStartDate=item.RegistrationStartDate,
                    TotalActualCosts=item.TotalActualCosts,
                    Status=item.Status,
                    TotalEstimatedCost=item.TotalEstimatedCost,
                    TotalMoneyCompany=item.TotalMoneyCompany,
                    TotalMoneyStaff=item.TotalMoneyStaff,
                    TotalStaffCompany=item.TotalStaffCompany,
                    TotalStaffCompanyAll=item.TotalStaffCompanyAll,
                    Code=item.Code
                }).OrderByDescending(m => m.ModifiedDate);
            if(!string.IsNullOrEmpty(Code))
            {
                q = q.Where(x => x.Code.Contains(Code));
            }
            if (!string.IsNullOrEmpty(Name))
            {
                q = q.Where(x => x.Name.Contains(Name));
            }
            if (!string.IsNullOrEmpty(Category))
            {
                q = q.Where(x => x.Category == Category);
            }
            if (!string.IsNullOrEmpty(Status))
            {
                q = q.Where(x => x.Status == Status);
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
            var model = new WelfareProgramsViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(WelfareProgramsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var WelfarePrograms = new WelfarePrograms();
                AutoMapper.Mapper.Map(model, WelfarePrograms);
                WelfarePrograms.IsDeleted = false;
                WelfarePrograms.CreatedUserId = WebSecurity.CurrentUserId;
                WelfarePrograms.ModifiedUserId = WebSecurity.CurrentUserId;
                WelfarePrograms.AssignedUserId = WebSecurity.CurrentUserId;
                WelfarePrograms.CreatedDate = DateTime.Now;
                WelfarePrograms.ModifiedDate = DateTime.Now;
                WelfarePrograms.Status = "pending";
                WelfareProgramsRepository.InsertWelfarePrograms(WelfarePrograms);
                string prefixOutbound = Helpers.Common.GetSetting("prefixOrderNo_WelfarePrograms");
                WelfarePrograms.Code = Helpers.Common.GetCode(prefixOutbound, WelfarePrograms.Id);
                WelfareProgramsRepository.UpdateWelfarePrograms(WelfarePrograms);
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var WelfarePrograms = WelfareProgramsRepository.GetWelfareProgramsById(Id.Value);
            if (WelfarePrograms != null && WelfarePrograms.IsDeleted != true)
            {
                var model = new WelfareProgramsViewModel();
                AutoMapper.Mapper.Map(WelfarePrograms, model);
                
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
        public ActionResult Edit(WelfareProgramsViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var WelfarePrograms = WelfareProgramsRepository.GetWelfareProgramsById(model.Id);
                    AutoMapper.Mapper.Map(model, WelfarePrograms);
                    WelfarePrograms.ModifiedUserId = WebSecurity.CurrentUserId;
                    WelfarePrograms.ModifiedDate = DateTime.Now;
                    WelfareProgramsRepository.UpdateWelfarePrograms(WelfarePrograms);

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
            var WelfarePrograms = WelfareProgramsRepository.GetWelfareProgramsById(Id.Value);
            
            if (WelfarePrograms != null && WelfarePrograms.IsDeleted != true)
            {
                var model = new WelfareProgramsViewModel();
                AutoMapper.Mapper.Map(WelfarePrograms, model);
                model.ListStaff = new List<WelfareProgramsDetailViewModel>();
                if (model.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                {
                    TempData["FailedMessage"] = "NotOwner";
                    return RedirectToAction("Index");
                }
                var staff = WelfareProgramsDetailRepository.GetAllvwWelfareProgramsDetail().Where(x => x.WelfareProgramsId == model.Id);
                if (staff.ToList().Count > 0)
                {
                    model.ListStaff = staff.Select(item => new WelfareProgramsDetailViewModel
                    {
                        Id = item.Id,
                        CreatedUserId = item.CreatedUserId,
                        //CreatedUserName = item.CreatedUserName,
                        CreatedDate = item.CreatedDate,
                        ModifiedUserId = item.ModifiedUserId,
                        //ModifiedUserName = item.ModifiedUserName,
                        ModifiedDate = item.ModifiedDate,
                        Note = item.Note,
                        StaffId = item.StaffId,
                        RegistrationDate = item.RegistrationDate,
                        StaffName = item.StaffName,
                        WelfareProgramsId = item.WelfareProgramsId
                    }).ToList();
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
                    var item = WelfareProgramsRepository.GetWelfareProgramsById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if(item != null)
                    {
                        if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        {
                            TempData["FailedMessage"] = "NotOwner";
                            return RedirectToAction("Index");
                        }

                        item.IsDeleted = true;
                        WelfareProgramsRepository.UpdateWelfarePrograms(item);
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
        #region Approval
        public ActionResult Approval(int Id)
        {
            var q = WelfareProgramsRepository.GetWelfareProgramsById(Id);
            if (q.Status == "pending")
            {
                q.Status ="inprogress";
                WelfareProgramsRepository.UpdateWelfarePrograms(q);
            }
            return RedirectToAction("Detail", "WelfarePrograms", new { area = "Staff", Id = Id });
        }
        #endregion

        #region Complete
        public ActionResult Complete(int Id)
        {
            var q = WelfareProgramsRepository.GetWelfareProgramsById(Id);
            if (q != null && q.IsDeleted != true)
            {
                var model = new WelfareProgramsViewModel();
                AutoMapper.Mapper.Map(q, model);
                model.ImplementationStartDate =model.ProvideStartDate;
                model.ImplementationEndDate = model.ProvideEndDate;
                model.TotalActualCosts = model.TotalStaffCompanyAll;
                return View(model);
            }
            return RedirectToAction("Detail", "WelfarePrograms", new { area = "Staff", Id = Id });
        }
        [HttpPost]
        public ActionResult Complete(WelfareProgramsViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var q = WelfareProgramsRepository.GetWelfareProgramsById(model.Id);
                    q.Status ="complete";
                    q.ImplementationStartDate = model.ImplementationStartDate;
                    q.ImplementationEndDate = model.ImplementationEndDate;
                    q.TotalActualCosts = model.TotalActualCosts;
                    WelfareProgramsRepository.UpdateWelfarePrograms(q);
                    //TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.CancelSuccess + " " + q.Code;
                    if (Request["IsPopup"]=="true"|| Request["IsPopup"] == "True")
                    {
                        return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
                    }
                }
                return RedirectToAction("Detail", "WelfarePrograms", new { area = "Staff", Id = model.Id });
            }
          
            return RedirectToAction("Detail", "WelfarePrograms", new { area = "Staff", Id = model.Id });
        }
        #endregion

        #region Print
        public ActionResult Print(int Id, bool ExportExcel = false)
        {
            var model = new TemplatePrintViewModel();
            //lấy logo công ty
            var logo = Erp.BackOffice.Helpers.Common.GetSetting("LogoCompany");
            var company = Erp.BackOffice.Helpers.Common.GetSetting("companyName");
            var address = Erp.BackOffice.Helpers.Common.GetSetting("addresscompany");
            var phone = Erp.BackOffice.Helpers.Common.GetSetting("phonecompany");
            var fax = Erp.BackOffice.Helpers.Common.GetSetting("faxcompany");
            var bankcode = Erp.BackOffice.Helpers.Common.GetSetting("bankcode");
            var bankname = Erp.BackOffice.Helpers.Common.GetSetting("bankname");
            var ImgLogo = "<div class=\"logo\"><img src=" + logo + " height=\"73\" /></div>";
            //lấy hóa đơn.
            var aa = WelfareProgramsRepository.GetWelfareProgramsById(Id);
            var user = userRepository.GetUserById(aa.CreatedUserId.Value);

            //lấy thông tin khách hàng
            var staff = WelfareProgramsDetailRepository.GetAllvwWelfareProgramsDetail().Where(x => x.WelfareProgramsId == aa.Id).ToList();
            var ProvideDate = aa.ProvideStartDate.Value.ToString("dd/MM/yyyy HH:mm") + " - " + aa.ProvideEndDate.Value.ToString("dd/MM/yyyy HH:mm");
            var RegistrationDate = aa.RegistrationStartDate.Value.ToString("dd/MM/yyyy HH:mm") + " - " + aa.RegistrationEndDate.Value.ToString("dd/MM/yyyy HH:mm");
            //lấy template phiếu xuất.
            var template = templatePrintRepository.GetAllTemplatePrint().Where(x => x.Code.Contains("WelfarePrograms")).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            //truyền dữ liệu vào template.
            model.Content = template.Content;
            model.Content = model.Content.Replace("{WelfareCode}", aa.Code);
            model.Content = model.Content.Replace("{WelfareName}", aa.Name);
            model.Content = model.Content.Replace("{ProvideDate}", ProvideDate);
            model.Content = model.Content.Replace("{RegistrationDate}", RegistrationDate);
            model.Content = model.Content.Replace("{Address}", aa.Address);
            model.Content = model.Content.Replace("{Category}", aa.Category);
            model.Content = model.Content.Replace("{StaffCreateName}", user.FullName);

            model.Content = model.Content.Replace("{DataTable}", buildHtmlDetailList(staff));
            model.Content = model.Content.Replace("{System.Logo}", ImgLogo);
            model.Content = model.Content.Replace("{System.CompanyName}", company);
            model.Content = model.Content.Replace("{System.AddressCompany}", address);
            model.Content = model.Content.Replace("{System.PhoneCompany}", phone);
            model.Content = model.Content.Replace("{System.Fax}", fax);
            model.Content = model.Content.Replace("{System.BankCodeCompany}", bankcode);
            model.Content = model.Content.Replace("{System.BankNameCompany}", bankname);
            //Tạo barcode
            //Image barcode = Code128Rendering.MakeBarcodeImage(productInvoice.Code, 1, true);
            //model.Content = model.Content.Replace("{BarcodeImgSource}", ImageToBase64(barcode, System.Drawing.Imaging.ImageFormat.Png));
            if (ExportExcel)
            {
                Response.AppendHeader("content-disposition", "attachment;filename=" + "KeHoachPhucLoi_"+aa.Code + ".xls");
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.Write(model.Content);
                Response.End();
            }
            return View(model);
        }

        string buildHtmlDetailList(List<vwWelfareProgramsDetail> detailList)
        {
            //Tạo table html chi tiết phiếu xuất
            string detailLists = "<table class=\"invoice-detail\">\r\n";
            detailLists += "<thead>\r\n";
            detailLists += "	<tr>\r\n";
            detailLists += "		<th>STT</th>\r\n";
            detailLists += "		<th>Mã nhân viên</th>\r\n";
            detailLists += "		<th>Tên nhân viên</th>\r\n";
            detailLists += "		<th>Chức danh</th>\r\n";
            detailLists += "		<th>Điện thoại</th>\r\n";
            detailLists += "		<th>Ghi chú</th>\r\n";
            detailLists += "	</tr>\r\n";
            detailLists += "</thead>\r\n";
            detailLists += "<tbody>\r\n";
            var index = 1;
            var group = detailList.GroupBy(x => x.BranchName).ToList();
            foreach (var ii in group)
            {
                if (!string.IsNullOrEmpty(ii.Key))
                {
                    detailLists += "<tr>\r\n"
                   + "<td class=\"text-left\" colspan=\"6\">" + ii.Key+" ("+"Số lượng nhân viên: "+ii.Count()+")" + "</td>\r\n"
                   + "</tr>\r\n";
                }
                foreach (var item in detailList.Where(x=>x.BranchName==ii.Key).OrderBy(x=>x.StaffName))
                {

                    detailLists += "<tr>\r\n"
                     + "<td class=\"text-center orderNo\">" + (index++) + "</td>\r\n"
                     + "<td class=\"text-left code_product\">" + item.StaffCode + "</td>\r\n"
                     + "<td class=\"text-left \">" + item.StaffName + "</td>\r\n"
                     + "<td class=\"text-left staff\">" + item.PositionName + "</td>\r\n"
                     + "<td class=\"text-right orderNo\">" + item.Phone + "</td>\r\n"
                     + "<td class=\"text-left code_product\">" + item.Note+ "</td>\r\n"
                     + "</tr>\r\n";
                }
            }
            detailLists += "</tbody>\r\n";
     
            detailLists += "</table>\r\n";
          
            return detailLists;
        }

        //public string ImageToBase64(Image image, System.Drawing.Imaging.ImageFormat format)
        //{
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        // Convert Image to byte[]
        //        image.Save(ms, format);
        //        byte[] imageBytes = ms.ToArray();

        //        // Convert byte[] to Base64 String
        //        string base64String = Convert.ToBase64String(imageBytes);
        //        return base64String;
        //    }
        //}
        #endregion
    }
}
