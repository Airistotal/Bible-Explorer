namespace BE.Infrastructure.Model
{
    using System.Collections.Generic;

    public class BibleChapterInfo
    {
        public int MainBible { get; set; }

        public List<BibleID> OtherBibles { get; set; }

        public int Book { get; set; }

        public int Chapter { get; set; }
    }
}