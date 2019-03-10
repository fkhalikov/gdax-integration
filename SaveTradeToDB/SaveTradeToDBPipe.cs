using GDAX.Models;
using System;
using System.Data.SqlClient;

namespace SaveTradeToDB
{
    public class SaveTradeToDBPipe : IPipe
    {
        public void Process(TradeInfo tInfo)
        {
            SqlConnection sqlConnection = new SqlConnection("Server=LENOVOPCBK;Database=gdax;Trusted_Connection=Yes;");

            sqlConnection.Open();

            SqlCommand cmd = sqlConnection.CreateCommand();
        }
    }
}
