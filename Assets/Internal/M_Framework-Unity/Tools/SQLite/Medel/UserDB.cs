using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.Data.Common;
using System;
using System.IO;
using System.Text;

public class UserDB : BaseDB
{
    public UserDB(string dbPath) : base(dbPath)
    {
    }

    public UserDB(string dbPath, string table) : base(dbPath, table)
    {
    }

    public override void Create(string table = null)
    {
        if (!string.IsNullOrEmpty(table))
            base.table = table;
        if (!File.Exists(dbPath))
        {
            SQLiteDBHelper.CreateDB(dbPath, out string msg1);
        }
        //SQLiteDBHelper db = new SQLiteDBHelper(dbPath);
        StringBuilder builder = new StringBuilder();
        builder.Append($"CREATE TABLE {base.table}");
        builder.Append("(");
        builder.Append($"id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE").Append(","); // id
        builder.Append($"Username VARCHAR(20)").Append(","); // Username
        builder.Append($"Password VARCHAR(20)"); // Password
        builder.Append(")");

        SQLiteDBHelper.ExecuteNonQuery(dbPath, builder.ToString(), null, out string msg);
    }

    public int Insert(DBUserData userData, out string msg)
    {
        msg = "";
        int affectedRows = 0;
        try
        {
            string sql = "INSERT INTO user(Username,Password)values(@Username,@Password)";
            SqliteParameter[] parameters = new SqliteParameter[]{
                   new SqliteParameter("@Username",userData.Username),
                   new SqliteParameter("@Password",userData.Password),
            };

            affectedRows = SQLiteDBHelper.ExecuteNonQuery(dbPath, sql, parameters, out msg);
        }
        catch (System.Exception e)
        {
            msg = "返回错误：" + e.ToString();
        }

        return affectedRows;
    }

    public int Delate(string userName, out string msg)
    {
        int affectedRows = 0;
        msg = "";
        try
        {
            string sql =  $"DELETE FROM {table} WHERE Username='{userName}'";
            SQLiteDBHelper.ExecuteNonQuery(dbPath, sql, null, out msg);
        }
        catch (System.Exception)
        {

        }
        return affectedRows;
    }

    public int Delate(int id, out string msg)
    {
        int affectedRows = 0;
        msg = "";
        try
        {
            string sql = $"DELETE FROM {table} WHERE id='{id}'";
            affectedRows = SQLiteDBHelper.ExecuteNonQuery(dbPath, sql, null, out msg);
        }
        catch (System.Exception)
        {

        }
        return affectedRows;
    }

    public int Update(DBUserData userData, out string msg)
    {
        int affectedRows = 0;
        msg = "";
        try
        {
            string sql = $"UPDATE {table} SET Username=@Username,Password=@Password WHERE Username='{userData.Username}'";
            //SQLiteDBHelper db = new SQLiteDBHelper("D:\\Demo.db3");
            SqliteParameter[] parameters = new SqliteParameter[]{
                   new SqliteParameter("@Username",userData.Username),
                   new SqliteParameter("@Password",userData.Password),
            };

            affectedRows = SQLiteDBHelper.ExecuteNonQuery(dbPath, sql, parameters, out msg);
        }
        catch (System.Exception e)
        {
            msg = "返回错误：" + e.ToString();
        }
        return affectedRows;
    }

    public DBUserData Read(string userName, out string msg)
    {
        List<DBUserData> datas = new List<DBUserData>();
        msg = "";

        var sql = $"SELECT * FROM {table} WHERE Username='{userName}'";
        var dataTable = SQLiteDBHelper.ExecuteDataTable(dbPath, sql, null);

        if (dataTable == null || dataTable.Rows.Count <= 0)
        {
            return null;
        }

        foreach (DataRow r in dataTable.Rows)
        {
            DBUserData data = new DBUserData();
            data.Username = userName;
            try
            {
                data.Username = r["Username"] as string;
                data.Password = r["Password"] as string;
            }
            catch (Exception)
            {
            }
            finally {
                datas.Add(data);
            }
        }

        return datas[0];
    }

    public DBUserData Read(string userName, string password, out string msg)
    {
        List<DBUserData> datas = new List<DBUserData>();
        msg = "";

        var sql = $"SELECT * FROM {table} WHERE Username='{userName}' AND Password='{password}'";
        var dataTable = SQLiteDBHelper.ExecuteDataTable(dbPath, sql, null);

        if (dataTable == null || dataTable.Rows.Count <= 0)
        {
            return null;
        }

        foreach (DataRow r in dataTable.Rows)
        {
            DBUserData data = new DBUserData();
            data.Username = userName;
            try
            {
                data.Username = r["Username"] as string;
                data.Password = r["Password"] as string;
            }
            catch (Exception)
            {
            }
            finally
            {
                datas.Add(data);
            }
        }

        return datas[0];
    }

    public DBUserData[] ReadArray(int sex, out string msg)
    {
        List<DBUserData> datas = new List<DBUserData>();
        msg = "";

        var sql = $"SELECT * FROM {table} WHERE Sex='{sex}'";
        var dataTable = SQLiteDBHelper.ExecuteDataTable(dbPath, sql, null);

        if (dataTable == null || dataTable.Rows.Count <= 0)
        {
            return null;
        }

        foreach (DataRow r in dataTable.Rows)
        {
            DBUserData data = new DBUserData();
            try
            {
                data.Username = r["Username"] as string;
                data.Password = r["Password"] as string;
            }
            catch (Exception)
            {
            }
            finally
            {
                datas.Add(data);
            }
        }

        return datas.ToArray();
    }

    public DBUserData[] ReadALL(out string msg)
    {
        List<DBUserData> datas = new List<DBUserData>();
        msg = "";

        var sql = $"SELECT * FROM {table}";
        var dataTable = SQLiteDBHelper.ExecuteDataTable(dbPath, sql, null);

        if (dataTable == null || dataTable.Rows.Count <= 0)
        {
            return null;
        }

        foreach (DataRow r in dataTable.Rows)
        {
            DBUserData data = new DBUserData();
            try
            {
                data.Username = r["Username"] as string;
                data.Password = r["Password"] as string;
            }
            catch (Exception)
            {
            }
            finally
            {
                datas.Add(data);
            }
        }

        return datas.ToArray();
    }

}
