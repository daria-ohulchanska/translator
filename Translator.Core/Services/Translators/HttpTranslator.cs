using Translator.Core.Interfaces;

namespace Translator.Core.Services.Translators
{
    public abstract class HttpTranslator : ITranslator
    {
        private HttpClient _httpClient;

        public HttpTranslator(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> Translate(string text)
        {
            var request = BuildRequest(text);
            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();
            
            var translated = await GetTranslatedAsync(response);

            return translated;
        }

        protected abstract HttpRequestMessage BuildRequest(string text);

        protected abstract Task<string> GetTranslatedAsync(HttpResponseMessage response);
    }
}
