using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using WeatherApplication.Models;

namespace WeatherApplication.Controllers
{
    public class HomeController : Controller
    {
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        SqlConnection con = new SqlConnection();
        List<WeatherWarsaw> weathers = new List<WeatherWarsaw>();
        List<WeatherGdansk> weathersGdansk = new List<WeatherGdansk>();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            con.ConnectionString = (@"Server =(localdb)\MSSQLLocalDB;Database=aspnet-WeatherApp1-FC8D5B57-A762-4523-8E00-18F4C6693C5D;Trusted_Connection=True");
        }

        public IActionResult Index()
        {
            FetchDataWarsaw();
            FetchDataGdansk();
            var tupleModel = new Tuple<List<WeatherWarsaw>, List<WeatherGdansk>>(weathers,weathersGdansk);
            return View(tupleModel);
        }

        private void FetchDataWarsaw()
        {
            if (weathers.Count > 0)
            {
                weathers.Clear();
            }
          
            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT TOP (3) [City], [Temp], [Humidity],[Descr],[UpdateDate] FROM [Weathers] Where [City] LIKE 'Warsaw' ORDER BY [UpdateDate] DESC";

            dr = com.ExecuteReader();
            while (dr.Read())
            {
                weathers.Add(new WeatherWarsaw()
                {
                    Temp = dr["Temp"].ToString()
                ,
                    City = dr["City"].ToString()
                ,
                    Humidity = dr["Humidity"].ToString()
                ,
                    Descr = dr["Descr"].ToString()
                ,
                    UpdateDate = dr["UpdateDate"].ToString()
                });
            }

            con.Close();
        }

        private void FetchDataGdansk()
        {
            if (weathersGdansk.Count > 0)
            {
                weathersGdansk.Clear();
            }

            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT TOP (3) [City], [Temp], [Humidity],[Descr],[UpdateDate] FROM [Weathers] Where [City] LIKE 'Gdansk' ORDER BY [UpdateDate] DESC";

            dr = com.ExecuteReader();
            while (dr.Read())
            {
                weathersGdansk.Add(new WeatherGdansk()
                {
                    Temp = dr["Temp"].ToString()
                ,
                    City = dr["City"].ToString()
                ,
                    Humidity = dr["Humidity"].ToString()
                ,
                    Descr = dr["Descr"].ToString()
                ,
                    UpdateDate = dr["UpdateDate"].ToString()
                });
            }

            con.Close();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public void GetWeather()
        {
            Weather weather = new Weather();
            weather.getWeatherForWarsaw();
            weather.getWeatherForGdansk();
        }
    }
}