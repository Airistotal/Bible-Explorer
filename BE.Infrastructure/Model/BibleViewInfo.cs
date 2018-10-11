namespace BE.Infrastructure.Model
{
    using System.Collections.Generic;

    public class BibleViewInfo
    {
        public BibleID MainBible { get; set; }

        public List<BibleID> OtherBibles { get; set; }

        public int Book { get; set; }

        public int Chapter { get; set; }
    }
}