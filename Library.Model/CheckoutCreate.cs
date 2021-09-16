using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public class CheckoutCreate
    {
 
        public int BookId { get; set; }
        
        public int LibraryCardId { get; set; }
        
        
        public int Quantity { get; set; }

<<<<<<< HEAD
        //[Required]
       // public string DateOfCheckout { get; set; } look into format for date time otherwise a temporary solution

        public int Month { get; set; }
        public int Year { get; set; }
        public int Day { get; set; }
=======
        public DateTime DateOfCheckout { get; set; }
>>>>>>> 5beee2520e0e6fc8b3d9f07931753185678dddb3
    }
}
