using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.UserInterface.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUniteOfWork _unitOfwork;

        public CategoryController(IUniteOfWork unitOfwork)
        {
            _unitOfwork = unitOfwork;
        }
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Category> obj = _unitOfwork.Category.GetAll();
            return View(obj);
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfwork.Category.Add(obj);
                _unitOfwork.Save();
                TempData["success"] = "Category is Created Successfully ";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var obj = _unitOfwork.Category.GetFirstOrDefault(u => u.Id == id);
            if (id == 0 || obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfwork.Category.Update(obj);
                _unitOfwork.Save();
                TempData["success"] = "Category Updated Successfully ";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var obj = _unitOfwork.Category.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
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
                _unitOfwork.Category.Remove(obj);
                _unitOfwork.Save();
                TempData["success"] = "Category Delete Successfully ";
                return RedirectToAction("Index");
            }

        }
    }
}
