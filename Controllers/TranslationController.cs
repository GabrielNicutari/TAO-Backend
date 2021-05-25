using Microsoft.AspNetCore.Mvc;
using TAO_Backend.Models;
using TAO_Backend.Services;

namespace TAO_Backend.Controllers
{
    public class TranslationController: BaseApiController
    {
        private readonly ITranslationService _translationService;
        public TranslationController(ITranslationService translationService)
        {
            _translationService = translationService; 
        }
        
        [HttpPost]
        public string[] PostContactForm([FromBody] PageWords pageWords)
        {
            return _translationService.Translate(pageWords.Words, pageWords.ToLanguage);
        }
    }
}