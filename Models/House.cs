using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TAO_Backend.Models
{
    [Table("houses")]
    public partial class House
    {
        public House()
        {
            DailyReadings = new HashSet<DailyReading>();
        }

        public House(int id, string address, string zip, int area, int yearBuilt)
        {
            Id = id;
            Address = address;
            Zip = zip;
            Area = area;
            YearBuilt = yearBuilt;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Required]
        [Column("address")]
        [StringLength(45)]
        public string Address { get; set; }
        [Required]
        [Column("zip")]
        [StringLength(45)]
        public string Zip { get; set; }
        [Column("area", TypeName = "int(11)")]
        public int Area { get; set; }
        [Column("year_built", TypeName = "int(11)")]
        public int YearBuilt { get; set; }

        [InverseProperty("House")]
        public virtual User User { get; set; }
        [InverseProperty(nameof(DailyReading.HouseReading))]
        public virtual ICollection<DailyReading> DailyReadings { get; set; }
    }
}
