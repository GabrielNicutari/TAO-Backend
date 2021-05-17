using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TAO_Backend.Models;

namespace TAO_Backend.Controllers
{
    public class ContactFormController: BaseApiController
    {
        private readonly DBContext _context;
        
        public ContactFormController(DBContext context)
        {
            _context = context; 
        }
        
        [HttpPost]
        public void PostContactForm([FromBody] ContactForm contactForm)
        {
            // send email to us using the body of the request
            Console.WriteLine("Sending email...");
            Console.WriteLine(contactForm.message);
        }
    }
}