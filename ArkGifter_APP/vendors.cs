using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ArkGifter_APP
{
    public class Vendor
    {
        public int vendor_id { get; set; }
        public string vendor_name { get; set; }
        public string vendor_city { get; set; }
        public bool separate_distributor { get; set; }
        public string distributor { get; set; }

        public void DisplayVendorInfo()
        {
            Console.WriteLine($"Vendor ID: {vendor_id}");
            Console.WriteLine($"Vendor Name: {vendor_name}");
            Console.WriteLine($"Vendor City: {vendor_city}");
            Console.WriteLine($"Separate Distributor: {separate_distributor}");
            Console.WriteLine($"Distributor: {distributor}");
            Console.WriteLine();
        }

        public static List<Vendor> RetrieveVendors(SqlConnection sqlConnection)
        {
            List<Vendor> vendors = new List<Vendor>();

            // SQL query to retrieve vendor information
            string query = "SELECT * FROM Vendors";

            using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
            {
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Create a new Vendor object and populate its properties from the database
                        Vendor vendor = new Vendor
                        {
                            vendor_id = Convert.ToInt32(reader["vendor_id"]),
                            vendor_name = reader["vendor_name"].ToString(),
                            vendor_city = reader["vendor_city"].ToString(),
                            separate_distributor = Convert.ToBoolean(reader["separate_distributor"]),
                            distributor = reader["distributor"].ToString()
                        };

                        // Add the Vendor object to the list
                        vendors.Add(vendor);
                    }
                }
            }

            return vendors;
        }
    }
}
