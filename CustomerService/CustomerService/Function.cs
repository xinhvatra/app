using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomerService
{
	class Function 
	{
		
		public static int fmName;
		static string host = "localhost";
		static int port = 3306;
		static string database = "customer";
		static string username = "root";
		static string password = "";
		public static int sttKetoan,sttDichvu;
        public static  MySqlConnection GetConnection()
        {
            
            String connString = "Server=" + host + ";Database=" + database+ ";port=" + port + ";User Id=" + username + ";password=" + password;

            MySqlConnection conn = new MySqlConnection(connString);

            return conn;
        }
    }
}
