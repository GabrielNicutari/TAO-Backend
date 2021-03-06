using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TAO_Backend.Models
{
    [Table("users")]
    [Index(nameof(HouseId), Name = "house_id_UNIQUE", IsUnique = true)]
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
        [Column("phone_number")]
        [StringLength(45)]
        public string PhoneNumber { get; set; }

        // [ForeignKey(nameof(HouseId))]
        // [InverseProperty("User")]
        // public virtual House House { get; set; }
    }
}
