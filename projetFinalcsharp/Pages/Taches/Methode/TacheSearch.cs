using MySql.Data.MySqlClient;
using projetFinalcsharp.Pages.MethodeConnection;

namespace projetFinalcsharp.Pages.Taches.Methode
{
    public class TacheSearch
    {
        public void rechercher(int idEmployer, List<TacheInfo> listTaches)
        {
            try
            {
                BDConnection con = new BDConnection();
                MySqlConnection connection = con.GetConnection();
                {
                    connection.Open();
                    string sqlQuery = "SELECT * FROM tache WHERE idEmployer = @idEmployer";
                    using (MySqlCommand command = new MySqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@idEmployer", idEmployer);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TacheInfo tacheInfo = new TacheInfo();
                                tacheInfo.id = reader.GetInt32(0);
                                tacheInfo.employeId = reader.GetInt32(1);
                                tacheInfo.description = reader.GetString(2);
                                tacheInfo.dateDebut = reader.GetDateTime(3);
                                tacheInfo.dateFin = reader.GetDateTime(4);
                                tacheInfo.statut = reader.GetString(5);

                                listTaches.Add(tacheInfo);
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
