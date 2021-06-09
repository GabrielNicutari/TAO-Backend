using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TAO_Backend.Models;

namespace TAO_Backend.DTOs
{
    public class UserDto
    {
        public string DisplayName { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
        public House House { get; set; }
    }
}
