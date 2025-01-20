using CEORM.Data.Interfaces;

namespace CEORM.Columns.Data;

class DateTimeColumn : IColumn
{
    public DateTimeColumn(string name)
    {
        Name = name;
    }

    public string Name { get; private set; }

    public string ToCreateSql()
    {
        return String.Format("[{0}] datetime NOT NULL", Name);
    }
}