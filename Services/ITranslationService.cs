namespace TAO_Backend.Services
{
    public interface ITranslationService
    {
        string[] Translate(string[] words, string toLanguage);
    }
}