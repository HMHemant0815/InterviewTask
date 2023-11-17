using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PracticalTask1;
using System;
using System.Security.Cryptography;

namespace PracticalTask1.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly DataEngine _dataEngine;
        private readonly IMapper _mapper;
        public ProductController(AppDbContext context,
            IMapper mapper)
        {
            _context = context;
            _dataEngine = new DataEngine(context);
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateProduct()
        {
            string userRole = HttpContext.Session.GetString("UserRole");
            if (userRole==null ||userRole == "User" || userRole == "")
            {
                TempData["msg"] = "You Dont Have Permission of this page";
                return RedirectToAction("Index","Home");
            }

            var categories = _context.Categories.ToList();
            var viewModel = new ProductModel
            {
                CategoryList = categories.Select(c => new SelectListItem
                {
                    Value = c.categoryId.ToString(),
                    Text = c.categoryName
                }).ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductModel Product)
        {
            if (!ModelState.IsValid)
            {
                TempData["msg"] = ModelState.ValidationState;
                return View();
            }
            try
            {
                _context.Products.Add(Product);
                _context.SaveChanges();
                TempData["msg"] = "Added successfully";
                return RedirectToAction("CreateProduct");

            }
            catch (Exception ex)
            {
                TempData["msg"] = "Could not added!!!";
                return View();
            }

        }

        public IActionResult ViewProduct()
        {
            var result = _dataEngine.ExecuteStoredProcedure<ViewProductDTO>("getProductData");
            return View(result);
        }

     
        public IActionResult EditProduct(int id)
        {
            var Product = _context.Products.Find(id);
            var categories = _context.Categories.ToList();
            var viewModel = new ProductModel
            {
                CategoryList = categories.Select(c => new SelectListItem
                {
                    Value = c.categoryId.ToString(),
                    Text = c.categoryName
                }).ToList()
            };
            Product.CategoryList = viewModel.CategoryList;
            return View(Product);
        }

        [HttpPost]
        public IActionResult EditProduct(ProductModel  Product)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _context.Products.Update(Product);
                _context.SaveChanges();
                return RedirectToAction("ViewProduct");

            }
            catch (Exception ex)
            {
                TempData["msg"] = "Could not update!!!";
                return View();
            }

        }

        public IActionResult DeleteProduct(int id)
        {
            try
            {
                var Product = _context.Products.Find(id);
                if (Product != null)
                {
                    _context.Products.Remove(Product);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {


            }
            return RedirectToAction("ViewProduct");

        }
    }
}
