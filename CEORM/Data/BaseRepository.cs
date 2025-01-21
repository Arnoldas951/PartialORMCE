using CEORM.Data.Interfaces;
using Microsoft.Data.SqlClient;

namespace CEORM.Data;

public class BaseRepository<T> : IBaseRepository<T>
{
    private IDatabaseTable _databaseTableable;
    private string _connectionString = Connection.ConnectionString;

    public BaseRepository(IDatabaseTable databaseTableable)
    {
        _databaseTableable = databaseTableable;
    }

    public void Save(T entity)
    {
        Type t = entity.GetType();

        var prop = t.GetProperty("Id");

        if (prop == null)
            throw new NullReferenceException("ID is null");

        var id = Convert.ToInt32(prop.GetValue(entity, null));

        if (id == 0)
            Add(entity);
        else
            Update(entity, id);
    }

    private void Add(T entity)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            conn.Open();

            var query = _databaseTableable.ToInsertSql();

            Type t = entity.GetType();

            var cmd = CreateCommand(entity, conn, t, query);

            try
            {
                int affectedRows = cmd.ExecuteNonQuery();

                Console.WriteLine(string.Format("Affected rows: {0}", affectedRows));
            }
            catch (SqlException ex)
            {
                foreach (SqlError error in ex.Errors)
                {
                    Console.WriteLine(error.Message);
                }

                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    private void Update(T entity, int id)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            conn.Open();

            Type t = entity.GetType();

            var query = _databaseTableable.ToUpdateSql();

            var cmd = CreateCommand(entity, conn, t, query);

            cmd.Parameters.AddWithValue("@ID", id);

            try
            {
                int affectedRows = cmd.ExecuteNonQuery();

                Console.WriteLine(string.Format("Affected rows: {0}", affectedRows));
            }
            catch (SqlException ex)
            {
                foreach (SqlError error in ex.Errors)
                {
                    //Error handling, etc.
                    Console.WriteLine(error.Message);
                }

                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    public void Delete(int id)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            conn.Open();

            var query = _databaseTableable.ToDeleteSql();

            var cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@ID", id);

            try
            {
                int affectedRows = cmd.ExecuteNonQuery();

                Console.WriteLine(string.Format("Affected rows: {0}", affectedRows));
            }
            catch (SqlException ex)
            {
                foreach (SqlError error in ex.Errors)
                {
                    Console.WriteLine(error.Message);
                }

                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    private SqlCommand CreateCommand(T entity, SqlConnection conn, Type t, string query)
    {
        var cmd = new SqlCommand(query, conn);

        foreach (var colName in _databaseTableable.ColumnNames)
        {
            var prop = t.GetProperty(colName);

            var value = prop.GetValue(entity, null);

            if (value == null)
            {
                value = string.Empty;
            }

            cmd.Parameters.AddWithValue("@" + colName, value);
        }

        return cmd;
    }
}