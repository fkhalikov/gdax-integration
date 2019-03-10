using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using GDAX.API.Configuration;
using Boukenken.Gdax;
using Newtonsoft.Json;
using GDAX.Models;
using System.IO;
using GDAX.Models.Readers;
using GDAXQuerySystem.Models;

namespace GDAXQuerySystem.Reader
{
    public class TradeReader : ITradeReader
    {

        int lastTradeId = 0;

        public TradeReader()
        {
        
        }

        

        public List<ITradeInfo> Read()
        {
            GDAXConfiguration config = new GDAXConfiguration();

            ProductClient productClient = new ProductClient(config.Url, new RequestAuthenticator(config.Key, config.Passphrase, config.Secret));

            var res = productClient.GetResponseAsync(new ApiRequest(System.Net.Http.HttpMethod.Get, System.Configuration.ConfigurationManager.AppSettings["productEndPoint"]));
            var resp = res.Result;
            var datat = resp.Content.ReadAsStringAsync();
            var data = datat.Result;
            
            var listData = JsonConvert.DeserializeObject<List<TradeInfo>>(data);

            List<ITradeInfo> trades = new List<ITradeInfo>();

            foreach (var trd in listData.Where(x => x.trade_id > lastTradeId))
            {
                //string strData = $"{trd.time},{trd.side},{trd.price},{trd.size},{trd.trade_id}";

                ///Take last one if lastTradeId is not equal to 0
                if ((lastTradeId == 0)==false
                    || trd.trade_id == listData.Max(x => x.trade_id))
                {
                    trades.Add(trd);
                }
            }

            lastTradeId = listData.Max(x => x.trade_id);

            return trades;
        }
    }
}
