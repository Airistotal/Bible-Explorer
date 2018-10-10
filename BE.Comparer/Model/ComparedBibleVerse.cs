namespace BE.Comparer.Model
{
    using System;
    using System.Collections.Generic;
    using BE.Comparer.Business;
    using BE.Infrastructure.Model;

    public class ComparedBibleVerse : BibleVerse
    {
        private readonly ITextDiff txtDiff;

        private Dictionary<BibleID, Tuple<BibleVerse, CompareResult>> comparedVerses;

        public ComparedBibleVerse(ITextDiff txtDiff)
        {
            this.comparedVerses = new Dictionary<BibleID, Tuple<BibleVerse, CompareResult>>();
            this.txtDiff = txtDiff;
        }

        public ComparedBibleVerse(ITextDiff txtDiff, BibleVerse verse)
            : this(txtDiff)
        {
            this.ID = verse.ID;
            this.Book = verse.Book;
            this.Chapter = verse.Chapter;
            this.Verse = verse.Verse;
            this.Text = verse.Text;
        }

        public void AddComparison(BibleID bibleID, BibleVerse verse)
        {
            CompareResult diff = this.txtDiff.GetTextDifferences(this.Text, verse.Text);
            this.comparedVerses.Add(bibleID, new Tuple<BibleVerse, CompareResult>(verse, diff));
        }

        public Tuple<BibleVerse, CompareResult> GetComparison(BibleID bible_id)
        {
            return this.comparedVerses[bible_id];
        }
    }
}