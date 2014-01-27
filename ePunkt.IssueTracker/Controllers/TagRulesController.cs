using System.Linq;
using EntityFramework.Extensions;
using ePunkt.IssueTracker.Code;
using ePunkt.IssueTracker.Models;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ePunkt.IssueTracker.Controllers
{
    [Authorize]
    public class TagRulesController : Controller
    {
        private readonly Db _db = new Db();

        public async Task<ActionResult> Index()
        {
            return View(await _db.TagRules.OrderBy(x => x.Group).ThenBy(x => x.Tag).ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var tagrule = await _db.TagRules.FirstOrDefaultAsync(x => x.Id == id);
            if (tagrule == null)
                return HttpNotFound();
            return View(tagrule);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Tag,Group,TextRegex,CreatorRegex,ServerVariablesRegex,StackTraceRegex")] TagRule tagrule)
        {
            if (ModelState.IsValid)
            {
                _db.TagRules.Add(tagrule);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tagrule);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var tagrule = await _db.TagRules.FirstOrDefaultAsync(x => x.Id == id);
            if (tagrule == null)
                return HttpNotFound();
            return View(tagrule);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Tag,Group,TextRegex,CreatorRegex,ServerVariablesRegex,StackTraceRegex")] TagRule tagrule)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(tagrule).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tagrule);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var tagrule = await _db.TagRules.FirstOrDefaultAsync(x => x.Id == id);
            if (tagrule == null)
                return HttpNotFound();
            return View(tagrule);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var tagrule = await _db.TagRules.FirstOrDefaultAsync(x => x.Id == id);
            _db.IssueTags.Delete(x => x.Tag.ToLower() == tagrule.Tag.ToLower());
            _db.TagRules.Remove(tagrule);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Reset()
        {
            await new DeleteTagsService(_db).DeleteAllTags();
            new AddTagsService(_db).AddTagsForAllIssues();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
