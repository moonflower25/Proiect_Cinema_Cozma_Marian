using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Proiect_Cinema_Cozma_Marian.Models;

namespace Proiect_Cinema_Cozma_Marian.Data
{
    public class Proiect_Cinema_Cozma_MarianContext : DbContext
    {
        public Proiect_Cinema_Cozma_MarianContext (DbContextOptions<Proiect_Cinema_Cozma_MarianContext> options)
            : base(options)
        {
        }

        public DbSet<Proiect_Cinema_Cozma_Marian.Models.Movie> Movie { get; set; } = default!;

        public DbSet<Proiect_Cinema_Cozma_Marian.Models.Genre> Genre { get; set; }
    }
}
