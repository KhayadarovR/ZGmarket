using Dapper;
using MySqlConnector;

namespace ZGmarket.Models.Repository;

public class NomTypeRepository
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

    public async Task<NomType> GetNomType(string title)
    {
        var query = $"SELECT * FROM n_type where (title = '{title}')";

        var type = await _context.QuerySingleAsync<NomType>(query);
        return type;
    }

    public async Task<NomType> AddNomType(NomType model)
    {
        var query = $"INSERT INTO `zgmarket`.`n_type` (`title`) " +
            $"VALUES ('{model.Title}');";

        try
        {
            await _context.QueryAsync<NomType>(query);
            var res = await GetNomType(model.Title);
            return res;
        }
        catch (Exception ex)
        {
            throw new Exception("Ошибка при добавлении новой категории " + ex.Message);
        }
    }

    public async Task DeleteNomType(int id)
    {
        var query = $"DELETE FROM `zgmarket`.`n_type` WHERE (`id` = '{id}');";

        try
        {
            await _context.QueryAsync<NomType>(query);
        }
        catch (Exception ex)
        {
            throw new Exception("Ошибка при удалении " + ex.Message);
        }
    }
}



