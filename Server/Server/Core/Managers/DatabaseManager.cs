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

namespace Server.Core.Managers
{
    class DatabaseManager
    {
        private static String server = "localhost";
        private static String user = "root";
        private static String password = "hRSW871zq";
        private static String database = "tracker";
        private static String stringConnection = "SERVER = " + server + "; " +
                                              "DATABASE = " + database + "; " +
                                              "UID = " + user + "; " +
                                              "PASSWORD = " + password + ";";
        private static String tableName = "trackinglog";

        public static MySqlConnection connection;
        public static MySqlCommand command;

        public static string[] Keys = { "TimeReceived", "Latitude", "Longitude", "Speed", "StatusBit", "QtyLt", "StatusBitTank1", "QtyLtTank1", "StatusBitTank2", "QtyLtTank2" };

        public static bool Start()
        {
            if (connection == null)
            {
                try
                {
                    Console.WriteLine("Connecting to database...");
                    connection = new MySqlConnection(stringConnection);
                    command = connection.CreateCommand();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Could not connect to database: {0}", ex.Message);
                    return false;
                }
            }

            return true;
        }

        public static bool InsertToDatabase(Dictionary<string, string> rows)
        {
            string fieldNames = "";

            for (int i = 0; i < Keys.Count(); i++)
            {
                fieldNames += Keys[i];
                if (i < Keys.Count() - 1)
                    fieldNames += ",";
            }

            command.CommandText = "INSERT INTO " + tableName;
            command.CommandText += "(";
            command.CommandText += fieldNames;
            command.CommandText += ")";
            command.CommandText += " VALUES ";
            command.CommandText += "(";

            command.CommandText += "'" + rows[Keys[0]] + "',";
            command.CommandText += rows[Keys[1]] + ",";
            command.CommandText += rows[Keys[2]] + ",";
            command.CommandText += rows[Keys[3]] + ",";
            command.CommandText += "'" + rows[Keys[4]] + "',";            
            command.CommandText += rows[Keys[5]] + ",";
            command.CommandText += "'" + rows[Keys[6]] + "',";            
            command.CommandText += rows[Keys[7]] + ",";
            command.CommandText += "'" + rows[Keys[8]] + "',";            
            command.CommandText += rows[Keys[9]];

            command.CommandText += ")";

            bool result;

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            finally
            {
                connection.Close();
            }

            return result;
        }
    }
}
