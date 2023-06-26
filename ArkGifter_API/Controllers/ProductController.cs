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
                    SELECT p.Product_ID as ProductID, v.vendor_name AS Maker, p.product_name AS Product, p.price AS Price
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
                            ArkansasProduct arkansasProduct = new ArkansasProduct(_connectionString)
                            {
                                ProductID = reader.GetInt32(reader.GetOrdinal("ProductID")),
                                Maker = reader.GetString(reader.GetOrdinal("Maker")),
                                Product = reader.GetString(reader.GetOrdinal("Product")),
                                Price = reader.GetDecimal(reader.GetOrdinal("Price"))
                            };

                            arkansasProducts.Add(arkansasProduct);
                        }

                        return Ok(arkansasProducts);
                    }
                }
            }
        }
        [HttpGet("create")]
        public ActionResult CreateProduct(string product, string maker, decimal price)
        {
            if (product == null)
            {
                return BadRequest("Invalid product data");
            }

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                string insertQuery = @"
            INSERT INTO Products (Vendor_ID, Product_Name, Price)
            SELECT V.Vendor_ID, @Product, @Price
            FROM Vendors V
            WHERE V.Vendor_Name = @Maker;
        ";

                using (SqlCommand sqlCommand = new SqlCommand(insertQuery, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@Maker", maker);
                    sqlCommand.Parameters.AddWithValue("@Product", product);
                    sqlCommand.Parameters.AddWithValue("@Price", price);

                    int rowsAffected = sqlCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok("Product created successfully");
                    }
                    else
                    {
                        return BadRequest("Failed to create the product");
                    }
                }
            }
        }



        [HttpDelete("{productID}")]
        public ActionResult DeleteProduct(string productID)
        {
            if (string.IsNullOrEmpty(productID))
            {
                return BadRequest("Invalid product ID");
            }

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                string deleteQuery = @"
            DELETE FROM Products
            WHERE Product_ID = @ProductID
        ";

                using (SqlCommand sqlCommand = new SqlCommand(deleteQuery, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@ProductID", productID);

                    int rowsAffected = sqlCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok($"Product '{productID}' deleted successfully");
                    }
                    else
                    {
                        return NotFound($"Product '{productID}' not found");
                    }
                }
            }
        }



        [HttpGet("update")]
        public ActionResult UpdateProduct(int productId, string product, string maker, decimal price)
        {
            if (product == null)
            {
                return BadRequest("Invalid product data");
            }

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                string updateQuery = @"
            UPDATE Products
            SET Product_Name = @Product,
                Vendor_ID = (SELECT V.Vendor_ID FROM Vendors V WHERE V.Vendor_Name = @Maker),
                Price = @Price
            WHERE Product_ID = @ProductId;
        ";

                using (SqlCommand sqlCommand = new SqlCommand(updateQuery, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@ProductId", productId);
                    sqlCommand.Parameters.AddWithValue("@Product", product);
                    sqlCommand.Parameters.AddWithValue("@Maker", maker);
                    sqlCommand.Parameters.AddWithValue("@Price", price);

                    int rowsAffected = sqlCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok("Product updated successfully");
                    }
                    else
                    {
                        return NotFound("Product not found");
                    }
                }
            }
        }
    }
}