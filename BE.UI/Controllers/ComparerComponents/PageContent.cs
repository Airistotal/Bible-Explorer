namespace BE.UI.Controllers.ComparerComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BE.Comparer.Business;
    using BE.Comparer.Model;
    using BE.Infrastructure.Context;
    using BE.Infrastructure.Model;
    using BE.Infrastructure.Service;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class PageContent : ViewComponent
    {
        private readonly IBibleService bibleService;
        private readonly ITextDiff textDiff;

        public PageContent(IBibleService bibleService, ITextDiff textDiff)
        {
            this.bibleService = bibleService;
            this.textDiff = textDiff;
        }

        public async Task<IViewComponentResult> InvokeAsync(BibleViewInfo bibleViewInfo)
        {
            IEnumerable<ComparedBibleVerse> verses = this.GetComparedBibleBookAsync(
                bibleViewInfo.MainBible,
                bibleViewInfo.OtherBibles,
                bibleViewInfo.Book);

            return this.View(verses);
        }

        private IEnumerable<ComparedBibleVerse> GetComparedBibleBookAsync(
            BibleID bibleID,
            IEnumerable<BibleID> otherBibles,
            int book)
        {
            Comparison<BibleVerse> comparison = new Comparison<BibleVerse>((x, y) => x.Verse.CompareTo(y.Verse));
            List<BibleVerse> mainBook = this.bibleService.GetBook(bibleID, book).ToList();
            mainBook.Sort(comparison);

            Dictionary<BibleID, List<BibleVerse>> otherBooks = new Dictionary<BibleID, List<BibleVerse>>();
            foreach (var otherBible in otherBibles)
            {
                List<BibleVerse> otherBook = this.bibleService.GetBook(otherBible, book).ToList();
                otherBook.Sort(comparison);
                otherBooks.Add(otherBible, otherBook);
            }

            List<ComparedBibleVerse> list = new List<ComparedBibleVerse>();

            foreach (var verse in mainBook)
            {
                var index = mainBook.IndexOf(verse);
                var comparedBibleVerse = new ComparedBibleVerse(this.textDiff, verse);

                foreach (var otherBook in otherBooks)
                {
                    var otherBibleID = otherBook.Key;
                    var otherBibleBook = otherBook.Value;
                    var otherVerse = otherBibleBook[index];

                    comparedBibleVerse.AddComparison(otherBibleID, otherVerse);
                }

                list.Add(comparedBibleVerse);
            }

            return list;
        }
    }
}
