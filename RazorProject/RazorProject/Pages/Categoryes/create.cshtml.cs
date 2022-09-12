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
            if (category.Name ==  category.DisplayOrder.ToString())
            {
                ModelState.AddModelError(string.Empty, "Category name and display order can not be equal");
            }
            if (ModelState.IsValid)
            {
                await _db.AddAsync(category);
                await _db.SaveChangesAsync();
                TempData["success"] = "Category Created Successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
