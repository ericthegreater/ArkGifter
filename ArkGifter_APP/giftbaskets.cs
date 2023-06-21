using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ArkGifter_APP
{
    public class GiftBaskets
    {
        public int basket_id { get; set; }
        public string? basket_name { get; set; }
        public string? basket_summary { get; set; }
        public void DisplayBasketInfo()
        {
            Console.WriteLine($"Basket ID: {basket_id}");
            Console.WriteLine($"Basket Name: {basket_name}");
            Console.WriteLine($"Basket Summary: {basket_summary}");
        }
    }
}