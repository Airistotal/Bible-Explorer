namespace BE.UI.Controllers.ComparerComponents
{
    using System.Threading.Tasks;
    using BE.Infrastructure.Model;
    using Microsoft.AspNetCore.Mvc;

    public class BiblePage : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(BibleViewInfo bibleViewInfo)
        {
            return this.View(bibleViewInfo);
        }
    }
}
