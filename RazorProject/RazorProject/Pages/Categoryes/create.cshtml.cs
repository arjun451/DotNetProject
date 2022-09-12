using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorProject.Data;
using RazorProject.Model;

namespace RazorProject.Pages.Categoryes
{
    public class createModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public createModel(ApplicationDbContext db)
        {
            this._db = db;
        }
        public Category Category { get; set; }
        public void OnGet()
        {
        }
        public async  Task<IActionResult> OnPost(Category category)
        {
              await _db.AddAsync(category);
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
