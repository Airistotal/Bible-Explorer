using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BE.Comparer.Models
{
    public class BibleChapterMeta
    {
        public int mBible;
        public List<int> cBibles { get; set; }
        public int B { get; set; }
        public int C { get; set; }
    }
}