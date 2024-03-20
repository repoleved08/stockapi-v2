using System.Diagnostics.CodeAnalysis;
using Api.Dtos.Stock;
using Api.Helpers;
using Api.Interface;
using Api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController(IStockInterface stockInterface) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stocks = await stockInterface.GetAllAsync(query);
            var stockDto = stocks.Select(x => x.ToStockDto()).ToList();
            return Ok(stockDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await stockInterface.GetByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [SuppressMessage("ReSharper.DPA", "DPA0011: High execution time of MVC action", MessageId = "time: 2655ms")]
        public async Task<IActionResult> Create([FromBody] CreateRequestDto stRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var request = stRequestDto.ToStockFromCreateRequestDto();
            var stockItem = await stockInterface.CreateStockAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = stockItem.Id }, stockItem.ToStockDto());
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateStockRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockItem = await stockInterface.UpdateStockAsync(id, updateStockRequestDto);
            if (stockItem == null)
            {
                return NotFound();
            }

            return Ok(stockItem.ToStockDto());
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteStock([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockItem = await stockInterface.DeleteStockAsync(id);
            return Ok(stockItem);
        }
    }
}
