using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using projetFinalcsharp.Pages.MethodeConnection;
using projetFinalcsharp.Pages.Taches.Methode;

namespace projetFinalcsharp.Pages.Taches
{
    public class EditModel : PageModel
    {
        public List<string> ChargerEmploye()
        {
            GetIdEmploye charger = new GetIdEmploye();
            return charger.GetEmployeeId();
        }

        public TacheInfo tacheInfo = new TacheInfo();
        public String errorMessage = "";
        public String succesMessage = "";

        public void OnGet()
        {
            string id = Request.Query["id"];

            try
            {
                BDConnection con = new BDConnection();
                MySqlConnection connection = con.GetConnection();
                {
                    connection.Open();
                    string sqlQuery = "SELECT * FROM Tache WHERE idTache=@id";
                    using (MySqlCommand command = new MySqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                tacheInfo.id = reader.GetInt32(0);
                                tacheInfo.employeId = Convert.ToInt32(reader.GetString(1));
                                tacheInfo.description = reader.GetString(2);
                                tacheInfo.dateDebut = reader.GetDateTime(3);
                                tacheInfo.dateFin = reader.GetDateTime(4);
                                tacheInfo.statut = reader.GetString(5);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            string idValue = Request.Form["id"];
            if (!string.IsNullOrEmpty(idValue) && int.TryParse(idValue, out int id))
            {
                tacheInfo.id = id;
            }

            tacheInfo.description = Request.Form["description"];
            tacheInfo.statut = Request.Form["statut"];
            string dateDebutValue = Request.Form["dateDebut"];
            string dateFinValue = Request.Form["dateFin"];
            string employeIdValue = Request.Form["empId"];
            if (int.TryParse(employeIdValue, out int idV))
            {
                tacheInfo.employeId = idV;
            }
            if (DateTime.TryParse(dateDebutValue, out DateTime dateD) && DateTime.TryParse(dateFinValue, out DateTime dateF))
            {
                tacheInfo.dateDebut = dateD;
                tacheInfo.dateFin = dateF;
            }
            if (tacheInfo.description.Length == 0 || dateDebutValue.Length == 0 || dateFinValue.Length == 0 || tacheInfo.statut.Length == 0 || tacheInfo.employeId == 0)
            {
                errorMessage = "Tous les formulaires doivent �tre compl�t�s";
                return;
            }

            // Enregistrement dans la base de donn�es

            try
            {
                BDConnection con = new BDConnection();
                MySqlConnection connection = con.GetConnection();
                {
                    connection.Open();
                    string sqlQuery = "UPDATE  Tache SET idEmployer=@employeeId, Description=@description, DateDeb=@dateDebut, DateFin=@dateFin, Statut=@statut WHERE idTache=@id";
                    using (MySqlCommand command = new MySqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@employeeId", tacheInfo.employeId);
                        command.Parameters.AddWithValue("@description", tacheInfo.description);
                        command.Parameters.AddWithValue("@dateDebut", tacheInfo.dateDebut);
                        command.Parameters.AddWithValue("@dateFin", tacheInfo.dateFin);
                        command.Parameters.AddWithValue("@statut", tacheInfo.statut);
                        command.Parameters.AddWithValue("@id", tacheInfo.id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            succesMessage = "Modification de taches r�ussi";
            Response.Redirect("/Taches/Index");
        }
    }
}

