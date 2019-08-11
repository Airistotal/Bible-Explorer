namespace BB.Comparer.Model
{
    using System.Collections.Generic;
    using BB.Infrastructure.Model;

    public class ComparedBibleVerse : BibleVerse
    {
        public ComparedBibleVerse(BibleVerse verse, List<ComparedWord> comparedWords)
        {
            this.ID = verse.ID;
            this.Book = verse.Book;
            this.Chapter = verse.Chapter;
            this.Verse = verse.Verse;
            this.Text = verse.Text;
            this.ComparedWords = comparedWords;
        }

        public List<ComparedWord> ComparedWords { get; }
    }
}
