using Contracts.Commons.Interfaces;
using Customer.API.Persistence;

namespace Customer.API.Repositories.Interfaces
{
    public interface ICustomerRepository : IRepositoryBaseAsync<Entities.Customer,int,CustomerContext>
    {
        public Task<IEnumerable<Entities.Customer>> GetCustomerAllAsync();
    }
}
