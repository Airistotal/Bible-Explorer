using BE_webapp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BE_webapp.Controllers
{
    public class BibleComparerController : Controller
    {
        // GET: BibleComparer
        public ActionResult Index(DataTable Bibles = null, int B = 1, int C = 1)
        {
            if(Bibles == null)
            {
                Bibles = new DataTable();

                var dataColumn = new DataColumn("Bible", typeof(int));
                Bibles.Columns.Add(dataColumn);

                var row = Bibles.NewRow();

                row["Bible"] = 1;

                Bibles.Rows.Add(row);
            }

            return View(new BibleChapter() { Bible = Bibles, B = B, C = C });
        }

        public ActionResult GetPage(BibleChapter page)
        {
            return PartialView("BiblePage", page);
        }
    }
}