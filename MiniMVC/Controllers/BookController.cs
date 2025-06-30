using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniMVC.Data;
using MiniMVC.Models;
using System.Linq;

namespace MiniMVC.Controllers
{
    public class BookController : Controller
    {
        private readonly AppDbContext _context;

        public BookController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index(string searchText, string authorSearch, int? genreId, string sortOrder)
        {
            var query = _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(b => b.Title.Contains(searchText));
            }

            if (!string.IsNullOrEmpty(authorSearch))
            {
                query = query.Where(b => b.Author.Name.Contains(authorSearch));
            }

            if (genreId.HasValue)
            {
                query = query.Where(b => b.GenreId == genreId.Value);
            }

            query = sortOrder switch
            {
                "za" => query.OrderByDescending(b => b.Title),
                _ => query.OrderBy(b => b.Title)
            };

            var books = query.ToList();

            ViewBag.Genres = _context.Genres.ToList();
            ViewBag.SelectedGenreId = genreId;
            ViewBag.SearchText = searchText;
            ViewBag.AuthorSearch = authorSearch;
            ViewBag.SortOrder = sortOrder;

            return View(books);
        }



        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Authors = new SelectList(_context.Authors, "AuthorId", "Name");
            ViewBag.Genres = new SelectList(_context.Genres, "GenreId", "Name");

            return View();
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            
            _context.Books.Add(book);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var book = _context.Books.Find(id);
            ViewBag.Authors = new SelectList(_context.Authors, "AuthorId", "Name", book.AuthorId);
            ViewBag.Genres = new SelectList(_context.Genres, "GenreId", "Name", book.GenreId);
            return View(book);
        }

        [HttpPost]
        public IActionResult Edit(Book book)
        {

            _context.Books.Update(book);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var book = _context.Books.Find(id);
            _context.Books.Remove(book);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
