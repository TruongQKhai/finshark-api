﻿using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;

[Table("Stocks")]
public class Stock
{
    public int Id { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    [Column(TypeName = "Decimal(18, 2)")]
    public decimal Purchase { get; set; }
    [Column(TypeName = "Decimal(18, 2)")]
    public decimal LastDiv { get; set; }
    public string Industry { get; set; } = string.Empty;
    public long MarketCap { get; set; }

    public List<Comment> Comments { get; set; } = new List<Comment>();
    public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
}
