using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proiect_Cinema_Cozma_Marian.Data;
using Proiect_Cinema_Cozma_Marian.Models;

namespace Proiect_Cinema_Cozma_Marian.Pages.Members
{
    public class IndexModel : PageModel
    {
        private readonly Proiect_Cinema_Cozma_Marian.Data.Proiect_Cinema_Cozma_MarianContext _context;

        public IndexModel(Proiect_Cinema_Cozma_Marian.Data.Proiect_Cinema_Cozma_MarianContext context)
        {
            _context = context;
        }

        public IList<Member> Member { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Member != null)
            {
                Member = await _context.Member.ToListAsync();
            }
        }
    }
}
