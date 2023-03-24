using Dapper;
using MySqlConnector;
using ZGmarket.Data;
using ZGmarket.Models;

namespace ZGmarket.Models.Repository;

public class NomRepository
{
    private readonly MySqlConnection _context;
    public NomRepository(MySqlConnection context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Nom>> GetNom()
    {
        var query = $@"SELECT id as {nameof(Models.Nom.Id)}, 
                            type_id as {nameof(Models.Nom.NomTypeId)}, 
                            title as {nameof(Models.Nom.Title)},
                            expiration as {nameof(Models.Nom.ShelfLife)},
                            price as {nameof(Models.Nom.Price)}
                            FROM `zgmarket`.`nom`;";

        var Nom = await _context.QueryAsync<Nom>(query);
        return Nom.ToList();
    }

    public async Task<Nom> GetNom(int id)
    {
        var query = $@"SELECT id as {nameof(Models.Nom.Id)}, 
                            type_id as {nameof(Models.Nom.NomTypeId)}, 
                            title as {nameof(Models.Nom.Title)},
                            expiration as {nameof(Models.Nom.ShelfLife)},
                            price as {nameof(Models.Nom.Price)}
                            FROM `zgmarket`.`nom`";

        var Nom =  _context.QueryFirst<Nom>(query);
        if (Nom == null)
        {
            throw new Exception($"id({id}) not found");
        }

        return Nom;
    }
    public async Task<Nom> GetNom(string title)
    {
        var query = $@"SELECT id as {nameof(Models.Nom.Id)}, 
                            type_id as {nameof(Models.Nom.NomTypeId)}, 
                            title as {nameof(Models.Nom.Title)},
                            expiration as {nameof(Models.Nom.ShelfLife)},
                            price as {nameof(Models.Nom.Price)}
                            FROM `zgmarket`.`nom`";

        Nom Nom = await _context.QueryFirstOrDefaultAsync<Nom>(query);
        if (Nom == null)
        {
            throw new Exception($"product ({title}) not found");
        }

        return Nom;
    }

    public async Task<Nom> AddNom(Nom model)
    {

        var query = $@"INSERT INTO `zgmarket`.`nom` (`type_id`, `title`, `expiration`, `price`) 
                    VALUES ('{model.NomTypeId}', '{model.Title}', '{model.ShelfLife}', '{model.Price}');";

        try
        {
            await _context.QueryAsync<Nom>(query);
            var resdb = await GetNom(model.Title);
            return resdb;
        }
        catch (Exception ex)
        {
            throw new Exception("Ошибка при добавлении нового товара " + ex.Message);
        }
    }

    public async Task<Nom> EditNom(int id, Nom model)
    {

        var query = $@"UPDATE `zgmarket`.`nom` 
                    SET `type_id` = '{model.NomTypeId}', `title` = '{model.Title}', `expiration` = '{model.ShelfLife}', `price` = '{model.Price}' 
                    WHERE (`id` = '{id}');";

        try
        {
            await _context.QueryAsync<Nom>(query);
            return await GetNom(id);
        }
        catch (Exception ex)
        {
            throw new Exception("Ошибка при изминении данных товара " + ex.Message);
        }
    }

    public async Task DeleteNom(int id)
    {
        var query = $"DELETE FROM `zgmarket`.`nom` WHERE (`id` = '{id}');";

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



