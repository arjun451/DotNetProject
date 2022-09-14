using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.UserInterface.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUniteOfWork _unitOfwork;

        public CoverTypeController(IUniteOfWork unitOfwork)
        {
            _unitOfwork = unitOfwork;
        }
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<CoverType> obj = _unitOfwork.CoverType.GetAll();
            return View(obj);
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfwork.CoverType.Add(obj);
                _unitOfwork.Save();
                TempData["success"] = "CoverType is Created Successfully ";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var obj = _unitOfwork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (id == 0 || obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Edit(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfwork.CoverType.Update(obj);
                _unitOfwork.Save();
                TempData["success"] = "CoverType Updated Successfully ";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var obj = _unitOfwork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Delete(CoverType obj)
        {

            if (obj == null)
            {
                return NotFound();
            }
            else
            {
                _unitOfwork.CoverType.Remove(obj);
                _unitOfwork.Save();
                TempData["success"] = "CoverType Delete Successfully ";
                return RedirectToAction("Index");
            }

        }
    }
}
