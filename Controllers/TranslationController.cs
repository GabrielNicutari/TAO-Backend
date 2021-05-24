using Microsoft.AspNetCore.Mvc;
using TAO_Backend.Models;
using TAO_Backend.Services;

namespace TAO_Backend.Controllers
{
    public class TranslationController: BaseApiController
    {
        [HttpPost]
        public string[] PostContactForm([FromBody] PageWords pageWords)
        {
            TranslationService translationService = new TranslationService();
            return translationService.Translate(pageWords.Words, pageWords.ToLanguage);
        }
    }
}