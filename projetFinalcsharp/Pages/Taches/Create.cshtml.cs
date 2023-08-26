using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using projetFinalcsharp.Pages.MethodeConnection;
using projetFinalcsharp.Pages.Taches.Methode;
using System;
using System.Collections.Generic;

namespace projetFinalcsharp.Pages.Taches
{
    public class CreateModel : PageModel
    {
        public List<string> ChargerEmploye()
        {
            GetIdEmploye charger = new GetIdEmploye();
            return charger.GetEmployeeId();
        }

        public TacheInfo tacheInfo = new TacheInfo();
        public String errorMessage = "";
        public String succesMessage = "";

        public void OnPost()
        {
            tacheInfo.description = Request.Form["description"];
            tacheInfo.statut = Request.Form["statut"];
            string dateDebutValue = Request.Form["dateDebut"];
            string dateFinValue = Request.Form["dateFin"];
            string employeIdValue = Request.Form["id"];
            if (int.TryParse(employeIdValue, out int idValue))
            {
                tacheInfo.employeId = idValue;
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
                    string sqlQuery = "INSERT INTO Tache (idEmployer,Description, DateDeb, DateFin, Statut) VALUES ( @employeeId,@description, @dateDebut, @dateFin, @statut)";
                    using (MySqlCommand command = new MySqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@employeeId", tacheInfo.employeId);
                        command.Parameters.AddWithValue("@description", tacheInfo.description);
                        command.Parameters.AddWithValue("@dateDebut", tacheInfo.dateDebut);
                        command.Parameters.AddWithValue("@dateFin", tacheInfo.dateFin);
                        command.Parameters.AddWithValue("@statut", tacheInfo.statut);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            succesMessage = "Ajout de l'employ� r�ussi";
            Response.Redirect("/Taches/Index");
        }
    }
}
