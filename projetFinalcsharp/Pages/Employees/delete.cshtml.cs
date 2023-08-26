using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using projetFinalcsharp.Pages.MethodeConnection;

namespace projetFinalcsharp.Pages.Employees
{
    public class deleteModel : PageModel
    {
        public bool Supprimer()
        {
            try
            {
                String id = Request.Query["id"];

                BDConnection con = new BDConnection();
                MySqlConnection connection = con.GetConnection();
                {
                    connection.Open();
                    String sqlQuery = "DELETE FROM Employer WHERE idEmployer=@id;";
                    using (MySqlCommand command = new MySqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return true;
        }
        public void OnGet()
        {
        }
    }
}
