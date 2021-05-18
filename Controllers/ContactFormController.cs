using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TAO_Backend.Models;
using TAO_Backend.Services;

namespace TAO_Backend.Controllers
{
    public class ContactFormController: BaseApiController
    {
        private readonly IEmailService _emailService;
        public ContactFormController(IEmailService emailService)
        {
            _emailService = emailService;
        }
        [HttpPost]
        public bool PostContactForm([FromBody] ContactForm contactForm)
        {
            EmailData emailData = new EmailData
            {
                EmailBody =  $"Sender's name: {contactForm.name}\n\nMessage: {contactForm.message}" +
                             $"\n\nSender's email: {contactForm.email}",
                EmailSubject = "Someone contacted you via the website", 
                EmailToId = "vrnova.update.user.profile@gmail.com", 
                EmailToName = "Energy Department"
            };
            return _emailService.SendEmail(emailData);
        }
    }
}