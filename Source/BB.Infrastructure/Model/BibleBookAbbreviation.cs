namespace BB.Infrastructure.Model
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class BibleBookAbbreviation
    {
        public int Id { get; set; }

        public string Abbreviation { get; set; }

        [ForeignKey("BibleBook")]
        public int Book { get; set; }

        public bool IsPrimaryAbbreviation { get; set; }

        public BibleBook BibleBook { get; set; }
    }
}
