using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class ProductOutboundRepository : GenericRepository<ErpSaleDbContext, ProductOutbound>, IProductOutboundRepository
    {
        public ProductOutboundRepository(ErpSaleDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all ProductOutbound
        /// </summary>
        /// <returns>ProductOutbound list</returns>
        public IQueryable<ProductOutbound> GetAllProductOutbound()
        {
            return Context.ProductOutbound.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<ProductOutbound> GetAllProductOutboundFull()
        {
            return Context.ProductOutbound;
        }
        public IQueryable<vwProductOutbound> GetAllvwProductOutbound()
        {
            return Context.vwProductOutbound.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwProductOutbound> GetAllvwProductOutboundFull()
        {
            return Context.vwProductOutbound;
        }
        /// <summary>
        /// Get ProductOutbound information by specific id
        /// </summary>
        /// <param name="ProductOutboundId">Id of ProductOutbound</param>
        /// <returns></returns>
        public ProductOutbound GetProductOutboundById(int Id)
        {
            return Context.ProductOutbound.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwProductOutbound GetvwProductOutboundById(int Id)
        {
            return Context.vwProductOutbound.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwProductOutbound GetvwProductOutboundFullById(int Id)
        {
            return Context.vwProductOutbound.SingleOrDefault(item => item.Id == Id);
        }
        public ProductOutbound GetProductOutboundBySaleOrderId(int SaleOrderId)
        {
            return Context.ProductOutbound.SingleOrDefault(item => item.InvoiceId == SaleOrderId && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert ProductOutbound into database
        /// </summary>
        /// <param name="ProductOutbound">Object infomation</param>
        public void InsertProductOutbound(ProductOutbound ProductOutbound)
        {
            Context.ProductOutbound.Add(ProductOutbound);
            Context.Entry(ProductOutbound).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete ProductOutbound with specific id
        /// </summary>
        /// <param name="Id">ProductOutbound Id</param>
        public void DeleteProductOutbound(int Id)
        {
            ProductOutbound deletedProductOutbound = GetProductOutboundById(Id);
            Context.ProductOutbound.Remove(deletedProductOutbound);
            Context.Entry(deletedProductOutbound).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a ProductOutbound with its Id and Update IsDeleted IF that ProductOutbound has relationship with others
        /// </summary>
        /// <param name="ProductOutboundId">Id of ProductOutbound</param>
        public void DeleteProductOutboundRs(int Id)
        {
            ProductOutbound deleteProductOutboundRs = GetProductOutboundById(Id);
            deleteProductOutboundRs.IsDeleted = true;
            UpdateProductOutbound(deleteProductOutboundRs);
        }

        /// <summary>
        /// Update ProductOutbound into database
        /// </summary>
        /// <param name="ProductOutbound">ProductOutbound object</param>
        public void UpdateProductOutbound(ProductOutbound ProductOutbound)
        {
            Context.Entry(ProductOutbound).State = EntityState.Modified;
            Context.SaveChanges();
        }

        // --------------------------------------------------------------------------------
        //outbound detail

        public IQueryable<ProductOutboundDetail> GetAllProductOutboundDetailByOutboundId(int OutboundId)
        {
            return Context.ProductOutboundDetail.Where(item => item.ProductOutboundId == OutboundId && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public IQueryable<vwProductOutboundDetail> GetAllvwProductOutboundDetailByOutboundId(int OutboundId)
        {
            return Context.vwProductOutboundDetail.Where(item => item.ProductOutboundId == OutboundId && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public IQueryable<vwProductOutboundDetail> GetAllvwProductOutboundDetailByProductId(int ProductId)
        {
            return Context.vwProductOutboundDetail.Where(item => item.ProductId == ProductId && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public ProductOutboundDetail GetProductOutboundDetailById(int Id)
        {
            return Context.ProductOutboundDetail.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public IQueryable<vwProductOutboundDetail> GetAllvwProductOutboundDetail()
        {
            return Context.vwProductOutboundDetail.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        public void InsertProductOutboundDetail(ProductOutboundDetail ProductOutboundDetail)
        {
            Context.ProductOutboundDetail.Add(ProductOutboundDetail);
            Context.Entry(ProductOutboundDetail).State = EntityState.Added;
            Context.SaveChanges();
        }

        public void DeleteProductOutboundDetail(int Id)
        {
            ProductOutboundDetail deletedProductOutboundDetail = GetProductOutboundDetailById(Id);
            Context.ProductOutboundDetail.Remove(deletedProductOutboundDetail);
            Context.Entry(deletedProductOutboundDetail).State = EntityState.Deleted;
            Context.SaveChanges();
        }


        public void DeleteProductOutboundDetailRs(int Id)
        {
            ProductOutboundDetail deleteProductOutboundDetailRs = GetProductOutboundDetailById(Id);
            deleteProductOutboundDetailRs.IsDeleted = true;
            UpdateProductOutboundDetail(deleteProductOutboundDetailRs);
        }


        public void UpdateProductOutboundDetail(ProductOutboundDetail ProductOutboundDetail)
        {
            Context.Entry(ProductOutboundDetail).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
