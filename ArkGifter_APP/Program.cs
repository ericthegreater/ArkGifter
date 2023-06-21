using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ArkGifter_APP
{
    class Program
    {
        static void Main(string[] args)
        {
            string serverName = @"INSPERIC23\SQLEXPRESS";
            string databaseName = "Ark_Gifter"; 
            string connectionString = $"data source={serverName}; database={databaseName}; Integrated Security=true;";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                List<Vendor> vendors = Vendor.RetrieveVendors(sqlConnection);
                foreach (Vendor vendor in vendors)
                {
                    vendor.DisplayVendorInfo();
                }


                List<BasketDetails> basketDetails = BasketDetails.RetrieveBasketDetails(sqlConnection);
//this code was to test the functionality of the basket details creation.  at the current time there are no baskets in the database(the app has yet to create any, so the following code produced a writeline with no data. perfect!)
                // Console.WriteLine("Basket Details:");
                // foreach (var details in basketDetails)
                // {
                //     details.DisplayDetails();
                // }
        
                List<Product> products = Product.RetrieveProducts(connectionString);

//this lists every single product to the console. it works! yay! nulls and all!
                // foreach (Product product in products)
                // {
                //     product.DisplayProductInfo();
                //     Console.WriteLine();
                // }


//and lastly we call the baskets from the database.... i'm leaving the writing loop intact so that 
                List<GiftBaskets> giftBaskets = GiftBaskets.RetrieveGiftBaskets(sqlConnection);
                foreach (GiftBaskets basket in giftBaskets)
                {
                    basket.DisplayBasketInfo();
                }

                
	        }
       }
    }
}