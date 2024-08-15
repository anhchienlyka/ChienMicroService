using Shared.Dtos.Customer;

namespace Customer.API.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IResult> GetCustomerByUserNameAsync(string userName);

        Task<IEnumerable<Entities.Customer>> GetAllCustomerAsync();

        Task CreateCustomer(Entities.Customer entity);
    }
}
