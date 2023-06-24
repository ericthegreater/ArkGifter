using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ArkGifter_API
{
    public class ArkansasProduct
    {
        public string? Maker { get; set; }
        public string? Product { get; set; }
        public decimal Price { get; set; }
    }
}
