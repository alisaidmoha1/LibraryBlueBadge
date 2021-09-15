using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public class LibraryCard
    {
        [Key]
        public int LibraryCardId { get; set; }
        [Required]
        public string FullName { get; set; }
        public string Address { get; set; }
        public Guid AdminId { get; set; }

        public int BookId { get; set; }
        public virtual Book Books { get; set; }
        public ICollection<Book> book {get; set;}
    }
}

