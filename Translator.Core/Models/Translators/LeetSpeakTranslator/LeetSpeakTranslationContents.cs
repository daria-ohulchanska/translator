using System.Text.Json.Serialization;

namespace Translator.Core.Models.Translators.LeetSpeakTranslator
{
    public class LeetSpeakTranslationContents
    {
        [JsonPropertyName("translated")]
        public string Translated { get; set; }
    }
}
