using Microsoft.EntityFrameworkCore;
using Translator.Data.Contexts;
using Translator.Data.Entities;

namespace Translator.Data.Repositories
{
    public class TranslationRepository : ITranslationRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Translation> _set;

        public TranslationRepository(ApplicationDbContext context)
        {
            _context = context;
            _set = _context.Set<Translation>();
        }

        public async Task AddAsync(Translation item)
        {
            await _set.AddAsync(item);
            await _context.SaveChangesAsync();
        }
    }
}
