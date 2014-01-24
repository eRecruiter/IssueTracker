using EntityFramework.Extensions;
using ePunkt.IssueTracker.Models;
using System.Threading.Tasks;

namespace ePunkt.IssueTracker.Code
{
    public class DeleteTagsService
    {
        private readonly Db _db;

        public DeleteTagsService(Db db)
        {
            _db = db;
        }

        public async Task DeleteAllTags()
        {
            _db.IssueTags.Delete();
            await _db.SaveChangesAsync();
        }

        public async Task DeleteTags(int issueId)
        {
            /*foreach (var tag in _db.IssueTags.Where(x => x.IssueId == issueId))
                _db.IssueTags.Remove(tag);
            await _db.SaveChangesAsync();*/
            _db.IssueTags.Delete(x => x.IssueId == issueId);
            await _db.SaveChangesAsync();
        }
    }
}