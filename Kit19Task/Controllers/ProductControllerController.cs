using Kit19Task.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Web.Mvc;
using Kit19Task.Models;

namespace Kit19Task.Controllers
{
    public class ProductController : Controller
    {
        private string connectionString = "Data Source=DESKTOP-SMV24UQ;Initial Catalog=tempdb;Integrated Security=True";

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(SearchCriteriaModel criteria)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand("SearchMoProducts", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProductName", criteria.ProductName ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Size", criteria.Size ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Price", criteria.Price ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@MfgDate", criteria.MfgDate ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Category", criteria.Category ?? (object)DBNull.Value);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            List<MoProduct> products = new List<MoProduct>();
                            while (reader.Read())
                            {
                                MoProduct product = new MoProduct
                                {
                                    ProductName = reader["ProductName"].ToString(),
                                    Size = reader["Size"].ToString(),
                                    Price = (decimal)reader["Price"],
                                    MfgDate = (DateTime)reader["MfgDate"],
                                    Category = reader["Category"].ToString()
                                };
                                products.Add(product);
                            }
                            //return View("SearchResults", products);
                            return View(products);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, log errors, or display an error view.
                return View("ErrorView");
            }
        }
    }
}
