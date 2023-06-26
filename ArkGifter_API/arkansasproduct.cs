using System;
using System.Data.SqlClient;

namespace ArkGifter_API
{
    public class ArkansasProduct
    {
        private readonly string _connectionString;

        public ArkansasProduct(string? connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public int ProductID { get; set; }
        public string? Maker { get; set; }
        public string? Product { get; set; }
        public decimal Price { get; set; }

        public void Create()
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                // Retrieve the vendor_id based on the vendor name
                int vendorId;
                string vendorIdQuery = "SELECT Vendor_ID FROM Vendors WHERE Vendor_Name = @VendorName";
                using (SqlCommand vendorIdCommand = new SqlCommand(vendorIdQuery, sqlConnection))
                {
                    vendorIdCommand.Parameters.AddWithValue("@VendorName", Maker);
                    object vendorIdResult = vendorIdCommand.ExecuteScalar();
                    if (vendorIdResult == null || vendorIdResult == DBNull.Value)
                    {
                        throw new InvalidOperationException("Invalid vendor name");
                    }
                    vendorId = Convert.ToInt32(vendorIdResult);
                }

                string insertQuery = @"
                    INSERT INTO Products (Product_ID, Vendor_ID, Product_Name, Price)
                    VALUES ((SELECT Product_ID, Vendor_ID FROM Vendors WHERE Vendor_Name = @Maker), @Product, @Price)
                ";

                using (SqlCommand sqlCommand = new SqlCommand(insertQuery, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@Product_ID", ProductID);
                    sqlCommand.Parameters.AddWithValue("@Maker", Maker);
                    sqlCommand.Parameters.AddWithValue("@Product", Product);
                    sqlCommand.Parameters.AddWithValue("@Price", Price);

                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
//uhhh i guess this is in the controller now
// public void Update()
// {
//     using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
//     {
//         sqlConnection.Open();

//         // Retrieve the vendor_id based on the vendor name
//         int vendorId;
//         string vendorIdQuery = "SELECT Vendor_ID FROM Vendors WHERE Vendor_Name = @VendorName";
//         using (SqlCommand vendorIdCommand = new SqlCommand(vendorIdQuery, sqlConnection))
//         {
//             vendorIdCommand.Parameters.AddWithValue("@VendorName", Maker);
//             object vendorIdResult = vendorIdCommand.ExecuteScalar();
//             if (vendorIdResult == null || vendorIdResult == DBNull.Value)
//             {
//                 throw new InvalidOperationException("Invalid vendor name");
//             }
//             vendorId = Convert.ToInt32(vendorIdResult);
//         }

//         string updateQuery = @"
//             UPDATE Products
//             SET Vendor_ID = @VendorId, Product_Name = @Product, Price = @Price
//             WHERE Product_ID = @ProductID
//         ";

//         using (SqlCommand sqlCommand = new SqlCommand(updateQuery, sqlConnection))
//         {
//             sqlCommand.Parameters.AddWithValue("@VendorId", vendorId);
//             sqlCommand.Parameters.AddWithValue("@Product", Product);
//             sqlCommand.Parameters.AddWithValue("@Price", Price);
//             sqlCommand.Parameters.AddWithValue("@ProductID", ProductID);

//             int rowsAffected = sqlCommand.ExecuteNonQuery();

//             if (rowsAffected > 0)
//             {
//                 Console.WriteLine("Product updated successfully");
//             }
//             else
//             {
//                 Console.WriteLine("Product update failed");
//             }
//         }
//     }
// }



//i don't think i ended up using this delete method
        public void Delete()
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                string deleteQuery = @"
                    DELETE FROM Products
                    WHERE -- Specify the condition for deleting a specific record
                ";

                using (SqlCommand sqlCommand = new SqlCommand(deleteQuery, sqlConnection))
                {
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
    }
}
