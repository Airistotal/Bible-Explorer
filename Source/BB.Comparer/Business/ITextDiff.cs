namespace BB.Comparer.Business
{
    using BB.Comparer.Model;

    public interface ITextComparer
    {
        CompareResult GetTextDifferences(string orig, string other, bool ignoreCase = true);
    }
}