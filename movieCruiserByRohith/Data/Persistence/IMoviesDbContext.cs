using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using movieCruiserByRohith.Data.Models;

namespace movieCruiserByRohith.Data.Persistence
{
    public interface IMoviesDbContext
    {
        DbSet<Movie> Movies { get; set; }

        EntityEntry Entry(object entity);

        int SaveChanges();
    }
}
