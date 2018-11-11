using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface IRequestInboundRepository
    {
        /// <summary>
        /// Get all RequestInbound
        /// </summary>
        /// <returns>RequestInbound list</returns>
        IQueryable<RequestInbound> GetAllRequestInbound();
        IQueryable<vwRequestInbound> GetAllvwRequestInbound();
        /// <summary>
        /// Get RequestInbound information by specific id
        /// </summary>
        /// <param name="Id">Id of RequestInbound</param>
        /// <returns></returns>
        RequestInbound GetRequestInboundById(int Id);
        vwRequestInbound GetvwRequestInboundById(int Id);
        /// <summary>
        /// Insert RequestInbound into database
        /// </summary>
        /// <param name="RequestInbound">Object infomation</param>
        //void InsertRequestInbound(RequestInbound RequestInbound);

        /// <summary>
        /// Delete RequestInbound with specific id
        /// </summary>
        /// <param name="Id">RequestInbound Id</param>
        void DeleteRequestInbound(int Id);

        /// <summary>
        /// Delete a RequestInbound with its Id and Update IsDeleted IF that RequestInbound has relationship with others
        /// </summary>
        /// <param name="Id">Id of RequestInbound</param>
        void DeleteRequestInboundRs(int Id);

        /// <summary>
        /// Update RequestInbound into database
        /// </summary>
        /// <param name="RequestInbound">RequestInbound object</param>
        void UpdateRequestInbound(RequestInbound RequestInbound);


        int InsertRequestInbound(RequestInbound RequestInbound, List<RequestInboundDetail> orderDetails);
        // Order detail
        IQueryable<vwRequestInboundDetail> GetAllvwRequestInboundDetails();
        IQueryable<RequestInboundDetail> GetAllRequestInboundDetailsByInvoiceId(int InvoiceId);
        IQueryable<vwRequestInboundDetail> GetAllvwRequestInboundDetailsByInvoiceId(int InvoiceId);
        RequestInboundDetail GetRequestInboundDetailById(int Id);
        vwRequestInboundDetail GetvwRequestInboundDetailById(int Id);

        void InsertRequestInboundDetail(RequestInboundDetail RequestInboundDetail);

        void DeleteRequestInboundDetail(int Id);

        void DeleteRequestInboundDetail(List<RequestInboundDetail> list);

        void DeleteRequestInboundDetailRs(int Id);

        void UpdateRequestInboundDetail(RequestInboundDetail RequestInboundDetail);
    }
}
