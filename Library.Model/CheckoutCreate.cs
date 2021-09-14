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
        [Required]
        public int CheckoutID { get; set; }
       
        [Required]
        public int BookId { get; set; }
        
        [Required]
        public int LibraryCardId { get; set; }
        
        [Required]
        public string FullName { get; set; }

        //[Required]
       // public string DateOfCheckout { get; set; }

        public int Month { get; set; }
        public int Year { get; set; }
        public int Day { get; set; }
    }
}
