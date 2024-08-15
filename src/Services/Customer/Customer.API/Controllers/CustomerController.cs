using AutoMapper;
using Customer.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.Customer;

namespace Customer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        private IMapper _mapper;

        public CustomerController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetCustomers()
        {
            var result = _mapper.Map<IEnumerable<CustomerDto>>(await _customerService.GetAllCustomerAsync());
            return Ok(result);
        }

        [HttpPost("createCustomer")]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDto createCustomerDto)
        {
            var entity = _mapper.Map<Entities.Customer>(createCustomerDto);
            await _customerService.CreateCustomer(entity);
            return Ok(entity);
        }
    }
}