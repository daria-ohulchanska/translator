namespace Translator.Core.Interfaces
{
    public interface ITranslator
    {
        Task<string> Translate(string text);
    }
}
