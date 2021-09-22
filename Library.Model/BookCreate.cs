using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public class BookCreate
    {
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string AuthorName { get; set; }
        public string PublishedDate { get; set; }
        public int Quantity { get; set; }     
    }
}
