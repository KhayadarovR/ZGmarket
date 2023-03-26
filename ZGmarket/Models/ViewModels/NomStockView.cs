using System.ComponentModel.DataAnnotations;

namespace ZGmarket.Models.ViewModels
{
    public class NomStockView
    {
        public NomStock NomStok { get; set; }

        [Required]
        public Stock Stock { get; set; }

        [Required]
        public Nom Nom { get; set; }

        [Required]
        public int Quantity { get; set; }
        [Required]
        public string Depart { get; set; }
    }
}
