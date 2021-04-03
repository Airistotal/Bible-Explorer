namespace BB.Comparer.Business
{
  using System;
  using System.Collections.Generic;

  using BB.Comparer.Model;

  public class TextComparer : ITextComparer
  {
    public TextComparer()
    {
    }

    public CompareResult GetTextDifferences(string orig, string other, bool ignoreCase = true)
    {
      var res = new CompareResult();
      var tempResult = this.CompareText_Indices(orig, other, ignoreCase);
      var tempResultSorted = new Dictionary<Tuple<int, int>, int>();

      while (tempResult.Count > 0)
      {
        Tuple<int, int> min_key = null;

        // Sort tempResult by Tuple.Item1
        foreach (var key in tempResult.Keys)
        {
          // TODO: If this has runtime issues, use a more complicated sort
          if (min_key == null || min_key.Item1 > key.Item1)
          {
            min_key = key;
          }
        }

        tempResultSorted.Add(min_key, tempResult[min_key]);
        tempResult.Remove(min_key);
      }

      // Change the sorted matches into differences
      Tuple<int, int> prev_key = null;
      foreach (var key in tempResultSorted.Keys)
      {
        if (prev_key == null)
        {
          if (key.Item1 != 0 || key.Item2 != 0)
          {
            res.Differences.Add(new TextChange(0, key.Item1, 0, key.Item2));
          }
        }
        else
        {
          res.Differences.Add(
              new TextChange(
                  prev_key.Item1 + tempResultSorted[prev_key],
                  key.Item1,
                  prev_key.Item2 + tempResultSorted[prev_key],
                  key.Item2));
        }

        prev_key = key;
      }

      var origs = orig.Trim().Split(' ');
      var others = other.Trim().Split(' ');
      if (prev_key.Item1 + tempResultSorted[prev_key] < origs.Length ||
          prev_key.Item2 + tempResultSorted[prev_key] < others.Length)
      {
        res.Differences.Add(
            new TextChange(
                prev_key.Item1 + tempResultSorted[prev_key],
                origs.Length,
                prev_key.Item2 + tempResultSorted[prev_key],
                others.Length));
      }

      return res;
    }

    /// <summary>
    /// Compares two sentences by word and returns the commonalities.
    /// </summary>
    /// <param name="a">the first string to compare.</param>
    /// <param name="b">the second string to compare.</param>
    /// <param name="ignoreCase">choose whether to ignore the case of the strings a and b.</param>
    /// <returns>
    /// A dictionary where the keys are indices (a, b) and the values
    /// are the lengths of the commonalities.
    /// </returns>
    private Dictionary<Tuple<int, int>, int> CompareText_Indices(string a, string b, bool ignoreCase = true)
    {
      return this.GetHighestCoverageMatchingSequences(
        this.GetMatchingSequences(
          (ignoreCase ? a : a.ToLower()).Trim().Split(' '),
          (ignoreCase ? b : b.ToLower()).Trim().Split(' ')));
    }

    /// <summary>
    /// Finds a set of matching sequences that don't overlap, but have good coverage over
    /// the originals used to create the matching sequences.
    /// </summary>
    /// <param name="allMatchingSequences">All matching sequences.</param>
    /// <returns>A dictionary where the keys are indices (a, b) and the values
    /// are the lengths of the commonalities, where the indices+length don't overlap</returns>
    private Dictionary<Tuple<int, int>, int> GetHighestCoverageMatchingSequences(
      Dictionary<Tuple<int, int>, int> allMatchingSequences)
    {
      // Get the dictionary with good coverage
      var filteredMatchingSequences = new Dictionary<Tuple<int, int>, int>();
      while (allMatchingSequences.Count > 0)
      {
        Tuple<int, int> maxKey = null;
        int maxCount = 0;

        // Get the largest overlap
        foreach (Tuple<int, int> key in allMatchingSequences.Keys)
        {
          if (allMatchingSequences[key] > maxCount)
          {
            maxCount = allMatchingSequences[key];
            maxKey = key;
          }
        }

        filteredMatchingSequences.Add(maxKey, maxCount);

        if (maxKey != null)
        {
          // Remove max, on opposite sides, and overlapping from found
          var i = maxKey.Item1;
          var iEnd = i + maxCount - 1;
          var k = maxKey.Item2;
          var kEnd = k + maxCount - 1;

          allMatchingSequences.Remove(maxKey);

          foreach (var key in allMatchingSequences.Keys)
          {
            var currSize = allMatchingSequences[key];
            var ci = key.Item1;
            var ciEnd = ci + currSize - 1;
            var ck = key.Item2;
            var ckEnd = ck + currSize - 1;

            if (// overlapping
                (ci >= i && ci <= iEnd) ||
                (ciEnd >= i && ciEnd <= iEnd) ||
                (ck >= k && ck <= kEnd) ||
                (ckEnd >= k && ckEnd <= kEnd) ||

                // on opposite sides
                (ci < i && ck > kEnd) ||
                (ck < k && ci > iEnd))
            {
              allMatchingSequences.Remove(key);
            }
          }
        }
      }

      return filteredMatchingSequences;
    }

    /// <summary>
    /// Gets a list of all matching sequences in two lists of strings.
    /// </summary>
    /// <param name="wordsA">The first list of words to compare.</param>
    /// <param name="wordsB">The second list of words to compare.</param>
    /// <returns>
    /// A dictionary that has the starting index in wordsA and wordsB as the Tuple key
    /// with the number of matching words as the value.
    /// </returns>
    private Dictionary<Tuple<int, int>, int> GetMatchingSequences(string[] wordsA, string[] wordsB)
    {
      var found = new Dictionary<Tuple<int, int>, int>();
      var pointers = new Dictionary<Tuple<int, int>, Tuple<int, int>>();

      for (var i = 0; i < wordsA.Length; i++)
      {
        for (var k = 0; k < wordsB.Length; k++)
        {
          var currPos = new Tuple<int, int>(i, k);
          var lastPos = new Tuple<int, int>(i - 1, k - 1);

          if (wordsA[i].Equals(wordsB[k]))
          {
            if (pointers.ContainsKey(lastPos))
            {
              if (pointers[lastPos] == null)
              {
                found[lastPos]++;
                pointers.Add(currPos, lastPos);
              }
              else
              {
                found[pointers[lastPos]]++;
                pointers.Add(currPos, pointers[lastPos]);
              }
            }
            else
            {
              found.Add(currPos, 1);
              pointers.Add(currPos, null);
            }
          }
        }
      }

      return found;
    }
  }
}