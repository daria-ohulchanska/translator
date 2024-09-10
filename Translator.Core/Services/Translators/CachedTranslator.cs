using Microsoft.Extensions.Caching.Memory;
using Translator.Core.Interfaces;

namespace Translator.Core.Services.Translators
{
    public class CachedTranslator : ITranslator
    {
        private readonly ITranslator _translator;
        private readonly IMemoryCache _cache;

        public CachedTranslator(ITranslator translator, IMemoryCache cache)
        {
            _translator = translator;
            _cache = cache;
        }

        public async Task<string> Translate(string text)
        {
            if (_cache.TryGetValue(text, out string? cached))
            {
                return cached ?? string.Empty;
            }

            var translated = await _translator.Translate(text);

            _cache.Set(text, translated);

            return translated;
        }
    }
}
