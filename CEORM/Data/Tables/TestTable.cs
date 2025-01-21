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
    
    public int Id { get; set; }
    
    public int IntColumn { get; set; }
    
    public string StringColumn { get; set; }
    
    public DateTime DateTimeColumn { get; set; }
    
    public decimal DecimalColumn { get; set; }
    
    public bool IsSelected { get; set; }
}