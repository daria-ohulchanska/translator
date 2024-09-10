using Translator.Data.Entities;

namespace Translator.Data.Repositories
{
    public interface ITranslationRepository
    {
        Task AddAsync(Translation item);
    }
}
