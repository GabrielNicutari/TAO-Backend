using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace TAO_Backend.Models
{
    [DataContract]
    public class DailyReadingCsv
    { 
        public String Timestamp { get; set; }

        public String Energy { get; set; }

        public String Volume { get; set; }

        public String HourCounter { get; set; }

        public String TempForward { get; set; }

        public String TempReturn { get; set; }

        public String Power { get; set; }

        public String Flow { get; set; }

        
    }
}
