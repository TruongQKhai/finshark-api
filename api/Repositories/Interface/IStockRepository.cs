using api.Dtos.Stocks;
using api.Models;

namespace api.Repositories.Interface;

public interface IStockRepository
{
    Task<List<Stock>> GetAllAsync();
    Task<Stock?> GetByIdAsync(int id);
    Task<Stock> CreateAsync(Stock stock);
    Task<Stock> UpdateAsync(int id, UpdateStockRequestDto req);
    Task<Stock> DeleteAsync(int id);

    Task<bool> StockExists(int id);
}
