using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorProject.Data;
using RazorProject.Model;

namespace RazorProject.Pages.Categoryes
{

    public class EditeModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Category Category { get; set; }
        public EditeModel(ApplicationDbContext db)
        {
            this._db = db;
        }
        public void OnGet(int id)
        {
            Category = _db.categories.Find(id);

        }

        public async Task<IActionResult> OnPost(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.categories.Update(category);
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
