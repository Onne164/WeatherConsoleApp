using System.Net.Http;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Linq;
using System.Text.Json;
using System.Net.Http.Json;
using System.Text;

namespace WeatherConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string url = "https://api.met.no/weatherapi/locationforecast/2.0/compact?lat=59.43696&lon=24.75353";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "WeatherApp");

            var httpResponse = await client.GetAsync(url);
            string jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            JObject jObject = JObject.Parse(jsonResponse);

            for (int i = 0; i < 10; i++)
            {               
                string time = (string)jObject["properties"]["timeseries"][i]["time"];
                string temperature = (string)jObject["properties"]["timeseries"][i]["data"]["instant"]["details"]["air_temperature"];
                Console.WriteLine(string.Concat(time, "   ", temperature, " °C"));
            }            
        }    
    }
}
  