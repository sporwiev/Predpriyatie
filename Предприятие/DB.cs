using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Предприятие
{
	internal class DB
	{ 

        MySqlConnection conn = new MySqlConnection($"server=localhost;port=3306;username=root;password=root;database=users");
        public void OpenConnection()
        {
			if (conn.State == System.Data.ConnectionState.Closed)
			{
				conn.Open();
			}




		}
		public void CloseConnection()
		{
			if (conn.State == System.Data.ConnectionState.Open)
			{
                conn.Close();
            }
        }
		public MySqlConnection getConnection()
		{
			return conn;
		}

	}
}

