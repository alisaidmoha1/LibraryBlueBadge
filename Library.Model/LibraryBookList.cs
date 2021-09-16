using Library.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public class LibraryBookList
    {
        public int LibrarCardId { get; set; }
        public int BookId { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
