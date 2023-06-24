using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;
using ArkGifter_API;

namespace ArkGifter_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly string _connectionString;

        public ProductController()
        {
            string serverName = @"INSPERIC23\SQLEXPRESS";
            string databaseName = "Ark_Gifter";
            _connectionString = $"data source={serverName}; database={databaseName}; Integrated Security=true;";
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                List<Product> products = Product.RetrieveProducts(_connectionString);
                return Ok(products);
            }
        }

        [HttpGet("arkansas")]
        public ActionResult<IEnumerable<ArkansasProduct>> GetArkansasProducts()
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                string query = @"
                    SELECT v.vendor_name AS Maker, p.product_name AS Product, p.price AS Price
                    FROM Products p
                    JOIN Vendors v ON p.vendor_id = v.vendor_id
                ";

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        List<ArkansasProduct> arkansasProducts = new List<ArkansasProduct>();

                        while (reader.Read())
                        {
                            ArkansasProduct product = new ArkansasProduct
                            {
                                Maker = reader.GetString(reader.GetOrdinal("Maker")),
                                Product = reader.GetString(reader.GetOrdinal("Product")),
                                Price = reader.GetDecimal(reader.GetOrdinal("Price"))
                            };

                            arkansasProducts.Add(product);
                        }

                        return Ok(arkansasProducts);
                    }
                }
            }
        }
    }
}
