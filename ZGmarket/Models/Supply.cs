using Microsoft.Build.Framework;

namespace ZGmarket.Models;

public class Supply
{
    
    public int Id { get; set; }

    public int NomId { get; set; }

    [Required]
    public int StockId { get; set; }

    public int EmpId { get; set; }

    [Required]
    public int Quantity { get; set; }
    
    [Required]
    public DateOnly Delivery { get; set; }
}
