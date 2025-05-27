using api.Dtos;
using api.Models;

namespace api.Mappers;

public static class StockMappers
{
    public static StockDto ToDto(this Stock stock)
    {
        return new StockDto
        { 
            Id = stock.Id,
            Symbol = stock.Symbol,
            Company = stock.Company,
            Purchase = stock.Purchase,
            LastDiv = stock.LastDiv,
            Industry = stock.Industry,
            MarketCap = stock.MarketCap
        };
    }

    public static Stock ToEntity(this CreateStockRequestDto request)
    {
        return new Stock
        {
            Symbol = request.Symbol,
            Company = request.Company,
            Purchase = request.Purchase,
            LastDiv = request.LastDiv,
            Industry = request.Industry,
            MarketCap = request.MarketCap
        };
    }
}
