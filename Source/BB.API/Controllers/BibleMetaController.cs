namespace BB.API.Controllers
{
  using System.Linq;

  using BB.Infrastructure.Model;
  using BB.Infrastructure.Model.ApiContract;
  using BB.Infrastructure.Service;
  using Microsoft.AspNetCore.Mvc;
  using Newtonsoft.Json;

  [Route("api/[controller]")]
  [ApiController]
  public class BibleMetaController : ControllerBase
  {
    private readonly IBibleService bibleService;

    public BibleMetaController(IBibleService bibleService)
    {
      this.bibleService = bibleService;
    }

    // get: api/BibleMeta/GetBibles
    [Route("GetBibles")]
    public string GetBibles()
    {
      var bibleVersions = this.bibleService.GetBibleVersions();
      var bibleVersionsForDropdown = bibleVersions.Select(bible =>
      {
        return new DropDownData() { Text = bible.Abbreviation, Value = bible.ID };
      }).ToList();

      return JsonConvert.SerializeObject(bibleVersionsForDropdown);
    }

    // get: api/BibleMeta/GetBibleBooks
    [Route("GetBibleBooks")]
    public string GetBibleBooks()
    {
      var bibleBooks = this.bibleService.GetBibleBooks();
      var bibleBooksForDropdown = bibleBooks.Select(book =>
      {
        return new DropDownData() { Text = book.Abbreviation, Value = book.Id };
      });

      return JsonConvert.SerializeObject(bibleBooksForDropdown);
    }

    // get: api/BibleMeta/GetChaptersForBookInBible?bible=1&book=1
    [Route("GetChaptersForBookInBible")]
    public string GetChaptersForBookInBible([FromQuery]BibleID bible, [FromQuery]int book)
    {
      var numberOfChapters = this.bibleService.GetNumberOfChaptersForBookInBible(bible, book);
      var chapterDropdown = Enumerable.Range(1, numberOfChapters)
                                      .Select(ch => new DropDownData() { Text = ch.ToString(), Value = ch });

      return JsonConvert.SerializeObject(chapterDropdown);
    }
  }
}
