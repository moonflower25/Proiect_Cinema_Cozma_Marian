using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Proiect_Cinema_Cozma_Marian.Models
{
    public class Movie
    {
        public int ID { get; set; }


        [Display(Name = "Movie Title")]
        public string Title { get; set; }

        public string Director { get; set; }

        [Display(Name = "Actors")]
        public string Actor1 { get; set; }
        [Display(Name = " ")]
        public string Actor2 { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        public ICollection<MovieGenre>? MovieGenres { get; set; }

    }
}
