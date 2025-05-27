using api.Database;
using api.Dtos;
using api.Mappers;
using api.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Identity.Client;

namespace api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StocksController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IStockRepository _stockRepository;

    public StocksController(AppDbContext context, IStockRepository stockRepository)
    {
        _context = context;
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
        var stock = await _context.Stocks.FindAsync(id);

        if (stock is null)
            return NotFound();

        return Ok(stock.ToDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockRequestDto req)
    {
        var stockModel = req.ToEntity();
        
        await _context.Stocks.AddAsync(stockModel);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id}, stockModel.ToDto());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, UpdateStockRequestDto req)
    {
        var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
        if (stock is null)
            return NotFound();

        stock.Symbol = req.Symbol;
        stock.Company = req.Company;
        stock.Purchase = req.Purchase;
        stock.LastDiv = req.LastDiv;
        stock.Industry = req.Industry;
        stock.MarketCap = req.MarketCap;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
        
        if (stock is null) 
            return NotFound();

        await _context.SaveChangesAsync();

        return NoContent();
    }
}
