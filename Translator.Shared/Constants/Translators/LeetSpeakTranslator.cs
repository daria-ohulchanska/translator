namespace Translator.Shared.Constants.Translators
{
    public static partial class Constants
    {
        public static partial class AppSettings
        {
            public static class LeetSpeakTranslator
            {
                public const string Url = "https://api.funtranslations.com/translate/leetspeak.json";
                public const int RateLimitCount = 5;
                public const int RateLimitPeriod = 60; //minutes
            }
        }
    }
}
