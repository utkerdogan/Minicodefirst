using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiniMVC.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public virtual ICollection<BorrowedBook> BorrowedBooks { get; set; }

        public User()
        {
            BorrowedBooks = new HashSet<BorrowedBook>();
        }
    }
}
