namespace BE.Infrastructure.Model
{
    using System;
    using System.Collections.Generic;

    public class BibleBook
    {
        private string t;

        public int Book { get; set; }

        public string Name { get; set; }

        public int GenreID { get; set; }

        public BibleBookGenre Genre { get; set; }

        public List<BibleBookAbbreviation> Abbreviations { get; set; }

        public TestamentID Testament
        {
            get
            {
                if (this.t.Equals("ot", StringComparison.InvariantCultureIgnoreCase))
                {
                    return TestamentID.OldTestament;
                }
                else if (this.t.Equals("nt", StringComparison.InvariantCultureIgnoreCase))
                {
                    return TestamentID.NewTestament;
                }
                else
                {
                    throw new Exception("Can't parse testament ID " + this.t + ".");
                }
            }

            set
            {
                switch (value)
                {
                    case TestamentID.ERROR:
                        throw new Exception("Not a testment ID.");
                    case TestamentID.NewTestament:
                        this.t = "NT";
                        break;
                    case TestamentID.OldTestament:
                        this.t = "OT";
                        break;
                }
            }
        }
    }
}
