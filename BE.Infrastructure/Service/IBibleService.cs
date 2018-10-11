namespace BE.Infrastructure.Service
{
    using System.Collections.Generic;
    using BE.Infrastructure.Model;

    public interface IBibleService
    {
        IList<BibleVerse> GetChapter(BibleID bibleID, int chapter);
    }
}
