using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategory _db;

        public CategoryController(ICategory db)
        {
            this._db = db;
        }
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Category> obj = _db.GetAll();
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
                _db.Save();
                TempData["success"] = "Category is Created Successfully ";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var obj = _db.GetFirstOrDefault(u=>u.Id==id);
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
                _db.Update(obj);
                _db.Save();
                TempData["success"] = "Category Updated Successfully ";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var obj = _db.GetFirstOrDefault(u=>u.Id == id);
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
                _db.Remove(obj);
                _db.Save();
                TempData["success"] = "Category Delete Successfully ";
                return RedirectToAction("Index");
            }
             
        }
    }
}
