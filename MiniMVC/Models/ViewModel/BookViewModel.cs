using System;
namespace MiniMVC.Models.ViewModel
{
    public class BookViewModel
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string AuthorName { get; set; }
        public string GenreName { get; set; }
    }
}

