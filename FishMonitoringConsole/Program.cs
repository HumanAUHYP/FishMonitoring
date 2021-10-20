using System;
using System.Collections.Generic;
using System.IO;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace FishMonitoringConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            string connStr = "server=10.0.4.74;user=user_name;database=fishSchem;port=3306;password=password";

            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();

                string sql = "SELECT * FROM fishSchem.fishTable";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader res = cmd.ExecuteReader();

                while (res.Read())
                {
                    Console.WriteLine($"{res[1]} {res[2]} {res[3]}");
                }
                res.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            conn.Close();
            Console.WriteLine("Done.");
            // temperature data
            int interval = 10; //min
            string tempData = "-6+-5";
            //fish data
            int maxTemp = -4;
            int maxTempTime = 10; // min


            Quality quality = new TempQuality(interval , tempData);
            Fish mentai = new FrozenFish(quality, (double)maxTemp, new TimeSpan(0, maxTempTime, 0));
            Console.WriteLine(mentai.isValid());
        }
    }



 
}