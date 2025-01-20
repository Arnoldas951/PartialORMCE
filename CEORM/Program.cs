// See https://aka.ms/new-console-template for more information

using CEORM.Data;

try
{
    var database = new DbContext();

    database.CreateDatabase();
}
catch (Exception e)
{
    Console.Write(e);
}