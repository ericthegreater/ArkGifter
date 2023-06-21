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
        }
    }
}