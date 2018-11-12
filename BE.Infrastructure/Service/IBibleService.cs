namespace BE.Infrastructure.Service
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BE.Infrastructure.Model;

    public interface IBibleService
    {
        Task<List<BibleVerse>> GetBookChapterVersesAsync(BibleViewInfo bibleViewInfo);

        Task<int> GetLastChapterNumberOfBookAsync(BibleID bibleID, int book);

        Task<List<BibleVersion>> GetBibleVersionsAsync();

        Task<List<BibleBookAbbreviation>> GetBibleBooksAsync();
    }
}
