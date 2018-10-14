namespace BE.Infrastructure.Service
{
    using System.Collections.Generic;
    using BE.Infrastructure.Model;

    public interface IBibleService
    {
        IList<BibleVerse> GetBook(BibleID bibleID, int book);
    }
}
