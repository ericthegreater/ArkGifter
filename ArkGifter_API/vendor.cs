using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ArkGifter_API
{
    public class Vendor
    {
        public int vendor_id { get; set; }
        public string? vendor_name { get; set; }
        public string? vendor_city { get; set; }
        public bool separate_distributor { get; set; }
        public string? distributor { get; set; }

        public void DisplayVendorInfo()
        {
            Console.WriteLine($"Vendor ID: {vendor_id}");
            Console.WriteLine($"Vendor Name: {vendor_name}");
            Console.WriteLine($"Vendor City: {vendor_city}");
            Console.WriteLine($"Separate Distributor: {separate_distributor}");
            Console.WriteLine($"Distributor: {distributor}");
            Console.WriteLine();
        }
        public static List<Vendor> GetVendors(SqlConnection sqlConnection)
        {
            List<Vendor> vendors = new List<Vendor>();

            string query = "SELECT Vendor_ID, Vendor_Name, Vendor_City, Separate_Distributor, Distributor FROM Ark_Gifter.dbo.Vendors";

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.Text;

            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                Vendor vendor = new Vendor();

                vendor.vendor_id = Convert.ToInt32(reader["Vendor_ID"]);
                vendor.vendor_name = reader["Vendor_Name"] != DBNull.Value ? reader["Vendor_Name"].ToString() : null;
                vendor.vendor_city = reader["Vendor_City"] != DBNull.Value ? reader["Vendor_City"].ToString() : null;
                vendor.separate_distributor = Convert.ToBoolean(reader["Separate_Distributor"]);
                vendor.distributor = reader["Distributor"] != DBNull.Value ? reader["Distributor"].ToString() : null;

                vendors.Add(vendor);
            }

            return vendors;
        }
    }
}