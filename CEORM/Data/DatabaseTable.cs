using System.Text;
using CEORM.Data.Interfaces;

namespace CEORM.Data;

public class DatabaseTable : IDatabaseTable
{
    public List<IColumn> Columns { get; set; }

    private string TableName { get; set; }

    protected DatabaseTable(string tableName)
    {
        TableName = tableName;
        Columns = new List<IColumn>();
    }

    protected void AddColumn(IColumn column)
    {
        Columns.Add(column);
    }

    public string ToCreateSql()
    {
        var query = String.Format("CREATE TABLE [" + TableName
                                                   + "] (ID int IDENTITY PRIMARY KEY, "
                                                   + GetColumnCsvListString(o => o.ToCreateSql()) + ")");

        return query;
    }

    public string ToInsertSql()
    {
        var query = String.Format("INSERT INTO [" + TableName + "] ("
                                  + GetColumnCsvListString(o => string.Format("[{0}]", o.Name))
                                  + ") VALUES (" + GetColumnCsvListString(o => string.Format("@{0}", o.Name)) + ")");

        return query;
    }

    public string ToDeleteSql()
    {
        var query = String.Format("Delete FROM [" + TableName + "] WHERE [ID] = @ID");

        return query;
    }

    public string ToUpdateSql()
    {
        var query = "UPDATE [" + TableName
                               + "] SET " + GetColumnCsvListString(o => string.Format("[{0}]=@{0}", o.Name))
                               + " WHERE [ID] = @ID";

        return query;
    }

    private string GetColumnCsvListString(Func<IColumn, string> columnFunction)
    {
        var columnBuilder = new StringBuilder();

        foreach (var column in Columns)
        {
            var colName = columnFunction(column);

            columnBuilder.Append(colName);

            columnBuilder.Append(", ");
        }

        if (columnBuilder.Length > 2)
            columnBuilder.Remove(columnBuilder.Length - 2, 2);

        return columnBuilder.ToString();
    }

    public List<string> ColumnNames
    {
        get
        {
            var colNames = Columns.Select(o => o.Name).ToList();

            return colNames;
        }
    }
}