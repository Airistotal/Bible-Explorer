namespace BE.Comparer.Models
{
    using BE.Comparer.Business;
    using BE.Comparer.Models;
    using System;
    using System.Collections.Generic;

    public class ComparedBibleVerse : BibleVerse
    {
        // Dependencies
        private readonly ITextDiff txtDiff;

        /// <summary>
        /// Key: bible ID -- BibleVerse: the verse from the specified bible
        /// </summary>
        private Dictionary<int, Tuple<BibleVerse, CompareResult>> comparedVerses;

        public ComparedBibleVerse(ITextDiff txtDiff)
        {
            comparedVerses = new Dictionary<int, Tuple<BibleVerse, CompareResult>>();
        }

        public ComparedBibleVerse(ITextDiff txtDiff, BibleVerse verse) : this(txtDiff)
        {
            id = verse.id;
            bible_id = verse.bible_id;
            b = verse.b;
            c = verse.c;
            v = verse.v;
            t = verse.t;
        }

        public void AddComparison(BibleVerse verse)
        {
            CompareResult diff = txtDiff.GetTextDifferences(t, verse.t);
            comparedVerses.Add(verse.bible_id, new Tuple<BibleVerse, CompareResult>(verse, diff));
        }

        public Tuple<BibleVerse, CompareResult> GetComparison(int bible_id)
        {
            return comparedVerses[bible_id];
        }
    }
}