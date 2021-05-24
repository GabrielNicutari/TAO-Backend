using System;
using System.Net;
using System.Web;

namespace TAO_Backend.Services
{
    public class TranslationService
    {
        public string[] Translate(string[] words, string toLanguage)
        {
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = TranslateSingleString(words[i], toLanguage);
            }
            return words;
        }
        private string TranslateSingleString(string word, string toLanguage)
        {
            var fromLanguage = "en"; // English
            var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={fromLanguage}&tl={toLanguage}&dt=t&q={HttpUtility.UrlEncode(word)}";
            var webClient = new WebClient
            {
                Encoding = System.Text.Encoding.UTF8
            };
            var result = webClient.DownloadString(url);
            try
            {
                result = result.Substring(4, result.IndexOf("\"", 4, StringComparison.Ordinal) - 4);
                return result;
            }
            catch
            {
                return "Error";
            }
        }
    }
}