using GDAX.Models;
using GDAX.Models.Pipe;
using GDAXQuerySystem.Reader;
using SaveTradeToDB;
using System;
using System.Timers;

namespace GDAX.FeedReader
{
    class Program
    {

        static Timer tmr;
        
        static Pipeline tPipe = new Pipeline();
        private static TradeReader trReader;

        static void Main(string[] args)
        {
            trReader = new TradeReader();

            tPipe.Add(new SaveTradeToDBPipe());

            tmr = new Timer();
            tmr.Elapsed += Tmr_Elapsed;
            tmr.Interval = 1000;
            tmr.Start();
            
            Console.ReadLine();
        }

        private static void Tmr_Elapsed(object sender, ElapsedEventArgs e)
        {
            tmr.Enabled = false;


            try
            {
               var trades = trReader.Read();

                foreach(var trade in trades)
                {
                    TrReader_OnNewTrade(trade);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {

            }
        }

        private static void TrReader_OnNewTrade(ITradeInfo trd)
        {
            Console.WriteLine($"{trd.Time},{ trd.Side},{ trd.Price},{ trd.Size},{ trd.TradeId} ");

            tPipe.Process(trd);
        }

        //static void startFeed()
        //{
        //    var jsonSerializer = new Newtonsoft.Json.JsonSerializer();

        //    ClientWebSocket socket = new ClientWebSocket();
        //    Task task = socket.ConnectAsync(new Uri("wss://ws-feed.gdax.com"), CancellationToken.None);
        //    task.Wait();

        //    Action delToRun = () =>
        //    {
        //        byte[] recBytes = new byte[1024];
        //        while (true)
        //        {
        //            try
        //            {
        //                ArraySegment<byte> t = new ArraySegment<byte>(recBytes);
        //                Task<WebSocketReceiveResult> receiveAsync = socket.ReceiveAsync(t, CancellationToken.None);
        //                receiveAsync.Wait();
        //                string jsonString = Encoding.UTF8.GetString(recBytes);

        //                var ti = JsonConvert.DeserializeObject<TrackerInfo>(jsonString);

        //                string tradeData = $"{ti.trade_id},{ti.product_id},{ti.side},{ti.price},{ti.last_size},{ti.time}";

        //                Console.Out.WriteLine(tradeData);
        //                File.AppendAllText(@"c:\gdax\data\data.csv", tradeData + Environment.NewLine);
        //                File.AppendAllText(@"c:\gdax\data\dataraw.txt", jsonString + Environment.NewLine);

        //                recBytes = new byte[1024];
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.Out.WriteLine("Failed");

        //                File.AppendAllText(@"c:\gdax\data\error.txt", DateTime.Now.ToString() + " " + ex.ToString() + Environment.NewLine + Environment.NewLine);

        //                try
        //                {
        //                    Task tsk = socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "t", new CancellationToken(true));
        //                    tsk.Wait(5000);
        //                }
        //                catch (Exception ex1)
        //                {
        //                    File.AppendAllText(@"c:\gdax\data\error.txt", DateTime.Now.ToString() + " " + ex1.ToString() + Environment.NewLine + Environment.NewLine);
        //                }

        //                socket = new ClientWebSocket();
        //                task = socket.ConnectAsync(new Uri("wss://ws-feed.gdax.com"), CancellationToken.None);
        //                task.Wait();
        //            }
        //        }
        //    };

        //    Thread readThread = new Thread(
        //        delegate (object obj)
        //        {

        //                delToRun();

        //        });

        //    readThread.Start();

        //    string json = "{\"product_ids\":[\"ETH-EUR\"],\"type\":\"subscribe\",\"channels\": [{\"name\": \"ticker\",\"product_ids\": [\"ETH-EUR\"]}]}";
        //    byte[] bytes = Encoding.UTF8.GetBytes(json);
        //    ArraySegment<byte> subscriptionMessageBuffer = new ArraySegment<byte>(bytes);
        //    socket.SendAsync(subscriptionMessageBuffer, WebSocketMessageType.Text, true, CancellationToken.None);
        //    Console.ReadLine();
        //}


    }
}
