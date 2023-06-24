// GiftBasketWithTotalCost.cs
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ArkGifter_API
{
    public class GiftBasketWithTotalCost
    {
        public int BasketId { get; set; }
        public string? BasketName { get; set; }
        public string? BasketSummary { get; set; }
        public decimal TotalCost { get; set; }

        public static List<GiftBasketWithTotalCost> RetrieveGiftBasketsWithTotalCost(SqlConnection sqlConnection)
        {
            List<GiftBasketWithTotalCost> giftBaskets = new List<GiftBasketWithTotalCost>();

//query to get cost of individual items +5 charge (basket cost+handling)
            string query = @"
                SELECT gb.basket_id, gb.basket_name, gb.basket_summary, SUM(bd.quantity * p.price) + 5 AS total_cost 
                FROM GiftBaskets gb
                JOIN BasketDetails bd ON gb.basket_id = bd.basket_id
                JOIN Products p ON bd.product_id = p.product_id
                GROUP BY gb.basket_id, gb.basket_name, gb.basket_summary
            ";

            using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
            {
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        GiftBasketWithTotalCost basket = new GiftBasketWithTotalCost
                        {
                            BasketId = reader.GetInt32(reader.GetOrdinal("basket_id")),
                            BasketName = reader.GetString(reader.GetOrdinal("basket_name")),
                            BasketSummary = reader.GetString(reader.GetOrdinal("basket_summary")),
                            TotalCost = reader.GetDecimal(reader.GetOrdinal("total_cost"))
                        };

                        giftBaskets.Add(basket);
                    }
                }
            }

            return giftBaskets;
        }
    }
}
