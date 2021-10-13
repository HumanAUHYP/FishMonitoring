using System;
using System.Collections.Generic;
using System.Text;

namespace FishMonitoringConsole
{
    public abstract class Fish
    {
        public string name;
        public Quality quality;

        public Fish(Quality q)
        {
            quality = q;
        }
        public abstract bool isValid();

        public abstract string GetReport();

    }

    public class FrozenFish : Fish
    {
        public double maxStoreTemp;
        public TimeSpan deathTime;

        public FrozenFish(Quality q, double t, TimeSpan d) : base(q)
        {
            maxStoreTemp = t;
            deathTime = d;
        }

        public override bool isValid()
        {
            return !((quality as TempQuality).GetTempUpperTime(maxStoreTemp) > deathTime);
        }

        public override string GetReport()
        {
            string report = "<p>Fish is bad</p>";
            if (isValid())
            {
                report = "<p>All OK</p>";
            }
            else
            {
                foreach (DateTime key in (quality as TempQuality).temperature.Keys)
                {
                    if ((quality as TempQuality).temperature[key] > maxStoreTemp)
                    {
                        report += $"<p>{key}\t{(quality as TempQuality).temperature[key]}</p>";
                    }
                }
            }

            return report;
        }
    }
    public class ChilledFish : Fish
    {
        public double minStoreTemp;
        public double maxStoreTemp;
        public TimeSpan minDeathTime;
        public TimeSpan maxDeathTime;

        public ChilledFish(Quality q, double t, TimeSpan d) : base(q)
        { }

        public override bool isValid()
        {
            return !((quality as TempQuality).GetTempUpperTime(maxStoreTemp) > maxDeathTime
            || (quality as TempQuality).GetTempLowerTime(maxStoreTemp) < minDeathTime);
        }
        public override string GetReport()
        {
            throw new NotImplementedException();
        }
    }
}
