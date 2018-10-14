namespace BE.Infrastructure.Model
{
    using System.Collections.Generic;

    public class BibleBookGenre
    {
        public int Id { get; set; }

        public string GenreName { get; set; }

        public List<BibleBook> BibleBooks { get; set; }
    }
}
