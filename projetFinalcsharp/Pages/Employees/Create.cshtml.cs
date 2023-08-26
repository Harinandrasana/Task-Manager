using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using projetFinalcsharp.Pages.MethodeConnection;
using System;

namespace projetFinalcsharp.Pages.Employees
{
    public class CreateModel : PageModel
    {
        public EmployeeInfo employeeInfo = new EmployeeInfo();
        public String errorMessage = "";
        public String succesMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            employeeInfo.nom = Request.Form["nom"];
            employeeInfo.prenom = Request.Form["prenom"];
            employeeInfo.poste = Request.Form["poste"];
            if (employeeInfo.nom.Length == 0 || employeeInfo.prenom.Length == 0 || employeeInfo.poste.Length == 0)
            {
                errorMessage = "Tous les formulaires doivent être complétés";
                return;
            }

            // Enregistrement dans la base de données

            try
            {
                BDConnection con = new BDConnection();
                MySqlConnection connection = con.GetConnection();
                {
                    connection.Open();
                    string sqlQuery = "INSERT INTO Employer (nom, prenom, poste) " +
                                      "VALUES (@nom, @prenom, @poste);";
                    using (MySqlCommand command = new MySqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@nom", employeeInfo.nom);
                        command.Parameters.AddWithValue("@prenom", employeeInfo.prenom);
                        command.Parameters.AddWithValue("@poste", employeeInfo.poste);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            employeeInfo.nom = "";
            employeeInfo.prenom = "";
            employeeInfo.poste = "";
            succesMessage = "Ajout de l'employé réussi";
            Response.Redirect("/Employees/Index");
        }
    }
}
