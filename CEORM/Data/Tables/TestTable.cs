using CEORM.Columns.Data;

namespace CEORM.Data.Tables;

public class TestTable : DatabaseTable
{
    public TestTable() : base("Test")
    {
        AddColumn(new BoolColumn("IsSelected"));
        AddColumn(new NVarCharColumn("StringColumn", 300));
        AddColumn(new DateTimeColumn("DateTimeColumn"));
        AddColumn(new IntColumn("IntColumn"));
        AddColumn(new DecimalColumn("DecimalColumn", 10, 2));
    }
}