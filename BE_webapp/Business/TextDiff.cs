using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BE_webapp.Business
{
    public class TextDiff
    {
        public TextDiff() { }

        public static Dictionary<Tuple<int, int>, int> compareText(string a, string b, bool ignoreCase=true)
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
            for( int i = 0; i < _As.Length; i++)
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
                    int iEnd = i + maxCount;
                    int k = maxIndex.Item2;
                    int kEnd = k + maxCount;

                    List<Tuple<int, int>> keys = new List<Tuple<int, int>>();

                    foreach (Tuple<int, int> key in found.Keys)
                    {
                        int currSize = found[key];
                        int ci = key.Item1;
                        int ciEnd = ci + currSize;
                        int ck = key.Item2;
                        int ckEnd = ck + currSize;

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