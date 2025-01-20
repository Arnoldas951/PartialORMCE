namespace CEORM.Data.Interfaces;

public interface IDatabaseTable
{
    List<IColumn> Columns { get; }
    List<string> ColumnNames { get; }
    string ToCreateSql();
    string ToInsertSql();
    string ToUpdateSql();
    string ToDeleteSql();
}