using System;

namespace GDAX.Models
{
    public class TrackerInfo
    {

        public string type { get; set; }

        public string sequence { get; set; }

        public string product_id { get; set; }

        public string price { get; set; }

        public string trade_id { get; set; }

        public string side { get; set; }///sell/buy

        public string last_size { get; set; }

        public string time { get; set; }

    }
}
