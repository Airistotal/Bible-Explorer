namespace BE.Comparer.Models
{
    using System.Collections.Generic;

    public class BibleChapterInfo
    {
        public int MainBible { get; set; }

        public List<int> OtherBibles { get; set; }

        public int B { get; set; }

        public int C { get; set; }
    }
}