using Dapper;
using MySqlConnector;
using ZGmarket.Data;
using ZGmarket.Models;

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

    public async Task<Supply> GetSupply(int id)
    {
        var query = $@"SELECT id as {nameof(Models.Supply.Id)}, 
                            nom_id as {nameof(Models.Supply.NomId)}, 
                            stock_id as {nameof(Models.Supply.StockId)},
                            emp_id as {nameof(Models.Supply.EmpId)},
                            quantity as {nameof(Models.Supply.Quantity)},
                            delivery as {nameof(Models.Supply.Delivery)}
                            FROM supply";

        var Supply =  _context.QueryFirst<Supply>(query);
        if (Supply == null)
        {
            throw new Exception($"id({id}) not found");
        }

        return Supply;
    }
  

    public async Task<NomStock> AddSupply(Supply model)
    {

        var query = $@"INSERT INTO `zgmarket`.`supply` (`nom_id`, `stock_id`, `emp_id`, `quantity`, `delivery`) 
                    VALUES ('{model.NomId}', '{model.StockId}', '{model.EmpId}', '{model.Quantity}', '{model.Delivery}');
                    INSERT INTO `zgmarket`.`nom_stock` ( `stock_id`, `nom_id`, `quantity`) 
                    VALUES ('{model.StockId}', '{model.NomId}', '{model.Quantity}')";

        try
        {
            await _context.QueryAsync<Supply>(query);
            var resdb = new NomStock() { NomId = model.NomId, StockId= model.StockId, Quantity = model.Quantity };
            return resdb;
        }
        catch (Exception ex)
        {
            throw new Exception("Ошибка при добавлении нового товара " + ex.Message);
        }
    }
}



