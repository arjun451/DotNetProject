using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUniteOfWork _db;

        public CategoryController(IUniteOfWork db)
        {
            this._db = db;
        }
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Category> obj = _db.Category.GetAll();
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
                _db.Category.Add(obj);
                _db.Save();
                TempData["success"] = "Category is Created Successfully ";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var obj = _db.Category.GetFirstOrDefault(u=>u.Id==id);
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
                _db.Category.Update(obj);
                _db.Save();
                TempData["success"] = "Category Updated Successfully ";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var obj = _db.Category.GetFirstOrDefault(u=>u.Id == id);
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
                _db.Category.Remove(obj);
                _db.Save();
                TempData["success"] = "Category Delete Successfully ";
                return RedirectToAction("Index");
            }
             
        }
    }
}
