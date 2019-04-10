namespace BE.Infrastructure.Model
{
    public class BibleChapterInfo
    {
        public int Book { get; set; }

        public string BookGenre { get; set; }

        public string BookName { get; set; }

        public int Chapter { get; set; }

        public TestamentID TestamentID { get; set; }
    }
}
