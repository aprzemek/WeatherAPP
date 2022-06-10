using System;
using System.Net;
using Nancy.Json;
using Newtonsoft.Json.Linq;
using WeatherApplication.Data;

namespace WeatherApplication.Models
{
    public class Weather
    {
        public int WeatherId { get; set; }
        public string City { get; set; }
        public string Temp { get; set; }
        public string Humidity { get; set; }
        public string Descr { get; set; }
        public string UpdateDate { get; set; }

        Random rnd = new Random();
        public Object getWeatherForWarsaw()
        {
            string url = "http://api.openweathermap.org/data/2.5/weather?q=Warsaw&appid=0d601a2736cfe0ea40ed0b761bd23392&units=metric";
            var client = new WebClient();
            var content = client.DownloadString(url);
            var serializer = new JavaScriptSerializer();
            var jsonContent = serializer.Deserialize<Object>(content);

            var d = jsonContent.ToString();
            dynamic data = JObject.Parse(d);
            Temp = data.main.temp ;
            City = data.name;
            Humidity = data.main.humidity;
            Descr = data.weather[0].description;
            WeatherId = rnd.Next();
            UpdateDate = DateTime.Now.ToString();

            var db = new AppDataBase();
            db.Weathers.Add(this);
            db.SaveChanges();
            return jsonContent;
        }
        public Object getWeatherForGdansk()
        {
            string url = "http://api.openweathermap.org/data/2.5/weather?q=Gdansk&appid=0d601a2736cfe0ea40ed0b761bd23392&units=metric";
            var client = new WebClient();
            var content = client.DownloadString(url);
            var serializer = new JavaScriptSerializer();
            var jsonContent = serializer.Deserialize<Object>(content);

            var d = jsonContent.ToString();
            dynamic data = JObject.Parse(d);
            Temp = data.main.temp;
            City = data.name;
            Humidity = data.main.humidity;
            Descr = data.weather[0].description;
            WeatherId = rnd.Next();
            UpdateDate = DateTime.Now.ToString();

            var db = new AppDataBase();
            db.Weathers.Add(this);
            db.SaveChanges();
            return jsonContent;
        }
    }
}
