namespace ZGmarket.Models.ViewModels
{
    public class SupplyCreateView
    {
        public IEnumerable<Nom> Noms { get; set; }

        public IEnumerable<Emp> Emps { get; set; }

        public IEnumerable<Stock> Stocks { get; set; }

        public int Quantity { get; set; }

        public DateTime Delivery { get; set; }

        public Supply NewSupply { get; set; }

    }
}
