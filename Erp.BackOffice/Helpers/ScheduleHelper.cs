using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using Quartz.Impl;
using WebMatrix.WebData;
using Erp.Domain.Crm.Entities;
using Erp.Domain.Crm.Repositories;
using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Repositories;
using System.Net;
using System.Data;
using Erp.BackOffice.Crm.Controllers;

namespace Erp.BackOffice.Helpers
{
    public class ScheduleHelper
    {
        public static IScheduler Scheduler;
        public static void init()
        {
            //Common.Logging.LogManager.Adapter = new Common.Logging.Simple.ConsoleOutLoggerFactoryAdapter { Level = Common.Logging.LogLevel.Info };
            // Grab the Scheduler instance from the Factory 
            Scheduler = StdSchedulerFactory.GetDefaultScheduler();

            // and start it off
            Scheduler.Start();

            // define the job and tie it to our Job_NotifyCustomerLiabilities class
            IJobDetail job = JobBuilder.Create<Job_NotifyCustomerLiabilities>()
                .WithIdentity("job1", "group1")
                .Build();

           
            // Trigger the job to run now, and then repeat every 10 seconds
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(15)
                    .RepeatForever())
                .Build();

          //   Tell quartz to schedule the job using our trigger
            Scheduler.ScheduleJob(job, trigger);

            // define the job and tie it to our Job_NotifyCustomerLiabilities class
            //IJobDetail job_cham_cong = JobBuilder.Create<Job_Cham_Cong>()
            //    .WithIdentity("job_cham_cong1", "group2")
            //    .Build();
            //var hour = Convert.ToInt32(Common.GetSetting("run_hour"));
            //var minute = Convert.ToInt32(Common.GetSetting("run_minute"));
            // Trigger the job to run now, and then repeat every 10 seconds
            //ITrigger trigger_cham_cong = TriggerBuilder.Create()
            //    .WithIdentity("trigger_cham_cong1", "group2")
            //    .WithDailyTimeIntervalSchedule
            //      (s =>
            //         s.WithIntervalInHours(24)
            //        .OnEveryDay()
            //        .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(hour, minute))
            //      )
            //    .Build();
            ////   Tell quartz to schedule the job using our trigger
            //Scheduler.ScheduleJob(job_cham_cong, trigger_cham_cong);
        }

        public class Job_NotifyCustomerLiabilities : IJob
        {
            public void Execute(IJobExecutionContext context)
            {
                TaskRepository taskRepository = new TaskRepository(new Domain.Crm.ErpCrmDbContext());
                List<Task> task = new List<Task>();
                task = taskRepository.GetAllTask().Where(x => x.Type == "task" && (DateTime.Now > x.StartDate && x.DueDate > DateTime.Now)
                   && (x.Status == "pending" || x.Status == "inprogress") && (x.IsSendNotifications == null || x.IsSendNotifications == false)).ToList();
                if (task.Count() > 0)
                {
                    for (int i = 0; i < task.Count(); i++)
                    {
                        task[i].IsSendNotifications = true;
                        taskRepository.UpdateTask(task[i]);
                        //gửi notifications cho người được phân quyền.
                        //  var q = taskRepository.GetvwTaskById(task[i].Id);
                        ProcessController.Run("Task"
                            , "NotificationsJob"
                            , task[i].Id
                            , task[i].AssignedUserId
                            , null
                            , task[i]);
                    }
                }
            }
        }


        //public class Job_Cham_Cong : IJob
        //{
        //    public void Execute(IJobExecutionContext context)
        //    {
        //        //TimekeepingListRepository timekeepingListRepository = new TimekeepingListRepository(new Domain.Staff.ErpStaffDbContext());
        //        WorkSchedulesRepository workSchedulesRepository = new WorkSchedulesRepository(new Domain.Staff.ErpStaffDbContext());
        //        var date = DateTime.Now.AddDays(-1);
        //        //lấy danh sách phân công làm việc.
        //        IEnumerable<vwWorkSchedules> Listworkschedules = workSchedulesRepository.GetvwAllWorkSchedules()
        //            .Where(x => x.Month == date.Month && x.Year == date.Year && x.Day.Value.Date == date.Date).ToList();
               
        //        foreach (var item in Listworkschedules)
        //        {
        //            ////lấy dòng phân công làm việc đang xét để update dữ liệu lại.
        //            var update_du_lieu = workSchedulesRepository.GetWorkSchedulesById(item.Id);
        //            //hàm tính dữ liệu chấm công.
        //            Erp.BackOffice.Staff.Controllers.TimekeepingController.KiemTraVaTinhDuLieuChamCong(item, update_du_lieu);
        //            //lưu lại
        //            workSchedulesRepository.UpdateWorkSchedules(update_du_lieu);
        //        }
        //    }
        //}

        //public class Job_GetDataFingerPrinter : IJob
        //{
        //    public void Execute(IJobExecutionContext context)
        //    {
        //        FPMachineRepository fPMachineRepository = new FPMachineRepository(new Domain.Staff.ErpStaffDbContext());
        //        var fPMachineList = fPMachineRepository.GetAllFPMachine().ToList();
        //        //resolve from domain to ip
        //        foreach (FPMachine item in fPMachineList)
        //        {
        //            if (item.useurl)
        //                item.Dia_chi_IP = Dns.GetHostAddresses(item.url)[0].ToString();
        //        }

        //    foreach (FPMachine item in fPMachineList)
        //        {
        //            //int autoID = item.AutoID;

        //            //lastDateUpdated = DateTime.Parse(CheckInOutAccess.GetLastDate(autoID.ToString()));

        //            //get Data from specified machine
        //            timeEntities = FingerPrinterHelper.GetAllTimeData(item.Dia_chi_IP, item.Port, item.ID_Ket_noi_IP);

        //            foreach (TimeEntity timeEntity in timeEntities)
        //            {
        //                string currUserFullCode = "";
        //                if (lastDateUpdated < DateTime.Parse(timeEntity.TimeString))
        //                {
                            

        //                    DataRow row = checkInOutToInsert.NewRow();
        //                    if (string.IsNullOrEmpty(currUserFullCode))
        //                        row["UserFullCode"] = "0";
        //                    else
        //                    {
        //                        row["UserFullCode"] = currUserFullCode;
        //                    }

        //                    row["UserEnrollNumber"] = timeEntity.EnrollNum;
        //                    row["TimeType"] = timeEntity.InOutMode == 0 ? "I" : "O";
        //                    row["MachineNo"] = timeEntity.MachineNum;
        //                    row["TimeDate"] = timeEntity.TimeString.Substring(0, 10) + " 00:00:00.000";
        //                    row["TimeStr"] = timeEntity.TimeString;
        //                    row["AutoID"] = autoID;
        //                    checkInOutToInsert.Rows.Add(row);
        //                }
        //            }
        //        //Chỗ này bắt đầu insert zô db
        //        }
        //    }
        //}
    }
}