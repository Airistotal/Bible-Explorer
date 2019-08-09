namespace BB.Comparer.Business
{
    using BB.Comparer.Model;

    public interface ITextDiff
    {
        CompareResult GetTextDifferences(string orig, string other, bool ignoreCase = true);
    }
}