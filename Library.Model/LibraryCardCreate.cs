using Library.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public class LibraryCardCreate

    {
        public int LibraryCardId { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public int Amount { get; set; }
    }
}
