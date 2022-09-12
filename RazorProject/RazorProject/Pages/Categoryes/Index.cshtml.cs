using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorProject.Data;
using RazorProject.Model;
namespace RazorProject.Pages.Categoryes
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
         public IEnumerable<Category> Categories { get; set; }
        public IndexModel(ApplicationDbContext db)
        {
            this._db = db;
        }
        public void OnGet()
        {
            Categories = _db.categories.ToList();
        }
    }
}
