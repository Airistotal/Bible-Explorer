namespace BB.Comparer.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CompareResult
    {
        public CompareResult()
        {
            this.Differences = new List<TextChange>();
        }

        public CompareResult(List<TextChange> differences)
        {
            this.Differences = differences;
        }

        public List<TextChange> Differences { get; set; }

        public Tuple<int, int> GetDifference(int index)
        {
            return this.Differences.
                Where(x => index >= x.IndexFromOrig && index < x.IndexToOrig).
                Select(x => new Tuple<int, int>(x.IndexFrom, x.IndexTo)).
                DefaultIfEmpty(null).FirstOrDefault();
        }
    }
}