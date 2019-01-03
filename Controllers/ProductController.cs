using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using WebApi.Model;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class ProductController : Controller
    {
        public string GetConnectionString()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = "irynaserver.database.windows.net";
            builder.UserID = "irynamoiseeva";
            builder.Password = "Simcorp66!";
            builder.InitialCatalog = "customdb";

            return builder.ConnectionString;

        }

        [HttpGet]
        public IEnumerable<Catalog> GetCatalog()
        {
            List<Catalog> listCatalog = new List<Catalog>();

                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    connection.Open();
                    var CommandText = "Select Id, Name, Price, PhotoUrl from dbo.Catalog";

                    using (SqlCommand command = new SqlCommand(CommandText, connection))
                    
                    using (SqlDataReader sqlreader = command.ExecuteReader())
             
                   {
                       while (sqlreader.Read())
                     {
                         var productItem = new Catalog();
                         productItem.Id = Convert.ToInt32(sqlreader["Id"]);
                         productItem.Name = sqlreader["Name"].ToString();
                         productItem.Price = Convert.ToDecimal(sqlreader["Price"]);
                         productItem.PhotoUrl = sqlreader["PhotoUrl"].ToString();
                         listCatalog.Add(productItem);
                     }
                   }
                
                }

            return listCatalog;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
