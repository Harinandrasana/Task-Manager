using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Cmp;
using projetFinalcsharp.Pages.MethodeConnection;

namespace projetFinalcsharp.Pages.Taches
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
                    String sqlQuery = "DELETE FROM Tache WHERE idTache=@id;";
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
