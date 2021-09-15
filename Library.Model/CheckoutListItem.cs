using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public class CheckoutListItem
    {
        public int CheckoutID { get; set; }
        
        public int BookId { get; set; }
        
        public int LibraryCardId { get; set; }
        
        public int Quantity { get; set; }
        public DateTime DateOfCheckout { get; set; }
    }
}
