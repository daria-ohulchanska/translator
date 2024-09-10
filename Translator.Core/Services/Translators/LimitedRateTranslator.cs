using Translator.Core.Interfaces;

namespace Translator.Core.Services.Translators
{
    public class LimitedRateTranslator : ITranslator
    {
        private readonly ITranslator _translator;

        private readonly int _count;
        private readonly TimeSpan _requestWindow;

        private int _currentCount;
        private DateTime _windowStart;

        public LimitedRateTranslator(ITranslator translator, int count, int perMinutes)
        {
            _translator = translator;

            _count = count;
            _requestWindow = TimeSpan.FromMinutes(perMinutes);
            _currentCount = 0;
            _windowStart = DateTime.UtcNow;
        }

        public Task<string> Translate(string text)
        {
            if (_currentCount >= _count)
            {
                var waitTime = _windowStart.Add(_requestWindow) - DateTime.UtcNow;
                if (waitTime > TimeSpan.Zero)
                {
                    throw new InvalidOperationException($"API request limit reached. Please wait {waitTime.TotalMinutes:F1} minutes before retrying.");
                }
                else
                {
                    _windowStart = DateTime.UtcNow;
                    _currentCount = 0;
                }
            }

            _currentCount++;

            return _translator.Translate(text);
        }
    }
}
