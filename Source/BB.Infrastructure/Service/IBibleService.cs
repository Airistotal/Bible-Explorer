namespace BB.Infrastructure.Service
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BB.Infrastructure.Model;

    public interface IBibleService
    {
        List<BibleVerse> GetBookChapterVerses(BibleID bibleID, int book, int chapter);

        int GetLastChapterNumberOfBook(BibleID bibleID, int book);

        List<BibleVersion> GetBibleVersions();

        List<BibleBookAbbreviation> GetBibleBooks();
    }
}
