using GDAX.Models;
using GDAX.Models.Readers;
using SimulationTradeReader.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SimulationTradeReader
{
  public class SimulatedTradeReader : ITradeReader
    {

        int? lastTradeId = null;

        public List<ITradeInfo> Read()
        {
            SqlConnection sqlConnection = new SqlConnection("Server=LENOVOPCBK;Database=gdax;Trusted_Connection=Yes;");

            SqlCommand cmd = sqlConnection.CreateCommand();

      //cmd.CommandText = "simulation.GetData";
      //cmd.CommandText = "simulation.GetData3";

      cmd.CommandText = "simulation.[GetDataBitCoin]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@tradeid", lastTradeId ?? Convert.DBNull));

            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd);

            var data = new DataTable();
            
            dAdapter.Fill(data);
            sqlConnection.Close();
            
            try
            {
                List<ITradeInfo> result = new List<ITradeInfo>();

                foreach (DataRow row in data.Rows)
                {
                    ITradeInfo trdInfo = new SimulatedTradeInfo(row["date"], row["side"], row["price"], row["size"], row["trade_id"]);

                    // Thread.Sleep(200);
                    lastTradeId = trdInfo.TradeId;
                    result.Add(trdInfo);
                }

                return result;
            }
            finally
            {
                
            }

        }
    }
}
