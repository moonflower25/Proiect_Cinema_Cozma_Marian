using Microsoft.AspNetCore.Mvc.RazorPages;
using Proiect_Cinema_Cozma_Marian.Data;

namespace Proiect_Cinema_Cozma_Marian.Models
{
    public class MovieGenrePageModel : PageModel
    {
        public List<AssignedGenreData> AssignedGenreDataList;
        public void PopulateAssignedGenreData(Proiect_Cinema_Cozma_MarianContext context, Movie movie)
        {
            var allGenres = context.Genre;
            var movieGenres = new HashSet<int>(
                movie.MovieGenres.Select(g => g.GenreID)); 
            AssignedGenreDataList = new List<AssignedGenreData>();
            foreach (var gen in allGenres)
            {
                AssignedGenreDataList.Add(new AssignedGenreData
                {
                    GenreID = gen.ID,
                    Name = gen.GenreName,
                    Assigned = movieGenres.Contains(gen.ID)
                });
            }
        }
        public void UpdateMovieGenres(Proiect_Cinema_Cozma_MarianContext context, string[] selectedGenres, Movie movieToUpdate)
        {
            if (selectedGenres == null)
            {
                movieToUpdate.MovieGenres = new List<MovieGenre>();
                return;
            }

            var selectedGenresHS = new HashSet<string>(selectedGenres);
            var movieGenres = new HashSet<int>
                (movieToUpdate.MovieGenres.Select(g => g.Genre.ID));
            foreach (var gen in context.Genre)
            {
                if (selectedGenresHS.Contains(gen.ID.ToString()))
                {
                    if (!movieGenres.Contains(gen.ID))
                    {
                        movieToUpdate.MovieGenres.Add(
                        new MovieGenre
                        {
                            MovieID = movieToUpdate.ID,
                            GenreID = gen.ID
                        });
                    }
                }
                else
                {
                    if (movieGenres.Contains(gen.ID))
                    {
                        MovieGenre courseToRemove
                            = movieToUpdate
                                .MovieGenres
                                .SingleOrDefault(i => i.GenreID == gen.ID);
                        context.Remove(courseToRemove);
                    }
                }
            }
        }
    }
}
