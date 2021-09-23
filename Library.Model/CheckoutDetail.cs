using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public class CheckoutDetail
    {
        public int CheckoutID { get; set; }
        
        public int BookId { get; set; }
        
        public int LibraryCardId { get; set; }
        
        public string FullName { get; set; }
        
        public DateTime DateOfCheckout { get; set; }
    }
}
