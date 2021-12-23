using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Net;
using Newtonsoft.Json;

namespace Weather_Forcast_Service_v3
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {

        const string APPID = "7ab4461169d48e6c88ea44f3ec6c162c";

        public List<Weather> forcast(string zipcode)
        {
            List<Weather> weatherForcast = new List<Weather>(); //list storing stores
            string lat, lon;

            using (WebClient web = new WebClient())
            {

                //Obtaing Forcast
                string url = string.Format("http://api.openweathermap.org/data/2.5/forecast/daily?zip={0},us&appid={1}&units=metric", zipcode, APPID);
                var json = web.DownloadString(url);
                var Object = JsonConvert.DeserializeObject<weatherForcast>(json);
                weatherForcast forcast = Object;

                //5-day Forcast
                for (int i = 0; i < 5; i++)
                {
                    Weather daily_forcast = new Weather();

                    daily_forcast.day = i + 1;
                    daily_forcast.tmp = string.Format("{0} \u00B0 C", forcast.list[i].temp.day);
                    daily_forcast.pressure = string.Format("{0} hPa", forcast.list[i].pressure);
                    daily_forcast.hum = string.Format("{0} %", forcast.list[i].humidity);


                    weatherForcast.Add(daily_forcast);

                }



            }



            return weatherForcast;
        }
    }

    public class Weather
    {
        public int day;
        public string tmp, pressure, hum;

    }
}
