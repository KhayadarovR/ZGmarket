namespace ZGmarket.Models.Contracts
{
    public interface INomTypeRepository
    {
        public Task<IEnumerable<NomType>> GetTypes();
    }
}
