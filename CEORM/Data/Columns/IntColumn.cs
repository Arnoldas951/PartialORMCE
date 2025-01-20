using CEORM.Data.Interfaces;

namespace CEORM.Columns.Data;

class IntColumn : IColumn
{
    public IntColumn(string name)
    {
        Name = name;
    }

    public string Name { get; private set; }

    public string ToCreateSql()
    {
        return String.Format("[{0}] int", Name);
    }
}