namespace BE.Comparer.Business
{
    using BE.Comparer.Model;

    public interface ITextDiff
    {
        CompareResult GetTextDifferences(string orig, string other, bool ignoreCase = true);
    }
}