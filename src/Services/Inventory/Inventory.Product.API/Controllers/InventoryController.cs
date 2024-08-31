using Inventory.Product.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.Inventory;
using Shared.SeedWord;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Inventory.Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        /// <summary>
        /// api/inventory/items
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("items/{itemNo}", Name = "GetAllByItemNo")]
        [ProducesResponseType(typeof(IEnumerable<InventoryEntryDto>), 200)]
        public async Task<IActionResult> GetAllByItemNo([Required] string itemNo)
        {
            var result = await _inventoryService.GetAllByItemNoAsync(itemNo);

            return Ok(result);
        }

        [Route("items/{itemNo}/paging", Name = "GetAllByItemNoPaging")]
        [HttpGet]
        [ProducesResponseType(typeof(PageList<InventoryEntryDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PageList<InventoryEntryDto>>> GetAllByItemNoPaging([Required] string itemNo, [FromQuery] GetInventoryPagingQuery query)
        {
            query.SetItemNo(itemNo);
            var result = await _inventoryService.GetAllByItemNoPagingAsync(query);
            return Ok(result);
        }

        [Route("{id}", Name = "GetInventoryById")]
        [HttpGet]
        [ProducesResponseType(typeof(InventoryEntryDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<InventoryEntryDto>> GetById([Required] string id)
        {
            var result = await _inventoryService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost("purchase/{itemNo}", Name = "PurchaseOrder")]
        [ProducesResponseType(typeof(InventoryEntryDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<InventoryEntryDto>> PurchaseOrder([Required] string itemNo,
            [FromBody] PurchaseProductDto model)
        {
            model.SetItemNo(itemNo);
            var result = await _inventoryService.PurchaseItemAsync(itemNo, model);
            return Ok(result);
        }

        [HttpPost("sales/{itemNo}", Name = "SalesItem")]
        [ProducesResponseType(typeof(InventoryEntryDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<InventoryEntryDto>> SalesItem([Required] string itemNo,
            [FromBody] SalesProductDto model)
        {
            model.SetItemNo(itemNo);
            var result = await _inventoryService.SalesItemAsync(itemNo, model);
            return Ok(result);
        }

        [HttpDelete("{id}", Name = "DeleteById")]
        [ProducesResponseType(typeof(InventoryEntryDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<InventoryEntryDto>> PurchaseOrder([Required] string id)
        {
            var inventory = await _inventoryService.GetByIdAsync(id);
            if (inventory == null)
                return NotFound();

            await _inventoryService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("sales/order-no/{orderNo}", Name = "SalesOrder")]
        [ProducesResponseType(typeof(CreatedOrderSuccessDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreatedOrderSuccessDto>> SalesOrder([Required] string orderNo,
           [FromBody] SalesOrderDto model)
        {
            model.OrderNo = orderNo;
            var documentNo = await _inventoryService.SalesOrderAsync(model);
            var result = new CreatedOrderSuccessDto(documentNo);
            return Ok(result);
        }
    }
}