using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface IProductInboundRepository
    {
        /// <summary>
        /// Get all ProductInbound
        /// </summary>
        /// <returns>ProductInbound list</returns>
        IQueryable<ProductInbound> GetAllProductInbound();
        IQueryable<vwProductInbound> GetAllvwProductInbound();
        IQueryable<vwProductInbound> GetAllvwProductInboundFull();
        /// <summary>
        /// Get ProductInbound information by specific id
        /// </summary>
        /// <param name="Id">Id of ProductInbound</param>
        /// <returns></returns>
        ProductInbound GetProductInboundById(int Id);
        vwProductInbound GetvwProductInboundById(int Id);
        vwProductInbound GetvwProductInboundByTransactionCode(string Code);
        vwProductInbound GetvwProductInboundFullById(int Id);
        /// <summary>
        /// Insert ProductInbound into database
        /// </summary>
        /// <param name="ProductInbound">Object infomation</param>
        void InsertProductInbound(ProductInbound ProductInbound);

        /// <summary>
        /// Delete ProductInbound with specific id
        /// </summary>
        /// <param name="Id">ProductInbound Id</param>
        void DeleteProductInbound(int Id);

        /// <summary>
        /// Delete a ProductInbound with its Id and Update IsDeleted IF that ProductInbound has relationship with others
        /// </summary>
        /// <param name="Id">Id of ProductInbound</param>
        void DeleteProductInboundRs(int Id);

        /// <summary>
        /// Update ProductInbound into database
        /// </summary>
        /// <param name="ProductInbound">ProductInbound object</param>
        void UpdateProductInbound(ProductInbound ProductInbound);


        //----------------------------------------------------------------------------------------
        // inbound detail

        IQueryable<ProductInboundDetail> GetAllProductInboundDetailByInboundId(int inboundId);

        IQueryable<vwProductInboundDetail> GetAllvwProductInboundDetailByInboundId(int inboundId);

        IQueryable<vwProductInboundDetail> GetAllvwProductInboundDetailByProductId(int ProductId);
        IQueryable<vwProductInboundDetail> GetAllvwProductInboundDetail();

        ProductInboundDetail GetProductInboundDetailById(int Id);

        void InsertProductInboundDetail(ProductInboundDetail ProductInBoundDetail);

        void DeleteProductInboundDetail(int Id);

        void DeleteProductInboundDetailRs(int Id);

        void UpdateProductInboundDetail(ProductInboundDetail ProductInBoundDetail);
    }
}
