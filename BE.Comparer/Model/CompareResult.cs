namespace BE.Comparer.Model
{
    using System.Collections.Generic;

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
    }
}