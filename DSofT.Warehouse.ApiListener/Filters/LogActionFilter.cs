using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Erp.BackOffice.Filters
{
    using System.Web.Mvc;

    using Erp.Domain;
    using Erp.Domain.Entities;
    using Erp.Domain.Repositories;
    using Erp.Utilities;

    public class LogActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.HttpContext.Request.HttpMethod == "POST")
            {
                var actionDescriptor = filterContext.ActionDescriptor;
                string controllerName = actionDescriptor.ControllerDescriptor.ControllerName;
                string actionName = actionDescriptor.ActionName;
                string userName = filterContext.HttpContext.User.Identity.Name;
                string area = filterContext.RouteData.DataTokens["Area"] != null
                              ? filterContext.RouteData.DataTokens["Area"].ToString()
                              : string.Empty;

                var actionFilterName = new ActionFilterName();

                int logType = actionFilterName.GetActionType(actionName);
                if (logType != 0)
                {
                    var keys = filterContext.HttpContext.Request.Form.AllKeys;
                    List<string> listData = keys.Select(key => string.Format("{0} = {1}", key, filterContext.HttpContext.Request.Form[key])).ToList();

                    string data = string.Join(", ", listData);

                    var boLog = new BOLog
                        {
                            UserName = userName,
                            Area = area,
                            Controller = controllerName,
                            Action = actionName,
                            Data = data,
                            Type = logType,
                            CreatedDate = DateTime.Now,
                            Note = string.Empty
                        };

                    this.Log(boLog);
                }
            }
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
        }

        private void Log(BOLog boLog)
        {
            var boLogRepository = new BOLogRepository(new ErpDbContext());
            boLogRepository.SaveBOLog(boLog);
        }
    }
}