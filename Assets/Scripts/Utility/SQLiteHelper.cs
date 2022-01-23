using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System;

public enum SQLDataType
{
    Bool,
    Int,
    Float,
    String
}
public class SQLiteHelper
{
    /// <summary>
    /// 数据库连接定义
    /// </summary>
    private SqliteConnection dbConnection;

    /// <summary>
    /// SQL命令定义
    /// </summary>
    private SqliteCommand dbCommand;

    /// <summary>
    /// 数据读取定义
    /// </summary>
    private SqliteDataReader dataReader;

    /// <summary>
    /// 构造函数    
    /// </summary>
    /// <param name="connectionString">数据库连接字符串</param>
    public SQLiteHelper(string connectionString)
    {
        try
        {
            //构造数据库连接
            dbConnection = new SqliteConnection("data source ="+connectionString);
            //打开数据库
            dbConnection.Open();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    /// <summary>
    /// 执行SQL命令
    /// </summary>
    /// <returns>The query.</returns>
    /// <param name="queryString">SQL命令字符串</param>
    public SqliteDataReader ExecuteQuery(string queryString)
    {

        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = queryString;
        dataReader = dbCommand.ExecuteReader();
        return dataReader;
    }



    /// <summary>
    /// 关闭数据库连接
    /// </summary>
    public void CloseConnection()
    {
        //销毁Command
        if (dbCommand != null)
        {
            dbCommand.Cancel();
        }
        dbCommand = null;

        //销毁Reader
        if (dataReader != null)
        {
            dataReader.Close();
        }
        dataReader = null;

        //销毁Connection
        if (dbConnection != null)
        {
            dbConnection.Close();
        }
        dbConnection = null;
    }


    /// <summary>
    /// 读取整张数据表
    /// </summary>
    /// <returns>The full table.</returns>
    /// <param name="tableName">数据表名称</param>
    public SqliteDataReader ReadFullTable(string tableName)
    {
        string queryString = "SELECT * FROM " + tableName;
        return ExecuteQuery(queryString);
    }

    /// <summary>
    /// 向指定数据表中插入数据
    /// </summary>
    /// <returns>The values.</returns>
    /// <param name="tableName">数据表名称</param>
    /// <param name="values">插入的数值</param>
    public SqliteDataReader InsertValues(string tableName, string[] values)
    {
        //获取数据表中字段数目
        int fieldCount = ReadFullTable(tableName).FieldCount;
        //当插入的数据长度不等于字段数目时引发异常
        if (values.Length != fieldCount)
        {
            throw new SqliteException("values.Length!=fieldCount");
        }

        string queryString = "INSERT INTO " + tableName + " VALUES (" + values[0];
        for (int i = 1; i < values.Length; i++)
        {
            queryString += ", " + values[i];
        }
        queryString += " )";
       
        return ExecuteQuery(queryString);
    }

    /// <summary>
    /// 更新指定数据表内的数据
    /// </summary>
    /// <returns>The values.</returns>
    /// <param name="tableName">数据表名称</param>
    /// <param name="colNames">字段名</param>
    /// <param name="colValues">字段名对应的数据</param>
    /// <param name="key">关键字</param>
    /// <param name="value">关键字对应的值</param>
    public SqliteDataReader UpdateValues(string tableName, string[] colNames, string[] colValues, string[] key, string operation, string[] value)
    {
        //当字段名称和字段数值不对应时引发异常
        if (colNames.Length != colValues.Length)
        {
            throw new SqliteException("colNames.Length!=colValues.Length");
        }

        string queryString = "UPDATE " + tableName + " SET " + colNames[0] + "=" + colValues[0];
        for (int i = 1; i < colValues.Length; i++)
        {
            queryString += ", " + colNames[i] + "=" + colValues[i];
        }
        queryString += " WHERE " + key[0] + operation + value[0];
        for (int i = 1; i < key.Length; i++)
        {
            queryString += " And " + key[i] + operation + value[i];
        }
        Debug.Log(queryString);
        return ExecuteQuery(queryString);
    }

    /// <summary>
    /// 删除指定数据表内的数据
    /// </summary>
    /// <returns>The values.</returns>
    /// <param name="tableName">数据表名称</param>
    /// <param name="colNames">字段名</param>
    /// <param name="colValues">字段名对应的数据</param>
    public SqliteDataReader DeleteValuesOR(string tableName, string[] colNames, string[] operations, string[] colValues)
    {
        //当字段名称和字段数值不对应时引发异常
        if (colNames.Length != colValues.Length || operations.Length != colNames.Length || operations.Length != colValues.Length)
        {
            throw new SqliteException("colNames.Length!=colValues.Length || operations.Length!=colNames.Length || operations.Length!=colValues.Length");
        }

        string queryString = "DELETE FROM " + tableName + " WHERE " + colNames[0] + operations[0] + colValues[0];
        for (int i = 1; i < colValues.Length; i++)
        {
            queryString += "OR " + colNames[i] + operations[0] + colValues[i];
        }
        return ExecuteQuery(queryString);
    }

    /// <summary>
    /// 删除指定数据表内的数据
    /// </summary>
    /// <returns>The values.</returns>
    /// <param name="tableName">数据表名称</param>
    /// <param name="colNames">字段名</param>
    /// <param name="colValues">字段名对应的数据</param>
    public SqliteDataReader DeleteValuesAND(string tableName, string[] colNames, string[] operations, string[] colValues)
    {
     
            //当字段名称和字段数值不对应时引发异常
        if (colNames.Length != colValues.Length || operations.Length != colNames.Length || operations.Length != colValues.Length)
        {
            throw new SqliteException("colNames.Length!=colValues.Length || operations.Length!=colNames.Length || operations.Length!=colValues.Length");
        }

        string queryString = "DELETE FROM " + tableName + " WHERE " + colNames[0] + operations[0] + colValues[0];
        for (int i = 1; i < colValues.Length; i++)
        {
            queryString += " AND " + colNames[i] + operations[i] + colValues[i];
        }
        return ExecuteQuery(queryString);
    }

    /// <summary>
    /// 创建数据表
    /// </summary> +
    /// <returns>The table.</returns>
    /// <param name="tableName">数据表名</param>
    /// <param name="colNames">字段名</param>
    /// <param name="colTypes">字段名类型</param>
    public SqliteDataReader CreateTable(string tableName, string[] colNames, string[] colTypes)
    {
        string queryString = "CREATE TABLE " + tableName + "( " + colNames[0] + " " + colTypes[0];
        for (int i = 1; i < colNames.Length; i++)
        {
            queryString += ", " + colNames[i] + " " + colTypes[i];
        }
        queryString += "  ) ";
        return ExecuteQuery(queryString);
    }

    /// <summary>
    /// Reads the table.
    /// </summary>
    /// <returns>The table.</returns>
    /// <param name="tableName">Table name.</param>
    /// <param name="items">Items.</param>
    /// <param name="colNames">Col names.</param>
    /// <param name="operations">Operations.</param>
    /// <param name="colValues">Col values.</param>
    public SqliteDataReader ReadTable(string tableName, string[] items,  string[] colNames,  string[] operations,  string[] colValues)
    {
        string queryString = "SELECT " + items[0];
        for (int i = 1; i < items.Length; i++)
        {
            queryString += ", " + items[i];
        }
        queryString += " FROM " + tableName + " WHERE " + colNames[0] + " " + operations[0] + " " + colValues[0];
        for (int i = 1; i < colNames.Length; i++)
        {
            queryString += " AND " + colNames[i] + " " + operations[i] + " " + colValues[i] + " ";
        }
        return ExecuteQuery(queryString);
    }

    
  /// <summary>
  /// 查询表去重
  /// </summary>
  /// <param name="tableName"></param>
  /// <param name="item"></param>
  /// <returns></returns>
    public SqliteDataReader ReadTableDistinct(string tableName, string item)
    {
        string queryString = "SELECT "+"DISTINCT "+ item;
         queryString += " FROM " + tableName;
        return ExecuteQuery(queryString); 
    }

    public SqliteDataReader ReadTableDistinct(string tableName, string item,string where)
    {
        string queryString = "SELECT " + "DISTINCT " + item;
        queryString += " FROM " + tableName + " WHERE "+ where;
        return ExecuteQuery(queryString);
    }

    public SqliteDataReader FuzzyQuery(string tableName, string[] items, string[] colNames, string[] operations, string[] colValues)
    {
        string queryString = "SELECT " + items[0];
        for (int i = 1; i < items.Length; i++)
        {
            queryString += ", " + items[i];
        }
        queryString += " FROM " + tableName + " WHERE " + colNames[0] + " " + operations[0] + " " + colValues[0];
        for (int i = 1; i < colNames.Length; i++)
        {
            queryString += " OR " + colNames[i] + " " + operations[i] + " " + colValues[i] + " ";
        }
        return ExecuteQuery(queryString);
    }


    public object QueryValue(string tableName,string key, SQLDataType dataType)
    {
        SqliteDataReader reader = ReadTable(tableName, new string[] { dataType .ToString ()}, new string[] { "Key" }, new string[] { "=" }, new string[] { @$"'{key}'" });
        object data = null;
        while (reader.Read())
        {
            switch (dataType)
            {
                case SQLDataType.Bool:
                    data = reader.GetBoolean(reader.GetOrdinal(SQLDataType.Bool.ToString ()));
                    break;
                case SQLDataType.Float:
                    data = reader.GetFloat(reader.GetOrdinal(SQLDataType.Float .ToString()));
                    break;
                case SQLDataType.Int:
                    data = reader.GetInt32(reader.GetOrdinal(SQLDataType.Int .ToString()));
                    break;
                case SQLDataType.String:
                    data = reader.GetString(reader.GetOrdinal(SQLDataType.String .ToString()));
                    break;
                default:
                    break;
            }
        }
        return data;
    }

    public void  InsertOrUpdataValue(string tableName, string key, SQLDataType dataType, object value)
    {
        SqliteDataReader reader =ReadTable(tableName, new string[] { "Key" }, new string[] { "Key" }, new string[] { "=" }, new string[] { @$"'{key}'" });

        while (reader.Read())
        {
            if (!string.IsNullOrEmpty(reader.GetString(reader.GetOrdinal("Key"))))
            {
                switch (dataType)
                {
                    case SQLDataType.Bool:
                        UpdateValues(tableName, new string[] { "Key", "Bool", "Int", "Float", "String" }, new string[] { $@"'{key}'", $@"'{value}'", "NULL", "NULL", "NULL" }, new string[] { "Key" }, "=", new string[] { $@"'{key}'" });
                        break;
                    case SQLDataType.Float:
                        UpdateValues(tableName, new string[] { "Key", "Bool", "Int", "Float", "String" }, new string[] { $@"'{key}'", "NULL", "NULL", $@"'{value}'", "NULL" }, new string[] { "Key" }, "=", new string[] { $@"'{key}'" });
                        break;
                    case SQLDataType.Int:
                        UpdateValues(tableName, new string[] { "Key", "Bool", "Int", "Float", "String" }, new string[] { $@"'{key}'", "NULL", $@"'{value}'", "NULL", "NULL" }, new string[] { "Key" }, "=", new string[] { $@"'{key}'" });
                        break;
                    case SQLDataType.String:
                        UpdateValues(tableName, new string[] { "Key", "Bool", "Int", "Float", "String" }, new string[] { $@"'{key}'", "NULL", "NULL", "NULL", $@"'{value}'" }, new string[] { "Key" }, "=", new string[] { $@"'{key}'" });
                        break;
                    default:
                        break;
                }

                return;
            }

        }
        switch (dataType)
        {
            case SQLDataType.Bool:
                InsertValues(tableName, new string[] { $@"'{key}'", $@"'{value}'", "NULL", "NULL", "NULL" });
                break;
            case SQLDataType.Float:
                InsertValues(tableName, new string[] { $@"'{key}'", "NULL", "NULL", $@"'{value}'", "NULL" });
                break;
            case SQLDataType.Int:
              InsertValues(tableName, new string[] { $@"'{key}'", "NULL", $@"'{value}'", "NULL", "NULL" });
                break;
            case SQLDataType.String:
                InsertValues(tableName, new string[] { $@"'{key}'", "NULL", "NULL", "NULL", $@"'{value}'" });
               
                break;
            default:
                break;
        }

    
    }
}