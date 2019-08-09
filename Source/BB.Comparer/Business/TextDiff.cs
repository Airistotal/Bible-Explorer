namespace BB.Comparer.Business
{
    using System;
    using System.Collections.Generic;
    using BB.Comparer.Model;

    public class TextDiff : ITextDiff
    {
        public TextDiff()
        {
        }

        public CompareResult GetTextDifferences(string orig, string other, bool ignoreCase = true)
        {
            CompareResult res = new CompareResult();
            Dictionary<Tuple<int, int>, int> tempResult = this.CompareText_Indices(orig, other, ignoreCase);
            Dictionary<Tuple<int, int>, int> tempResultSorted = new Dictionary<Tuple<int, int>, int>();

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

            string[] origs = orig.Trim().Split(' ');
            string[] others = other.Trim().Split(' ');
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
        /// Compares two sentences by word and returns the commonalities
        /// </summary>
        /// <param name="a">the first string to compare</param>
        /// <param name="b">the second string to compare</param>
        /// <param name="ignoreCase">choose whether to ignore the case of the strings a and b</param>
        /// <returns>
        /// A dictionary where the keys are indices (a, b) and the values
        /// are the lengths of the commonalities
        /// </returns>
        private Dictionary<Tuple<int, int>, int> CompareText_Indices(string a, string b, bool ignoreCase = true)
        {
            string compareA = a;
            string compareB = b;
            Dictionary<Tuple<int, int>, int> result = new Dictionary<Tuple<int, int>, int>();
            Dictionary<Tuple<int, int>, int> found = new Dictionary<Tuple<int, int>, int>();
            Dictionary<Tuple<int, int>, Tuple<int, int>> pointers = new Dictionary<Tuple<int, int>, Tuple<int, int>>();

            if (ignoreCase)
            {
                compareA = compareA.ToLower();
                compareB = compareB.ToLower();
            }

            string[] compareAs = compareA.Trim().Split(' ');
            string[] compareBs = compareB.Trim().Split(' ');

            // Get all subphrases that match in a and b
            for (int i = 0; i < compareAs.Length; i++)
            {
                string currA = compareAs[i];

                for (int k = 0; k < compareBs.Length; k++)
                {
                    string currB = compareBs[k];
                    Tuple<int, int> currPos = new Tuple<int, int>(i, k);
                    Tuple<int, int> lastPos = new Tuple<int, int>(i - 1, k - 1);

                    if (currA.Equals(currB))
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

            // Get the dictionary with good coverage
            while (found.Count > 0)
            {
                Tuple<int, int> maxIndex = null;
                int maxCount = 0;

                foreach (Tuple<int, int> key in found.Keys)
                {
                    if (found[key] > maxCount)
                    {
                        maxCount = found[key];
                        maxIndex = key;
                    }
                }

                result.Add(maxIndex, maxCount);

                if (maxIndex != null)
                {
                    // Remove max, on opposite sides, and overlapping from found
                    found.Remove(maxIndex);

                    int i = maxIndex.Item1;
                    int iEnd = i + maxCount - 1;
                    int k = maxIndex.Item2;
                    int kEnd = k + maxCount - 1;

                    List<Tuple<int, int>> keys = new List<Tuple<int, int>>();

                    foreach (Tuple<int, int> key in found.Keys)
                    {
                        int currSize = found[key];
                        int ci = key.Item1;
                        int ciEnd = ci + currSize - 1;
                        int ck = key.Item2;
                        int ckEnd = ck + currSize - 1;

                        if ( // overlapping
                            (ci >= i && ci <= iEnd) ||
                            (ciEnd >= i && ciEnd <= iEnd) ||
                            (ck >= k && ck <= kEnd) ||
                            (ckEnd >= k && ckEnd <= kEnd) ||

                            // on opposite sides
                            (ci < i && ck > kEnd) ||
                            (ck < k && ci > iEnd))
                        {
                            keys.Add(key);
                        }
                    }

                    foreach (Tuple<int, int> key in keys)
                    {
                        found.Remove(key);
                    }
                }
            }

            return result;
        }
    }
}