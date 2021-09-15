using Library.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public class LibraryCardListItem
    {
        public int LibraryCardId { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public Guid AdminId { get; set; }
        public IQueryable BookId { get; set; }
    }
}
