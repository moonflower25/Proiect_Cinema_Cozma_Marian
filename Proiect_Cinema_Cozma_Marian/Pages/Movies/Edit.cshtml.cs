using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proiect_Cinema_Cozma_Marian.Data;
using Proiect_Cinema_Cozma_Marian.Models;

namespace Proiect_Cinema_Cozma_Marian.Pages.Movies
{
    [Authorize(Roles = "Admin")]
    public class EditModel : MovieGenrePageModel
    {
        private readonly Proiect_Cinema_Cozma_Marian.Data.Proiect_Cinema_Cozma_MarianContext _context;

        public EditModel(Proiect_Cinema_Cozma_Marian.Data.Proiect_Cinema_Cozma_MarianContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Movie Movie { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            Movie = await _context.Movie
                .Include(f => f.MovieGenres).ThenInclude(f => f.Genre)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);


            //var movie =  await _context.Movie.FirstOrDefaultAsync(m => m.ID == id);
            if (Movie == null)
            {
                return NotFound();
            }
            //Movie = movie;

            PopulateAssignedGenreData(_context, Movie);

            return Page();


        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedGenres)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieToUpdate = await _context.Movie
                .Include(i => i.MovieGenres)
                .ThenInclude(i => i.Genre)
                .FirstOrDefaultAsync(s => s.ID == id);

            if (movieToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Movie>(movieToUpdate, "Movie",
                i => i.Title, i => i.Director, i => i.Actor1, i => i.Actor2))
            {
                UpdateMovieGenres(_context, selectedGenres, movieToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            UpdateMovieGenres(_context, selectedGenres, movieToUpdate);
            PopulateAssignedGenreData(_context, movieToUpdate);
            return Page();
        }
    }
}

        /*private bool MovieExists(int id)
        {
          return _context.Movie.Any(e => e.ID == id);
        }*/
    

