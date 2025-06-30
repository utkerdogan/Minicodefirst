using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiniMVC.Models
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        public Genre()
        {
            Books = new HashSet<Book>();
        }
    }
}
