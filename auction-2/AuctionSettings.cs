using System;
using System.Configuration;
using auction;

namespace Auction
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
            MinSaleDuration = TimeSpan.FromSeconds(Double.Parse(
                ConfigurationManager.AppSettings[AuctionConfigurationStrings.MinSaleDurationInSec]));
        }
    }
}
