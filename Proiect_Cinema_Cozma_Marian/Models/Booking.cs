using System.ComponentModel.DataAnnotations;

namespace Proiect_Cinema_Cozma_Marian.Models
{
    public class Booking
    {
        public int ID { get; set; }

        public int? MemberID { get; set; }

        public Member? Member { get; set; }

        public int? MovieID { get; set; }

        public Movie? Movie { get; set; }

        [DataType(DataType.Date)]
        public DateTime BookingDate { get; set; }
    }
}
