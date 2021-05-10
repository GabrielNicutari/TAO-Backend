using CsvHelper.Configuration;
using TAO_Backend.Models;

namespace TAO_Backend.CsvData
{
    public sealed class DailyReadingMapper : ClassMap<DailyReadingCsv>
    {
        public DailyReadingMapper()
        {
            Map(m => m.Timestamp).Name("Timestamp");
            Map(m => m.Energy).Name("Energy");
            Map(m => m.Volume).Name("Volume");
            Map(m => m.HourCounter).Name("HourCounter");
            Map(m => m.TempForward).Name("TempForward");
            Map(m => m.TempReturn).Name("TempReturn");
            Map(m => m.Power).Name("Power");
            Map(m => m.Flow).Name("Flow");
        }
    }
}
