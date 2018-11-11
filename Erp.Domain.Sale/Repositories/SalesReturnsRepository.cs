using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class SalesReturnsRepository : GenericRepository<ErpSaleDbContext, SalesReturns>, ISalesReturnsRepository
    {
        public SalesReturnsRepository(ErpSaleDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all SalesReturns
        /// </summary>
        /// <returns>SalesReturns list</returns>
        public IQueryable<SalesReturns> GetAllSalesReturns()
        {
            return Context.SalesReturns.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwSalesReturns> GetAllvwSalesReturns()
        {
            return Context.vwSalesReturns.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Get SalesReturns information by specific id
        /// </summary>
        /// <param name="SalesReturnsId">Id of SalesReturns</param>
        /// <returns></returns>
        public SalesReturns GetSalesReturnsById(int Id)
        {
            return Context.SalesReturns.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwSalesReturns GetvwSalesReturnsById(int Id)
        {
            return Context.vwSalesReturns.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwSalesReturns GetvwSalesReturnsByTransactionCode(string code)
        {
            return Context.vwSalesReturns.SingleOrDefault(item => item.Code == code && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert SalesReturns into database
        /// </summary>
        /// <param name="SalesReturns">Object infomation</param>
        public int InsertSalesReturns(SalesReturns SalesReturns, List<SalesReturnsDetail> orderDetails)
        {
            Context.SalesReturns.Add(SalesReturns);
            Context.Entry(SalesReturns).State = EntityState.Added;
            Context.SaveChanges();
            for (int i = 0; i < orderDetails.Count; i++)
            {
                orderDetails[i].SalesReturnsId = SalesReturns.Id;
                InsertSalesReturnsDetail(orderDetails[i]);
            }

            return SalesReturns.Id;
        }
        public void InsertSalesReturn(SalesReturns SalesReturns)
        {
            Context.SalesReturns.Add(SalesReturns);
            Context.Entry(SalesReturns).State = EntityState.Added;
            Context.SaveChanges();
        }
        /// <summary>
        /// Delete SalesReturns with specific id
        /// </summary>
        /// <param name="Id">SalesReturns Id</param>
        public void DeleteSalesReturns(int Id)
        {
            SalesReturns deletedSalesReturns = GetSalesReturnsById(Id);
            Context.SalesReturns.Remove(deletedSalesReturns);
            Context.Entry(deletedSalesReturns).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a SalesReturns with its Id and Update IsDeleted IF that SalesReturns has relationship with others
        /// </summary>
        /// <param name="SalesReturnsId">Id of SalesReturns</param>
        public void DeleteSalesReturnsRs(int Id)
        {
            SalesReturns deleteSalesReturnsRs = GetSalesReturnsById(Id);
            deleteSalesReturnsRs.IsDeleted = true;
            UpdateSalesReturns(deleteSalesReturnsRs);
        }

        /// <summary>
        /// Update SalesReturns into database
        /// </summary>
        /// <param name="SalesReturns">SalesReturns object</param>
        public void UpdateSalesReturns(SalesReturns SalesReturns)
        {
            Context.Entry(SalesReturns).State = EntityState.Modified;
            Context.SaveChanges();
        }
        //order detail

        public IQueryable<vwSalesReturnsDetail> GetAllReturnsDetailsByReturnId(int ReturnsId)
        {
            return Context.vwSalesReturnsDetail.Where(item => item.SalesReturnsId == ReturnsId);
        }
        public IQueryable<vwSalesReturnsDetail> GetAllvwReturnsDetails()
        {
            return Context.vwSalesReturnsDetail.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public SalesReturnsDetail GetSalesReturnsDetailById(int Id)
        {
            return Context.SalesReturnsDetail.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public void InsertSalesReturnsDetail(SalesReturnsDetail SalesReturnsDetail)
        {
            Context.SalesReturnsDetail.Add(SalesReturnsDetail);
            Context.Entry(SalesReturnsDetail).State = EntityState.Added;
            Context.SaveChanges();
        }

        public void DeleteSalesReturnsDetail(int Id)
        {
            SalesReturnsDetail deletedSalesReturnsDetail = GetSalesReturnsDetailById(Id);
            Context.SalesReturnsDetail.Remove(deletedSalesReturnsDetail);
            Context.Entry(deletedSalesReturnsDetail).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        public void DeleteSalesReturnsDetail(IEnumerable<SalesReturnsDetail> list)
        {
            for (int i = 0; i < list.Count(); i++)
            {
                SalesReturnsDetail deletedSalesReturnsDetail = GetSalesReturnsDetailById(list.ElementAt(i).Id);
                Context.SalesReturnsDetail.Remove(deletedSalesReturnsDetail);
                Context.Entry(deletedSalesReturnsDetail).State = EntityState.Deleted;
            }
            Context.SaveChanges();
        }

        public void DeleteSalesReturnsDetailRs(int Id)
        {
            SalesReturnsDetail deleteSalesReturnsDetailRs = GetSalesReturnsDetailById(Id);
            deleteSalesReturnsDetailRs.IsDeleted = true;
            UpdateSalesReturnsDetail(deleteSalesReturnsDetailRs);
        }

        public void UpdateSalesReturnsDetail(SalesReturnsDetail SalesReturnsDetail)
        {
            Context.Entry(SalesReturnsDetail).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
