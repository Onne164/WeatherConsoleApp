using System.Net.Http;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace WeatherConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //var details = new List<Details>
            //{
            //   new Details
            //   {
                   
            //   }
            //};

            string url = "https://api.met.no/weatherapi/locationforecast/2.0/compact?lat=59.43696&lon=24.75353";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", "WeatherApp");

            try
            {
                var httpResponse = await client.GetAsync(url);
                string jsonResponse = await httpResponse.Content.ReadAsStringAsync();
                Console.WriteLine(jsonResponse);
                var details = JsonConvert.DeserializeObject<Details[]>(jsonResponse);

                foreach (var detail in details)
                {
                    Console.WriteLine($"{detail.air_temperature}");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                client.Dispose();
            }
            //var msg = new HttpRequestMessage(HttpMethod.Get, url);
            //msg.Headers.Add("User-Agent", "C# Program");
            //var res = await client.SendAsync(msg);

            //var response = await res.Content.ReadAsStringAsync();

            //var result1 = JObject.Parse(response);
            //Console.WriteLine(string.Concat("Temperature: ", result1["air_temperature"]));
            //var result2  = JsonConvert.DeserializeObject<Timesery>(response);
            //Console.WriteLine(string.Concat("Time: ", result2.time));
            //Console.ReadLine();
            //Console.WriteLine(String.Empty);
            ////Details output1 = result1;
            //Timesery output2 = result2;

            ////var temperature = output1.air_temperature;
            //var time = output2.time;

            ////Console.WriteLine(response);

            //string [] temperatures;
            //string[] times;

        
        }

    }
    public class Geometry
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; }
    }

    public class Units
    {
        public string air_pressure_at_sea_level { get; set; }
        public string air_temperature { get; set; }
        public string cloud_area_fraction { get; set; }
        public string precipitation_amount { get; set; }
        public string relative_humidity { get; set; }
        public string wind_from_direction { get; set; }
        public string wind_speed { get; set; }
    }

    public class Meta
    {
        public DateTime updated_at { get; set; }
        public Units units { get; set; }
    }

    public class Details
    {
        public string air_pressure_at_sea_level { get; set; }
        public string air_temperature { get; set; }
        public string cloud_area_fraction { get; set; }
        public string relative_humidity { get; set; }
        public string wind_from_direction { get; set; }
        public string wind_speed { get; set; }
        public string precipitation_amount { get; set; }
    }

    public class Instant
    {
        public Details details { get; set; }
    }

    public class Summary
    {
        public string symbol_code { get; set; }
    }

    public class Next12Hours
    {
        public Summary summary { get; set; }
    }

    public class Next1Hours
    {
        public Summary summary { get; set; }
        public Details details { get; set; }
    }

    public class Next6Hours
    {
        public Summary summary { get; set; }
        public Details details { get; set; }
    }

    public class Data
    {
        public Instant instant { get; set; }
        public Next12Hours next_12_hours { get; set; }
        public Next1Hours next_1_hours { get; set; }
        public Next6Hours next_6_hours { get; set; }
    }

    public class Timesery
    {
        public string time { get; set; }
        public Data data { get; set; }
    }

    public class Properties
    {
        public Meta meta { get; set; }
        public List<Timesery> timeseries { get; set; }
    }

    public class Root
    {
        public string type { get; set; }
        public Geometry geometry { get; set; }
        public Properties properties { get; set; }
    }
}