using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Model.Models;
using BookStore.Model.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.UserInterface.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUniteOfWork _unitOfwork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUniteOfWork unitOfwork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfwork = unitOfwork;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Product> obj = _unitOfwork.Product.GetAll();
            return View(obj);
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        //[HttpPost]
        //public IActionResult Create(Product obj)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfwork.Product.Add(obj);
        //        _unitOfwork.Save();
        //        TempData["success"] = "Product is Created Successfully ";
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}
        [HttpGet]
        public IActionResult Upsert(int? id)
        {
             
            ProductVM productVM = new ProductVM()
            {
                Product =  new(),
                CategoryList = _unitOfwork.Category.GetAll().Select(
                              u => new SelectListItem
                               {
                                  Text = u.Name,
                                  Value = u.Id.ToString()
                               }),
                CoverTypeList = _unitOfwork.CoverType.GetAll().Select(
               u => new SelectListItem
               {
                   Text = u.Name,
                   Value = u.Id.ToString()
               })

        };
             
            if (id == 0||id==null)
            {
                //create product
                
                return View(productVM);
            }
            else
            {
                //update product
            }
            return View(productVM);
        }
        [HttpPost]
        public IActionResult Upsert(ProductVM obj,IFormFile? File )
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(File!=null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"Images\Product");
                    var extansion = Path.GetExtension(File.FileName);
                    using (var fileStreams = new FileStream(Path.Combine(uploads,fileName+extansion), FileMode.Create))
                    {
                        File.CopyTo(fileStreams);
                    }
                    obj.Product.ImageUrl = @"\Images\Product\" + fileName + extansion;
                }
                _unitOfwork.Product.Add(obj.Product);
                _unitOfwork.Save();
                TempData["success"] = "Product Created Successfully ";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var obj = _unitOfwork.Product.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Delete(Product obj)
        {

            if (obj == null)
            {
                return NotFound();
            }
            else
            {
                _unitOfwork.Product.Remove(obj);
                _unitOfwork.Save();
                TempData["success"] = "Category Delete Successfully ";
                return RedirectToAction("Index");
            }

        }
    }
}
