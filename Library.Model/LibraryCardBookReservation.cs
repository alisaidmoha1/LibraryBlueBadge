using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public class LibraryCardBookReservation
    {
        public int LibraryCardId { get; set; }
        public int BookId { get; set; }
        public bool IsReserved { get; set; }
        public DateTime ReserveDate { get; set; }
        public DateTime PickUpDate { get; set; }
    }
}
