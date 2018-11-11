using Erp.BackOffice.Models;
using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Erp.BackOffice.Controllers
{
    public class CheckInOutServiceController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetFPMachineList()
        {
            FPMachineRepository fPMachineRepository = new FPMachineRepository(new Domain.Staff.ErpStaffDbContext());
            var q = fPMachineRepository.GetAllFPMachine();
            var res = Request.CreateResponse(HttpStatusCode.OK, q.ToList());
            return res;
        }

        [HttpGet]
        public HttpResponseMessage GetFPMachine(string name)
        {
            FPMachineRepository fPMachineRepository = new FPMachineRepository(new Domain.Staff.ErpStaffDbContext());
            var fPMachine = fPMachineRepository.GetAllFPMachine()
                .Where(item => item.Ten_may_tinh == name).FirstOrDefault();

            if (fPMachine == null)
            {
                fPMachine = new FPMachine();
                fPMachine.IsDeleted = false;
                fPMachine.CreatedDate = DateTime.Now;
                fPMachine.ModifiedDate = DateTime.Now;
                fPMachine.UpdatedDate = DateTime.Now;
                fPMachine.Ten_may_tinh = name;
                fPMachine.Dia_chi_IP = "192.168.1.201";
                fPMachine.Toc_do_truyen = "115200";
                fPMachine.Port = 4370;
                fPMachine.GetDataSchedule = "19:00";
                fPMachine.useurl = false;

                fPMachineRepository.InsertFPMachine(fPMachine);
            }
            else
            {
                fPMachine.UpdatedDate = DateTime.Now;
                fPMachineRepository.UpdateFPMachine(fPMachine);
            }

            var res = Request.CreateResponse(HttpStatusCode.OK, fPMachine);
            return res;
        }

        [HttpGet]
        public HttpResponseMessage GetFingerPrintList(int FPMachineId)
        {
           
            CheckInOutRepository checkInOutRepository = new CheckInOutRepository(new Domain.Staff.ErpStaffDbContext());
            var list = checkInOutRepository.GetAllvwFingerPrint().Where(item => FPMachineId > 0);

            var res = Request.CreateResponse(HttpStatusCode.OK, list.ToList());
            return res;
        }

        [HttpPost]
        public void Insert([FromBody]CheckInOut checkInOut)
        {
            CheckInOutRepository checkInOutRepository = new CheckInOutRepository(new Domain.Staff.ErpStaffDbContext());
            checkInOutRepository.InsertCheckInOut(checkInOut);
        }

        [HttpPost]
        public void InsertList([FromBody]CheckInOutInsertListModel model)
        {
            if (model.ListCheckInOut != null && model.ListCheckInOut.Count > 0)
            {
                CheckInOutRepository checkInOutRepository = new CheckInOutRepository(new Domain.Staff.ErpStaffDbContext());
                foreach (var item in model.ListCheckInOut)
                {
                    item.CreatedDate = DateTime.Now;
                    checkInOutRepository.InsertCheckInOut(item);
                }

                //Xóa trùng lặp
                string sqlQuery = " WITH CTE AS " +
                                     " ( SELECT 	FPMachineId, TimeStr, UserId, ROW_NUMBER() OVER ( PARTITION BY FPMachineId, TimeStr, UserId ORDER BY  TimeStr DESC, UserId, FPMachineId) AS RowID " +
                                     " FROM [dbo].[Staff_CheckInOut] ) " +
                                     " DELETE FROM CTE " +
                                     " WHERE RowID > 1; ";

                Domain.Helper.SqlHelper.ExecuteSQL(sqlQuery);
            }
        }

        [HttpPost]
        public void FingerPrintInsertList([FromBody]FingerPrintInsertListModel model)
        {
            if (model.ListFingerPrint != null && model.ListFingerPrint.Count > 0)
            {
                ////Xóa dữ liệu cũ
                //var listUserId = string.Join(",", list.Select(n => n.UserId.ToString()).Distinct().ToArray());
                //string sqlQuery = "DELETE FROM Staff_FingerPrint WHERE UserId in (" + listUserId + ");";

                //Domain.Helper.SqlHelper.ExecuteSQL(sqlQuery);

                CheckInOutRepository checkInOutRepository = new CheckInOutRepository(new Domain.Staff.ErpStaffDbContext());
                foreach (var item in model.ListFingerPrint)
                {
                    var fingerPrint = checkInOutRepository.GetAllFingerPrint()
                        .Where(i => i.UserId == item.UserId 
                            && i.Name == item.Name 
                            && i.FingerIndex == item.FingerIndex)
                            .FirstOrDefault();

                    if (fingerPrint == null)
                    {
                        fingerPrint = new FingerPrint();
                        fingerPrint.CreatedDate = DateTime.Now;
                        fingerPrint.ModifiedDate = DateTime.Now;
                        fingerPrint.FPMachineId = item.FPMachineId;
                        fingerPrint.UserId = item.UserId;
                        fingerPrint.Name = item.Name;
                        fingerPrint.FingerIndex = item.FingerIndex;
                        fingerPrint.TmpData = item.TmpData;
                        fingerPrint.Privilege = item.Privilege;
                        fingerPrint.Password = item.Password;
                        fingerPrint.Enabled = item.Enabled;
                        fingerPrint.Flag = item.Flag;
                        fingerPrint.FPMachineId = model.FPMachineId;
                        checkInOutRepository.InsertFingerPrint(fingerPrint);
                    }
                    else
                    {
                        if (fingerPrint.FPMachineId == model.FPMachineId && string.IsNullOrEmpty(fingerPrint.TmpData))
                        {
                            fingerPrint.ModifiedDate = DateTime.Now;
                            fingerPrint.TmpData = item.TmpData;
                            checkInOutRepository.UpdateFingerPrint(fingerPrint);
                        }
                    }
                }
            }
        }
    }
}