using Dapper;
using MySqlConnector;

namespace ZGmarket.Models.Repository;

public class SupplyRepository
{
    private readonly MySqlConnection _context;
    public SupplyRepository(MySqlConnection context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Supply>> GetSupply()
    {
        var query = $@"SELECT id as {nameof(Models.Supply.Id)}, 
                            nom_id as {nameof(Models.Supply.NomId)}, 
                            stock_id as {nameof(Models.Supply.StockId)},
                            emp_id as {nameof(Models.Supply.EmpId)},
                            quantity as {nameof(Models.Supply.Quantity)},
                            delivery as {nameof(Models.Supply.Delivery)}
                            FROM supply";

        var Supply = await _context.QueryAsync<Supply>(query);
        return Supply.ToList();
    }

    public async Task<IEnumerable<Supply>> GetSupply(int stockId)
    {
        var query = $@"SELECT id as {nameof(Models.Supply.Id)}, 
                            nom_id as {nameof(Models.Supply.NomId)}, 
                            stock_id as {nameof(Models.Supply.StockId)},
                            emp_id as {nameof(Models.Supply.EmpId)},
                            quantity as {nameof(Models.Supply.Quantity)},
                            delivery as {nameof(Models.Supply.Delivery)}
                            FROM supply
                            WHERE stock_id = {stockId}";

        var supplys = await _context.QueryAsync<Supply>(query);
        if (supplys == null)
        {
            throw new Exception($"stock ({stockId}) not found");
        }

        return supplys;
    }


    public async Task<IEnumerable<Supply>> AddSupply(Supply model)
    {
        string date = $"{model.Delivery.Year}-{model.Delivery.Month}-{model.Delivery.Day}";
        var query = $@"INSERT INTO `zgmarket`.`supply` (`nom_id`, `stock_id`, `emp_id`, `quantity`, `delivery`) 
                    VALUES ('{model.NomId}', '{model.StockId}', '{model.EmpId}', '{model.Quantity}', '{date}')";

        try
        {
            await _context.QueryAsync<Supply>(query);

            var addStockNom = $@"INSERT INTO `zgmarket`.`nom_stock` ( `stock_id`, `nom_id`, `quantity`) 
                    VALUES ('{model.StockId}', '{model.NomId}', '{model.Quantity}')";

            await _context.QueryAsync<NomStock>(addStockNom);

            var resdb = await GetSupply(model.StockId);
            return resdb;
        }
        catch (Exception ex)
        {
            throw new Exception("Ошибка при добавлении новой поставки " + ex.Message);
        }
    }
}



