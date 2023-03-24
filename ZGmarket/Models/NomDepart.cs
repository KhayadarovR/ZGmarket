using Microsoft.Build.Framework;

namespace ZGmarket.Models;

public class NomDepart
{

    public int NomId { get; set; }

    [Required]
    public string Department { get; set; }

    [Required]
    public int Quantity { get; set; }

}
