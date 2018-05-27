using movieCruiserByRohith.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace movieCruiserByRohith.Data.Persistence
{
    public interface IMovieRepository
    {
        List<Movie> GetAllMovies();
    }
}
