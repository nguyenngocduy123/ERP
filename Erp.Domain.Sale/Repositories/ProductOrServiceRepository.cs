using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class ProductOrServiceRepository : GenericRepository<ErpSaleDbContext, Product>, IProductOrServiceRepository
    {
        public ProductOrServiceRepository(ErpSaleDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all Product
        /// </summary>
        /// <returns>Product list</returns>
        /// 
        public IQueryable<Product> GetAllProduct()
        {
            return Context.Product.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwProductAndService> GetAllvwProductAndService()
        {
            return Context.vwProductAndService.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwProduct> GetAllvwProduct()
        {
            return Context.vwProduct.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwService> GetAllvwService()
        {
            return Context.vwService.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<Product> GetAllProductByType(string type)
        {
            return Context.Product.Where(item => item.Type.Contains(type) && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public IQueryable<vwProduct> GetAllvwProductByType(string type)
        {
            return Context.vwProduct.Where(item => item.Type.Contains(type) && (item.IsDeleted == null || item.IsDeleted == false));
        }


        /// <summary>
        /// Get Product information by specific id
        /// </summary>
        /// <param name="ProductId">Id of Product</param>
        /// <returns></returns>
        
        public Product GetProductById(int Id)
        {
            return Context.Product.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public vwProduct GetvwProductById(int Id)
        {
            return Context.vwProduct.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwService GetvwServiceById(int Id)
        {
            return Context.vwService.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert Product into database
        /// </summary>
        /// <param name="Product">Object infomation</param>
        public void InsertProduct(Product Product)
        {
            Context.Product.Add(Product);
            Context.Entry(Product).State = EntityState.Added;
            Context.SaveChanges();
        }
        public void InsertService(Product Service)
        {
            Context.Product.Add(Service);
            Context.Entry(Service).State = EntityState.Added;
            Context.SaveChanges();
        }


        /// <summary>
        /// Delete Product with specific id
        /// </summary>
        /// <param name="Id">Product Id</param>
        public void DeleteProduct(int Id)
        {
            Product deletedProduct = GetProductById(Id);
            Context.Product.Remove(deletedProduct);
            Context.Entry(deletedProduct).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        public void DeleteService(int Id)
        {
            Product deletedService = GetProductById(Id);
            Context.Product.Remove(deletedService);
            Context.Entry(deletedService).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        /// <summary>
        /// Delete a Product with its Id and Update IsDeleted IF that Product has relationship with others
        /// </summary>
        /// <param name="ProductId">Id of Product</param>
        public void DeleteProductRs(int Id)
        {
            Product deleteProductRs = GetProductById(Id);
            deleteProductRs.IsDeleted = true;
            UpdateProduct(deleteProductRs);
        }
        public void DeleteServiceRs(int Id)
        {
            Product deleteServiceRs = GetProductById(Id);
            deleteServiceRs.IsDeleted = true;
            UpdateService(deleteServiceRs);
        }
        /// <summary>
        /// Update Product into database
        /// </summary>
        /// <param name="Product">Product object</param>
        public void UpdateProduct(Product Product)
        {
            Context.Entry(Product).State = EntityState.Modified;
            Context.SaveChanges();
        }
        public void UpdateService(Product Service)
        {
            Context.Entry(Service).State = EntityState.Modified;
            Context.SaveChanges();
        }


        public void InsertServiceCombo(vwService Service, List<ServiceCombo> orderDetails)
        {
            Context.vwService.Add(Service);
            Context.Entry(Service).State = EntityState.Added;
            Context.SaveChanges();
            for (int i = 0; i < orderDetails.Count; i++)
            {
                orderDetails[i].ComboId = Service.Id;
                InsertServiceCombo(orderDetails[i]);
            }

            //return ServiceInvoice.Id;
        }
        public void InsertServiceCombo(ServiceCombo ServiceCombo)
        {
            Context.ServiceCombo.Add(ServiceCombo);
            Context.Entry(ServiceCombo).State = EntityState.Added;
            Context.SaveChanges();
        }
        public ServiceCombo GetServiceComboById(int Id)
        {
            return Context.ServiceCombo.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public void DeleteServiceCombo(IEnumerable<ServiceCombo> list)
        {
            for (int i = 0; i < list.Count(); i++)
            {
                ServiceCombo deletedServiceCombo = GetServiceComboById(list.ElementAt(i).Id);
                Context.ServiceCombo.Remove(deletedServiceCombo);
                Context.Entry(deletedServiceCombo).State = EntityState.Deleted;
            }
            Context.SaveChanges();
        }
    }
}
