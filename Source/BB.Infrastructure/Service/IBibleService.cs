namespace BB.Infrastructure.Service
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BB.Infrastructure.Model;

    public interface IBibleService
    {
        Task<List<BibleVerse>> GetBookChapterVersesAsync(BibleID bibleID, int book, int chapter);

        Task<int> GetLastChapterNumberOfBookAsync(BibleID bibleID, int book);

        Task<List<BibleVersion>> GetBibleVersionsAsync();

        Task<List<BibleBookAbbreviation>> GetBibleBooksAsync();
    }
}
