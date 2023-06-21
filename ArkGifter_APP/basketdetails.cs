using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ArkGifter_APP
{
    public class BasketDetails
    {
        public int details_id { get; set; }
        public int basket_id { get; set; }
        public int product_id { get; set; }
        public int quantity { get; set; }

        public void DisplayDetails()
        {
            Console.WriteLine($"Details ID: {details_id}");
            Console.WriteLine($"Basket ID: {basket_id}");
            Console.WriteLine($"Product ID: {product_id}");
            Console.WriteLine($"Quantity: {quantity}");
            Console.WriteLine();
        }

        public static List<BasketDetails> RetrieveBasketDetails(SqlConnection sqlConnection)
        {
            List<BasketDetails> basketDetails = new List<BasketDetails>();

            // SQL query to retrieve basket details
            string query = "SELECT * FROM BasketDetails";

            using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
            {
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Create a new BasketDetails object and populate its properties from the database
                        BasketDetails details = new BasketDetails
                        {
                            details_id = Convert.ToInt32(reader["details_id"]),
                            basket_id = Convert.ToInt32(reader["basket_id"]),
                            product_id = Convert.ToInt32(reader["product_id"]),
                            quantity = Convert.ToInt32(reader["quantity"])
                        };

                        // Add the BasketDetails object to the list
                        basketDetails.Add(details);
                    }
                }
            }

            return basketDetails;
        }
    }
}
