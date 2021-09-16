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
        [Required]
        public int CheckoutID { get; set; }
        public Guid AdminId { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        public int LibraryCardId { get; set; }
        public virtual Book Books { get; set; }
        public virtual LibraryCard LibraryCards { get; set; }
        [Required]
        public DateTime DateOfCheckout { get; set; }
<<<<<<< HEAD
        
        public Guid AdminId { get; set; }
=======
        [Required]
        public int Quantity { get; set; }
>>>>>>> 5beee2520e0e6fc8b3d9f07931753185678dddb3
    }
}
