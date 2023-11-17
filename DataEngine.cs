using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;

public class DataEngine
{
    private readonly DbContext _context;

    public DataEngine(DbContext context)
    {
        _context = context;
    }

    public void ExecuteStoredProcedureNonQuery(string storedProcedureName)
    {
        ExecuteStoredProcedureNonQuery(storedProcedureName, Array.Empty<SqlParameter>());
    }

    public List<T> ExecuteStoredProcedure<T>(string storedProcedureName)
    {
        return ExecuteStoredProcedure<T>(storedProcedureName, Array.Empty<SqlParameter>());
    }

    public void ExecuteStoredProcedureNonQuery(string storedProcedureName, params SqlParameter[] parameters)
    {
        using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = storedProcedureName;
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddRange(parameters);

            _context.Database.OpenConnection();

            command.ExecuteNonQuery();
        }
    }

    public List<T> ExecuteStoredProcedure<T>(string storedProcedureName, params SqlParameter[] parameters)
    {
        using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = storedProcedureName;
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddRange(parameters);

            _context.Database.OpenConnection();

            using (var reader = command.ExecuteReader())
            {
                var dataTable = new DataTable();
                dataTable.Load(reader);
                return ConvertDataTableToList<T>(dataTable);
            }
        }
    }

    private List<T> ConvertDataTableToList<T>(DataTable dataTable)
    {
        var list = new List<T>();

        foreach (DataRow row in dataTable.Rows)
        {
            var item = Activator.CreateInstance<T>();

            foreach (DataColumn column in dataTable.Columns)
            {
                var propertyName = column.ColumnName;
                var property = typeof(T).GetProperty(propertyName);

                if (property != null && row[column] != DBNull.Value)
                {
                    var value = Convert.ChangeType(row[column], property.PropertyType);
                    property.SetValue(item, value);
                }
            }

            list.Add(item);
        }

        return list;
    }

}
