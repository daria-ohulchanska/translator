using System.Text.Json.Serialization;

namespace Translator.Core.Models.Translators.LeetSpeakTranslator
{
    public class LeetSpeakTranslationResult
    {
        [JsonPropertyName("contents")]
        public LeetSpeakTranslationContents Contents { get; set; }
    }
}
