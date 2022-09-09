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
        [HttpGet]
        public IActionResult Create()
        {
             
            return View();
        }
        [HttpPost]
        public IActionResult Create( Category obj)
        {
            if(ModelState.IsValid)
            {
                _db.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete()
        {

            return View();
        }
    }
}
