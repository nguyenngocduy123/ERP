using Erp.BackOffice.Account.Models;
using Erp.Domain.Entities;
using System.Web.Mvc;

namespace Erp.BackOffice.Account
{
    public class AccountAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Account";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            context.MapRoute(
                 "Account_Customer",
                 "Customer/{action}/{id}",
                 new { controller = "Customer", action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "Account_Contact",
                "Contact/{action}/{id}",
                new { controller = "Contact", action = "Index", id = UrlParameter.Optional }
           );
            context.MapRoute(
            "Account_ContractLease",
            "ContractLease/{action}/{id}",
            new { controller = "ContractLease", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Account_InfoPartyA",
            "InfoPartyA/{action}/{id}",
            new { controller = "InfoPartyA", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Account_ContractSell",
            "ContractSell/{action}/{id}",
            new { controller = "ContractSell", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Account_Contract",
            "Contract/{action}/{id}",
            new { controller = "Contract", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Account_ProcessPayment",
            "ProcessPayment/{action}/{id}",
            new { controller = "ProcessPayment", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Account_Transaction",
            "Transaction/{action}/{id}",
            new { controller = "Transaction", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Account_Receipt",
            "Receipt/{action}/{id}",
            new { controller = "Receipt", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Account_Payment",
            "Payment/{action}/{id}",
            new { controller = "Payment", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Account_Report",
            "AccountReport/{action}/{id}",
            new { controller = "AccountReport", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Account_CustomerDiscount",
            "CustomerDiscount/{action}/{id}",
            new { controller = "CustomerDiscount", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Account_CustomerCommitment",
            "CustomerCommitment/{action}/{id}",
            new { controller = "CustomerCommitment", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Account_TransactionLiabilities",
            "TransactionLiabilities/{action}/{id}",
            new { controller = "TransactionLiabilities", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Account_MemberCard",
            "MemberCard/{action}/{id}",
            new { controller = "MemberCard", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Account_MemberCardDetail",
            "MemberCardDetail/{action}/{id}",
            new { controller = "MemberCardDetail", action = "Index", id = UrlParameter.Optional }
            );

            //<append_content_route_here>
            RegisterAutoMapperMap();
        }

        private static void RegisterAutoMapperMap()
        {
            AutoMapper.Mapper.CreateMap<Domain.Account.Entities.Contact, ContactViewModel>();
            AutoMapper.Mapper.CreateMap<ContactViewModel, Domain.Account.Entities.Contact>();
            AutoMapper.Mapper.CreateMap<CustomerViewModel, Domain.Account.Entities.Contact>();
            AutoMapper.Mapper.CreateMap<Domain.Account.Entities.Customer, CustomerViewModel>();
            AutoMapper.Mapper.CreateMap<CustomerViewModel, Domain.Account.Entities.Customer>();
            AutoMapper.Mapper.CreateMap<Domain.Account.Entities.vwContact, ContactViewModel>();
            AutoMapper.Mapper.CreateMap<ContactViewModel, Domain.Account.Entities.vwContact>();

            AutoMapper.Mapper.CreateMap<Domain.Account.Entities.vwCustomer, CustomerViewModel>();
            AutoMapper.Mapper.CreateMap<CustomerViewModel, Domain.Account.Entities.vwCustomer>();

            AutoMapper.Mapper.CreateMap<Domain.Account.Entities.ContractLease, ContractLeaseViewModel>();
            AutoMapper.Mapper.CreateMap<ContractLeaseViewModel, Domain.Account.Entities.ContractLease>();
            AutoMapper.Mapper.CreateMap<Domain.Account.Entities.vwContractLease, ContractLeaseViewModel>();
            AutoMapper.Mapper.CreateMap<ContractLeaseViewModel, Domain.Account.Entities.vwContractLease>();

            AutoMapper.Mapper.CreateMap<Domain.Account.Entities.InfoPartyA, InfoPartyAViewModel>();
            AutoMapper.Mapper.CreateMap<InfoPartyAViewModel, Domain.Account.Entities.InfoPartyA>();
            AutoMapper.Mapper.CreateMap<Domain.Account.Entities.vwInfoPartyA, InfoPartyAViewModel>();
            AutoMapper.Mapper.CreateMap<InfoPartyAViewModel, Domain.Account.Entities.vwInfoPartyA>();

            AutoMapper.Mapper.CreateMap<Domain.Account.Entities.ContractSell, ContractSellViewModel>();
            AutoMapper.Mapper.CreateMap<ContractSellViewModel, Domain.Account.Entities.ContractSell>();
            AutoMapper.Mapper.CreateMap<Domain.Account.Entities.vwContractSell, ContractSellViewModel>();
            AutoMapper.Mapper.CreateMap<ContractSellViewModel, Domain.Account.Entities.vwContractSell>();

            AutoMapper.Mapper.CreateMap<Domain.Account.Entities.Contract, ContractViewModel>();
            AutoMapper.Mapper.CreateMap<ContractViewModel, Domain.Account.Entities.Contract>();
            AutoMapper.Mapper.CreateMap<Domain.Account.Entities.vwContract, ContractViewModel>();
            AutoMapper.Mapper.CreateMap<ContractViewModel, Domain.Account.Entities.vwContract>();

            AutoMapper.Mapper.CreateMap<Domain.Account.Entities.ProcessPayment, ProcessPaymentViewModel>();
            AutoMapper.Mapper.CreateMap<ProcessPaymentViewModel, Domain.Account.Entities.ProcessPayment>();
            AutoMapper.Mapper.CreateMap<Domain.Account.Entities.Transaction, TransactionViewModel>();
            AutoMapper.Mapper.CreateMap<TransactionViewModel, Domain.Account.Entities.Transaction>();
            AutoMapper.Mapper.CreateMap<TransactionRelationshipViewModel, Domain.Account.Entities.TransactionRelationship>();
            AutoMapper.Mapper.CreateMap<Domain.Account.Entities.vwTransactionRelationship, TransactionRelationshipViewModel>();
            AutoMapper.Mapper.CreateMap<Domain.Account.Entities.vwTransactionLiabilities, TransactionLiabilitiesViewModel>();

            AutoMapper.Mapper.CreateMap<Domain.Account.Entities.vwLogContractbyCondos, LogContractbyCondosViewModel>();
            AutoMapper.Mapper.CreateMap<LogContractbyCondosViewModel, Domain.Account.Entities.vwLogContractbyCondos>();
            AutoMapper.Mapper.CreateMap<Domain.Account.Entities.Receipt, ReceiptViewModel>();
            AutoMapper.Mapper.CreateMap<ReceiptViewModel, Domain.Account.Entities.Receipt>();
            AutoMapper.Mapper.CreateMap<Domain.Account.Entities.vwReceipt, ReceiptViewModel>();
            AutoMapper.Mapper.CreateMap<ReceiptViewModel, Domain.Account.Entities.vwReceipt>();

            AutoMapper.Mapper.CreateMap<Domain.Account.Entities.Payment, PaymentViewModel>();
            AutoMapper.Mapper.CreateMap<PaymentViewModel, Domain.Account.Entities.Payment>();
            AutoMapper.Mapper.CreateMap<Domain.Account.Entities.vwPayment, PaymentViewModel>();
            AutoMapper.Mapper.CreateMap<PaymentViewModel, Domain.Account.Entities.vwPayment>();

            AutoMapper.Mapper.CreateMap<Domain.Account.Entities.Receipt, ResolveLiabilitiesViewModel>();
            AutoMapper.Mapper.CreateMap<ResolveLiabilitiesViewModel, Domain.Account.Entities.Receipt>();
            AutoMapper.Mapper.CreateMap<Domain.Account.Entities.Payment, ResolveLiabilitiesViewModel>();
            AutoMapper.Mapper.CreateMap<ResolveLiabilitiesViewModel, Domain.Account.Entities.Payment>();

            AutoMapper.Mapper.CreateMap<Domain.Account.Entities.CustomerDiscount, CustomerDiscountViewModel>();
            AutoMapper.Mapper.CreateMap<CustomerDiscountViewModel, Domain.Account.Entities.CustomerDiscount>();

            AutoMapper.Mapper.CreateMap<Domain.Account.Entities.CustomerCommitment, CustomerCommitmentViewModel>();
            AutoMapper.Mapper.CreateMap<CustomerCommitmentViewModel, Domain.Account.Entities.CustomerCommitment>();

            AutoMapper.Mapper.CreateMap<Domain.Account.Entities.Transaction, TransactionViewModel>();
            AutoMapper.Mapper.CreateMap<TransactionViewModel, Domain.Account.Entities.Transaction>();

            AutoMapper.Mapper.CreateMap<Domain.Account.Entities.MemberCard, MemberCardViewModel>();
            AutoMapper.Mapper.CreateMap<MemberCardViewModel, Domain.Account.Entities.MemberCard>();
            AutoMapper.Mapper.CreateMap<Domain.Account.Entities.vwMemberCard, MemberCardViewModel>();
            AutoMapper.Mapper.CreateMap<MemberCardViewModel, Domain.Account.Entities.vwMemberCard>();
            AutoMapper.Mapper.CreateMap<Domain.Account.Entities.MemberCardDetail, MemberCardDetailViewModel>();
            AutoMapper.Mapper.CreateMap<MemberCardDetailViewModel, Domain.Account.Entities.MemberCardDetail>();
            AutoMapper.Mapper.CreateMap<Domain.Account.Entities.vwCustomer, CustomerNTViewModel>();
            AutoMapper.Mapper.CreateMap<CustomerNTViewModel, Domain.Account.Entities.vwCustomer>();
            AutoMapper.Mapper.CreateMap<Domain.Account.Entities.Customer, CustomerNTViewModel>();
            AutoMapper.Mapper.CreateMap<CustomerNTViewModel, Domain.Account.Entities.Customer>();
            //<append_content_mapper_here>
        }
    }
}
