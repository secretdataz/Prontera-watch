using HtmlAgilityPack;
using LibProntera.KafraKr;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LibProntera
{
    public class KafraKrApi
    {
        public async Task<KafraItem> GetItem(int id)
        {
            using(var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(string.Format("http://api.kafra.kr/KRO/{0}/item.json", id));
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var json = JsonConvert.DeserializeObject<KafraItem[]>(responseBody);
                    if (json.Length > 0)
                        return json[0];
                    else
                        return null;
                }
                catch (HttpRequestException e)
                {
                    return null;
                }
            }
        }

        public async Task<List<KafraItemHistory>> GetItemHistory(KafraItem item, int page = 1)
        {
            using (var client = new HttpClient())
            {
                var url = string.Format("http://api.kafra.kr/itemdealhistory.json?svr_const=129&type=8&q=\"{0}\"&page=1&ver=2022011901", item.screen_name);
                //                       http://api.kafra.kr/itemdealhistory.json?svr_const=129&type=8&slot_num=2_2&q=%22%EB%8D%B0%EB%AA%A8%EB%8B%89%20%ED%81%AC%EB%A1%9C%22&page=1&ver=2022011901
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                KafraItemHistory[] json = JsonConvert.DeserializeObject<KafraItemHistory[]>(responseBody);
                var result = json.ToList();
                result.RemoveAll(x => x.item_const != item.item_const);

                return result;
            }
        }

        public async Task<int> CountItemDealPages(KafraItem item)
        {
            using (var client = new HttpClient())
            {
                int Proxy = new Random().Next(1, 20);
                var url = "http://proxy{0}.kafra.kr/?url=https%3A%2F%2Fro.gnjoy.com%2FitemDeal%2FitemDealList.asp%3FsvrID%3D129%26itemFullName%3D{1}%26itemOrder%3D%26inclusion%3D%26curpage%3D1&ver=2022011901";
                url = String.Format(url, Proxy, item.screen_name);
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(responseBody);

                var pagingNav = htmlDoc.DocumentNode.SelectSingleNode("//span[@class='pagingNav'][1]");
                if (pagingNav == null)
                {
                    return 1;
                }

                var pageLastLink = pagingNav.SelectSingleNode("//a[@class='pageNav_last']");
                if (pageLastLink == null)
                {
                    return 1;
                }
                var href = pageLastLink.GetAttributeValue("href", "");
                Regex rg = new Regex("\\,(\\d+)\\)");
                var matches = rg.Matches(href);
                if (matches.Count == 0)
                {
                    return 1;
                }

                return int.Parse(matches[0].Groups[1].Value);
            }
        }

        public async Task<List<KafraLiveItemDeal>> GetItemDeals(KafraItem item, int page = 1)
        {
            using (var client = new HttpClient())
            {
                int Proxy = new Random().Next(1, 20);
                var url = "http://proxy{0}.kafra.kr/?url=https%3A%2F%2Fro.gnjoy.com%2FitemDeal%2FitemDealList.asp%3FsvrID%3D129%26itemFullName%3D{1}%26itemOrder%3D%26inclusion%3D%26curpage%3D{2}&ver=2022011901";
                url = String.Format(url, Proxy, item.screen_name, page);
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                List<KafraLiveItemDeal> deals = new List<KafraLiveItemDeal>();
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(responseBody);

                var table = htmlDoc.DocumentNode.SelectNodes("//div/table/tbody/tr");
                foreach(var tr in table)
                {
                    var imgNode = tr.SelectSingleNode("td/a/img");
                    var qtyNode = tr.SelectSingleNode("td[@class='quantity'][1]");
                    var priceNode = tr.SelectSingleNode("td[@class='price']/span");
                    var isBuyShop = tr.SelectSingleNode("td[@class='shop buy']");

                    if (isBuyShop != null)
                    {
                        continue;
                    }

                    if (imgNode == null || qtyNode == null || priceNode == null)
                    {
                        continue;
                    }

                    // Extract item ID
                    var imgUrl = imgNode.GetAttributeValue("src", "");
                    Regex rg = new Regex("201306\\/(\\d+).png");
                    var matches = rg.Matches(imgUrl);
                    if (matches.Count == 0)
                    {
                        continue;
                    }
                    int itemId = int.Parse(matches[0].Groups[1].Value);
                    if (itemId != item.item_const)
                    {
                        continue;
                    }

                    int qty = int.Parse(qtyNode.InnerHtml);
                    int price = int.Parse(priceNode.InnerHtml.Replace(",", ""));
                    var deal = new KafraLiveItemDeal()
                    {
                        Id = itemId,
                        Quantity = qty,
                        Price = price,
                        Server = "Baphomet", // TODO: Only works for Bapho anyways
                    };
                    deals.Add(deal);
                }

                return deals;
            }
        }

        public async Task<Item> GetItemStat(int id, int limit = 200, int days = 365)
        {
            var item = await GetItem(id);
            int[] pages = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
            var tasks = pages.ToList().Select(x => GetItemHistory(item, x)).ToArray();
            Task.WaitAll(tasks);
            List<KafraItemHistory> finalList = new List<KafraItemHistory>();
            tasks.Select(x => x.Result).ToList().ForEach(x => finalList.AddRange(x));
            finalList.Sort((x, y) => y.dealdate - x.dealdate);
            var now = DateTimeOffset.Now.ToUnixTimeSeconds();
            var secondsSinceSpecifiedDay = 60 * 60 * 24 * days;
            finalList.RemoveAll(x => x.dealdate < now - secondsSinceSpecifiedDay);
            finalList = finalList.Take(limit).ToList();

            List<KafraLiveItemDeal> deals = new List<KafraLiveItemDeal>();
            List<Task<List<KafraLiveItemDeal>>> liveTasks = new List<Task<List<KafraLiveItemDeal>>>();
            int availableLiveDealPage = await CountItemDealPages(item);
            for(int i = 1; i <= 5 && i <= availableLiveDealPage; ++i)
            {
                liveTasks.Add(GetItemDeals(item, i));
            }
            Task.WaitAll(liveTasks.ToArray());
            liveTasks.Select(x => x.Result).ToList().ForEach(x => deals.AddRange(x));
            deals.Sort((x, y) => x.Price - y.Price);

            Item itemStat = new Item()
            {
                Id = item.item_const,
                Name = item.screen_name,
                PastAveragePrice = finalList.Count > 0 ? finalList.Average(x => x.itemprice) : -1,
                LatestSoldPrice = finalList.Count > 0 ? finalList[0].itemprice : -1,
                OldestSoldPrice = finalList.Count > 0 ? finalList[finalList.Count - 1].itemprice : -1,
                OldestSoldFound = finalList.Count > 0 ? DateTimeOffset.FromUnixTimeSeconds(finalList[finalList.Count - 1].dealdate).LocalDateTime : DateTimeOffset.FromUnixTimeSeconds(0).UtcDateTime,
                LatestSoldFound = finalList.Count > 0 ? DateTimeOffset.FromUnixTimeSeconds(finalList[0].dealdate).LocalDateTime : DateTimeOffset.FromUnixTimeSeconds(0).UtcDateTime,
                SoldDealsFound = finalList.Count,
                CurrentCheapestPrice = deals.Count > 0 ? deals[0].Price : -1,
                CurrentAveragePrice = deals.Count > 0 ? deals.Average(x => x.Price) : -1,
            };
            return itemStat;
        }
    }
}
