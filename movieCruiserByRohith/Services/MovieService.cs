using movieCruiserByRohith.Data.Models;
using movieCruiserByRohith.Data.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace movieCruiserByRohith.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _repo;

        public MovieService(IMovieRepository repo) {
            _repo = repo;
        }

        public List<Movie> GetAllMovies() {
            return _repo.GetAllMovies();
        }
    }
}
