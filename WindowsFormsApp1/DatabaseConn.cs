using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    internal class DatabaseConn
    {
        private static SqlConnection con = new SqlConnection("Data Source=LAPTOP-BECHRSCI;Initial Catalog=RegLog;Integrated Security=True");

        public static SqlConnection GetConnection()

        {
            return con;
        }
    }
    }
