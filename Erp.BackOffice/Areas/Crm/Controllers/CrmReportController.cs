using System.Globalization;
using Erp.BackOffice.Filters;
using Erp.Domain.Interfaces;
using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Erp.Utilities;
using WebMatrix.WebData;
using Erp.BackOffice.Sale.Models;
using Erp.BackOffice.Helpers;
using Erp.Domain.Helper;
using Newtonsoft.Json;
using Erp.Domain.Account.Interfaces;
using Erp.BackOffice.Account.Models;
using System.Data;
using Erp.BackOffice.Crm.Models;
using Erp.Domain.Crm.Interfaces;
using Erp.BackOffice.Areas.Cms.Models;
namespace Erp.BackOffice.Crm.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class CrmReportController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository userRepository;
        private readonly ILogServiceRemminderRepository logServiceReminderRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly IQueryHelper QueryHelper;
        private readonly ITaskRepository taskRepository;
        public CrmReportController(
             ILogServiceRemminderRepository logserviceReminder
            , IUserRepository _user
          , ITaskRepository task
            , IQueryHelper _QueryHelper
             , ICustomerRepository _Customer
               , ICategoryRepository category
            )
        {
            logServiceReminderRepository = logserviceReminder;
            customerRepository = _Customer;
            QueryHelper = _QueryHelper;
            userRepository = _user;
            taskRepository = task;
            _categoryRepository = category;
        }

        #region ServiceReminder
        public ViewResult ServiceReminder(string status_check)
        {
            //danh sách tái khám đến hạn
            //var quantityDate = Dormitory.BackOffice.Helpers.Common.GetSetting("quantity_Reminder_Date");
            //var date = DateTime.Now.AddDays(Convert.ToInt32(quantityDate));
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
            var user = userRepository.GetUserById(WebSecurity.CurrentUserId);
            IEnumerable<TaskViewModel> model = taskRepository.GetAllvwTask().Where(x => x.Type == "task" && x.AssignedUserId == user.Id)
                .Select(item => new TaskViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    CreatedDate = item.CreatedDate,
                    Description = item.Description,
                    DueDate = item.DueDate,
                    IsDeleted = item.IsDeleted,
                    Note = item.Note,
                    ParentId = item.ParentId,
                    ParentType = item.ParentType,
                    Priority = item.Priority,
                    StartDate = item.StartDate,
                    Status = item.Status,
                    Subject = item.Subject
                }).ToList();
            //q = q.Where(x => DateTime.Now <= x.DueDate && x.DueDate <= date);

            for (int i = 0; i < ListCheck.Count(); i++)
            {
                var a = model.Where(x => x.Status == ListCheck[i].ToString());
                q = q.Union(a).ToList();
            }
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
            return View(q);
        }
        #endregion
    }
}
