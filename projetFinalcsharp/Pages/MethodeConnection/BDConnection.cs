using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace projetFinalcsharp.Pages.MethodeConnection
{
    public class BDConnection
    {

        string connectionString = "Server=localhost;Database=gestionTache;Uid=root;Pwd=root;";
        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
