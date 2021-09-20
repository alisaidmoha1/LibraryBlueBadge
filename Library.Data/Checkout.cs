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

        [Required]
        public int Quantity { get; set; }

    }
}
