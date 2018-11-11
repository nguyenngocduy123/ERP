using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class RequestInboundRepository : GenericRepository<ErpSaleDbContext, RequestInbound>, IRequestInboundRepository
    {
        public RequestInboundRepository(ErpSaleDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all RequestInbound
        /// </summary>
        /// <returns>RequestInbound list</returns>
        public IQueryable<RequestInbound> GetAllRequestInbound()
        {
            return Context.RequestInbound.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwRequestInbound> GetAllvwRequestInbound()
        {
            return Context.vwRequestInbound.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Get RequestInbound information by specific id
        /// </summary>
        /// <param name="RequestInboundId">Id of RequestInbound</param>
        /// <returns></returns>
        public RequestInbound GetRequestInboundById(int Id)
        {
            return Context.RequestInbound.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwRequestInbound GetvwRequestInboundById(int Id)
        {
            return Context.vwRequestInbound.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert RequestInbound into database
        /// </summary>
        /// <param name="RequestInbound">Object infomation</param>
        public int InsertRequestInbound(RequestInbound RequestInbound, List<RequestInboundDetail> orderDetails)
        {
            Context.RequestInbound.Add(RequestInbound);
            Context.Entry(RequestInbound).State = EntityState.Added;
            Context.SaveChanges();

            for (int i = 0; i < orderDetails.Count; i++)
            {
                orderDetails[i].RequestInboundId = RequestInbound.Id;
                InsertRequestInboundDetail(orderDetails[i]);
            }

            return RequestInbound.Id;
        }

        /// <summary>
        /// Delete RequestInbound with specific id
        /// </summary>
        /// <param name="Id">RequestInbound Id</param>
        public void DeleteRequestInbound(int Id)
        {
            RequestInbound deletedRequestInbound = GetRequestInboundById(Id);
            Context.RequestInbound.Remove(deletedRequestInbound);
            Context.Entry(deletedRequestInbound).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a RequestInbound with its Id and Update IsDeleted IF that RequestInbound has relationship with others
        /// </summary>
        /// <param name="RequestInboundId">Id of RequestInbound</param>
        public void DeleteRequestInboundRs(int Id)
        {
            RequestInbound deleteRequestInboundRs = GetRequestInboundById(Id);
            deleteRequestInboundRs.IsDeleted = true;
            UpdateRequestInbound(deleteRequestInboundRs);
        }

        /// <summary>
        /// Update RequestInbound into database
        /// </summary>
        /// <param name="RequestInbound">RequestInbound object</param>
        public void UpdateRequestInbound(RequestInbound RequestInbound)
        {
            Context.Entry(RequestInbound).State = EntityState.Modified;
            Context.SaveChanges();
        }

        //order detail

        public IQueryable<vwRequestInboundDetail> GetAllvwRequestInboundDetails()
        {
            return Context.vwRequestInboundDetail.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        public IQueryable<RequestInboundDetail> GetAllRequestInboundDetailsByInvoiceId(int InvoiceId)
        {
            return Context.RequestInboundDetail.Where(item => item.RequestInboundId == InvoiceId && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwRequestInboundDetail> GetAllvwRequestInboundDetailsByInvoiceId(int InvoiceId)
        {
            return Context.vwRequestInboundDetail.Where(item => item.RequestInboundId == InvoiceId && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public RequestInboundDetail GetRequestInboundDetailById(int Id)
        {
            return Context.RequestInboundDetail.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwRequestInboundDetail GetvwRequestInboundDetailById(int Id)
        {
            return Context.vwRequestInboundDetail.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public void InsertRequestInboundDetail(RequestInboundDetail RequestInboundDetail)
        {
            Context.RequestInboundDetail.Add(RequestInboundDetail);
            Context.Entry(RequestInboundDetail).State = EntityState.Added;
            Context.SaveChanges();
        }

        public void DeleteRequestInboundDetail(int Id)
        {
            RequestInboundDetail deletedRequestInboundDetail = GetRequestInboundDetailById(Id);
            Context.RequestInboundDetail.Remove(deletedRequestInboundDetail);
            Context.Entry(deletedRequestInboundDetail).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        public void DeleteRequestInboundDetail(List<RequestInboundDetail> list)
        {
            for (int i = 0; i < list.Count(); i++)
            {
                RequestInboundDetail deletedRequestInboundDetail = GetRequestInboundDetailById(list[i].Id);
                Context.RequestInboundDetail.Remove(deletedRequestInboundDetail);
                Context.Entry(deletedRequestInboundDetail).State = EntityState.Deleted;
            }
            Context.SaveChanges();
        }

        public void DeleteRequestInboundDetailRs(int Id)
        {
            RequestInboundDetail deleteRequestInboundDetailRs = GetRequestInboundDetailById(Id);
            deleteRequestInboundDetailRs.IsDeleted = true;
            UpdateRequestInboundDetail(deleteRequestInboundDetailRs);
        }

        public void UpdateRequestInboundDetail(RequestInboundDetail RequestInboundDetail)
        {
            Context.Entry(RequestInboundDetail).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
