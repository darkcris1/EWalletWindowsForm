using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet
{
    internal class DB
    {
        private static string Server = "localhost";
        private static string Database = "ewallet";
        private static string Username = "root";
        private static string Password = "";
        private static string Port = "3306";
        private static string Connstr = "";

        public static MySqlConnection conn;


        public static string GetConnectionStr()
        {
            return Connstr = "SERVER=" + Server + ";" + "DATABASE=" + Database + ";" + "UID=" + Username + ";" + "PASSWORD=" + Password + ";" + "PORT=" + Port + ";";
        }


        public static MySqlConnection Connection()
        {
            if (conn != null && conn.State != ConnectionState.Closed) {
                if (conn.State != ConnectionState.Open) { 
                    conn.Open();
                }
                return conn;
            };

            conn = new MySqlConnection(DB.GetConnectionStr());
            conn.Open();
            return conn;
        }
    }
}
