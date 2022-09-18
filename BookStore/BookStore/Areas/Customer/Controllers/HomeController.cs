using BookStore.DataAccess.Repository;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Model.Models;
using BookStore.Model.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookStore.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IUniteOfWork _unitOfwork;

        public HomeController(ILogger<HomeController> logger, IUniteOfWork uniteOfWork)
        {
            _logger = logger;
            _unitOfwork = uniteOfWork;
        }
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfwork.Product.GetAll(includeProprites: "Category,CoverType");
            return View(productList);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            ShoppingCart objCart = new()
            {
                Count = 1,
                Product = _unitOfwork.Product.GetFirstOrDefault(u => u.Id == id, includeProprites: "Category,CoverType")

            };
              
            return View(objCart);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}