using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace TAO_Backend.Models
{
    public class AppUser : IdentityUser
    {
        // The other fields are inherited from Identity User
        public string DisplayName { get; set; }
        public string Bio { get; set; }

        [Column("house_id", TypeName = "int(11)")]
        public int HouseId { get; set; }
    
        [ForeignKey(nameof(HouseId))]
        [InverseProperty("User")]
        public virtual House House { get; set; }
    }
}
