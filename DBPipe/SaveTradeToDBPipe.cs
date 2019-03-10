using GDAX.Models;
using System;
using System.Data.SqlClient;

namespace SaveTradeToDB
{
    public class SaveTradeToDBPipe : IPipe
    {
        public ITradeInfo Process(ITradeInfo tInfo)
        {
            SqlConnection sqlConnection = new SqlConnection("Server=LENOVOPCBK;Database=gdax;Trusted_Connection=Yes;");

            sqlConnection.Open();
            try
            {
                SqlCommand cmd = sqlConnection.CreateCommand();

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.Configuration.ConfigurationManager.AppSettings["productSaveProc"];
                cmd.Parameters.Add(new SqlParameter("@time", tInfo.Time));
                cmd.Parameters.Add(new SqlParameter("@trade_id", tInfo.TradeId));
                cmd.Parameters.Add(new SqlParameter("@price", tInfo.Price));
                cmd.Parameters.Add(new SqlParameter("@size", tInfo.Size));
                cmd.Parameters.Add(new SqlParameter("@marketside", tInfo.Side == TradeSide.Buy ? "sell" : "buy"));

                cmd.ExecuteNonQuery();
                cmd.Dispose();
            } finally
            {
                sqlConnection.Close();
            }

            return tInfo;
        }
    }
}

