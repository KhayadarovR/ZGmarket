using Dapper;
using MySqlConnector;

namespace ZGmarket.Models.Repository;

public class StockRepository
{
    private readonly MySqlConnection _context;
    private readonly StockRepository _StockTypeRepo;
    public StockRepository(MySqlConnection context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Stock>> GetStock()
    {
        var query = $@"SELECT stock.id as {nameof(Models.Stock.Id)}, 
                            title as {nameof(Models.Stock.Title)},
                            address as {nameof(Models.Stock.Address)},
                            description as {nameof(Models.Stock.Description)}
                            from stock";

        var Stocks = await _context.QueryAsync<Stock>(query);

        return Stocks.ToList();
    }

    public async Task<Stock> GetStock(int id)
    {
        var query = $@"SELECT stock.id as {nameof(Models.Stock.Id)}, 
                            title as {nameof(Models.Stock.Title)},
                            address as {nameof(Models.Stock.Address)},
                            description as {nameof(Models.Stock.Description)}
                            from stock where `id` = {id}";

        var Stock = _context.QueryFirst<Stock>(query);
        if (Stock == null)
        {
            throw new Exception($"id({id}) not found");
        }

        return Stock;
    }
    public async Task<Stock> GetStock(string title)
    {
        var query = $@"SELECT id as {nameof(Models.Stock.Id)}, 
                            title as {nameof(Models.Stock.Title)},
                            address as {nameof(Models.Stock.Address)},
                            description as {nameof(Models.Stock.Description)}
                            from stock
                            where title = '{title}'";

        Stock _st = await _context.QueryFirstOrDefaultAsync<Stock>(query);
        if (_st == null)
        {
            throw new Exception($"stock ({title}) not found");
        }

        return _st;
    }

    public async Task<Stock> AddStock(Stock model)
    {

        var query = $@"INSERT INTO zgmarket.stock (`title`, `address`, `description`) 
                    VALUES ('{model.Title}', '{model.Address}', '{model.Description}');";

        try
        {
            await _context.QueryAsync<Stock>(query);
            var resdb = await GetStock(model.Title);
            return resdb;
        }
        catch (Exception ex)
        {
            throw new Exception("Ошибка при добавлении нового склада " + ex.Message);
        }
    }

    public async Task<Stock> EditStock(int id, Stock model)
    {

        var query = $@"UPDATE `zgmarket`.`stock` 
                    SET `address` = '{model.Address}', `title` = '{model.Title}', `description` = '{model.Description}' 
                    WHERE (`id` = '{id}');";

        try
        {
            await _context.QueryAsync<Stock>(query);
            return await GetStock(id);
        }
        catch (Exception ex)
        {
            throw new Exception("Ошибка при изминении данных склада " + ex.Message);
        }
    }

    public async Task DeleteStock(int id)
    {
        var query = $"DELETE FROM `zgmarket`.`stock` WHERE (`id` = '{id}');";

        try
        {
            await _context.QueryAsync<Stock>(query);
        }
        catch (Exception ex)
        {
            throw new Exception("Ошибка при удалении " + ex.Message);
        }
    }
}



