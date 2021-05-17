using CsvHelper.Configuration;
using TAO_Backend.Models;

namespace TAO_Backend.CsvData
{
    public sealed class HouseMapper : ClassMap<HouseCsv>
    {
        public HouseMapper()
        {
            Map(m => m.Address).Name("Address");
            Map(m => m.HouseId).Name("HouseId");
            Map(m => m.YearBuilt).Name("YearBuilt");
            Map(m => m.Zip).Name("Zip");
            Map(m => m.Area).Name("Area"); 
        }
    }
}
