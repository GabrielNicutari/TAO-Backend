using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TAO_Backend.Models
{
    [Table("users")]
    [Index(nameof(HouseId), Name = "house_id_idx")]
    public partial class User
    {
        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Required]
        [Column("username")]
        [StringLength(45)]
        public string Username { get; set; }
        [Required]
        [Column("password")]
        [StringLength(255)]
        public string Password { get; set; }
        [Required]
        [Column("email")]
        [StringLength(100)]
        public string Email { get; set; }
        [Column("house_id", TypeName = "int(11)")]
        public int HouseId { get; set; }
        [Required]
        [Column("address")]
        [StringLength(45)]
        public string Address { get; set; }
        [Required]
        [Column("zip")]
        [StringLength(45)]
        public string Zip { get; set; }
        [Required]
        [Column("phone_number")]
        [StringLength(45)]
        public string PhoneNumber { get; set; }

        [ForeignKey(nameof(HouseId))]
        [InverseProperty(nameof(DailyReading.Users))]
        public virtual DailyReading House { get; set; }
    }
}
