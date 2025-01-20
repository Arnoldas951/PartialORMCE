using CEORM.Data.Interfaces;

namespace CEORM.Columns.Data;

class DecimalColumn : IColumn
{
    private int _precision;
    private int _scale;

    public DecimalColumn(string name, int precision, int scale)
    {
        Name = name;
        _precision = precision;
        _scale = scale;            
    }

    public string Name { get; private set; }

    public string ToCreateSql()
    {
        return String.Format("[{0}] decimal({1},{2})", Name, _precision, _scale);
    }
}