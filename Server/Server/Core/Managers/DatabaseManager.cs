using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data;
using MySql.Data.Common;
using MySql.Data.Entity;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System.Data.SqlClient;

namespace Server.Core.Managers
{
    enum ServerType { Mysql, SqlServer };

    class DatabaseManager
    {
        /*private static String server = "localhost";
        private static String user = "root";
        private static String password = "hRSW871zq";
        private static String database = "tracker";
        private static String tableName = "trackinglog";*/

        private static String server = @"localhost";
        private static String user = "sa";
        private static String password = "tracksol";
        private static String database = "tracker";
        private static String tableName = "trackinglog";

        private static String mysqlConnectionString = "SERVER = " + server + "; " +
                                              "DATABASE = " + database + "; " +
                                              "UID = " + user + "; " +
                                              "PASSWORD = " + password + ";";

        private static String sqlserverConnectionString = "data source = " + server + "; " + 
                                                          "initial catalog = " + database + ";" +
                                                          "user id = " + user + "; " +
                                                          "password = " + password + "; Persist Security Info=True;Pooling=true;Connect Timeout=10";

        public static MySqlConnection mysqlConnection;
        public static MySqlCommand mysqlCommand;

        public static SqlConnection sqlserverConnection;

        public static string[] Keys = { "TimeReceived", "Latitude", "Longitude", "Speed", "StatusBit", "QtyLt", "StatusBitTank1", "QtyLtTank1", "StatusBitTank2", "QtyLtTank2" };
        public static ServerType serverType;

        public static bool Start(ServerType serverType)
        {
            DatabaseManager.serverType = serverType;

            if (mysqlConnection == null && serverType == ServerType.Mysql)
            {
                try
                {
                    Console.WriteLine("Connecting to database...");
                    mysqlConnection = new MySqlConnection(mysqlConnectionString);
                    mysqlCommand = mysqlConnection.CreateCommand();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Could not connect to database: {0}", ex.Message);
                    return false;
                }
            }
            else if (sqlserverConnection == null && serverType == ServerType.SqlServer)
            {
                try
                {
                    Console.WriteLine("Connecting to database...");
                    sqlserverConnection = new SqlConnection(sqlserverConnectionString);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Could not connect to database: {0}", ex.Message);
                    return false;
                }
            }

            return true;
        }

        private static string GetFieldNames()
        {
            string fieldNames = "";

            for (int i = 0; i < Keys.Count(); i++)
            {
                fieldNames += Keys[i];
                if (i < Keys.Count() - 1)
                    fieldNames += ",";
            }

            return fieldNames;
        }

        private static bool InsertToMysqlDatabase(Dictionary<string, string> rows)
        {
            mysqlCommand.CommandText = "INSERT INTO " + tableName;
            mysqlCommand.CommandText += "(";
            mysqlCommand.CommandText += GetFieldNames();
            mysqlCommand.CommandText += ")";
            mysqlCommand.CommandText += " VALUES ";
            mysqlCommand.CommandText += "(";

            mysqlCommand.CommandText += "'" + rows[Keys[0]] + "',";
            mysqlCommand.CommandText += rows[Keys[1]] + ",";
            mysqlCommand.CommandText += rows[Keys[2]] + ",";
            mysqlCommand.CommandText += rows[Keys[3]] + ",";
            mysqlCommand.CommandText += "'" + rows[Keys[4]] + "',";
            mysqlCommand.CommandText += rows[Keys[5]] + ",";
            mysqlCommand.CommandText += "'" + rows[Keys[6]] + "',";
            mysqlCommand.CommandText += rows[Keys[7]] + ",";
            mysqlCommand.CommandText += "'" + rows[Keys[8]] + "',";
            mysqlCommand.CommandText += rows[Keys[9]];

            mysqlCommand.CommandText += ")";

            bool result;

            try
            {
                mysqlConnection.Open();
                mysqlCommand.ExecuteNonQuery();
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting to database: " + ex.Message);
                result = false;
            }
            finally
            {
                mysqlConnection.Close();
            }

            return result;
        }

        private static bool InsertToSqlDatabase(Dictionary<string, string> rows)
        {
            bool result = false;

            sqlserverConnection.Open();

            string command = "INSERT INTO " + tableName;
            command += "(";
            command += GetFieldNames();
            command += ")";
            command += " VALUES ";
            command += "(";

            command += "CONVERT(Datetime, '" + rows[Keys[0]] + "', 120),";
            command += rows[Keys[1]] + ",";
            command += rows[Keys[2]] + ",";
            command += rows[Keys[3]] + ",";
            command += "'" + rows[Keys[4]] + "',";
            command += rows[Keys[5]] + ",";
            command += "'" + rows[Keys[6]] + "',";
            command += rows[Keys[7]] + ",";
            command += "'" + rows[Keys[8]] + "',";
            command += rows[Keys[9]];

            command += ")";

            try
            {
                SqlCommand sqlCommand = new SqlCommand(command, sqlserverConnection);
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting to database: " + ex.Message);
                result = false;
            }
            finally
            {
                sqlserverConnection.Close();
            }

            return result;
        }

        public static bool InsertToDatabase(Dictionary<string, string> rows)
        {
            if (serverType == ServerType.Mysql)
                return InsertToMysqlDatabase(rows);
            else
                return InsertToSqlDatabase(rows);

        }
    }
}
