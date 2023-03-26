using Dapper;
using MySqlConnector;
using ZGmarket.Data;

namespace ZGmarket.Models.Repository;

public class EmpRepository
{
    private readonly MySqlConnection _context;
    public EmpRepository(MySqlConnection context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Emp>> GetEmp()
    {
        var query = $@"SELECT id as {nameof(Emp.Id)}, 
                            name as {nameof(Emp.Name)}, 
                            last_name as {nameof(Emp.LastName)},
                            birth_date as {nameof(Emp.BirthDate)},
                            position as {nameof(Emp.Position)}, 
                            phone as {nameof(Emp.Phone)}, 
                            password as {nameof(Emp.Password)}
                            FROM emp";

        var emp = await _context.QueryAsync<Emp>(query);
        return emp.ToList();
    }

    public async Task<Emp> GetEmp(int id)
    {
        var query = $@"SELECT id as {nameof(Emp.Id)}, 
                            name as {nameof(Emp.Name)}, 
                            last_name as {nameof(Emp.LastName)},
                            birth_date as {nameof(Emp.BirthDate)},
                            position as {nameof(Emp.Position)}, 
                            phone as {nameof(Emp.Phone)}, 
                            password as {nameof(Emp.Password)}
                            FROM emp where id = {id}";

        var emp = _context.QueryFirst<Emp>(query);
        if (emp == null)
        {
            throw new Exception($"id({id}) not found");
        }

        return emp;
    }
    public async Task<Emp> GetEmp(string phone)
    {
        var query = $@"SELECT id as {nameof(Emp.Id)}, 
                            name as {nameof(Emp.Name)}, 
                            last_name as {nameof(Emp.LastName)},
                            birth_date as {nameof(Emp.BirthDate)},
                            position as {nameof(Emp.Position)}, 
                            phone as {nameof(Emp.Phone)}, 
                            password as {nameof(Emp.Password)}
                            FROM emp where phone = '{phone}'";

        Emp emp = await _context.QueryFirstOrDefaultAsync<Emp>(query);
        if (emp == null)
        {
            throw new Exception($"phone ({phone}) not found");
        }

        return emp;
    }

    public async Task<Emp> AddEmp(Emp model)
    {
        model.Position = model.Position == null ? Positions.Trainee : model.Position;

        var query = $"INSERT INTO `zgmarket`.`emp` (`name`, `last_name`, `birth_date`, `position`, `phone`, `password`) " +
            $"VALUES ('{model.Name}', '{model.Name}', '{model.BirthDate.ToShortDateString()}', '{model.Position}', '{model.Phone}', '{model.Password}');";

        try
        {
            await _context.QueryAsync<Emp>(query);
            return model;
        }
        catch (Exception ex)
        {
            throw new Exception("Ошибка при добавлении нового сотрудника " + ex.Message);
        }
    }
    public async Task<Emp> AddEmp(string phone, string password)
    {

        var query = $"INSERT INTO `zgmarket`.`emp` (`phone`, `password`, `position`) " +
            $"VALUES ('{phone}', '{password}', '{Positions.Trainee}')";

        try
        {
            await _context.QueryAsync<Emp>(query);
            Emp newEmp = await GetEmp(phone);
            return newEmp;
        }
        catch (Exception ex)
        {
            throw new Exception("Ошибка при добавлении нового сотрудника" + ex.Message);
        }
    }

    //del
    public async Task<Emp> EditEmp(int id, Emp Model)
    {
        string _date = $"{Model.BirthDate.Year}-{Model.BirthDate.Month}-{Model.BirthDate.Day}";
        var query = $"UPDATE `zgmarket`.`emp` SET `name` = '{Model.Name}', `last_name` = '{Model.LastName}', `birth_date` = '{_date}', `position` = '{Model.Position}',`phone` = '{Model.Phone}', `password` = '{Model.Password}'" +
            $" WHERE (`id` = '{id}');";

        try
        {
            await _context.QueryAsync<Emp>(query);
            return await GetEmp(id);
        }
        catch (Exception ex)
        {
            throw new Exception("Ошибка при изминении данных сотрудника" + ex.Message);
        }
    }


    public async Task<Emp> EditAcc(int id, DateTime birthDay, string name, string lastName, string phone, string pwd)
    {
        string _date;

        if (birthDay == null)
        {
            _date = new DateOnly().ToLongDateString();
        }
        else
        {
            _date = $"{birthDay.Year}-{birthDay.Month}-{birthDay.Day}";
        }
        Console.WriteLine(_date);
        var query = $"UPDATE `zgmarket`.`emp` SET `name` = '{name}', `last_name` = '{lastName}', `birth_date` = '{_date}',`phone` = '{phone}', `password` = '{pwd}'" +
            $" WHERE (`id` = '{id}');";

        try
        {
            await _context.QueryAsync<Emp>(query);
            return await GetEmp(id);
        }
        catch (Exception ex)
        {
            throw new Exception("Ошибка при изминении данных сотрудника" + ex.Message);
        }
    }
}



