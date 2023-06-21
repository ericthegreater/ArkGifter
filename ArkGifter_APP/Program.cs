﻿using System;
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


                
	        }
       }
    }
}