using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using projetFinalcsharp.Pages.MethodeConnection;
using System;

namespace projetFinalcsharp.Pages.Employees
{
    public class EditModel : PageModel
    {
        public EmployeeInfo employeeInfo = new EmployeeInfo();
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
                    string sqlQuery = "SELECT * FROM Employer WHERE idEmployer=@id";
                    using (MySqlCommand command = new MySqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                employeeInfo.id = reader.GetInt32(0);
                                employeeInfo.nom = reader.GetString(1);
                                employeeInfo.prenom = reader.GetString(2);
                                employeeInfo.poste = reader.GetString(3);
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
                employeeInfo.id = id;
            }
            employeeInfo.nom = Request.Form["nom"];
            employeeInfo.prenom = Request.Form["prenom"];
            employeeInfo.poste = Request.Form["poste"];

            if (employeeInfo.id == 0 || employeeInfo.nom.Length == 0 || employeeInfo.prenom.Length == 0 || employeeInfo.poste.Length == 0)
            {
                errorMessage = "Tous les formulaires doivent être complétés";
                return;
            }

            try
            {
                BDConnection con = new BDConnection();
                MySqlConnection connection = con.GetConnection();
                {
                    connection.Open();
                    string sqlQuery = "UPDATE Employer SET nom=@nom, prenom=@prenom, poste=@poste WHERE idEmployer=@id;";
                    using (MySqlCommand command = new MySqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@nom", employeeInfo.nom);
                        command.Parameters.AddWithValue("@prenom", employeeInfo.prenom);
                        command.Parameters.AddWithValue("@poste", employeeInfo.poste);
                        command.Parameters.AddWithValue("@id", employeeInfo.id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Employees/Index");
        }
    }
}
