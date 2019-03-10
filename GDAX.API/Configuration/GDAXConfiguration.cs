using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAX.API.Configuration
{
    public class GDAXConfiguration
    {
        public string Url
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["apiurl"];
            }
        }

        public string Key
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["key"];
            }
        }

        public string Passphrase { 
                get
            {
                return System.Configuration.ConfigurationManager.AppSettings["passphrase"];
            }
            }

        public string Secret
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["apisecret"];
            }
        }
        
    }
}
