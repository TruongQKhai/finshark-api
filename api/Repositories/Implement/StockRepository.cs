using api.Database;
using api.Dtos;
using api.Mappers;
using api.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories.Implement;

public class StockRepository : IStockRepository
{
    private readonly AppDbContext _context;

    public StockRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<StockDto>> GetAllAsync()
    {
        return await _context.Stocks
            .Select(s => s.ToDto())
            .ToListAsync();
    }
}
