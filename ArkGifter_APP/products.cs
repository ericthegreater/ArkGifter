using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ArkGifter_APP
{
    public class Products
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
    }
}