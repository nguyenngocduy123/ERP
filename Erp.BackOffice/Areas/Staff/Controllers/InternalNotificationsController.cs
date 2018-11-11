using System.Globalization;
using Erp.BackOffice.Staff.Models;
using Erp.BackOffice.Administration.Models;
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

namespace Erp.BackOffice.Staff.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class InternalNotificationsController : Controller
    {
        private readonly IInternalNotificationsRepository InternalNotificationsRepository;
        private readonly IUserRepository userRepository;
        private readonly IStaffsRepository StaffsRepository;
        private readonly INotificationsDetailRepository notificationsRepository;
        public InternalNotificationsController(
            IInternalNotificationsRepository _InternalNotifications
            , IUserRepository _user
            ,IStaffsRepository staffs
            ,INotificationsDetailRepository notificaations
            )
        {
            InternalNotificationsRepository = _InternalNotifications;
            userRepository = _user;
            StaffsRepository = staffs;
            notificationsRepository = notificaations;
        }

        #region Index

        public ViewResult Index(string titles, int? UserId, string type)
        {
            var user = userRepository.GetUserById(WebSecurity.CurrentUserId);
            var start_date = Request["start_date"];
            var end_date = Request["end_date"];
            var staff = Helpers.Common.GetStaffByCurrentUser();

            IEnumerable<InternalNotificationsViewModel> q = InternalNotificationsRepository.GetAllvwInternalNotifications()
                .Select(item => new InternalNotificationsViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    ModifiedDate = item.ModifiedDate,
                    Content=item.Content,
                   Type=item.Type,
                    PlaceOfNotice = item.PlaceOfNotice,
                    PlaceOfReceipt = item.PlaceOfReceipt,
                    Titles = item.Titles,
                    UserName=item.UserName,
                    FullName=item.FullName
                }).ToList();
            if (!string.IsNullOrEmpty(titles))
            {
                q = q.Where(item => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.Titles).ToLower().Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(titles).ToLower()));
            }
            if (!string.IsNullOrEmpty(type))
            {
                q = q.Where(item => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.Type).ToLower().Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(type).ToLower()));
            }
            if (UserId != null && UserId.Value > 0)
            {
                q = q.Where(item => item.CreatedUserId == UserId);
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
            if (user.UserTypeId == 1)
            {
                q = q.OrderByDescending(x => x.ModifiedDate);
            }
            else
            {
                foreach (var i in q)
                {
                    List<string> listIdStaff = new List<string>();
                    if (!string.IsNullOrEmpty(i.PlaceOfReceipt))
                    {
                        listIdStaff = i.PlaceOfReceipt.Split(',').ToList();
                        var aaa = listIdStaff.Any(id2 => id2 == staff.Id.ToString());
                        if (aaa == false && i.CreatedUserId != user.Id)
                        {
                            i.IsDeleted = true;
                        }
                    }
                    else
                    {
                        if (i.CreatedUserId != user.Id)
                        {
                            i.IsDeleted = true;
                        }
                    }
                }
                q = q.Where(x => x.IsDeleted != true).OrderByDescending(x => x.ModifiedDate);
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
            var model = new InternalNotificationsViewModel();
            Session["file"] = null;
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(InternalNotificationsViewModel model)
        {
            var q = Request["staffListcancel"];
            if (ModelState.IsValid)
            {
                var user = userRepository.GetUserById(WebSecurity.CurrentUserId);
                var Notification = new Domain.Staff.Entities.InternalNotifications();
                Notification.IsDeleted = false;
                Notification.CreatedUserId = WebSecurity.CurrentUserId;
                Notification.ModifiedUserId = WebSecurity.CurrentUserId;
                Notification.CreatedDate = DateTime.Now;
                Notification.ModifiedDate = DateTime.Now;
                Notification.Titles = model.Titles;
                Notification.Type ="notifications";
                Notification.Content = model.Content;
                Notification.PlaceOfNotice = WebSecurity.CurrentUserId.ToString();
                Notification.PlaceOfReceipt = q;
                Notification.Disable = false;
                Notification.Seen = false;
                Notification.ModuleName = "InternalNotifications";
                Notification.ActionName = "NotificationsDetail";
                InternalNotificationsRepository.InsertInternalNotifications(Notification);
                Erp.BackOffice.Staff.Controllers.DocumentFieldController.SaveUpload(Notification.Titles, q, Notification.Id, "InternalNotifications");
             
                var ProfileImage = "";
                var Name = user.FullName;
                var path = Helpers.Common.GetSetting("Staff");
                if (string.IsNullOrEmpty(user.ProfileImage))
                {
                    ProfileImage = "/assets/img/no-avatar.png";
                }
                else
                {
                    ProfileImage = path + user.ProfileImage;
                }
                return Json(new
                {
                    id = Notification.Id,
                    message = Erp.BackOffice.Hubs.ErpHub.Contentmessage(Notification.Id, Notification.Id, ProfileImage, Notification.Titles, Name, Notification.CreatedDate, Notification.ActionName, Notification.ModuleName)
                });
            }
            Session["file"] = null;
            return View(model);
        }

        #endregion

     
        
        #region search staff
        public ViewResult Search(string CodeStaff, string NameStaff, string Position, int? branchId, int? DepartmentId,string StaffList)
        {
            List<string> listIdStaff = new List<string>();

            var model = new InternalNotificationsViewModel();
            model.StaffList = StaffsRepository.GetvwAllStaffs()
                .Select(item => new StaffsViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    ProfileImage = item.ProfileImage,
                    Code = item.Code,
                    BranchDepartmentId = item.BranchDepartmentId,
                    Sale_BranchId = item.Sale_BranchId,
                    Staff_DepartmentId = item.Staff_DepartmentId,
                    Position = item.Position,
                    Gender = item.Gender
                }).ToList();
            if (!string.IsNullOrEmpty(StaffList))
            {
                listIdStaff = StaffList.Split(',').ToList();
                model.StaffList = model.StaffList.Where(id1 => !listIdStaff.Any(id2 => id2 == id1.Id.ToString())).ToList();
            }
            if (!string.IsNullOrEmpty(CodeStaff))
            {
                model.StaffList = model.StaffList.Where(item => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.Code).ToLower().Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(CodeStaff).ToLower()));
            }

            if (!string.IsNullOrEmpty(NameStaff))
            {
                model.StaffList = model.StaffList.Where(item => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.Name).ToLower().Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(NameStaff).ToLower()));

            }
            if (!string.IsNullOrEmpty(Position))
            {
                model.StaffList = model.StaffList.Where(item => item.Position == Position);

            }
            if (branchId != null && branchId.Value > 0)
            {
                model.StaffList = model.StaffList.Where(item => item.Sale_BranchId == branchId);

            }
            if (DepartmentId != null && DepartmentId.Value > 0)
            {
                model.StaffList = model.StaffList.Where(item => item.BranchDepartmentId == DepartmentId);

            }
            var user = userRepository.GetUserById(WebSecurity.CurrentUserId);
         
                if (user.UserTypeId == 1)
                {
                    model.StaffList = model.StaffList.OrderBy(m => m.Name);
                }
                else
                {
                    model.StaffList = model.StaffList.Where(x => x.Sale_BranchId == user.BranchId).OrderBy(m => m.Name);
                }
            return View(model);
        }


        #endregion

        #region AddStaff
        [HttpPost]
        public ActionResult AddStaff(string check)
        {
            string[] arrDeleteId = check.Split(',');
            List<StaffsViewModel> list = new List<StaffsViewModel>();
            for (int i = 0; i < arrDeleteId.Count(); i++)
            {
                if (arrDeleteId[i]!="")
                {
                    var item = StaffsRepository.GetStaffsById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null&&item.IsDeleted!=true)
                    {
                        var model = new StaffsViewModel();
                        AutoMapper.Mapper.Map(item, model);
                        list.Add(model);
                    }
                }
            }
           
            return View(list);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var InternalNotifications = InternalNotificationsRepository.GetInternalNotificationsById(Id.Value);
            if (InternalNotifications != null && InternalNotifications.IsDeleted != true)
            {
                var model = new InternalNotificationsViewModel();
                AutoMapper.Mapper.Map(InternalNotifications, model);
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
        [ValidateInput(false)]
        public ActionResult Edit(InternalNotificationsViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var q = Request["staffListcancel"];
                    var user = userRepository.GetUserById(WebSecurity.CurrentUserId);

                    var InternalNotifications = InternalNotificationsRepository.GetInternalNotificationsById(model.Id);
                    AutoMapper.Mapper.Map(model, InternalNotifications);
                    InternalNotifications.ModifiedUserId = WebSecurity.CurrentUserId;
                    InternalNotifications.ModifiedDate = DateTime.Now;
                    InternalNotifications.PlaceOfReceipt = q;

                    InternalNotificationsRepository.UpdateInternalNotifications(InternalNotifications);

                    //cập nhật lại phân quyền ở trang lưu trữ tài liệu.
                    Erp.BackOffice.Staff.Controllers.DocumentFieldController.UpdateStaff(q,model.Id,"InternalNotifications");

                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                    //return Json(new { id = InternalNotifications.Id, message = "<li id=\"notifications_item_" + InternalNotifications.Id + "\"><a style=\"color:red !important\" href=\"/InternalNotifications/Edit/?Id=" + InternalNotifications.Id + "\"><b>" + user.FullName + " " + string.Format("Cập nhật \"{0}\" lúc {1}", InternalNotifications.Titles, InternalNotifications.ModifiedDate.Value.ToString("HH:mm - dd/MM/yyyy")) + "</b></a></li>" });
                    var staff = Helpers.Common.GetStaffByCurrentUser();
                    var ProfileImage = "";
                    var Name = "";
                    if (staff.Name == null)
                    {
                        Name = user.FullName;
                    }
                    else
                    {
                        Name = staff.Name;
                    }
                    if (string.IsNullOrEmpty(staff.ProfileImage))
                    {
                        ProfileImage = "/assets/img/no-avatar.png";
                    }
                    else
                    {
                        ProfileImage = "/Data/HinhSV/" + staff.ProfileImage;
                    }
                    return Json(new
                    {
                        id = InternalNotifications.Id,
                        StaffId = InternalNotifications.PlaceOfReceipt,
                        message = "<li id=\"notifications_item_" +
                        InternalNotifications.Id + "\"><a style=\"color:red !important\" href=\"/InternalNotifications/NotificationsDetail/?NotificationsId="
                        + InternalNotifications.Id + "\"><img  style=\"max-height: 47px;min-width:42px\" src=\""
                        + ProfileImage + "\"class=\"msg-photo\"><span class=\"msg-body\"><span class=\"msg-title\"><span class=\"blue\"><b>"
                        + Name + "</b></span>cập nhật:" + InternalNotifications.Titles + "...</span><span class=\"msg-time\"><i class=\"ace-icon fa fa-clock-o\"></i> "
                        + InternalNotifications.ModifiedDate.Value.ToString("HH:mm dd/MM/yyyy") + "</span></span></a></li>"
                    });
                }

                return RedirectToAction("Edit", new { Id = model.Id });
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
            var InternalNotifications = InternalNotificationsRepository.GetInternalNotificationsById(Id.Value);
            if (InternalNotifications != null && InternalNotifications.IsDeleted != true)
            {
                var model = new InternalNotificationsViewModel();
                AutoMapper.Mapper.Map(InternalNotifications, model);
                
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
                    var item = InternalNotificationsRepository.GetInternalNotificationsById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if(item != null)
                    {
                        //if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        //{
                        //    TempData["FailedMessage"] = "NotOwner";
                        //    return RedirectToAction("Index");
                        //}

                        item.IsDeleted = true;
                        InternalNotificationsRepository.UpdateInternalNotifications(item);
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

        public ViewResult NotificationsDetail(int? NotificationsId)
        {
            var user = userRepository.GetUserById(WebSecurity.CurrentUserId);
            var staff = Erp.BackOffice.Helpers.Common.GetStaffByCurrentUser();
            var notifications = InternalNotificationsRepository.GetvwInternalNotificationsById(NotificationsId.Value);
            var model = new InternalNotificationsViewModel();
            AutoMapper.Mapper.Map(notifications, model);
            var staffCreateNotifications = StaffsRepository.GetvwStaffsByUser(model.CreatedUserId.Value);
            if (staffCreateNotifications != null)
            {
                ViewBag.Staff = staffCreateNotifications;
            }
            else
            {
                ViewBag.Staff = null;
            }
            model.NotificationsDetailList = notificationsRepository.GetAllvwNotificationsDetailbyId(NotificationsId)
                .Select(item => new NotificationsDetailViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Code = item.Code,
                    Comment = item.Comment,
                    Gender = item.Gender,
                    Name = item.Name,
                    ProfileImage = item.ProfileImage,
                    StaffId = item.StaffId
                }).OrderByDescending(x => x.ModifiedDate);

            return View(model);
        }

        #region NotificationsDetail
        [HttpPost]
        public ActionResult NotificationsDetail(string comment, int? NotificationsId)
        {
            var staff = Erp.BackOffice.Helpers.Common.GetStaffByCurrentUser();
            if (staff.UserId > 0)
            {
                if (!string.IsNullOrEmpty(comment))
                {
                    var notificationsDetail = new NotificationsDetail();
                    notificationsDetail.IsDeleted = false;
                    notificationsDetail.CreatedUserId = WebSecurity.CurrentUserId;
                    notificationsDetail.ModifiedUserId = WebSecurity.CurrentUserId;
                    notificationsDetail.CreatedDate = DateTime.Now;
                    notificationsDetail.ModifiedDate = DateTime.Now;
                    notificationsDetail.Comment = comment;
                    notificationsDetail.NotificationsId = NotificationsId;
                    notificationsDetail.StaffId = staff.Id;
                    notificationsRepository.InsertNotificationsDetail(notificationsDetail);
                }
            }
            //IQueryable<NotificationsDetailViewModel> q = notificationsRepository.GetAllvwNotificationsDetailbyId(NotificationsId)
            //    .Select(item => new NotificationsDetailViewModel
            //    {
            //     Id=item.Id,
            //     CreatedUserId = item.CreatedUserId,
            //     CreatedDate = item.CreatedDate,
            //     ModifiedUserId = item.ModifiedUserId,
            //     ModifiedDate = item.ModifiedDate,
            //     Code = item.Code,
            //     Comment = item.Comment,
            //     Gender = item.Gender,
            //     Name = item.Name,
            //     ProfileImage = item.ProfileImage,
            //     StaffId = item.StaffId
            //    }).OrderByDescending(x => x.ModifiedDate);
            return Content("");
        }
        #endregion
    }
}
