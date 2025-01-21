using CEORM.Data.Interfaces;
using CEORM.Data.Tables;

namespace CEORM.Data;

public class TestTableQuery : BaseQuery<TestTable>
{
    protected override string QueryText()
    {
        return "SELECT * FROM [TestTable] ORDER BY [Id]";
    }
}