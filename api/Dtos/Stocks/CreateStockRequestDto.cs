﻿using System.ComponentModel.DataAnnotations.Schema;

namespace api.Dtos.Stocks;

public class CreateStockRequestDto
{
    public string Symbol { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public decimal Purchase { get; set; }
    public decimal LastDiv { get; set; }
    public string Industry { get; set; } = string.Empty;
    public long MarketCap { get; set; }
}
