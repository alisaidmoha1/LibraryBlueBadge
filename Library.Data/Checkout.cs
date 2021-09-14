using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public class Checkout
    {
        [Key]
        public int CheckoutID { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        public int LibraryCardId { get; set; }
        [Required]
        public string FullName { get; set; } 
        [Required]
        public DateTime DateOfCheckout { get; set; }
        
        public Guid AdminId { get; set; }
    }
}
