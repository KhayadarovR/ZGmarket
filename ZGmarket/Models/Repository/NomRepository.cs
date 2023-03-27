using Dapper;
using MySqlConnector;

namespace ZGmarket.Models.Repository;

public class NomRepository
{
    private readonly MySqlConnection _context;
    private readonly NomTypeRepository _nomTypeRepo;
    public NomRepository(MySqlConnection context, NomTypeRepository nomTypeRepo)
    {
        _context = context;
        _nomTypeRepo = nomTypeRepo;
    }

    public async Task<IEnumerable<Nom>> GetNom()
    {
        var query = $@"SELECT nom.id as {nameof(Models.Nom.Id)}, 
                            n_type.title as {nameof(Models.Nom.NType)},
                            nom.title as {nameof(Models.Nom.Title)},
                            shelf_life as {nameof(Models.Nom.ShelfLife)},
                            price as {nameof(Models.Nom.Price)}
                            from nom
                            left join n_type
                            on(nom.type_id = n_type.id)";

        var Noms = await _context.QueryAsync<Nom>(query);

        return Noms.ToList();
    }

    public async Task<Nom> GetNom(int id)
    {
        var query = $@"SELECT nom.id as {nameof(Models.Nom.Id)}, 
                            n_type.title as {nameof(Models.Nom.NType)},
                            nom.title as {nameof(Models.Nom.Title)},
                            nom.shelf_life as {nameof(Models.Nom.ShelfLife)},
                            nom.price as {nameof(Models.Nom.Price)},
                            nom.type_id as {nameof(Nom.TypeId)}
                            from nom
                            left join n_type
                            on(nom.type_id = n_type.id)
                            where nom.id = '{id}'";

        var nombd = await _context.QueryFirstAsync<Nom>(query);
        if (nombd == null)
        {
            throw new Exception($"id({id}) not found");
        }

        return nombd;
    }
    public async Task<Nom> GetNom(string title)
    {
        var query = $@"SELECT nom.id as {nameof(Models.Nom.Id)}, 
                            n_type.title as {nameof(Models.Nom.NType)},
                            nom.title as {nameof(Models.Nom.Title)},
                            shelf_life as {nameof(Models.Nom.ShelfLife)},
                            price as {nameof(Models.Nom.Price)}
                            from nom
                            left join n_type
                            on(nom.type_id = n_type.id)
                            where nom.title = '{title}'";

        Nom Nom = await _context.QueryFirstOrDefaultAsync<Nom>(query);
        if (Nom == null)
        {
            throw new Exception($"product ({title}) not found");
        }

        return Nom;
    }

    public async Task<Nom> AddNom(Nom model)
    {

        var query = $@"INSERT INTO `zgmarket`.`nom` (`type_id`, `title`, `shelf_life`, `price`) 
                    VALUES ('{model.TypeId}', '{model.Title}', '{model.ShelfLife}', '{model.Price}');";

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
                    SET `type_id` = '{model.TypeId}', `title` = '{model.Title}', `shelf_life` = '{model.ShelfLife}', `price` = '{model.Price}' 
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



