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
                TempData["success"] = "Category is Created";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var obj = _db.Categories.Find(id);
            if(id==0||obj==null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Edit( Category obj)
        {
            if(ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Updated ";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var obj = _db.Categories.Find(id);
            if(obj==null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Delete(Category obj)
        {

            if (obj == null)
            {
                return NotFound();
            }
            else
            {
                _db.Categories.Remove(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Delete ";
                return RedirectToAction("Index");
            }
             
        }
    }
}
