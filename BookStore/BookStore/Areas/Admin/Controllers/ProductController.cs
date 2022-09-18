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
            
            return View( );
        }
        
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
                productVM.Product = _unitOfwork.Product.GetFirstOrDefault(u => u.Id == id);
                return View(productVM);
            }
            
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
                    if(obj.Product.ImageUrl!=null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.Product.ImageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStreams = new FileStream(Path.Combine(uploads,fileName+extansion), FileMode.Create))
                    {
                        File.CopyTo(fileStreams);
                    }
                    obj.Product.ImageUrl = @"\Images\Product\" + fileName + extansion;
                }
                string success = "";
                if (obj.Product.Id == 0)
                {
                    _unitOfwork.Product.Add(obj.Product);
                    success= "Product Created Successfully ";
                }
                else
                {
                    _unitOfwork.Product.Update(obj.Product);
                    success = "Product Update Successfully ";
                }
                _unitOfwork.Save();
                TempData["success"] = success;
                return RedirectToAction("Index");
            }
            return View(obj);
        }
       
        
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfwork.Product.GetAll(includeProprites:"Category,CoverType");
            return Json(new {data=productList});    
        }

        [HttpDelete]
        public IActionResult Delete(int?id)
        {
            var obj = _unitOfwork.Product.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error While deleting" });
            }
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            var oldImagePath = Path.Combine(wwwRootPath, obj.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfwork.Product.Remove(obj);
                _unitOfwork.Save();
                
                return Json(new {success = true,message = "Delete Successful"});
            

        }
        #endregion
    }


}
