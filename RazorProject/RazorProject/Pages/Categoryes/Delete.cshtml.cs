using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorProject.Data;
using RazorProject.Model;

namespace RazorProject.Pages.Categoryes
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Category Category { get; set; }
        public DeleteModel(ApplicationDbContext db)
        {
            this._db = db;
        }
        public void OnGet(int id)
        {
            Category = _db.categories.Find(id);
        }
        public async Task<IActionResult> OnPost(Category category)
        {

            _db.categories.Remove(category);
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
