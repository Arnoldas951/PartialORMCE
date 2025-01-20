using CEORM.Data.Interfaces;

namespace CEORM.Columns.Data;

class NVarCharColumn : IColumn
{
    private int _size;

    public NVarCharColumn(string name, int size)
    {
        Name = name;
        _size = size;
    }

    public string Name { get; private set; }

    public string ToCreateSql()
    {
        return String.Format("[{0}] nvarchar({1})", Name, _size);
    }
}