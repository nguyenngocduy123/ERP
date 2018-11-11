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
using Erp.BackOffice.Areas.Administration.Models;
using System.IO;
using System.Web;
using System.Web.Hosting;
using System.Linq.Expressions;
using Erp.Domain.Staff.Repositories;
using Erp.Domain.Account.Interfaces;
using System.Web.Script.Serialization;
using Erp.BackOffice.Sale.Models;
using Erp.Domain.Sale.Interfaces;
using Erp.Domain.Sale.Entities;
namespace Erp.BackOffice.Staff.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class DocumentFieldController : Controller
    {
        private readonly IDocumentFieldRepository DocumentFieldRepository;
        private readonly IUserRepository userRepository;
        private readonly IDocumentAttributeRepository DocumentAttributeRepository;
        private readonly IPageRepository pageRepository;
        private readonly IUserTypePageRepository userTypePageRepository;
        private readonly ILogDocumentAttributeRepository LogDocumentAttributeRepositorty;
        private readonly ICategoryRepository categoryRepository;
        private readonly IContractRepository contractRepository;
        private readonly ILabourContractRepository labourContractRepository;
        private readonly IObjectAttributeRepository ObjectAttributeRepository;
        private readonly IInternalNotificationsRepository InternalNotificationsRepository;
        public DocumentFieldController(
            IDocumentFieldRepository _DocumentField
            , IUserRepository _user
            , IDocumentAttributeRepository DocumentAttribute
            , IPageRepository page
            , IUserTypePageRepository userTypePage
            , ILogDocumentAttributeRepository logDocumentAttribute
            , ICategoryRepository category
            , IContractRepository contract
            , ILabourContractRepository labourContract
           , IObjectAttributeRepository objectAttribute
           , IInternalNotificationsRepository _InternalNotifications
            )
        {
            DocumentFieldRepository = _DocumentField;
            userRepository = _user;
            DocumentAttributeRepository = DocumentAttribute;
            pageRepository = page;
            userTypePageRepository = userTypePage;
            LogDocumentAttributeRepositorty = logDocumentAttribute;
            categoryRepository = category;
            contractRepository = contract;
            labourContractRepository = labourContract;
            InternalNotificationsRepository = _InternalNotifications;
            ObjectAttributeRepository = objectAttribute;
        }

        #region Index


        public ViewResult Index(string Name, string Category, string Code, int? UID, string TypeFile, int? CategoryId, SearchObjectAttributeViewModel SearchOjectAttr)
        {
            var user = userRepository.GetUserById(WebSecurity.CurrentUserId);
            var staff = Erp.BackOffice.Helpers.Common.GetStaffByCurrentUser();
            var start_date = Request["start_date"];
            var end_date = Request["end_date"];
            IEnumerable<DocumentFieldViewModel> q = DocumentFieldRepository.GetAllvwDocumentField()
                .Select(item => new DocumentFieldViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    Category = item.Category,
                    Code = item.Code,
                    //  DocumentTypeId = item.DocumentTypeId,
                    IsSearch = item.IsSearch,
                    TypeName = item.TypeName,
                    CategoryId = item.CategoryId
                }).ToList();
            //nếu có tìm kiếm nâng cao thì lọc trước
            if (SearchOjectAttr.ListField != null)
            {
                var SearchOjectAttrNoneEmpty = SearchOjectAttr.ListField.Where(x => string.IsNullOrEmpty(x.Value) == false).ToList();

                if (SearchOjectAttrNoneEmpty.Count > 0)
                {
                    //lấy các đối tượng ObjectAttributeValue nào thỏa đk có AttributeId trong ListField và có giá trị tương ứng trong ListField
                    var listObjectAttrValue = ObjectAttributeRepository.GetAllObjectAttributeValue()
                        .Where(x => x.Value != null && x.Value != "")
                        .AsEnumerable().Where(attr =>
                        SearchOjectAttrNoneEmpty.Any(item =>
                            item.Id == attr.AttributeId &&
                             Helpers.Common.ChuyenThanhKhongDau(attr.Value).Contains(Helpers.Common.ChuyenThanhKhongDau(item.Value))
                            )
                        ).ToList();

                    //tiếp theo tìm các sản phẩm có id bằng với ObjectId trong listObjectAttrValue vừa tìm được
                    q = q.Where(product => listObjectAttrValue.Any(item => item.ObjectId == product.Id));
                    ViewBag.ListOjectAttrSearch = new JavaScriptSerializer().Serialize(SearchOjectAttr.ListField.Select(x => new { Id = x.Id, Value = x.Value }));
                }
            }

            if (!string.IsNullOrEmpty(Code))
            {
                q = q.Where(item => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.Code).ToLower().Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(Code).ToLower()));
            }

            if (!string.IsNullOrEmpty(Name))
            {
                q = q.Where(item => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.Name).ToLower().Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(Name).ToLower()));
            }
            if (!string.IsNullOrEmpty(Category))
            {
                q = q.Where(item => item.Category == Category);
            }
            //if (DocumentTypeId != null && DocumentTypeId.Value > 0)
            //{
            //    q = q.Where(item => item.DocumentTypeId == DocumentTypeId);
            //}
            if (CategoryId != null && CategoryId.Value > 0)
            {
                q = q.Where(item => item.CategoryId == CategoryId);
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
                        q = q.Where(x => start_d <= x.CreatedDate && x.CreatedDate <= end_d);
                    }
                }
            }
            if (UID != null && UID.Value > 0)
            {
                q = q.Where(item => item.CreatedUserId == UID);
            }

            if (user.Id == 1)
            {
                q = q.OrderByDescending(x => x.ModifiedDate);
            }
            else
            {
                foreach (var i in q)
                {
                    List<string> listIdStaff = new List<string>();
                    if (!string.IsNullOrEmpty(i.IsSearch))
                    {
                        listIdStaff = i.IsSearch.Split(',').ToList();
                        var aaa = listIdStaff.Any(id2 => id2 == staff.Id.ToString());
                        if (aaa == false)
                        {
                            i.IsDeleted = true;
                        }
                        if (i.CreatedUserId == user.Id)
                        {
                            i.IsDeleted = false;
                        }
                    }
                    if (i.CreatedUserId != user.Id)
                    {
                        i.IsDeleted = true;
                    }
                }
                q = q.Where(x => x.IsDeleted != true).OrderByDescending(x => x.ModifiedDate);
            }

            //lấy giá trị mới nhất trong bảng DocumentAttribute gán ra bảng ngoài DocumentField
            //bảng DocumentAttribute sẽ trở thành Lịch sử theo dõi thay đổi file dữ liệu của 1 tập tin.
            foreach (var item in q)
            {
                var attribute = DocumentAttributeRepository.GetAllDocumentAttribute().Where(x => x.DocumentFieldId == item.Id).OrderByDescending(x => x.CreatedDate);
                if (attribute != null && attribute.Count() > 0)
                {
                    item.DocumentAttributeId = attribute.FirstOrDefault().Id;
                    item.TypeFile = attribute.FirstOrDefault().TypeFile;
                    item.File = attribute.FirstOrDefault().File;
                    item.FilePath = "/Files/somefiles/" + item.File;
                    item.CountFile = attribute.Count();
                    var id = (attribute.FirstOrDefault().Id);
                    item.QuantityDownload = LogDocumentAttributeRepositorty.GetAllLogDocumentAttribute().Where(x => x.DocumentAttributeId == id).Count();
                }
            }

            if (!string.IsNullOrEmpty(TypeFile))
            {
                q = q.Where(item => item.TypeFile == TypeFile);
            }
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        #endregion

        #region Delete
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            try
            {
                var item = DocumentAttributeRepository.GetDocumentAttributeById(Id);
                if (item != null)
                {
                    var documentfield = DocumentFieldRepository.GetDocumentFieldById(item.DocumentFieldId.Value);
                    var count_list_item = DocumentAttributeRepository.GetAllDocumentAttribute().Where(x => x.DocumentFieldId == documentfield.Id).ToList().Count();

                    var path = Helpers.Common.GetSetting(documentfield.Category);
                    FileInfo fi = new FileInfo(Server.MapPath("~" + path) + item.File);
                    if (fi.Exists)
                    {
                        fi.Delete();
                    }
                    if (count_list_item == 1)
                    {
                        DocumentFieldRepository.DeleteDocumentField(documentfield.Id);
                    }
                    DocumentAttributeRepository.DeleteDocumentAttribute(item.Id);
                    return Content("success");
                }
                return Content("error");
            }
            catch (DbUpdateException)
            {
                TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.RelationError;
                return Content("error");
            }
        }
        #endregion

        #region Create
        public ActionResult Create(int? Id, string Category)
        {
            var model = new DocumentFieldViewModel();
            model.CategoryId = Id;
            model.Category = Category;
            Session["file"] = null;
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(DocumentFieldViewModel model)
        {
            var urlRefer = Request["UrlReferrer"];
            var segments = urlRefer.Split('/');
            var IsSearch = Request["staffListcancel"];
            if (ModelState.IsValid)
            {
                //hàm insert documentfield
                DocumentFieldController.SaveUpload(model.Name, IsSearch, model.CategoryId.HasValue ? model.CategoryId.Value : 0, model.Category);
                //lưu các trường thuộc tính động.

                //tạo hoặc cập nhật đặc tính động cho sản phẩm nếu có danh sách đặc tính động truyền vào và đặc tính đó phải có giá trị
                Sale.Controllers.ObjectAttributeController.CreateOrUpdateForObject(model.Id, model.AttributeValueList);

                if (Request["IsPopup"] == "true")
                {
                    ViewBag.closePopup = "close and append to page parent";
                    ViewBag.urlRefer = urlRefer;
                    //  model.Id = InfoPartyA.Id;
                    return View(model);
                }

                return Redirect(urlRefer);
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var DocumentField = DocumentFieldRepository.GetDocumentFieldById(Id.Value);
            if (DocumentField != null && DocumentField.IsDeleted != true)
            {
                var model = new DocumentFieldViewModel();
                AutoMapper.Mapper.Map(DocumentField, model);
                if (model.Category == "Contract")
                {
                    model.DetailCategoryList = SelectListHelper.GetSelectList_Contract(model.CategoryId);
                }
                else if (model.Category == "LabourContract")
                {
                    model.DetailCategoryList = SelectListHelper.GetSelectList_LabourContract(model.CategoryId, App_GlobalResources.Wording.Empty);
                }
                else if (model.Category == "InternalNotifications")
                {
                    model.DetailCategoryList = SelectListHelper.GetSelectList_InternalNotifications(model.CategoryId, App_GlobalResources.Wording.Empty);
                }
                else
                {
                    model.DetailCategoryList = new List<SelectListItem>();
                }
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
        public ActionResult Edit(DocumentFieldViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var q = Request["staffListcancel"];
                    var DocumentField = DocumentFieldRepository.GetDocumentFieldById(model.Id);
                    AutoMapper.Mapper.Map(model, DocumentField);
                    DocumentField.ModifiedUserId = WebSecurity.CurrentUserId;
                    DocumentField.ModifiedDate = DateTime.Now;
                    DocumentField.IsSearch = q;
                    DocumentFieldRepository.UpdateDocumentField(DocumentField);
                    //tạo hoặc cập nhật đặc tính động cho sản phẩm nếu có danh sách đặc tính động truyền vào và đặc tính đó phải có giá trị
                    Sale.Controllers.ObjectAttributeController.CreateOrUpdateForObject(DocumentField.Id, model.AttributeValueList);
                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess + " " + DocumentField.Code;
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.UpdateUnsuccess + " " + model.Code;
            return RedirectToAction("Index");
        }
        #endregion

        #region Detail
        public ActionResult Detail(int? Id)
        {
            Session["file"] = null;
            var DocumentField = DocumentFieldRepository.GetvwDocumentFieldById(Id.Value);
            var path = Helpers.Common.GetSetting(DocumentField.Category);
            if (DocumentField != null && DocumentField.IsDeleted != true)
            {
                var model = new DocumentFieldViewModel();
                AutoMapper.Mapper.Map(DocumentField, model);
                if (model.Category == "Contract")
                {
                    var q = contractRepository.GetContractById(model.CategoryId);
                    if (q != null)
                        model.CategoryDetail = q.Code;
                }
                else if (model.Category == "LabourContract")
                {
                    var q = labourContractRepository.GetLabourContractById(model.CategoryId);
                    if (q != null)
                        model.CategoryDetail = q.Name;
                }
                else if (model.Category == "InternalNotifications")
                {
                    var q = InternalNotificationsRepository.GetInternalNotificationsById(model.CategoryId);
                    if (q != null)
                        model.CategoryDetail = q.Titles;
                }
                //model.PositionName = categoryRepository.GetCategoryByCode("position").Where(x => x.Value == model.IsSearch).FirstOrDefault().Name;
                model.DocumentAttributeList = DocumentAttributeRepository.GetAllDocumentAttribute().Where(x => x.DocumentFieldId == Id)
                .Select(item => new DocumentAttributeViewModel
            {
                Id = item.Id,
                CreatedUserId = item.CreatedUserId,
                //CreatedUserName = item.CreatedUserName,
                CreatedDate = item.CreatedDate,
                ModifiedUserId = item.ModifiedUserId,
                //ModifiedUserName = item.ModifiedUserName,
                ModifiedDate = item.ModifiedDate,
                File = item.File,
                Note = item.Note,
                OrderNo = item.OrderNo,
                Size = item.Size,
                TypeFile = item.TypeFile
            }).OrderByDescending(x => x.CreatedDate).ToList();
                foreach (var item in model.DocumentAttributeList)
                {
                    if (item.TypeFile.Equals("jpeg") || item.TypeFile.Equals("jpg") || item.TypeFile.Equals("png") || item.TypeFile.Equals("gif"))
                    {
                        item.FilePath = path + item.File;
                    }
                    else
                    {
                        item.FilePath = "/assets/file-icons-upload/48px/" + item.TypeFile + ".png";
                    }
                    item.QuantityDownload = LogDocumentAttributeRepositorty.GetAllLogDocumentAttribute().Where(x => x.DocumentAttributeId == item.Id).Count();
                }
                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }
        #endregion

        #region Upload
        public ActionResult Upload()
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            foreach (string fileName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[fileName];
                //Save file content goes here
                fName = file.FileName;
                if (file != null && file.ContentLength > 0)
                {
                    List<HttpPostedFileBase> listFile = new List<HttpPostedFileBase>();
                    if (Session["file"] != null)
                    {
                        listFile = (List<HttpPostedFileBase>)Session["file"];
                    }

                    listFile.Add(file);

                    Session["file"] = listFile;

                }

            }

            if (isSavedSuccessfully)
            {
                return Json(new { Message = fName });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }
        }
        #endregion

        #region Thêm mới tập tin trong trang Detail
        [HttpPost]
        public ActionResult Detail(DocumentFieldViewModel model)
        {
            List<HttpPostedFileBase> listFile = new List<HttpPostedFileBase>();
            if (Session["file"] != null)
                listFile = (List<HttpPostedFileBase>)Session["file"];

            if (listFile.Count <= 0)
            {
                TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.InsertUnsucess;
                return View(model);
            }
            else
            {
                int dem = DocumentAttributeRepository.GetAllDocumentAttribute().Where(x => x.DocumentFieldId == model.Id).OrderByDescending(x => x.OrderNo).FirstOrDefault().OrderNo.Value;
                foreach (var item in listFile)
                {
                    dem = dem + 1;
                    var type = item.FileName.Split('.').Last();
                    var FileName = model.Name.Replace(" ", "_");
                    var name = Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(FileName).ToLower();
                    var DocumentAttribute = new DocumentAttribute();
                    DocumentAttribute.IsDeleted = false;
                    DocumentAttribute.CreatedUserId = WebSecurity.CurrentUserId;
                    DocumentAttribute.ModifiedUserId = WebSecurity.CurrentUserId;
                    DocumentAttribute.AssignedUserId = WebSecurity.CurrentUserId;
                    DocumentAttribute.CreatedDate = DateTime.Now;
                    DocumentAttribute.ModifiedDate = DateTime.Now;
                    DocumentAttribute.OrderNo = dem;
                    DocumentAttribute.File = name + "(" + model.Id + "_" + DocumentAttribute.OrderNo + ")" + "." + type;
                    DocumentAttribute.Size = item.ContentLength.ToString();
                    DocumentAttribute.TypeFile = type;
                    DocumentAttribute.DocumentFieldId = model.Id;
                    DocumentAttributeRepository.InsertDocumentAttribute(DocumentAttribute);
                    var documentfield = DocumentFieldRepository.GetDocumentFieldById(model.Id);
                    var path = System.Web.HttpContext.Current.Server.MapPath("~" + Helpers.Common.GetSetting(documentfield.Category));
                    bool isExists = System.IO.Directory.Exists(path);
                    if (!isExists)
                        System.IO.Directory.CreateDirectory(path);
                    item.SaveAs(path + DocumentAttribute.File);
                }
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Detail", new { Id = model.Id });
            }
        }
        #endregion

        #region Download
        //[HttpPost]
        public ActionResult CheckDownload(string file, int? Id)
        {
            var documentattribute = DocumentAttributeRepository.GetDocumentAttributeById(Id.Value);
            var documentfield = DocumentFieldRepository.GetDocumentFieldById(documentattribute.DocumentFieldId.Value);
            var path = System.Web.HttpContext.Current.Server.MapPath(Helpers.Common.GetSetting(documentfield.Category));
            //var originalDirectory = new DirectoryInfo(string.Format("{0}Files\\somefiles\\DocumentAttribute\\", Server.MapPath(@"\")));

            //string pathString = System.IO.Path.Combine(originalDirectory.ToString());
            //var s = pathString + file;

            if (System.IO.File.Exists(path + file))
            {

                return Content("success");
            }
            else
            {

                return Content("error");
            }
        }

        public ActionResult Download(string file, int? Id)
        {
            var documentattribute = DocumentAttributeRepository.GetDocumentAttributeById(Id.Value);
            var documentfield = DocumentFieldRepository.GetDocumentFieldById(documentattribute.DocumentFieldId.Value);
            var path = System.Web.HttpContext.Current.Server.MapPath(Helpers.Common.GetSetting(documentfield.Category));
            //var originalDirectory = new DirectoryInfo(string.Format("{0}Files\\somefiles\\DocumentAttribute\\", Server.MapPath(@"\")));

            //string pathString = System.IO.Path.Combine(originalDirectory.ToString());
            var s = path + file;
            var logDocumentAttribute = new Domain.Staff.Entities.LogDocumentAttribute();
            var user = userRepository.GetUserById(WebSecurity.CurrentUserId);
            logDocumentAttribute.IsDeleted = false;
            logDocumentAttribute.CreatedUserId = WebSecurity.CurrentUserId;
            logDocumentAttribute.ModifiedUserId = WebSecurity.CurrentUserId;
            logDocumentAttribute.CreatedDate = DateTime.Now;
            logDocumentAttribute.ModifiedDate = DateTime.Now;
            logDocumentAttribute.DocumentAttributeId = Id;
            logDocumentAttribute.UserId = user.Id;
            LogDocumentAttributeRepositorty.InsertLogDocumentAttribute(logDocumentAttribute);
            byte[] fileBytes = System.IO.File.ReadAllBytes(s);
            //return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, file);

            var stream = new MemoryStream(fileBytes);
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel, còn rất nhiều content-type khác nhưng cái này mình thấy okay nhất
            //Response.ContentType = "image/JPEG";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file);
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            return Content("error");
        }
        #endregion

        #region lấy danh sách tập tin theo module và id detail module
        public ViewResult DocumentFieldList(string category, int? CategoryId, bool? IsLayout)
        {
            var list = DocumentFieldRepository.GetAllvwDocumentField().Where(x => x.Category == category && x.CategoryId == CategoryId).OrderByDescending(x => x.CreatedDate).AsEnumerable()
                .Select(item => new DocumentFieldViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    Category = item.Category,
                    Code = item.Code,
                    //   DocumentTypeId = item.DocumentTypeId,
                    IsSearch = item.IsSearch,
                    TypeName = item.TypeName,
                    CategoryId = item.CategoryId
                }).ToList();
            foreach (var item in list)
            {
                var attribute = DocumentAttributeRepository.GetAllDocumentAttribute().Where(x => x.DocumentFieldId == item.Id).OrderByDescending(x => x.CreatedDate).ToList();
                var Fpath = Helpers.Common.GetSetting(item.Category);
                var path = System.Web.HttpContext.Current.Server.MapPath(Fpath);

                //if (attribute != null && attribute.Count() > 0)
                //{

                //    item.DocumentAttributeId = attribute.FirstOrDefault().Id;
                //    item.TypeFile = attribute.FirstOrDefault().TypeFile;
                //    item.File = attribute.FirstOrDefault().File;
                //    item.FilePath = path + item.File;
                //    item.CountFile = attribute.Count();
                //    item.Icon = Erp.BackOffice.Helpers.Common.LayIconTapTin(item.TypeFile, item.FilePath);
                //    var id = (attribute.FirstOrDefault().Id);
                //    item.QuantityDownload = LogDocumentAttributeRepositorty.GetAllLogDocumentAttribute().Where(x => x.DocumentAttributeId == id).Count();
                //}
                item.DocumentAttributeList = attribute.Select(x => new DocumentAttributeViewModel
                {
                    CreatedDate = x.CreatedDate,
                    Id = x.Id,
                    File = x.File,
                    TypeFile = x.TypeFile,
                    Size = x.Size,
                    FilePath = Fpath + x.File,
                    Icon = Erp.BackOffice.Helpers.Common.LayIconTapTin(x.TypeFile, x.File, item.Category, "product")
                }).ToList();
            }
            ViewBag.IsLayout = IsLayout;
            return View(list);
        }
        #endregion

        #region SaveUpload
        public static void SaveUpload(string NameField, string IsSearch, int CategoryId, string Category, string FailedMessageKey = null, string fileName = null)
        {
            //insert và upload documentField, documentAttribute.
            List<HttpPostedFileBase> listFile = new List<HttpPostedFileBase>();
            if (System.Web.HttpContext.Current.Session["file"] != null)
                listFile = (List<HttpPostedFileBase>)System.Web.HttpContext.Current.Session["file"];

            DocumentFieldRepository DocumentFieldRepository = new DocumentFieldRepository(new Domain.Staff.ErpStaffDbContext());

            DocumentAttributeRepository DocumentAttributeRepository = new DocumentAttributeRepository(new Domain.Staff.ErpStaffDbContext());

            if (listFile.Count > 0)
            {
                var DocumentField = new DocumentField();

                DocumentField.IsDeleted = false;
                DocumentField.CreatedUserId = WebSecurity.CurrentUserId;
                DocumentField.ModifiedUserId = WebSecurity.CurrentUserId;
                DocumentField.AssignedUserId = WebSecurity.CurrentUserId;
                DocumentField.CreatedDate = DateTime.Now;
                DocumentField.ModifiedDate = DateTime.Now;
                DocumentField.Name = NameField;
                //  DocumentField.DocumentTypeId = DocumentTypeId;
                DocumentField.IsSearch = IsSearch;
                DocumentField.Category = Category;
                DocumentField.CategoryId = CategoryId;
                DocumentFieldRepository.InsertDocumentField(DocumentField);
                var prefix = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_DocumentField");
                DocumentField.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix, DocumentField.Id);
                DocumentFieldRepository.UpdateDocumentField(DocumentField);
                int dem = 0;
                foreach (var item in listFile)
                {
                    dem = dem + 1;
                    var type = item.FileName.Split('.').Last();
                    var FileName = DocumentField.Name.Replace(" ", "_");
                    var name = Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(FileName).ToLower();
                    var DocumentAttribute = new DocumentAttribute();
                    DocumentAttribute.IsDeleted = false;
                    DocumentAttribute.CreatedUserId = WebSecurity.CurrentUserId;
                    DocumentAttribute.ModifiedUserId = WebSecurity.CurrentUserId;
                    DocumentAttribute.AssignedUserId = WebSecurity.CurrentUserId;
                    DocumentAttribute.CreatedDate = DateTime.Now;
                    DocumentAttribute.ModifiedDate = DateTime.Now;
                    DocumentAttribute.OrderNo = dem;
                    if (!string.IsNullOrEmpty(fileName))
                    {
                        DocumentAttribute.File = fileName + "(" + DocumentField.Id + "_" + DocumentAttribute.OrderNo + ")" + "." + type;
                    }
                    else
                    {
                        DocumentAttribute.File = name + "(" + DocumentField.Id + "_" + DocumentAttribute.OrderNo + ")" + "." + type;
                    }
                    DocumentAttribute.Size = item.ContentLength.ToString();
                    DocumentAttribute.TypeFile = type;
                    DocumentAttribute.DocumentFieldId = DocumentField.Id;
                    DocumentAttributeRepository.InsertDocumentAttribute(DocumentAttribute);
                    var path = System.Web.HttpContext.Current.Server.MapPath("~" + Helpers.Common.GetSetting(Category));
                    bool isExists = System.IO.Directory.Exists(path);
                    if (!isExists)
                        System.IO.Directory.CreateDirectory(path);
                    item.SaveAs(path + DocumentAttribute.File);
                }
            }
            else
            {
                FailedMessageKey = "Vui lòng chụp ảnh!";
            }
        }
        #endregion

        // cập nhat phan quyen xem tài liệu khi các moduel khác chỉnh sửa phân quyền
        #region UpdateStaff
        public static void UpdateStaff(string Staff, int? CategoryId, string Category)
        {
            DocumentFieldRepository DocumentFieldRepository = new DocumentFieldRepository(new Domain.Staff.ErpStaffDbContext());
            var document = DocumentFieldRepository.GetDocumentFieldByCategory(Category, CategoryId);
            if (document != null)
            {
                document.IsSearch = Staff;
                DocumentFieldRepository.UpdateDocumentField(document);
            }
        }
        #endregion

    }
}
