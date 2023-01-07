using LibProntera.DivinePride;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LibProntera
{
    public class DivinePrideApi
    {
        private const string BaseUrl = "https://divine-pride.net/api/database/";
        public string ApiKey { get; set; }

        public DivinePrideApi(string ApiKey) => this.ApiKey = ApiKey;

        public async Task<DivinePrideItem> GetItem(int id)
        {
            using(var client = new HttpClient { BaseAddress = new Uri(BaseUrl)})
            {
                try
                {
                    var response = await client.GetAsync(string.Format("Item/{0}?apiKey={1}", id, ApiKey));
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var json = JsonConvert.DeserializeObject<DivinePrideItem>(responseBody);
                    return json;
                }
                catch (HttpRequestException e)
                {
                    return null;
                }
            }
        }
    }
}
