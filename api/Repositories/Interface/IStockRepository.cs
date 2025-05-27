using api.Dtos;

namespace api.Repositories.Interface;

public interface IStockRepository
{
    Task<List<StockDto>> GetAllAsync();
}
