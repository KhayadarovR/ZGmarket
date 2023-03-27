using Dapper;
using MySqlConnector;

namespace ZGmarket.Models.Repository;

public class NomStockRepository
{
    private readonly MySqlConnection _context;
    public NomStockRepository(MySqlConnection context)
    {
        _context = context;
    }

    public async Task<IEnumerable<NomStock>> GetNomStock()
    {
        var query = $@"SELECT stock_id as {nameof(Models.NomStock.StockId)}, 
                            nom_id as {nameof(Models.NomStock.NomId)}, 
                            quantity as {nameof(Models.NomStock.Quantity)},
                            id as {nameof(Models.NomStock.Id)}
                            FROM nom_stock
                            WHERE depart is null";

        var NomStock = await _context.QueryAsync<NomStock>(query);
        return NomStock.ToList();
    }

    public async Task<NomStock> GetNomStock(int id)
    {
        var query = $@"SELECT stock_id as {nameof(Models.NomStock.StockId)}, 
                            nom_id as {nameof(Models.NomStock.NomId)}, 
                            quantity as {nameof(Models.NomStock.Quantity)},
                            id as {nameof(Models.NomStock.Id)},
                            depart as {nameof(NomStock.Depart)}
                            FROM nom_stock
                            WHERE id = {id}";

        var nomStock = await _context.QueryFirstAsync<NomStock>(query);
        if (nomStock == null)
        {
            throw new Exception($"id stock ({id}) not found");
        }

        return nomStock;
    }

    public async Task<NomStock> GetOneNomStock(int id)
    {
        var query = $@"SELECT stock_id as {nameof(Models.NomStock.StockId)}, 
                            nom_id as {nameof(Models.NomStock.NomId)}, 
                            quantity as {nameof(Models.NomStock.Quantity)},
                            id as {nameof(Models.NomStock.Id)}
                            FROM nom_stock
                            WHERE id = {id}";

        var NomStock = await _context.QuerySingleAsync<NomStock>(query);
        if (NomStock == null)
        {
            throw new Exception($"id ({id}) not found");
        }

        return NomStock;
    }

    public async Task<IEnumerable<NomStock>> GetNomDepart()
    {
        var query = $@"SELECT stock_id as {nameof(Models.NomStock.StockId)}, 
                            nom_id as {nameof(Models.NomStock.NomId)}, 
                            quantity as {nameof(Models.NomStock.Quantity)},
                            id as {nameof(Models.NomStock.Id)},
                            depart as {nameof(Models.NomStock.Depart)}
                            FROM nom_stock
                            WHERE depart is not null";

        var noms = await _context.QueryAsync<NomStock>(query);
        if (noms == null)
        {
            throw new Exception($"products not found");
        }

        return noms;
    }

    public async Task<IEnumerable<NomStock>> GetNomDepart(string departName)
    {
        var query = $@"SELECT stock_id as {nameof(Models.NomStock.StockId)}, 
                            nom_id as {nameof(Models.NomStock.NomId)}, 
                            quantity as {nameof(Models.NomStock.Quantity)},
                            id as {nameof(Models.NomStock.Id)}
                            FROM nom_stock
                            WHERE depart = '{departName}'";

        var NomStock = _context.QueryAsync<NomStock>(query);
        if (NomStock == null)
        {
            throw new Exception($"products not found");
        }

        return (IEnumerable<NomStock>)NomStock;
    }

    public async Task<NomStock> SendNomToDepart(int id, NomStock nomStock)
    {

        var queryChangeLoction = $@"UPDATE `zgmarket`.`nom_stock` SET `depart` = '{nomStock.Depart}' 
                                where id = {id}";


        try
        {
            await _context.QueryAsync<Emp>(queryChangeLoction);
            return await GetOneNomStock(id);
        }
        catch (Exception ex)
        {
            throw new Exception("Ошибка заполнения данных " + ex.Message);
        }
    }

    public async Task DeleteNomStock(int id)
    {
        var query = $"DELETE FROM `zgmarket`.`nom_stock` WHERE (`id` = '{id}');";

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



