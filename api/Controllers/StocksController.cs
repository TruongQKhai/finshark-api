using api.Dtos.Stocks;
using api.Mappers;
using api.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/stocks")]
public class StocksController : ControllerBase
{
    private readonly IStockRepository _stockRepository;

    public StocksController(IStockRepository stockRepository)
    {
        _stockRepository = stockRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _stockRepository.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var stock = await _stockRepository.GetByIdAsync(id);

        if (stock is null)
            return NotFound();

        return Ok(stock.ToDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockRequestDto req)
    {
        var stock = req.ToEntity();
        await _stockRepository.CreateAsync(stock);

        return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToDto());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, UpdateStockRequestDto req)
    {
        var stock = await _stockRepository.UpdateAsync(id, req);
        if (stock is null)
            return NotFound();

        return Ok(stock.ToDto());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var stock = await _stockRepository.DeleteAsync(id);
        if (stock is null)
            return NotFound();

        return NoContent();
    }
}
