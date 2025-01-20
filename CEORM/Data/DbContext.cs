using CEORM.Data.Interfaces;
using CEORM.Data.Tables;
using Microsoft.Data.SqlClient;

namespace CEORM.Data;

public class DbContext
{
    private readonly List<IDatabaseTable> _databaseTables = new List<IDatabaseTable>();

    public DbContext()
    {
        _databaseTables.Add(new TestTable());
    }

    public void CreateDatabase()
    {
        string conString = Connection.ConnectionString;
        string databaselessConString = Connection.DatabaselessString;

        if (!CheckDatabaseExists("TestDb"))
        {
            using (SqlConnection conn = new SqlConnection(databaselessConString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("CREATE DATABASE TestDb", conn))
                {
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }

            using (SqlConnection conn = new SqlConnection(conString))
            {
                conn.Open();
                foreach (IDatabaseTable table in _databaseTables)
                {
                    var createCommand = table.ToCreateSql();
                    using (SqlCommand cmd = new SqlCommand(createCommand, conn))
                    {
                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch (SqlException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
                conn.Close();
            }
        }
    }

    private static bool CheckDatabaseExists(string dataBase)
    {
        string conString = Connection.DatabaselessString;

        string cmdText = String.Format("SELECT * FROM sys.databases where Name='{0}'", dataBase);
        bool isExist = false;
        using (SqlConnection conn = new SqlConnection(conString))
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    isExist = reader.HasRows;
                }
            }

            conn.Close();
        }

        return isExist;
    }
}