using Microsoft.AspNetCore.Mvc;
using MiniMVC.Data;
using MiniMVC.Models;
using System.Linq;

namespace MiniMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        [HttpGet]
        public IActionResult Goruntule()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string userName, string password)
        {
            var user = _context.Users.FirstOrDefault(u =>
                (u.Username == userName || u.Email == userName) &&
                u.Password == password);

            if (user != null)
            {
                return RedirectToAction("Index","BorrowedBook", new { userId = user.UserId });
            }

            ModelState.AddModelError("", "Kullanıcı adı veya şifre yanlış");
            return View();
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var user = _context.Users.Find(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User user, string pass)
        {
            var userInDb = _context.Users.Find(user.UserId);

            if (userInDb.Password != pass)
            {
                ModelState.AddModelError("", "Şifre yanlış");
                return View(user);
            }

            userInDb.Username = user.Username;
            userInDb.Email = user.Email;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            var user = _context.Users.Find(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
