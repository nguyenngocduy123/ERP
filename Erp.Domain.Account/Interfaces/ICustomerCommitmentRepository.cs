using Erp.Domain.Account.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Account.Interfaces
{
    public interface ICustomerCommitmentRepository
    {
        
        IQueryable<CustomerCommitment> GetAllCustomerCommitment();

        CustomerCommitment GetCustomerCommitmentById(int Id);

        void InsertCustomerCommitment(CustomerCommitment CustomerCommitment);

        void DeleteCustomerCommitment(int Id);

        void DeleteCustomerCommitmentRs(int Id);

        void UpdateCustomerCommitment(CustomerCommitment CustomerCommitment);
    }
}
