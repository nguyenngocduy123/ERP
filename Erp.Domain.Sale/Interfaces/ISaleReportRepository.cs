using Erp.Domain.Sale.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface ISaleReportRepository
    {
        IQueryable<vwReportCustomer> GetAllvwReportCustomer();

        IQueryable<vwReportProduct> GetAllvwReportProduct();

        IEnumerable<vwReportProduct> GetAllspReportProductOnSale(int branchId);

        IEnumerable<spReportProductInbound> GetAllspReportProductInbound(int branchId, DateTime startDate, DateTime endDate, decimal minTotalAmount, decimal maxTotalAmount, int warehouseDestinationId);

        IEnumerable<spReportProductInbound> GetAllspReportProductOutbound(int branchId, DateTime startDate, DateTime endDate, decimal minTotalAmount, decimal maxTotalAmount, int warehouseDestinationId);
    }
}
