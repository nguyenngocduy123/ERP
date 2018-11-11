using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class ProductInvoiceRepository : GenericRepository<ErpSaleDbContext, ProductInvoice>, IProductInvoiceRepository
    {
        public ProductInvoiceRepository(ErpSaleDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all ProductInvoice
        /// </summary>
        /// <returns>ProductInvoice list</returns>
        public IQueryable<ProductInvoice> GetAllProductInvoice()
        {
            return Context.ProductInvoice.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        public IQueryable<vwProductInvoice> GetAllvwProductInvoice()
        {
            return Context.vwProductInvoice.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        public IQueryable<vwProductInvoice> GetAllvwProductInvoiceFull()
        {
            return Context.vwProductInvoice;
        }

        /// <summary>
        /// Get ProductInvoice information by specific id
        /// </summary>
        /// <param name="ProductInvoiceId">Id of ProductInvoice</param>
        /// <returns></returns>
        public ProductInvoice GetProductInvoiceById(int Id)
        {
            return Context.ProductInvoice.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public vwProductInvoice GetvwProductInvoiceById(int Id)
        {
            return Context.vwProductInvoice.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwProductInvoice GetvwProductInvoiceFullById(int Id)
        {
            return Context.vwProductInvoice.SingleOrDefault(item => item.Id == Id);
        }
        public vwProductInvoice GetvwProductInvoiceByCode(string code)
        {
            return Context.vwProductInvoice.SingleOrDefault(item => item.Code == code && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert ProductInvoice into database
        /// </summary>
        /// <param name="ProductInvoice">Object infomation</param>
        public int InsertProductInvoice(ProductInvoice ProductInvoice, List<ProductInvoiceDetail> orderDetails)
        {
            Context.ProductInvoice.Add(ProductInvoice);
            Context.Entry(ProductInvoice).State = EntityState.Added;
            Context.SaveChanges();
           
            for (int i = 0; i < orderDetails.Count; i++)
            {
                orderDetails[i].ProductInvoiceId = ProductInvoice.Id;
                InsertProductInvoiceDetail(orderDetails[i]);
            }

            return ProductInvoice.Id;
        }

        /// <summary>
        /// Delete ProductInvoice with specific id
        /// </summary>
        /// <param name="Id">ProductInvoice Id</param>
        public void DeleteProductInvoice(int Id)
        {
            ProductInvoice deletedProductInvoice = GetProductInvoiceById(Id);
            Context.ProductInvoice.Remove(deletedProductInvoice);
            Context.Entry(deletedProductInvoice).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a ProductInvoice with its Id and Update IsDeleted IF that ProductInvoice has relationship with others
        /// </summary>
        /// <param name="ProductInvoiceId">Id of ProductInvoice</param>
        public void DeleteProductInvoiceRs(int Id)
        {
            ProductInvoice deleteProductInvoiceRs = GetProductInvoiceById(Id);
            deleteProductInvoiceRs.IsDeleted = true;
            UpdateProductInvoice(deleteProductInvoiceRs);
        }

        /// <summary>
        /// Update ProductInvoice into database
        /// </summary>
        /// <param name="ProductInvoice">ProductInvoice object</param>
        public void UpdateProductInvoice(ProductInvoice ProductInvoice)
        {
            Context.Entry(ProductInvoice).State = EntityState.Modified;
            Context.SaveChanges();
        }


        //order detail

        public IQueryable<vwProductInvoiceDetail> GetAllvwInvoiceDetails()
        {
            return Context.vwProductInvoiceDetail.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        public IQueryable<ProductInvoiceDetail> GetAllInvoiceDetailsByInvoiceId(int InvoiceId)
        {
            return Context.ProductInvoiceDetail.Where(item => item.ProductInvoiceId == InvoiceId && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwProductInvoiceDetail> GetAllvwInvoiceDetailsByInvoiceId(int InvoiceId)
        {
            return Context.vwProductInvoiceDetail.Where(item => item.ProductInvoiceId == InvoiceId && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public ProductInvoiceDetail GetProductInvoiceDetailById(int Id)
        {
            return Context.ProductInvoiceDetail.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwProductInvoiceDetail GetvwProductInvoiceDetailById(int Id)
        {
            return Context.vwProductInvoiceDetail.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        //dùng để xóa userservicelog khi xóa product invoice detail
        public UsingServiceLog GetUsingServiceLogById(int Id)
        {
            return Context.UsingServiceLog.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        //dùng để xóa UsingServiceLogDetail khi xóa UsingServiceLog
        public UsingServiceLogDetail GetUsingServiceLogDetailById(int Id)
        {
            return Context.UsingServiceLogDetail.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        //dùng để xóa LogServiceRemminder khi xóa ProductInvoiceDetail
        public LogServiceRemminder GetLogServiceRemminderById(int Id)
        {
            return Context.LogServiceRemminder.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public void InsertProductInvoiceDetail(ProductInvoiceDetail ProductInvoiceDetail)
        {
            Context.ProductInvoiceDetail.Add(ProductInvoiceDetail);
            Context.Entry(ProductInvoiceDetail).State = EntityState.Added;
            Context.SaveChanges();
        }

        public void DeleteProductInvoiceDetail(int Id)
        {
            ProductInvoiceDetail deletedProductInvoiceDetail = GetProductInvoiceDetailById(Id);
            Context.ProductInvoiceDetail.Remove(deletedProductInvoiceDetail);
            Context.Entry(deletedProductInvoiceDetail).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        public IQueryable<UsingServiceLog> GetAllUsingServiceLog()
        {
            return Context.UsingServiceLog.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<UsingServiceLogDetail> GetAllUsingServiceLogDetail()
        {
            return Context.UsingServiceLogDetail.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<LogServiceRemminder> GetAllLogServiceRemminder()
        {
            return Context.LogServiceRemminder.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public void DeleteProductInvoiceDetail(List<ProductInvoiceDetail> list)
        {
            foreach(var item in list)
            {
                var listlog = GetAllUsingServiceLog().Where(x => x.ServiceInvoiceDetailId == item.Id).Select(x => x.Id).ToList();
                var listlogReminder = GetAllLogServiceRemminder().Where(x => x.ProductInvoiceDetailId == item.Id).Select(x => x.Id).ToList();
                foreach (var logId in listlog)
                {
                    var listlogDetail = GetAllUsingServiceLogDetail().Where(x => x.UsingServiceId == logId).Select(x => x.Id).ToList();
                    foreach (var logDetailId in listlogDetail)
                    {
                        UsingServiceLogDetail deleteUsingServiceLogDetail = GetUsingServiceLogDetailById(logDetailId);
                        Context.UsingServiceLogDetail.Remove(deleteUsingServiceLogDetail);
                        Context.Entry(deleteUsingServiceLogDetail).State = EntityState.Deleted;
                    }
                    UsingServiceLog deleteUsingServiceLog = GetUsingServiceLogById(logId);
                    Context.UsingServiceLog.Remove(deleteUsingServiceLog);
                    Context.Entry(deleteUsingServiceLog).State = EntityState.Deleted;
                }
                foreach (var reminder in listlogReminder)
                {
                    LogServiceRemminder deleteLogServiceRemminder = GetLogServiceRemminderById(reminder);
                    Context.LogServiceRemminder.Remove(deleteLogServiceRemminder);
                    Context.Entry(deleteLogServiceRemminder).State = EntityState.Deleted;
                }
                ProductInvoiceDetail deletedProductInvoiceDetail = GetProductInvoiceDetailById(item.Id);
                Context.ProductInvoiceDetail.Remove(deletedProductInvoiceDetail);
                Context.Entry(deletedProductInvoiceDetail).State = EntityState.Deleted;
            }

            Context.SaveChanges();
        }

        public void DeleteProductInvoiceDetailRs(int Id)
        {
            ProductInvoiceDetail deleteProductInvoiceDetailRs = GetProductInvoiceDetailById(Id);
            deleteProductInvoiceDetailRs.IsDeleted = true;
            UpdateProductInvoiceDetail(deleteProductInvoiceDetailRs);
        }

        public void UpdateProductInvoiceDetail(ProductInvoiceDetail ProductInvoiceDetail)
        {
            Context.Entry(ProductInvoiceDetail).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
