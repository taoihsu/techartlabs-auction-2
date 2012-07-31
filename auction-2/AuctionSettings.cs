using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace auction_2
{
    public class AuctionSettings
    {
        public TimeSpan MinSaleDuration { get; private set; }

        public AuctionSettings()
        {
            ReLoadConfig();
        }

        public void ReLoadConfig()
        {
            MinSaleDuration = TimeSpan.FromSeconds(Double.Parse(ConfigurationManager.AppSettings["MinSaleDurationInSeconds"]));
        }
    }
}
