namespace BB.Comparer.Model
{
    public class ComparedWord
    {
        public string MainWord { get; set; }

        public string Difference { get; set; }

        public bool IsEnd { get; set; }

        public bool IsBeginning { get; set; }
    }
}
