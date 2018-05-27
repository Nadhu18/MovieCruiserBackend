using movieCruiserByRohith.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace movieCruiserByRohith.Data.Persistence
{
    public class MovieRepository : IMovieRepository
    {
        private readonly IMoviesDbContext _context;
        
        public MovieRepository(IMoviesDbContext context) {
            _context = context;
        }

        public List<Movie> GetAllMovies() {
            return _context.Movies.ToList();
        }
    }
}
