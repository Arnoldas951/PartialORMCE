using System.Reflection;
using CEORM.Data.Interfaces;
using Microsoft.Data.SqlClient;

namespace CEORM.Data;

public abstract class BaseQuery<T> : IBaseQuery<T> where T : class, new()
{
    protected abstract string QueryText();
    private readonly string _connectionString = Connection.ConnectionString;
    
    public T FirstOrDefault(int id)
    {
        using (var conn = new SqlConnection(_connectionString))
        {
            conn.Open();

            var query = QueryText();

            var type = typeof(T);

            var properties = type.GetProperties(BindingFlags.Public
                                                | BindingFlags.Instance
                                                | BindingFlags.SetProperty);

            using (var command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@ID", id);

                using (var reader = command.ExecuteReader())
                {
                    var values = new Object[reader.FieldCount];

                    if (reader.Read())
                    {
                        reader.GetValues(values);

                        var item = new T();

                        foreach (var property in properties)
                        {
                            if (property.CanWrite)
                                property.SetValue(item, reader[property.Name], null);
                        }

                        return item;
                    }

                    return null;
                }
            }
        }
    }

    public T FirstOrDefault()
    {
        using (var conn = new SqlConnection(_connectionString))
        {
            conn.Open();

            var query = QueryText();

            var type = typeof(T);

            var properties = type.GetProperties(BindingFlags.Public
                                                | BindingFlags.Instance
                                                | BindingFlags.SetProperty);

            using (var command = new SqlCommand(query, conn))
            {
                using (var reader = command.ExecuteReader())
                {
                    var values = new Object[reader.FieldCount];

                    if (reader.Read())
                    {
                        reader.GetValues(values);

                        var item = new T();

                        foreach (var property in properties)
                        {
                            if (property.CanWrite)
                                property.SetValue(item, reader[property.Name], null);
                        }

                        return item;
                    }

                    return GetDefaultValue();
                }
            }
        }
    }

    public List<T> ToList()
    {
        return GetList(null);
    }

    public List<T> ToList(int id)
    {
        return GetList(id);
    }
    
    private List<T> GetList(int? id)
    {
        var items = new List<T>();

        using (var conn = new SqlConnection(_connectionString))
        {
            conn.Open();

            var query = QueryText();

            var type = typeof(T);

            var properties = type.GetProperties(BindingFlags.Public
                                                | BindingFlags.Instance
                                                | BindingFlags.SetProperty);

            using (var command = new SqlCommand(query, conn))
            {
                if (id.HasValue)
                    command.Parameters.AddWithValue("@ID", id);

                using (var reader = command.ExecuteReader())
                {
                    var values = new Object[reader.FieldCount];

                    while (reader.Read())
                    {
                        reader.GetValues(values);

                        var item = new T();

                        foreach (var property in properties)
                        {
                            if (property.CanWrite)
                                property.SetValue(item, reader[property.Name], null);
                        }

                        items.Add(item);
                    }
                }
            }
        }

        return items;
    }
    
    protected virtual T GetDefaultValue()
    {
        return null;
    }
}