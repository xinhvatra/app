using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomerService
{
	class Function 
	{
		
		public static int fmName;
		static string host = "10.27.0.10";
		static int port = 3306;
		static string database = "customer";
		static string username = "root";
		static string password = "tng@123";
		public static DataTable services;
		public static string data_services;
        public static  MySqlConnection GetConnection()
        {
            String connString = "Server=" + host + ";Database=" + database+ ";port=" + port + ";User Id=" + username + ";password=" + password;

            MySqlConnection conn = new MySqlConnection(connString);

            return conn;
        }
    }
}
