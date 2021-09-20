using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public class ReturnBook
    {
        public int BookId { get; set; }
        public int LibraryId { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
