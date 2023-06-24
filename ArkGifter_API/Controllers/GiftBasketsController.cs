// GiftBasketsController.cs
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;
using ArkGifter_API; // Add this using directive

namespace ArkGifter_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GiftBasketsController : ControllerBase
    {
        private readonly string _connectionString;

        public GiftBasketsController()
        {
            string serverName = @"INSPERIC23\SQLEXPRESS";
            string databaseName = "Ark_Gifter";
            _connectionString = $"data source={serverName}; database={databaseName}; Integrated Security=true;";
        }

        [HttpGet]
        public ActionResult<IEnumerable<GiftBasketWithTotalCost>> GetGiftBaskets()
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                List<GiftBasketWithTotalCost> giftBaskets = GiftBasketWithTotalCost.RetrieveGiftBasketsWithTotalCost(sqlConnection);
                return Ok(giftBaskets);
            }
        }
    }
}
