using Microsoft.EntityFrameworkCore;
using movieCruiserByRohith.Data.Models;

namespace movieCruiserByRohith.Data.Persistence
{
    public class MoviesDbContext : DbContext,IMoviesDbContext
    {
        public MoviesDbContext() { }

        public MoviesDbContext(DbContextOptions options): base(options) { }

        public DbSet<Movie> Movies { get; set; }
    }
}
