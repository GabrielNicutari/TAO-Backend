using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TAO_Backend.Models
{
    [Table("daily_readings")]
    public partial class DailyReading
    {
        public DailyReading()
        {
            Users = new HashSet<User>();
        }

        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("timestamp", TypeName = "date")]
        public DateTime Timestamp { get; set; }
        [Column("energy", TypeName = "int(11)")]
        public int Energy { get; set; }
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

        [InverseProperty(nameof(User.House))]
        public virtual ICollection<User> Users { get; set; }
    }
}
