using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace s3loginbackend
{
    public class DbConn
    {
        //voor niet localhost moet de sslmode none weg
        static string connectionString = "Server=localhost;Uid=root;Database=dbips3;Pwd=admin;SslMode=none"; 
        public static MySqlConnection connection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
