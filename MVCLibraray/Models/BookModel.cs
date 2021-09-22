using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCLibraray.Models
{
    public class BookModel
    {
        
        [Required(ErrorMessage="This Field is Required" )]
        public int BookId { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public Guid AdminId { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string ISBN { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string AuthorName { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string PublishedDate { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
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