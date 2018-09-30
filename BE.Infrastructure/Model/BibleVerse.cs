namespace BE.Infrastructure.Model
{
    public class BibleVerse
    {
        public int ID { get; set; }

        public int Book { get; set; }

        public int Chapter { get; set; }

        public int Verse { get; set; }

        public string Text { get; set; }
    }
}
