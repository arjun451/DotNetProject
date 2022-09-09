using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            this._db = db;
        }
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Category> obj = _db.Categories.ToList();
            return View(obj);
        }
    }
}
