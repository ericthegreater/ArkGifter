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
                    INSERT INTO Products (Vendor_ID, Product_Name, Price)
                    VALUES ((SELECT Vendor_ID FROM Vendors WHERE Vendor_Name = @Maker), @Product, @Price)
                ";

                using (SqlCommand sqlCommand = new SqlCommand(insertQuery, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@Maker", Maker);
                    sqlCommand.Parameters.AddWithValue("@Product", Product);
                    sqlCommand.Parameters.AddWithValue("@Price", Price);

                    sqlCommand.ExecuteNonQuery();
                }
            }
        }

        public void Update()
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

                string updateQuery = @"
                    UPDATE Products
                    SET Vendor_ID = @VendorId, Product_Name = @Product, Price = @Price
                    WHERE -- Specify the condition for updating a specific record
                ";

                using (SqlCommand sqlCommand = new SqlCommand(updateQuery, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@VendorId", vendorId);
                    sqlCommand.Parameters.AddWithValue("@Product", Product);
                    sqlCommand.Parameters.AddWithValue("@Price", Price);

                    sqlCommand.ExecuteNonQuery();
                }
            }
        }

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
