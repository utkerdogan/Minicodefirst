using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniMVC.Data;
using MiniMVC.Models;
using MiniMVC.Models.ViewModel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MiniMVC.Controllers
{
    public class BorrowedBookController : Controller
    {
      
        private readonly AppDbContext _context;

        public BorrowedBookController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int userId)
        {
            var books = (from b in _context.Books
                         join a in _context.Authors on b.AuthorId equals a.AuthorId
                         join g in _context.Genres on b.GenreId equals g.GenreId
                         select new BookViewModel
                         {
                             BookId = b.BookId,
                             Title = b.Title,
                             Year = b.Year,
                             AuthorName = a.Name,
                             GenreName = g.Name
                         }).ToList();

            ViewBag.UserId = userId;  

            return View(books);
        }

        [HttpPost]
        public IActionResult Borrow(int bookId, int userId)
        {
            _context.BorrowedBooks.Add(new BorrowedBook
            {
                UserId = userId,
                BookId = bookId,
                BorrowDate = DateTime.Now
            });
            _context.SaveChanges();

            return RedirectToAction("MyBooks","BorrowedBook", new { userId });
        }

        public IActionResult MyBooks(int userId)
        {
            var borrowedBooks = (from bb in _context.BorrowedBooks
                                 where bb.UserId == userId
                                 join b in _context.Books on bb.BookId equals b.BookId
                                 join a in _context.Authors on b.AuthorId equals a.AuthorId
                                 join g in _context.Genres on b.GenreId equals g.GenreId
                                 select new BookViewModel
                                 {
                                     BookId = b.BookId,
                                     Title = b.Title,
                                     Year = b.Year,
                                     AuthorName = a.Name,
                                     GenreName = g.Name
                                 }).ToList();

            ViewBag.UserId = userId;
            return View(borrowedBooks);
        }

        [HttpPost]
        public IActionResult Return(int bookId, int userId)
        {
            var borrowed = _context.BorrowedBooks
                .FirstOrDefault(bb => bb.BookId == bookId && bb.UserId == userId);

            if (borrowed != null)
            {
                _context.BorrowedBooks.Remove(borrowed);
                _context.SaveChanges();
                TempData["Success"] = "Kitap başarıyla geri verildi.";
            }
            else
            {
                TempData["Error"] = "Kitap zaten geri verilmiş veya bulunamadı.";
            }

            return RedirectToAction("MyBooks", new { userId });
        }

    }
}

