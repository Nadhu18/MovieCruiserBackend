using Microsoft.EntityFrameworkCore;
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

        int SaveChanges();
    }
}
