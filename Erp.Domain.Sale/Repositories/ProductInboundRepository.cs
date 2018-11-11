using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class ProductInboundRepository : GenericRepository<ErpSaleDbContext, ProductInbound>, IProductInboundRepository
    {
        public ProductInboundRepository(ErpSaleDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all ProductInbound
        /// </summary>
        /// <returns>ProductInbound list</returns>
        public IQueryable<ProductInbound> GetAllProductInbound()
        {
            return Context.ProductInbound.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwProductInbound> GetAllvwProductInbound()
        {
            return Context.vwProductInbound.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwProductInbound> GetAllvwProductInboundFull()
        {
            return Context.vwProductInbound;
        }
        /// <summary>
        /// Get ProductInbound information by specific id
        /// </summary>
        /// <param name="ProductInboundId">Id of ProductInbound</param>
        /// <returns></returns>
        public ProductInbound GetProductInboundById(int Id)
        {
            return Context.ProductInbound.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public vwProductInbound GetvwProductInboundByTransactionCode(string TransactionCode)
        {
            return Context.vwProductInbound.SingleOrDefault(item => item.Code == TransactionCode);
        }
        public vwProductInbound GetvwProductInboundById(int Id)
        {
            return Context.vwProductInbound.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwProductInbound GetvwProductInboundFullById(int Id)
        {
            return Context.vwProductInbound.SingleOrDefault(item => item.Id == Id);
        }
        /// <summary>
        /// Insert ProductInbound into database
        /// </summary>
        /// <param name="ProductInbound">Object infomation</param>
        public void InsertProductInbound(ProductInbound ProductInbound)
        {
            Context.ProductInbound.Add(ProductInbound);
            Context.Entry(ProductInbound).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete ProductInbound with specific id
        /// </summary>
        /// <param name="Id">ProductInbound Id</param>
        public void DeleteProductInbound(int Id)
        {
            ProductInbound deletedProductInbound = GetProductInboundById(Id);
            Context.ProductInbound.Remove(deletedProductInbound);
            Context.Entry(deletedProductInbound).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a ProductInbound with its Id and Update IsDeleted IF that ProductInbound has relationship with others
        /// </summary>
        /// <param name="ProductInboundId">Id of ProductInbound</param>
        public void DeleteProductInboundRs(int Id)
        {
            ProductInbound deleteProductInboundRs = GetProductInboundById(Id);
            deleteProductInboundRs.IsDeleted = true;
            UpdateProductInbound(deleteProductInboundRs);
        }

        /// <summary>
        /// Update ProductInbound into database
        /// </summary>
        /// <param name="ProductInbound">ProductInbound object</param>
        public void UpdateProductInbound(ProductInbound ProductInbound)
        {
            Context.Entry(ProductInbound).State = EntityState.Modified;
            Context.SaveChanges();
        }


        //----------------------------------------------------------------------------------------
        // Inbound detail

        public IQueryable<ProductInboundDetail> GetAllProductInboundDetailByInboundId(int inboundId)
        {
            return Context.ProductInboundDetail.Where(item => item.ProductInboundId == inboundId && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public IQueryable<vwProductInboundDetail> GetAllvwProductInboundDetailByInboundId(int inboundId)
        {
            return Context.vwProductInboundDetail.Where(item => item.ProductInboundId == inboundId && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public IQueryable<vwProductInboundDetail> GetAllvwProductInboundDetailByProductId(int ProductId)
        {
            return Context.vwProductInboundDetail.Where(item => item.ProductId == ProductId && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwProductInboundDetail> GetAllvwProductInboundDetail()
        {
            return Context.vwProductInboundDetail.Where(item =>item.IsDeleted == null || item.IsDeleted == false);
        }
        public ProductInboundDetail GetProductInboundDetailById(int Id)
        {
            return Context.ProductInboundDetail.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public void InsertProductInboundDetail(ProductInboundDetail ProductInboundDetail)
        {
            Context.ProductInboundDetail.Add(ProductInboundDetail);
            Context.Entry(ProductInboundDetail).State = EntityState.Added;
            Context.SaveChanges();
        }


        public void DeleteProductInboundDetail(int Id)
        {
            ProductInboundDetail deletedProductInboundDetail = GetProductInboundDetailById(Id);
            Context.ProductInboundDetail.Remove(deletedProductInboundDetail);
            Context.Entry(deletedProductInboundDetail).State = EntityState.Deleted;
            Context.SaveChanges();
        }


        public void DeleteProductInboundDetailRs(int Id)
        {
            ProductInboundDetail deleteProductInboundDetailRs = GetProductInboundDetailById(Id);
            deleteProductInboundDetailRs.IsDeleted = true;
            UpdateProductInboundDetail(deleteProductInboundDetailRs);
        }


        public void UpdateProductInboundDetail(ProductInboundDetail ProductInboundDetail)
        {
            Context.Entry(ProductInboundDetail).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
