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
using System.Web.Hosting;
using System.Web;
using System.IO;
using Erp.BackOffice.Sale.Models;
using System.Web.Script.Serialization;

namespace Erp.BackOffice.Staff.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class LabourContractController : Controller
    {
        private readonly ILabourContractRepository LabourContractRepository;
        private readonly IUserRepository userRepository;
        private readonly IObjectAttributeRepository ObjectAttributeRepository;
        private readonly IStaffsRepository staffRepository;
        private readonly ILabourContractTypeRepository LabourContractTypeRepository;
        private readonly IDocumentFieldRepository DocumentFieldRepository;
        private readonly IDocumentAttributeRepository DocumentAttributeRepository;
        private readonly IProcessPayRepository processPayRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IBranchRepository branchRepository;
        private readonly ITemplatePrintRepository templatePrintRepository;
        public LabourContractController(
            ILabourContractRepository _LabourContract
            , IUserRepository _user
            , IObjectAttributeRepository objectAttribute
            , IStaffsRepository staff
            , ILabourContractTypeRepository LabourContractType
            , IDocumentFieldRepository documentField
            , IDocumentAttributeRepository documentAttribute
            ,IProcessPayRepository processpay
            ,ICategoryRepository category
            ,IBranchRepository branch
            , ITemplatePrintRepository templatePrint
            )
        {
            LabourContractRepository = _LabourContract;
            userRepository = _user;
            ObjectAttributeRepository = objectAttribute;
            staffRepository = staff;
            LabourContractTypeRepository = LabourContractType;
            DocumentFieldRepository = documentField;
            DocumentAttributeRepository = documentAttribute;
            processPayRepository = processpay;
            categoryRepository = category;
            branchRepository = branch;
            templatePrintRepository = templatePrint;
        }

        #region Index

        public ViewResult Index(string Code, string Staff, string Status, int? Type, string FormWork, string NameStaff, SearchObjectAttributeViewModel SearchOjectAttr)
        {
            var staff = Erp.BackOffice.Helpers.Common.GetStaffByCurrentUser();
            IEnumerable<LabourContractViewModel> q = LabourContractRepository.GetAllvwLabourContract()
                .Select(item => new LabourContractViewModel
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
                    Status = item.Status,
                    Type = item.Type,
                    StaffbranchId = item.StaffbranchId,
                    FormWork = item.FormWork,
                    CreatedUserName = item.CreatedUserName,
                    ContractTypeName = item.ContractTypeName,
                    QuantityMonth = item.QuantityMonth,
                    Notice = item.Notice,
                    EffectiveDate = item.EffectiveDate,
                    ExpiryDate = item.ExpiryDate,
                    StaffName = item.StaffName,
                    StaffCode = item.StaffCode,
                    StaffBirthday=item.StaffBirthday,
                    StaffId=item.StaffId,
                    StaffProfileImage=item.StaffProfileImage,
                    ApprovedUserId=item.ApprovedUserId,
                    ApprovedUserName=item.ApprovedUserName,
                    WageAgreement=item.WageAgreement,
                    SignedDay=item.SignedDay,
                    StaffBranchName=item.StaffBranchName,
                    StaffDepartmentName=item.StaffDepartmentName,
                    PositionStaff=item.PositionStaff
                }).ToList();
            bool bIsSearch = false;
            foreach (var item in q)
            {
                if (item.QuantityMonth > 0)
                {
                    if (item.Status != "Hết hạn")
                    {
                        if (item.ExpiryDate != null)
                        {
                            //  DateTime bb = item..AddHours(ShiftsOfItem2.EndTime.Value.Hour).AddMinutes(ShiftsOfItem2.EndTime.Value.Minute).AddSeconds(ShiftsOfItem2.EndTime.Value.Second);
                            TimeSpan b = item.ExpiryDate.Value.Subtract(DateTime.Now);
                            var w = b.TotalDays;
                            if (Convert.ToInt32(w) <= 0)
                            {
                                item.Status = "Hết hạn";
                                item.Status2 = "Hết hạn";
                                var labourcontract = LabourContractRepository.GetLabourContractById(item.Id);
                                labourcontract.Status = item.Status;
                                LabourContractRepository.UpdateLabourContract(labourcontract);
                            }
                            else
                                if (Convert.ToInt32(w) < item.Notice && w > 0)
                            {
                                item.Status = "Sắp hết";
                                item.Status2 = "Còn " + Convert.ToInt32(w) + " ngày hết hợp đồng";
                                var labourcontract = LabourContractRepository.GetLabourContractById(item.Id);
                                labourcontract.Status = item.Status;
                                LabourContractRepository.UpdateLabourContract(labourcontract);
                            }
                            else
                            {
                                item.Status2 = "Còn hiệu lực";
                                var labourcontract = LabourContractRepository.GetLabourContractById(item.Id);
                                labourcontract.Status = item.Status;
                                LabourContractRepository.UpdateLabourContract(labourcontract);
                            }
                        }
                    }

                }
                else
                {
                    item.Status = "Còn hiệu lực";
                    item.Status2 = "Còn hiệu lực";
                    var labourcontract = LabourContractRepository.GetLabourContractById(item.Id);
                    labourcontract.Status = item.Status;
                    LabourContractRepository.UpdateLabourContract(labourcontract);
                }
            }
            //nếu có tìm kiếm nâng cao thì lọc trước
            if (SearchOjectAttr.ListField != null)
            {
                if (SearchOjectAttr.ListField.Count > 0)
                {
                    //lấy các đối tượng ObjectAttributeValue nào thỏa đk có AttributeId trong ListField và có giá trị tương ứng trong ListField
                    var listObjectAttrValue = ObjectAttributeRepository.GetAllObjectAttributeValue().AsEnumerable().Where(attr => SearchOjectAttr.ListField.Any(item => item.Id == attr.AttributeId && Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(attr.Value).Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.Value)))).ToList();

                    //tiếp theo tìm các sản phẩm có id bằng với ObjectId trong listObjectAttrValue vừa tìm được
                    q = q.Where(product => listObjectAttrValue.Any(item => item.ObjectId == product.Id));
                    bIsSearch = true;
                    ViewBag.ListOjectAttrSearch = new JavaScriptSerializer().Serialize(SearchOjectAttr.ListField.Select(x => new { Id = x.Id, Value = x.Value }));
                }
            }

            if (!string.IsNullOrEmpty(Code))
            {
                q = q.Where(item => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.Code).ToLower().Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(Code).ToLower()));
                bIsSearch = true;
            }

            if (!string.IsNullOrEmpty(Staff))
            {
                q = q.Where(item => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.StaffCode).ToLower().Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(Staff).ToLower()));
                bIsSearch = true;
            }
            if (!string.IsNullOrEmpty(NameStaff))
            {
                q = q.Where(item => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.StaffName).ToLower().Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(NameStaff).ToLower()));
                bIsSearch = true;
            }
            if (!string.IsNullOrEmpty(Status))
            {
                q = q.Where(item => item.Status == Status);
                bIsSearch = true;
            }
            if (Type != null && Type.Value > 0)
            {
                q = q.Where(item => item.Type == Type);
                bIsSearch = true;
            }
            if (!string.IsNullOrEmpty(FormWork))
            {
                q = q.Where(item => item.FormWork == FormWork);
                bIsSearch = true;
            }

            var user = userRepository.GetUserById(WebSecurity.CurrentUserId);
            if (bIsSearch)
            {
                if (user.UserTypeId == 1)
                {
                    q = q.OrderByDescending(m => m.CreatedDate);
                }
                else
                {
                    q = q.Where(x => x.StaffbranchId == user.BranchId).OrderByDescending(m => m.CreatedDate);
                }
            }
            else
            {
                if (Request["search"] != null)
                {
                    if (user.UserTypeId == 1)
                    {
                        q = q.OrderByDescending(m => m.CreatedDate);
                    }
                    else
                    {
                        q = q.Where(x => x.StaffbranchId == user.BranchId).OrderByDescending(m => m.CreatedDate);
                    }
                }
                else
                {
                    if (user.UserTypeId == 1)
                    {
                        q = q.Where(x => x.Status == "Sắp hết").OrderByDescending(m => m.CreatedDate);
                    }
                    else
                    {
                        q = q.Where(x => x.StaffbranchId == user.BranchId && x.Status == "Sắp hết").OrderByDescending(m => m.CreatedDate);
                    }
                }

            }

            ViewBag.Search = bIsSearch;
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        #endregion
        #region List

        public ViewResult List(int? StaffId)
        {
            IEnumerable<LabourContractViewModel> q = LabourContractRepository.GetAllvwLabourContract().Where(x=>x.StaffId==StaffId)
                .Select(item => new LabourContractViewModel
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
                    Status = item.Status,
                    Type = item.Type,
                    StaffbranchId = item.StaffbranchId,
                    FormWork = item.FormWork,
                    CreatedUserName = item.CreatedUserName,
                    ContractTypeName = item.ContractTypeName,
                    QuantityMonth = item.QuantityMonth,
                    Notice = item.Notice,
                    EffectiveDate = item.EffectiveDate,
                    ExpiryDate = item.ExpiryDate,
                    StaffName = item.StaffName,
                    StaffCode = item.StaffCode,
                    StaffBirthday = item.StaffBirthday,
                    StaffId = item.StaffId,
                    StaffProfileImage = item.StaffProfileImage,
                    ApprovedUserId = item.ApprovedUserId,
                    ApprovedUserName = item.ApprovedUserName,
                    WageAgreement = item.WageAgreement,
                    SignedDay = item.SignedDay,
                    StaffBranchName = item.StaffBranchName,
                    StaffDepartmentName = item.StaffDepartmentName,
                    PositionStaff = item.PositionStaff
                }).ToList();
        
            foreach (var item in q)
            {
                if (item.QuantityMonth > 0)
                {
                    if (item.Status != "Hết hạn")
                    {
                        if (item.ExpiryDate != null)
                        {
                            //  DateTime bb = item..AddHours(ShiftsOfItem2.EndTime.Value.Hour).AddMinutes(ShiftsOfItem2.EndTime.Value.Minute).AddSeconds(ShiftsOfItem2.EndTime.Value.Second);
                            TimeSpan b = item.ExpiryDate.Value.Subtract(DateTime.Now);
                            var w = b.TotalDays;
                            if (Convert.ToInt32(w) <= 0)
                            {
                                item.Status = "Hết hạn";
                                item.Status2 = "Hết hạn";
                                var labourcontract = LabourContractRepository.GetLabourContractById(item.Id);
                                labourcontract.Status = item.Status;
                                LabourContractRepository.UpdateLabourContract(labourcontract);
                            }
                            else
                                if (Convert.ToInt32(w) < item.Notice && w > 0)
                            {
                                item.Status = "Sắp hết";
                                item.Status2 = "Còn " + Convert.ToInt32(w) + " ngày hết hợp đồng";
                                var labourcontract = LabourContractRepository.GetLabourContractById(item.Id);
                                labourcontract.Status = item.Status;
                                LabourContractRepository.UpdateLabourContract(labourcontract);
                            }
                            else
                            {
                                item.Status2 = "Còn hiệu lực";
                            }
                        }
                    }

                }
                else
                {
                    item.Status = "Còn hiệu lực";
                    item.Status2 = "Còn hiệu lực";
                }
            }
           
            return View(q);
        }
        #endregion
        #region Create
        public ViewResult Create()
        {
            var model = new LabourContractViewModel();
            model.EffectiveDate = DateTime.Now;
            model.SignedDay = DateTime.Now;
            // xóa file đính kèm trong session để bắt đầu thêm mới
            Session["file"] = null;
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(LabourContractViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = userRepository.GetUserById(WebSecurity.CurrentUserId);
                var LabourContract = new LabourContract();
                AutoMapper.Mapper.Map(model, LabourContract);
                LabourContract.IsDeleted = false;
                LabourContract.CreatedUserId = WebSecurity.CurrentUserId;
                LabourContract.ModifiedUserId = WebSecurity.CurrentUserId;
                LabourContract.AssignedUserId = WebSecurity.CurrentUserId;
                LabourContract.CreatedDate = DateTime.Now;
                LabourContract.ModifiedDate = DateTime.Now;
                LabourContract.Status = "Còn hiệu lực";

                var q = LabourContractTypeRepository.GetLabourContractTypeById(model.Type.Value);
                if (Convert.ToInt32(model.Type) > 0)
                {
                    LabourContract.ExpiryDate = model.EffectiveDate.Value.AddMonths(Convert.ToInt32(q.QuantityMonth));
                }
                //lấy thông tin nhân viên lúc ký hợp đồng lưu lại.
                var staff = staffRepository.GetvwStaffsById(model.StaffId.Value);
                //LabourContract.DepartmentStaffId = staff.BranchDepartmentId;
                LabourContract.PositionStaff = staff.PositionId.Value.ToString();
                //lấy thông tin người đại diện công ty ký.
                var approved = staffRepository.GetvwStaffsById(model.ApprovedUserId.Value);
                //LabourContract.DepartmentApprovedId = approved.BranchDepartmentId;
                //LabourContract.PositionApproved = approved.Position;
                LabourContractRepository.InsertLabourContract(LabourContract);
                //tạo mã hợp đồng lao động
                var prefix = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_LabourContract");
                LabourContract.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix, LabourContract.Id);
                LabourContractRepository.UpdateLabourContract(LabourContract);
                //lưu các trường thuộc tính động.
                //tạo hoặc cập nhật đặc tính động cho sản phẩm nếu có danh sách đặc tính động truyền vào và đặc tính đó phải có giá trị
                Sale.Controllers.ObjectAttributeController.CreateOrUpdateForObject(LabourContract.Id, model.AttributeValueList);
                //lưu file
                DocumentFieldController.SaveUpload(LabourContract.Code,"", LabourContract.Id, "LabourContract");
                //tạo quá trình lương cho nhân viên
                Staff.Controllers.ProcessPayController.CreateProcessPay(model.StaffId,model.WageAgreement,model.EffectiveDate);
                //cập nhật ngày vào làm, ngày nghỉ vào thông tin nhân viên
                Staff.Controllers.StaffsController.UpdateStaff(model.StaffId, model.EffectiveDate, LabourContract.ExpiryDate);
                //
                var labourcontract = LabourContractRepository.GetvwLabourContractById(LabourContract.Id);
                CreateLabourContract(labourcontract);
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess + " " + LabourContract.Code;

                return RedirectToAction("Index");
            }
            TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.InsertUnsucess;
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var LabourContract = LabourContractRepository.GetLabourContractById(Id.Value);
            if (LabourContract != null && LabourContract.IsDeleted != true)
            {
                var model = new LabourContractViewModel();
                AutoMapper.Mapper.Map(LabourContract, model);
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
        [ValidateInput(false)]
        public ActionResult Edit(LabourContractViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var LabourContract = LabourContractRepository.GetLabourContractById(model.Id);
                    if (LabourContract.WageAgreement != model.WageAgreement)
                    {
                        //tạo quá trình lương cho nhân viên
                        Staff.Controllers.ProcessPayController.CreateProcessPay(model.StaffId, model.WageAgreement, model.EffectiveDate);
                    }
                    var q = LabourContractTypeRepository.GetLabourContractTypeById(model.Type.Value);
                    //kiem tra loai hop dong, neu khac loại hd vô thời hạn thì tính ngày nghỉ làm của nhân viên.
                    if (q.QuantityMonth > 0)
                    {
                        model.ExpiryDate = model.EffectiveDate.Value.AddMonths(Convert.ToInt32(q.QuantityMonth));
                    }
                    if (LabourContract.EffectiveDate != model.EffectiveDate)
                    {
                        //cập nhật ngày vào làm, ngày nghỉ vào thông tin nhân viên
                        Staff.Controllers.StaffsController.UpdateStaff(model.StaffId, model.EffectiveDate, model.ExpiryDate);
                    }
                    AutoMapper.Mapper.Map(model, LabourContract);
                    LabourContract.ModifiedUserId = WebSecurity.CurrentUserId;
                    LabourContract.ModifiedDate = DateTime.Now;
                    if (LabourContract.ExpiryDate != null)
                    {
                        //  DateTime bb = item..AddHours(ShiftsOfItem2.EndTime.Value.Hour).AddMinutes(ShiftsOfItem2.EndTime.Value.Minute).AddSeconds(ShiftsOfItem2.EndTime.Value.Second);
                        TimeSpan b = LabourContract.ExpiryDate.Value.Subtract(DateTime.Now);
                        var w = b.TotalDays;
                        if (Convert.ToInt32(w) <= 0)
                        {
                            LabourContract.Status = "Hết hạn";
                        }
                        else
                            if (Convert.ToInt32(w) < q.Notice && w > 0)
                            {
                                LabourContract.Status = "Sắp hết";
                            }
                            else
                            {
                                LabourContract.Status = "Còn hiệu lực";
                            }
                    }
                    LabourContractRepository.UpdateLabourContract(LabourContract);
                    //tạo hoặc cập nhật đặc tính động cho sản phẩm nếu có danh sách đặc tính động truyền vào và đặc tính đó phải có giá trị
                    Sale.Controllers.ObjectAttributeController.CreateOrUpdateForObject(LabourContract.Id, model.AttributeValueList);
                  
                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess + " " + LabourContract.Code;
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }

            TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.UpdateUnsuccess + " " + model.Code;
            return RedirectToAction("Index");

            //if (Request.UrlReferrer != null)
            //    return Redirect(Request.UrlReferrer.AbsoluteUri);
            //return RedirectToAction("Index");
        }

        #endregion

        #region Detail
        public ActionResult Detail(int? Id)
        {
            var LabourContract = LabourContractRepository.GetvwLabourContractById(Id.Value);
            if (LabourContract != null && LabourContract.IsDeleted != true)
            {
                var model = new LabourContractViewModel();
                AutoMapper.Mapper.Map(LabourContract, model);

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
        #endregion

        //#region Delete
        //[HttpPost]
        //public ActionResult Delete(int? Id)
        //{
        //    try
        //    {
        //        string idDeleteAll = Request["DeleteId-checkbox"];
        //        string[] arrDeleteId = idDeleteAll.Split(',');
        //        for (int i = 0; i < arrDeleteId.Count(); i++)
        //        {
        //            var item = LabourContractRepository.GetLabourContractById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
        //            if (item != null)
        //            {
        //                if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
        //                {
        //                    TempData["FailedMessage"] = "NotOwner";
        //                    return RedirectToAction("Index");
        //                }

        //                item.IsDeleted = true;
        //                LabourContractRepository.UpdateLabourContract(item);
        //            }
        //        }
        //        TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.DeleteSuccess;
        //        return RedirectToAction("Index");
        //    }
        //    catch (DbUpdateException)
        //    {
        //        TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.RelationError;
        //        return RedirectToAction("Index");
        //    }
        //}
        //#endregion

        #region Delete
        [HttpPost]
        public ActionResult Delete(int? Id)
        {
            try
            {

                var item = LabourContractRepository.GetLabourContractById(Id.Value);
                if (item != null)
                {
                    //if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                    //{
                    //    TempData["FailedMessage"] = "NotOwner";
                    //    return RedirectToAction("Index");
                    //}

                    item.IsDeleted = true;
                    LabourContractRepository.UpdateLabourContract(item);

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

        #region gia hạn hợp đồng
        public ActionResult Extend(int? Id)
        {
            Session["file"] = null;
            var LabourContract = LabourContractRepository.GetvwLabourContractById(Id.Value);
            if (LabourContract != null && LabourContract.IsDeleted != true)
            {
                var model = new LabourContractViewModel();
                AutoMapper.Mapper.Map(LabourContract, model);
                model.EffectiveDate = DateTime.Now;
                model.SignedDay = DateTime.Now;
              
                //lấy thông tin nhân viên lúc ký hợp đồng lưu lại.
                var staff = staffRepository.GetvwStaffsById(model.StaffId.Value);
                //model.DepartmentStaffId = staff.BranchDepartmentId;
                //model.PositionStaff = staff.Position;
                //model.StaffBranchName = staff.BranchName;
                //model.StaffDepartmentName = staff.Staff_DepartmentId;
                model.Name = "Hợp đồng gia hạn";
                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Extend(LabourContractViewModel model)
        {
            if (ModelState.IsValid)
            {
                var LabourContract = new LabourContract();
                AutoMapper.Mapper.Map(model, LabourContract);
                LabourContract.IsDeleted = false;
                LabourContract.CreatedUserId = WebSecurity.CurrentUserId;
                LabourContract.ModifiedUserId = WebSecurity.CurrentUserId;
                LabourContract.AssignedUserId = WebSecurity.CurrentUserId;
                LabourContract.CreatedDate = DateTime.Now;
                LabourContract.ModifiedDate = DateTime.Now;
                LabourContract.Status = "Còn hiệu lực";
                if (Convert.ToInt32(model.Type) > 0)
                {
                    LabourContract.ExpiryDate = model.EffectiveDate.Value.AddMonths(Convert.ToInt32(model.Type));
                }
                //lấy thông tin người đại diện công ty ký.
                var approved = staffRepository.GetvwStaffsById(model.ApprovedUserId.Value);
                //LabourContract.DepartmentApprovedId = approved.BranchDepartmentId;
                LabourContract.PositionApproved = approved.PositionId.Value.ToString();
                LabourContractRepository.InsertLabourContract(LabourContract);
                var prefix = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_DocumentField");
                LabourContract.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix, LabourContract.Id);
                LabourContractRepository.UpdateLabourContract(LabourContract);
                //lưu các trường thuộc tính động.
                //tạo hoặc cập nhật đặc tính động cho sản phẩm nếu có danh sách đặc tính động truyền vào và đặc tính đó phải có giá trị
                Sale.Controllers.ObjectAttributeController.CreateOrUpdateForObject(LabourContract.Id, model.AttributeValueList);
                //lưu file
                DocumentFieldController.SaveUpload(LabourContract.Code, "", LabourContract.Id, "LabourContract");
                //tạo quá trình lương cho nhân viên
                Staff.Controllers.ProcessPayController.CreateProcessPay(model.StaffId, model.WageAgreement, model.EffectiveDate);
                //cập nhật ngày vào làm, ngày nghỉ vào thông tin nhân viên
                Staff.Controllers.StaffsController.UpdateStaff(model.StaffId, model.EffectiveDate, LabourContract.ExpiryDate);
                //
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess + " " + LabourContract.Code;
                return RedirectToAction("Index");
            }
            TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.InsertUnsucess;
            return RedirectToAction("Index");
        }

        #endregion

        #region Create Contract Sell file word.
        public void CreateLabourContract(vwLabourContract model)
        {
           // var branch = branchRepository.GetvwBranchById(model.StaffbranchId.Value);
           // var staff = staffRepository.GetStaffsById(model.StaffId.Value);
           // string filePath = Server.MapPath("~/Data/LabourContract.html");

           // string strOutput = System.IO.File.ReadAllText(filePath, Encoding.UTF8);
           //// thông tin bên A
           // strOutput = strOutput.Replace("#ApprovedUserName#", model.ApprovedUserName);
           // strOutput = strOutput.Replace("#ApprovedUserPositionName#", model.ApprovedUserPositionName);
           // strOutput = strOutput.Replace("#ApprovedBranchName#", model.ApprovedBranchName);
           // strOutput = strOutput.Replace("#ApprovedPhoneBranch#", branch.Phone);
           // strOutput = strOutput.Replace("#ApprovedBranchAddress#", branch.Address+" - "+branch.WardName+" - "+branch.DistrictName+" - " +branch.ProvinceName);
           // strOutput = strOutput.Replace("#StaffName#", model.StaffName);
           // strOutput = strOutput.Replace("#StaffBirthday#", model.StaffBirthday.Value.ToString("dd/MM/yyyy"));
           // strOutput = strOutput.Replace("#Job#", staff.Technique);
           // strOutput = strOutput.Replace("#StaffAddress#", model.StaffAddress);
           // strOutput = strOutput.Replace("#StaffWard#", model.StaffWard);
           // strOutput = strOutput.Replace("#StaffDistrict#", model.StaffDistrict);
           // strOutput = strOutput.Replace("#StaffProvince#", model.StaffProvince);
           // //thông tin bên B
           // strOutput = strOutput.Replace("#StaffIdCardNumber#", model.StaffIdCardNumber);
           // strOutput = strOutput.Replace("#StaffIdCardDate#", model.StaffIdCardDate.Value.ToString("dd/MM/yyyy"));
           // strOutput = strOutput.Replace("#StaffCardIssuedName#", model.StaffCardIssuedName);
           // strOutput = strOutput.Replace("#ContractTypeName#", model.ContractTypeName);
           // strOutput = strOutput.Replace("#EffectiveDate#", model.EffectiveDate.Value.ToString("dd/MM/yyyy"));
           // strOutput = strOutput.Replace("#ExpiryDate#", model.ExpiryDate.Value.ToString("dd/MM/yyyy"));
           // strOutput = strOutput.Replace("#FormWork#", model.FormWork);
           // strOutput = strOutput.Replace("#WageAgreement#", Erp.BackOffice.Helpers.Common.PhanCachHangNgan(model.WageAgreement));
           // strOutput = strOutput.Replace("#StaffPositionName#", model.StaffPositionName);
           // strOutput = strOutput.Replace("#Content#", model.Content);
           // strOutput = strOutput.Replace("#Code#", model.Code);
           // strOutput = strOutput.Replace("#SignedDay.Date#", model.SignedDay.Value.ToString("dd"));
           // strOutput = strOutput.Replace("#SignedDay.Month#", model.SignedDay.Value.ToString("MM"));
           // strOutput = strOutput.Replace("#SignedDay.Year#", model.SignedDay.Value.ToString("yyyy"));
           // strOutput = strOutput.Replace("#Name#", model.Name);
            //strOutput = strOutput.Replace("#Place#", contract.Place);
            //strOutput = strOutput.Replace("#Day#", contract.CreatedDate.Value.Day.ToString());
            //strOutput = strOutput.Replace("#Month#", contract.CreatedDate.Value.Month.ToString());
            //strOutput = strOutput.Replace("#Year#", contract.CreatedDate.Value.Year.ToString());

            //lưu thông tin dữ liệu vào database
            //lưu document field

            var DocumentField = new DocumentField();
            DocumentField.IsDeleted = false;
            DocumentField.CreatedUserId = WebSecurity.CurrentUserId;
            DocumentField.ModifiedUserId = WebSecurity.CurrentUserId;
            DocumentField.AssignedUserId = WebSecurity.CurrentUserId;
            DocumentField.CreatedDate = DateTime.Now;
            DocumentField.ModifiedDate = DateTime.Now;
            DocumentField.Name = model.Code;
            DocumentField.DocumentTypeId = 9;
           // DocumentField.IsSearch = "";
            DocumentField.Category = "LabourContract";
            DocumentField.CategoryId = model.Id;
            DocumentFieldRepository.InsertDocumentField(DocumentField);
            var prefix = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_DocumentField");
            DocumentField.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix, DocumentField.Id);
            DocumentFieldRepository.UpdateDocumentField(DocumentField);
            // lưu dữ liệu thành file word
            var originalDirectory = new DirectoryInfo(string.Format("{0}Files\\somefiles", System.Web.HttpContext.Current.Server.MapPath(@"\")));
            string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "DocumentAttribute");
            bool isExists = System.IO.Directory.Exists(pathString);
            if (!isExists)
                System.IO.Directory.CreateDirectory(pathString);
            var name = model.Code + "(" + DocumentField.Id + ")" + ".doc";
            var path = string.Format("{0}\\{1}", pathString, name);
            System.IO.File.WriteAllText(path, model.Content);
            //lưu documentAttribute
            var DocumentAttribute = new DocumentAttribute();
            DocumentAttribute.IsDeleted = false;
            DocumentAttribute.CreatedUserId = WebSecurity.CurrentUserId;
            DocumentAttribute.ModifiedUserId = WebSecurity.CurrentUserId;
            DocumentAttribute.AssignedUserId = WebSecurity.CurrentUserId;
            DocumentAttribute.CreatedDate = DateTime.Now;
            DocumentAttribute.ModifiedDate = DateTime.Now;
            DocumentAttribute.OrderNo = 1;
            DocumentAttribute.File = name;
            byte[] str = System.IO.File.ReadAllBytes(path);
            DocumentAttribute.Size = str.Length.ToString();
            DocumentAttribute.TypeFile = "doc";
            DocumentAttribute.DocumentFieldId = DocumentField.Id;
            DocumentAttributeRepository.InsertDocumentAttribute(DocumentAttribute);


        }
        #endregion

        public ContentResult Contract(string Name, int? staffId, int? Type, int? ApprovedUserId, string FormWork,int WageAgreement)
        {
            var model = new LabourContractViewModel();
            model.Name = Name;
            model.StaffId = staffId;
            model.EffectiveDate = DateTime.Now;
            model.SignedDay = DateTime.Now;
            model.Type = Type;
            model.ApprovedUserId = ApprovedUserId;
            model.FormWork = FormWork;
            model.WageAgreement = WageAgreement;
            model.Content = bill(model);
            return Content(model.Content);
        }
        string bill(LabourContractViewModel model)
        {

            //lấy thông tin
            var company = Erp.BackOffice.Helpers.Common.GetSetting("companyName");
            //var BranchName = Erp.BackOffice.Helpers.Common.CurrentUser.BranchName;
            var staff = staffRepository.GetvwStaffsById(model.StaffId.Value);
            var ApprovedStaff = staffRepository.GetvwStaffsById(model.ApprovedUserId.Value);
            var q = LabourContractTypeRepository.GetLabourContractTypeById(model.Type.Value);
            //var branch = branchRepository.GetvwBranchById(ApprovedStaff.Sale_BranchId.Value);
            var address = Erp.BackOffice.Helpers.Common.GetSetting("addresscompany");
            var phone = Erp.BackOffice.Helpers.Common.GetSetting("phonecompany");
            var fax = Erp.BackOffice.Helpers.Common.GetSetting("faxcompany");
            var bankcode = Erp.BackOffice.Helpers.Common.GetSetting("bankcode");
            var bankname = Erp.BackOffice.Helpers.Common.GetSetting("bankname");
            if (Convert.ToInt32(model.Type) > 0)
            {
                model.ExpiryDate = model.EffectiveDate.Value.AddMonths(Convert.ToInt32(q.QuantityMonth));
            }
            //lấy template phiếu xuất.
            var template = templatePrintRepository.GetAllTemplatePrint().Where(x => x.Code.Contains("LabourContract")).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            //truyền dữ liệu vào template.
            model.Content = template.Content;
            model.Content = model.Content.Replace("{Name}", model.Name);
            model.Content = model.Content.Replace("{SignedDay.Date}", model.SignedDay.Value.Day.ToString());
            model.Content = model.Content.Replace("{SignedDay.Month}", model.SignedDay.Value.Month.ToString());
            model.Content = model.Content.Replace("{SignedDay.Year}", model.SignedDay.Value.Year.ToString());
            model.Content = model.Content.Replace("{ApprovedUserName}", ApprovedStaff.Name);
            model.Content = model.Content.Replace("{ApprovedUserPositionName}", ApprovedStaff.PositionName);
            model.Content = model.Content.Replace("{ApprovedBranchName}", company);
            model.Content = model.Content.Replace("{ApprovedPhoneBranch}", phone);
            model.Content = model.Content.Replace("{ApprovedBranchAddress}", address);
            model.Content = model.Content.Replace("{StaffName}", staff.Name);
            model.Content = model.Content.Replace("{StaffBirthday}", staff.Birthday.Value.ToString("dd/MM/yyyy"));
            model.Content = model.Content.Replace("{Job}", model.Job);
            model.Content = model.Content.Replace("{StaffAddress}", staff.Address);
            model.Content = model.Content.Replace("{StaffWard}", staff.WardName);
            model.Content = model.Content.Replace("{StaffDistrict}", staff.DistrictName);
            model.Content = model.Content.Replace("{StaffProvince}", staff.ProvinceName);
            model.Content = model.Content.Replace("{StaffIdCardNumber}", staff.IdCardNumber);
            model.Content = model.Content.Replace("{StaffIdCardDate}", staff.IdCardDate.Value.ToString("dd/MM/yyyy"));
            model.Content = model.Content.Replace("{StaffCardIssuedName}", staff.CardIssuedName);
            model.Content = model.Content.Replace("{ContractTypeName}", model.Name);
            model.Content = model.Content.Replace("{EffectiveDate}", model.EffectiveDate.Value.ToString("dd/MM/yyyy"));
            model.Content = model.Content.Replace("{ExpiryDate}", model.ExpiryDate.Value.ToString("dd/MM/yyyy "));
            model.Content = model.Content.Replace("{FormWork}", model.FormWork);
            model.Content = model.Content.Replace("{WageAgreement}", Helpers.Common.PhanCachHangNgan(model.WageAgreement));
            model.Content = model.Content.Replace("{StaffPositionName}", staff.PositionName);

            model.Content = model.Content.Replace("{System.CompanyName}", company);
            model.Content = model.Content.Replace("{System.AddressCompany}", address);
            return model.Content;
        }

        public ActionResult Print(int? Id)
        {
            var LabourContract = LabourContractRepository.GetvwLabourContractById(Id.Value);
            if (LabourContract != null && LabourContract.IsDeleted != true)
            {
                var model = new LabourContractViewModel();
                var branch = branchRepository.GetvwBranchById(LabourContract.ApprovedBranchId.Value);
                AutoMapper.Mapper.Map(LabourContract, model);
                ViewBag.BranchAddress = branch.Address;
                ViewBag.BranchPhone = branch.Phone;
                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }
    }
}
