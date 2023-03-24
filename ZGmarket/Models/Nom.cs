using Microsoft.Build.Framework;

namespace ZGmarket.Models;

public class Nom
{
    
    public int Id { get; set; }
    
    public int NomTypeId { get; set; }

    [Required]
    public string Title { get; set; }
    
    public int ShelfLife { get; set; }

    [Required]
    public int Price { get; set; }
}
