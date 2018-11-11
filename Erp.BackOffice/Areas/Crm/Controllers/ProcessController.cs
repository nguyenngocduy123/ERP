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
using Erp.Domain.Crm.Repositories;
using Erp.Domain.Repositories;
using System.Data;
using System.Data.SqlClient;

namespace Erp.BackOffice.Crm.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class ProcessController : Controller
    {
        private readonly IProcessRepository processRepository;
        private readonly IUserRepository userRepository;
        private readonly IProcessActionRepository processActionRepository;
        private readonly IProcessStageRepository processStageRepository;
        private readonly IProcessStepRepository processStepRepository;
        private readonly IProcessAppliedRepository processAppliedRepository;

        public ProcessController(
            IProcessRepository _Process
            , IUserRepository _user
            , IProcessActionRepository _ProcessAction
            , IProcessStageRepository _ProcessStage
            , IProcessStepRepository _ProcessStep
            , IProcessAppliedRepository processApplied
            )
        {
            processRepository = _Process;
            userRepository = _user;
            processActionRepository = _ProcessAction;
            processStageRepository = _ProcessStage;
            processStepRepository = _ProcessStep;
            processAppliedRepository = processApplied;
        }

        #region Index

        public ViewResult Index(string txtSearch)
        {

            IQueryable<ProcessViewModel> q = processRepository.GetAllProcess()
                .Select(item => new ProcessViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                }).OrderByDescending(m => m.ModifiedDate);

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        #endregion

        #region Create
        public ViewResult Create()
        {
            var model = new ProcessViewModel();
            model.CategorySelectList = Helpers.SelectListHelper.GetSelectList_Category("process_category", null, "Value", App_GlobalResources.Wording.Empty);
            model.EntitySelectList = Helpers.SelectListHelper.GetSelectList_Module(null);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ProcessViewModel model)
        {
            if (ModelState.IsValid)
            {
                var Process = new Domain.Crm.Entities.Process();
                AutoMapper.Mapper.Map(model, Process);
                Process.IsDeleted = false;
                Process.CreatedUserId = WebSecurity.CurrentUserId;
                Process.ModifiedUserId = WebSecurity.CurrentUserId;
                Process.CreatedDate = DateTime.Now;
                Process.ModifiedDate = DateTime.Now;
                processRepository.InsertProcess(Process);
                //lưu danh sách action sử dụng process
                foreach (var item in model.DetailList)
                {
                    var action = new Domain.Crm.Entities.ProcessApplied();
                    action.IsDeleted = false;
                    action.CreatedUserId = WebSecurity.CurrentUserId;
                    action.ModifiedUserId = WebSecurity.CurrentUserId;
                    action.CreatedDate = DateTime.Now;
                    action.ModifiedDate = DateTime.Now;
                    action.ActionName = item.ActionName;
                    action.ModuleName = item.ModuleName;
                    action.ProcessId = Process.Id;
                    processAppliedRepository.InsertProcessApplied(action);
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
            var Process = processRepository.GetProcessById(Id.Value);
            if (Process != null && Process.IsDeleted != true)
            {
                var model = new ProcessViewModel();
                AutoMapper.Mapper.Map(Process, model);

                //if (model.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                //{
                //    TempData["FailedMessage"] = "NotOwner";
                //    return RedirectToAction("Index");
                //}
                model.CategorySelectList = Helpers.SelectListHelper.GetSelectList_Category("process_category", null, "Value", App_GlobalResources.Wording.Empty);
                model.EntitySelectList = Helpers.SelectListHelper.GetSelectList_Module(null);
                //danh sách action sử dụng process
                model.DetailList = processAppliedRepository.GetAllProcessApplied().Where(x => x.ProcessId.Value == Process.Id)
                    .Select(x => new ProcessAppliedViewModel
                    {
                        ActionName = x.ActionName,
                        Id = x.Id,
                        ModuleName = x.ModuleName,
                        ProcessId = x.ProcessId
                    }).ToList();
                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(ProcessViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var Process = processRepository.GetProcessById(model.Id);
                    AutoMapper.Mapper.Map(model, Process);
                    Process.ModifiedUserId = WebSecurity.CurrentUserId;
                    Process.ModifiedDate = DateTime.Now;
                    processRepository.UpdateProcess(Process);
                    //xóa list dưới database cập nhật lại list mới.
                    var actionList = processAppliedRepository.GetAllProcessApplied().Where(x => x.ProcessId.Value == Process.Id).ToList();
                    for (int i = 0; i < actionList.Count(); i++)
                    {
                        processAppliedRepository.DeleteProcessApplied(actionList[i].Id);
                    }
                    //lưu danh sách action sử dụng process
                    foreach (var item in model.DetailList)
                    {
                        var action = new Domain.Crm.Entities.ProcessApplied();
                        action.IsDeleted = false;
                        action.CreatedUserId = WebSecurity.CurrentUserId;
                        action.ModifiedUserId = WebSecurity.CurrentUserId;
                        action.CreatedDate = DateTime.Now;
                        action.ModifiedDate = DateTime.Now;
                        action.ActionName = item.ActionName;
                        action.ModuleName = item.ModuleName;
                        action.ProcessId = Process.Id;
                        processAppliedRepository.InsertProcessApplied(action);
                    }
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
        public ActionResult Detail(int? Id, bool Test = false)
        {
            var Process = processRepository.GetProcessById(Id.Value);
            if (Process != null && Process.IsDeleted != true)
            {
                var model = new ProcessViewModel();
                AutoMapper.Mapper.Map(Process, model);

                //if (model.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                //{
                //    TempData["FailedMessage"] = "NotOwner";
                //    return RedirectToAction("Index");
                //}

                //if (Test)
                //{
                //    Erp.Domain.Sale.Repositories.PurchaseOrderRepository purchaseOrderRepository = new Domain.Sale.Repositories.PurchaseOrderRepository(new Domain.Sale.ErpSaleDbContext());
                //    var inputData = purchaseOrderRepository.GetPurchaseOrderById(21);
                //    var inputDataAfter = inputData;
                //    inputDataAfter.AssignedUserId = 2613;
                //    Run(ActionName.Edit, inputData, inputDataAfter);
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
                    var item = processRepository.GetProcessById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                        {
                            TempData["FailedMessage"] = "NotOwner";
                            return RedirectToAction("Index");
                        }

                        item.IsDeleted = true;
                        processRepository.UpdateProcess(item);
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

        #region SubList

        public ViewResult SubList_ProcessAction(int ProcessId)
        {
            IQueryable<ProcessActionViewModel> q = processActionRepository.GetAllProcessAction()
                .Where(item => item.ProcessId == ProcessId)
                .Select(item => new ProcessActionViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    ActionType = item.ActionType,
                    OrderNo = item.OrderNo
                }).OrderBy(m => m.OrderNo);
            return View(q);
        }
        #endregion

        #region Run workflow
        //public enum ActionName
        //{
        //    Create,
        //    Edit,
        //    Delete
        //}

        public static void Run(string moduleName, string actionName, int? IdData, int? UserId, object beforeEntityData = null, object afterEntityData = null,string DrugStore=null)
        {
            if (afterEntityData == null)
                return;

            string Entity = afterEntityData.GetType().Name;
            ProcessRepository processRepository = new ProcessRepository(new Domain.Crm.ErpCrmDbContext());
            ProcessAppliedRepository processAppliedRepository = new ProcessAppliedRepository(new Domain.Crm.ErpCrmDbContext());
            UserRepository userRepository = new UserRepository(new Domain.ErpDbContext());
           
            //Lấy danh sách proccess theo thứ tự
            //   var q = processRepository.GetAllProcess().Where(item => item.Category == "Workflow" && item.DataSource == Entity && item.IsActive.Value);
            List<Process> q = new List<Process>();
            var action = processAppliedRepository.GetAllProcessApplied().Where(x => x.ModuleName == moduleName && x.ActionName == actionName).ToList();
            foreach (var i in action)
            {
                var process = processRepository.GetProcessById(i.ProcessId.Value);
                q.Add(process);
            }
            //if (actionName == ActionName.Create)
            //{
            //  q = q.Where(item => item.RecordIsCreated.Value);
            //}
            //else if (actionName == ActionName.Edit)
            //{
            //Check case user proceed assignee user
            //var assignedUserId_Before = beforeEntityData.GetType().GetProperties()
            //    .Where(p => p.Name == "AssignedUserId").FirstOrDefault()
            //    .GetGetMethod().Invoke(beforeEntityData, null);

            //var assignedUserId_After = afterEntityData.GetType().GetProperties()
            //    .Where(p => p.Name == "AssignedUserId").FirstOrDefault()
            //    .GetGetMethod().Invoke(afterEntityData, null);

            //var hasAssignee = assignedUserId_Before != assignedUserId_After ? true : false;

            //  q = q.Where(item => (item.RecordIsAssigned.Value && hasAssignee) || item.RecordFieldsChanges.Value);
            //}
            //else if (actionName == ActionName.Delete)
            //{
            //    q = q.Where(item => item.RecordIsDeleted.Value);
            //}

            var qFiltered = q.ToList();
            if (qFiltered != null && qFiltered.Count > 0)
            {
                ProcessActionRepository processActionRepository = new ProcessActionRepository(new Domain.Crm.ErpCrmDbContext());
                ModuleRelationshipRepository moduleRelationshipRepository = new ModuleRelationshipRepository(new Domain.ErpDbContext());
                ModuleRepository moduleRepository = new ModuleRepository(new Domain.ErpDbContext());
                Dictionary<string, string> data = (from x in afterEntityData.GetType().GetProperties() select x)
                    .ToDictionary(x => "{" + Entity + "." + x.Name + "}", x => (x.GetGetMethod().Invoke(afterEntityData, null) == null ? "" : x.GetGetMethod().Invoke(afterEntityData, null).ToString()));

                //Add module relationship data
                var moduleRelationship = moduleRelationshipRepository.GetAllModuleRelationship()
                    .Where(item => item.First_ModuleName == Entity)
                    .ToList();

                if (moduleRelationship != null && moduleRelationship.Count > 0)
                {
                    foreach (var item in moduleRelationship)
                    {
                        //Get table name
                        var secondTableName = moduleRepository.GetAllModule().Where(i => i.Name == item.Second_ModuleName).FirstOrDefault().TableName;
                        //Get data of each module_relationship item
                        string keyValue = data["{" + Entity + "." + item.First_MetadataFieldName + "}"];
                        if (!string.IsNullOrEmpty(keyValue))
                        {
                            string sql = string.Format("select * from [{0}] where [{1}] = {2}", secondTableName, item.Second_MetadataFieldName, keyValue);
                            var q_temp = Domain.Helper.SqlHelper.QuerySQL(sql).FirstOrDefault();

                            if (q_temp != null)
                            {
                                foreach (KeyValuePair<string, object> c in q_temp)
                                {
                                    data.Add("{" + item.Second_ModuleName_Alias + "." + c.Key + "}", c.Value == null ? "" : c.Value.ToString());
                                }
                            }
                        }
                    }
                }

                foreach (var item in qFiltered)
                {
                    //Lấy danh sách action theo thứ tự
                    var actionList = processActionRepository.GetAllProcessAction().Where(x => x.ProcessId == item.Id).ToList();
                    if (UserId != null)
                    {
                        var user = userRepository.GetUserById(UserId.Value);
                        item.QueryRecivedUser = item.QueryRecivedUser.Replace("{UserTypeId}", "'" + user.UserTypeId.ToString() + "'");
                        item.QueryRecivedUser = item.QueryRecivedUser.Replace("{BranchId}", "'" + user.BranchId.ToString() + "'");
                        item.QueryRecivedUser = item.QueryRecivedUser.Replace("{AssignedUserId}", "'" + user.Id.ToString() + "'");
                    }
                    if (DrugStore != null)
                    {
                        item.QueryRecivedUser = item.QueryRecivedUser.Replace("{DrugStore}",DrugStore);
                    }
                    var collection = Domain.Helper.SqlHelper.QuerySQL<int>(item.QueryRecivedUser);
                  
                        foreach (var x in actionList)
                        {
                            switch (x.ActionType)
                            {
                                case "SendEmail":
                                    foreach (var ii in collection)
                                       {
                                          sendEmail(data, x.TemplateObject);
                                       }
                                    break;
                                case "CreateTask":
                                    createTask(data, x.TemplateObject);
                                    break;
                                case "CreateNotifications":
                                      foreach (var ii in collection)
                                       {
                                          createNotifications(data, x.TemplateObject, ii, moduleName, IdData);
                                       }
                                    break;
                                default:
                                    break;
                            }
                        }
                   
                }
            }
        }

        static void sendEmail(Dictionary<string, string> data, byte[] TemplateObject)
        {
            //Lấy TemplateObject chuyển thành EmailTemplate
            EmailTemplateViewModel emailTemplateViewModel = Erp.BackOffice.Helpers.Common.ByteArrayToObject(TemplateObject) as EmailTemplateViewModel;

            //Điền dữ liệu từ dòng dữ liệu vừa được tạo ra
            emailTemplateViewModel.From = replaceData(emailTemplateViewModel.From, data);
            emailTemplateViewModel.To = replaceData(emailTemplateViewModel.To, data);
            emailTemplateViewModel.Cc = replaceData(emailTemplateViewModel.Cc, data);
            emailTemplateViewModel.Bcc = replaceData(emailTemplateViewModel.Bcc, data);
            emailTemplateViewModel.Subject = replaceData(emailTemplateViewModel.Subject, data);
            emailTemplateViewModel.Body = replaceData(emailTemplateViewModel.Body, data);
            emailTemplateViewModel.Regarding = replaceData(emailTemplateViewModel.Regarding, data);

            //Thực hiện gửi mail bình thường
            Erp.BackOffice.Helpers.Common.SendEmailAttachment(
                Erp.BackOffice.Helpers.Common.GetSetting("Email")
                , Erp.BackOffice.Helpers.Common.GetSetting("EmailPassword")
                , emailTemplateViewModel.To
                , emailTemplateViewModel.Subject
                , emailTemplateViewModel.Body
                , emailTemplateViewModel.Cc
                , emailTemplateViewModel.Bcc
                , "Warehouse ELUTUS"
                );
        }

        static void createTask(Dictionary<string, string> data, byte[] TemplateObject)
        {
            //Lấy TemplateObject chuyển thành EmailTemplate
            TaskTemplateViewModel taskTemplateViewModel = Erp.BackOffice.Helpers.Common.ByteArrayToObject(TemplateObject) as TaskTemplateViewModel;

            //Điền dữ liệu từ dòng dữ liệu vừa được tạo ra
            taskTemplateViewModel.Subject = replaceData(taskTemplateViewModel.Subject, data);
            taskTemplateViewModel.Description = replaceData(taskTemplateViewModel.Description, data);
            taskTemplateViewModel.AssignedUserId = Convert.ToInt32(replaceData(taskTemplateViewModel.AssignedUserName, data));
            taskTemplateViewModel.StartDate = replaceData(taskTemplateViewModel.StartDate, data);
            taskTemplateViewModel.DueDate = replaceData(taskTemplateViewModel.DueDate, data);
            //Thực hiện gửi mail bình thường
            TaskRepository TaskRepository = new TaskRepository(new Domain.Crm.ErpCrmDbContext());
            var Task = new Task();
            AutoMapper.Mapper.Map(taskTemplateViewModel, Task);
            Task.IsDeleted = false;
            Task.CreatedUserId = WebSecurity.CurrentUserId;
            Task.ModifiedUserId = WebSecurity.CurrentUserId;
            Task.CreatedDate = DateTime.Now;
            Task.ModifiedDate = DateTime.Now;
            Task.Type = "task";
            Task.StartDate = Convert.ToDateTime(taskTemplateViewModel.StartDate);
            Task.DueDate = Convert.ToDateTime(taskTemplateViewModel.DueDate);
            TaskRepository.InsertTask(Task);
         
        }
        static void createNotifications(Dictionary<string, string> data, byte[] TemplateObject, int? UserId, string ModuleName, int? ID)
        {
            //Lấy TemplateObject chuyển thành EmailTemplate
            TaskViewModel notificationsViewModel = Erp.BackOffice.Helpers.Common.ByteArrayToObject(TemplateObject) as TaskViewModel;
            UserRepository userRepository = new UserRepository(new Domain.ErpDbContext());
            //Điền dữ liệu từ dòng dữ liệu vừa được tạo ra
            notificationsViewModel.Subject = replaceData(notificationsViewModel.Subject, data);
            notificationsViewModel.Description = replaceData(notificationsViewModel.Description, data);
            notificationsViewModel.AssignedUserId = Convert.ToInt32(replaceData(notificationsViewModel.AssignedUserName, data));

            //Thực hiện gửi mail bình thường
            TaskRepository TaskRepository = new TaskRepository(new Domain.Crm.ErpCrmDbContext());
            var Task = new Task();
            AutoMapper.Mapper.Map(notificationsViewModel, Task);
            Task.IsDeleted = false;
            Task.CreatedUserId = WebSecurity.CurrentUserId;
            Task.ModifiedUserId = UserId;
            Task.CreatedDate = DateTime.Now;
            Task.ModifiedDate = DateTime.Now;
            Task.Type = "notifications";
            Task.ParentId = ID;
            TaskRepository.InsertTask(Task);
            //lấy thông tin user gửi notifications hiện trên notifications 
            var user = userRepository.GetUserById(Task.CreatedUserId.Value);
            //send va hien thong bao notifications
            Erp.BackOffice.Hubs.ErpHub.CreateNotifications(ID, Task.Subject, user.FullName, user.ProfileImage, ModuleName, Task.CreatedDate, Task.Id, UserId);
        }
        static string replaceData(string inputStr, Dictionary<string, string> data)
        {
            if (string.IsNullOrEmpty(inputStr))
                return null;
            foreach (var item in data)
            {
                inputStr = inputStr.Replace(item.Key, item.Value);
            }

            return inputStr;
        }
        #endregion

        #region Apply business process flow
        public ActionResult Apply(string ProcessEntity, object EntityData = null)
        {
            //Get business proccess flow
            var process = processRepository.GetAllProcess().Where(item => item.Category == "business_process_flow" && item.DataSource == ProcessEntity && item.IsActive.Value).FirstOrDefault();
            if (process != null)
            {
                var model = new ProcessApplyViewModel();
                model.ProcessEntity = ProcessEntity;
                model.EntityData = EntityData;
                model.ListProcessStage = processStageRepository.GetAllProcessStage()
                    .Where(item => item.ProcessId == process.Id)
                    .Select(item => new ProcessStageViewModel
                    {
                        Id = item.Id,
                        Name = item.Name,
                        OrderNo = item.OrderNo
                    }).OrderBy(m => m.OrderNo).ToList();

                int activeStageId = model.ListProcessStage[0].Id;

                var q = processStepRepository.GetAllProcessStep()
                    .Where(item => item.StageId == activeStageId)
                    .Select(i => new ProcessStepViewModel
                    {
                        Id = i.Id,
                        Name = i.Name,
                        StepValue = i.StepValue,
                        IsRequired = i.IsRequired,
                        IsSequential = i.IsSequential,
                        EditControl = i.EditControl,
                        OrderNo = i.OrderNo
                    }).OrderBy(m => m.OrderNo).ToList();

                model.ListEditViewField = new List<Administration.Models.EditViewField>();
                foreach (var item in q)
                {
                    var editViewField = new Administration.Models.EditViewField();
                    editViewField.LabelName = item.Name;
                    editViewField.FieldName = item.StepValue;
                    editViewField.IsRequired = item.IsRequired;
                    editViewField.EditControl = item.EditControl;
                    editViewField.OrderNo = item.OrderNo;

                    model.ListEditViewField.Add(editViewField);
                }

                return View(model);
            }

            return null;
        }
        #endregion
    }
}
