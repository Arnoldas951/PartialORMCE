using CEORM.Data.Interfaces;

namespace CEORM.Columns.Data;

class BoolColumn : IColumn
{
    public BoolColumn(string name)
    {
        Name = name;
    }

    public string Name { get; private set; }

    public string ToCreateSql()
    {
        return String.Format("[{0}] BIT", Name);
    }
}