using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proiect_Cinema_Cozma_Marian.Data;
using Proiect_Cinema_Cozma_Marian.Models;

namespace Proiect_Cinema_Cozma_Marian.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly Proiect_Cinema_Cozma_Marian.Data.Proiect_Cinema_Cozma_MarianContext _context;

        public IndexModel(Proiect_Cinema_Cozma_Marian.Data.Proiect_Cinema_Cozma_MarianContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; } = default!;
        public MovieData MovieD { get; set; }
        public int MovieID { get; set; }
        public int GenreID { get; set; }

        public string TitleSort { get; set; }
        public string DirectorSort { get; set; }

        public string CurrentFilter { get; set; }



        public async Task OnGetAsync(int? id, int? genreID, string sortOrder, string searchString)
        {
            MovieD = new MovieData();
            TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            DirectorSort = String.IsNullOrEmpty(sortOrder) ? "director_desc" : "";

            CurrentFilter = searchString;


            MovieD.Movies = await _context.Movie
                .Include(m => m.MovieGenres)
                .ThenInclude(m => m.Genre)
                .AsNoTracking()
                .OrderBy(m => m.Title)
                .ToListAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                MovieD.Movies = MovieD.Movies.Where(s => s.Director.Contains(searchString)
                                                    || s.Title.Contains(searchString));
            }


            if (id != null)
            {
                MovieID = id.Value;
                Movie movie = MovieD.Movies
                    .Where(i => i.ID == id.Value).Single();

                MovieD.Genres = movie.MovieGenres.Select(s => s.Genre);
            }

            switch (sortOrder)
            {
                case "title_desc":
                    MovieD.Movies = MovieD.Movies.OrderByDescending(s => s.Title);
                    break;
                case "director_desc":
                    MovieD.Movies = MovieD.Movies.OrderByDescending(s => s.Director);
                    break;

            }
        }  
    }
}
