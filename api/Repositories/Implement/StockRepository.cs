using api.Database;
using api.Dtos.Stocks;
using api.Mappers;
using api.Models;
using api.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;

namespace api.Repositories.Implement;

public class StockRepository : IStockRepository
{
    private readonly AppDbContext _context;

    public StockRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Stock>> GetAllAsync()
    {
        return await _context.Stocks.Include(s => s.Comments).ToListAsync();
    }

    public async Task<Stock> CreateAsync(Stock stock)
    {
        // Validation

        await _context.Stocks.AddAsync(stock);
        await _context.SaveChangesAsync();
        return stock;
    }

    public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto req)
    {
        // Validate

        var existingStock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
        if (existingStock is null)
            return null;

        existingStock.Symbol = req.Symbol;
        existingStock.Company = req.Company;
        existingStock.Purchase = req.Purchase;
        existingStock.LastDiv = req.LastDiv;
        existingStock.Industry = req.Industry;
        existingStock.MarketCap = req.MarketCap;

        await _context.SaveChangesAsync();

        return existingStock;
    }

    public async Task<Stock?> DeleteAsync(int id)
    {
        var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);

        if (stock is null)
            return null;

        _context.Stocks.Remove(stock);
        await _context.SaveChangesAsync();

        return stock;
    }

    public async Task<Stock?> GetByIdAsync(int id)
    {
        return await _context.Stocks
            .Include(s => s.Comments)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<bool> StockExists(int id)
    {
        return await _context.Stocks.AnyAsync(s => s.Id == id);
    }
}


