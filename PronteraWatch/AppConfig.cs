using System;
using System.Collections.Generic;
using System.Text;

namespace PronteraWatch
{
    public class AppConfig
    {
        public string DiscordToken { get; set; }
        public ulong[] ChannelWhitelist { get; set; }
        public bool EnableKafraCommand { get; set; }
    }
}
