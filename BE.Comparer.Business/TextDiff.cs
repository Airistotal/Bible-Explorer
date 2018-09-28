namespace BE.Comparer.Business
{
    using BE.Comparer.Models;
    using System;
    using System.Collections.Generic;

    public class TextDiff : ITextDiff
    {
        public TextDiff() { }

        public CompareResult GetTextDifferences(string orig, string other, bool ignoreCase = true)
        {
            CompareResult res = new CompareResult();
            Dictionary<Tuple<int, int>, int> _res = compareText_Indices(orig, other, ignoreCase);
            Dictionary<Tuple<int, int>, int> _res_sorted = new Dictionary<Tuple<int, int>, int>();

            while (_res.Count > 0)
            {
                Tuple<int, int> min_key = null;

                // Sort _res by Tuple.Item1
                foreach (var key in _res.Keys)
                {
                    //TODO: If this has runtime issues, use a more complicated sort
                    if (min_key == null || min_key.Item1 > key.Item1)
                    {
                        min_key = key;
                    }
                }

                _res_sorted.Add(min_key, _res[min_key]);
                _res.Remove(min_key);
            }

            // Change the sorted matches into differences
            Tuple<int, int> prev_key = null;
            foreach (var key in _res_sorted.Keys)
            {
                if (prev_key == null)
                {
                    if (key.Item1 != 0 || key.Item2 != 0)
                    {
                        res.Differences.Add(
                            new TextChange(0, key.Item1, 0, key.Item2)
                        );
                    }
                }
                else
                {
                    res.Differences.Add(
                        new TextChange(
                            prev_key.Item1 + _res_sorted[prev_key], key.Item1,
                            prev_key.Item2 + _res_sorted[prev_key], key.Item2)
                    );
                }

                prev_key = key;
            }

            string[] _Origs = orig.Trim().Split(' ');
            string[] _Others = other.Trim().Split(' ');
            if (prev_key.Item1 + _res_sorted[prev_key] < _Origs.Length ||
                prev_key.Item2 + _res_sorted[prev_key] < _Others.Length)
            {
                res.Differences.Add(
                    new TextChange(
                        prev_key.Item1 + _res_sorted[prev_key], _Origs.Length,
                        prev_key.Item2 + _res_sorted[prev_key], _Others.Length
                    )
                );
            }

            return res;
        }

        /// <summary>
        /// Compares two sentences by word and returns the commonalities
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="ignoreCase"></param>
        /// <returns>A dictionary where the keys are indices (a, b) and the values are the lengths of the commonalities</returns>
        private Dictionary<Tuple<int, int>, int> compareText_Indices(string a, string b, bool ignoreCase=true)
        {
            string _a = a;
            string _b = b;
            Dictionary<Tuple<int, int>, int> result = new Dictionary<Tuple<int, int>, int>();
            Dictionary<Tuple<int, int>, int> found = new Dictionary<Tuple<int, int>, int>();
            Dictionary<Tuple<int, int>, Tuple<int, int>> pointers = new Dictionary<Tuple<int, int>, Tuple<int, int>>();

            if (ignoreCase)
            {
                _a = _a.ToLower();
                _b = _b.ToLower();
            }

            string[] _As = _a.Trim().Split(' ');
            string[] _Bs = _b.Trim().Split(' ');

            // Get all subphrases that match in a and b
            for ( int i = 0; i < _As.Length; i++)
            {
                string currA = _As[i];

                for (int k = 0; k < _Bs.Length; k++)
                {
                    string currB = _Bs[k];
                    Tuple<int, int> currPos = new Tuple<int, int>(i, k);
                    Tuple<int, int> lastPos = new Tuple<int, int>(i - 1, k - 1);

                    if(currA.Equals(currB))
                    {
                        if(pointers.ContainsKey(lastPos))
                        {
                            if(pointers[lastPos] == null)
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
            while(found.Count > 0)
            {
                Tuple<int, int> maxIndex = null;
                int maxCount = 0;

                foreach(Tuple<int, int> key in found.Keys)
                {
                    if(found[key] > maxCount)
                    {
                        maxCount = found[key];
                        maxIndex = key;
                    }
                }

                result.Add(maxIndex, maxCount);

                if(maxIndex != null)
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
                            (ck < k && ci > iEnd)
                           )
                        {
                            keys.Add(key);
                        }
                    }

                    foreach(Tuple<int, int> key in keys)
                    {
                        found.Remove(key);
                    }
                }
            }

            return result;
        }
    }
}