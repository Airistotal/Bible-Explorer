using BB.Comparer.Business;
using BB.Comparer.Model;
using BB.Infrastructure.Model;
using BB.Infrastructure.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BibleComparerController : ControllerBase
    {
        private readonly IBibleService bibleService;
        private readonly ITextDiff textDiff;

        public BibleComparerController(IBibleService bibleService, ITextDiff textDiff)
        {
            this.bibleService = bibleService;
            this.textDiff = textDiff;
        }

        // GET: api/BibleComparer?mainBible=2&book=1&chapter=1&compareBible=3
        [HttpGet]
        public async Task<string> GetAsync([FromQuery]BibleID mainBible, 
                                           [FromQuery]int book, 
                                           [FromQuery]int chapter, 
                                           [FromQuery]BibleID compareBible)
        {
            var comparison = new Comparison<BibleVerse>((x, y) => x.Verse.CompareTo(y.Verse));
            var mainVerses = await this.bibleService.GetBookChapterVersesAsync(mainBible, book, chapter);
            mainVerses.Sort(comparison);

            List<BibleVerse> compareVerses = null;
            if (compareBible != BibleID.INVALID && compareBible != BibleID.NONE)
            {
                compareVerses = await this.bibleService.GetBookChapterVersesAsync(compareBible, book, chapter);
                compareVerses.Sort(comparison);
            }

            List<ComparedBibleVerse> comparedBibleVerses = new List<ComparedBibleVerse>();
            for (int j = 0; j < mainVerses.Count; j++)
            {
                if (compareVerses != null)
                {
                    comparedBibleVerses.Add(
                        new ComparedBibleVerse(mainVerses[j], GetComparedWords(mainVerses[j], compareVerses[j]))
                    );
                }
            }

            return JsonConvert.SerializeObject(comparedBibleVerses);
        }

        private List<ComparedWord> GetComparedWords(BibleVerse mainVerse, BibleVerse compareVerse)
        {
            CompareResult diff = this.textDiff.GetTextDifferences(mainVerse.Text, compareVerse.Text);

            var mainWords = mainVerse.Text.Trim().Split(' ');
            var compareWords = compareVerse.Text.Trim().Split(' ');

            List<ComparedWord> comparedWords = new List<ComparedWord>();
            Tuple<int, int> lastDiff = null;
            Tuple<int, int> currDiff = diff.GetDifference(0);
            for (int i = 0; i < mainWords.Length; i++)
            {
                string phraseDifference = null;
                if (currDiff != null)
                {
                    phraseDifference = string.Join(' ', compareWords.Skip(currDiff.Item1).Take(currDiff.Item2 - currDiff.Item1));
                }

                var nextDiff = diff.GetDifference(i + 1);
                comparedWords.Add(new ComparedWord()
                {
                    MainWord = mainWords[i],
                    Difference = phraseDifference,
                    IsBeginning = lastDiff == null && textDiff != null,
                    IsEnd = textDiff != null && nextDiff == null
                });

                lastDiff = currDiff;
                currDiff = nextDiff;
            }

            return comparedWords;
        }
    }
}
