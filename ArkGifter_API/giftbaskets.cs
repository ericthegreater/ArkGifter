using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ArkGifter_API
{
    public class GiftBaskets
    {
        public int basket_id { get; set; }
        public string? basket_name { get; set; }
        public string? basket_summary { get; set; }

        public void DisplayBasketInfo()
        {
            Console.WriteLine($"Basket ID: {basket_id}");
            Console.WriteLine($"Basket Name: {(basket_name ?? "N/A")}");
            Console.WriteLine($"Basket Summary: {(basket_summary ?? "N/A")}");
            Console.WriteLine();
        }

        public static List<GiftBaskets> RetrieveGiftBaskets(SqlConnection sqlConnection)
        {
            List<GiftBaskets> giftBaskets = new List<GiftBaskets>();

            // SQL query to retrieve gift basket information
            string query = "SELECT * FROM GiftBaskets";

            using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
            {
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Create a new GiftBaskets object and populate its properties from the database
                        GiftBaskets basket = new GiftBaskets
                        {
                            basket_id = Convert.ToInt32(reader["basket_id"]),
                            basket_name = reader["basket_name"] != DBNull.Value ? reader["basket_name"].ToString() : null,
                            basket_summary = reader["basket_summary"] != DBNull.Value ? reader["basket_summary"].ToString() : null
                        };

                        // Add the GiftBaskets object to the list
                        giftBaskets.Add(basket);
                    }
                }
            }

            return giftBaskets;
        }
    }
}
