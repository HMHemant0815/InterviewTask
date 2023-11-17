using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PracticalTask1.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;
        private readonly DataEngine _dataEngine;
        private readonly IMapper _mapper;
        public UserController(AppDbContext context,
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

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["msg"] = ModelState.ValidationState;
                return View();
            }
            try
            {
                _context.Users.Add(userModel);
                _context.SaveChanges();
                TempData["msg"] = "User registered successfully!";
                return RedirectToAction("Register");

            }
            catch (Exception ex)
            {
                TempData["msg"] = "Something Went Wrong!!!";
                return View();
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserModel userModel)
        {
            // Perform user login logic here
            // For demonstration purposes, check if the provided credentials are valid
            var checkData = _context.Users.FirstOrDefault(x => x.Username == userModel.Username && x.Password == userModel.Password);

            if (checkData.Id>0)
            {
                HttpContext.Session.SetInt32("UserId", checkData.Id);;
                HttpContext.Session.SetString("UserRole", checkData.RoleType);

                TempData["msg"] = "Login successful!";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["msg"] = "Invalid username or password";
                return RedirectToAction("Login");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.SetString("UserId","0"); ;
            HttpContext.Session.SetString("UserRole", "");


            return RedirectToAction("Login", "User"); 
        }

    }
}
