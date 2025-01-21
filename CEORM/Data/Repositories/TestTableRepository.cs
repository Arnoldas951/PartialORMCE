using CEORM.Data.Tables;

namespace CEORM.Data.Repositories;

public class TestTableRepository : BaseRepository<TestTable>
{
    public TestTableRepository() : base(new TestTable())
    {
    }
}