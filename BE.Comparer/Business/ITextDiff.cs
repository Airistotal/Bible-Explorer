namespace BE.Comparer.Business
{
    using BE.Comparer.Models;

    public interface ITextDiff
    {
        CompareResult GetTextDifferences(string orig, string other, bool ignoreCase = true);
    }
}