using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public class LibraryCardDetail
    {
        public int LibraryCardId { get; set; }
        public string FullName { get; set; }
        public int? BookId { get; set; }
    }
}
