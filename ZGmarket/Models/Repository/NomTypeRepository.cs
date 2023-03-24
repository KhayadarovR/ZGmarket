using Dapper;
using MySqlConnector;
using System.Configuration;
using System.Data.Common;
using ZGmarket.Models.Contracts;

namespace ZGmarket.Models.Repository;

public class NomTypeRepository: INomTypeRepository
{
    private readonly MySqlConnection _context;
    public NomTypeRepository(MySqlConnection context)
    {
        _context = context;
    }

    public async Task<IEnumerable<NomType>> GetTypes()
    {
        var query = "SELECT * FROM n_type";

        var types = await _context.QueryAsync<NomType>(query);
        return types.ToList();
    }

}



