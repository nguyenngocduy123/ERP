using System.Globalization;
using Erp.BackOffice.Crm.Models;
using Erp.BackOffice.Filters;
using Erp.Domain.Entities;
using Erp.Domain.Interfaces;
using Erp.Domain.Crm.Entities;
using Erp.Domain.Crm.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Erp.Utilities;
using WebMatrix.WebData;
using Erp.BackOffice.Helpers;
using Erp.BackOffice.Areas.Cms.Models;
using System.Web.Script.Serialization;

namespace Erp.BackOffice.Crm.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class TaskController : Controller
    {
        private readonly ITaskRepository TaskRepository;
        private readonly IUserRepository userRepository;
        private readonly IUserTypeRepository userTypeRepository;
        private readonly ICategoryRepository _categoryRepository;

        public TaskController(
            ITaskRepository _Task
            , IUserRepository _user
              ,IUserTypeRepository userType
            ,ICategoryRepository category
            )
        {
            TaskRepository = _Task;
            userRepository = _user;
            userTypeRepository = userType;
            _categoryRepository = category;
        }

        #region Index

        public ViewResult Index(string status, string Priority, int? CreateId, int? AssignId)
        {
            var start_date = Request["startDate"];
            var end_date = Request["endDate"];
            IQueryable<TaskViewModel> q = TaskRepository.GetAllvwTaskFull()
                .Where(x => x.Type == "task")
                .Select(item => new TaskViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Subject = item.Subject,
                    Status = item.Status,
                    ParentType = item.ParentType,
                    ParentId = item.ParentId,
                    AssignedUserId = item.AssignedUserId,
                    ProfileImage = item.ProfileImage,
                    FullName = item.FullName,
                    Type = item.Type,
                    Note = item.Note,
                    ReceiverImage = item.ReceiverImage,
                    ReceiverName = item.ReceiverName,
                    ReceiverUser = item.ReceiverUser
                }).OrderByDescending(m => m.ModifiedDate);
            bool bIsSearch = false;
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
                        bIsSearch = true;
                    }
                }
            }

            if (!string.IsNullOrEmpty(status))
            {
                q = q.Where(item => item.Status == status);
                bIsSearch = true;
            }
            if (!string.IsNullOrEmpty(Priority))
            {
                q = q.Where(item => item.Priority == Priority);
                bIsSearch = true;
            }
            if (CreateId != null && CreateId.Value > 0)
            {
                q = q.Where(item => item.CreatedUserId == CreateId);
                bIsSearch = true;
            }
            if (AssignId != null && AssignId.Value > 0)
            {
                q = q.Where(item => item.AssignedUserId == AssignId);
                bIsSearch = true;
            }
            ViewBag.Search = bIsSearch;
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }

        public ViewResult MyTasks()
        {
            IQueryable<TaskViewModel> q = TaskRepository.GetAllTask()
                .Where(item => item.AssignedUserId == WebSecurity.CurrentUserId
                && item.Status != "Deferred"
                && item.Status != "Completed")
                .Select(item => new TaskViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Subject = item.Subject,
                    Status = item.Status
                }).OrderByDescending(m => m.ModifiedDate);
            return View(q);
        }
        #endregion

        #region CreateOfUser
        public ViewResult CreateOfUser(DateTime date)
        {
            var model = new TaskViewModel();
            model.StartDate = date;
            model.DueDate = date;
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateOfUser(TaskViewModel model)
        {
            var urlRefer = Request["UrlReferrer"];
            if (ModelState.IsValid)
            {
                var Task = new Task();
                AutoMapper.Mapper.Map(model, Task);
                Task.IsDeleted = false;
                Task.CreatedUserId = WebSecurity.CurrentUserId;
                Task.ModifiedUserId = WebSecurity.CurrentUserId;
                Task.AssignedUserId = WebSecurity.CurrentUserId;
                Task.CreatedDate = DateTime.Now;
                Task.ModifiedDate = DateTime.Now;
                Task.Status = "pending";
                Task.ParentType = "Task";
                Task.Type = "task";
                TaskRepository.InsertTask(Task);
                Task.ParentId = Task.Id;
                TaskRepository.UpdateTask(Task);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                {
                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                    ViewBag.closePopup = "true";
                    model.Id = Task.Id;
                    ViewBag.urlRefer = urlRefer;
                    return View(model);
                }
                return Redirect(urlRefer);
            }
            return View(model);
        }
        #endregion

        #region Create
        public ViewResult Create(string ParentType, int? ParentId, string Description, string Subject,DateTime start, DateTime end)
        {
            var model = new TaskViewModel();
            model.ListUser = userRepository.GetAllUsers().ToList();
            model.ListUserType = userTypeRepository.GetUserTypes().ToList();
            model.ParentId = ParentId;
            model.ParentType = ParentType;
            model.Description = Description;
            model.Subject = Subject;
            //var date = DateTime.Now;
            model.StartDate = start;
            model.DueDate = end;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(TaskViewModel model)
        {
            model.ListUser = userRepository.GetAllUsers().ToList();
            model.ListUserType = userTypeRepository.GetUserTypes().ToList();
            var urlRefer = Request["UrlReferrer"];
            if (ModelState.IsValid)
            {
                List<string> ListUser = new List<string>();
                if (Request["user_check"] != null)
                {
                    ListUser = Request["user_check"].Split(',').ToList();
                    for (int i = 0; i < ListUser.Count(); i++)
                    {
                        var Task = new Task();
                        AutoMapper.Mapper.Map(model, Task);
                        Task.IsDeleted = false;
                        Task.CreatedUserId = WebSecurity.CurrentUserId;
                        Task.ModifiedUserId = WebSecurity.CurrentUserId;
                        Task.AssignedUserId = Convert.ToInt32(ListUser[i].ToString());
                        Task.CreatedDate = DateTime.Now;
                        Task.ModifiedDate = DateTime.Now;
                        Task.Status = "pending";
                        if (Task.ParentType == null)
                        {
                            Task.ParentType = "Task";
                        }
                        else
                        {
                            Task.ParentType = model.ParentType;
                        }
                        Task.Type = "Task";
                        TaskRepository.InsertTask(Task);
                        if (Task.ParentType == "Task")
                        {
                            Task.ParentId = Task.Id;
                        }
                        else
                        {
                            Task.ParentId = model.ParentId;
                        }
                        TaskRepository.UpdateTask(Task);

                        //gửi notifications cho người được phân quyền.
                        var task = TaskRepository.GetvwTaskById(Task.Id);
                        ProcessController.Run("Task"
                            , "Create"
                            , Task.Id
                            , Task.AssignedUserId
                            , null
                            , task);
                    }
                    if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                    {
                        ViewBag.closePopup = "true";
                        // model.Id = Task.Id;
                        ViewBag.urlRefer = urlRefer;
                      
                        return View(model);
                    }
                    return Redirect(urlRefer);
                }
                else
                {
                    TempData[Globals.FailedMessageKey] = App_GlobalResources.Wording.NoUserCheckTask;
                    return View(model);
                }

            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var Task = TaskRepository.GetTaskById(Id.Value);
            if (Task != null && Task.IsDeleted != true)
            {
                var model = new TaskViewModel();
                AutoMapper.Mapper.Map(Task, model);

                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(TaskViewModel model)
        {
            var urlRefer = Request["UrlReferrer"];
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var Task = TaskRepository.GetTaskById(model.Id);
                    AutoMapper.Mapper.Map(model, Task);
                    Task.ModifiedUserId = WebSecurity.CurrentUserId;
                    Task.ModifiedDate = DateTime.Now;
                    TaskRepository.UpdateTask(Task);

                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                    if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                    {
                        TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                        ViewBag.closePopup = "true";
                        model.Id = Task.Id;
                        ViewBag.urlRefer = urlRefer;
                        return View(model);
                    }
                    return Redirect(urlRefer);
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
            var Task = TaskRepository.GetvwTaskById(Id.Value);
            if (Task != null && Task.IsDeleted != true)
            {
                var model = new TaskViewModel();
                AutoMapper.Mapper.Map(Task, model);

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
                    var item = TaskRepository.GetTaskById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                        {
                            TempData["FailedMessage"] = "NotOwner";
                            return RedirectToAction("Index");
                        }

                        item.IsDeleted = true;
                        TaskRepository.UpdateTask(item);
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

        #region CheckSeen
        //[HttpPost]
        public ActionResult CheckSeen(int? Id)
        {
            var notifications = TaskRepository.GetTaskById(Id.Value);
            if (notifications != null)
            {
                if (notifications.AssignedUserId == notifications.ModifiedUserId)
                {
                    notifications.AssignedUserId = 0;
                    TaskRepository.UpdateTask(notifications);
                    return Content("notseen");
                }
                else
                {
                    notifications.AssignedUserId = WebSecurity.CurrentUserId;
                    TaskRepository.UpdateTask(notifications);
                    return Content("success");
                }
            }
            else
            {
                return Content("error");
            }
        }
        #endregion

        #region CheckAllSeen
        //[HttpPost]
        public ActionResult CheckAllSeen()
        {
            var notifications = TaskRepository.GetAllTask().Where(x => x.ModifiedUserId == WebSecurity.CurrentUserId).ToList();
            for (int i = 0; i < notifications.Count(); i++)
            {
                notifications[i].AssignedUserId = WebSecurity.CurrentUserId;
                TaskRepository.UpdateTask(notifications[i]);
            }
            return Content("success");
        }
        #endregion

        #region CheckDisable
        //[HttpPost]
        public ActionResult CheckDisable(int? Id)
        {
            var notifications = TaskRepository.GetTaskFullById(Id.Value);
            //chia làm 2 trường hợp. 1 TH chưa xem thì chuyển thành đã xem và ẩn thông báo đi
            //TH2 đã xem rồi thì chỉ ẩn thông báo thôi.
            if (notifications != null)
            {
                if (notifications.AssignedUserId.Value <= 0)
                {
                    notifications.IsDeleted = true;
                    notifications.AssignedUserId = WebSecurity.CurrentUserId;
                    TaskRepository.UpdateTask(notifications);
                    return Content("notseen");
                }
                else
                {
                    notifications.IsDeleted = true;
                    TaskRepository.UpdateTask(notifications);
                    return Content("seen");
                }
            }
            else
            {
                return Content("error");
            }
        }
        #endregion

        #region UpdateTask

        public ActionResult UpdateTask(int? Id)
        {
            var task = TaskRepository.GetvwTaskById(Id.Value);
            if (task != null && task.IsDeleted != true)
            {
                var model = new TaskViewModel();
                AutoMapper.Mapper.Map(task, model);
                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult UpdateTask(TaskViewModel model)
        {
            var urlRefer = Request["UrlReferrer"];
            var Task = TaskRepository.GetTaskById(model.Id);
            //   AutoMapper.Mapper.Map(model, Task);
            Task.Description = model.Description;
            Task.Priority = model.Priority;
            Task.Note = model.Note;
            Task.StartDate = model.StartDate;
            Task.DueDate = model.DueDate;
            Task.Status = model.Status;
            Task.ModifiedUserId = WebSecurity.CurrentUserId;
            Task.ModifiedDate = DateTime.Now;
            TaskRepository.UpdateTask(Task);

            TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
            if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
            {
                ViewBag.closePopup = "true";
                model.Id = Task.Id;
                ViewBag.urlRefer = urlRefer;
                return View(model);
            }
            return Redirect(urlRefer);
        }
        #endregion

        public ActionResult LogNotifications()
        {
            var user = userRepository.GetUserById(WebSecurity.CurrentUserId);
            //lấy danh sách thông báo của user hiện lên.
            List<TaskViewModel> q = TaskRepository.GetAllvwTask().Where(x => x.ModifiedUserId == user.Id && x.Type == "notifications")
                .Select(x => new TaskViewModel
                {
                    AssignedUserId = x.AssignedUserId,
                    CreatedDate = x.CreatedDate,
                    CreatedUserId = x.CreatedUserId,
                    FullName = x.FullName,
                    ProfileImage = x.ProfileImage,
                    Id = x.Id,
                    IsDeleted = x.IsDeleted,
                    ParentId = x.ParentId,
                    ParentType = x.ParentType,
                    Subject = x.Subject,
                    UserName = x.UserName,
                    ModifiedUserId = x.ModifiedUserId
                }).OrderByDescending(x => x.CreatedDate).ToList();
            return View(q);
        }

        #region Calendar
        public ViewResult Calendar(string status_check, int? month, int? year)
        {
            var user = userRepository.GetUserById(WebSecurity.CurrentUserId);
            List<string> ListCheck = new List<string>();
            if (!string.IsNullOrEmpty(status_check))
            {
                ListCheck = status_check.Split(',').ToList();
            }
            else
            {
                ListCheck = "pending,inprogress".Split(',').ToList();
            }
            List<TaskViewModel> q = new List<TaskViewModel>();
            IEnumerable<TaskViewModel> model = TaskRepository.GetAllvwTask()
                .Where(item => item.Type == "task" && item.AssignedUserId == user.Id)
                .Select(i => new TaskViewModel
                {
                    Description = i.Description,
                    CreatedDate = i.CreatedDate,
                    DueDate = i.DueDate,
                    FullName = i.FullName,
                    Id = i.Id,
                    ModifiedDate = i.ModifiedDate,
                    Note = i.Note,
                    ParentId = i.ParentId,
                    ParentType = i.ParentType,
                    Priority = i.Priority,
                    StartDate = i.StartDate,
                    Status = i.Status,
                    CreatedUserId = i.CreatedUserId,
                    ProfileImage = i.ProfileImage,
                    Subject = i.Subject,
                    Type = i.Type,
                    UserName = i.UserName,
                    ContactId = i.ContactId
                }).OrderByDescending(x => x.CreatedDate).ToList();
            for (int i = 0; i < ListCheck.Count(); i++)
            {
                var a = model.Where(x => x.Status == ListCheck[i].ToString());
                q = q.Union(a).ToList();
            }
            DateTime aDateTime = new DateTime(year.Value, month.Value, 1);
            // Cộng thêm 1 tháng và trừ đi một ngày.
            DateTime retDateTime = aDateTime.AddMonths(1).AddDays(-1);
            q = q.Where(x => aDateTime <= x.StartDate.Value && x.StartDate.Value <= retDateTime).OrderBy(x => x.StartDate).ToList();

            var category = _categoryRepository.GetCategoryByCode("task_status").Select(x => new CategoryViewModel
            {
                Code = x.Code,
                Description = x.Description,
                Id = x.Id,
                Name = x.Name,
                Value = x.Value,
                OrderNo = x.OrderNo
            }).ToList();
            var aa = category.Where(id1 => ListCheck.Any(id2 => id2 == id1.Value)).ToList();
            ViewBag.Category = aa.OrderBy(x => x.OrderNo).ToList();

            var dataEvent = q.Select(e => new
            {
                title = e.Subject,
                start = e.StartDate.Value.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                end = e.DueDate.Value.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                allDay = false,
                className = (e.Status == "pending" ? "label-info" : (e.Status == "inprogress" ? "label-warning" : (e.Status == "completed" ? "label-success" : "label-danger"))),
                url = string.Format("/Task/{0}/?Id={1}", (e.Status == "completed" ? "Detail" : "UpdateTask"), e.Id)
            }).ToList();

            ViewBag.dataEvent = new JavaScriptSerializer().Serialize(dataEvent);
            ViewBag.aDateTime = aDateTime.ToString("yyyy-MM-dd");

            return View(q);
        }
        #endregion

        #region DeleteTask
        [HttpPost]
        public ActionResult DeleteTask(int? id)
        {
            try
            {
                var item = TaskRepository.GetTaskById(id.Value);
                if (item != null)
                {
                    TaskRepository.DeleteTask(item.Id);
                }
                return Content("success");
            }
            catch (DbUpdateException)
            {
                return Content("error");
            }
        }
        #endregion
    }
}