namespace BE.Comparer.Model
{
    using System;
    using System.Collections.Generic;
    using BE.Comparer.Business;
    using BE.Infrastructure.Model;

    public class ComparedBibleVerse : BibleVerse
    {
        public ComparedBibleVerse(ITextDiff txtDiff)
        {
            this.ComparedVerses = new Dictionary<BibleID, Tuple<BibleVerse, CompareResult>>();
            this.TxtDiff = txtDiff;
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

        public ITextDiff TxtDiff { get; }

        public Dictionary<BibleID, Tuple<BibleVerse, CompareResult>> ComparedVerses { get; }

        public void AddComparison(BibleID bibleID, BibleVerse verse)
        {
            CompareResult diff = this.TxtDiff.GetTextDifferences(this.Text, verse.Text);
            this.ComparedVerses.Add(bibleID, new Tuple<BibleVerse, CompareResult>(verse, diff));
        }

        public string GetDifference(BibleID bibleID, int index)
        {
            var compareResult = this.ComparedVerses[bibleID].Item2.GetDifference(index);

            if (compareResult != null)
            {
                return this.Text.Substring(compareResult.Item1, compareResult.Item2 - compareResult.Item1);
            }
            else
            {
                return string.Empty;
            }
        }
    }
}