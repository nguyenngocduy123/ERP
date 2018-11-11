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
using Erp.BackOffice.Helpers;

namespace Erp.BackOffice.Staff.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class SalarySettingDetail_StaffController : Controller
    {
        private readonly ISalarySettingDetail_StaffRepository SalarySettingDetail_StaffRepository;
        private readonly IUserRepository userRepository;
        private readonly ISalarySettingDetailRepository SalarySettingDetailRepository;
        private readonly ISalarySettingRepository salarySettingRepository;
        private readonly IStaffsRepository staffRepository;
        public SalarySettingDetail_StaffController(
            ISalarySettingDetail_StaffRepository _SalarySettingDetail_Staff
            , IUserRepository _user
            , ISalarySettingDetailRepository salarySettingDetail
            , ISalarySettingRepository salarySetting
            , IStaffsRepository staffs
            )
        {
            SalarySettingDetail_StaffRepository = _SalarySettingDetail_Staff;
            userRepository = _user;
            SalarySettingDetailRepository = salarySettingDetail;
            salarySettingRepository = salarySetting;
            staffRepository = staffs;
        }

        #region Index
     
        [HttpPost]
        public ActionResult Index(SalarySettingDetail_StaffViewModel model)
        {
            var urlRefer = Request["UrlReferrer"];
            var staffId = Request["StaffId"];
            if (model.ListAllColumns.Count() > 0)
            {
                for (int i = 0; i < model.ListAllColumns.Count(); i++)
                {
                    var w = SalarySettingDetail_StaffRepository.GetSalarySettingDetail_StaffBySetting(model.ListAllColumns[i].SalarySettingId.Value, Convert.ToInt32(staffId), model.ListAllColumns[i].Id);
                    if (w == null)
                    {
                        var q = new SalarySettingDetail_Staff();
                        q.IsDeleted = false;
                        q.CreatedUserId = WebSecurity.CurrentUserId;
                        q.ModifiedUserId = WebSecurity.CurrentUserId;
                        q.AssignedUserId = WebSecurity.CurrentUserId;
                        q.CreatedDate = DateTime.Now;
                        q.ModifiedDate = DateTime.Now;

                        q.StaffId = Convert.ToInt32(staffId);
                        q.SalarySettingId = model.ListAllColumns[i].SalarySettingId;
                        q.SalarySettingDetailId = model.ListAllColumns[i].Id;
                        //q.DefaultValue = model.ListAllColumns[i].DefaultValue;
                        q.DefaultValue = double.Parse(Request[string.Format("ListAllColumns[{0}].DefaultValue", i)].Replace('.', ','));
                        SalarySettingDetail_StaffRepository.InsertSalarySettingDetail_Staff(q);
                    }
                    else
                    {
                        w.DefaultValue = double.Parse(Request[string.Format("ListAllColumns[{0}].DefaultValue", i)].Replace('.', ','));
                        SalarySettingDetail_StaffRepository.UpdateSalarySettingDetail_Staff(w);
                    }
                }
                var listSalarySettingDetailStaff = SalarySettingDetail_StaffRepository.GetAllSalarySettingDetail_Staff().AsEnumerable().Where(i => i.StaffId.Value == Convert.ToInt32(staffId))
                    .Select(i => new SalarySettingDetail_StaffViewModel
                    {
                        Id = i.Id,
                        SalarySettingDetailId = i.SalarySettingDetailId
                    }).ToList();
                var listxoa = listSalarySettingDetailStaff.Where(x => model.ListAllColumns.Any(i => i.Id == x.SalarySettingDetailId.Value) == false).ToList();
                if (listxoa.Count() > 0)
                {
                    foreach (var item in listxoa)
                    {
                        SalarySettingDetail_StaffRepository.DeleteSalarySettingDetail_Staff(item.Id);
                    }
                }
            }
            return Redirect(urlRefer);
        }
        #endregion
    }
}
