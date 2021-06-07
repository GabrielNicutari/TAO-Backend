using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TAO_Backend.Models
{
    public class DailyReadingStats
    {
        public double AvgEnergy { get; set; }
        public double MinEnergy { get; set; }
        public double MaxEnergy { get; set; }
    }
}
