using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface IProductOutboundRepository
    {
        /// <summary>
        /// Get all ProductOutbound
        /// </summary>
        /// <returns>ProductOutbound list</returns>
        IQueryable<ProductOutbound> GetAllProductOutbound();
        IQueryable<vwProductOutbound> GetAllvwProductOutbound();
        IQueryable<ProductOutbound> GetAllProductOutboundFull();
        IQueryable<vwProductOutbound> GetAllvwProductOutboundFull();
        /// <summary>
        /// Get ProductOutbound information by specific id
        /// </summary>
        /// <param name="Id">Id of ProductOutbound</param>
        /// <returns></returns>
        ProductOutbound GetProductOutboundById(int Id);
        vwProductOutbound GetvwProductOutboundById(int Id);
        ProductOutbound GetProductOutboundBySaleOrderId(int InvoiceId);
        vwProductOutbound GetvwProductOutboundFullById(int Id);
        /// <summary>
        /// Insert ProductOutbound into database
        /// </summary>
        /// <param name="ProductOutbound">Object infomation</param>
        void InsertProductOutbound(ProductOutbound ProductOutbound);

        /// <summary>
        /// Delete ProductOutbound with specific id
        /// </summary>
        /// <param name="Id">ProductOutbound Id</param>
        void DeleteProductOutbound(int Id);

        /// <summary>
        /// Delete a ProductOutbound with its Id and Update IsDeleted IF that ProductOutbound has relationship with others
        /// </summary>
        /// <param name="Id">Id of ProductOutbound</param>
        void DeleteProductOutboundRs(int Id);

        /// <summary>
        /// Update ProductOutbound into database
        /// </summary>
        /// <param name="ProductOutbound">ProductOutbound object</param>
        void UpdateProductOutbound(ProductOutbound ProductOutbound);

        // ---------------------------------------------------------------------
        // outbound detail

        IQueryable<ProductOutboundDetail> GetAllProductOutboundDetailByOutboundId(int OutboundId);
        IQueryable<vwProductOutboundDetail> GetAllvwProductOutboundDetailByOutboundId(int OutboundId);

        IQueryable<vwProductOutboundDetail> GetAllvwProductOutboundDetailByProductId(int ProductId);
        IQueryable<vwProductOutboundDetail> GetAllvwProductOutboundDetail();

        ProductOutboundDetail GetProductOutboundDetailById(int Id);

        void InsertProductOutboundDetail(ProductOutboundDetail ProductOutboundDetail);

        void DeleteProductOutboundDetail(int Id);

        void DeleteProductOutboundDetailRs(int Id);

        void UpdateProductOutboundDetail(ProductOutboundDetail ProductOutboundDetail);
    }
}
