using System.Text.Json;
using Translator.Core.Models.Translators.LeetSpeakTranslator;
using Constants = Translator.Shared.Constants.Constants;

namespace Translator.Core.Services.Translators
{
    public class LeetSpeakTranslator : HttpTranslator
    {
        public LeetSpeakTranslator(HttpClient httpClient) 
            : base(httpClient)
        {
        }

        protected override HttpRequestMessage BuildRequest(string text)
        {
            var content = new FormUrlEncodedContent(
                [
                    new KeyValuePair<string, string>("text", text)
                ]);

            return new HttpRequestMessage(HttpMethod.Post, Constants.AppSettings.LeetSpeak.Url)
            {
                Content = content,
            };
        }

        protected override async Task<string> GetTranslatedAsync(HttpResponseMessage message)
        {
            var responseString = await message.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<LeetSpeakTranslationResult>(responseString);
            return result?.Contents.Translated ?? string.Empty;
        }
    }
}
