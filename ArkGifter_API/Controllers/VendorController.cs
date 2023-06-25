using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ArkGifter_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendorController : ControllerBase
    {
        private readonly string _connectionString;

        public VendorController()
        {
            string serverName = @"INSPERIC23\SQLEXPRESS";
            string databaseName = "Ark_Gifter";
            _connectionString = $"data source={serverName}; database={databaseName}; Integrated Security=true;";
        }

        [HttpGet]
        public ActionResult<IEnumerable<Vendor>> GetVendors()
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                List<Vendor> vendors = Vendor.GetVendors(sqlConnection);
                return Ok(vendors);
            }
        }
    }
}
