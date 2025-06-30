using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiniMVC.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [Range(25, 55, ErrorMessage = "BU KADAR KÜÇÜK YAZAR OLMAZ!")]
        public int Yas { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        public Author()
        {
            Books = new HashSet<Book>();
        }
    }
}
