using MySql.Data.MySqlClient;
using projetFinalcsharp.Pages.MethodeConnection;
using projetFinalcsharp.Pages.Taches.Methode;

namespace projetFinalcsharp.Pages.Employees.MethodeEmploye
{
    public class EmployeSearch
    {
        public void rechercher(string valeurEntrer, List<EmployeeInfo> listEmployees)
        {
            try
            {
                BDConnection con = new BDConnection();
                MySqlConnection connection = con.GetConnection();
                {
                    connection.Open();
                    string sqlQuery = "SELECT * FROM employer WHERE idEmployer = @idEmployer OR nom LIKE @valeurEntrer";
                    using (MySqlCommand command = new MySqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@valeurEntrer", "%" + valeurEntrer + "%");

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                EmployeeInfo employeeInfo = new EmployeeInfo();
                                employeeInfo.id = reader.GetInt32(0);
                                employeeInfo.nom = reader.GetString(1);
                                employeeInfo.prenom = reader.GetString(2);
                                employeeInfo.poste = reader.GetString(3);

                                listEmployees.Add(employeeInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }
}
        