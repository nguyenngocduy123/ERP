using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface IPurchaseOrderRepository
    {
        /// <summary>
        /// Get all PurchaseOrder
        /// </summary>
        /// <returns>PurchaseOrder list</returns>
        IQueryable<PurchaseOrder> GetAllPurchaseOrder();
        IQueryable<vwPurchaseOrder> GetAllvwPurchaseOrder();
        vwPurchaseOrder GetvwPurchaseOrderByCode(string Code);
        /// <summary>
        /// Get PurchaseOrder information by specific id
        /// </summary>
        /// <param name="Id">Id of PurchaseOrder</param>
        /// <returns></returns>
        PurchaseOrder GetPurchaseOrderById(int Id);
        vwPurchaseOrder GetvwPurchaseOrderById(int Id);

        /// <summary>
        /// Insert PurchaseOrder into database
        /// </summary>
        /// <param name="PurchaseOrder">Object infomation</param>
        int InsertPurchaseOrder(PurchaseOrder PurchaseOrder, List<PurchaseOrderDetail> orderDetails);

        /// <summary>
        /// Delete PurchaseOrder with specific id
        /// </summary>
        /// <param name="Id">PurchaseOrder Id</param>
        void DeletePurchaseOrder(int Id);

        /// <summary>
        /// Delete a PurchaseOrder with its Id and Update IsDeleted IF that PurchaseOrder has relationship with others
        /// </summary>
        /// <param name="Id">Id of PurchaseOrder</param>
        void DeletePurchaseOrderRs(int Id);

        void UpdatePurchaseOrder(PurchaseOrder PurchaseOrder);


        // Order detail
        IQueryable<PurchaseOrderDetail> GetAllOrderDetailsByOrderId(int orderId);
        IQueryable<vwPurchaseOrderDetail> GetvwAllOrderDetailsByOrderId(int orderId);
        vwPurchaseOrderDetail GetvwPurchaseOrderDetailById(int Id);
        PurchaseOrderDetail GetPurchaseOrderDetailById(int Id);

        void InsertPurchaseOrderDetail(PurchaseOrderDetail PurchaseOrderDetail);


        void DeletePurchaseOrderDetail(int Id);
        void DeletePurchaseOrderDetail(IEnumerable<PurchaseOrderDetail> list);

        void DeletePurchaseOrderDetailRs(int Id);


        void UpdatePurchaseOrderDetail(PurchaseOrderDetail PurchaseOrderDetail);
    }
}
