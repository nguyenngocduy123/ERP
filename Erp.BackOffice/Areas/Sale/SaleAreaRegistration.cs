using Erp.BackOffice.Sale.Models;
using Erp.Domain.Sale.Entities;
using System.Web.Mvc;

namespace Erp.BackOffice.Sale
{
    public class SaleAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Sale";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                 "Sale_SaleCategory",
                 "SaleCategory/{action}/{id}",
                 new { controller = "SaleCategory", action = "Index", id = UrlParameter.Optional }
             );

            context.MapRoute(
                 "Sale_Product",
                 "Product/{action}/{id}",
                 new { controller = "Product", action = "Index", id = UrlParameter.Optional }
             );
            context.MapRoute(
                 "Sale_Supplier",
                 "Supplier/{action}/{id}",
                 new { controller = "Supplier", action = "Index", id = UrlParameter.Optional }
             );

            context.MapRoute(
                 "Sale_Warehouse",
                 "Warehouse/{action}/{id}",
                 new { controller = "Warehouse", action = "Index", id = UrlParameter.Optional }
             );

            context.MapRoute(
                 "Sale_PurchaseOrder",
                 "PurchaseOrder/{action}/{id}",
                 new { controller = "PurchaseOrder", action = "Index", id = UrlParameter.Optional }
             );

            context.MapRoute(
                 "Sale_ProductInvoice",
                 "ProductInvoice/{action}/{id}",
                 new { controller = "ProductInvoice", action = "Index", id = UrlParameter.Optional }
             );

            context.MapRoute(
                 "Sale_Inventory",
                 "Inventory/{action}/{id}",
                 new { controller = "Inventory", action = "Index", id = UrlParameter.Optional }
             );

            context.MapRoute(
                 "Sale_ProductOutbound",
                 "ProductOutbound/{action}/{id}",
                 new { controller = "ProductOutbound", action = "Index", id = UrlParameter.Optional }
             );

            context.MapRoute(
                 "Sale_ProductInBound",
                 "ProductInBound/{action}/{id}",
                 new { controller = "ProductInBound", action = "Index", id = UrlParameter.Optional }
             );
            context.MapRoute(
                 "Sale_PhysicalInventory",
                 "PhysicalInventory/{action}/{id}",
                 new { controller = "PhysicalInventory", action = "Index", id = UrlParameter.Optional }
             );

            context.MapRoute(
                 "Sale_PhysicalInventoryDetail",
                 "PhysicalInventoryDetail/{action}/{id}",
                 new { controller = "PhysicalInventoryDetail", action = "Index", id = UrlParameter.Optional }
             );

            context.MapRoute(
                 "Sale_ObjectAttribute",
                 "ObjectAttribute/{action}/{id}",
                 new { controller = "ObjectAttribute", action = "Index", id = UrlParameter.Optional }
             );

            context.MapRoute(
                 "Sales_Commision",
                 "Commision/{action}/{id}",
                 new { controller = "Commision", action = "Index", id = UrlParameter.Optional }
             );

            context.MapRoute(
                "Sale_vwCommision_Branch",
                "vwCommision_Branch/{action}/{id}",
                new { controller = "vwCommision_Branch", action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
            "Sale_vwBranchDepartment",
            "vwBranchDepartment/{action}/{id}",
            new { controller = "vwBranchDepartment", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Sale_SaleOrder",
            "SaleOrder/{action}/{id}",
            new { controller = "SaleOrder", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Sale_SaleOrderDetail",
            "SaleOrderDetail/{action}/{id}",
            new { controller = "SaleOrderDetail", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Sale_Report",
            "SaleReport/{action}/{id}",
            new { controller = "SaleReport", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Sale_WarehouseLocationItem",
            "WarehouseLocationItem/{action}/{id}",
            new { controller = "WarehouseLocationItem", action = "Index", id = UrlParameter.Optional }
            );


            context.MapRoute(
            "Sale_Promotion",
            "Promotion/{action}/{id}",
            new { controller = "Promotion", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Sale_SalesReturns",
            "SalesReturns/{action}/{id}",
            new { controller = "SalesReturns", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Sale_CommisionStaff",
            "CommisionStaff/{action}/{id}",
            new { controller = "CommisionStaff", action = "Index", id = UrlParameter.Optional }
            );


            context.MapRoute(
            "Sale_TemplatePrint",
            "TemplatePrint/{action}/{id}",
            new { controller = "TemplatePrint", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Sale_CommisionDetail",
            "CommisionDetail/{action}/{id}",
            new { controller = "CommisionDetail", action = "Index", id = UrlParameter.Optional }
            );

       
            context.MapRoute(
            "Sale_Service",
            "Service/{action}/{id}",
            new { controller = "Service", action = "Index", id = UrlParameter.Optional }
            );


            context.MapRoute(
            "Sale_UsingServiceLog",
            "UsingServiceLog/{action}/{id}",
            new { controller = "UsingServiceLog", action = "Index", id = UrlParameter.Optional }
            );

       
            context.MapRoute(
            "Sale_RequestInbound",
            "RequestInbound/{action}/{id}",
            new { controller = "RequestInbound", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Sale_RequestInboundDetail",
            "RequestInboundDetail/{action}/{id}",
            new { controller = "RequestInboundDetail", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Sale_UsingServiceLogDetail",
            "UsingServiceLogDetail/{action}/{id}",
            new { controller = "UsingServiceLogDetail", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Sale_ProductDamaged",
            "ProductDamaged/{action}/{id}",
            new { controller = "ProductDamaged", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Sale_ServiceReminder",
            "ServiceReminder/{action}/{id}",
            new { controller = "ServiceReminder", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Sale_ServiceReminderGroup",
            "ServiceReminderGroup/{action}/{id}",
            new { controller = "ServiceReminderGroup", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Sale_LogServiceRemminder",
            "LogServiceRemminder/{action}/{id}",
            new { controller = "LogServiceRemminder", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Sale_Commision_Customer",
            "CommisionCustomer/{action}/{id}",
            new { controller = "CommisionCustomer", action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
            "Sale_ServiceSchedule",
            "ServiceSchedule/{action}/{id}",
            new { controller = "ServiceSchedule", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Sale_LogLoyaltyPoint",
            "LogLoyaltyPoint/{action}/{id}",
            new { controller = "LogLoyaltyPoint", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Sale_LoyaltyPoint",
            "LoyaltyPoint/{action}/{id}",
            new { controller = "LoyaltyPoint", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Sale_CommissionCus",
            "CommissionCus/{action}/{id}",
            new { controller = "CommissionCus", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Sale_TotalDiscountMoneyNT",
            "TotalDiscountMoneyNT/{action}/{id}",
            new { controller = "TotalDiscountMoneyNT", action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
          "Sale_LogVip",
          "LogVip/{action}/{id}",
          new { controller = "LogVip", action = "Index", id = UrlParameter.Optional }
          );
            //<append_content_route_here>

            RegisterAutoMapperMap();
        }

        private static void RegisterAutoMapperMap()
        {
            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.Product, ProductViewModel>();
            AutoMapper.Mapper.CreateMap<ProductViewModel, Domain.Sale.Entities.Product>();
            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.vwProduct, ProductViewModel>();
            AutoMapper.Mapper.CreateMap<ServiceViewModel, Domain.Sale.Entities.Product>();

            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.Supplier, SupplierViewModel>();
            AutoMapper.Mapper.CreateMap<SupplierViewModel, Domain.Sale.Entities.Supplier>();

            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.Warehouse, WarehouseViewModel>();
            AutoMapper.Mapper.CreateMap<WarehouseViewModel, Domain.Sale.Entities.Warehouse>();

            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.PurchaseOrder, PurchaseOrderViewModel>();
            AutoMapper.Mapper.CreateMap<PurchaseOrderViewModel, Domain.Sale.Entities.PurchaseOrder>();
            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.vwPurchaseOrder, PurchaseOrderViewModel>();

            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.ProductInvoice, ProductInvoiceViewModel>();
            AutoMapper.Mapper.CreateMap<ProductInvoiceViewModel, Domain.Sale.Entities.ProductInvoice>();
            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.vwProductInvoice, ProductInvoiceViewModel>();
            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.vwProductInvoiceDetail, ProductInvoiceDetailViewModel>();
            AutoMapper.Mapper.CreateMap<ProductInvoiceDetailViewModel, ProductOutboundDetailViewModel>();

            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.PurchaseOrderDetail, PurchaseOrderDetailViewModel>();
            AutoMapper.Mapper.CreateMap<PurchaseOrderDetailViewModel, Domain.Sale.Entities.PurchaseOrderDetail>();

            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.Inventory, InventoryViewModel>();
            AutoMapper.Mapper.CreateMap<InventoryViewModel, Domain.Sale.Entities.Inventory>();

            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.ProductOutbound, ProductOutboundViewModel>();
            AutoMapper.Mapper.CreateMap<ProductOutboundViewModel, Domain.Sale.Entities.ProductOutbound>();
            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.vwProductOutbound, ProductOutboundViewModel>();
            AutoMapper.Mapper.CreateMap<ProductOutboundViewModel, Domain.Sale.Entities.vwProductOutbound>();

            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.ProductOutbound, ProductOutboundTransferViewModel>();
            AutoMapper.Mapper.CreateMap<ProductOutboundTransferViewModel, Domain.Sale.Entities.ProductOutbound>();
            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.vwProductOutbound, ProductOutboundTransferViewModel>();
            AutoMapper.Mapper.CreateMap<ProductOutboundTransferViewModel, Domain.Sale.Entities.vwProductOutbound>();

            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.ProductInbound, ProductInboundViewModel>();
            AutoMapper.Mapper.CreateMap<ProductInboundViewModel, Domain.Sale.Entities.ProductInbound>();
            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.vwProductInbound, ProductInboundViewModel>();

            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.ProductInboundDetail, ProductInboundDetailViewModel>();
            AutoMapper.Mapper.CreateMap<ProductInboundDetailViewModel, Domain.Sale.Entities.ProductInboundDetail>();

            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.ProductOutboundDetail, ProductOutboundDetailViewModel>();
            AutoMapper.Mapper.CreateMap<ProductOutboundDetailViewModel, Domain.Sale.Entities.ProductOutboundDetail>();
            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.vwProductOutboundDetail, ProductOutboundDetailViewModel>();
            AutoMapper.Mapper.CreateMap<ProductOutboundDetailViewModel, Domain.Sale.Entities.vwProductOutboundDetail>();

            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.PhysicalInventory, PhysicalInventoryViewModel>();
            AutoMapper.Mapper.CreateMap<PhysicalInventoryViewModel, Domain.Sale.Entities.PhysicalInventory>();
            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.vwPhysicalInventory, PhysicalInventoryViewModel>();
            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.vwPhysicalInventory, PhysicalInventory>();

            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.PhysicalInventoryDetail, PhysicalInventoryDetailViewModel>();
            AutoMapper.Mapper.CreateMap<PhysicalInventoryDetailViewModel, Domain.Sale.Entities.PhysicalInventoryDetail>();

            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.ObjectAttribute, ObjectAttributeViewModel>();
            AutoMapper.Mapper.CreateMap<ObjectAttributeViewModel, Domain.Sale.Entities.ObjectAttribute>();

            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.ObjectAttributeValue, ObjectAttributeValueViewModel>();
            AutoMapper.Mapper.CreateMap<ObjectAttributeValueViewModel, Domain.Sale.Entities.ObjectAttributeValue>();

            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.Commision, CommisionViewModel>();
            AutoMapper.Mapper.CreateMap<CommisionViewModel, Domain.Sale.Entities.Commision>();

            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.vwCommision, CommisionViewModel>();
            AutoMapper.Mapper.CreateMap<CommisionViewModel, Domain.Sale.Entities.vwCommision>();

            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.WarehouseLocationItem, WarehouseLocationItemViewModel>();
            AutoMapper.Mapper.CreateMap<WarehouseLocationItemViewModel, Domain.Sale.Entities.WarehouseLocationItem>();

            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.WarehouseLocationItem, vwWarehouseLocationItem>();
            AutoMapper.Mapper.CreateMap<vwWarehouseLocationItem, Domain.Sale.Entities.WarehouseLocationItem>();

            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.SalesReturns, SalesReturnsViewModel>();
            AutoMapper.Mapper.CreateMap<SalesReturnsViewModel, Domain.Sale.Entities.SalesReturns>();
            AutoMapper.Mapper.CreateMap<SalesReturnsDetailViewModel, Domain.Sale.Entities.SalesReturnsDetail>();
            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.vwSalesReturns, SalesReturnsViewModel>();
            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.vwProductInvoiceDetail, SalesReturnsDetailViewModel>();
            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.vwSalesReturnsDetail, SalesReturnsDetailViewModel>();

            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.Promotion, PromotionViewModel>();
            AutoMapper.Mapper.CreateMap<PromotionViewModel, Domain.Sale.Entities.Promotion>();

            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.PromotionDetail, PromotionDetailViewModel>();
            AutoMapper.Mapper.CreateMap<PromotionDetailViewModel, Domain.Sale.Entities.PromotionDetail>();

            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.vwProductInvoice, SalesReturnsViewModel>();
            AutoMapper.Mapper.CreateMap<SalesReturnsViewModel, Domain.Sale.Entities.vwProductInvoice>();

            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.CommisionStaff, CommisionStaffViewModel>();
            AutoMapper.Mapper.CreateMap<CommisionStaffViewModel, Domain.Sale.Entities.CommisionStaff>();

            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.TemplatePrint, TemplatePrintViewModel>();
            AutoMapper.Mapper.CreateMap<TemplatePrintViewModel, Domain.Sale.Entities.TemplatePrint>();


            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.vwService, ServiceViewModel>();
            AutoMapper.Mapper.CreateMap<ServiceViewModel, Domain.Sale.Entities.vwService>();

            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.UsingServiceLog, UsingServiceLogViewModel>();
            AutoMapper.Mapper.CreateMap<UsingServiceLogViewModel, Domain.Sale.Entities.UsingServiceLog>();
            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.vwUsingServiceLog, UsingServiceLogViewModel>();
            AutoMapper.Mapper.CreateMap<UsingServiceLogViewModel, Domain.Sale.Entities.vwUsingServiceLog>();

            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.ServiceCombo, ServiceComboViewModel>();
            AutoMapper.Mapper.CreateMap<ServiceComboViewModel, Domain.Sale.Entities.ServiceCombo>();
            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.vwServiceCombo, ServiceComboViewModel>();
            AutoMapper.Mapper.CreateMap<ServiceComboViewModel, Domain.Sale.Entities.vwServiceCombo>();

            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.RequestInbound, RequestInboundViewModel>();
            AutoMapper.Mapper.CreateMap<RequestInboundViewModel, Domain.Sale.Entities.RequestInbound>();
            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.vwRequestInbound, RequestInboundViewModel>();
            AutoMapper.Mapper.CreateMap<RequestInboundViewModel, Domain.Sale.Entities.vwRequestInbound>();

            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.RequestInboundDetail, RequestInboundDetailViewModel>();
            AutoMapper.Mapper.CreateMap<RequestInboundDetailViewModel, Domain.Sale.Entities.RequestInboundDetail>();
            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.vwRequestInboundDetail, RequestInboundDetailViewModel>();
            AutoMapper.Mapper.CreateMap<RequestInboundDetailViewModel, Domain.Sale.Entities.vwRequestInboundDetail>();

            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.UsingServiceLogDetail, UsingServiceLogDetailViewModel>();
            AutoMapper.Mapper.CreateMap<UsingServiceLogDetailViewModel, Domain.Sale.Entities.UsingServiceLogDetail>();
            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.vwUsingServiceLogDetail, UsingServiceLogDetailViewModel>();
            AutoMapper.Mapper.CreateMap<UsingServiceLogDetailViewModel, Domain.Sale.Entities.vwUsingServiceLogDetail>();

            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.ProductDamaged, ProductDamagedViewModel>();
            AutoMapper.Mapper.CreateMap<ProductDamagedViewModel, Domain.Sale.Entities.ProductDamaged>();

            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.ServiceReminder, ServiceReminderViewModel>();
            AutoMapper.Mapper.CreateMap<ServiceReminderViewModel, Domain.Sale.Entities.ServiceReminder>();

            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.ServiceReminderGroup, ServiceReminderGroupViewModel>();
            AutoMapper.Mapper.CreateMap<ServiceReminderGroupViewModel, Domain.Sale.Entities.ServiceReminderGroup>();

            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.LogServiceRemminder, LogServiceRemminderViewModel>();
            AutoMapper.Mapper.CreateMap<LogServiceRemminderViewModel, Domain.Sale.Entities.LogServiceRemminder>();

            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.CommisionCustomer, CommisionCustomerViewModel>();
            AutoMapper.Mapper.CreateMap<CommisionCustomerViewModel, Domain.Sale.Entities.CommisionCustomer>();

            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.vwCommisionCustomer, CommisionCustomerViewModel>();
            AutoMapper.Mapper.CreateMap<CommisionCustomerViewModel, Domain.Sale.Entities.vwCommisionCustomer>();

            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.ServiceSchedule, ServiceScheduleViewModel>();
            AutoMapper.Mapper.CreateMap<ServiceScheduleViewModel, Domain.Sale.Entities.ServiceSchedule>();
            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.vwServiceSchedule, ServiceScheduleViewModel>();
            AutoMapper.Mapper.CreateMap<ServiceScheduleViewModel, Domain.Sale.Entities.vwServiceSchedule>();

            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.LogLoyaltyPoint, LogLoyaltyPointViewModel>();
            AutoMapper.Mapper.CreateMap<LogLoyaltyPointViewModel, Domain.Sale.Entities.LogLoyaltyPoint>();
            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.vwLogLoyaltyPoint, LogLoyaltyPointViewModel>();
            AutoMapper.Mapper.CreateMap<LogLoyaltyPointViewModel, Domain.Sale.Entities.vwLogLoyaltyPoint>();

            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.LoyaltyPoint, LoyaltyPointViewModel>();
            AutoMapper.Mapper.CreateMap<LoyaltyPointViewModel, Domain.Sale.Entities.LoyaltyPoint>();

            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.CommissionCus, CommissionCusViewModel>();
            AutoMapper.Mapper.CreateMap<CommissionCusViewModel, Domain.Sale.Entities.CommissionCus>();

            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.TotalDiscountMoneyNT, TotalDiscountMoneyNTViewModel>();
            AutoMapper.Mapper.CreateMap<TotalDiscountMoneyNTViewModel, Domain.Sale.Entities.TotalDiscountMoneyNT>();
            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.vwTotalDiscountMoneyNT, TotalDiscountMoneyNTViewModel>();
            AutoMapper.Mapper.CreateMap<TotalDiscountMoneyNTViewModel, Domain.Sale.Entities.vwTotalDiscountMoneyNT>();

            AutoMapper.Mapper.CreateMap<Domain.Sale.Entities.LogVip, LogVipViewModel>();
            AutoMapper.Mapper.CreateMap<LogVipViewModel, Domain.Sale.Entities.LogVip>();
            //<append_content_mapper_here>
        }
    }
}
