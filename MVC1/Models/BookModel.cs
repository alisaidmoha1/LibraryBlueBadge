using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC1.Models
{
    public class BookModel
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

        //[ForeignKey("LibraryCard")]
        //public int LibraryCardId { get; set; }
        //public virtual LibraryCard LibraryCards { get; set; }

        //public virtual ICollection<LibraryCard> ListOfLibraryCards { get; set; }

        //public Book()
        //{
        //    ListOfLibraryCards = new HashSet<LibraryCard>();
        //}
    }
}