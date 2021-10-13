using System;
using FishMonitoringConsole;
using System.IO;

namespace FishMonitoringWeb
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"/home/name/form.html";
            //string path = @"C:\Users\gutuf\Desktop\FishMonitoring-develop\FishMonitoringWeb\form.html";

            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    Console.WriteLine("Content-Type: text/html \n\n");
                    Console.WriteLine(sr.ReadToEnd());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
