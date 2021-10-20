using System;
using FishMonitoringConsole;
using System.IO;
using System.Web;
using MySql.Data.MySqlClient;

namespace HtmlFormHandle
{
    class FormHandle
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Content-Type: text/html \n\n");
            string path = @"/home/name/style.html";

            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    Console.WriteLine(sr.ReadToEnd());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            var queryStr = Environment.GetEnvironmentVariable("QUERY_STRING");
            //Console.WriteLine(queryStr);
            int interval = 0;
            string tempData = "";

            DateTime date = DateTime.Now;

            queryStr = HttpUtility.UrlDecode(queryStr);

            string[] strList = queryStr.Split('&');
            foreach (string el in strList)
            {
                if (el.Split('=')[0] == "fish") Console.WriteLine($"<p>Fish: {el.Split('=')[1]}</p>");
                else if (el.Split('=')[0] == "date")
                {
                    Console.WriteLine($"<p>Date: {el.Split('=')[1]}</p>");
                    date = DateTime.Parse(el.Split('=')[1]);
                }
                else if (el.Split('=')[0] == "interval")
                {
                    interval = int.Parse(el.Split('=')[1]);
                    Console.WriteLine($"<p>Interval: {interval}</p>");
                }
                else if (el.Split('=')[0] == "temp")
                {
                    tempData = el.Split('=')[1];
                    Console.WriteLine($"<p>Temp: {el.Split('=')[1]}</p>");
                }
            }
            int maxTemp = -4;
            int maxTempTime = 10; // min

           

            Quality quality = new TempQuality(date, interval, tempData);
            Fish mentai = new FrozenFish(quality, (double)maxTemp, new TimeSpan(0, maxTempTime, 0));
            Console.WriteLine($"{mentai.GetReport()}");
        }
    }
}

