using Boukenken.Gdax;
using GDAX.API.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TradingPlatform.Common;

namespace GDAXQuerySystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GDAXConfiguration config = new GDAXConfiguration();

            string method = "GET";
            string requestURI = "/accounts";
            string body = "";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(config.Url + requestURI);

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            int timestamp = TimeHelper.GetUnixTimeStamp();

            string signature = timestamp.ToString() + method + requestURI + body;
            byte[] secret64 = Convert.FromBase64String(config.Secret);

            HMACSHA256 hMACSHA256 = new HMACSHA256(secret64);
            var hash = Convert.ToBase64String(hMACSHA256.ComputeHash(ASCIIEncoding.ASCII.GetBytes(signature)));

            client.DefaultRequestHeaders.Add("CB-ACCESS-KEY", config.Key);
            client.DefaultRequestHeaders.Add("CB-ACCESS-SIGN", hash);
            client.DefaultRequestHeaders.Add("CB-ACCESS-TIMESTAMP", timestamp.ToString());
            client.DefaultRequestHeaders.Add("CB-ACCESS-PASSPHRASE", config.Passphrase);
            client.DefaultRequestHeaders.Add("User-Agent", "NA");

            // List data response.
            HttpResponseMessage response = client.GetAsync("").Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                txtbResult.Text = response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                txtbResult.Text = ($"{response.StatusCode} ({response.ReasonPhrase}) {response.Content.ReadAsStringAsync().Result}");
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

        }

       

        private async void btnHistoricRate_Click(object sender, RoutedEventArgs e)
        {
            GDAXConfiguration config = new GDAXConfiguration();

            ProductClient productClient = new ProductClient(config.Url, new RequestAuthenticator(config.Key, config.Passphrase, config.Secret));

            var products = await productClient.GetProductsAsync();

            txtbResult.Text = string.Join("" + Environment.NewLine, products.Value.Select(x => $"{x.id} {x.quote_currency} {x.quote_currency} {x.base_min_size} {x.quote_increment}"));


            var productData = await productClient.GetHistoricRatesAsync("BTC-EUR",DateTime.UtcNow.AddDays(-1), DateTime.UtcNow,60);

        }
    }
}
