using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PracticalTask1.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        private readonly DataEngine _dataEngine;
        public CategoryController(AppDbContext context)
        {
            _context = context;
            _dataEngine = new DataEngine(context);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateCategory()
        {
            string userRole = HttpContext.Session.GetString("UserRole");
            if (userRole == null || userRole == "User" || userRole == "")
            {
                TempData["msg"] = "You Dont Have Permission of this page";
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(CategoryModel Category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _context.Categories.Add(Category);
                _context.SaveChanges();
                TempData["msg"] = "Added successfully";
                return RedirectToAction("CreateCategory");

            }
            catch (Exception ex)
            {
                TempData["msg"] = "Could not added!!!";
                return View();
            }

        }

        public IActionResult ViewCategory()
        {
            var result = _dataEngine.ExecuteStoredProcedure<CategoryModel>("getCategory");
            return View(result);
        }

        public IActionResult EditCategory(int id)
        {
            var Category = _context.Categories.Find(id);
            return View(Category);
        }

        [HttpPost]
        public IActionResult EditCategory(CategoryModel Category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _context.Categories.Update(Category);
                _context.SaveChanges();
                return RedirectToAction("ViewCategory");

            }
            catch (Exception ex)
            {
                TempData["msg"] = "Could not update!!!";
                return View();
            }

        }

        public IActionResult DeleteCategory(int id)
        {
            try
            {
                var Category = _context.Categories.Find(id);
                if (Category != null)
                {

                    var checkProduct = _context.Products.Any(x => x.categoryId == id);
                    if (checkProduct) {
                        TempData["msg"] = "You Can't Delete this category, There is a Product Mapped with this category";
                        return RedirectToAction("ViewCategory");
                    }

                    _context.Categories.Remove(Category);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {


            }
            return RedirectToAction("ViewCategory");

        }
    }
}
