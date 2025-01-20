namespace CEORM.Data.Interfaces;

public interface IColumn
{
    string Name { get; }
    string ToCreateSql();
}