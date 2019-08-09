namespace BB.Infrastructure.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class BibleBook
    {
        public string T { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("Genre")]
        public int GenreID { get; set; }

        public BibleBookGenre Genre { get; set; }

        public List<BibleBookAbbreviation> Abbreviations { get; set; }

        public TestamentID Testament
        {
            get
            {
                if (this.T.Equals("ot", StringComparison.InvariantCultureIgnoreCase))
                {
                    return TestamentID.OldTestament;
                }
                else if (this.T.Equals("nt", StringComparison.InvariantCultureIgnoreCase))
                {
                    return TestamentID.NewTestament;
                }
                else
                {
                    throw new Exception("Can't parse testament ID " + this.T + ".");
                }
            }

            set
            {
                switch (value)
                {
                    case TestamentID.ERROR:
                        throw new Exception("Not a testment ID.");
                    case TestamentID.NewTestament:
                        this.T = "NT";
                        break;
                    case TestamentID.OldTestament:
                        this.T = "OT";
                        break;
                }
            }
        }
    }
}
