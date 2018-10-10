namespace BE.Comparer.Model
{
    public class TextChange
    {
        public TextChange()
        {
        }

        public TextChange(int index_from_orig, int index_to_orig, int index_from, int index_to)
        {
            this.IndexFromOrig = index_from_orig;
            this.IndexToOrig = index_to_orig;
            this.IndexFrom = index_from;
            this.IndexTo = index_to;
        }

        public int IndexFromOrig { get; set; }

        public int IndexToOrig { get; set; }

        public int IndexFrom { get; set; }

        public int IndexTo { get; set; }
    }
}