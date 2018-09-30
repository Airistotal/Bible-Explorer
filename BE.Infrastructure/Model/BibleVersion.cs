namespace BE.Infrastructure.Model
{
    public class BibleVersion
    {
        public int ID { get; set; }

        public string Table { get; set; }

        public string Abbreviation { get; set; }

        public string Language { get; set; }

        public string Version { get; set; }

        public string Info_text { get; set; }

        public string Info_url { get; set; }

        public string Publisher { get; set; }

        public string Copyright { get; set; }

        public string Copyright_info { get; set; }
    }
}
