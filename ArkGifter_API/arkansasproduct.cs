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

                string insertQuery = @"
                    INSERT INTO ArkansasProducts (maker, product, price)
                    VALUES (@Maker, @Product, @Price)
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

                string updateQuery = @"
                    UPDATE ArkansasProducts
                    SET maker = @Maker, product = @Product, price = @Price
                    WHERE -- Specify the condition for updating a specific record
                ";

                using (SqlCommand sqlCommand = new SqlCommand(updateQuery, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@Maker", Maker);
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
                    DELETE FROM ArkansasProducts
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
