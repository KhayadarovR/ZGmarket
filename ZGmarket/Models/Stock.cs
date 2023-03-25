using Microsoft.Build.Framework;

namespace ZGmarket.Models
{
    public class Stock
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string? Address { get; set; }

        public string? Description { get; set; }
    }
}
