using System;
using System.Collections.Generic;
using System.IO;

namespace FishMonitoringConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            
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