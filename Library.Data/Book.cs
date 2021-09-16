using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public class Book
    {
        [Key]
        [Required]
        public int BookId { get; set; }
        [Required]
        public Guid AdminId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string AuthorName { get; set; }
       [Required]
        public string PublishedDate { get; set; }
       [Required]
        public int Quantity { get; set; }

        [ForeignKey("LibraryCard")]
        public int LibraryCardId { get; set; }
        public virtual LibraryCard LibraryCards { get; set; }
    }
}
