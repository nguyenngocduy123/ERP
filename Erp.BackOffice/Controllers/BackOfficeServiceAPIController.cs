using Erp.Domain;
using Erp.Domain.Entities;
using Erp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Erp.Domain.Staff;
using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Repositories;
using Erp.Domain.Account.Entities;
using Erp.Domain.Account.Repositories;
using Erp.BackOffice.Helpers;
using Erp.Domain.Account;
using Erp.Domain.Helper;
using Erp.BackOffice.Sale.Models;
namespace Erp.BackOffice.Controllers
{
    public class BackOfficeServiceAPIController : ApiController
    {
        #region Save Image Base64
        [HttpPost]
        public static string WriteFileFromBase64String(List<string> val)
        {
            string base64String = val[0];
            string filePath = val[1];
            string fileName = val[2];

            string dataType = fileName.Split('.').ElementAt(1);
            string nameFile = Guid.NewGuid().ToString() + "." + dataType;
            string absoluteFileName = HttpContext.Current.Server.MapPath(filePath) + nameFile;

            string dataBinaryContent = base64String;
            byte[] buffer = Convert.FromBase64String(dataBinaryContent);

            if (WriteFile(buffer, absoluteFileName))
            {
                string relativeFileName = filePath + nameFile;
                return relativeFileName;
            }
            return string.Empty;
        }

        public static bool WriteFile(byte[] buffer, string fullname)
        {
            try
            {
                var fileStream = new FileStream(fullname, FileMode.Append);
                fileStream.Write(buffer, 0, buffer.Length);
                fileStream.Flush();
                fileStream.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region Save Image PostFileBase
        [HttpPost]
        public static string SaveFile(List<object> value)
        {
            var objFile = (HttpPostedFileBase)value[0];
            string filePath = (string)value[1];
            string strFileName = string.Empty;
            string strTargetFolder;
            if (objFile != null)
            {
                try
                {
                    strTargetFolder = HttpContext.Current.Server.MapPath(filePath);
                    strFileName = Path.GetFileName(objFile.FileName);
                    string fileName = StandardizeFileName(strTargetFolder, strFileName);
                    objFile.SaveAs(strTargetFolder + fileName);
                    return filePath + fileName;
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
            return string.Empty;
        }

        public static string StandardizeFileName(string path, string filename)
        {
            string name = Path.GetFileNameWithoutExtension(filename);
            string ext = Path.GetExtension(filename);
            string result = name;
            int count = 1;
            while (File.Exists(path + filename.Replace(name, result)) == true)
            {
                result = name + "_" + count.ToString(CultureInfo.InvariantCulture);
                count++;
            }
            result = result + ext;
            return result;
        }
        #endregion

        [HttpGet]
        public IEnumerable<Location> FetchLocation(string parentId)
        {
            LocationRepository locationRepository = new LocationRepository(new ErpDbContext());
            List<Location> locationList = locationRepository.GetList(parentId).ToList();
            //locationList.Insert(0, new Location() { Id = "", Name = "- Rỗng -" });

            return locationList;
        }

        public IHttpActionResult GetListLocation(string parentId)
        {
            LocationRepository locationRepository = new LocationRepository(new ErpDbContext());
            List<Location> locationList = locationRepository.GetList(parentId).ToList();
            return Ok(locationList);
        }

        public IHttpActionResult GetListLocation2(string parentId)
        {
            LocationRepository locationRepository = new LocationRepository(new ErpDbContext());
            List<Location> locationList = locationRepository.GetList(parentId).ToList();
            return Ok(locationList);
        }

        [HttpGet]
        public IEnumerable<vwBranchDepartment> FetchBranchDepartment(int? BranchId)
        {
            BranchDepartmentRepository departmentRepository = new BranchDepartmentRepository(new ErpStaffDbContext());
            List<vwBranchDepartment> departmentList = departmentRepository.GetAllvwBranchDepartment().Where(x => x.Sale_BranchId == BranchId).ToList();
            departmentList.Insert(0, new vwBranchDepartment() { Id = -1, Staff_DepartmentId = "- Rỗng -" });

            return departmentList;
        }
        [HttpGet]
        public IEnumerable<System.Web.Mvc.SelectListItem> FetchCategoryBy(string value, string getByType)
        {
            return Erp.BackOffice.Helpers.Common.GetSelectList_Category(value, null, getByType);
        }

        [HttpGet]
        public IEnumerable<LabourContract> FetchLabourContract()
        {
            LabourContractRepository labourcontractRepository = new LabourContractRepository(new ErpStaffDbContext());
            List<LabourContract> labourContractList = labourcontractRepository.GetAllLabourContract().AsEnumerable()
                .Select(item => new LabourContract { Id = item.Id, Name = item.Code + " - " + item.Name }).ToList();
            labourContractList.Insert(0, new LabourContract() { Id = -1, Name = "- Rỗng -" });
            return labourContractList;
        }
        [HttpGet]
        public IEnumerable<Contract> FetchContract()
        {
            ContractRepository contractRepository = new ContractRepository(new ErpAccountDbContext());
            List<Contract> ContractList = contractRepository.GetAllContract().AsEnumerable()
                .Select(item => new Contract { Id = item.Id, Code = item.Code }).ToList();
            ContractList.Insert(0, new Contract() { Id = -1, Code = "- Rỗng -" });
            return ContractList;
        }

        [HttpGet]
        public IEnumerable<InternalNotifications> FetchInternalNotifications()
        {
            InternalNotificationsRepository internalNotificationsRepository = new InternalNotificationsRepository(new Domain.Staff.ErpStaffDbContext());
            List<InternalNotifications> NotificationsList = internalNotificationsRepository.GetAllInternalNotifications().AsEnumerable()
                .Select(item => new InternalNotifications { Id = item.Id, Titles = item.Titles }).ToList();
            NotificationsList.Insert(0, new InternalNotifications() { Id = -1, Titles = "- Rỗng -" });
            return NotificationsList;
        }

        [HttpGet]
        public string FetchStaff(int? StaffId)
        {
            StaffsRepository staffRepository = new StaffsRepository(new ErpStaffDbContext());
            var profileimage = staffRepository.GetvwStaffsById(StaffId.Value);
            return Erp.BackOffice.Helpers.Common.KiemTraTonTaiHinhAnh(profileimage.ProfileImage, "Staffs", "user");
        }
         [HttpGet]
        public string GetCustomerbyPhone(string Phone)
        {
            CustomerRepository customerRepository = new CustomerRepository(new ErpAccountDbContext());
            var cus = customerRepository.GetCustomerByPhone(Phone);
            if (cus == null)
                return null;
            return cus.CompanyName;
        }
        
        [HttpGet]
        public vwStaffs FetchListStaff(int? StaffId)
        {
            StaffsRepository staffRepository = new StaffsRepository(new ErpStaffDbContext());
            //CategoryRepository categoryRepository = new CategoryRepository(new ErpDbContext());
            vwStaffs staff = staffRepository.GetvwStaffsById(StaffId.Value);
            staff.ProfileImage = Erp.BackOffice.Helpers.Common.KiemTraTonTaiHinhAnh(staff.ProfileImage, "Staff", "user");
            return staff;
        }

        [HttpGet]
        public IEnumerable<vwBranch> FetchDrugStore(string CityId, string DistrictId)
        {
            BranchRepository branchRepository = new BranchRepository(new ErpStaffDbContext());
            UserRepository userRepository = new UserRepository(new ErpDbContext());
            List<vwBranch> locationList = branchRepository.GetAllvwBranch().ToList();
            var user = userRepository.GetByvwUserName(Helpers.Common.CurrentUser.UserName);
            if (!string.IsNullOrEmpty(CityId))
                locationList = locationList.Where(x => x.CityId == CityId).ToList();
            if (!string.IsNullOrEmpty(DistrictId))
                locationList = locationList.Where(x => x.DistrictId == DistrictId).ToList();

            if (!Erp.BackOffice.Filters.SecurityFilter.IsAdmin() && !Erp.BackOffice.Filters.SecurityFilter.IsKeToan())
            {
                locationList = locationList.Where(x => ("," + user.DrugStore + ",").Contains("," + x.Id + ",") == true).ToList();
            }
            return locationList;
        }

        [HttpGet]
        public IEnumerable<int> FetchListStaffLabourContractType(string LabourContractName, bool? IsSeniority, string BranchId)
        {
            //tìm ds các nhân viên thuộc khu vực và phòng ban
            StaffsRepository staffRepository = new StaffsRepository(new ErpStaffDbContext());
            var listStaffSeniority = new List<int?>();




            var staff = new List<int>();
            IEnumerable<vwStaffs> listStaffs;
            if (string.IsNullOrEmpty(LabourContractName))
                listStaffs = staffRepository.GetvwAllStaffs().ToList();
            else
            {
                listStaffs = staffRepository.GetvwAllStaffs().ToList();
            }

            if (BranchId != null)
            {
                listStaffs = listStaffs.Where(n => n.DrugStore == BranchId).ToList();
            }
            //listStaffs = staffRepository.GetvwAllStaffs().Where(u => u.LabourContractName == LabourContractName).ToList();

            //if (IsSeniority != null && IsSeniority.Value == true)
            //{
            //    DateTime dateNow = DateTime.Now;
            //    int monthsetup = 5;
            //    if (dateNow.Month <= monthsetup)
            //        listStaffSeniority = salarySeniority.GetAllSalarySeniority()
            //            .Where(n => n.NgayTinhHuong.Value.Month <= monthsetup && n.NgayTinhHuong.Value.Year == dateNow.Year).Select(n => n.StaffId).Distinct().ToList();
            //    else
            //        listStaffSeniority = salarySeniority.GetAllSalarySeniority()
            //            .Where(n => n.NgayTinhHuong.Value.Month > monthsetup && n.NgayTinhHuong.Value.Year == dateNow.Year).Select(n => n.StaffId).Distinct().ToList();

            //    //setup list staff
            //    listStaffs = listStaffs.Where(n => listStaffSeniority.Contains(n.Id));
            //}

            foreach (var item in listStaffs)
            {
                staff.Add(item.Id);
            }
            //staff.Position = Erp.BackOffice.Helpers.Common.GetCategoryByValueCodeOrId("value", staff.Position, "position").Name.ToString();

            return staff;
        }

        public object GetListSale_BaoCaoXuatKho(string CategoryCode, string ProductGroup, string Manufacturer, int? WarehouseId, string StartDate, string EndDate)
        {
            CategoryCode = CategoryCode == null ? "" : CategoryCode;
            Manufacturer = Manufacturer == null ? "" : Manufacturer;
            ProductGroup = ProductGroup == null ? "" : ProductGroup;
            WarehouseId = WarehouseId == null ? 0 : WarehouseId;
            StartDate = StartDate == null ? "" : StartDate;
            EndDate = EndDate == null ? "" : EndDate;
            DateTime aDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime retDateTime = aDateTime.AddMonths(1).AddDays(-1);
            if (StartDate == null && EndDate == null && WarehouseId != null)
            {
                StartDate = aDateTime.ToString("dd/MM/yyyy");
                EndDate = retDateTime.ToString("dd/MM/yyyy");
            }
            var d_startDate = (!string.IsNullOrEmpty(StartDate) ? DateTime.ParseExact(StartDate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd HH:mm:ss") : "");
            var d_endDate = (!string.IsNullOrEmpty(EndDate) ? DateTime.ParseExact(EndDate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd HH:mm:ss") : "");

            var data = SqlHelper.QuerySP<Erp.BackOffice.Sale.Models.Sale_BaoCaoXuatKhoViewModel>("spSale_BaoCaoXuatKho", new
            {
                StartDate = d_startDate,
                EndDate = d_endDate,
                WarehouseId = WarehouseId,
                CategoryCode = CategoryCode,
                ProductGroup = ProductGroup,
                Manufacturer = Manufacturer
            }).ToList();
            return data;
        }


        public object GetListSale_BaoCaoNhapXuatTon(string StartDate, string EndDate, int? WarehouseId, string CategoryCode, string ProductGroup, string Manufacturer)
        {
            StartDate = StartDate == null ? "" : StartDate;
            EndDate = EndDate == null ? "" : EndDate;
            WarehouseId = WarehouseId == null ? 0 : WarehouseId;
            CategoryCode = CategoryCode == null ? "" : CategoryCode;
            ProductGroup = ProductGroup == null ? "" : ProductGroup;
            Manufacturer = Manufacturer == null ? "" : Manufacturer;

            DateTime aDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime retDateTime = aDateTime.AddMonths(1).AddDays(-1);
            if (string.IsNullOrEmpty(StartDate) && string.IsNullOrEmpty(EndDate))
            {
                StartDate = aDateTime.ToString("dd/MM/yyyy");
                EndDate = retDateTime.ToString("dd/MM/yyyy");
            }
            var d_startDate = (!string.IsNullOrEmpty(StartDate) ? DateTime.ParseExact(StartDate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd HH:mm:ss") : "");
            var d_endDate = (!string.IsNullOrEmpty(EndDate) ? DateTime.ParseExact(EndDate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd HH:mm:ss") : "");

            var data = SqlHelper.QuerySP<Sale_BaoCaoNhapXuatTonViewModel>("spSale_BaoCaoNhapXuatTon", new
            {
                StartDate = d_startDate,
                EndDate = d_endDate,
                WarehouseId = WarehouseId,
                CategoryCode = CategoryCode,
                ProductGroup = ProductGroup,
                Manufacturer = Manufacturer
            }).ToList();
            return data;
        }


        public object GetListSale_BaoCaoTonKho(int? BranchId, int? WarehouseId, string ProductGroup, string CategoryCode, string Manufacturer)
        {
            BranchId = BranchId == null ? 0 : BranchId;
            WarehouseId = WarehouseId == null ? 0 : WarehouseId;
            ProductGroup = ProductGroup == null ? "" : ProductGroup;
            CategoryCode = CategoryCode == null ? "" : CategoryCode;
            Manufacturer = Manufacturer == null ? "" : Manufacturer;


            var data = SqlHelper.QuerySP<Sale_BaoCaoTonKhoViewModel>("spSale_BaoCaoTonKho", new
            {
                BranchId = BranchId,
                WarehouseId = WarehouseId,
                ProductGroup = ProductGroup,
                CategoryCode = CategoryCode,
                Manufacturer = Manufacturer
            }).ToList();
            return data;
        }


        public object GetListSale_BaoCaoCongNoTongHop(string StartDate, string EndDate, string BranchId, int? SalerId)
        {
            StartDate = StartDate == null ? "" : StartDate;
            EndDate = EndDate == null ? "" : EndDate;
            BranchId = BranchId == null ? "" : BranchId;
            SalerId = SalerId == null ? 0 : SalerId;

            DateTime aDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime retDateTime = aDateTime.AddMonths(1).AddDays(-1);
            if (StartDate == null && EndDate == null && string.IsNullOrEmpty(BranchId))
            {
                StartDate = aDateTime.ToString("dd/MM/yyyy");
                EndDate = retDateTime.ToString("dd/MM/yyyy");
            }
            var d_startDate = (!string.IsNullOrEmpty(StartDate) ? DateTime.ParseExact(StartDate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd HH:mm:ss") : "");
            var d_endDate = (!string.IsNullOrEmpty(EndDate) ? DateTime.ParseExact(EndDate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd HH:mm:ss") : "");

            var data = SqlHelper.QuerySP<Sale_BaoCaoCongNoTongHopViewModel>("spSale_BaoCaoCongNoTongHop", new
            {
                StartDate = d_startDate,
                EndDate = d_endDate,
                BranchId = BranchId,
                SalerId = SalerId
            }).ToList();
            return data;
        }


        //<append_content_action_here>
    }
}