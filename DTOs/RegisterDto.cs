using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TAO_Backend.DTOs
{
    public class RegisterDto
    {
        public int DisplayName { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
    }
}
