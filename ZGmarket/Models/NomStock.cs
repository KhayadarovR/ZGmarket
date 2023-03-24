using Microsoft.Build.Framework;

namespace ZGmarket.Models;

public class NomStock
{

    public int NomId { get; set; }

    [Required]
    public int StockId { get; set; }

    [Required]
    public int Quantity { get; set; }

}
