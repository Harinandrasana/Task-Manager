using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using projetFinalcsharp.Pages.Taches.Methode;
using projetFinalcsharp.Pages.MethodeConnection;

namespace projetFinalcsharp.Pages.Taches
{
    public class IndexModel : PageModel
    {
        string searchTerm;

        public List<TacheInfo> listTaches = new List<TacheInfo>();
        public void OnPostSearch(int idEmployer)
        {
            TacheSearch tacheSearch = new TacheSearch();
            tacheSearch.rechercher(idEmployer, listTaches);

        }
        public void OnGet()
        {
            // if (searchTerm != null)
            // {
            //     string query = "SELECT * FROM Tache";
            // }
            try
            {
                BDConnection con = new BDConnection();
                MySqlConnection connection = con.GetConnection();

                {
                    connection.Open();
                    string sqlQuery = "SELECT * FROM Tache ORDER BY idTache ASC";
                    using (MySqlCommand command = new MySqlCommand(sqlQuery, connection))
                    {
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
