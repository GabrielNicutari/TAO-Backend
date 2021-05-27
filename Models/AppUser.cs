using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TAO_Backend.Models
{
    public class AppUser : IdentityUser
    {
        // The other fields are inherited from Identity User
        public string DisplayName { get; set; }
        public string Bio { get; set; }
    }
}
