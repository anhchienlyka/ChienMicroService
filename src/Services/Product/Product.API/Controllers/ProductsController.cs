using AutoMapper;
using Contracts.Commons.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Product.API.Entities;
using Product.API.Persistence;
using Product.API.Repositories.Interfaces;
using Shared.DTOs.Product;
using System.ComponentModel.DataAnnotations;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<ProductContext> _unitOfWork;

        public ProductsController(IProductRepository productRepository, IMapper mapper, IUnitOfWork<ProductContext> unitOfWork)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var listProduct = await _productRepository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<ProductDto>>(listProduct);
            return Ok(result);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetProduct([Required] long id)
        {
            var product = await _productRepository.GetById(id);

            if (product == null) return NotFound();

            var result = _mapper.Map<ProductDto>(product);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([Required] CreateProductDto createProduct)
        {
            var product = _mapper.Map<CatalogProduct>(createProduct);

            var result = await _productRepository.CreateAsync(product);
            await _unitOfWork.SaveChangeAsync();
            return Ok(result);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateProduct([Required] long id, UpdateProductDto updateProduct)
        {
            var isExist = await _productRepository.IsExist(id);
            if (!isExist)
            {
                return NotFound();
            }
            var product = await _productRepository.GetById(id);

            _mapper.Map(product, updateProduct);

            await _productRepository.UpdateAsync(product);
            await _unitOfWork.SaveChangeAsync();

            return Ok(_mapper.Map<ProductDto>(product));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct([Required] long id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            await _productRepository.DeleteAsync(product);
            await _unitOfWork.SaveChangeAsync();
            return NoContent();
        }

        [HttpGet("get-product-by-no/{productNo}")]
        public async Task<IActionResult> GetProductByNo([Required] string productNo)
        {
            var product = await _productRepository.GetByCondition(x => x.No == productNo);
            if (product == null) { return NotFound(); }
            var result = _mapper.Map<ProductDto>(product);
            return Ok(result);
        }
    }
}