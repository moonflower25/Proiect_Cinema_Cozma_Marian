using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proiect_Cinema_Cozma_Marian.Data;
using Proiect_Cinema_Cozma_Marian.Models;

namespace Proiect_Cinema_Cozma_Marian.Pages.Genres
{
    public class DetailsModel : PageModel
    {
        private readonly Proiect_Cinema_Cozma_Marian.Data.Proiect_Cinema_Cozma_MarianContext _context;

        public DetailsModel(Proiect_Cinema_Cozma_Marian.Data.Proiect_Cinema_Cozma_MarianContext context)
        {
            _context = context;
        }

      public Genre Genre { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Genre == null)
            {
                return NotFound();
            }

            var genre = await _context.Genre.FirstOrDefaultAsync(m => m.ID == id);
            if (genre == null)
            {
                return NotFound();
            }
            else 
            {
                Genre = genre;
            }
            return Page();
        }
    }
}
