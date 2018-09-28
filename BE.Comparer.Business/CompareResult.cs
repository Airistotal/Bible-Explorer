using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BE.Comparer.Models
{
    public class CompareResult
    {
        public List<TextChange> Differences { get; set; }

        public CompareResult()
        {
            Differences = new List<TextChange>();
        }

        public CompareResult(List<TextChange> Differences)
        {
            this.Differences = Differences;
        }
    }

    public class TextChange
    {
        public int index_from_orig;
        public int index_to_orig;
        public int index_from;
        public int index_to;

        public TextChange() {}

        public TextChange(int index_from_orig, int index_to_orig, int index_from, int index_to)
        {
            this.index_from_orig = index_from_orig;
            this.index_to_orig = index_to_orig;
            this.index_from = index_from;
            this.index_to = index_to;
        }
    }
}