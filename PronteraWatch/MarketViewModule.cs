using Discord.Commands;
using LibProntera;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PronteraWatch
{
    public class MarketViewModule : ModuleBase<SocketCommandContext>
    {
        private KafraKrApi _kafraKrApi = new KafraKrApi(); 

        private string FormatNumber(double number)
        {
            return number == -1 ? "No data" : string.Format("{0:n} Zeny", number);
        }

        private string CompareTrend(double a, double b)
        {
            if (a == -1 || b == -1)
            {
                return "";
            }
            return a > b ? "📈" : "📉";
        }

        [Command("itemdata")]
        [Alias("i")]
        [Summary("View price data of item")]
        public async Task ItemDataAsync(
            [Summary("Item ID or Name")]
        [Remainder]string name)
        {
            if (!PronteraWatchApp._appConfig.EnableKafraCommand)
            {
                await Context.Channel.SendMessageAsync("kRO market viewer module is disabled for this PronteraWatch instance.");
                return;
            }

            uint id;
            bool isNumber = uint.TryParse(name, out id);
            if (!isNumber)
            {
                id = PronteraWatchApp.NameToIdDatabase.ResolveName(name);
            }

            var stat = await _kafraKrApi.GetItemStat((int)id);
            string msg = $"Item ID {stat.Id} - Name {stat.Name}\n" +
                            $"Past deals found - {stat.SoldDealsFound} deals\n" +
                            $"Past average price - {FormatNumber(stat.PastAveragePrice)}\n" +
                            $"Latest sold price - {FormatNumber(stat.LatestSoldPrice)} {CompareTrend(stat.LatestSoldPrice, stat.PastAveragePrice)}\n" +
                            $"Oldest sold price - {FormatNumber(stat.OldestSoldPrice)}\n" +
                            $"Latest sold time used - {stat.LatestSoldFound.ToString("yyyy-MM-dd HH:mm:ss")}\n" +
                            $"Oldest sold time used - {stat.OldestSoldFound.ToString("yyyy-MM-dd HH:mm:ss")}\n" +
                            $"Time diff between latest and oldest sold - {string.Format("{0:dd\\.hh\\:mm\\:ss}", stat.LatestSoldFound.Subtract(stat.OldestSoldFound))}\n" +
                            $"Current cheapest price (no buying shop) - {FormatNumber(stat.CurrentCheapestPrice)} {CompareTrend(stat.CurrentCheapestPrice, stat.PastAveragePrice)}\n" +
                            $"Current average price (no buying shop) - {FormatNumber(stat.CurrentAveragePrice)}\n" +
                            $"DP - https://divine-pride.net/database/item/{id}\n" +
                            $"Kafra - http://kafra.kr/#!/en/KRO/itemdetail/{id}";
            await Context.Channel.SendMessageAsync(msg);
        }
    }
}
