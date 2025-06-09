using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SQLSanitizorNator.Data;
using SQLSanitizorNator.Data.Models;
using SQLSanitizorNator.Logic.Services;

namespace SQLSanitizorNator.Logic.CRUD
{
    public class NaughtyWordServiceCRUD : BaseCrud<NaughtyWord, Guid>, INaughtyWordService<NaughtyWordServiceCRUD>
    {
        public NaughtyWordServiceCRUD(IDbContextFactory<ApplicationDbContext> dbContextFactory) : base(dbContextFactory)
        {
        }

        public async Task<List<NaughtyWord>> GetUnderSeverity(int severity = 10, CancellationToken token = default)
        {
            using var context = _dbContextFactory.CreateDbContext();
            return await context.NaughtyWords.Where(nw => nw.Severity < severity)
                .OrderBy(nw => nw.UsageCount)
                .ToListAsync(token);
        }

        public async Task<string> Sanitize(string toSanitize, int severity = 10, CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(toSanitize))
                return toSanitize;

            var naughtyWords = await GetUnderSeverity(severity, token);
            List<NaughtyWord> sanitizedWords = new();
            foreach (var naughtyWord in naughtyWords)
            {
                if (!toSanitize.Contains(naughtyWord.Value, StringComparison.OrdinalIgnoreCase))
                    continue;

                toSanitize = toSanitize.Replace(naughtyWord.Value, new string('*', naughtyWord.Value.Length), StringComparison.OrdinalIgnoreCase);
                naughtyWord.UsageCount++;
            }

            using var context = _dbContextFactory.CreateDbContext();
            context.NaughtyWords.UpdateRange(naughtyWords);
            await context.SaveChangesAsync(token);
            return toSanitize;
        }
    }
}
