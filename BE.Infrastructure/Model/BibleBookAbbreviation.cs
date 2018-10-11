namespace BE.Infrastructure.Model
{
    public class BibleBookAbbreviation
    {
        public int ID { get; set; }

        public string Abbreviation { get; set; }

        public int Book { get; set; }

        public bool IsPrimaryAbbreviation { get; set; }
    }
}
