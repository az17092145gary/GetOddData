using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace getdata
{
    internal class Program
    {
        public async Task<string> getJwt()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate;
            HttpClient httpClient = new HttpClient(clientHandler);
            Dictionary<string, string> key = new Dictionary<string, string>()
            {
                { "username","eds_test_1"},
                {"password","1234qwer"}
            };
            string json = JsonSerializer.Serialize(key);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            string url = "https://www.isn88.com/membersite-api/api/member/authenticate";
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, url);
            message.Content = content;
            message.Headers.Add("locale", "en_US");
            var response = await httpClient.SendAsync(message);
            var data = await response.Content.ReadAsStringAsync();
            jwtToken reult = JsonSerializer.Deserialize<jwtToken>(data);
            return reult.token;
        }
        public async Task<IEnumerable<data>> getdata(string token)
        {
            string url = "https://www.isn88.com/membersite-api/api/data/sports";
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            httpClientHandler.AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate;
            HttpClient httpClient = new HttpClient(httpClientHandler);
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            requestMessage.Headers.Add("Authorization", $"{token}");
            //requestMessage.Headers.Add("Authorization ", $"Bearer {token}");
            requestMessage.Headers.Add("locale", "en_US");
            var response = await httpClient.SendAsync(requestMessage);
            var data = await response.Content.ReadAsStringAsync();
            IEnumerable<data> datas = JsonSerializer.Deserialize<IEnumerable<data>>(data);
            return datas;


        }
        public async Task<IEnumerable<Weather>> gettestAPI()
        {
            string url = "https://localhost:7041/WeatherForecast";
            using (HttpClient client = new HttpClient())
            {
                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
                var response = await client.SendAsync(requestMessage);
                if (response.IsSuccessStatusCode != false)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var weathers = JsonSerializer.Deserialize<IEnumerable<Weather>>(data);
                    return weathers;
                }
                return null;
            }
        }
        public async Task<string> gettestuserAPI()
        {
            var postdata = new Dictionary<string, string>()
            {
                { "memberName","vincent"},
                { "memberAge","99"}
            };
            var json = JsonSerializer.Serialize(postdata);
            var content = new StringContent(json,Encoding.UTF8, "application/json");
            string url = $"https://localhost:7041/User/authenticate";
            using (HttpClient client = new HttpClient())
            {
                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
                requestMessage.Content = content;
                var response = await client.SendAsync(requestMessage);
                if (response.IsSuccessStatusCode != false)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    return data;
                }
                return null;
            }
        }
        static async Task Main(string[] args)
        {
            Program mm = new Program();
            //string a = await mm.getJwt();
            //var b = await mm.getdata(a);
            //foreach (data item in b) 
            //{
            //    Console.WriteLine(item.id);
            //    Console.WriteLine(item.name);
            //    Console.WriteLine(item.numberOfEvents);
            //}
            //var c = await mm.gettestAPI();
            //foreach (var item in c) 
            //{
            //    Console.WriteLine(item.date);
            //    Console.WriteLine(item.temperatureC);
            //    Console.WriteLine(item.temperatureF);
            //    Console.WriteLine(item.summary);
            //}
            var d = await mm.gettestuserAPI();
            Console.WriteLine(d);
            Console.ReadKey();
        }
    }
}
