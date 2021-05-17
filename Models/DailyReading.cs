using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TAO_Backend.Models
{
    [Table("daily_readings")]
    [Index(nameof(HouseReadingId), Name = "house_id_idx")]
    public partial class DailyReading
    {
        public DailyReading(
            DateTime timestamp, 
            double energy, 
            double volume, 
            int hourCounter, 
            double tempForward, 
            double tempReturn, 
            double power, 
            int flow, 
            int houseReadingId
            )
        {
            Timestamp = timestamp;
            Energy = energy;
            Volume = volume;
            HourCounter = hourCounter;
            TempForward = tempForward;
            TempReturn = tempReturn;
            Power = power;
            Flow = flow;
            HouseReadingId = houseReadingId;
        }
        
        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("timestamp", TypeName = "date")]
        public DateTime Timestamp { get; set; }
        [Column("energy", TypeName = "int(11)")]
        public double Energy { get; set; }
        [Column("volume")]
        public double Volume { get; set; }
        [Column("hour_counter", TypeName = "int(11)")]
        public int HourCounter { get; set; }
        [Column("temp_forward")]
        public double TempForward { get; set; }
        [Column("temp_return")]
        public double TempReturn { get; set; }
        [Column("power")]
        public double Power { get; set; }
        [Column("flow", TypeName = "int(11)")]
        public int Flow { get; set; }
        [Column("house_reading_id", TypeName = "int(11)")]
        public int HouseReadingId { get; set; }

        [ForeignKey(nameof(HouseReadingId))]
        [InverseProperty(nameof(House.DailyReadings))]
        public virtual House HouseReading { get; set; }
    }
}
