using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using movieCruiserByRohith.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace movieCruiserByRohith.Data.Persistence
{
    public interface IMoviesDbContext
    {
        DbSet<Movie> Movies { get; set; }

        EntityEntry Entry(object entity);

        int SaveChanges();
    }
}
