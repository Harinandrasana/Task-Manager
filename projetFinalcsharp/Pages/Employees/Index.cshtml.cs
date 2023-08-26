using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using projetFinalcsharp.Pages.Employees;
using projetFinalcsharp.Pages.Employees.MethodeEmploye;
using projetFinalcsharp.Pages.MethodeConnection;
using projetFinalcsharp.Pages.Taches.Methode;
using System;
using System.Collections.Generic;

namespace projetFinalcsharp.Pages.Employees
{
    public class IndexModel : PageModel
    {
        public List<EmployeeInfo> listEmployees = new List<EmployeeInfo>();

        public void OnPostSearch(string idEmployer)
        {
            EmployeSearch employeSearch = new EmployeSearch();
            employeSearch.rechercher(idEmployer, listEmployees);

        }

        public void OnGet()
        {
            try
            {
                BDConnection con = new BDConnection();
                MySqlConnection connection = con.GetConnection();
                {
                    connection.Open();
                    string sqlQuery = "SELECT * FROM employer ORDER BY idEmployer ASC";
                    using (MySqlCommand command = new MySqlCommand(sqlQuery, connection))
                    {
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

        public void rechercher(string idEmployer)
        {
            try
            {
                BDConnection con = new BDConnection();
                MySqlConnection connection = con.GetConnection();
                {
                    connection.Open();
                    string sqlQuery = "SELECT * FROM employer WHERE idEmployer like @rech or nom like @rech";
                    using (MySqlCommand command = new MySqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@rech", '%'+idEmployer+'%');
                        

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




/*
 using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using projetFinalcsharp.Pages.Employees;
using projetFinalcsharp.Pages.MethodeConnection;
using System;
using System.Collections.Generic;

namespace projetFinalcsharp.Pages.Employees
{
    public class IndexModel : PageModel
    {
        public List<EmployeeInfo> listEmployees = new List<EmployeeInfo>();

        public void OnGet(string searchName)
        {
            try
            {
                BDConnection con = new BDConnection();
                MySqlConnection connection = con.GetConnection();
                {
                    connection.Open();

                    string sqlQuery = "SELECT * FROM employer ORDER BY idEmployer ASC";
                    if (!string.IsNullOrEmpty(searchName))
                    {
                        sqlQuery = "SELECT * FROM employer WHERE nom LIKE @searchName ORDER BY idEmployer ASC";
                    }

                    using (MySqlCommand command = new MySqlCommand(sqlQuery, connection))
                    {
                        if (!string.IsNullOrEmpty(searchName))
                        {
                            command.Parameters.AddWithValue("@searchName", $"%{searchName}%");
                        }

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

 */