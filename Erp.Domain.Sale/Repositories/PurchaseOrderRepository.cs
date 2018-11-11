using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class PurchaseOrderRepository : GenericRepository<ErpSaleDbContext, PurchaseOrder>, IPurchaseOrderRepository
    {
        public PurchaseOrderRepository(ErpSaleDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all PurchaseOrder
        /// </summary>
        /// <returns>PurchaseOrder list</returns>
        public IQueryable<PurchaseOrder> GetAllPurchaseOrder()
        {
            return Context.PurchaseOrder.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        public IQueryable<vwPurchaseOrder> GetAllvwPurchaseOrder()
        {
            return Context.vwPurchaseOrder;
        }
        public vwPurchaseOrder GetvwPurchaseOrderByCode(string Code)
        {
            return Context.vwPurchaseOrder.SingleOrDefault(item => item.Code == Code && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Get PurchaseOrder information by specific id
        /// </summary>
        /// <param name="PurchaseOrderId">Id of PurchaseOrder</param>
        /// <returns></returns>
        public PurchaseOrder GetPurchaseOrderById(int Id)
        {
            return Context.PurchaseOrder.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public vwPurchaseOrder GetvwPurchaseOrderById(int Id)
        {
            return Context.vwPurchaseOrder.SingleOrDefault(item => item.Id == Id);
        }

        /// <summary>
        /// Insert PurchaseOrder into database
        /// </summary>
        /// <param name="PurchaseOrder">Object infomation</param>
        public int InsertPurchaseOrder(PurchaseOrder PurchaseOrder, List<PurchaseOrderDetail> orderDetails)
        {
            Context.PurchaseOrder.Add(PurchaseOrder);
            Context.Entry(PurchaseOrder).State = EntityState.Added;
            Context.SaveChanges();

            for (int i = 0; i < orderDetails.Count; i++)
            {
                orderDetails[i].PurchaseOrderId = PurchaseOrder.Id;
                InsertPurchaseOrderDetail(orderDetails[i]);
            }

            return PurchaseOrder.Id;
        }

        /// <summary>
        /// Delete PurchaseOrder with specific id
        /// </summary>
        /// <param name="Id">PurchaseOrder Id</param>
        public void DeletePurchaseOrder(int Id)
        {
            PurchaseOrder deletedPurchaseOrder = GetPurchaseOrderById(Id);
            Context.PurchaseOrder.Remove(deletedPurchaseOrder);
            Context.Entry(deletedPurchaseOrder).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete a PurchaseOrder with its Id and Update IsDeleted IF that PurchaseOrder has relationship with others
        /// </summary>
        /// <param name="PurchaseOrderId">Id of PurchaseOrder</param>
        public void DeletePurchaseOrderRs(int Id)
        {
            PurchaseOrder deletePurchaseOrderRs = GetPurchaseOrderById(Id);
            deletePurchaseOrderRs.IsDeleted = true;
            UpdatePurchaseOrder(deletePurchaseOrderRs);
        }

        /// <summary>
        /// Update PurchaseOrder into database
        /// </summary>
        /// <param name="PurchaseOrder">PurchaseOrder object</param>
        public void UpdatePurchaseOrder(PurchaseOrder PurchaseOrder)
        {
            Context.Entry(PurchaseOrder).State = EntityState.Modified;
            Context.SaveChanges();
        }


        //order detail

        public IQueryable<PurchaseOrderDetail> GetAllOrderDetailsByOrderId(int orderId)
        {
            return Context.PurchaseOrderDetail.Where(item => item.PurchaseOrderId == orderId && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwPurchaseOrderDetail> GetvwAllOrderDetailsByOrderId(int orderId)
        {
            return Context.vwPurchaseOrderDetail.Where(item => item.PurchaseOrderId == orderId && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwPurchaseOrderDetail GetvwPurchaseOrderDetailById(int Id)
        {
            return Context.vwPurchaseOrderDetail.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public PurchaseOrderDetail GetPurchaseOrderDetailById(int Id)
        {
            return Context.PurchaseOrderDetail.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public void InsertPurchaseOrderDetail(PurchaseOrderDetail PurchaseOrderDetail)
        {
            Context.PurchaseOrderDetail.Add(PurchaseOrderDetail);
            Context.Entry(PurchaseOrderDetail).State = EntityState.Added;
            Context.SaveChanges();
        }

        public void DeletePurchaseOrderDetail(int Id)
        {
            PurchaseOrderDetail deletedPurchaseOrderDetail = GetPurchaseOrderDetailById(Id);
            Context.PurchaseOrderDetail.Remove(deletedPurchaseOrderDetail);
            Context.Entry(deletedPurchaseOrderDetail).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        public void DeletePurchaseOrderDetail(IEnumerable<PurchaseOrderDetail> list)
        {
            for (int i = 0; i < list.Count(); i++)
            {
                PurchaseOrderDetail deletedPurchaseOrderDetail = GetPurchaseOrderDetailById(list.ElementAt(i).Id);
                Context.PurchaseOrderDetail.Remove(deletedPurchaseOrderDetail);
                Context.Entry(deletedPurchaseOrderDetail).State = EntityState.Deleted;
            }
            Context.SaveChanges();
        }

        public void DeletePurchaseOrderDetailRs(int Id)
        {
            PurchaseOrderDetail deletePurchaseOrderDetailRs = GetPurchaseOrderDetailById(Id);
            deletePurchaseOrderDetailRs.IsDeleted = true;
            UpdatePurchaseOrderDetail(deletePurchaseOrderDetailRs);
        }

        public void UpdatePurchaseOrderDetail(PurchaseOrderDetail PurchaseOrderDetail)
        {
            Context.Entry(PurchaseOrderDetail).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
