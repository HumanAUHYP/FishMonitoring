using System;
using FishMonitoringConsole;
using System.IO;
using System.Web;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

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

            var library = new Dictionary<string, string>();

            queryStr = HttpUtility.UrlDecode(queryStr);

            string[] strList = queryStr.Split('&');
            foreach (string el in strList)
            {
                library.Add(el.Split('=')[0], el.Split('=')[1]);
            }
            foreach(KeyValuePair<string, string> entry in library)
            {
                Console.WriteLine($"<p>{entry.Key}: {entry.Value}</p>");
            }
            int maxTemp = -4;
            int maxTempTime = 10;

            string connStr = "server=10.0.4.74;user=user_name;database=fishSchem;port=3306;password=password";

            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();

                string sql = $"SELECT * FROM fishSchem.fishTable Where fishName = '{library["fish"]}'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader res = cmd.ExecuteReader();

                while (res.Read())
                {
                    maxTemp = Convert.ToInt32(res[2]);
                    maxTempTime = Convert.ToInt32(res[4]);
                }
                res.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            conn.Close();



            Quality quality = new TempQuality(date, interval, tempData);
            Fish mentai = new FrozenFish(quality, (double)maxTemp, new TimeSpan(0, maxTempTime, 0));
            Console.WriteLine($"{mentai.GetReport()}");
        }
    }
}

