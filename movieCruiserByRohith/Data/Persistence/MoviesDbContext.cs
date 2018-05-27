using Microsoft.EntityFrameworkCore;
using movieCruiserByRohith.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace movieCruiserByRohith.Data.Persistence
{
    public class MoviesDbContext : DbContext,IMoviesDbContext
    {
        public MoviesDbContext() { }

        public MoviesDbContext(DbContextOptions options): base(options) { }

        public DbSet<Movie> Movies { get; set; }
    }
}
