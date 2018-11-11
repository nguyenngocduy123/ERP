using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface IStaffsRepository
    {
        /// <summary>
        /// Get all Staffs
        /// </summary>
        /// <returns>Staffs list</returns>
        IQueryable<Staffs> GetAllStaffs();
        IQueryable<vwStaffs> GetvwAllStaffs();
        /// <summary>
        /// Get Staffs information by specific id
        /// </summary>
        /// <param name="Id">Id of Staffs</param>
        /// <returns></returns>
        Staffs GetStaffsById(int Id);
        vwStaffs GetvwStaffsByUser(int UserId);
        //vwStaffs GetvwStaffsByUserEnrollNumber(int UserEnrollNumber);
       // vwStaffs GetvwStaffsByListDrugStore(string DrugStore);
        vwStaffs GetvwStaffsById(int Id);
        IQueryable<vwStaffs> GetvwStaffsByListDrugStore(string DrugStore);
        //IQueryable<vwStaffs> GetvwStaffsByBranchIdAndBranchDepartmentId(int BranchId, int BranchDepartmentId);
        //vwStaffs GetvwStaffsByBranchId(int? BranchId, string PositionId);
        /// <summary>
        /// Insert Staffs into database
        /// </summary>
        /// <param name="Staffs">Object infomation</param>
        void InsertStaffs(Staffs Staffs);

        /// <summary>
        /// Delete Staffs with specific id
        /// </summary>
        /// <param name="Id">Staffs Id</param>
        void DeleteStaffs(int Id);

        /// <summary>
        /// Delete a Staffs with its Id and Update IsDeleted IF that Staffs has relationship with others
        /// </summary>
        /// <param name="Id">Id of Staffs</param>
        void DeleteStaffsRs(int Id);

        /// <summary>
        /// Update Staffs into database
        /// </summary>
        /// <param name="Staffs">Staffs object</param>
        void UpdateStaffs(Staffs Staffs);
    }
}
