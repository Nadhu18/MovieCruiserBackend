using movieCruiserByRohith.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace movieCruiserByRohith.Services
{
    public interface IMovieService
    {
        List<Movie> GetAllMovies();
    }
}
