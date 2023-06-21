using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ArkGifter_APP
{
    public class Product
    {
        public int product_id { get; set; }
        public string? handle { get; set; }
        public string? product_name { get; set; }
        public int vendor_id { get; set; }
        public string? product_type { get; set; }
        public decimal price { get; set; }
        public string? barcode { get; set; }
        public string? image_src { get; set; }

        public void DisplayProductInfo()
        {
            Console.WriteLine($"Product ID: {product_id}");
            Console.WriteLine($"Handle: {handle}");
            Console.WriteLine($"Product Name: {product_name}");
            Console.WriteLine($"Vendor ID: {vendor_id}");
            Console.WriteLine($"Product Type: {product_type}");
            Console.WriteLine($"Price: {price}");
            Console.WriteLine($"Barcode: {barcode}");
            Console.WriteLine($"Image Source: {image_src}");
        }
        public static List<Product> RetrieveProducts(string connectionString)
        {
            List<Product> products = new List<Product>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Products";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product();
                    product.product_id = Convert.ToInt32(reader["product_id"]);
                    product.handle = reader["handle"] as string;
                    product.product_name = reader["product_name"] as string;
                    product.vendor_id = Convert.ToInt32(reader["vendor_id"]);
                    product.product_type = reader["product_type"] as string;
                    product.price = Convert.ToDecimal(reader["price"]);
                    product.barcode = reader["barcode"] as string;
                    product.image_src = reader["image_src"] as string;

                    products.Add(product);
                }

                reader.Close();
            }

            return products;
        }
    }
}