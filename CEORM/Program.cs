// See https://aka.ms/new-console-template for more information

using CEORM.Data;
using CEORM.Data.Repositories;
using CEORM.Data.Tables;

try
{
    var database = new DbContext();

    database.CreateDatabase();
    var testRepo = new TestTableRepository();
    var newRecord = new TestTable()
    {
        Id = 0,
        IntColumn = 1,
        DateTimeColumn = DateTime.Now,
        IsSelected = true,
        StringColumn = "TestingValueInsert",
        DecimalColumn = (decimal)18.7,
    };
    
    testRepo.Save(newRecord);

}
catch (Exception e)
{
    Console.Write(e);
}