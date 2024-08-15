using Contracts.Commons.Interfaces;
using Customer.API.Persistence;
using Customer.API.Repositories.Interfaces;
using Customer.API.Services.Interfaces;
using Shared.Dtos.Customer;

namespace Customer.API.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork<CustomerContext> _unitOfWork;
        public CustomerService(ICustomerRepository customerRepository, IUnitOfWork<CustomerContext> unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateCustomer(Entities.Customer entity)
        {
           await  _customerRepository.CreateAsync(entity);
           await _unitOfWork.SaveChangeAsync();
        }

        public async Task<IEnumerable<Entities.Customer>> GetAllCustomerAsync()
        {
           return await _customerRepository.GetCustomerAllAsync();
        }

        public async Task<IResult> GetCustomerByUserNameAsync(string userName)
        {
            var result = await _customerRepository.GetByCondition(x => x.UserName == userName);
            return Results.Ok(result);
        }
    }
}