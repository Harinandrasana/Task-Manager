using MySql.Data.MySqlClient;
using projetFinalcsharp.Pages.MethodeConnection;

namespace projetFinalcsharp.Pages.Taches.Methode
{
    public class GetIdEmploye
    {
        public List<string> GetEmployeeId()
        {
            List<string> employeeIds = new List<string>();

            try
            {
                BDConnection con = new BDConnection();
                MySqlConnection connection = con.GetConnection();
                {
                    connection.Open();
                    string sqlQuery = "SELECT idEmployer FROM Employer";
                    using (MySqlCommand command = new MySqlCommand(sqlQuery, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string id = reader.GetString("idEmployer");
                                employeeIds.Add(id);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }

            return employeeIds;
        }
    }
}
